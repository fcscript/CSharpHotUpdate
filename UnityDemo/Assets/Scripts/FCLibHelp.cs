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

struct GLPlane
{
    public Vector3 vNormal;
    public float fDist;
};

public struct IntRect
{
    public int left, top, right, bottom;
}

public struct Sphere
{
    public Vector3 center;
    public float radius;
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
    public static string fc_get_value_string_a(long ptr)
    {
        int nLen = FCLibHelper.fc_get_value_string_len(ptr);
        byte[] buff = new byte[nLen];
        FCLibHelper.fc_get_value_string(ptr, buff, nLen);
        string szParam = System.Text.Encoding.UTF8.GetString(buff, 0, nLen);
        return szParam;
    }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LPCustomPrintCallback(string pcsInfo);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int fc_call_back(long L);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int fc_call_back_inport_class_func(long L);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool fc_call_back_inport_class_equal(long L, long R);
#else
    public delegate void LPCustomPrintCallback(string pcsInfo);
    public delegate int fc_call_back(long L);
    public delegate int fc_call_back_inport_class_func(long L);
    public delegate bool fc_call_back_inport_class_equal(long L, long R);
#endif

#if !UNITY_EDITOR && UNITY_IPHONE
        const string FCDLL = "__Internal";
#else
    const string FCDLL = "fclib_dll";
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
    public static extern void fc_start_debug(int nPort);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_is_debug_mode();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_debug_port();
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
    public static extern void fc_test_int_ptr();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_test_color32(Color32 c);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_test_color(ref Color c);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_test_plane(ref Plane p);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_test_matrix(ref Matrix4x4 mat);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_test_box(ref Bounds box);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_test_ray(ref Ray ray);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_func(string pcsFuncName, fc_call_back func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_inport_class_id(string pcsClassName);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_func(int nClassNameID, string pcsFuncName, fc_call_back_inport_class_func func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_attrib(int nClassNameID, string pcsFuncName, fc_call_back_inport_class_func pGet, fc_call_back_inport_class_func pSet);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_cast(int nClassNameID, fc_call_back_inport_class_func func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_new(int nClassNameID, fc_call_back_inport_class_func func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_del(int nClassNameID, fc_call_back_inport_class_func func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_release_ref(int nClassNameID, fc_call_back_inport_class_func func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_hash(int nClassNameID, fc_call_back_inport_class_func func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_equal(int nClassNameID, fc_call_back_inport_class_equal func);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_param_count(long L);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_param_ptr(long L, int i);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_inport_obj_ptr(long L);
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
    public static extern void fc_get_vector2(long L, int i, ref Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_vector3(long L, int i, ref Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_vector4(long L, int i, ref Vector4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_plane(long L, int i, ref Plane v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_martix(long L, int i, ref Matrix4x4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_bounds(long L, int i, ref Bounds v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_ray(long L, int i, ref Ray v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_color32(long L, int i, ref Color32 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_color(long L, int i, ref Color v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_intrect(long L, int i, ref IntRect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_rect(long L, int i, ref Rect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_sphere(long L, int i, ref Sphere v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_return_ptr(long L);
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
    public static extern void fc_push_return_vector2(long L, ref Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_vector3(long L, ref Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_return_vector4(long L, ref Vector4 v);
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
    public static extern void fc_push_plane(ref Plane v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_matrix(ref Matrix4x4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_bounds(ref Bounds v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_ray(ref Ray v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_color32(ref Color32 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_color(ref Color v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_intrect(ref IntRect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_rect(ref Rect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_sphere(ref Sphere v);
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
    public static extern void fc_get_return_string_a(byte[] pOutBuff, int nOutBuffSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_return_string_len();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_return_vector2(ref Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_return_vector3(ref Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_return_vector4(ref Vector4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_class_value(long ptr, string value_name);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_bool(long ptr, bool v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_char(long ptr, char v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_byte(long ptr, byte v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_short(long ptr, short v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_ushort(long ptr, ushort v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_int(long ptr, int v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_uint(long ptr, uint v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_float(long ptr, float v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_double(long ptr, double v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_int64(long ptr, long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_uint64(long ptr, ulong v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_intptr(long ptr, long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_string(long ptr, string v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_vector2(long ptr, ref Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_vector3(long ptr, ref Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_vector4(long ptr, ref Vector4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_plane(long ptr, ref Plane v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_matrix(long ptr, ref Matrix4x4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_bounds(long ptr, ref Bounds v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_ray(long ptr, ref Ray v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_color32(long ptr, ref Color32 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_color(long ptr, ref Color v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_intrect(long ptr, ref IntRect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_rect(long ptr, ref Rect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_sphere(long ptr, ref Sphere v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_get_value_bool(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern char fc_get_value_char(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern byte fc_get_value_byte(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern short fc_get_value_short(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort fc_get_value_ushort(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_value_int(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint fc_get_value_uint(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern float fc_get_value_float(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern double fc_get_value_double(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_value_int64(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong fc_get_value_uint64(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_value_intptr(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_string(long ptr, byte[] pOutBuff, int nOutBuffSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_value_string_len(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_vector2(long ptr, ref Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_vector3(long ptr, ref Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_vector4(long ptr, ref Vector4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_plane(long ptr, ref Plane v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_matrix(long ptr, ref Matrix4x4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_bounds(long ptr, ref Bounds v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_ray(long ptr, ref Ray v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_color32(long ptr, ref Color32 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_color(long ptr, ref Color v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_intrect(long ptr, ref IntRect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_rect(long ptr, ref Rect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_sphere(long ptr, ref Sphere v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_array_size(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_size(long ptr, int nSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_array_node_temp_ptr(long ptr, int nIndex);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_bool(long ptr, bool[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_byte(long ptr, byte[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_short(long ptr, short[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_ushort(long ptr, ushort[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_int(long ptr, int[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_uint(long ptr, uint[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_float(long ptr, float[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_double(long ptr, double[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_color32(long ptr, Color32[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_color(long ptr, Color[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_vector2(long ptr, Vector2[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_vector3(long ptr, Vector3[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_vector4(long ptr, Vector4[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_rect(long ptr, Rect[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_bool(long ptr, bool[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_byte(long ptr, byte[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_short(long ptr, short[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_ushort(long ptr, ushort[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_int(long ptr, int[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_uint(long ptr, uint[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_float(long ptr, float[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_double(long ptr, double[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_color32(long ptr, Color32[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_color(long ptr, Color[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_vector2(long ptr, Vector2[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_vector3(long ptr, Vector3[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_vector4(long ptr, Vector4[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_array_rect(long ptr, Rect[] pArray, int nStart, int nCount);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_map_size(long pMapPtr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_map_prepare_view(long pMapPtr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_map_to_next_pair();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_map_get_cur_key_ptr();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_map_get_cur_value_ptr();
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_map_clear(long pMapPtr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_map_push_key_ptr(long pMapPtr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_map_push_value_ptr(long pMapPtr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_map_push_key_value(long pMapPtr);
}