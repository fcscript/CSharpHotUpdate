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
            FCLibHelper.fc_init();
            FCLibHelper.fc_set_output_error_func(fc_print_error_call_back);
            FCLibHelper.fc_set_debug_print_func(fc_print_debug_call_back);

            // 注册其他的函数
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

    // 功能：调用FC脚本时，传递参数
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
    public static void PushCallParam(Vector2 v)
    {
        FCLibHelper.fc_push_vector2(ref v);
    }
    public static void PushCallParam(Vector3 v)
    {
        FCLibHelper.fc_push_vector3(ref v);
    }
    public static void PushCallParam(Vector4 v)
    {
        FCLibHelper.fc_push_vector4(ref v);
    }

    //----------------------------------------------------------------------------

    // 功能：调用FC脚本时，传递参数
    public static void GetCallParam(ref bool v)
    {
        v = FCLibHelper.fc_get_return_bool();
    }
    public static void GetCallParam(ref byte v)
    {
        v = FCLibHelper.fc_get_return_byte();
    }
    public static void GetCallParam(ref char v)
    {
        v = FCLibHelper.fc_get_return_char();
    }
    public static void GetCallParam(ref short v)
    {
        v = FCLibHelper.fc_get_return_short();
    }
    public static void GetCallParam(ref ushort v)
    {
        v = FCLibHelper.fc_get_return_ushort();
    }
    public static void GetCallParam(ref int v)
    {
        v = FCLibHelper.fc_get_return_int();
    }
    public static void GetCallParam(ref uint v)
    {
        v = FCLibHelper.fc_get_return_uint();
    }
    public static void GetCallParam(ref float v)
    {
        v = FCLibHelper.fc_get_return_float();
    }
    public static void GetCallParam(ref double v)
    {
        v = FCLibHelper.fc_get_return_double();
    }
    public static void GetCallParam(ref long v)
    {
        v = FCLibHelper.fc_get_return_int64();
    }
    public static void GetCallParam(ref ulong v)
    {
        v = FCLibHelper.fc_get_return_uint64();
    }
    public static void GetCallParam(ref IntPtr v)
    {
        v = new IntPtr(FCLibHelper.fc_get_return_intptr());
    }
    public static void GetCallParam(ref string v)
    {
        v = FCLibHelper.fc_get_return_string_a();
    }
    //public static void GetCallParam(ref byte[] v)
    //{
    //    FCLibHelper.fc_get_return_string_a();
    //}
    public static void GetCallParam(ref Vector2 v)
    {
        v = FCLibHelper.fc_get_return_vector2();
    }
    public static void GetCallParam(ref Vector3 v)
    {
        v = FCLibHelper.fc_get_return_vector3();
    }
    public static void GetCallParam(ref Vector4 v)
    {
        v = FCLibHelper.fc_get_return_vector4();
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

            if(fileData != null)
                FCLibHelper.fc_set_byte_array(L, 1, fileData, 0, fileData.Length);
            else
                FCLibHelper.fc_set_byte_array(L, 1, null, 0, 0);
        }
        catch(Exception e)
        {
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
        }
        return 1;
    }    
}
