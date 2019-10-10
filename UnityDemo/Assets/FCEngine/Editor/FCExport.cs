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
    [MenuItem("FCScript/导出脚本类", false, 5)]
    static void ExportAll()
    {
        FCClassWrap pWrap = new FCClassWrap();
        pWrap.BeginExport("");

        WrapUnityClass(pWrap);
        WrapUIClass(pWrap);
        WrapCustomAttribClass(pWrap); // 导出打有[ClassAutoWrap]标签的类

        pWrap.EndExport();
    }
    [MenuItem("FCScript/精简导出脚本类", false, 5)]
    static void ExportSimple()
    {
        // 先加载配置表
        string szPath = Application.dataPath;
        szPath = szPath.Substring(0, szPath.Length - 6);
        FCRefClassCfg used_cfg = FCRefClassCfg.LoadCfg(szPath + "ref_name.xml");
        FCRefClassCfg custom = FCRefClassCfg.LoadCfg(szPath + "custom_name.xml");
        if(used_cfg != null)
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

        //pWrap.WrapClass(typeof(System.Type));
        pWrap.WrapClass(typeof(UnityEngine.Time));
        pWrap.WrapClass(typeof(UnityEngine.Object));
        AddTemplateSurport(pWrap); // 添加模板函数的wrap支持
        pWrap.WrapClass(typeof(UnityEngine.Component));
        pWrap.WrapClass(typeof(UnityEngine.Transform));
        pWrap.PushCurrentDontWrapName("alphaIsTransparency");
        pWrap.WrapClass(typeof(UnityEngine.Texture2D));
        AddTemplateSurport(pWrap); // 添加模板函数的wrap支持
        pWrap.WrapClass(typeof(UnityEngine.GameObject));
        pWrap.WrapClass(typeof(UnityEngine.Behaviour));
        pWrap.PushCurrentDontWrapName("Item");
        pWrap.WrapClass(typeof(UnityEngine.Animation));
        pWrap.WrapClass(typeof(UnityEngine.Transform));
        pWrap.WrapClass(typeof(UnityEngine.Renderer));
        pWrap.WrapClass(typeof(UnityEngine.MeshRenderer));
        pWrap.WrapClass(typeof(UnityEngine.SkinnedMeshRenderer));
        pWrap.PushCurrentDontWrapName("IsJoystickPreconfigured");
        pWrap.WrapClass(typeof(UnityEngine.Input));
        pWrap.PushCurrentDontWrapName("areaSize");
        pWrap.PushCurrentDontWrapName("lightmapBakeType");
        pWrap.WrapClass(typeof(UnityEngine.Light));
        pWrap.WrapClass(typeof(UnityEngine.Material));
        pWrap.WrapClass(typeof(UnityEngine.Events.UnityEvent));

        pWrap.EndModleWrap();
    }
    static void WrapUIClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("UnityUI");
        // 导出UI类
        pWrap.WrapClass(typeof(UnityEngine.UI.Button));
        pWrap.PushCurrentDontWrapName("OnRebuildRequested");
        pWrap.WrapClass(typeof(UnityEngine.UI.Text));

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
    static void AddTemplateSurport(FCClassWrap pWrap)
    {
        List<Type> aSupportType = new List<Type>();
        aSupportType.Add(typeof(UnityEngine.SkinnedMeshRenderer));
        aSupportType.Add(typeof(UnityEngine.MeshRenderer));
        aSupportType.Add(typeof(UnityEngine.Animation));
        aSupportType.Add(typeof(UnityEngine.Light));

        aSupportType.Add(typeof(UnityEngine.UI.Button));
        aSupportType.Add(typeof(UnityEngine.UI.Text));
        // 在这里添加其他的类的吧

        pWrap.PushTemplateFuncWrapSupport("AddComponent", aSupportType);
        pWrap.PushTemplateFuncWrapSupport("GetComponent", aSupportType);
    }

    [MenuItem("FCScript/编译脚本 _F7", false, 5)]
    static void CompilerScript()
    {
        string szPath = Application.dataPath;
        szPath = szPath.Substring(0, szPath.Length - 6);
        szPath += "脚本测试工程.fcproj";
        FCCompilerHelper.CompilerProj(szPath);
    }
    //[MenuItem("FCScript/测试", false, 5)]
    //static void TestXml()
    //{
    //    Assembly assembly = Assembly.Load("UnityEngine.UI");
    //    Type t1 = assembly.GetType("Button");
    //    Type t2 = assembly.GetType("UnityEngine.UI.Button");
    //    Type t3 = assembly.GetType("UnityEngine.Button");                
    //}
}
