using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

class TestScript : MonoBehaviour
{
    static string s_szFromFC = string.Empty;
    static List<string> m_ScriptLog = new List<string>();

    void  Start()
    {
        InitDll();
    }   
        
    void  InitDll()
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
                
                // 注册两个测试函数
                FCLibHelper.fc_register_func("fc2csharp_set_vector3", fc2csharp_set_vector3);
                FCLibHelper.fc_register_func("fc2csharp_set_vector4", fc2csharp_set_vector4);
                FCLibHelper.fc_register_func("fc2csharp_set_string", fc2csharp_set_string);
            }
            catch(Exception e)
            {
                print_error(e.ToString());
            }            
            StartCoroutine(LoadByteCode());
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    static void  print_error(string szInfo)
    {
        m_ScriptLog.Add(szInfo);
    }

    public static string GetAssetStreamingUrlForWWW(string szFileName)
    {
        string url = null;

#if UNITY_STANDALONE || UNITY_EDITOR
        url = string.Format("file:///{0}/{1}", Application.streamingAssetsPath, szFileName);
#elif UNITY_IPHONE
		url = string.Format( "file://{0}/{1}", Application.streamingAssetsPath, szFileName );
#elif UNITY_ANDROID
		url = string.Format( "{0}/{1}", Application.streamingAssetsPath, szFileName );
#endif
        return url;
    }

    IEnumerator   LoadByteCode()
    {
        string url = GetAssetStreamingUrlForWWW("test.code");
        WWW _www = new WWW(url);
        yield return _www;
        if(string.IsNullOrEmpty(_www.error))
        {
            byte []fileData = _www.bytes;
            try
            {
                FCLibHelper.fc_set_code_data(fileData, fileData.Length, 0);
            }
            catch(Exception e)
            {
                print_error(e.ToString());
            }
            print_error("test.code 加载成功：" + url);
        }
        else
        {
            print_error(_www.error);
            print_error("test.code 加载失败:" + url);
        }
        _www.Dispose();
        _www = null;
        yield break;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int fc2csharp_set_vector3(long L)
    {
        Vector3 v = Vector3.zero;
        v = FCLibHelper.fc_get_vector3(L, 0);
        print_error(string.Format("收到脚本回调：fc2csharp_set_vector3 ==>({0},{1},{2})", v.x, v.y, v.z));
        return 1;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int fc2csharp_set_vector4(long L)
    {
        Vector4 v = Vector4.zero;
        v = FCLibHelper.fc_get_vector4(L, 0);
        print_error(string.Format("收到脚本回调：fc2csharp_set_vector4 ==>({0},{1},{2},{3})", v.x, v.y, v.z, v.w));
        return 1;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int fc2csharp_set_string(long L)
    {
        string v = FCLibHelper.fc_get_string_a(L, 0);
        print_error(string.Format("收到脚本回调：fc2csharp_set_string ==>{0}", v));
        s_szFromFC = v;
        return 1;
    }

    void  TestScriptFunc1()
    {
        try
        {
            int nV2 = FCLibHelper.fc_get_version();
            print_error("fc_get_version() is " + nV2);
            bool bDebugMode = FCLibHelper.fc_is_debug_mode();
            print_error("fc_is_debug_mode() is " + bDebugMode);
        }
        catch(Exception e)
        {
            print_error(e.ToString());
        }

        try
        {
            Vector3 v = new Vector3(1, 2, 3);
            FCDll.PushCallParam(v);
            FCLibHelper.fc_call(0, "csharp2fc_set_vector3");

            Vector4 v2 = new Vector4(22, 33, 44, 55);
            FCDll.PushCallParam(v2);
            FCLibHelper.fc_call(0, "csharp2fc_set_vector4");

            string szTest = "测试字符串传参";
            FCDll.PushCallParam(szTest);
            FCLibHelper.fc_call(0, "csharp2fc_set_string");
        }
        catch (Exception e)
        {
            print_error(e.ToString());
        }
    }
    void LoadScript()
    {
        InitDll();
        FCLibHelper.fc_call(0, "main");
    }
    long m_nTestPtr;
    void TestObjectScript()
    {
        InitDll();
        if(m_nTestPtr == 0)
            m_nTestPtr = FCLibHelper.fc_instance("CTestD");
        string szLog = string.Format("脚本实例IntPtr = {0}", m_nTestPtr);
        print_error(szLog);
        FCLibHelper.fc_call(m_nTestPtr, "Start");
    }
    void InvalidObjectScriptCall()
    {
        InitDll();
        FCLibHelper.fc_call(m_nTestPtr, "Start");
    }
    void  DeleteScriptObject()
    {
        if(m_nTestPtr != 0)
        {
            FCLibHelper.fc_relese_ins(m_nTestPtr);
            m_nTestPtr = 0;
        }
    }

    void OnGUI()
    {
        int nLeft = 300;
        int nTop = 200;
        nLeft = 300;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试脚本接口"))
        {
            TestScriptFunc1();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "执行main函数"))
        {
            LoadScript();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试脚本对象"))
        {
            TestObjectScript();
        }
        nLeft = 300;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "删除对象不置空"))
        {
            FCLibHelper.fc_relese_ins(m_nTestPtr);
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "删除对象并置空"))
        {
            FCLibHelper.fc_relese_ins(m_nTestPtr);
            m_nTestPtr = 0;
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "调用对象函数"))
        {
            InvalidObjectScriptCall();
        }
        nLeft = 300;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "清空LOG"))
        {
            m_ScriptLog.Clear();
        }
        float fy = 10.0f;
        float fWidth = Screen.width - fy - 10;
        for(int i = 0; i<m_ScriptLog.Count; ++i)
        {
            GUI.Label(new Rect(10.0f, fy, fWidth, 20.0f), m_ScriptLog[i]);
            fy += 25;
        }
    }
}