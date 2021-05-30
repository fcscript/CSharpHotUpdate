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

public struct IntRect
{
    public int left, top, right, bottom;
}

public struct Sphere
{
    public Vector3 center;
    public float radius;
}

public class FCLibHelper
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
    public static string fc_get_return_string_a(long VM)
    {
        int nLen = FCLibHelper.fc_get_return_string_len(VM);
        byte[] buff = new byte[nLen];
        FCLibHelper.fc_get_return_string_a(VM, buff, nLen);
        string szParam = System.Text.Encoding.UTF8.GetString(buff, 0, nLen);
        return szParam;
    }
    public static string fc_get_value_string_a(long VM, long ptr)
    {
        int nLen = FCLibHelper.fc_get_value_string_len(VM, ptr);
        byte[] buff = new byte[nLen];
        FCLibHelper.fc_get_value_string(VM, ptr, buff, nLen);
        string szParam = System.Text.Encoding.UTF8.GetString(buff, 0, nLen);
        return szParam;
    }
    public static void fc_set_value_string_a(long ptr, string v)
    {
        FCLibHelper.fc_set_value_string(ptr, v);
    }
    public static void fc_get_array_void_ptr(long ptr, IntPtr[] pArray, int nStart, int nCount)
    {
        // 这个暂不支持噢
    }
    public static void fc_get_void_ptr(long L, int i, ref IntPtr ptr)
    {
        long nAddr = fc_get_void_ptr(L, i);
        ptr = new IntPtr(nAddr);
    }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
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

//#if !UNITY_EDITOR && UNITY_IPHONE
#if UNITY_IOS
    const string FCDLL = "__Internal";
#else
    const string FCDLL = "fclib_dll";
#endif


    //---------------------------------------------------------------------
    // 参数说明：VM 指虚拟机的指针
    //           L  指脚本调用C#端Wrap接口时，传递的参数列表(fc_call_param_array)
    //           ptr 一般指具体的参数(fc_c_param_ins)
    //---------------------------------------------------------------------
    // 功能：初始化脚本系统，创建一个线程主虚拟机
    // 参数：bMainThread - true表示复用主线程虚拟机, false表示使用独立虚拟机(可以用于其他线程)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_init(bool bMainThread);
    // 功能：释放脚本组件, 释放主线程虚拟机
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_release(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_is_init();
    // 功能：返回主线程虚拟机
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_main_vm();
    // 功能：设置脚本的字节码
    // 参数：pFileData - 字节码数据指针
    //       nFileDataSize - 字节码的长度
    //       nProjCode - 项目编号（这个起初设计是用于加解密，目前未使用, 可以是任意值）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_code_data(long VM, byte[] pFileData, int nFileDataSize, int nProjCode);
    // 功能：切换到调试模式, 默认端口是2600
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_switch_debug(bool bDebug);
    // 功能：启动调试器,  用户指定调试端口
    // 说明：调试端口从指定的开始，如果有占用，就向上加1，直到找到一个合适的。
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_start_debug(int nPort);
    // 功能：返回是不是调试模式
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_is_debug_mode();
    // 功能：返回本地监控的端口
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_debug_port();
    // 功能：得到脚本的版本号
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_version();
    // 功能：创建一个脚本对象, 并增加引用计数,由外部脚本来释放
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_instance(long VM, string pcsClassName);
    // 功能：释放一个脚本对象
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_relese_ins(long VM, long ptr);
    // 功能：检测脚本对象是不是合法的
    // 参数：ptr - 脚本对象ID(或C++指针)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_is_valid_ins(long VM, long ptr);
    // 功能：执行协程逻辑
    // 说明：这个函数必须中上层主动调用，不调用就不会执行协程代码
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_coroutine_udpate(long VM);
    // 功能：设置自定义的错误日志的函数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_output_error_func(LPCustomPrintCallback pFunc);
    // 功能：设置脚本中用于打印调试的函数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_debug_print_func(LPCustomPrintCallback pFunc);
    // 功能：数据异或加密或解密
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_pack_data(byte[] pData, int nOffset, int nSize, long nKey);
    // 功能：随机加密或解密（由3个密钥组成）
    // 返回值：返回新的密钥
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint fc_rand_pack_data(byte[] pData, int nOffset, int nSize, uint nKey, uint key2, uint key3);
    // 功能：产生一下随机的KEY
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_next_key(long nKey, ref uint nRandKey);
    //--------------------------------------------------------------------------------------------------
    // 以下fc_test开头的是测试代码接口，没有意义
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
    public static extern void fc_test_quaternion(ref Quaternion qua);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static unsafe extern void fc_test_struct(void* ptr, int nSize);
    // 功能：注册C#委托回调函数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_func(long VM, string pcsFuncName, fc_call_back func);
    // 功能：得到导入类的名字ID
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_inport_class_id(long VM, string pcsClassName);
    // 功能：注册C#导入类的回调函数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_func(long VM, int nClassNameID, string pcsFuncName, fc_call_back_inport_class_func func);
    // 功能：注册C#导入类的属性get/set方法
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_attrib(long VM, int nClassNameID, string pcsFuncName, fc_call_back_inport_class_func pGet, fc_call_back_inport_class_func pSet);
    // 功能：注册C#导入类的属性get/set方法, += , -=
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_attrib_ex(long VM, int nClassNameID, string pcsFuncName, fc_call_back_inport_class_func pGet, fc_call_back_inport_class_func pSet, fc_call_back_inport_class_func pAddSet, fc_call_back_inport_class_func pSubSet);
    // 功能：注册C#导入类的cast强制转换接口
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_cast(long VM, int nClassNameID, fc_call_back_inport_class_func func);
    // 功能：注册C#导入类的new
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_new(long VM, int nClassNameID, fc_call_back_inport_class_func func);
    // 功能：注册C#导入类的delete
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_del(long VM, int nClassNameID, fc_call_back_inport_class_func func);
    // 功能：注册C#导入类的全局释放引用接口
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_release_ref(long VM, int nClassNameID, fc_call_back_inport_class_func func);
    // 功能：注册C#导入类的全局的释放引用接口, 不分class
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_global_release_ref(long VM, fc_call_back_inport_class_func func);
    // 功能：注册C#导入类的hash函数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_hash(long VM, int nClassNameID, fc_call_back_inport_class_func func);
    // 功能：注册C#导入类的Equal函数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_register_class_equal(long VM, int nClassNameID, fc_call_back_inport_class_equal func);
    // 功能：平台注册的回调函数，取脚本所传的函数参数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_param_count(long L);
    // 功能：得到函数第N个参数的指针
    // 说明：可以通过这个指针，调用fc_get_value_xxx接口获取对应的变量; 可以直接调用fc_get_xxx直接获取
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_param_ptr(long L, int i);
    // 功能：得到当前模板参数个数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_template_param_count(long L);
    // 功能：得到外部导入对象的指针或句柄(外部对象的this指针, 由外部平台维护)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_inport_obj_ptr(long L);
    // 功能：得到对象类型
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_value_type(long ptr);
    // 功能：得到map对象value类型
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_map_value_type(long ptr);
    // 功能：得到对象模板类型
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_value_template_type(long ptr);
    //--------------------------------------------------------------------------------------------------
    // 以下接口脚本调用C#层wrap函数时，取脚本传递参数用的接口
    // 
    // 功能：取wrap函数的第i个变量（变量类型为char)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern char fc_get_char(long L, int i);
    // 功能：取wrap函数的第i个变量（变量类型为bool)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_get_bool(long L, int i);
    // 功能：取wrap函数的第i个变量（变量类型为byte)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern byte fc_get_byte(long L, int i);
    // 功能：取wrap函数的第i个变量（变量类型为short)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern short fc_get_short(long L, int i);
    // 功能：取wrap函数的第i个变量（变量类型为ushort)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort fc_get_ushort(long L, int i);
    // 功能：取wrap函数的第i个变量（变量类型为int)
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
    // 功能：返回C#平台对象的地址(在Unity的FC引擎中，这个是一个数字, 也就是FCGetObj管理对象的ID）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_intptr(long L, int i);
    // 功能：返回宿主平台对象ID（或地址)
    // 说明：在Unity的FC引擎中，这个是一个数字, 也就是FCGetObj管理对象的ID）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_wrap_objptr(long L, int i);
    // 功能：返回脚本对象(C++ void 指针),对应的数据类型是C#中的IntPtr
    // 功能：L - 脚本调用C#时的参数列表指针(fc_call_param_array)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_void_ptr(long L, int i);
    // 功能：当脚本调用C#Wrap接口时，取脚本指定参数变量的指针地址
    // 功能：L - 脚本调用C#时的参数列表指针(fc_call_param_array)
    // 返回值：返回指定参数的指针地址(fc_c_param_ins)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_script_param(long L, int i);
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
    public static extern void fc_get_matrix(long L, int i, ref Matrix4x4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_bounds(long L, int i, ref Bounds v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_quaternion(long L, int i, ref Quaternion v);
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
    // 功能：通过不安全的方式，直接传递指针，拷贝struct对象
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static unsafe extern void fc_get_struct_param(long L, int i, void* csharp_ptr, int nSize);
    // 功能：添加C#的函数的返回值
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_return_ptr(long L);
    // 功能：根据当前调参数取虚拟机的地址
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_vm_ptr(long L);
    // 功能：清除当前脚本函数临时传递的参数
    // 说明：这个函数并不需要上层调用，每次通过fc_call调用函数脚本后，会自动清除当前栈上的临时参数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_clear_param(long VM);
    //--------------------------------------------------------------------------------------
    // 以下是调用脚本函数前的，传给脚本函数的参数的接口
    // 
    // 功能：将一个字符参数压入函数参数栈（函数调用传参）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_char(long VM, char v);
    // 功能：将一个bool变量压入函数参数栈（函数调用传参）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_bool(long VM, bool v);
    // 功能：将一个byte变量压入函数参数栈（函数调用传参）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_byte(long VM, byte v);
    // 功能：将一个short变量压入函数参数栈（函数调用传参）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_short(long VM, short v);
    // 功能：将一个ushort变量压入函数参数栈（函数调用传参）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_ushort(long VM, ushort v);
    // 功能：将一个utf16的宽字符压入函数参数栈（函数调用传参）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_wchar(long VM, ushort v);
    // 功能：将一个int参数压入函数参数栈（函数调用传参）
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_int(long VM, int v);
    // 功能：将一个uint参数压入函数参数栈（函数调用传参)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_uint(long VM, uint v);
    // 功能：将一个float参数压入函数参数栈（函数调用传参)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_float(long VM, float v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_double(long VM, double v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_int64(long VM, long v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_uint64(long VM, ulong v);
    // 功能：将一个C#平台的对象ID压入函数参数栈（函数调用传参)
    // 说明：这个参数参数在C++平台，可以是对象的地址（转换成64位整数）
    //       如果在C#平台，可以是自己管理的对象的ID(如Unity工程中FCGetObj管理器的对象ID)
    // 这个接口有歧义，后面换成fc_push_wrap_objptr
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_intptr(long VM, long v);
    // 功能：将一个跨平台的对象ID(或对象的地址）压入函数参数栈（函数调用传参)
    // 参数：v - 宿主平台(C#或C++)对象的唯一标识（可以是管理器的对象ID, 或对象的地址）
    // 说明：Unity C#平台, 对象管理FCGetObj管理的
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_wrap_objptr(long VM, long v);
    // 功能：将一个void指针(C#中的IntPtr)参数压入函数参数栈（函数调用传参)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_void_ptr(long VM, IntPtr v);
    // 功能：将一个字符串参数压入函数参数栈（函数调用传参)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_string_a(long VM, string v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_string_w(long VM, ushort[] v, int nLen);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_byte_array(long VM, byte[] v, int nStart, int nLen);
    // 功能：将一个Vector2参数压入函数参数栈（函数调用传参)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_vector2(long VM, ref Vector2 v);
    // 功能：将一个Vector3参数压入函数参数栈（函数调用传参)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_vector3(long VM, ref Vector3 v);
    // 功能：将一个Vector4参数压入函数参数栈（函数调用传参)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_vector4(long VM, ref Vector4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_plane(long VM, ref Plane v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_matrix(long VM, ref Matrix4x4 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_bounds(long VM, ref Bounds v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_quaternion(long VM, ref Quaternion v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_ray(long VM, ref Ray v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_color32(long VM, ref Color32 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_color(long VM, ref Color v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_intrect(long VM, ref IntRect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_rect(long VM, ref Rect v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_push_sphere(long VM, ref Sphere v);
    // 功能：预备调用脚本函数
    // 参数：pIns - 脚本对象指针
    //       pcsFuncName - 函数名
    // 说明：这个接口做预备工作，主要是为了传参数的的需要，需要初始化参数列表, 如果是没有参数，或不需要传对象的参数，就不需要调用这个
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_prepare_call(long VM, long pInsPtr, string pcsFuncName);
    // 功能：调用脚本函数
    // 参数：pIns - 脚本对象指针
    //       pcsFuncName - 函数名
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_call(long VM, long pInsPtr, string pcsFuncName);
    // 功能：睡眠当前运行中的函数(虚拟器)
    // 返回值：返回当前虚拟器的指针
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_await(long L);
    // 功能：唤醒指定的函数(虚拟器), 继续当前虚拟器的运行
    // 参数：pPtr - 指定的虚拟器，由fc_await返回
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_continue(long VM, long pPtr);
    // 功能：是不是一个合法的await
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_is_valid_await(long VM, long pPtr);
    // 功能：调用指定消息读写的接口，参数必须是CSerialize
    // 参数：pIns - 脚本对象指针
    //       pcsFuncName - 函数名
    //       msgPtr - C#消息指针
    //       nStart - 开始位置
    //       nLen - 消息包的长度
    //       bReadMode - 是不是读模式,true表示读, false表示写
    // 说明：脚本函数的声明必须是    void   xxxxx(CSerialize  ar); // 这样的形式
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_serialize_msg_call(long VM, long pInsPtr, string pcsFuncName, byte[] msgPtr, int nStart, int nLen, bool bReadMode);
    //--------------------------------------------------------------------------------------
    // 以下函数(fc_get_return_xxxx)是在调用脚本函数，反函数的返回值，目前只支持少量的数据类型
    // 
    // 功能：返回最后一个fc_call调用的返回值
    // 说明：返回值的类型是bool
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_get_return_bool(long VM);
    // 功能：返回最后一个fc_call调用的返回值
    // 说明：返回值的类型是char
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern char fc_get_return_char(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern byte fc_get_return_byte(long VM);
    // 功能：返回最后一个fc_call调用的返回值
    // 说明：返回值的类型是short
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern short fc_get_return_short(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort fc_get_return_ushort(long VM);
    // 功能：返回最后一个fc_call调用的返回值
    // 说明：返回值的类型是int
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_return_int(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint fc_get_return_uint(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern float fc_get_return_float(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern double fc_get_return_double(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_return_int64(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong fc_get_return_uint64(long VM);
    // 功能：返回最后一个fc_call调用的返回值
    // 返回值：返回C#平台wrap对象ID
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_return_wrap_objptr(long VM);
    // 功能：返回最后一个fc_call调用的返回值
    // 返回值：返回c#平台的IntPtr(或C++的void *)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_return_void_ptr(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_return_string_a(long VM, byte[] pOutBuff, int nOutBuffSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_return_string_len(long VM);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_return_vector2(long VM, ref Vector2 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_return_vector3(long VM, ref Vector3 v);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_return_vector4(long VM, ref Vector4 v);
    //--------------------------------------------------------------------------------------
    // 以下是对一些常规变量的设置
    // 
    // 功能：设置对象成员变量，必须将这个类设置成导出类，或变量前标记export
    // 
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_class_value(long VM, long ptr, string value_name);
    //--------------------------------------------------------------------------------------
    // 功能：查找对象是不是有指定名字的成员函数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_find_class_func(long VM, long ptr, string func_name);
    // 功能：将一个bool值设置给脚本变量
    // 参数：ptr - 脚本对象地址
    //       v - 要设置的bool值
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
    public static extern void fc_set_value_intptr(long VM, long ptr, long v);
    // 功能：将跨平台的wrap对象(ID或地址)设置给脚本变量
    // 参数：ptr - 脚本对象地址
    //       v - wrap对象ID
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_wrap_objptr(long VM, long ptr, long v);
    // 功能：设置void *指针(IntPtr)设置给脚本变量
    // 参数：ptr - 脚本对象地址
    //       v - iNT
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_value_void_ptr(long ptr, IntPtr v);
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
    public static extern void fc_set_value_quaternion(long ptr, ref Quaternion v);
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
    // 功能：当脚本调用C#Wrap接口时，取脚本指定参数变量的指针地址
    // 功能：VM - 脚本虚拟机
    //       ptr - 脚本对象地址
    //       csharp_ptr - C#端结构指针
    //       nSize - 对象所占字节数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static unsafe extern void fc_set_struct_value(long ptr, void* csharp_ptr, int nSize);
    //--------------------------------------------------------------------------------------
    // 以下是对一些常规变量的获取
    // ptr来源：
    // (1) fc_intptr  fc_get_param_ptr(fc_intptr L, int i);  // 取wrap函数参数指针
    // (2) fc_intptr  fc_get_class_value(fc_intptr ptr, fc_pcstr value_name); // 取对象成员指针
    // (3) fc_intptr  fc_get_array_node_temp_ptr(fc_intptr ptr, int nIndex);  // 取数组对象的第N个节点的指针
    // (4) fc_intptr  fc_map_get_cur_key_ptr();      // 取当前map迭代器的key指针
    // (5) fc_intptr  fc_map_get_cur_value_ptr();    // 取当前map迭代器的value指针
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
    // 功能：从脚本变量中，取出wrap对象的ID或地址
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_value_wrap_objptr(long ptr);
    // 功能：从脚本对象中，取出void *指针(C# 中的 IntPtr)
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_value_void_ptr(long ptr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_get_value_string(long VM, long ptr, byte[] pOutBuff, int nOutBuffSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_value_string_len(long VM, long ptr);
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
    public static extern void fc_get_value_quaternion(long ptr, ref Quaternion v);
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
    // 功能：当脚本调用C#Wrap接口时，取脚本指定参数变量的指针地址
    // 功能：VM - 脚本虚拟机
    //       ptr - 脚本对象地址
    //       csharp_ptr - C#端结构指针
    //       nSize - 对象所占字节数
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static unsafe extern void fc_get_struct_value(long ptr, void* csharp_ptr, int nSize);
    //--------------------------------------------------------------------------------------
    // 以下是数组相关操作的接口
    //
    // 功能：设置对象数组
    // 功能：得到数组的长度
    //       ptr - 脚本数组指针
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_array_size(long ptr);
    // 功能：分配数组大小, 指定数组长度
    // 参数：VM - 虚拟机指针
    //       ptr - 脚本数组指针
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_set_array_size(long VM, long ptr, int nSize);
    // 功能：得到数组指定下标的节点
    // 说明：这个是全局的，请不要在外部保存，只是临时的噢, 只是用来做拷贝或读取数组参数
    // 参数：VM - 虚拟机指针
    //       ptr - 数组变量指针(fc_c_param_ins)
    // 返回值：返回数组下标指向的对象指针
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_array_node_temp_ptr(long VM, long ptr, int nIndex);
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
    //--------------------------------------------------------------------------------------
    // 以下是对map的操作的接口
    // 
    // 功能：得到map的大小
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_get_map_size(long VM, long pMapPtr);
    // 功能：准备初始化map迭代器
    // 返回值：成功返回true, map为空时返回false
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_map_prepare_view(long VM, long pMapPtr);
    // 功能：将map的全局迭代器指向下一个节点
    // 返回值：成功返回true, 失败返回false
    // 说明：如果是首次调用，就指向第一个节点, 在调用这个函数之前，必须必须fc_map_prepare_view_pair,不然可能出现异常情况
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool fc_map_to_next_pair(long VM);
    // 功能：得到map的key节点的指针 
    // 参数：
    // 返回值 - 可以调用get_value_xxx(pPair)来获取 key的值，但切不可调用 set_value_xxx(pPair)来设置的噢
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_map_get_cur_key_ptr(long VM);
    // 功能：得到map的value节点的指针 
    // 参数：
    // 返回值 - 可以调用get_value_xxx(pPair)来获取 key的值，也可以调用 set_value_xxx(pPair)来设置
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_map_get_cur_value_ptr(long VM);
    // -----------------------------以下是对map的修改接口-----------------------------------
    // 功能：将map清空
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_map_clear(long VM, long pMapPtr);
    // 功能：得到map临时添加的key指针
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_map_push_key_ptr(long VM, long pMapPtr);
    // 功能：得到map临时添加的value指针
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_get_map_push_value_ptr(long VM, long pMapPtr);
    // 功能：添加一个map的key-value
    // 参数：pMapPtr - map跨平台的指针
    // 说明：key -  可由fc_get_map_push_key设置
    //       value - 可由fc_get_map_push_value设置
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fc_map_push_key_value(long VM, long pMapPtr);
    //--------------------------------------------------------------------------------------
    // 以下是关于委托相关函数
    // 
    // 功能：得到委托函数的参数对象指针
    // 参数：pDelegatePtr - 函数指针
    // 返回值：返回参数对象的指针
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern long fc_inport_delegate_get_obj_ptr(long VM, long pDelegatePtr);
    // 功能：得到委托函数的类ID
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_inport_delegate_get_class_name_id(long VM, long pDelegatePtr);
    // 功能：得到委托函数的函数ID
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_inport_delegate_get_func_name_id(long VM, long pDelegatePtr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_inport_delegate_get_class_name_len(long VM, long pDelegatePtr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_inport_delegate_get_class_name(long VM, long pDelegatePtr, byte[] pOutBuff, int nOutBuffSize);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_inport_delegate_get_func_name_len(long VM, long pDelegatePtr);
    [DllImport(FCDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fc_inport_delegate_get_func_name(long VM, long pDelegatePtr, byte[] pOutBuff, int nOutBuffSize);

}