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

// 性能测试的范例

class TestProfiler: FCScriptLoader
{
    // 记录脚本的LOG
    protected override bool IsRecrodLog()
    {
        return true;
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
    void TestFunc91()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test91");
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
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test11");
    }
    void EmptyCall()
    {
        // 测试空函数调用
        long nBeginTime = DateTime.Now.Ticks / 10000;
        for (int i = 0; i < 200000; ++i)
        {
            FCCallHelper.fc_void_call(m_VMPtr, 0, "EmptyFunc");
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        int nTotalCount = 200000;
        string szTips = string.Format("Test11 花费总时间={0}秒,平均时间={1}毫秒,总调用次数={2}", nCostTime / 1000, nCostTime / nTotalCount, nTotalCount);
        print_error(szTips);
        FCCallHelper.fc_void_call(m_VMPtr, 0, "PrintV0");
    }
    void Mandelbrot()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "TestMandelbrot");
    }
    void TestFunc12()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test12");
    }
    void TestFunc13()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test13");
    }
    void TestFunc14()
    {
        FCCallHelper.fc_void_call(m_VMPtr, 0, "Test14");
    }
    void OnGUI()
    {
        int nLeft = 200;
        int nTop = 200;
        nLeft = 200;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "清空LOG"))
        {
            m_ScriptLog.Clear();
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
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test13"))
        {
            TestFunc13();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test14"))
        {
            TestFunc14();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试Test91"))
        {
            TestFunc91();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "空调用"))
        {
            EmptyCall();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "Mandelbrot"))
        {
            Mandelbrot();
        }
        // Mandelbrot
        float fy = 10.0f;
        float fWidth = Screen.width - fy - 10;
        List<string> aLog = ScriptLog;
        for (int i = 0; i < aLog.Count; ++i)
        {
            GUI.Label(new Rect(10.0f, fy, fWidth, 20.0f), aLog[i]);
            fy += 25;
        }
    }
}