using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCDll
{
    static FCDll_coroutine_udpate s_pIns = null;
    static long s_VMPtr = 0;
    class FCDll_coroutine_udpate : MonoBehaviour
    {
        void Update()
        {
            FCLibHelper.fc_coroutine_udpate(s_VMPtr);// 执行协程逻辑
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
        Debug.LogError(pcsInfo);
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    static void fc_print_debug_call_back(string pcsInfo)
    {
        Debug.Log(pcsInfo);
    }

    public static long  GetMainVMPtr()
    {
        return s_VMPtr;
    }

    public static void  ReleaseDll()
    {
        FCLibHelper.fc_release(s_VMPtr);
        s_VMPtr = 0;
        if (s_pIns != null)
        {
            GameObject.DestroyImmediate(s_pIns.gameObject);
            s_pIns = null;
        }
    }

    public static long  InitDll(bool bMainThread = true)
    {
        long VMPtr = 0;
        try
        {
            FCLibHelper.fc_set_output_error_func(fc_print_error_call_back);
            FCLibHelper.fc_set_debug_print_func(fc_print_debug_call_back);

            if(bMainThread)
                ReleaseDll();
            CreateInstance();
            if(bMainThread)
                VMPtr = s_VMPtr = FCLibHelper.fc_init(true);  // 创建脚本全局单例
            else
                VMPtr = FCLibHelper.fc_init(false);  // 创建脚本全局单例

            // 注册两个读写文件的wrap接口
            FCLibHelper.fc_register_func(VMPtr, "ReadFile", csharp_readfile);
            FCLibHelper.fc_register_func(VMPtr, "WriteFile", csharp_writefile);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return VMPtr;
    }
    public static bool  IsInitDll()
    {
        return s_pIns != null;
    }

    // 功能：调用FC脚本时，传递C#参数给脚本函数
    public static void PushCallParam(long VM, bool v)
    {
        FCLibHelper.fc_push_bool(VM, v);
    }
    public static void PushCallParam(long VM, byte v)
    {
        FCLibHelper.fc_push_byte(VM, v);
    }
    public static void PushCallParam(long VM, char v)
    {
        FCLibHelper.fc_push_char(VM, v);
    }
    public static void PushCallParam(long VM, short v)
    {
        FCLibHelper.fc_push_short(VM, v);
    }
    public static void PushCallParam(long VM, ushort v)
    {
        FCLibHelper.fc_push_ushort(VM, v);
    }
    public static void PushCallParam(long VM, int v)
    {
        FCLibHelper.fc_push_int(VM, v);
    }
    public static void PushCallParam(long VM, uint v)
    {
        FCLibHelper.fc_push_uint(VM, v);
    }
    public static void PushCallParam(long VM, float v)
    {
        FCLibHelper.fc_push_float(VM, v);
    }
    public static void PushCallParam(long VM, double v)
    {
        FCLibHelper.fc_push_double(VM, v);
    }
    public static void PushCallParam(long VM, long v)
    {
        FCLibHelper.fc_push_int64(VM, v);
    }
    public static void PushCallParam(long VM, ulong v)
    {
        FCLibHelper.fc_push_uint64(VM, v);
    }
    public static void PushCallParam(long VM, IntPtr v)
    {
        FCLibHelper.fc_push_void_ptr(VM, v);
    }
    public static void PushCallParam(long VM, string v)
    {
        FCLibHelper.fc_push_string_a(VM, v);
    }
    public static void PushCallParam(long VM, byte []v)
    {
        if (v != null)
            FCLibHelper.fc_push_byte_array(VM, v, 0, v.Length);
        else
            FCLibHelper.fc_push_byte_array(VM, v, 0, 0);
    }
    public static void PushCallParam(long VM, byte[] v, int nStart, int nLen)
    {
        if (v != null && nStart > 0 && nStart + nLen < v.Length)
            FCLibHelper.fc_push_byte_array(VM, v, nStart, nLen);
        else
            FCLibHelper.fc_push_byte_array(VM, v, 0, 0);
    }
    public static void PushCallParam(long VM, ref Vector2 v)
    {
        FCLibHelper.fc_push_vector2(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Vector3 v)
    {
        FCLibHelper.fc_push_vector3(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Vector4 v)
    {
        FCLibHelper.fc_push_vector4(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Plane v)
    {
        FCLibHelper.fc_push_plane(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Matrix4x4 v)
    {
        FCLibHelper.fc_push_matrix(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Bounds v)
    {
        FCLibHelper.fc_push_bounds(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Ray v)
    {
        FCLibHelper.fc_push_ray(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Quaternion v)
    {
        FCLibHelper.fc_push_quaternion(VM, ref v);
    }
    public static void PushCallParam(long VM, Color32 v)
    {
        FCLibHelper.fc_push_color32(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Color v)
    {
        FCLibHelper.fc_push_color(VM, ref v);
    }
    public static void PushCallParam(long VM, ref IntRect v)
    {
        FCLibHelper.fc_push_intrect(VM, ref v);
    }
    public static void PushCallParam(long VM, ref Rect v)
    {
        FCLibHelper.fc_push_rect(VM, ref v);
    }
    public static void PushCallParam(long VM, Sphere v)
    {
        FCLibHelper.fc_push_sphere(VM, ref v);
    }
    public static void PushCallParam(long VM, UnityEngine.Object obj)
    {
        long nPtr = FCGetObj.PushObj(obj);
        FCLibHelper.fc_push_intptr(VM, nPtr);
    }
    public static void PushCallParam(long VM, System.Object obj)
    {
        long nPtr = FCGetObj.PushObj(obj);
        FCLibHelper.fc_push_intptr(VM, nPtr);
    }
    public static void PushCallObjectParam<_Ty>(long VM, _Ty obj)
    {
        long  nPtr = FCGetObj.PushObj(obj);
        FCLibHelper.fc_push_intptr(VM, nPtr);
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
