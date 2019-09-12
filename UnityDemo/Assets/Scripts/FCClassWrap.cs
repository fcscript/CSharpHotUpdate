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
    bool m_bPartWrap = false;
    bool m_bOnlyThisAPI = false;
    Type m_nCurClassType;

    List<string> m_AllWrapClassName = new List<string>(); // 所有wrap的类名
    List<string> m_CurWrapClassNames = new List<string>(); // 当前模块wrap的类名
    List<WrapFuncDesc> m_CurClassFunc = new List<WrapFuncDesc>();
    Dictionary<string, int> m_CurSameName = new Dictionary<string, int>();
    Dictionary<string, int> m_CurFuncCount = new Dictionary<string, int>();
    List<string> m_CurRefNameSpace = new List<string>();
    Dictionary<string, int> m_CurRefNameSpacesFlags = new Dictionary<string, int>();
    List<string> m_AllRefNameSpace = new List<string>();
    Dictionary<string, int> m_AllRefNameSpaceFlags = new Dictionary<string, int>();

    Dictionary<string, int> m_CurDontWrapName = new Dictionary<string, int>();
    Dictionary<string, List<Type>> m_CurSupportTemplateFunc = new Dictionary<string, List<Type>>(); // 支持导出的wrap函数

    StringBuilder m_szTempBuilder;

    FCClassExport m_export;
    FCTemplateWrap m_templateWrap;
    FCDelegateWrap m_deleteWrap;

    // 功能：
    public FCClassWrap()
    {
    }

    // 功能：删除指定目录下的所有文件
    public static void  DeletePath(string szPath)
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
            //m_szExportPath = Application.dataPath;
            //m_szExportPath = m_szExportPath.Substring(0, m_szExportPath.Length - 6) + "FCWrap/";
            m_szExportPath = Application.dataPath + "/FCWrap/";
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
        m_deleteWrap = new FCDelegateWrap();
        m_deleteWrap.BeginExport(m_szExportPath);
    }

    // 功能：结束所有的导出
    public void EndExport()
    {
        m_deleteWrap.EndExport(m_szTempBuilder);
        m_templateWrap.EndExport(m_szTempBuilder);
        m_export.ExportDefaultClass(m_szFCScriptPath);
        ExportWrapInit("all_class_wrap", m_AllWrapClassName);
    }

    void  ExportWrapInit(string szClassName, List<string> aWrapNames)
    {
        string szPathName = m_szExportPath + szClassName + ".cs";
        // 这里只导出一个

        StringBuilder fileData = new StringBuilder(1024 * 1024 * 1);
        fileData.AppendLine("using System;");
        fileData.AppendLine("using UnityEngine;\r\n");
        fileData.AppendLine("using UnityEngine.Rendering;\r\n");
        fileData.AppendLine("");
        fileData.AppendFormat("public class {0}\r\n", szClassName);
        fileData.AppendLine("{");
        fileData.AppendLine("    public static void Register()");
        fileData.AppendLine("    {");
        foreach(string szCurClassName in aWrapNames)
        {
            fileData.AppendFormat("        {0}.Register();\r\n", szCurClassName);
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
        string szModelClassName = m_szCurModleName + "_wrap";
        ExportWrapInit(szModelClassName, m_CurWrapClassNames);
        m_AllWrapClassName.Add(szModelClassName);
    }

    // 功能：添加当前禁止导出的接口名字
    public void  PushCurrentDontWrapName(string szName)
    {
        m_CurDontWrapName[szName] = 1;
    }
    // 功能：添加支持模板导出的接口信息
    // 参数：szFuncName - 支持导出的函数名
    //       aSurportType - 支持导出的类型
    public void  PushTemplateFuncWrapSupport(string szFuncName, List<Type> aSupportType)
    {
        m_CurSupportTemplateFunc[szFuncName] = aSupportType;
    }

    public void  WrapClass(Type nClassType, bool bPartWrap = false)
    {
        WrapClassEx(nClassType, bPartWrap, true);
    }
    public void WrapClassEx(Type nClassType, bool bPartWrap, bool bOnlyThisApi)
    {
        m_bPartWrap = bPartWrap;
        m_bOnlyThisAPI = bOnlyThisApi;
        m_szTempBuilder.Length = 0;
        string szWrapName = FCValueType.GetClassName(nClassType) + "_wrap";
        m_CurWrapClassNames.Add(szWrapName);
        m_szCurClassName = FCValueType.GetClassName(nClassType);
        m_nCurClassType = nClassType;
        WrapSubClass(m_szTempBuilder, nClassType);
        m_export.ExportClass(nClassType, m_szFCScriptPath + m_szCurClassName + ".cs", bPartWrap, bOnlyThisApi, m_CurDontWrapName, m_CurSupportTemplateFunc);
        m_CurDontWrapName.Clear();
        m_CurSupportTemplateFunc.Clear();
    }

    void  WrapSubClass(StringBuilder fileData, Type nClassType)
    {
        string szWrapName = FCValueType.GetClassName(nClassType) + "_wrap";
        m_CurClassFunc.Clear();
        m_CurSameName.Clear();
        m_CurRefNameSpace.Clear();
        m_CurRefNameSpacesFlags.Clear();

        PushNameSpace("System");
        PushNameSpace("System.Collections.Generic");
        PushNameSpace("System.Text");
        PushNameSpace("UnityEngine");
        PushNameSpace("UnityObject = UnityEngine.Object"); // 给这个家伙换个名字吧
        //PushNameSpace("UnityEngine.Rendering");

        // 先生成init函数
        FieldInfo[] allFields = FCValueType.GetFields(nClassType, m_bOnlyThisAPI);
        PropertyInfo[] allProperties = FCValueType.GetProperties(nClassType, m_bOnlyThisAPI);
        MethodInfo[] allMethods = FCValueType.GetMethods(nClassType, m_bOnlyThisAPI);
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
        MakeInitFunc(FCValueType.GetClassName(nClassType));
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
        foreach(string szNameSpace in m_CurRefNameSpace)
        {
            fileData.AppendFormat("using {0};\r\n", szNameSpace);
        }
        fileData.AppendLine("");
        //fileData.AppendLine("using System;");
        //fileData.AppendLine("using System.Collections.Generic;");
        //fileData.AppendLine("using System.Text;");
        //fileData.AppendLine("using UnityEngine;\r\n");
        //if (!string.IsNullOrEmpty(szNamespace) && szNamespace != "UnityEngine")
        //    fileData.AppendFormat("using {0};\r\n", szNamespace);

        fileData.AppendFormat("public class {0}\r\n", szWrapName);
        fileData.AppendLine("{");
        foreach (WrapFuncDesc func in m_CurClassFunc)
        {
            fileData.AppendLine(func.m_szContent);
        }
        fileData.AppendLine("}");
        File.WriteAllText(szPathName, fileData.ToString());
    }
    void  PushNameSpace(string szNameSpace)
    {
        if (string.IsNullOrEmpty(szNameSpace))
            return;
        if (m_CurRefNameSpacesFlags.ContainsKey(szNameSpace))
            return;
        m_CurRefNameSpacesFlags[szNameSpace] = 1;
        m_CurRefNameSpace.Add(szNameSpace);

        if(!m_AllRefNameSpaceFlags.ContainsKey(szNameSpace))
        {
            m_AllRefNameSpaceFlags[szNameSpace] = 1;
            m_AllRefNameSpace.Add(szNameSpace);
        }
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
        fileData.AppendLine("        long ret = FCLibHelper.fc_get_return_ptr(L);");
        fileData.AppendLine("        FCLibHelper.fc_set_value_intptr(ret, nPtr);");
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
                SetMemberValue(fileData, "            ", param_value, szLeftName, "L", i.ToString(), true, param.IsOut);
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
        fileData.AppendLine("            long ret = FCLibHelper.fc_get_return_ptr(L);");
        fileData.AppendLine("            FCLibHelper.fc_set_value_intptr(ret, nPtr);");        
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
        if(value.IsDefined(typeof(ObsoleteAttribute), false))
        {
            return;
        }

        PushNameSpace(value.FieldType.Namespace);

        // 生成get_value, set_value方法
        FCValueType ret_value = FCTemplateWrap.Instance.PushGetTypeWrap(value.FieldType);
        if(ret_value.m_nTemplateType == fc_value_tempalte_type.template_none
            && ret_value.m_nValueType == fc_value_type.fc_value_delegate)
        {
            PushPropertyDelegate(ret_value, value.Name, false, true, value.IsStatic); // // 委托只能set, 不可get
        }
        else
            PushPropertyFunc(value.FieldType, value.Name, true, true, value.IsStatic);
    }
    void PushPropertyDelegate(FCValueType ret_value, string szName, bool bCanGet, bool bCanSet, bool bStatic)
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

        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;
        string szLeftName = string.Format("ret.{0}", szName);
        if (bStatic)
            szLeftName = string.Format("{0}.{1}", m_szCurClassName, szName);
        
        string szDelegateClassName = string.Format("{0}_deletate", ret_value.GetDelegateName(true));
        m_deleteWrap.PushDelegateWrap(ret_value.m_value, szDelegateClassName);

        if (bCanSet)
        {
            fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
            fileData.AppendFormat("    public static int {0}(long L)\r\n", func.m_szSetName);
            fileData.AppendLine("    {");
            fileData.AppendLine("        try");
            fileData.AppendLine("        {");

            fileData.AppendFormat("            {0} func = FCDelegateMng.Instance.GetDelegate<{1}>(L);\r\n", szDelegateClassName, szDelegateClassName);
            if (bStatic)
            {
                fileData.AppendLine("            if(func != null)");
                fileData.AppendFormat("                {0} = func.CallFunc;\r\n", szLeftName);
                fileData.AppendLine("            else");
                fileData.AppendFormat("                {0} = null;\r\n", szLeftName);
            }
            else
            {
                fileData.AppendLine("            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);");
                fileData.AppendFormat("            {0} ret = get_obj(nThisPtr);\r\n", m_szCurClassName);
                fileData.AppendLine("            if(func != null)");
                fileData.AppendFormat("                {0} = func.CallFunc;\r\n", szLeftName);
                fileData.AppendLine("            else");
                fileData.AppendFormat("                {0} = null;\r\n", szLeftName);
            }
            fileData.AppendFormat("            FCDelegateMng.Instance.RecordDelegate({0}, func);\r\n", szLeftName);
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
        if(m_CurDontWrapName.ContainsKey(property.Name))
        {
            return;
        }
        //if(property.IsDefined(typeof(DefaultMemberAttribute), false))
        //{
        //    return;
        //}
        Type nVaueType = property.PropertyType;
        PushNameSpace(nVaueType.Namespace);

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
            m_templateWrap.PushReturnTypeWrap(nVaueType);

            fileData.AppendLine("    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]");
            fileData.AppendFormat("    public static int {0}(long L)\r\n", func.m_szGetName);
            fileData.AppendLine("    {");
            fileData.AppendLine("        try");
            fileData.AppendLine("        {");
            fileData.AppendLine("            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);");
            fileData.AppendFormat("            {0} ret = get_obj(nThisPtr);\r\n", m_szCurClassName);
            fileData.AppendLine("            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);");
            FCValueType.PushReturnValue(fileData, "            ", ret_value, "ret_ptr", szLeftName, true);
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
            SetMemberValue(fileData, "            ", ret_value, "arg0", "L", "0", true, false);
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
    // 功能：判断指定类型是不是委托类型
    bool IsDelegate(Type nType)
    {
        if (!typeof(System.MulticastDelegate).IsAssignableFrom(nType) || nType == typeof(System.MulticastDelegate))
        {
            return false;
        }
        return true;
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
        if (m_CurDontWrapName.ContainsKey(method.Name))
        {
            return;
        }
        string szMethodName = method.ToString();
        bool bTemplateFunc = IsTemplateFunc(method);
        // 模板函数暂时不导出吧
        if(bTemplateFunc)
        {
            if (!m_CurSupportTemplateFunc.ContainsKey(method.Name))
                return;
        }
        FCValueType  ret_value = m_templateWrap.PushReturnTypeWrap(method.ReturnType);
        if(ret_value.m_nTemplateType == fc_value_tempalte_type.template_task || ret_value.m_nKeyType == fc_value_type.fc_value_task)
        {
            PushTaskMethod(method);
            return;
        }

        PushNameSpace(method.ReturnType.Namespace);

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
        if(bTemplateFunc)
        {
            fileData.AppendLine("            string arg0 = FCLibHelper.fc_get_string_a(L, 0);");
        }
        // 处理函数参数
        Type nParamType;
        string szLeftName = string.Empty;
        string szCallParam = string.Empty;
        string szLeftType = string.Empty;
        bool bStatic = method.IsStatic;
        string szFullFuncName = method.Name;
        int nParamOffset = bTemplateFunc ? 1 : 0;
        if(bTemplateFunc)
        {
            int nStart = szMethodName.IndexOf('[');
            int nEnd = szMethodName.IndexOf(']');
            string szSubName = szMethodName.Substring(nStart + 1, nEnd - nStart - 1);
            szFullFuncName = szFullFuncName + '_' + szSubName;
        }
        for (int i = 0; i<nParamCount; ++i)
        {
            int nParamIndex = i + nParamOffset;
            ParameterInfo param = allParams[i];
            nParamType = param.ParameterType;
            szLeftName = string.Format("arg{0}", nParamIndex);
            PushNameSpace(nParamType.Namespace);
            FCValueType param_value = FCTemplateWrap.Instance.PushGetTypeWrap(nParamType);
            if (param.IsOut)
            {
                string szCSharpName = param_value.GetTypeName(true);
                fileData.AppendFormat("            {0} {1};\r\n", szCSharpName, szLeftName);
            }
            else
            {
                SetMemberValue(fileData, "            ", param_value, szLeftName, "L", nParamIndex.ToString(), true, param.IsOut);
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
            szFullFuncName = szFullFuncName + '_' + param_value.GetTypeName(false);
        }
        // 处理返回值
        if(!bTemplateFunc)
        {
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
        }        

        // 处理输出参数
        for (int i = 0; i < nParamCount; ++i)
        {
            int nParamIndex = i + nParamCount;
            ParameterInfo param = allParams[i];
            nParamType = param.ParameterType;
            szLeftName = string.Format("arg{0}", nParamIndex);
            if (param.IsOut || nParamType.IsByRef)
            {
                FCValueType value = PushOutParamWrap(nParamType);
                fileData.Append(FCValueType.ModifyScriptCallParam("            ", value, szLeftName, "L", nParamIndex.ToString(), true));
            }
        }
        // 处理返回值
        if (ret_value.m_nValueType != fc_value_type.fc_value_void)
        {
            if (bTemplateFunc)
                PushTemplateFuncWrap(method, szCallParam);
            else
            {
                fileData.AppendLine("            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);");
                FCValueType.PushReturnValue(fileData, "            ", ret_value, "ret_ptr", "ret", false);
            }
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
    // 添加模板函数的导出
    void PushTemplateFuncWrap(MethodInfo method, string szCallParam)
    {
        List<Type> aSupportType = null;
        if(!m_CurSupportTemplateFunc.TryGetValue(method.Name, out aSupportType))
        {
            return;
        }
        StringBuilder fileData = m_szTempBuilder;
        fileData.AppendLine("            long nRetPtr = 0;");
        fileData.AppendLine("            switch(arg0)");
        fileData.AppendLine("            {");
        foreach(Type nType in aSupportType)
        {
            fileData.AppendFormat("                case \"{0}\":\r\n", nType.Name);
            fileData.AppendLine("                {");
            fileData.AppendFormat("                    {0} ret_obj = obj.{1}<{2}>({3});\r\n", nType.Name, method.Name, nType.Name, szCallParam);
            fileData.AppendFormat("                    nRetPtr = FCGetObj.PushObj<{0}>(ret_obj);\r\n", nType.Name);
            fileData.AppendLine("                }");
            fileData.AppendLine("                break;");
        }
        fileData.AppendLine("                default:");
        fileData.AppendLine("                break;");
        fileData.AppendLine("            }");
        fileData.AppendLine("            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);");
        fileData.AppendLine("            FCLibHelper.fc_set_value_intptr(ret_ptr, nRetPtr);");
    }

    // 添加回传的参数的wrap
    FCValueType PushOutParamWrap(Type nType)
    {
        return m_templateWrap.PushOutTypeWrap(nType);
    }
    void SetMemberValue(StringBuilder fileData, string szLeftEmpty, FCValueType value, string szLeftName, string Ptr, string szIndex, bool bTempValue, bool bOut)
    {
        string szCSharpName = value.GetTypeName(true, true);
        string szDefine = string.Empty;
        if (bTempValue)
            szDefine = szCSharpName + " ";
        if (value.IsArray)
        {
            fileData.AppendFormat("{0}{1} {2} = null;\r\n", szLeftEmpty, szCSharpName, szLeftName);
            fileData.AppendFormat("{0}{1} = FCCustomParam.GetArray(ref {2},{3},{4});\r\n", szLeftEmpty, szLeftName, szLeftName, Ptr, szIndex);
            return;
        }
        else if (value.IsList)
        {
            fileData.AppendFormat("{0}{1} {2} = null;\r\n", szLeftEmpty, szCSharpName, szLeftName);
            fileData.AppendFormat("{0}{1} = FCCustomParam.GetList(ref {2},{3},{4});\r\n", szLeftEmpty, szLeftName, szLeftName, Ptr, szIndex);
            return;
        }
        else if (value.IsMap)
        {
            fileData.AppendFormat("{0}{1} {2} = null;\r\n", szLeftEmpty, szCSharpName, szLeftName);
            fileData.AppendFormat("{0}{1} = FCCustomParam.GetDictionary(ref {2},{3},{4});\r\n", szLeftEmpty, szLeftName, szLeftName, Ptr, szIndex);
            return;
        }
        string szFuncAppend = FCValueType.GetFCLibFuncShortName(value.m_nValueType);
        if (string.IsNullOrEmpty(szFuncAppend))
        {
            if (value.m_nValueType == fc_value_type.fc_value_enum)
            {
                fileData.AppendFormat("{0}{1}{2} = ({3})(FCLibHelper.fc_get_int({4},{5}));\r\n", szLeftEmpty, szDefine, szLeftName, szCSharpName, Ptr, szIndex);
                return;
            }
            if (value.m_nValueType == fc_value_type.fc_value_int_ptr)
            {
                fileData.AppendFormat("{0}{1}{2} = ({3})(FCLibHelper.fc_get_intptr({4},{5}));\r\n", szLeftEmpty, szDefine, szLeftName, szCSharpName, Ptr, szIndex);
                return;
            }
            if (value.m_nValueType == fc_value_type.fc_value_system_object)
            {
                fileData.AppendFormat("{0}{1}{2} = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr({3},{4}));\r\n", szLeftEmpty, szDefine, szLeftName, Ptr, szIndex);
                return;
            }
            if(value.m_nValueType == fc_value_type.fc_value_unity_object)
            {
                fileData.AppendFormat("{0}{1}{2} = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr({3},{4}));\r\n", szLeftEmpty, szDefine, szLeftName, Ptr, szIndex);
                return;
            }
            if (value.m_nValueType == fc_value_type.fc_value_delegate)
            {
                string szDelegateClassName = string.Format("{0}_deletate", value.GetDelegateName(true));
                m_deleteWrap.PushDelegateWrap(value.m_value, szDelegateClassName);

                fileData.AppendFormat("{0}{1} func{2} = FCDelegateMng.Instance.GetDelegate<{3}>({4},{5});\r\n", szLeftEmpty, szDelegateClassName, szIndex, szDelegateClassName, Ptr, szIndex);
                fileData.AppendFormat("{0}{1} arg{2} = null;\r\n", szLeftEmpty, szCSharpName, szIndex);
                fileData.AppendFormat("{0}if(func{1} != null)\r\n", szLeftEmpty, szIndex);
                fileData.AppendFormat("{0}    arg{1} = func{2}.CallFunc;\r\n", szLeftEmpty, szIndex, szIndex);
                fileData.AppendFormat("{0}// 尽量不要在函数参数中传递委托指针，这个无法自动托管，要尽可能是使用get, set属性方法\r\n", szLeftEmpty);
                fileData.AppendFormat("{0}// 如果在参数中传递了委托指针，请在对应的函数中调用FCDelegateMng.Instance.RecordDelegate(delegate_func, func);\r\n", szLeftEmpty);
                //fileData.AppendFormat("{0}else\r\n", szLeftEmpty, szIndex);
                //fileData.AppendFormat("{0}    arg{1} = null;\r\n", szLeftEmpty, szIndex);
                //fileData.AppendFormat("{0}FCDelegateMng.Instance.RecordDelegate({1}, func{2});\r\n", szLeftEmpty, szLeftName, szIndex); // 这里不能调用这个，对象不一样的噢
                return;
            }
        }
        else
        {
            if (FCValueType.IsGraphicType(value.m_nValueType))
            {
                fileData.AppendFormat("{0}{1} {2} = new {3}();\r\n", szLeftEmpty, szCSharpName, szLeftName, szCSharpName);
                fileData.AppendFormat("{0}FCLibHelper.fc_get_{1}({2},{3},ref {4});\r\n", szLeftEmpty, szFuncAppend, Ptr, szIndex, szLeftName);
                return;
            }
            else
            {
                fileData.AppendFormat("{0}{1}{2} = FCLibHelper.fc_get_{3}({4},{5});\r\n", szLeftEmpty, szDefine, szLeftName, szFuncAppend, Ptr, szIndex);
                return;
            }
        }
        fileData.AppendFormat("{0}{1}{2} = FCGetObj.GetObj<{3}>(FCLibHelper.fc_get_intptr({4},{5}));\r\n", szLeftEmpty, szDefine, szLeftName, szCSharpName, Ptr, szIndex);
    }

    void  PushTaskMethod(MethodInfo method)
    {
        string szMethodName = method.ToString();
        FCValueType ret_value = m_templateWrap.PushReturnTypeWrap(method.ReturnType);
        bool bStatic = method.IsStatic;

        int nSameNameCount = 0;
        if (m_CurSameName.TryGetValue(method.Name, out nSameNameCount))
        {
        }
        m_CurSameName[method.Name] = nSameNameCount + 1;
        int nFuncCount = 0;
        m_CurFuncCount.TryGetValue(method.Name, out nFuncCount);

        WrapFuncDesc func = new WrapFuncDesc();
        func.m_bAttrib = false;
        func.m_szName = method.Name;
        if (nSameNameCount > 0)
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

        if(!bStatic)
        {
            fileData.AppendLine("            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);");
            fileData.AppendFormat("            {0} obj = get_obj(nThisPtr);\r\n", m_szCurClassName);
        }
        fileData.AppendLine("            long nPtr = FCLibHelper.fc_await(L);");
        fileData.AppendLine("            long nRetPtr = FCLibHelper.fc_get_return_ptr(L);");
        // 处理函数参数
        Type nParamType;
        string szLeftName = string.Empty;
        string szCallParam = string.Empty;
        string szLeftType = string.Empty;
        string szFullFuncName = method.Name;
        for (int i = 0; i < nParamCount; ++i)
        {
            int nParamIndex = i;
            ParameterInfo param = allParams[i];
            nParamType = param.ParameterType;
            szLeftName = string.Format("arg{0}", nParamIndex);
            PushNameSpace(nParamType.Namespace);
            FCValueType param_value = FCTemplateWrap.Instance.PushGetTypeWrap(nParamType);
            if (param.IsOut)
            {
                string szCSharpName = param_value.GetTypeName(true);
                fileData.AppendFormat("            {0} {1};\r\n", szCSharpName, szLeftName);
            }
            else
            {
                SetMemberValue(fileData, "            ", param_value, szLeftName, "L", nParamIndex.ToString(), true, param.IsOut);
            }
            if (i > 0)
                szCallParam += ',';
            if (param.IsOut)
            {
                szCallParam += "out ";
            }
            else if (nParamType.IsByRef)
            {
                szCallParam += "ref ";
            }
            szCallParam += szLeftName;
            szFullFuncName = szFullFuncName + '_' + param_value.GetTypeName(false);
        }
        // 处理返回值
        if(nParamCount> 0)
        {
            if(bStatic)
                fileData.AppendFormat("            {0}_bridge(nPtr, nRetPtr, {1});\r\n", func.m_szName, szCallParam);
            else
                fileData.AppendFormat("            {0}_bridge(obj, nPtr, nRetPtr, {1});\r\n", func.m_szName, szCallParam);
        }
        else
        {
            if (bStatic)
                fileData.AppendFormat("            {0}_bridge(nPtr, nRetPtr);\r\n", func.m_szName);
            else
                fileData.AppendFormat("            {0}_bridge(obj, nPtr, nRetPtr);\r\n", func.m_szName);
        }
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        return 0;");
        fileData.AppendLine("    }");

        func.m_szContent = fileData.ToString();
        if (nFuncCount > 1)
            func.m_szRegister = string.Format("FCLibHelper.fc_register_class_func(nClassName,\"{0}\",{1});", szFullFuncName, func.m_szGetName);
        else
            func.m_szRegister = string.Format("FCLibHelper.fc_register_class_func(nClassName,\"{0}\",{1});", func.m_szName, func.m_szGetName);
        m_CurClassFunc.Add(func);

        PushBridgeMethod(method, szCallParam); // 创建桥接函数
    }
    void PushBridgeMethod(MethodInfo method, string szCallParam)
    {
        m_szTempBuilder.Length = 0;
        StringBuilder fileData = m_szTempBuilder;

        bool bStatic = method.IsStatic;
        ParameterInfo[] allParams = method.GetParameters();  // 函数参数
        int nParamCount = allParams != null ? allParams.Length : 0;
        Type nParamType;
        string szLeftName;
        string szDeclare = string.Empty;
        for (int i = 0; i < nParamCount; ++i)
        {
            int nParamIndex = i;
            ParameterInfo param = allParams[i];
            nParamType = param.ParameterType;
            szLeftName = string.Format("arg{0}", nParamIndex);
            PushNameSpace(nParamType.Namespace);
            FCValueType param_value = FCTemplateWrap.Instance.PushGetTypeWrap(nParamType);
            string szCSharpName = param_value.GetTypeName(true);
            szDeclare += ',';
            if (param.IsOut)
            {
                szDeclare += "out ";
            }
            else if(nParamType.IsByRef)
            {
                szDeclare += "ref ";
            }
            szDeclare += szCSharpName;
            szDeclare += " ";
            szDeclare += szLeftName;
        }
        Type nRetType = method.ReturnType;   // 返回值
        FCValueType ret_value = FCTemplateWrap.Instance.PushGetTypeWrap(nRetType);
        
        string szCallName = string.Empty;
        if(bStatic)
        {
            fileData.AppendFormat("    static async void {0}_bridge(long nPtr, long nRetPtr{1})\r\n", method.Name, szDeclare);
            szCallName = m_szCurClassName + "." + method.Name;
        }
        else
        {
            fileData.AppendFormat("    static async void {0}_bridge({1} obj, long nPtr, long nRetPtr{2})\r\n", method.Name, m_szCurClassName, szDeclare);
            szCallName = "obj." + method.Name;
        }
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        if(ret_value.m_nTemplateType == fc_value_tempalte_type.template_task)
        {
            if(ret_value.m_nValueType != fc_value_type.fc_value_void)
            {
                fileData.AppendFormat("            {0} nRes = await {1}({2});\r\n", ret_value.GetValueName(true), szCallName, szCallParam);
                fileData.AppendLine("            if(FCLibHelper.fc_is_valid_await(nPtr))");
                fileData.AppendLine("            {");
                fileData.AppendLine("                // 设置返回值");
                FCValueType.PushReturnValue(fileData, "                ", ret_value, "nRetPtr", "nRes", false); // 设置返回值
                fileData.AppendLine("                FCLibHelper.fc_continue(nPtr); // 唤醒脚本");
                fileData.AppendLine("            }");
            }
            else
            {
                fileData.AppendFormat("            await {0}({1});\r\n", szCallName, szCallParam);
                fileData.AppendLine("            FCLibHelper.fc_continue(nPtr); // 唤醒脚本");
            }
        }
        else
        {
            fileData.AppendFormat("            await {0}({1});\r\n", szCallName, szCallParam);
            fileData.AppendLine("            FCLibHelper.fc_continue(nPtr); // 唤醒脚本");
        }

        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");

        WrapFuncDesc func = new WrapFuncDesc();
        func.m_szContent = fileData.ToString();
        func.m_szName = func.m_szGetName = func.m_szSetName = string.Format("{0}_bridge", method.Name);
        func.m_bAttrib = false;
        m_CurClassFunc.Add(func);
    }
}
