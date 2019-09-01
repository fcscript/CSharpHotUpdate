using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCDll
{
    static FCDll_coroutine_udpate s_pIns = null;
    class FCDll_coroutine_udpate : MonoBehaviour
    {
        void Update()
        {
            FCLibHelper.fc_coroutine_udpate();// 执行协程逻辑
        }
    }

    static void CreateInstance()
    {
        if(s_pIns == null)
        {
            GameObject go = new GameObject("fc_instance");
            GameObject.DontDestroyOnLoad(go);
            s_pIns = go.AddComponent<FCDll_coroutine_udpate>();
        }
    }
    
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    static void  fc_print_error_call_back(string pcsInfo)
    {
        Debug.Log(pcsInfo);
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    static void fc_print_debug_call_back(string pcsInfo)
    {
        Debug.Log(pcsInfo);
    }

    public static void  ReleaseDll()
    {
        FCLibHelper.fc_release();
        if(s_pIns != null)
        {
            GameObject.DestroyImmediate(s_pIns.gameObject);
        }
    }

    public static void  InitDll()
    {
        try
        {
            ReleaseDll();
            CreateInstance();
            FCLibHelper.fc_init();  // 创建脚本全局单例
            FCLibHelper.fc_set_output_error_func(fc_print_error_call_back);
            FCLibHelper.fc_set_debug_print_func(fc_print_debug_call_back);

            // 注册两个读写文件的wrap接口
            FCLibHelper.fc_register_func("ReadFile", csharp_readfile);
            FCLibHelper.fc_register_func("WriteFile", csharp_writefile);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static bool  IsInitDll()
    {
        return s_pIns != null;
    }

    // 功能：调用FC脚本时，传递C#参数给脚本函数
    public static void PushCallParam(bool v)
    {
        FCLibHelper.fc_push_bool(v);
    }
    public static void PushCallParam(byte v)
    {
        FCLibHelper.fc_push_byte(v);
    }
    public static void PushCallParam(char v)
    {
        FCLibHelper.fc_push_char(v);
    }
    public static void PushCallParam(short v)
    {
        FCLibHelper.fc_push_short(v);
    }
    public static void PushCallParam(ushort v)
    {
        FCLibHelper.fc_push_ushort(v);
    }
    public static void PushCallParam(int v)
    {
        FCLibHelper.fc_push_int(v);
    }
    public static void PushCallParam(uint v)
    {
        FCLibHelper.fc_push_uint(v);
    }
    public static void PushCallParam(float v)
    {
        FCLibHelper.fc_push_float(v);
    }
    public static void PushCallParam(double v)
    {
        FCLibHelper.fc_push_double(v);
    }
    public static void PushCallParam(long v)
    {
        FCLibHelper.fc_push_int64(v);
    }
    public static void PushCallParam(ulong v)
    {
        FCLibHelper.fc_push_uint64(v);
    }
    public static void PushCallParam(IntPtr v)
    {
        FCLibHelper.fc_push_intptr(v.ToInt64());
    }
    public static void PushCallParam(string v)
    {
        FCLibHelper.fc_push_string_a(v);
    }
    public static void PushCallParam(byte []v)
    {
        if (v != null)
            FCLibHelper.fc_push_byte_array(v, 0, v.Length);
        else
            FCLibHelper.fc_push_byte_array(v, 0, 0);
    }
    public static void PushCallParam(byte[] v, int nStart, int nLen)
    {
        if (v != null && nStart > 0 && nStart + nLen < v.Length)
            FCLibHelper.fc_push_byte_array(v, nStart, nLen);
        else
            FCLibHelper.fc_push_byte_array(v, 0, 0);
    }
    public static void PushCallParam(ref Vector2 v)
    {
        FCLibHelper.fc_push_vector2(ref v);
    }
    public static void PushCallParam(ref Vector3 v)
    {
        FCLibHelper.fc_push_vector3(ref v);
    }
    public static void PushCallParam(ref Vector4 v)
    {
        FCLibHelper.fc_push_vector4(ref v);
    }
    public static void PushCallParam(ref Plane v)
    {
        FCLibHelper.fc_push_plane(ref v);
    }
    public static void PushCallParam(ref Matrix4x4 v)
    {
        FCLibHelper.fc_push_matrix(ref v);
    }
    public static void PushCallParam(ref Bounds v)
    {
        FCLibHelper.fc_push_bounds(ref v);
    }
    public static void PushCallParam(ref Ray v)
    {
        FCLibHelper.fc_push_ray(ref v);
    }
    public static void PushCallParam(ref Quaternion v)
    {
        FCLibHelper.fc_push_quaternion(ref v);
    }
    public static void PushCallParam(Color32 v)
    {
        FCLibHelper.fc_push_color32(ref v);
    }
    public static void PushCallParam(ref Color v)
    {
        FCLibHelper.fc_push_color(ref v);
    }
    public static void PushCallParam(ref IntRect v)
    {
        FCLibHelper.fc_push_intrect(ref v);
    }
    public static void PushCallParam(ref Rect v)
    {
        FCLibHelper.fc_push_rect(ref v);
    }
    public static void PushCallParam(Sphere v)
    {
        FCLibHelper.fc_push_sphere(ref v);
    }
    
    // 功能：调用FC脚本后，获取脚本函数的返回值
    public static void GetReturnParam(ref bool v)
    {
        v = FCLibHelper.fc_get_return_bool();
    }
    public static void GetReturnParam(ref byte v)
    {
        v = FCLibHelper.fc_get_return_byte();
    }
    public static void GetReturnParam(ref char v)
    {
        v = FCLibHelper.fc_get_return_char();
    }
    public static void GetReturnParam(ref short v)
    {
        v = FCLibHelper.fc_get_return_short();
    }
    public static void GetReturnParam(ref ushort v)
    {
        v = FCLibHelper.fc_get_return_ushort();
    }
    public static void GetReturnParam(ref int v)
    {
        v = FCLibHelper.fc_get_return_int();
    }
    public static void GetReturnParam(ref uint v)
    {
        v = FCLibHelper.fc_get_return_uint();
    }
    public static void GetReturnParam(ref float v)
    {
        v = FCLibHelper.fc_get_return_float();
    }
    public static void GetReturnParam(ref double v)
    {
        v = FCLibHelper.fc_get_return_double();
    }
    public static void GetReturnParam(ref long v)
    {
        v = FCLibHelper.fc_get_return_int64();
    }
    public static void GetReturnParam(ref ulong v)
    {
        v = FCLibHelper.fc_get_return_uint64();
    }
    public static void GetReturnParam(ref string v)
    {
        v = FCLibHelper.fc_get_return_string_a();
    }
    //public static void GetReturnParam(ref byte[] v)
    //{
    //    FCLibHelper.fc_get_return_string_a();
    //}
    public static void GetReturnParam(ref Vector2 v)
    {
        FCLibHelper.fc_get_return_vector2(ref v);
    }
    public static void GetReturnParam(ref Vector3 v)
    {
        FCLibHelper.fc_get_return_vector3(ref v);
    }
    public static void GetReturnParam(ref Vector4 v)
    {
        FCLibHelper.fc_get_return_vector4(ref v);
    }
    //------------------------------------------------------------------------

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int csharp_readfile(long L)
    {
        try
        {
            int nParamCount = FCLibHelper.fc_get_param_count(L);
            if (nParamCount != 2)
                return 0;
            string pcsPathName = FCLibHelper.fc_get_string_a(L, 0);

            byte[] fileData = System.IO.File.ReadAllBytes(pcsPathName);

            long ptr = FCLibHelper.fc_get_param_ptr(L, 1);

            if (fileData != null)
                FCLibHelper.fc_set_array_byte(ptr, fileData, 0, fileData.Length);
            else
                FCLibHelper.fc_set_array_byte(ptr, null, 0, 0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 1;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int csharp_writefile(long L)
    {
        try
        {
            int nParamCount = FCLibHelper.fc_get_param_count(L);
            if (nParamCount != 2)
                return 0;

            string pcsPathName = FCLibHelper.fc_get_string_a(L, 0);
            byte[] fileData = FCLibHelper.fc_get_byte_array(L, 1);

            System.IO.File.WriteAllBytes(pcsPathName, fileData);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        return 1;
    }    
}
