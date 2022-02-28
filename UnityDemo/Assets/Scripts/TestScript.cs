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

class TestScript : FCScriptLoader
{
    protected override void OnAfterLoadScriptData()
    {
        FCLibHelper.fc_register_func(m_VMPtr, "fc2csharp_set_vector3", fc2csharp_set_vector3);
        FCLibHelper.fc_register_func(m_VMPtr, "fc2csharp_set_vector4", fc2csharp_set_vector4);
        FCLibHelper.fc_register_func(m_VMPtr, "fc2csharp_set_string", fc2csharp_set_string);
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
        int  nID = System.Threading.Thread.CurrentThread.ManagedThreadId;
        print_error("current thread id = " +nID);
        return 1;
    }

    public static TestScript get_obj(long L)
    {
        return FCGetObj.GetObj<TestScript>(L);
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
            FCCallHelper.fc_param_call(m_VMPtr, 0, "csharp2fc_set_vector3", v);

            Vector4 v2 = new Vector4(22, 33, 44, 55);
            FCCallHelper.fc_param_call(m_VMPtr, 0, "csharp2fc_set_vector4", v2);

            string szTest = "测试字符串传参";
            FCCallHelper.fc_param_call(m_VMPtr, 0, "csharp2fc_set_string", szTest);
        }
        catch (Exception e)
        {
            print_error(e.ToString());
        }
    }
    void LoadScript()
    {
        InitDll();
        FCCallHelper.fc_void_call(m_VMPtr, 0, "main");
    }
    long m_nTestPtr;
    void TestObjectScript()
    {
        InitDll();
        if(m_nTestPtr == 0)
            m_nTestPtr = FCLibHelper.fc_instance(m_VMPtr, "CTestD");
        string szLog = string.Format("脚本实例IntPtr = {0}", m_nTestPtr);
        print_error(szLog);
        FCCallHelper.fc_void_call(m_VMPtr, m_nTestPtr, "Start");
    }
    void TestFunc0()
    {
        GameObject obj = GameObject.Find("Empty");
        if (obj == null)
            obj = new GameObject("Empty");
        FCCallHelper.fc_param_call(m_VMPtr, 0, "Test0", obj.transform);
    }
    void TestFunc1()
    {
        GameObject obj = GameObject.Find("Empty");
        if (obj == null)
            obj = new GameObject("Empty");
        FCCallHelper.fc_param_call(m_VMPtr, 0, "Test1", obj.transform);
    }
    void TestFunc2()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test2");
    }
    void TestFunc3()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test3");
    }
    void TestFunc4()
    {
        GameObject obj = GameObject.Find("Empty");
        if (obj == null)
            obj = new GameObject("Empty");
        FCCallHelper.fc_param_call(m_VMPtr, 0, "Test4", obj.transform);
    }
    void TestFunc5()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test5");
    }
    void TestFunc6()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test6");
    }
    void TestFunc7()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test7");
    }
    void TestFunc8()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test8");
    }
    void TestFunc9()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test9");
    }
    void TestFunc10()
    {
        GameObject obj = GameObject.Find("Empty");
        if (obj == null)
            obj = new GameObject("Empty");
        FCCallHelper.fc_param_call(m_VMPtr, 0, "Test10", obj.transform);
    }
    void TestFunc11()
    {
        // 测试空函数调用
        long  nBeginTime = DateTime.Now.Ticks / 10000;
        for (int i = 0; i< 200000; ++i)
        {
            FCCallHelper.fc_void_call(m_VMPtr, 0, "EmptyFunc");
        }
        long  nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        int nTotalCount = 200000;
        string szTips = string.Format("Test11 花费总时间={0}秒,平均时间={1}毫秒,总调用次数={2}", nCostTime / 1000, nCostTime / nTotalCount, nTotalCount);        
        print_error(szTips);
        FCCallHelper.fc_void_call(m_VMPtr, 0, "PrintV0");
    }
    void TestFunc12()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test12");
    }
    // 功能：测试C#await功能
    void TestAwait()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "TestAwait");
    }
    
    void InvalidObjectScriptCall()
    {
        InitDll();
        FCCallHelper.fc_void_call(m_VMPtr, m_nTestPtr, "Start");
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
            FCLibHelper.fc_relese_ins(m_VMPtr, m_nTestPtr);
            m_nTestPtr = 0;
        }
    }

    void OnGUI()
    {
        int nLeft = 200;
        int nTop = 200;
        nLeft = 200;
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
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "删除对象不置空"))
        {
            FCLibHelper.fc_relese_ins(m_VMPtr, m_nTestPtr);
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "删除对象并置空"))
        {
            FCLibHelper.fc_relese_ins(m_VMPtr, m_nTestPtr);
            m_nTestPtr = 0;
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "调用对象函数"))
        {
            InvalidObjectScriptCall();
        }
        nLeft = 200;
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
            if (!FCLibHelper.fc_is_init())
                m_VMPtr = FCLibHelper.fc_init(true);
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "释放DLL"))
        {
            if (FCLibHelper.fc_is_init())
            {
                FCLibHelper.fc_release(m_VMPtr);
                m_VMPtr = 0;
            }
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试await"))
        {
            TestAwait();
        }
        nLeft = 200;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test0"))
        {
            TestFunc0();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test1"))
        {
            TestFunc1();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test2"))
        {
            TestFunc2();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test3"))
        {
            TestFunc3();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test4"))
        {
            TestFunc4();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test5"))
        {
            TestFunc5();
        }
        nLeft = 200;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test6"))
        {
            TestFunc6();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test7"))
        {
            TestFunc7();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test8"))
        {
            TestFunc8();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test9"))
        {
            TestFunc9();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test10"))
        {
            TestFunc10();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test11"))
        {
            TestFunc11();
        }
        nLeft = 200;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test12"))
        {
            TestFunc12();
        }
        float fy = 10.0f;
        float fWidth = Screen.width - fy - 10;
        List<string> aLog = ScriptLog;
        for (int i = 0; i<aLog.Count; ++i)
        {
            GUI.Label(new Rect(10.0f, fy, fWidth, 20.0f), aLog[i]);
            fy += 25;
        }
    }
}