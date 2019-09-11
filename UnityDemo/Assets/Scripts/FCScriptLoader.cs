using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 功能：加载脚本
/// </summary>

class FCScriptLoader : MonoBehaviour
{
    protected static string s_szFromFC = string.Empty;
    protected static List<string> m_ScriptLog = new List<string>();
    protected static List<string> m_ThreadScriptLog = new List<string>();
    protected static int m_nAddLogCount = 0;

    void Start()
    {
        InitDll();
    }

    public void InitDll()
    {
        if (!FCDll.IsInitDll())
        {
            try
            {
                FCLibHelper.fc_set_debug_print_func(print_error);
                FCLibHelper.fc_set_output_error_func(print_error);

                FCDll.InitDll();

                FCLibHelper.fc_set_debug_print_func(print_error);
                FCLibHelper.fc_set_output_error_func(print_error);
                
                // 启动调试器, 启动后，就可以用工具附加调试了，默认端口是2600
                FCLibHelper.fc_switch_debug(true);
                // FCLibHelper.fc_start_debug(3000); // 指定3000这个端口并启动调试器
            }
            catch (Exception e)
            {
                print_error(e.ToString());
            }
            StartCoroutine(LoadByteCode());
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    public static void print_error(string szInfo)
    {
        lock(m_ThreadScriptLog)
        {
            m_ThreadScriptLog.Add(szInfo);
            m_nAddLogCount++;
        }
        //m_ScriptLog.Add(szInfo);
    }
    protected  List<string>  ScriptLog
    {
        get
        {
            if(m_nAddLogCount > 0)
            {
                lock(m_ThreadScriptLog)
                {
                    m_ScriptLog.AddRange(m_ThreadScriptLog);
                    m_ThreadScriptLog.Clear();
                    m_nAddLogCount = 0;
                }
            }
            return m_ScriptLog;
        }
    }

    public static bool LoadBinText(ref byte[] fileData, AssetBundle bunlde)
    {
        if (bunlde == null)
            return false;
        TextAsset text = bunlde.mainAsset as TextAsset;
        if (text == null)
        {
            string[] Names = bunlde.GetAllAssetNames();
            if (Names != null && Names.Length > 0)
            {
                text = bunlde.LoadAsset(Names[0], typeof(TextAsset)) as TextAsset;
            }
        }
        if (text != null)
        {
            fileData = text.bytes;
            bunlde.Unload(true);
            return true;
        }
        bunlde.Unload(true);
        return false;
    }

    bool LoadByteCodeByFile()
    {
        string szPathName = "test.code";
        print_error("开始加载, 路径：" + szPathName);
        try
        {
            BetterStreamingAssets.Initialize();

            byte[] fileData = BetterStreamingAssets.ReadAllBytes(szPathName);
            if (fileData != null && fileData.Length > 0)
            {
                print_error("加载成功:" + szPathName + ", 字节大小：" + fileData.Length.ToString());
                FCLibHelper.fc_set_code_data(fileData, fileData.Length, 0);
                OnAfterLoadScriptData();
                return true;
            }
        }
        catch (Exception e)
        {
            print_error(e.ToString());
        }
        print_error("加载失败:" + szPathName);
        return false;
    }

    IEnumerator LoadByteCode()
    {
        if (LoadByteCodeByFile())
            yield break;
    }

    protected virtual void OnAfterLoadScriptData()
    {

    }
}
