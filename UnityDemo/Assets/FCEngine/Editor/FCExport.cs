﻿using System;
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
        pWrap.PushCurrentDontWrapName("allowOcclusionWhenDynamic");
        pWrap.WrapClass(typeof(UnityEngine.Renderer));
        pWrap.WrapClass(typeof(UnityEngine.MeshRenderer));
        pWrap.WrapClass(typeof(UnityEngine.SkinnedMeshRenderer));
        pWrap.PushCurrentDontWrapName("IsJoystickPreconfigured");
        pWrap.WrapClass(typeof(UnityEngine.Input));
        pWrap.PushCurrentDontWrapName("areaSize");
        pWrap.PushCurrentDontWrapName("lightmapBakeType");
        pWrap.PushCurrentDontWrapName("bakingOutput");
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

    static void MakeFCProj()
    {
        string szRoot = Application.dataPath;
        szRoot = szRoot.Substring(0, szRoot.Length - 6);
        string szPath = szRoot + "Script/inport/";
        string [] allPathFileNames = Directory.GetFiles(szPath, "*.cs", SearchOption.AllDirectories);
        string szProjPathName = szRoot + "Script/FCProj.csproj";
        if (!File.Exists(szProjPathName))
            return;
        StreamReader file = new StreamReader(szProjPathName);
        bool bFindInner = false;
        bool bAdd = false;
        StringBuilder fileData = new StringBuilder(1024 * 1024 * 2);
        while(true)
        {
            string szLine = file.ReadLine();
            if (string.IsNullOrEmpty(szLine))
                break;
            if(szLine.IndexOf("inner_class\\") != -1)
            {
                bFindInner = true;
                fileData.AppendLine(szLine);
                continue;
            }
            if (szLine.IndexOf("Include=\"inport\\") != -1)
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
                    szInportLine = string.Format("    <Compile Include=\"inport\\{0}\" />", szFileName);
                    fileData.AppendLine(szInportLine);
                }
            }
            fileData.AppendLine(szLine);
        }
        file.Close();
        File.Delete(szProjPathName);
        File.WriteAllText(szProjPathName, fileData.ToString());
    }

    [MenuItem("FCScript/编译脚本 _F7", false, 5)]
    static void CompilerScript()
    {
        string szPath = Application.dataPath;
        szPath = szPath.Substring(0, szPath.Length - 6);
        szPath += "脚本测试工程.fcproj";
        FCCompilerHelper.CompilerProj(szPath);
    }
    [MenuItem("FCScript/测试", false, 5)]
    static void TestExport()
    {
        Type t1 = typeof(IEnumerable<AssetBundle>);
        FCValueType v1 = FCValueType.TransType(t1);
        string s1 = v1.GetValueName(true);
        int iiii = 0;

        FCClassWrap pWrap = new FCClassWrap();
        pWrap.BeginExport("");

        pWrap.BeginModleWrap("AutoClass");
        pWrap.WrapClass(typeof(UnityEngine.AssetBundle));
        pWrap.EndModleWrap();

        pWrap.EndExport();
        MakeFCProj();
    }
}
