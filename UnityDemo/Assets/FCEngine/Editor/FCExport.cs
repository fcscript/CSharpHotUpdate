using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

public static class FCExport
{
    [MenuItem("FCScript/示例导出脚本类", false, 5)]
    static void ExportSpecial()
    {
        FCClassWrap pWrap = new FCClassWrap();
        pWrap.BeginExport("");

        WrapUnityClass(pWrap);
        WrapUIClass(pWrap);
        WrapCustomAttribClass(pWrap); // 导出打有[ClassAutoWrap]标签的类

        pWrap.EndExport();
        MakeFCProj();
    }
    [MenuItem("FCScript/全部导出脚本类", false, 5)]
    static void ExportAll()
    {
        FCClassWrap pWrap = new FCClassWrap();
        pWrap.BeginExport("");

        Dictionary<string, List<Type>> allExportTypes = FCExclude.GetAllExportType();
        foreach(var v in allExportTypes)
        {
            WrapAllClass(pWrap, v.Key, v.Value);
        }
        WrapCustomAttribClass(pWrap); // 导出打有[ClassAutoWrap]标签的类
        pWrap.EndExport();
        MakeFCProj();
    }
    [MenuItem("FCScript/精简导出脚本类", false, 5)]
    static void ExportSimple()
    {
        // 先加载配置表
        string szPath = Application.dataPath;
        szPath = szPath.Substring(0, szPath.Length - 6);
        FCRefClassCfg used_cfg = FCRefClassCfg.LoadCfg(szPath + "ref_name.xml");
        FCRefClassCfg custom = FCRefClassCfg.LoadCfg(szPath + "custom_name.xml");
        if (used_cfg != null)
            used_cfg.MergeFinder(custom);

        FCClassWrap pWrap = new FCClassWrap();
        pWrap.BeginExport("");

        pWrap.SetRefClassCfg(used_cfg);
        WrapUnityClass(pWrap);
        pWrap.SetRefClassCfg(used_cfg);
        WrapUIClass(pWrap);
        pWrap.SetRefClassCfg(null);
        WrapCustomAttribClass(pWrap); // 导出打有[ClassAutoWrap]标签的类

        pWrap.EndExport();
        MakeFCProj();
    }
    [MenuItem("FCScript/清除Wrap脚本", false, 5)]
    static void ClearWrapFile()
    {
        string szDataPath = Application.dataPath;
        string szExportPath = szDataPath + "/FCWrap/";
        string szFCScriptPath = szDataPath.Substring(0, szDataPath.Length - 6) + "Script/inport/";
        FCClassWrap.DeletePath(szExportPath);
        FCClassWrap.DeletePath(szFCScriptPath);
        MakeEmptyWrap(szExportPath);
    }
    static void MakeEmptyWrap(string szExportPath)
    {
        string szPathName = szExportPath + "all_class_wrap.cs";
        // 这里只导出一个
        StringBuilder fileData = new StringBuilder(1024 * 1024 * 1);
        fileData.AppendLine("using System;");
        fileData.AppendLine("using UnityEngine;\r\n");
        fileData.AppendLine("using UnityEngine.Rendering;\r\n");
        fileData.AppendLine("");
        fileData.AppendLine("public class all_class_wrap");
        fileData.AppendLine("{");
        fileData.AppendLine("    public static void Register()");
        fileData.AppendLine("    {");
        fileData.AppendLine("    }");
        fileData.AppendLine("}");
        File.WriteAllText(szPathName, fileData.ToString());
    }

    static void WrapUnityClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("Unity");
        
        WrapType(pWrap, typeof(UnityEngine.Time));
        WrapType(pWrap, typeof(UnityEngine.Object));
        WrapType(pWrap, typeof(UnityEngine.Component));
        WrapType(pWrap, typeof(UnityEngine.Transform));
        WrapType(pWrap, typeof(UnityEngine.Texture2D));
        WrapType(pWrap, typeof(UnityEngine.GameObject));
        WrapType(pWrap, typeof(UnityEngine.Behaviour));
        WrapType(pWrap, typeof(UnityEngine.MonoBehaviour));
        WrapType(pWrap, typeof(UnityEngine.Animation));
        WrapType(pWrap, typeof(UnityEngine.Renderer));
        WrapType(pWrap, typeof(UnityEngine.MeshRenderer));
        WrapType(pWrap, typeof(UnityEngine.SkinnedMeshRenderer));
        WrapType(pWrap, typeof(UnityEngine.Input));
        WrapType(pWrap, typeof(UnityEngine.Light));
        WrapType(pWrap, typeof(UnityEngine.Material));
        WrapType(pWrap, typeof(UnityEngine.Events.UnityEvent));
        WrapType(pWrap, typeof(UnityEngine.AsyncOperation));
        WrapType(pWrap, typeof(UnityEngine.SceneManagement.Scene));
        WrapType(pWrap, typeof(UnityEngine.SceneManagement.SceneManager));

        pWrap.EndModleWrap();
    }
    static void WrapType(FCClassWrap pWrap, Type nType)
    {
        PrepareWrap(pWrap, nType);
        pWrap.WrapClass(nType, false);
    }

    static void WrapUIClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("UnityUI");
        // 导出UI类
        WrapType(pWrap, typeof(UnityEngine.UI.Button));
        WrapType(pWrap, typeof(UnityEngine.UI.Text));

        pWrap.EndModleWrap();

    }
    // 功能：导出带有标签的类
    static void WrapCustomAttribClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("AutoClass");

        Assembly assembly = Assembly.Load("Assembly-CSharp");
        Type[] types = assembly.GetExportedTypes();
        foreach (Type t in types)
        {
            if (t.IsDefined(typeof(AutoWrapAttribute), false))
            {
                pWrap.WrapClass(t, false);
            }
            else if (t.IsDefined(typeof(PartWrapAttribute), false))
            {
                pWrap.WrapClass(t, true);
            }
        }
        pWrap.EndModleWrap();
    }

    // 功能：导出这个模块下的所有类
    static void WrapAllClass(FCClassWrap pWrap, string szNamespace, List<Type> rList)
    {
        string szModleName = szNamespace.Replace('.', '_');
        pWrap.BeginModleWrap(szModleName);
        int nIndex = 0;
        foreach(Type t in rList)
        {
            ++nIndex;
            try
            {
                PrepareWrap(pWrap, t);
                pWrap.WrapClass(t);
            }
            catch (Exception e)
            {
                Debug.LogError("导出异常, nIndex = " + nIndex);
                Debug.LogException(e);
            }
        }                
        pWrap.EndModleWrap();
    }

    static void PrepareWrap(FCClassWrap pWrap, Type nClassType)
    {
        // 添加黑名单函数
        List<string> rList = FCExclude.GetClassBlackList(nClassType);
        if(rList != null)
        {
            foreach(string funcName in rList)
            {
                pWrap.PushCurrentDontWrapName(funcName);
            }
        }
        // 目前只有两个类型支持模板函数，其他的需要用户自己扩展
        if(nClassType == typeof(UnityEngine.Component)
            || nClassType == typeof(UnityEngine.GameObject))
        {
            List<Type> aSupportType = FCExclude.SupportTemplateTypes;
            pWrap.PushTemplateFuncWrapSupport("AddComponent", aSupportType);
            pWrap.PushTemplateFuncWrapSupport("GetComponent", aSupportType);
        }
    }
    
    static void AddTemplateSurport(FCClassWrap pWrap)
    {
        List<Type> aSupportType = FCExclude.SupportTemplateTypes;
        pWrap.PushTemplateFuncWrapSupport("AddComponent", aSupportType);
        pWrap.PushTemplateFuncWrapSupport("GetComponent", aSupportType);
    }

    static public void InportPathToFCProj(string szInporPath)
    {
        string szRoot = Application.dataPath;
        szRoot = szRoot.Substring(0, szRoot.Length - 6);
        string szPath = szRoot + "Script/" + szInporPath + '/';  // szRoot + "Script/inport/";
        string[] allPathFileNames = Directory.GetFiles(szPath, "*.cs", SearchOption.AllDirectories);
        string szProjPathName = szRoot + "Script/FCProj.csproj";
        if (!File.Exists(szProjPathName))
            return;
        string InportKey = string.Format("Include=\"{0}\\", szInporPath);  // Include=\"inport\\
        StreamReader file = new StreamReader(szProjPathName);
        bool bFindInner = false;
        bool bAdd = false;
        StringBuilder fileData = new StringBuilder(1024 * 1024 * 2);
        // 写入UTF8的头
        while (true)
        {
            string szLine = file.ReadLine();
            if (string.IsNullOrEmpty(szLine))
                break;
            if (szLine.IndexOf("inner_class\\") != -1)
            {
                bFindInner = true;
                fileData.AppendLine(szLine);
                continue;
            }
            //if (szLine.IndexOf("Include=\"inport\\") != -1)
            if(szLine.IndexOf(InportKey) != -1)
            {
                continue;
            }
            if (bFindInner && !bAdd)
            {
                bAdd = true;
                string szInportLine = string.Empty;
                string szFileName = string.Empty;
                Dictionary<string, bool> fileFlags = new Dictionary<string, bool>();
                foreach (string szPathName in allPathFileNames)
                {
                    szFileName = szPathName.Substring(szPath.Length);
                    if (fileFlags.ContainsKey(szFileName))
                        continue;
                    fileFlags[szFileName] = true;
                    //szInportLine = string.Format("    <Compile Include=\"inport\\{0}\" />", szFileName);
                    szInportLine = string.Format("    <Compile Include=\"{0}\\{1}\" />", szInporPath, szFileName);
                    fileData.AppendLine(szInportLine);
                }
            }
            fileData.AppendLine(szLine);
        }
        file.Close();
        File.Delete(szProjPathName);
        File.WriteAllText(szProjPathName, fileData.ToString());
    }

    static public void MakeFCProj()
    {
        InportPathToFCProj("inport");
    }
    [UnityEditor.Callbacks.DidReloadScripts]
    static void OnUnityCompilerCallback()
    {
        Debug.Log("OnUnityCompilerCallback");
		#if UNITY_STANDALONE_WIN
        if (FCLibHelper.fc_is_init())
        {
            FCScriptLoader Loader = MonoBehaviour.FindObjectOfType<FCScriptLoader>();
            if (Loader != null)
            {
                Loader.OnAfterScriptCompiler(OnAfterLoadScript);
            }
            else
            {
                FCLibHelper.fc_set_debug_print_func(FCScriptLoader.print_error);
                FCLibHelper.fc_set_output_error_func(FCScriptLoader.print_error);
                all_class_wrap.Register();
            }
        }
		#endif
    }
    static void OnAfterLoadScript()
    {
        ScriptMono[] Scripts = MonoBehaviour.FindObjectsOfType<ScriptMono>();
        if (Scripts != null)
        {
            for (int i = 0; i < Scripts.Length; ++i)
            {
                if (Scripts[i] != null)
                    Scripts[i].OnAfterScriptCompiler();
            }
        }
    }

    [MenuItem("FCScript/编译脚本 _F7", false, 5)]
    static void CompilerScript()
    {
        string szPath = Application.dataPath;
        szPath = szPath.Substring(0, szPath.Length - 6);
        szPath += "脚本测试工程.fcproj";
        Debug.Log(szPath);
        FCCompilerHelper.CompilerProj(szPath, false);
    }
    [MenuItem("FCScript/编译导出引用接口", false, 5)]
    static void CompilerScriptAndSaveXml()
    {
        Debug.Log("特别说明：如果您想要导出精简接口，用于打包前的精简代码，就必须先使用全量导出，再执行这个命令，生成ref_name.xml");
        string szPath = Application.dataPath;
        szPath = szPath.Substring(0, szPath.Length - 6);
        szPath += "脚本测试工程.fcproj";
        FCCompilerHelper.CompilerProj(szPath, true);
    }
    //[MenuItem("FCScript/测试", false, 5)]
    //static void TestExport()
    //{
    //    FCClassWrap pWrap = new FCClassWrap();
    //    pWrap.BeginExport("", false);

    //    WrapUnityClass(pWrap);
    //    WrapUIClass(pWrap);
    //    WrapCustomAttribClass(pWrap); // 导出打有[ClassAutoWrap]标签的类

    //    pWrap.EndExport();
    //    MakeFCProj();
    //}
}
