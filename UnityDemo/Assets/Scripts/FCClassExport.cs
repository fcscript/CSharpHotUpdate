using System;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

class FCClassExport
{
    Type m_nClassType;
    string m_szPathName;
    StringBuilder m_szTempBuilder = new StringBuilder(1024 * 1024 * 1);
    Dictionary<Type, int> m_OuterType = new Dictionary<Type, int>();
    List<Type> m_DelayType = new List<Type>();

    public void ExportClass(Type nClassType, string szPathName)
    {
        m_nClassType = nClassType;
        m_szPathName = szPathName;
        m_DelayType.Clear();
        m_szTempBuilder.Length = 0;

        m_szTempBuilder.AppendFormat("\r\nclass  {0}\r\n", nClassType.Name);
        m_szTempBuilder.AppendLine("{");

        MakeInnerEnum();
        MakeConstructor();
        MakeProperty();
        MakeMethod();
        m_szTempBuilder.AppendLine("};\r\n");

        // 导出类外的枚举
        MakeOuterEnum();

        File.WriteAllText(m_szPathName, m_szTempBuilder.ToString());
    }

    void MakeConstructor()
    {
        ConstructorInfo[] allConInfos = m_nClassType.GetConstructors(); // 得到构造函数信息
        if (allConInfos == null)
            return;
        foreach(ConstructorInfo cons in allConInfos)
        {
            PushConstructor(cons);
        }
    }
    void PushConstructor(ConstructorInfo cons)
    {
        ParameterInfo[] allParams = cons.GetParameters();
        string szCallParam = string.Empty;
        Type nParamType;
        if(allParams != null)
        {
            for (int i = 0; i < allParams.Length; ++i)
            {
                ParameterInfo param = allParams[i];
                nParamType = param.ParameterType;
                PushDelayType(nParamType);
                if (i > 0)
                    szCallParam += ',';
                szCallParam += FCValueType.GetTypeDesc(nParamType);
                szCallParam = szCallParam + " " + param.Name;
            }
        }
        m_szTempBuilder.AppendFormat("    public {0}({1});\r\n", m_nClassType.Name, szCallParam);
    }

    void MakeProperty()
    {
        FieldInfo[] allFields = m_nClassType.GetFields(); // 所有成员变量(只有public的)
        if (allFields != null)
        {
            foreach (FieldInfo field in allFields)
            {
                PushFieldInfo(field);
            }
        }
        PropertyInfo[] allProperties = m_nClassType.GetProperties(); // 属性方法 get/set
        if (allProperties != null)
        {
            foreach (PropertyInfo property in allProperties)
            {
                PushPropertyInfo(property);
            }
        }
    }
    // 功能：添加公有变量
    void PushFieldInfo(FieldInfo value)
    {
        // 生成get_value, set_value方法
        PushPropertyFunc(value.FieldType, value.Name, true, true, false);
    }
    // 功能：添加get-set方法
    void PushPropertyInfo(PropertyInfo property)
    {
        Type nVaueType = property.PropertyType;
        MethodInfo metGet = property.GetGetMethod();
        MethodInfo metSet = property.GetSetMethod();
        bool bStatic = false;
        try
        {
            if (property.CanRead)
                bStatic = metGet.IsStatic;
            if (property.CanWrite)
                bStatic = metSet.IsStatic;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        PushPropertyFunc(nVaueType, property.Name, property.CanRead, property.CanWrite, bStatic);
    }
    void PushPropertyFunc(Type nVaueType, string szName, bool bCanGet, bool bCanSet, bool bStatic)
    {
        // bool alphaIsTransparency { get; set; }
        string szGetBody = string.Empty;
        string szSetBody = string.Empty;
        string szStatic = string.Empty;
        if (bCanGet)
            szGetBody = " get; ";
        if (bCanSet)
            szSetBody = " set; ";
        if (bStatic)
            szStatic = "static ";
        string szValueType = FCValueType.GetTypeDesc(nVaueType);
        m_szTempBuilder.AppendFormat("    public {0}{1} {2} {{{3}{4}}}\r\n", szStatic, szValueType, szName, szGetBody, szSetBody);
    }

    void MakeMethod()
    {
        MethodInfo[] allMethods = m_nClassType.GetMethods();  // 函数+get/set方法
        if (allMethods == null)
            return;
        foreach(MethodInfo method in allMethods)
        {
            PushMethodInfo(method);
        }
    }
    // 功能：添加函数调用的方法
    void PushMethodInfo(MethodInfo method)
    {
        if (0 != (MethodAttributes.SpecialName & method.Attributes))
        {
            return;
        }
        bool bStatic = method.IsStatic;
        string szStatic = string.Empty;
        if (bStatic)
            szStatic = "static ";
        ParameterInfo[] allParams = method.GetParameters();  // 函数参数
        string szCallParam = string.Empty;
        if(allParams != null)
        {
            Type nParamType;
            string szParamType = string.Empty;
            for (int i = 0; i < allParams.Length; ++i)
            {
                nParamType = allParams[i].ParameterType;
                if (i > 0)
                {
                    szCallParam += ',';
                }
                szCallParam += FCValueType.GetTypeDesc(nParamType);
                szCallParam += " ";
                szCallParam += allParams[i].Name;
            }
        }
        m_szTempBuilder.AppendFormat("    public {0}{1} {2}({3});\r\n", szStatic, FCValueType.GetTypeDesc(method.ReturnType), method.Name, szCallParam);
    }
    void PushDelayType(Type  nType)
    {
        if (!nType.IsEnum)
            return;
        if (m_OuterType.ContainsKey(nType))
            return;
        m_OuterType[nType] = 1;
        m_DelayType.Add(nType);
    }
    void MakeInnerEnum()
    {
        Type[] allInnerTypes = m_nClassType.GetNestedTypes(); // 内部类型
        if (allInnerTypes == null)
            return;
        foreach(Type  nClassType in allInnerTypes)
        {
            PushInnerType("    ", nClassType);
        }        
    }
    void MakeOuterEnum()
    {
        foreach(Type nEnumType  in m_DelayType)
        {
            PushInnerType("", nEnumType);
        }
    }
    void  PushInnerType(string szLeft, Type nClassType)
    {
        bool bEnum = nClassType.IsEnum;

        // 只支持枚举的导出
        if (!bEnum)
            return;
        string szEnumName = nClassType.Name;
        m_szTempBuilder.AppendFormat("{0}public enum {1}\r\n", szLeft, szEnumName);
        m_szTempBuilder.AppendLine(szLeft + "{");

        Array enumValues = Enum.GetValues(nClassType);
        string szValueName = string.Empty;
        foreach (Enum enumValue in enumValues)
        {
            int nKey = Convert.ToInt32(enumValue);
            szValueName = enumValue.ToString();
            m_szTempBuilder.AppendFormat("{0}    {1} = {2},\r\n", szLeft, szValueName, nKey);
        }        
        m_szTempBuilder.AppendLine(szLeft + "};\r\n");
    }
}
