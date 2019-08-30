using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FCClassWrap
{
    struct WrapFuncDesc
    {
        public bool m_bAttrib;   // 是不是属性
        public string m_szName;   // 函数名或变量名
        public string m_szGetName;   // 函数名
        public string m_szSetName;   // 函数名
        public string m_szContent;  // 内容
        public string m_szRegister; // 注册命令
    }

    string m_szExportPath; // 导出路径
    string m_szCurWrapPath;
    string m_szCurModleName; // 当前模块的名字
    string m_szCurClassName; // 当前的类名
    string m_szFCScriptPath; // 脚本导出路径
    Type m_nCurClassType;

    List<string> m_AllWrapClassName = new List<string>(); // 所有wrap的类名
    List<string> m_CurWrapClassNames = new List<string>(); // 当前模块wrap的类名
    List<WrapFuncDesc> m_CurClassFunc = new List<WrapFuncDesc>();
    Dictionary<string, int> m_CurSameName = new Dictionary<string, int>();
    Dictionary<string, int> m_CurFuncCount = new Dictionary<string, int>();

    StringBuilder m_szTempBuilder;

    FCClassExport m_export;
    FCTemplateWrap m_templateWrap;

    // 功能：
    public FCClassWrap()
    {
    }

    // 功能：删除指定目录下的所有文件
    void  DeletePath(string szPath)
    {
        try
        {
            if (!Directory.Exists(szPath))
                return;
            string [] allFileNames = Directory.GetFiles(szPath, "*.*", SearchOption.AllDirectories);
            if (allFileNames == null)
                return;
            foreach(string szFileName in allFileNames)
            {
                File.Delete(szFileName);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    // 功能：开始导出
    public void BeginExport(string szExportPath)
    {
        m_szExportPath = szExportPath;
        if (string.IsNullOrEmpty(m_szExportPath))
        {
            m_szExportPath = Application.dataPath;
            m_szExportPath = m_szExportPath.Substring(0, m_szExportPath.Length - 6) + "fcscript/";
        }
        m_szFCScriptPath = Application.dataPath;
        m_szFCScriptPath = m_szFCScriptPath.Substring(0, m_szFCScriptPath.Length - 6) + "Script/inport/";
        DeletePath(m_szExportPath);
        DeletePath(m_szFCScriptPath);
        Directory.CreateDirectory(m_szExportPath);
        Directory.CreateDirectory(m_szFCScriptPath);
        // 清除该目录下的文件
        m_szTempBuilder = new StringBuilder(1024 * 1024 * 1);
        m_export = new FCClassExport();
        m_templateWrap = new FCTemplateWrap();
        m_templateWrap.BeginExport(m_szExportPath);
    }

    // 功能：结束所有的导出
    public void EndExport()
    {
        m_templateWrap.EndExport(m_szTempBuilder);
        ExportWrapInit("AllClassWrap", m_AllWrapClassName);
    }

    void  ExportWrapInit(string szModle, List<string> aWrapNames)
    {
        string szPathName = m_szExportPath + szModle + ".cs";
        // 这里只导出一个

        StringBuilder fileData = new StringBuilder(1024 * 1024 * 1);
        fileData.AppendLine("using System;");
        fileData.AppendLine("using UnityEngine;\r\n");
        fileData.AppendFormat("public class {0}\r\n", szModle);
        fileData.AppendLine("{");
        fileData.AppendLine("    public static void Register()");
        fileData.AppendLine("    {");
        foreach(string szClassName in aWrapNames)
        {
            fileData.AppendFormat("        {0}.Register();\r\n", szClassName);
        }
        fileData.AppendLine("    }");
        fileData.AppendLine("}");
        File.WriteAllText(szPathName, fileData.ToString());
    }

    // 功能：开始一个模块的导出
    public void  BeginModleWrap(string szModle)
    {
        m_szCurModleName = szModle;
        m_szCurWrapPath = m_szExportPath + szModle + "/";
        m_szCurWrapPath = m_szCurWrapPath.Replace("\\/", "/");
        m_szCurWrapPath = m_szCurWrapPath.Replace("//", "/");
        Directory.CreateDirectory(m_szCurWrapPath);
        m_CurWrapClassNames.Clear();
    }
    public void  EndModleWrap()
    {
        ExportWrapInit(m_szCurModleName, m_CurWrapClassNames);
        m_AllWrapClassName.Add(m_szCurModleName);
    }

    public void  WrapClass(Type nClassType)
    {
        m_szTempBuilder.Length = 0;
        string szWrapName = nClassType.Name + "_wrap";
        m_CurWrapClassNames.Add(szWrapName);
        m_szCurClassName = nClassType.Name;
        m_nCurClassType = nClassType;
        WrapSubClass(m_szTempBuilder, nClassType);
        m_export.ExportClass(nClassType, m_szFCScriptPath + m_szCurClassName + ".cpp");
    }
    void  WrapSubClass(StringBuilder fileData, Type nClassType)
    {
        string szWrapName = nClassType.Name + "_wrap";
        m_CurClassFunc.Clear();
        m_CurSameName.Clear();

        // 先生成init函数
        FieldInfo[] allFields = nClassType.GetFields(); // 所有成员变量(只有public的)
        PropertyInfo[] allProperties = nClassType.GetProperties(); // 属性方法 get/set
        MethodInfo[] allMethods = nClassType.GetMethods();  // 函数+get/set方法
        if(allFields != null)
        {
            foreach(FieldInfo field in allFields)
            {
                PushFieldInfo(field);
            }
        }
        if(allProperties != null)
        {
            foreach(PropertyInfo property in allProperties)
            {
                PushPropertyInfo(property);
            }
        }
        if(allMethods != null)
        {
            m_CurFuncCount.Clear();
            string szFuncName = string.Empty;
            int nFuncCount = 0;
            foreach (MethodInfo method in allMethods)
            {
                szFuncName = method.Name;
                nFuncCount = 0;
                m_CurFuncCount.TryGetValue(szFuncName, out nFuncCount);
                m_CurFuncCount[szFuncName] = nFuncCount + 1;
            }
            foreach (MethodInfo method in allMethods)
            {
                PushMethodInfo(method);
            }
        }
        MakeEqual();
        MakeHash();
        MakeReleaseRef();
        MakeDel();
        MakeNew();
        // 生成Init函数
        MakeInitFunc(nClassType.Name);
        MakeGetObj(); // 生成 _Ty  get_obj()函数
        MakeWrapClass(szWrapName);
    }
    void MakeWrapClass(string szWrapName)
    {
        string szPathName = m_szCurWrapPath + szWrapName + ".cs";
        // 这里只导出一个
        string  szNamespace = m_nCurClassType.Namespace;
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendLine("using System;");
        fileData.AppendLine("using System.Collections.Generic;");
        fileData.AppendLine("using System.Text;");
        fileData.AppendLine("using UnityEngine;\r\n");
        if(!string.IsNullOrEmpty(szNamespace) && szNamespace != "UnityEngine")
            fileData.AppendFormat("using {0};\r\n", szNamespace);

        fileData.AppendFormat("public class {0}\r\n", szWrapName);
        fileData.AppendLine("{");
        foreach (WrapFuncDesc func in m_CurClassFunc)
        {
            fileData.AppendLine(func.m_szContent);
        }
        fileData.AppendLine("}");
        File.WriteAllText(szPathName, fileData.ToString());
    }
    void MakeGetObj()
    {
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendFormat("    public static {0} get_obj(long L)\r\n", m_szCurClassName);
        fileData.AppendLine("    {");
        fileData.AppendFormat("        return FCGetObj.GetObj<{0}>(L);\r\n", m_szCurClassName);
        fileData.AppendLine("    }");

        WrapFuncDesc init_func = new WrapFuncDesc();
        init_func.m_szName = init_func.m_szGetName = init_func.m_szSetName = "get_obj";
        init_func.m_bAttrib = false;
        init_func.m_szContent = fileData.ToString();
        m_CurClassFunc.Insert(0, init_func);
    }
    void MakeNew()
    {
        ConstructorInfo[] allConInfos = m_nCurClassType.GetConstructors(); // 得到构造函数信息
        // 先检测空的构造
        if (allConInfos == null)
            return;
        int nCount = 0;
        foreach(ConstructorInfo conInfo in allConInfos)
        {
            ++nCount;
            MakeParamNew(conInfo, nCount);
        }
    }
    void MakeDefNew()
    {
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
        fileData.AppendLine("    public static int  obj_new(long L)");
        fileData.AppendLine("    {");
        fileData.AppendFormat("        long nPtr = FCGetObj.NewObj<{0}>();\r\n", m_szCurClassName);
        fileData.AppendLine("        FCLibHelper.fc_push_return_intptr(L, nPtr);");
        fileData.AppendLine("        return 0;");
        fileData.AppendLine("    }");

        WrapFuncDesc func = new WrapFuncDesc();
        func.m_szName = func.m_szGetName = func.m_szSetName = "obj_new";
        func.m_bAttrib = false;
        func.m_szContent = fileData.ToString();
        func.m_szRegister = "FCLibHelper.fc_register_class_new(nClassName, obj_new);";
        m_CurClassFunc.Insert(0, func);
    }
    void MakeParamNew(ConstructorInfo conInfo, int nFuncIndex)
    {
        ParameterInfo[] allParams = conInfo.GetParameters();
        if(allParams == null || allParams.Length == 0)
        {
            MakeDefNew();
            return;
        }

        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        
        fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
        fileData.AppendFormat("    public static int  obj_new{0}(long L)\r\n", nFuncIndex);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        string szCallParam = string.Empty;
        Type nParamType;
        string szLeftName = string.Empty;
        for (int i = 0; i<allParams.Length; ++i)
        {
            ParameterInfo param = allParams[i];
            nParamType = param.ParameterType;

            FCValueType param_value = FCTemplateWrap.Instance.PushGetTypeWrap(nParamType);

            szLeftName = string.Format("arg{0}", i);
            if (param.IsOut)
            {
                string szCSharpName = param_value.GetTypeName(true);
                fileData.AppendFormat("            {0} {1};\r\n", szCSharpName, szLeftName);
            }
            else
            {
                fileData.Append(FCValueType.SetMemberValue("            ", param_value, szLeftName, "L", i.ToString(), true, param.IsOut));
            }
            if (i > 0)
                szCallParam += ',';
            if(param.IsOut)
            {
                szCallParam += "out ";
            }
            else if (nParamType.IsByRef)
            {
                szCallParam += "ref ";
            }
            szCallParam += szLeftName;
        }
        fileData.AppendFormat("            {0} obj = new {1}({2});\r\n", m_szCurClassName, m_szCurClassName, szCallParam);
        fileData.AppendFormat("            long nPtr = FCGetObj.PushNewObj<{0}>(obj);\r\n", m_szCurClassName);
        fileData.AppendLine("            FCLibHelper.fc_push_return_intptr(L, nPtr);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        return 0;");
        fileData.AppendLine("    }");
        
        WrapFuncDesc func = new WrapFuncDesc();
        func.m_szName = m_szCurClassName;
        func.m_szGetName = func.m_szSetName = string.Format("obj_new{0}", nFuncIndex);
        func.m_bAttrib = false;
        func.m_szContent = fileData.ToString();
        func.m_szRegister = string.Format("FCLibHelper.fc_register_class_func(nClassName, \"{0}\", {1});", m_szCurClassName, func.m_szGetName);
        m_CurClassFunc.Insert(0, func);
    }
    void MakeDel()
    {
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
        fileData.AppendLine("    public static int  obj_del(long L)");
        fileData.AppendLine("    {");
        fileData.AppendLine("        FCGetObj.DelObj(L);");
        fileData.AppendLine("        return 0;");
        fileData.AppendLine("    }");

        WrapFuncDesc func = new WrapFuncDesc();
        func.m_szName = func.m_szGetName = func.m_szSetName = "obj_del";
        func.m_bAttrib = false;
        func.m_szContent = fileData.ToString();
        func.m_szRegister = "FCLibHelper.fc_register_class_del(nClassName,obj_del);";
        m_CurClassFunc.Insert(0, func);
    }
    void MakeReleaseRef()
    {
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
        fileData.AppendLine("    public static int  obj_release(long L)");
        fileData.AppendLine("    {");
        fileData.AppendLine("        FCGetObj.ReleaseRef(L);");
        fileData.AppendLine("        return 0;");
        fileData.AppendLine("    }");

        WrapFuncDesc func = new WrapFuncDesc();
        func.m_szName = func.m_szGetName = func.m_szSetName = "obj_release";
        func.m_bAttrib = false;
        func.m_szContent = fileData.ToString();
        func.m_szRegister = "FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);";
        m_CurClassFunc.Insert(0, func);
    }
    void MakeHash()
    {
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
        fileData.AppendLine("    public static int  obj_hash(long L)");
        fileData.AppendLine("    {");
        fileData.AppendFormat("        {0} obj = FCGetObj.GetObj<{0}>(L);\r\n", m_szCurClassName, m_szCurClassName);
        fileData.AppendLine("        if(obj != null)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            return obj.GetHashCode();");
        fileData.AppendLine("        }");
        fileData.AppendLine("        return 0;");
        fileData.AppendLine("    }");

        WrapFuncDesc func = new WrapFuncDesc();
        func.m_szName = func.m_szGetName = func.m_szSetName = "obj_hash";
        func.m_bAttrib = false;
        func.m_szContent = fileData.ToString();
        func.m_szRegister = "FCLibHelper.fc_register_class_hash(nClassName,obj_hash);";
        m_CurClassFunc.Insert(0, func);
    }
    void MakeEqual()
    {
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]");
        fileData.AppendLine("    public static bool  obj_equal(long L, long R)");
        fileData.AppendLine("    {");
        fileData.AppendFormat("        {0} left  = FCGetObj.GetObj<{0}>(L);\r\n", m_szCurClassName, m_szCurClassName);
        fileData.AppendFormat("        {0} right = FCGetObj.GetObj<{0}>(R);\r\n", m_szCurClassName, m_szCurClassName);
        fileData.AppendLine("        if(left != null)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            return left.Equals(right);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        if(right != null)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            return right.Equals(left);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        return true;");
        fileData.AppendLine("    }");

        WrapFuncDesc func = new WrapFuncDesc();
        func.m_szName = func.m_szGetName = func.m_szSetName = "obj_equal";
        func.m_bAttrib = false;
        func.m_szContent = fileData.ToString();
        func.m_szRegister = "FCLibHelper.fc_register_class_equal(nClassName,obj_equal);";
        m_CurClassFunc.Insert(0, func);
    }
    void MakeInitFunc(string szClassName)
    {
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendLine("    public static void Register()");
        fileData.AppendLine("    {");
        fileData.AppendFormat("        int nClassName = FCLibHelper.fc_get_inport_class_id(\"{0}\");\r\n", szClassName);
        foreach (WrapFuncDesc func in m_CurClassFunc)
        {
            if (string.IsNullOrEmpty(func.m_szRegister))
                continue;
            fileData.AppendFormat("        {0}\r\n", func.m_szRegister);
        }
        fileData.AppendLine("    }");

        WrapFuncDesc init_func = new WrapFuncDesc();
        init_func.m_szName = init_func.m_szGetName = init_func.m_szSetName = "Register";
        init_func.m_bAttrib = false;
        init_func.m_szContent = fileData.ToString();
        m_CurClassFunc.Insert(0, init_func);
    }
    // 功能：添加公有变量
    void PushFieldInfo(FieldInfo value)
    {
        // 生成get_value, set_value方法
        PushPropertyFunc(value.FieldType, value.Name, true, true, value.IsStatic);
    }
    // 功能：添加get-set方法
    void PushPropertyInfo(PropertyInfo property)
    {
        Type nVaueType = property.PropertyType;
        MethodInfo  metGet = property.GetGetMethod();
        MethodInfo  metSet = property.GetSetMethod();
        bool bStatic = false;
        try
        {
            if (property.CanRead)
                bStatic = metGet.IsStatic;
            if (property.CanWrite)
                bStatic = metSet.IsStatic;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        PushPropertyFunc(nVaueType, property.Name, property.CanRead, property.CanWrite, bStatic);
    }

    void  PushPropertyFunc(Type nVaueType, string  szName, bool bCanGet, bool bCanSet, bool bStatic)
    {
        WrapFuncDesc func = new WrapFuncDesc();
        func.m_bAttrib = true;
        func.m_szName = szName;
        if (bCanGet)
            func.m_szGetName = string.Format("get_{0}_wrap", szName);
        else
            func.m_szGetName = "null";
        if (bCanSet)
            func.m_szSetName = string.Format("set_{0}_wrap", szName);
        else
            func.m_szSetName = "null";
        
        FCValueType ret_value = FCTemplateWrap.Instance.PushGetTypeWrap(nVaueType);

        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        string szLeftName = string.Format("ret.{0}", szName);
        if (bStatic)
            szLeftName = string.Format("{0}.{1}", m_szCurClassName, szName);
        if(bCanGet)
        {
            fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
            fileData.AppendFormat("    public static int {0}(long L)\r\n", func.m_szGetName);
            fileData.AppendLine("    {");
            fileData.AppendLine("        try");
            fileData.AppendLine("        {");
            fileData.AppendLine("            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);");
            fileData.AppendFormat("            {0} ret = get_obj(nThisPtr);\r\n", m_szCurClassName);
            FCValueType.PushReturnValue(fileData, "            ", ret_value, "L", szLeftName, true);
            fileData.AppendLine("        }");
            fileData.AppendLine("        catch(Exception e)");
            fileData.AppendLine("        {");
            fileData.AppendLine("            Debug.LogException(e);");
            fileData.AppendLine("        }");
            fileData.AppendLine("        return 0;");
            fileData.AppendLine("    }");
        }

        if(bCanSet)
        {
            fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
            fileData.AppendFormat("    public static int {0}(long L)\r\n", func.m_szSetName);
            fileData.AppendLine("    {");
            fileData.AppendLine("        try");
            fileData.AppendLine("        {");
            fileData.AppendLine("            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);");
            fileData.AppendFormat("            {0} ret = get_obj(nThisPtr);\r\n", m_szCurClassName);
            fileData.Append(FCValueType.SetMemberValue("            ", ret_value, "arg0", "L", "0", true, false));
            fileData.AppendFormat("            {0} = arg0;\r\n", szLeftName);
            fileData.AppendLine("        }");
            fileData.AppendLine("        catch(Exception e)");
            fileData.AppendLine("        {");
            fileData.AppendLine("            Debug.LogException(e);");
            fileData.AppendLine("        }");
            fileData.AppendLine("        return 0;");
            fileData.AppendLine("    }");
        }
        func.m_szContent = fileData.ToString();
        func.m_szRegister = string.Format("FCLibHelper.fc_register_class_attrib(nClassName,\"{0}\",{1},{2});", func.m_szName, func.m_szGetName, func.m_szSetName);
        m_CurClassFunc.Add(func);
    }

    // 功能：检测函数是不是模板函数
    bool IsTemplateFunc(MethodInfo method)
    {
        string szMethodName = method.ToString();
        // xxx func[T, V](...);
        int nIndex = szMethodName.IndexOf('(');
        if (nIndex == -1)
            return false;
        if(nIndex > 0 && szMethodName[nIndex-1] == ']')
        {
            return true;
        }
        return false;
    }
    
    // 功能：添加函数调用的方法
    void PushMethodInfo(MethodInfo method)
    {
        if (0 != (MethodAttributes.SpecialName & method.Attributes))
        {
            return;
        }
        string szMethodName = method.ToString();
        bool bTemplateFunc = IsTemplateFunc(method);
        // 模板函数暂时不导出吧
        if(bTemplateFunc)
        {
            return;
        }
        FCValueType  ret_value = m_templateWrap.PushReturnTypeWrap(method.ReturnType);

        int nSameNameCount = 0;
        if(m_CurSameName.TryGetValue(method.Name, out nSameNameCount))
        {
        }
        m_CurSameName[method.Name] = nSameNameCount + 1;
        int nFuncCount = 0;
        m_CurFuncCount.TryGetValue(method.Name, out nFuncCount);

        WrapFuncDesc func = new WrapFuncDesc();
        func.m_bAttrib = false;
        func.m_szName = method.Name;
        if(nSameNameCount > 0)
            func.m_szGetName = func.m_szSetName = string.Format("{0}{1}_wrap", method.Name, nSameNameCount);
        else
            func.m_szGetName = func.m_szSetName = string.Format("{0}_wrap", method.Name);
        
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;

        ParameterInfo[] allParams = method.GetParameters();  // 函数参数
        Type nRetType = method.ReturnType;   // 返回值
        int nParamCount = allParams != null ? allParams.Length : 0;
        bool bEqualFunc = func.m_szName == "Equals";

        fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
        fileData.AppendFormat("    public static int {0}(long L)\r\n", func.m_szSetName);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");

        fileData.AppendLine("            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);");
        fileData.AppendFormat("            {0} obj = get_obj(nThisPtr);\r\n", m_szCurClassName);
        // 处理函数参数
        Type nParamType;
        string szLeftName = string.Empty;
        string szCallParam = string.Empty;
        string szLeftType = string.Empty;
        bool bStatic = method.IsStatic;
        string szFullFuncName = method.Name;
        for (int i = 0; i<nParamCount; ++i)
        {
            ParameterInfo param = allParams[i];
            nParamType = param.ParameterType;
            szLeftName = string.Format("arg{0}", i);
            FCValueType param_value = FCTemplateWrap.Instance.PushGetTypeWrap(nParamType);
            if (param.IsOut)
            {
                string szCSharpName = param_value.GetTypeName(true);
                fileData.AppendFormat("            {0} {1};\r\n", szCSharpName, szLeftName);
            }
            else
            {
                fileData.Append(FCValueType.SetMemberValue("            ", param_value, szLeftName, "L", i.ToString(), true, param.IsOut));
            }
            if (i > 0)
                szCallParam += ',';
            if(param.IsOut)
            {
                szCallParam += "out ";
            }
            else if(nParamType.IsByRef)
            {
                szCallParam += "ref ";
            }
            szCallParam += szLeftName;
            szFullFuncName = szFullFuncName + '_' + param_value.GetTypeName(true);
        }
        // 处理返回值
        if (ret_value.m_nValueType == fc_value_type.fc_value_void)
        {
            if (bStatic)
                fileData.AppendFormat("            {0}.{1}({2});\r\n", m_szCurClassName, func.m_szName, szCallParam);
            else
                fileData.AppendFormat("            obj.{0}({1});\r\n", func.m_szName, szCallParam);
        }
        else
        {
            string szCShareRetName = ret_value.GetTypeName(true);
            if (bStatic)
                fileData.AppendFormat("            {0} ret = {1}.{2}({3});\r\n", szCShareRetName, m_szCurClassName, func.m_szName, szCallParam);
            else
                fileData.AppendFormat("            {0} ret = obj.{1}({2});\r\n", szCShareRetName, func.m_szName, szCallParam);
        }

        // 处理输出参数
        for (int i = 0; i < nParamCount; ++i)
        {
            ParameterInfo param = allParams[i];
            nParamType = param.ParameterType;
            szLeftName = string.Format("arg{0}", i);
            if (param.IsOut || nParamType.IsByRef)
            {
                FCValueType value = PushOutParamWrap(nParamType);
                fileData.Append(FCValueType.ModifyScriptCallParam("            ", value, szLeftName, "L", i.ToString(), true));
            }
        }
        // 处理返回值
        if (ret_value.m_nValueType != fc_value_type.fc_value_void)
        {
            FCValueType.PushReturnValue(fileData, "            ", ret_value, "L", "ret", false);
        }
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        return 0;");
        fileData.AppendLine("    }");

        func.m_szContent = fileData.ToString();
        if(nFuncCount > 1)
            func.m_szRegister = string.Format("FCLibHelper.fc_register_class_func(nClassName,\"{0}\",{1});", szFullFuncName, func.m_szGetName);
        else
            func.m_szRegister = string.Format("FCLibHelper.fc_register_class_func(nClassName,\"{0}\",{1});", func.m_szName, func.m_szGetName);
        m_CurClassFunc.Add(func);        
    }
    // 添加回传的参数的wrap
    FCValueType PushOutParamWrap(Type nType)
    {
        return m_templateWrap.PushOutTypeWrap(nType);
    }
}
