using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class Texture2D_wrap
{
    public static Texture2D get_obj(long L)
    {
        return FCGetObj.GetObj<Texture2D>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("Texture2D");
        FCLibHelper.fc_register_class_func(nClassName, "Texture2D", obj_new3);
        FCLibHelper.fc_register_class_func(nClassName, "Texture2D", obj_new2);
        FCLibHelper.fc_register_class_func(nClassName, "Texture2D", obj_new1);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(nClassName,"mipmapCount",get_mipmapCount_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"format",get_format_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"whiteTexture",get_whiteTexture_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"blackTexture",get_blackTexture_wrap,null);
        FCLibHelper.fc_register_class_func(nClassName,"CreateExternalTexture",CreateExternalTexture_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"UpdateExternalTexture",UpdateExternalTexture_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixel",SetPixel_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPixel",GetPixel_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPixelBilinear",GetPixelBilinear_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixels_List<Color>",SetPixels_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixels_List<Color>_int",SetPixels1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixels_int_int_int_int_List<Color>_int",SetPixels2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixels_int_int_int_int_List<Color>",SetPixels3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixels32_List<Color32>",SetPixels32_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixels32_List<Color32>_int",SetPixels321_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixels32_int_int_int_int_List<Color32>",SetPixels322_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPixels32_int_int_int_int_List<Color32>_int",SetPixels323_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadRawTextureData_List<byte>",LoadRawTextureData_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadRawTextureData_IntPtr_int",LoadRawTextureData1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetRawTextureData",GetRawTextureData_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPixels",GetPixels_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPixels_int",GetPixels1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPixels_int_int_int_int_int",GetPixels2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPixels_int_int_int_int",GetPixels3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPixels32_int",GetPixels32_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPixels32",GetPixels321_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Apply_bool_bool",Apply_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Apply_bool",Apply1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Apply",Apply2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Resize_int_int_TextureFormat_bool",Resize_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Resize_int_int",Resize1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Compress",Compress_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"PackTextures_List<Texture2D>_int_int_bool",PackTextures_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"PackTextures_List<Texture2D>_int_int",PackTextures1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"PackTextures_List<Texture2D>_int",PackTextures2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GenerateAtlas",GenerateAtlas_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"ReadPixels_Rect_int_int_bool",ReadPixels_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"ReadPixels_Rect_int_int",ReadPixels1_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new3(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)(FCLibHelper.fc_get_int(L,2));
            bool arg3 = FCLibHelper.fc_get_bool(L,3);
            bool arg4 = FCLibHelper.fc_get_bool(L,4);
            Texture2D obj = new Texture2D(arg0,arg1,arg2,arg3,arg4);
            long nPtr = FCGetObj.PushNewObj<Texture2D>(obj);
            long ret = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_intptr(ret, nPtr);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new2(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)(FCLibHelper.fc_get_int(L,2));
            bool arg3 = FCLibHelper.fc_get_bool(L,3);
            Texture2D obj = new Texture2D(arg0,arg1,arg2,arg3);
            long nPtr = FCGetObj.PushNewObj<Texture2D>(obj);
            long ret = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_intptr(ret, nPtr);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new1(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            Texture2D obj = new Texture2D(arg0,arg1);
            long nPtr = FCGetObj.PushNewObj<Texture2D>(obj);
            long ret = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_intptr(ret, nPtr);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_del(long L)
    {
        FCGetObj.DelObj(L);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_release(long L)
    {
        FCGetObj.ReleaseRef(L);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_hash(long L)
    {
        Texture2D obj = FCGetObj.GetObj<Texture2D>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        Texture2D left  = FCGetObj.GetObj<Texture2D>(L);
        Texture2D right = FCGetObj.GetObj<Texture2D>(R);
        if(left != null)
        {
            return left.Equals(right);
        }
        if(right != null)
        {
            return right.Equals(left);
        }
        return true;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_mipmapCount_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret.mipmapCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_format_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.format);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_whiteTexture_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(Texture2D.whiteTexture);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_blackTexture_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(Texture2D.blackTexture);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CreateExternalTexture_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            int arg7 = FCLibHelper.fc_get_int(L,7);
            UnityEngine.TextureFormat arg8 = (UnityEngine.TextureFormat)(FCLibHelper.fc_get_int(L,8));
            bool arg9 = FCLibHelper.fc_get_bool(L,9);
            bool arg10 = FCLibHelper.fc_get_bool(L,10);
            System.IntPtr arg11 = FCGetObj.GetObj<System.IntPtr>(FCLibHelper.fc_get_intptr(L,11));
            Texture2D ret = Texture2D.CreateExternalTexture(arg6,arg7,arg8,arg9,arg10,arg11);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int UpdateExternalTexture_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            System.IntPtr arg1 = FCGetObj.GetObj<System.IntPtr>(FCLibHelper.fc_get_intptr(L,1));
            obj.UpdateExternalTexture(arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixel_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            int arg4 = FCLibHelper.fc_get_int(L,4);
            Color arg5 = new Color();
            FCLibHelper.fc_get_color(L,5,ref arg5);
            obj.SetPixel(arg3,arg4,arg5);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetPixel_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            Color ret = obj.GetPixel(arg2,arg3);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Color temp_ret = ret;
            FCLibHelper.fc_set_value_color(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetPixelBilinear_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            float arg2 = FCLibHelper.fc_get_float(L,2);
            float arg3 = FCLibHelper.fc_get_float(L,3);
            Color ret = obj.GetPixelBilinear(arg2,arg3);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Color temp_ret = ret;
            FCLibHelper.fc_set_value_color(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixels_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Color[] arg1 = null;
            arg1 = FCCustomParam.GetArray(ref arg1,L,1);
            obj.SetPixels(arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixels1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Color[] arg2 = null;
            arg2 = FCCustomParam.GetArray(ref arg2,L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            obj.SetPixels(arg2,arg3);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixels2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            int arg7 = FCLibHelper.fc_get_int(L,7);
            int arg8 = FCLibHelper.fc_get_int(L,8);
            int arg9 = FCLibHelper.fc_get_int(L,9);
            Color[] arg10 = null;
            arg10 = FCCustomParam.GetArray(ref arg10,L,10);
            int arg11 = FCLibHelper.fc_get_int(L,11);
            obj.SetPixels(arg6,arg7,arg8,arg9,arg10,arg11);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixels3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            int arg7 = FCLibHelper.fc_get_int(L,7);
            int arg8 = FCLibHelper.fc_get_int(L,8);
            Color[] arg9 = null;
            arg9 = FCCustomParam.GetArray(ref arg9,L,9);
            obj.SetPixels(arg5,arg6,arg7,arg8,arg9);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixels32_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Color32[] arg1 = null;
            arg1 = FCCustomParam.GetArray(ref arg1,L,1);
            obj.SetPixels32(arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixels321_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Color32[] arg2 = null;
            arg2 = FCCustomParam.GetArray(ref arg2,L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            obj.SetPixels32(arg2,arg3);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixels322_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            int arg7 = FCLibHelper.fc_get_int(L,7);
            int arg8 = FCLibHelper.fc_get_int(L,8);
            Color32[] arg9 = null;
            arg9 = FCCustomParam.GetArray(ref arg9,L,9);
            obj.SetPixels32(arg5,arg6,arg7,arg8,arg9);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPixels323_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            int arg7 = FCLibHelper.fc_get_int(L,7);
            int arg8 = FCLibHelper.fc_get_int(L,8);
            int arg9 = FCLibHelper.fc_get_int(L,9);
            Color32[] arg10 = null;
            arg10 = FCCustomParam.GetArray(ref arg10,L,10);
            int arg11 = FCLibHelper.fc_get_int(L,11);
            obj.SetPixels32(arg6,arg7,arg8,arg9,arg10,arg11);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadRawTextureData_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            byte[] arg1 = null;
            arg1 = FCCustomParam.GetArray(ref arg1,L,1);
            obj.LoadRawTextureData(arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadRawTextureData1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            System.IntPtr arg2 = FCGetObj.GetObj<System.IntPtr>(FCLibHelper.fc_get_intptr(L,2));
            int arg3 = FCLibHelper.fc_get_int(L,3);
            obj.LoadRawTextureData(arg2,arg3);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetRawTextureData_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            byte[] ret = obj.GetRawTextureData();
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetPixels_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Color[] ret = obj.GetPixels();
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetPixels1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            Color[] ret = obj.GetPixels(arg1);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetPixels2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            int arg7 = FCLibHelper.fc_get_int(L,7);
            int arg8 = FCLibHelper.fc_get_int(L,8);
            int arg9 = FCLibHelper.fc_get_int(L,9);
            Color[] ret = obj.GetPixels(arg5,arg6,arg7,arg8,arg9);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetPixels3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg4 = FCLibHelper.fc_get_int(L,4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            int arg7 = FCLibHelper.fc_get_int(L,7);
            Color[] ret = obj.GetPixels(arg4,arg5,arg6,arg7);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetPixels32_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            Color32[] ret = obj.GetPixels32(arg1);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetPixels321_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Color32[] ret = obj.GetPixels32();
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Apply_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            bool arg2 = FCLibHelper.fc_get_bool(L,2);
            bool arg3 = FCLibHelper.fc_get_bool(L,3);
            obj.Apply(arg2,arg3);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Apply1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            obj.Apply(arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Apply2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            obj.Apply();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Resize_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg4 = FCLibHelper.fc_get_int(L,4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            UnityEngine.TextureFormat arg6 = (UnityEngine.TextureFormat)(FCLibHelper.fc_get_int(L,6));
            bool arg7 = FCLibHelper.fc_get_bool(L,7);
            bool ret = obj.Resize(arg4,arg5,arg6,arg7);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Resize1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            bool ret = obj.Resize(arg2,arg3);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Compress_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            obj.Compress(arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int PackTextures_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            UnityEngine.Texture2D[] arg4 = null;
            arg4 = FCCustomParam.GetArray(ref arg4,L,4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            bool arg7 = FCLibHelper.fc_get_bool(L,7);
            Rect[] ret = obj.PackTextures(arg4,arg5,arg6,arg7);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int PackTextures1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            UnityEngine.Texture2D[] arg3 = null;
            arg3 = FCCustomParam.GetArray(ref arg3,L,3);
            int arg4 = FCLibHelper.fc_get_int(L,4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            Rect[] ret = obj.PackTextures(arg3,arg4,arg5);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int PackTextures2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            UnityEngine.Texture2D[] arg2 = null;
            arg2 = FCCustomParam.GetArray(ref arg2,L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            Rect[] ret = obj.PackTextures(arg2,arg3);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GenerateAtlas_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Vector2[] arg4 = null;
            arg4 = FCCustomParam.GetArray(ref arg4,L,4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            List<Rect> arg7 = null;
            arg7 = FCCustomParam.GetList(ref arg7,L,7);
            bool ret = Texture2D.GenerateAtlas(arg4,arg5,arg6,arg7);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int ReadPixels_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Rect arg4 = new Rect();
            FCLibHelper.fc_get_rect(L,4,ref arg4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            int arg6 = FCLibHelper.fc_get_int(L,6);
            bool arg7 = FCLibHelper.fc_get_bool(L,7);
            obj.ReadPixels(arg4,arg5,arg6,arg7);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int ReadPixels1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Texture2D obj = get_obj(nThisPtr);
            Rect arg3 = new Rect();
            FCLibHelper.fc_get_rect(L,3,ref arg3);
            int arg4 = FCLibHelper.fc_get_int(L,4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            obj.ReadPixels(arg3,arg4,arg5);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
