using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 功能：加载脚本

/// </summary>

public class FCScriptLoader : MonoBehaviour
{
    public delegate void LPInitCallback();

    protected static string s_szFromFC = string.Empty;
    protected static List<string> m_ScriptLog = new List<string>();
    protected static List<string> m_ThreadScriptLog = new List<string>();
    protected static int m_nAddLogCount = 0;
    protected static bool m_bLoadScript = false;
    protected static LPInitCallback m_InitCallback = null;

    protected void Start()
    {
        InitDll();
    }

    public static void InitCall(LPInitCallback pFunc)
    {
        if (m_bLoadScript)
            pFunc();
        else
            m_InitCallback += pFunc;
    }

    protected virtual bool IsRecrodLog()
    {
        return false;
    }

    // 功能：得到工程加密的KEY
    protected virtual int  GetProjCode()
    {
        return 0;
    }

    protected void InitDll(bool bLoadByteCode = true)
    {
        if (!FCDll.IsInitDll())
        {
            try
            {
                if(IsRecrodLog())
                {
                    FCLibHelper.fc_set_debug_print_func(print_error);
                    FCLibHelper.fc_set_output_error_func(print_error);

                    FCDll.InitDll();

                    FCLibHelper.fc_set_debug_print_func(print_error);
                    FCLibHelper.fc_set_output_error_func(print_error);
                }
                else
                {
                    FCDll.InitDll();
                }                
                
                // 启动调试器, 启动后，就可以用工具附加调试了，默认端口是2600
                FCLibHelper.fc_switch_debug(true);
                // FCLibHelper.fc_start_debug(3000); // 指定3000这个端口并启动调试器
            }
            catch (Exception e)
            {
                print_error(e.ToString());
            }
            if(bLoadByteCode)
                LoadByteCode(OnLoadScriptCallback);
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    public static void print_error(string szInfo)
    {
        lock(m_ThreadScriptLog)
        {
            if (m_ThreadScriptLog.Count > 40)
                m_ThreadScriptLog.RemoveRange(0, m_ThreadScriptLog.Count - 40);
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

    protected bool LoadByteCodeByFile(OnLoadScriptByteCode pCallBack)
    {
        string szPathName = "test.code";
        print_error("开始加载, 路径：" + szPathName);
        try
        {
            BetterStreamingAssets.Initialize();
            byte[] fileData = BetterStreamingAssets.ReadAllBytes(szPathName);
            if(fileData != null && fileData.Length > 0)
            {
                print_error("加载成功, Path:" + szPathName + ", 文件大小：" + fileData.Length);
            }
            pCallBack(fileData);
            return true;
        }
        catch (Exception e)
        {
            print_error(e.ToString());
        }
        print_error("加载失败:" + szPathName);
        return false;
    }

    protected virtual void OnLoadScriptCallback(byte[] fileData)
    {
        if (fileData != null && fileData.Length > 0)
        {
            m_bLoadScript = true;
            FCLibHelper.fc_set_code_data(fileData, fileData.Length, GetProjCode());

            all_class_wrap.Register(); // 动态wrap
            OnAfterLoadScriptData();
            if (m_InitCallback != null)
            {
                m_InitCallback();
                m_InitCallback = null;
            }
        }
    }

    // 功能：重载这个接口, 可以自己实现字节码文件的加载
    public delegate void OnLoadScriptByteCode(byte []fileData);
    protected virtual void LoadByteCode(OnLoadScriptByteCode pCallBack)
    {
        LoadByteCodeByFile(pCallBack);
    }
    
    protected virtual void OnAfterLoadScriptData()
    {

    }
}
