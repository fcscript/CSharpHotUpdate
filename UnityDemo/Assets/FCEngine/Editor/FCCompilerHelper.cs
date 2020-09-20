using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Reflection;

class FCCompilerHelper
{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool fc_compiler_isneedstop(Int64 nUserData);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void fc_compiler_progress(Int64 nUserData, float fPos);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void fc_compiler_result(Int64 nUserData, bool bSuc);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void fc_compiler_print(Int64 nUserData, string szError);
#else
    public delegate bool fc_compiler_isneedstop(Int64 nUserData);
    public delegate void fc_compiler_progress(Int64 nUserData, float fPos);
    public delegate void fc_compiler_result(Int64 nUserData, bool bSuc);
    public delegate void fc_compiler_print(Int64 nUserData, string szError);        
#endif


#if !UNITY_EDITOR && UNITY_IPHONE
        const string FCCompilerDLL = "__Internal";
#else
    const string FCCompilerDLL = "compiler_dll";
#endif


#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
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
    static int s_nCompilerErrorCount = 0;
    static void  GetResult(Int64 nUserData, bool bSuc)
    {
        if (bSuc)
            Debug.Log("编译成功, 零错误");
        else
            Debug.LogError("编译失败," + s_nCompilerErrorCount + "错误！");
    }
    static void  CompilerPrint(Int64 nUserData, string szError)
    {
        ++s_nCompilerErrorCount;
        Debug.LogError(szError);
    }

    public static void  CompilerProj(string szProjPathName, bool bSaveInporXml)
    {
        fc_stop_compile(); // 先总是停止编译先
        ++s_nUserData;
        s_nCompilerErrorCount = 0;
        // 如果这个编译时间长了些的话，可以创建一个线程执行
        fc_compiler_proj(szProjPathName, s_nUserData, null, null, GetResult, CompilerPrint, bSaveInporXml);
    }
    public static void  StopCompiler()
    {
        fc_stop_compile();
    }
}
