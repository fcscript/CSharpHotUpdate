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
    StringBuilder m_szFileBuilder = new StringBuilder(1024 * 1024 * 1);
    Dictionary<Type, int> m_OuterType = new Dictionary<Type, int>();
    List<Type> m_DelayType = new List<Type>();
    bool m_bPartWrap = false;
    bool m_bOnlyThisAPI = false;
    List<string> m_CurRefNameSpace = new List<string>();
    Dictionary<string, int> m_CurRefNameSpacesFlags = new Dictionary<string, int>();

    Dictionary<Type, int> m_AllExportType = new Dictionary<Type, int>(); // 所有的类型
    Dictionary<Type, int> m_AllRefType = new Dictionary<Type, int>(); // 所有引用的类型

    Dictionary<string, int> m_CurDontWrapName = new Dictionary<string, int>();

    public void ExportDefaultClass(string szPath)
    {
        m_szFileBuilder.Length = 0;
        m_szFileBuilder.AppendLine();
        foreach (Type nType in m_AllRefType.Keys)
        {
            if (m_AllExportType.ContainsKey(nType))
                continue;
            if(nType.IsEnum)
            {
                PushInnerType(m_szFileBuilder, string.Empty, nType);
                continue;
            }
            if (nType.IsArray)
                continue;
            if(!nType.IsClass)
            {
                continue;
            }
            FCValueType value = FCValueType.TransType(nType);
            if (value.m_nTemplateType != fc_value_tempalte_type.template_none)
                continue;
            if (value.m_nValueType == fc_value_type.fc_value_delegate)
                continue;
            if (FCValueType.IsBaseType(value.m_nValueType))
                continue;

            if(nType == typeof(Type))
            {
                m_szFileBuilder.AppendLine("class Type{}");
            }
            else if(nType == typeof(System.Object))
            {
                m_szFileBuilder.AppendFormat("class {0}{{}}\r\n", nType.Name);
            }
            else if(nType == typeof(UnityEngine.Object))
            {
                m_szFileBuilder.AppendLine("class UnityObject{}");
            }
            else
            {
                m_szFileBuilder.AppendFormat("class {0} : {1}{{}}\r\n", nType.Name, nType.BaseType.Name);
            }
            m_szFileBuilder.AppendLine();
        }
        string szPathName = szPath + "all_default_class.cs";
        File.WriteAllText(szPathName, m_szFileBuilder.ToString());
    }
    
    public void ExportClass(Type nClassType, string szPathName, bool bPartWrap, bool bOnlyThisApi, Dictionary<string, int> aDontWrapName)
    {
        m_bPartWrap = bPartWrap;
        m_bOnlyThisAPI = bOnlyThisApi;
        m_nClassType = nClassType;
        m_szPathName = szPathName;
        m_DelayType.Clear();
        m_szTempBuilder.Length = 0;
        m_CurRefNameSpace.Clear();
        m_CurRefNameSpacesFlags.Clear();

        m_CurDontWrapName = aDontWrapName;

        m_AllExportType[nClassType] = 1;
        Type nParentType = nClassType.BaseType;
        m_AllRefType[nParentType] = 1;

        m_szTempBuilder.AppendFormat("\r\nclass  {0} : {1}\r\n", FCValueType.GetClassName(nClassType), FCValueType.GetClassName(nParentType));
        m_szTempBuilder.AppendLine("{");

        MakeInnerEnum();  // 分析内部的枚举类
        MakeConstructor();  // 分析构造函数
        MakeProperty();  // 分析get - set方法
        MakeMethod();  // 分析函数
        m_szTempBuilder.AppendLine("};\r\n");

        // 导出类外的枚举
        MakeOuterEnum();

        m_szFileBuilder.Length = 0;
        foreach(string szNameSpace in m_CurRefNameSpace)
        {
            m_szFileBuilder.AppendFormat("using {0};\r\n", szNameSpace);
        }
        m_szFileBuilder.AppendLine();
        m_szFileBuilder.Append(m_szTempBuilder.ToString());

        File.WriteAllText(m_szPathName, m_szFileBuilder.ToString());
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
    void PushNameSpace(string szNameSpace)
    {
        if (string.IsNullOrEmpty(szNameSpace))
            return;
        if (m_CurRefNameSpacesFlags.ContainsKey(szNameSpace))
            return;
        m_CurRefNameSpacesFlags[szNameSpace] = 1;
        m_CurRefNameSpace.Add(szNameSpace);        
    }
    void PushRefType(Type nType)
    {
        m_AllRefType[nType] = 1;
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
                PushNameSpace(nParamType.Namespace);
                PushDelayType(nParamType);
                PushRefType(nParamType);
                if (i > 0)
                    szCallParam += ',';
                FCValueType value = FCValueType.TransType(nParamType);
                szCallParam += value.GetTypeName(false);
                szCallParam = szCallParam + " " + param.Name;
            }
        }
        m_szTempBuilder.AppendFormat("    public {0}({1});\r\n", FCValueType.GetClassName(m_nClassType), szCallParam);
    }

    void MakeProperty()
    {
        FieldInfo[] allFields = null;
        PropertyInfo[] allProperties = null;
        if (m_bOnlyThisAPI)
        {
            allFields = m_nClassType.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static);
            allProperties = m_nClassType.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        }
        else
        {
            allFields = m_nClassType.GetFields(); // 所有成员变量(只有public的)
            allProperties = m_nClassType.GetProperties(); // 属性方法 get/set
        }        
        if (allFields != null)
        {
            foreach (FieldInfo field in allFields)
            {
                PushFieldInfo(field);
            }
        }
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
        if (m_bPartWrap)
        {
            if (!value.IsDefined(typeof(PartWrapAttribute), false))
            {
                return;
            }
        }
        // 如果该变量有不导出的标记
        if (value.IsDefined(typeof(DontWrapAttribute), false))
        {
            return;
        }
        if (value.IsDefined(typeof(ObsoleteAttribute), false))
        {
            return;
        }
        PushNameSpace(value.FieldType.Namespace);
        PushRefType(value.FieldType);
        // 生成get_value, set_value方法
        FCValueType ret_value = FCTemplateWrap.Instance.PushGetTypeWrap(value.FieldType);
        if (ret_value.m_nTemplateType == fc_value_tempalte_type.template_none
            && ret_value.m_nValueType == fc_value_type.fc_value_delegate)
        {
            PushPropertyFunc(value.FieldType, value.Name, false, true, value.IsStatic);
        }
        else
            PushPropertyFunc(value.FieldType, value.Name, true, true, value.IsStatic);
    }
    // 功能：添加get-set方法
    void PushPropertyInfo(PropertyInfo property)
    {
        if (m_bPartWrap)
        {
            if (!property.IsDefined(typeof(PartWrapAttribute), false))
            {
                return;
            }
        }
        // 如果该变量有不导出的标记
        if (property.IsDefined(typeof(DontWrapAttribute), false))
        {
            return;
        }
        if (property.IsDefined(typeof(ObsoleteAttribute), false))
        {
            return;
        }
        if (m_CurDontWrapName.ContainsKey(property.Name))
        {
            return;
        }
        //if (property.IsDefined(typeof(DefaultMemberAttribute), false))
        //{
        //    return;
        //}
        PushNameSpace(property.PropertyType.Namespace);
        PushRefType(property.PropertyType);
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
        FCValueType value = FCValueType.TransType(nVaueType);
        string szValueType = value.GetTypeName(false);
        m_szTempBuilder.AppendFormat("    public {0}{1} {2} {{{3}{4}}}\r\n", szStatic, szValueType, szName, szGetBody, szSetBody);
    }

    void MakeMethod()
    {
        MethodInfo[] allMethods = null;// m_nClassType.GetMethods();  // 函数+get/set方法
        if (m_bOnlyThisAPI)
            allMethods = m_nClassType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        else
            allMethods = m_nClassType.GetMethods();  // 函数+get/set方法
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
        if (m_bPartWrap)
        {
            if (!method.IsDefined(typeof(PartWrapAttribute), false))
            {
                return;
            }
        }
        // 如果该函数有不导出的标记
        if (method.IsDefined(typeof(DontWrapAttribute), false))
        {
            return;
        }
        if (method.IsDefined(typeof(ObsoleteAttribute), false))
        {
            return;
        }

        // 模板函数不导出了吧
        if (IsTemplateFunc(method))
            return;
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
                PushNameSpace(nParamType.Namespace);
                PushRefType(nParamType);
                FCValueType value = FCValueType.TransType(nParamType);
                szCallParam += value.GetTypeName(false);
                szCallParam += " ";
                szCallParam += allParams[i].Name;
            }
        }
        PushNameSpace(method.ReturnType.Namespace);
        PushRefType(method.ReturnType);
        FCValueType ret_value = FCValueType.TransType(method.ReturnType);
        if(ret_value.m_nTemplateType != fc_value_tempalte_type.template_none)
        {
            m_szTempBuilder.AppendFormat("    public {0}{1} {2}({3}){{ return null; }}\r\n", szStatic, ret_value.GetTypeName(false), method.Name, szCallParam);
        }
        else if(ret_value.m_nValueType == fc_value_type.fc_value_void)
        {
            m_szTempBuilder.AppendFormat("    public {0}{1} {2}({3}){{}}\r\n", szStatic, ret_value.GetTypeName(false), method.Name, szCallParam);
        }
        else
        {
            string szRetCShaprName = ret_value.GetTypeName(true);
            m_szTempBuilder.AppendFormat("    public {0}{1} {2}({3}){{ return default({4}); }}\r\n", szStatic, ret_value.GetTypeName(false), method.Name, szCallParam, szRetCShaprName);
        }
    }
    // 功能：检测函数是不是模板函数
    bool IsTemplateFunc(MethodInfo method)
    {
        string szMethodName = method.ToString();
        // xxx func[T, V](...);
        int nIndex = szMethodName.IndexOf('(');
        if (nIndex == -1)
            return false;
        if (nIndex > 0 && szMethodName[nIndex - 1] == ']')
        {
            return true;
        }
        return false;
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
            PushInnerType(m_szTempBuilder, "    ", nClassType);
        }        
    }
    void MakeOuterEnum()
    {
        foreach(Type nEnumType  in m_DelayType)
        {
            PushInnerType(m_szTempBuilder, "", nEnumType);
        }
    }
    void  PushInnerType(StringBuilder fileBuilder, string szLeft, Type nClassType)
    {
        bool bEnum = nClassType.IsEnum;

        // 只支持枚举的导出
        if (!bEnum)
            return;
        string szEnumName = nClassType.Name;
        fileBuilder.AppendFormat("{0}public enum {1}\r\n", szLeft, szEnumName);
        fileBuilder.AppendLine(szLeft + "{");
        m_AllExportType[nClassType] = 1;

        Array enumValues = Enum.GetValues(nClassType);
        string szValueName = string.Empty;
        foreach (Enum enumValue in enumValues)
        {
            int nKey = Convert.ToInt32(enumValue);
            szValueName = enumValue.ToString();
            fileBuilder.AppendFormat("{0}    {1} = {2},\r\n", szLeft, szValueName, nKey);
        }
        fileBuilder.AppendLine(szLeft + "};\r\n");
    }
}
