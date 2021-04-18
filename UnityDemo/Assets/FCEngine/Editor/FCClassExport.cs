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
    Dictionary<string, List<Type>> m_CurSupportTemplateFunc = new Dictionary<string, List<Type>>();

    Dictionary<string, MethodInfo> m_CurValidMethods = new Dictionary<string, MethodInfo>();
    List<MethodInfo> m_CurMethods = new List<MethodInfo>();

    Dictionary<Type, int> m_CurDelegate = new Dictionary<Type, int>(); // 当前引用的委托

    FCRefClass m_pRefClass;

    public void ExportDefaultClass(string szPath)
    {
        m_szFileBuilder.Length = 0;
        m_szFileBuilder.AppendLine();
        m_szFileBuilder.AppendLine("using System;");
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
            if (!nType.IsClass)
            {
                //if (!nType.IsValueType && !nType.IsInterface)  // 如果不是结构体, 也是接口类
                //    continue;
            }
            FCValueType value = FCValueType.TransType(nType);
            if (value.m_nTemplateType != fc_value_tempalte_type.template_none)
                continue;
            if (value.m_nValueType == fc_value_type.fc_value_delegate)
                continue;
            if (FCValueType.IsBaseType(value.m_nValueType))
                continue;

            // 如果是内部的类，不需要再导出了
            if (FCExclude.IsDontExportClass(nType))
                continue;
            //if (nType == typeof(IntPtr))
            //    continue;
            //if (nType == typeof(IEnumerator))
            //    continue;

            if (nType.Name == "T")
                continue;            
            if (nType.Name.IndexOf("UnityEvent`") != -1)
                continue;
            if (nType.Name.IndexOf('&') != -1)
                continue;
            if (nType.Name.IndexOf('`') != -1)
                continue;

            if (nType == typeof(Type))
            {
                m_szFileBuilder.AppendLine("class Type{}");
            }
            else if(nType == typeof(System.Object))
            {
                //m_szFileBuilder.AppendFormat("class {0}{{}}\r\n", nType.Name);
                continue;
            }
            else if(nType == typeof(UnityEngine.Object))
            {
                m_szFileBuilder.AppendLine("class UnityObject{}");
            }
            else
            {
                Type nParentType = nType.BaseType;
                if(nParentType != null && m_AllRefType.ContainsKey(nParentType))
                    m_szFileBuilder.AppendFormat("class {0}:{1}{{}}\r\n", nType.Name, nParentType.Name);
                else
                    m_szFileBuilder.AppendFormat("class {0}{{}}\r\n", nType.Name);
            }
            m_szFileBuilder.AppendLine();
        }
        string szPathName = szPath + "all_default_class.cs";
        File.WriteAllText(szPathName, m_szFileBuilder.ToString());
    }

    bool IsNeedExportMember(string szName)
    {
        if (m_pRefClass == null)
            return true;
        return m_pRefClass.FindMember(szName);
    }

    public void ExportClass(Type nClassType, string szPathName, bool bPartWrap, bool bOnlyThisApi, Dictionary<string, int> aDontWrapName, Dictionary<string, List<Type>> aTemplateFunc, FCRefClass  pRefClass)
    {
        m_bPartWrap = bPartWrap;
        m_bOnlyThisAPI = bOnlyThisApi;
        m_nClassType = nClassType;
        m_szPathName = szPathName;
        m_pRefClass = pRefClass;
        m_DelayType.Clear();
        m_szTempBuilder.Length = 0;
        m_CurRefNameSpace.Clear();
        m_CurRefNameSpacesFlags.Clear();
        m_CurDelegate.Clear();

        m_CurDontWrapName = aDontWrapName;
        m_CurSupportTemplateFunc = aTemplateFunc;

        m_AllExportType[nClassType] = 1;
        Type nParentType = nClassType.BaseType;
        m_AllRefType[nParentType] = 1;
        PushParentType(nParentType);

        string szParentName = FCValueType.GetClassName(nParentType);
        if (szParentName == "UnityEvent`1")
            szParentName = "UnityEventBase";

        m_szTempBuilder.AppendFormat("\r\nclass  {0} : {1}\r\n", FCValueType.GetClassName(nClassType), szParentName);
        m_szTempBuilder.AppendLine("{");

        MakeInnerEnum();  // 分析内部的枚举类
        MakeConstructor();  // 分析构造函数
        MakeProperty();  // 分析get - set方法
        MakeMethod();  // 分析函数
        MakeDelegate(); // 成生委托声明
        m_szTempBuilder.AppendLine("};\r\n");

        // 导出类外的枚举
        MakeOuterEnum();

        m_szFileBuilder.Length = 0;
        foreach(string szNameSpace in m_CurRefNameSpace)
        {
            if (szNameSpace.IndexOf("UnityEngine") != -1)
            {
                if (szNameSpace != "UnityEngine.Events")
                    continue;
            }
            m_szFileBuilder.AppendFormat("using {0};\r\n", szNameSpace);
        }
        if(m_CurRefNameSpace.Count == 0)
        {
            m_szFileBuilder.AppendLine("using System;");
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
        foreach (ConstructorInfo cons in allConInfos)
        {
            string szParentInitCall = GetParentInitCall(cons);
            PushConstructor(cons, szParentInitCall);
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
        try
        {
            if (nType.IsByRef)
            {
                Type nRealType = nType.GetElementType();
                if (nRealType != null)
                    nType = nRealType;
            }
            FCValueType value = FCValueType.TransType(nType);
            if (value.IsArray || value.IsList)
            {
                m_AllRefType[value.m_value] = 1;
            }
            else if (value.IsMap)
            {
                m_AllRefType[value.m_key] = 1;
                m_AllRefType[value.m_value] = 1;
            }
            else
            {
                m_AllRefType[nType] = 1;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    void  PushParentType(Type nType)
    {
        if (nType == null)
            return;
        PushDelegateType(nType);
        PushRefType(nType);
        Type nParentType = nType.BaseType;
        if (nParentType == null)
            return;
        if (nParentType == typeof(System.Object))
            return;
        if (nParentType == typeof(UnityEngine.Object))
            return;
        //Type  []allParent = nType.GetInterfaces(); // 接口类
        PushParentType(nParentType);
    }
    void PushDelegateType(Type nType)
    {
        if (nType.BaseType != null && nType.BaseType == typeof(MulticastDelegate))
        {
            m_CurDelegate[nType] = 1;
        }
    }
    bool  IsSameParam(ParameterInfo []left, ParameterInfo []right)
    {
        if (left.Length != right.Length)
            return false;
        for(int i = 0; i>left.Length; ++i)
        {
            if(left[i].ParameterType != right[i].ParameterType)
            {
                return false;
            }
        }
        return true;
    }
    string GetParentInitCall(ConstructorInfo consCall)
    {
        Type nBaseType = m_nClassType.BaseType;
        if (nBaseType == null)
            return string.Empty;
        ConstructorInfo[] allConInfos = nBaseType.GetConstructors(); // 得到构造函数信息
        if (allConInfos == null)
            return string.Empty;

        ParameterInfo[] InParams = consCall.GetParameters();
        if (InParams == null || InParams.Length == 0)
            return string.Empty;

        // 先检测构造参数
        ConstructorInfo pFindCon = null;
        foreach(ConstructorInfo cons in allConInfos)
        {
            ParameterInfo[] allParams = cons.GetParameters();
            int nCurParamCount = allParams != null ? allParams.Length : 0;
            if(nCurParamCount == 0)
            {
                return string.Empty;
            }
            if(nCurParamCount == InParams.Length)
            {
                // 比较一个参数
                if(IsSameParam(InParams, allParams))
                {
                    pFindCon = cons;
                    break;
                }
            }
        }
        if (pFindCon == null)
        {
            return string.Empty;
        }
        ParameterInfo[] Params = pFindCon.GetParameters();
        string szParamDesc = string.Empty;
        for(int i = 0; i< Params.Length; ++i)
        {
            if (i > 0)
                szParamDesc += ',';
            FCValueType value = FCValueType.TransType(Params[i].ParameterType);
            if(value.IsArray || value.IsList || value.IsMap || value.m_nValueType == fc_value_type.fc_value_system_object)
                szParamDesc += "null";
            else if (value.m_nValueType == fc_value_type.fc_value_string_a)
                szParamDesc += "string.Empty";
            else
                szParamDesc += string.Format("default({0})", value.GetValueName(false));
        }
        return string.Format("base({0})", szParamDesc);
    }
    void PushConstructor(ConstructorInfo cons, string szParentInitCall)
    {
        ParameterInfo[] allParams = cons.GetParameters();
        // 如果是有参数的，就要考虑要不是导出
        if(allParams != null && allParams.Length > 0)
        {
            if (m_bPartWrap)
            {
                if (!cons.IsDefined(typeof(PartWrapAttribute), false))
                {
                    return;
                }
            }
            // 如果该函数有不导出的标记
            if (cons.IsDefined(typeof(DontWrapAttribute), false))
            {
                return;
            }
            if (cons.IsDefined(typeof(ObsoleteAttribute), false))
            {
                return;
            }
        }
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
                PushDelegateType(nParamType);
                PushRefType(nParamType);
                if (i > 0)
                    szCallParam += ',';
                FCValueType value = FCValueType.TransType(nParamType);
                szCallParam += value.GetTypeName(false);
                szCallParam = szCallParam + " " + param.Name;
            }
        }
        if(string.IsNullOrEmpty(szParentInitCall))
            m_szTempBuilder.AppendFormat("    public {0}({1}){{}}\r\n", FCValueType.GetClassName(m_nClassType), szCallParam);
        else
            m_szTempBuilder.AppendFormat("    public {0}({1}):{2}{{}}\r\n", FCValueType.GetClassName(m_nClassType), szCallParam, szParentInitCall);
    }

    void MakeProperty()
    {
        FieldInfo[] allFields = FCValueType.GetFields(m_nClassType, m_bOnlyThisAPI);
        PropertyInfo[] allProperties = FCValueType.GetProperties(m_nClassType, m_bOnlyThisAPI);
        if (allFields != null)
        {
            foreach (FieldInfo field in allFields)
            {
                if(IsNeedExportMember(field.Name))
                    PushFieldInfo(field);
            }
        }
        if (allProperties != null)
        {
            foreach (PropertyInfo property in allProperties)
            {
                if(IsNeedExportMember(property.Name))
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
        if (m_CurDontWrapName.ContainsKey(value.Name))
        {
            return;
        }
        if (FCExclude.IsDontExportFieldInfo(value))
            return;

        PushNameSpace(value.FieldType.Namespace);
        PushDelegateType(value.FieldType);
        PushRefType(value.FieldType);
        bool bCanWrite = !(value.IsInitOnly || value.IsLiteral);
        // 生成get_value, set_value方法
        FCValueType ret_value = FCTemplateWrap.Instance.PushGetTypeWrap(value.FieldType);
        if (ret_value.m_nTemplateType == fc_value_tempalte_type.template_none
            && ret_value.m_nValueType == fc_value_type.fc_value_delegate)
        {
            PushPropertyFunc(value.FieldType, value.Name, false, bCanWrite, value.IsStatic);
        }
        else
            PushPropertyFunc(value.FieldType, value.Name, true, bCanWrite, value.IsStatic);
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
        if (FCExclude.IsDontExportPropertyInfo(property))
            return;
        //if (property.IsDefined(typeof(DefaultMemberAttribute), false))
        //{
        //    return;
        //}
        PushNameSpace(property.PropertyType.Namespace);
        PushDelegateType(property.PropertyType);
        PushRefType(property.PropertyType);
        Type nVaueType = property.PropertyType;
        MethodInfo metGet = property.GetGetMethod();
        MethodInfo metSet = property.GetSetMethod();
        bool bStatic = false;
        bool bCanRead = false;
        bool bCanWrite = false;
        try
        {
            if (property.CanRead)
            {
                bCanRead = metGet != null;
                if (metGet != null)
                    bStatic = metGet.IsStatic;
            }
            if (property.CanWrite)
            {
                bCanWrite = metSet != null;
                if(metSet != null)
                    bStatic = metSet.IsStatic;
                if(bCanWrite)
                {
                    if (FCExclude.IsDissablePropertySetMethod(m_nClassType, property.Name))
                        bCanWrite = false;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        PushPropertyFunc(nVaueType, property.Name, bCanRead, bCanWrite, bStatic);
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
        if(!bCanGet && bCanSet)
        {
            szSetBody = szSetBody.Replace(";", "{}");
        }
        FCValueType value = FCValueType.TransType(nVaueType);
        string szValueType = value.GetTypeName(false);
        if (szValueType.IndexOf('`') != -1)
            return ;
        m_szTempBuilder.AppendFormat("    public {0}{1} {2} {{{3}{4}}}\r\n", szStatic, szValueType, szName, szGetBody, szSetBody);
    }

    void MakeMethod()
    {
        MethodInfo[] allMethods = FCValueType.GetMethods(m_nClassType, m_bOnlyThisAPI);// m_nClassType.GetMethods();  // 函数+get/set方法
        if (allMethods == null)
            return;
        m_CurMethods.Clear();
        m_CurValidMethods.Clear();
        string szDeclareName = string.Empty;
        bool bNeedExport = false;
        foreach (MethodInfo method in allMethods)
        {
            if (!IsNeedExportMember(method.Name))
                continue;
            if (m_CurDontWrapName.ContainsKey(method.Name))
                continue;
            if (FCExclude.IsDontExportMethod(method))
                continue;
            bNeedExport = true;
            // 去掉参数都一样的，因为FC脚本中 []与List是一个数据类型
            szDeclareName = FCValueType.GetMethodDeclare(method, ref bNeedExport);
            if (!bNeedExport)
                continue;
            if (m_CurValidMethods.ContainsKey(szDeclareName))
            {
                // 必要的话，这里做个替换
                FCValueType.ReplaceMethod(m_CurValidMethods, m_CurMethods, szDeclareName, method);
                continue;
            }
            m_CurValidMethods[szDeclareName] = method;
            m_CurMethods.Add(method);
        }

        foreach (MethodInfo method in m_CurMethods)
        {
            PushMethodInfo(method);
        }

        // 特殊导出UnityEvent<T>模板类
        Type nBaseType = m_nClassType.BaseType;
        if(nBaseType != null && nBaseType.Name == "UnityEvent`1")
        {
            PushUnityEventTemplateFunc(m_nClassType);
        }
    }

    // 定制UnityEvent<T>模板函数的导出
    void PushUnityEventTemplateFunc(Type nClassType)
    {
        Type nBaseType = nClassType.BaseType;
        Type[] argTypes = nBaseType.GetGenericArguments();// GenericTypeArguments;
        if (argTypes == null || argTypes.Length == 0)
            return;
        Type nParamType = argTypes[0];
        FCValueType nParamValue = FCValueType.TransType(nParamType);
        string szParamName = nParamValue.GetValueName(false);
        
        m_szTempBuilder.AppendFormat("    public void AddListener(UnityAction<{0}> call){{}}\r\n", szParamName);
        m_szTempBuilder.AppendFormat("    public void Invoke({0} arg0){{}}\r\n", szParamName);
        m_szTempBuilder.AppendFormat("    public void RemoveListener(UnityAction<{0}> call){{}}\r\n", szParamName);
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
        bool bTemplateFunc = false;
        if (IsTemplateFunc(method))
        {
            if(!m_CurSupportTemplateFunc.ContainsKey(method.Name))
                return;
            bTemplateFunc = true;
        }
        bool bStatic = method.IsStatic;
        string szStatic = string.Empty;
        if (bStatic)
            szStatic = "static ";
        ParameterInfo[] allParams = method.GetParameters();  // 函数参数
        string szCallParam = string.Empty;
        string szBody = string.Empty;

        if(allParams != null)
        {
            Type nParamType;
            for (int i = 0; i < allParams.Length; ++i)
            {
                ParameterInfo param = allParams[i];
                nParamType = param.ParameterType;
                if (i > 0)
                {
                    szCallParam += ',';
                }
                PushNameSpace(nParamType.Namespace);
                PushDelegateType(nParamType);
                PushRefType(nParamType);
                FCValueType value = FCValueType.TransType(nParamType);
                if (param.IsOut)
                {
                    szCallParam += "out ";
                    szBody += string.Format("{0}=default({1});", allParams[i].Name, value.GetTypeName(false));
                }
                else if (nParamType.IsByRef)
                {
                    szCallParam += "ref ";
                }
                szCallParam += value.GetTypeName(false);
                szCallParam += " ";
                szCallParam += allParams[i].Name;
            }
        }
        PushNameSpace(method.ReturnType.Namespace);
        PushDelegateType(method.ReturnType);
        PushRefType(method.ReturnType);
        FCValueType ret_value = FCValueType.TransType(method.ReturnType);
        if(ret_value.m_nTemplateType != fc_value_tempalte_type.template_none)
        {
            m_szTempBuilder.AppendFormat("    public {0}{1} {2}({3}){{ {4}return null; }}\r\n", szStatic, ret_value.GetTypeName(false), GetMeshName(method, bTemplateFunc), szCallParam, szBody);
        }
        else if(ret_value.m_nValueType == fc_value_type.fc_value_void)
        {
            m_szTempBuilder.AppendFormat("    public {0}{1} {2}({3}){{{4}}}\r\n", szStatic, ret_value.GetTypeName(false), GetMeshName(method, bTemplateFunc), szCallParam, szBody);
        }
        else
        {
            string szRetCShaprName = ret_value.GetTypeName(false);
            m_szTempBuilder.AppendFormat("    public {0}{1} {2}({3}){{ {4}return default({5}); }}\r\n", szStatic, ret_value.GetTypeName(false), GetMeshName(method, bTemplateFunc), szCallParam, szBody, szRetCShaprName);
        }
    }
    string  GetMeshName(MethodInfo method, bool bTemplateFunc)
    {
        if (!bTemplateFunc)
            return method.Name;
        string szMethodName = method.ToString();
        int nStart = szMethodName.IndexOf('[');
        int nEnd = szMethodName.IndexOf(']');
        string szSubName = szMethodName.Substring(nStart + 1, nEnd - nStart - 1);
        return string.Format("{0}<{1}>", method.Name, szSubName);
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
        {
            // 如果是委托
            PushDelegateType(nClassType);
            return;
        }
        string szEnumName = nClassType.Name;
        fileBuilder.AppendFormat("{0}public enum {1}\r\n", szLeft, szEnumName);
        fileBuilder.AppendLine(szLeft + "{");
        m_AllExportType[nClassType] = 1;

        Array enumValues = Enum.GetValues(nClassType);
        string szValueName = string.Empty;
        string []allNames = Enum.GetNames(nClassType);
        int nIndex = 0;
        foreach (Enum enumValue in enumValues)
        {
            int nKey = Convert.ToInt32(enumValue);
            szValueName = allNames[nIndex];
            fileBuilder.AppendFormat("{0}    {1} = {2},\r\n", szLeft, szValueName, nKey);
            ++nIndex;
        }
        fileBuilder.AppendLine(szLeft + "};\r\n");
    }
    void MakeDelegate()
    {
        foreach(Type nType in m_CurDelegate.Keys)
        {
            PushDelegateType(m_szTempBuilder, "    ", nType);
        }
    }
    void PushDelegateType(StringBuilder fileBuilder, string szLeft, Type nClassType)
    {
        //ConstructorInfo[] c1 = nClassType.GetConstructors();
        MethodInfo method = nClassType.GetMethod("Invoke");

        ParameterInfo[] allParams = method.GetParameters();
        string szParamDesc = string.Empty;
        if (allParams != null)
        {
            for (int i = 0; i < allParams.Length; ++i)
            {
                PushRefType(allParams[i].ParameterType);
                FCValueType value_param = FCValueType.TransType(allParams[i].ParameterType);
                if (i > 0)
                    szParamDesc += ",";
                szParamDesc += value_param.GetTypeName(false);
                szParamDesc += " arg" + i;
            }
        }
        FCValueType value_type = FCValueType.TransType(nClassType);
        FCValueType value_ret = FCValueType.TransType(method.ReturnType);
        string szClassName = value_type.GetValueName(false);
        fileBuilder.AppendFormat("{0}public delegate {1} {2}({3});\r\n", szLeft, value_ret.GetValueName(false), szClassName, szParamDesc);
    }
}
