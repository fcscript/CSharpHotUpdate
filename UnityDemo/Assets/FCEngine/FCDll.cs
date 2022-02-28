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

    //------------------------------------------------------------------------
    //public static void WriteValueToScript(long VM, long ScriptValuePtr, System.Object obj)
    //{
    //    long nPtr = FCGetObj.PushObj(obj);
    //    FCLibHelper.fc_set_value_wrap_objptr(VM, ScriptValuePtr, nPtr);
    //}
	public static void WriteValueToScript(long VM, long ScriptValuePtr, System.Object obj)
	{
		long nPtr = FCGetObj.PushObj(obj);
		FCLibHelper.fc_set_value_wrap_objptr(VM, ScriptValuePtr, nPtr);
	}
    public static void WriteValueToScript(long VM, long ScriptValuePtr, UnityEngine.Object obj)
    {
        long nPtr = FCGetObj.PushObj(obj);
        FCLibHelper.fc_set_value_wrap_objptr(VM, ScriptValuePtr, nPtr);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, bool v)
    {
        FCLibHelper.fc_set_value_bool(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, byte v)
    {
        FCLibHelper.fc_set_value_byte(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, char v)
    {
        FCLibHelper.fc_set_value_char(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, short v)
    {
        FCLibHelper.fc_set_value_short(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, ushort v)
    {
        FCLibHelper.fc_set_value_ushort(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, int v)
    {
        FCLibHelper.fc_set_value_int(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, uint v)
    {
        FCLibHelper.fc_set_value_uint(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, float v)
    {
        FCLibHelper.fc_set_value_float(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, double v)
    {
        FCLibHelper.fc_set_value_double(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, long v)
    {
        FCLibHelper.fc_set_value_int64(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, ulong v)
    {
        FCLibHelper.fc_set_value_uint64(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, IntPtr v)
    {
        FCLibHelper.fc_set_value_void_ptr(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, string v)
    {
        FCLibHelper.fc_set_value_string_a(ScriptValuePtr, v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Vector2 v)
    {
        FCLibHelper.fc_set_value_vector2(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Vector3 v)
    {
        FCLibHelper.fc_set_value_vector3(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Vector4 v)
    {
        FCLibHelper.fc_set_value_vector4(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Plane v)
    {
        FCLibHelper.fc_set_value_plane(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Bounds v)
    {
        FCLibHelper.fc_set_value_bounds(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Quaternion v)
    {
        FCLibHelper.fc_set_value_quaternion(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Color32 v)
    {
        FCLibHelper.fc_set_value_color32(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Color v)
    {
        FCLibHelper.fc_set_value_color(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, IntRect v)
    {
        FCLibHelper.fc_set_value_intrect(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Rect v)
    {
        FCLibHelper.fc_set_value_rect(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Sphere v)
    {
        FCLibHelper.fc_set_value_sphere(ScriptValuePtr, ref v);
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, byte []v)
    {
        if (v != null)
        {
            FCLibHelper.fc_set_array_byte(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, bool[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_set_array_bool(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, short[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_set_array_short(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, ushort[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_set_array_ushort(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, int[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_set_array_int(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, uint[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_set_array_uint(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, float[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_set_array_float(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, double[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_set_array_double(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Color32[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_get_array_color32(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Color[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_get_array_color(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Vector2[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_get_array_vector2(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Vector3[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_get_array_vector3(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Vector4[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_get_array_vector4(ScriptValuePtr, v, 0, v.Length);
        }
    }
    public static void WriteValueToScript(long VM, long ScriptValuePtr, Rect[] v)
    {
        if (v != null)
        {
            FCLibHelper.fc_get_array_rect(ScriptValuePtr, v, 0, v.Length);
        }
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
