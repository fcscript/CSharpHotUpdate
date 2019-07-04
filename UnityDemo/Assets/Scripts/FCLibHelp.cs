using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Reflection;

[AttributeUsage(AttributeTargets.Method)]
public sealed class MonoPInvokeCallbackAttribute : Attribute
{
    public MonoPInvokeCallbackAttribute(Type typeBig)
    {
    }
}

class FCLibHelper
{
    // 扩展方法
    public static string fc_get_string_a(long L, int i)
    {
        int nLen = FCLibHelper.fc_get_string_len(L, i);
        byte[] buff = new byte[nLen];
        FCLibHelper.fc_get_string_a(L, i, buff, nLen);
        string szParam = System.Text.Encoding.UTF8.GetString(buff, 0, nLen);
        return szParam;
    }
    public static byte[] fc_get_byte_array(long L, int i)
    {
        int nLen = FCLibHelper.fc_get_byte_array_size(L, i);
        byte[] buff = new byte[nLen];
        fc_get_byte_array(L, i, buff, nLen);
        return buff;
    }
    // 扩展方法
    public static string fc_get_return_string_a()
    {
        int nLen = FCLibHelper.fc_get_return_string_len();
        byte[] buff = new byte[nLen];
        FCLibHelper.fc_get_return_string_a(buff, nLen);
        string szParam = System.Text.Encoding.UTF8.GetString(buff, 0, nLen);
        return szParam;
    }

#if !UNITY_EDITOR && UNITY_IPHONE
        const string FCDLL = "__Internal";
#else
    const string FCDLL = "fclib_dll";
#endif

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LPCustomPrintCallback(string pcsInfo);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int fc_call_back(long L);
#else
    public delegate void LPCustomPrintCallback(string pcsInfo);
    public delegate int fc_call_back(long L);
#endif


    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_init();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_release();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_is_init();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_code_data(byte[] pFileData, int nFileDataSize, int nProjCode);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_switch_debug(bool bDebug);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_is_debug_mode();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_version();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_instance(string pcsClassName);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_relese_ins(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_coroutine_udpate();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_output_error_func(LPCustomPrintCallback pFunc);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_debug_print_func(LPCustomPrintCallback pFunc);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_func(string pcsFuncName, fc_call_back func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_param_count(long L);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern char fc_get_char(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_get_bool(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern byte fc_get_byte(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern short fc_get_short(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort fc_get_ushort(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_int(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint fc_get_uint(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern float fc_get_float(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern double fc_get_double(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_int64(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong fc_get_uint64(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_intptr(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_string_a(long L, int i, byte[] pOutBuff, int nOutBuffSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_string_len(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_byte_array(long L, int i, byte[] pOutBuff, int nOutBuffSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_byte_array_size(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector2 fc_get_vector2(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector3 fc_get_vector3(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector4 fc_get_vector4(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_char(long L, int i, char v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_bool(long L, int i, bool v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_byte(long L, int i, byte v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_short(long L, int i, short v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_ushort(long L, int i, ushort v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_int(long L, int i, int v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_uint(long L, int i, uint v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_float(long L, int i, float v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_double(long L, int i, double v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_int64(long L, int i, long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_uint64(long L, int i, ulong v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_intptr(long L, int i, long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_string_a(long L, int i, string pcsStr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_byte_array(long L, int i, byte[] ptr, int nStart, int nLen);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_vector2(long L, int i, ref Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_vector3(long L, int i, ref Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_vector4(long L, int i, ref Vector4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_bool(long L, bool v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_char(long L, char v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_long_char(long L, int v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_byte(long L, byte v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_short(long L, short v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_ushort(long L, ushort v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_int(long L, int v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_uint(long L, uint v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_float(long L, float v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_double(long L, double v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_int64(long L, long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_uint64(long L, ulong v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_intptr(long L, long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_string_a(long L, string v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_byte_array(long L, byte[] ptr, int nStart, int nLen);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_vector2(long L, Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_vector3(long L, Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_vector4(long L, Vector4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_clear_param();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_char(char v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_bool(bool v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_byte(byte v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_short(short v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_ushort(ushort v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_wchar(ushort v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_int(int v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_uint(uint v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_float(float v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_double(double v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_int64(long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_uint64(ulong v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_intptr(long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_string_a(string v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_string_w(ushort[] v, int nLen);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_byte_array(byte[] v, int nStart, int nLen);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_vector2(ref Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_vector3(ref Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_vector4(ref Vector4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_call(long pInsPtr, string pcsFuncName);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_serialize_msg_call(long pInsPtr, string pcsFuncName, byte[] msgPtr, int nStart, int nLen, bool bReadMode);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_get_return_bool();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern char fc_get_return_char();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern byte fc_get_return_byte();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern short fc_get_return_short();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort fc_get_return_ushort();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_return_int();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint fc_get_return_uint();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern float fc_get_return_float();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern double fc_get_return_double();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_return_int64();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong fc_get_return_uint64();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_return_intptr();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_return_string_a(byte[] pOutBuff, int nOutBuffSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_return_string_len();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector2 fc_get_return_vector2();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector3 fc_get_return_vector3();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector4 fc_get_return_vector4();
}