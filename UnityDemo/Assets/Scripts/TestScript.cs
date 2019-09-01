using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class TestExport
{    
    public enum ValueType
    {
        value_none,
        value_int = 2,
        value_float = 3,
    }
    int m_nType = 0;
    public static TestCallback onPostRender; // 这个也于属性一样，有get, set
    public TestCallback onOwnPostRender; // 这个也于属性一样，有get, set
    public TestCallback2 onCallFunc2;

    //public void SetCallFunc(string szName, Action<int, float, string> func)
    //{

    //}
    public Material[] materials { get; set; }

    [Obsolete("test dissable func", true)]
    public int  value
    {
        get { return m_nType; }
        set { m_nType = value; }
    }

    void  Test()
    {
        TestExport ret = this;
        long v = FCGetObj.PushObj(TestExport.onPostRender);
        FCDll.PushCallParam(v);
        int[] buffer = new int[10];
        unsafe
        {
            fixed (void* p = buffer)
            {
            }
        }
        //this.GetRefList(ref arg0);
    }

    public delegate void TestCallback(int nType);
    public delegate void TestCallback2(int nType, string value, Vector2 v3);
}

[AutoWrap]
public class TestD
{
    [DontWrap]
    public int m_nValue;
    public void  SetValue(int nValue)
    {
    }
    [DontWrap]
    public void Update()
    {

    }
}

[PartWrap]
public class TestPart
{
    public int m_nValue;
    [PartWrap]
    public void SetValue(int nValue)
    {
        m_nValue = nValue;
    }
    public void SetFunc(Action func)
    {
    }
}

class onPostRender_deletate : FCDelegateBase
{
    public void   CallFunc(int nType)
    {
        try
        {
            FCDll.PushCallParam(nType);
            FCLibHelper.fc_call(m_nThisPtr, m_szFuncName);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
};

class TestExport_Handle_Wrap
{
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    int set_onPostRender_Wrap(long L)
    {
        try
        {
            onPostRender_deletate func = FCDelegateMng.Instance.GetDelegate<onPostRender_deletate>(L);
            TestExport.onPostRender = func.CallFunc;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    int set_onOwnPostRender_Wrap(long L)
    {
        try
        {
            onPostRender_deletate func = FCDelegateMng.Instance.GetDelegate<onPostRender_deletate>(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            TestExport ret = get_obj(nThisPtr);
            ret.onOwnPostRender = func.CallFunc;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    static TestExport get_obj(long nThisPtr)
    {
        return FCGetObj.GetObj<TestExport>(nThisPtr);
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetChild_T_V_string(long L)
    {
        // 这个是
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            TestExport obj = get_obj(nThisPtr);
            int nTemplateParamCount = FCLibHelper.fc_get_template_param_count(L);
            string T = FCLibHelper.fc_get_string_a(L, 0);
            string V = FCLibHelper.fc_get_string_a(L, 1);

            // 函数参数
            string arg0 = FCLibHelper.fc_get_string_a(L, 2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

};

class TestScript : FCScriptLoader
{
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
    public static string GetAssetStreamingPathName(string szFileName)
    {
        string szPathName = string.Format("{0}/{1}", Application.streamingAssetsPath, szFileName);
        return szPathName;
    }

    protected override void OnAfterLoadScriptData()
    {
        FCLibHelper.fc_register_func("fc2csharp_set_vector3", fc2csharp_set_vector3);
        FCLibHelper.fc_register_func("fc2csharp_set_vector4", fc2csharp_set_vector4);
        FCLibHelper.fc_register_func("fc2csharp_set_string", fc2csharp_set_string);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int fc2csharp_set_vector3(long L)
    {
        Vector3 v = Vector3.zero;
        FCLibHelper.fc_get_vector3(L, 0, ref v);
        print_error(string.Format("收到脚本回调：fc2csharp_set_vector3 ==>({0},{1},{2})", v.x, v.y, v.z));
        return 1;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int fc2csharp_set_vector4(long L)
    {
        Vector4 v = Vector4.zero;
        FCLibHelper.fc_get_vector4(L, 0, ref v);
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
            int nLisenPort = FCLibHelper.fc_get_debug_port();
            print_error("开启监控的端口: " + nLisenPort);
        }
        catch(Exception e)
        {
            print_error(e.ToString());
        }

        try
        {
            Vector3 v = new Vector3(1, 2, 3);
            FCDll.PushCallParam(ref v);
            FCLibHelper.fc_call(0, "csharp2fc_set_vector3");

            Vector4 v2 = new Vector4(22, 33, 44, 55);
            FCDll.PushCallParam(ref v2);
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
    void TestGraphicCall()
    {
        // 说明：这里只是为了检验C#中的结构体，传C++后的内存数据结构

        print_error("r, g, b, a ==>(60, 120, 180, 220)");
        Color32 c = new Color32(60, 120, 180, 220);
        FCLibHelper.fc_test_color32(c);
        Color c2 = new Color(1f, 2f, 3f, 4f);
        FCLibHelper.fc_test_color(ref c2);
        Vector3 vNormal = new Vector3(3, 2, 1);
        Plane p2 = new Plane(vNormal, 4.0f);
        FCLibHelper.fc_test_plane(ref p2);
        Ray r = new Ray(new Vector3(1.0f, 2.0f, 3.0f), new Vector3(0.0f, 1.0f, 0.5f));
        FCLibHelper.fc_test_ray(ref r);
        Bounds box = new Bounds();
        box.min = new Vector3(-1f, -2f, -3f);
        box.max = new Vector3(4f, 5f, 6f);
        FCLibHelper.fc_test_box(ref box);
        Matrix4x4 mat = new Matrix4x4();
        mat.m00 = 0f;
        mat.m01 = 1f;
        mat.m02 = 2f;
        mat.m03 = 3f;

        mat.m10 = 10f;
        mat.m11 = 11f;
        mat.m12 = 12f;
        mat.m13 = 13f;

        mat.m20 = 20f;
        mat.m21 = 21f;
        mat.m22 = 22f;
        mat.m23 = 23f;

        mat.m30 = 30f;
        mat.m31 = 31f;
        mat.m32 = 32f;
        mat.m33 = 33f;
        FCLibHelper.fc_test_matrix(ref mat);

        Quaternion qa = new Quaternion(1, 2, 3, 4);
        FCLibHelper.fc_test_quaternion(ref qa);
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
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试图形对象"))
        {
            TestGraphicCall();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "初始化DLL"))
        {
            if(!FCLibHelper.fc_is_init())
                FCLibHelper.fc_init();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "释放DLL"))
        {
            if(FCLibHelper.fc_is_init())
                FCLibHelper.fc_release();
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