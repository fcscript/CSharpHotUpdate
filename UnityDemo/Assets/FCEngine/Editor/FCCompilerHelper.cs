using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Reflection;

class FCCompilerHelper
{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool fc_compiler_isneedstop(Int64 nUserData);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void fc_compiler_progress(Int64 nUserData, float fPos);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void fc_compiler_result(Int64 nUserData, int nErrorNumb);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void fc_compiler_print(Int64 nUserData, string szError);
#else
    public delegate bool fc_compiler_isneedstop(Int64 nUserData);
    public delegate void fc_compiler_progress(Int64 nUserData, float fPos);
    public delegate void fc_compiler_result(Int64 nUserData, int nErrorNumb);
    public delegate void fc_compiler_print(Int64 nUserData, string szError);        
#endif


#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
    const string FCCompilerDLL = "compiler_dll";
#else
    const string FCCompilerDLL = "__Internal";
#endif


#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
    [DllImport(FCCompilerDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_compiler_proj(string szProjPathName, Int64 nUserData, fc_compiler_isneedstop isNeedStop, fc_compiler_progress GetProgtess, fc_compiler_result GetResult, fc_compiler_print Print, bool bSaveInportXml);
    
    // 功能：停止编译
    [DllImport(FCCompilerDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_stop_compile();
#else    
    public static bool fc_compiler_proj(string szProjPathName, Int64 nUserData, fc_compiler_isneedstop isNeedStop, fc_compiler_progress GetProgtess, fc_compiler_result GetResult, fc_compiler_print Print, bool bSaveInportXml)
    {
    // 其他平台暂不支持的噢
       return true;
    }
    public static void fc_stop_compile()
    {
    // 其他平台暂不支持的噢
    }
#endif

    static Int64 s_nUserData = 0;
    protected static List<string> m_ThreadScriptLog = new List<string>();
    protected static int m_nAddLogCount = 0;
    
    static void SafeOutputLog(string szTips)
    {
        lock (m_ThreadScriptLog)
        {
            if (m_ThreadScriptLog.Count > 40)
            {
                m_ThreadScriptLog.RemoveRange(0, m_ThreadScriptLog.Count - 40);
                m_nAddLogCount = m_ThreadScriptLog.Count;
            }
            m_ThreadScriptLog.Add(szTips);
            m_nAddLogCount++;
        }
    }
    protected static List<string> ScriptLog
    {
        get
        {
            List<string> Logs = new List<string>();
            if (m_nAddLogCount > 0)
            {
                Logs.Capacity = m_nAddLogCount;
                lock (m_ThreadScriptLog)
                {
                    Logs.AddRange(m_ThreadScriptLog);
                    m_ThreadScriptLog.Clear();
                    m_nAddLogCount = 0;
                }
            }
            return Logs;
        }
    }
    protected static void PrintCompilerLog()
    {
        List<string> aLog = ScriptLog;
        for (int i = 0; i < aLog.Count; ++i)
        {
            Debug.LogError(aLog[i]);
        }
        aLog.Clear();
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    static void  GetResult(Int64 nUserData, int nErrorNumb)
    {
        if (nErrorNumb <= 0)
            SafeOutputLog("编译成功, 零错误");
        else
            SafeOutputLog("编译失败," + nErrorNumb + "错误！");
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    static void  CompilerPrint(Int64 nUserData, string szError)
    {
        SafeOutputLog(szError);
    }

    public static void  CompilerProj(string szProjPathName, bool bSaveInporXml)
    {
        fc_stop_compile(); // 先总是停止编译先
        ++s_nUserData;
        // 如果这个编译时间长了些的话，可以创建一个线程执行
        fc_compiler_proj(szProjPathName, s_nUserData, null, null, GetResult, CompilerPrint, bSaveInporXml);
        PrintCompilerLog();
    }
    public static void  StopCompiler()
    {
        fc_stop_compile();
        PrintCompilerLog();
    }
}
