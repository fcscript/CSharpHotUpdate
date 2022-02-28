using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Reflection;


public class FCCallHelper
{	
    // 好坑啊，C#居然不支持模板推导(这个函数有问题）
    //public static void fc_param_call<_Ty1>(long VM, long ScriptIns, string FuncName, _Ty1 Param1)
    //{
    //    long CallKey = FCLibHelper.QueryCallKey();
    //    long L_Param = FCLibHelper.fc_prepare_ue_call(VM, ScriptIns, FuncName, CallKey);
    //    long Arg0 = FCLibHelper.fc_get_param_ptr(L_Param, 0);
    //    FCDll.WriteValueToScript(VM, Arg0, Param1);
    //    long L_Ret = FCLibHelper.fc_ue_call(VM, CallKey);
    //    FCLibHelper.fc_end_ue_call(VM, CallKey);
    //}
    public static void fc_void_call(long VM, long ScriptIns, string FuncName)
    {
        long CallKey = FCLibHelper.QueryCallKey();
        long ParamPtr = FCLibHelper.fc_prepare_ue_call(VM, ScriptIns, FuncName, CallKey);
        FCLibHelper.fc_ue_call(VM, CallKey);
        FCLibHelper.fc_end_ue_call(VM, CallKey);
    }
    public static void fc_param_call(long VM, long ScriptIns, string FuncName, string Param)
    {
        long CallKey = FCLibHelper.QueryCallKey();
        long L_Param = FCLibHelper.fc_prepare_ue_call(VM, ScriptIns, FuncName, CallKey);
        long Arg0 = FCLibHelper.fc_get_param_ptr(L_Param, 0);
        FCDll.WriteValueToScript(VM, Arg0, Param);
        long L_Ret = FCLibHelper.fc_ue_call(VM, CallKey);
        FCLibHelper.fc_end_ue_call(VM, CallKey);
    }
    public static void fc_param_call(long VM, long ScriptIns, string FuncName, UnityEngine.Object Param)
    {
        long CallKey = FCLibHelper.QueryCallKey();
        long L_Param = FCLibHelper.fc_prepare_ue_call(VM, ScriptIns, FuncName, CallKey);
        long Arg0 = FCLibHelper.fc_get_param_ptr(L_Param, 0);
        FCDll.WriteValueToScript(VM, Arg0, Param);
        long L_Ret = FCLibHelper.fc_ue_call(VM, CallKey);
        FCLibHelper.fc_end_ue_call(VM, CallKey);
    }
    public static void fc_param_call(long VM, long ScriptIns, string FuncName, Vector3 Param)
    {
        long CallKey = FCLibHelper.QueryCallKey();
        long L_Param = FCLibHelper.fc_prepare_ue_call(VM, ScriptIns, FuncName, CallKey);
        long Arg0 = FCLibHelper.fc_get_param_ptr(L_Param, 0);
        FCDll.WriteValueToScript(VM, Arg0, Param);
        long L_Ret = FCLibHelper.fc_ue_call(VM, CallKey);
        FCLibHelper.fc_end_ue_call(VM, CallKey);
    }
    public static void fc_param_call(long VM, long ScriptIns, string FuncName, Vector4 Param)
    {
        long CallKey = FCLibHelper.QueryCallKey();
        long L_Param = FCLibHelper.fc_prepare_ue_call(VM, ScriptIns, FuncName, CallKey);
        long Arg0 = FCLibHelper.fc_get_param_ptr(L_Param, 0);
        FCDll.WriteValueToScript(VM, Arg0, Param);
        long L_Ret = FCLibHelper.fc_ue_call(VM, CallKey);
        FCLibHelper.fc_end_ue_call(VM, CallKey);
    }
};