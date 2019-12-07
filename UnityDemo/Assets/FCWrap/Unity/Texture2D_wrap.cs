using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class Texture2D_wrap
{
    public static UnityEngine.Texture2D get_obj(long L)
    {
        return FCGetObj.GetObj<UnityEngine.Texture2D>(L);
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
            UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0,arg1,arg2,arg3,arg4);
            long nPtr = FCGetObj.PushNewObj<UnityEngine.Texture2D>(obj);
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
            UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0,arg1,arg2,arg3);
            long nPtr = FCGetObj.PushNewObj<UnityEngine.Texture2D>(obj);
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
            UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0,arg1);
            long nPtr = FCGetObj.PushNewObj<UnityEngine.Texture2D>(obj);
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
        UnityEngine.Texture2D obj = FCGetObj.GetObj<UnityEngine.Texture2D>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UnityEngine.Texture2D left  = FCGetObj.GetObj<UnityEngine.Texture2D>(L);
        UnityEngine.Texture2D right = FCGetObj.GetObj<UnityEngine.Texture2D>(R);
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
            UnityEngine.Texture2D ret = get_obj(nThisPtr);
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
            UnityEngine.Texture2D ret = get_obj(nThisPtr);
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
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(UnityEngine.Texture2D.whiteTexture);
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
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(UnityEngine.Texture2D.blackTexture);
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
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)(FCLibHelper.fc_get_int(L,2));
            bool arg3 = FCLibHelper.fc_get_bool(L,3);
            bool arg4 = FCLibHelper.fc_get_bool(L,4);
            System.IntPtr arg5 = FCGetObj.GetObj<System.IntPtr>(FCLibHelper.fc_get_intptr(L,5));
            UnityEngine.Texture2D ret = UnityEngine.Texture2D.CreateExternalTexture(arg0,arg1,arg2,arg3,arg4,arg5);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            System.IntPtr arg0 = FCGetObj.GetObj<System.IntPtr>(FCLibHelper.fc_get_intptr(L,0));
            obj.UpdateExternalTexture(arg0);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            Color arg2 = new Color();
            FCLibHelper.fc_get_color(L,2,ref arg2);
            obj.SetPixel(arg0,arg1,arg2);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            Color ret = obj.GetPixel(arg0,arg1);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            float arg0 = FCLibHelper.fc_get_float(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            Color ret = obj.GetPixelBilinear(arg0,arg1);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            Color[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            obj.SetPixels(arg0);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            Color[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            obj.SetPixels(arg0,arg1);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            Color[] arg4 = null;
            arg4 = FCCustomParam.GetArray(ref arg4,L,4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            obj.SetPixels(arg0,arg1,arg2,arg3,arg4,arg5);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            Color[] arg4 = null;
            arg4 = FCCustomParam.GetArray(ref arg4,L,4);
            obj.SetPixels(arg0,arg1,arg2,arg3,arg4);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            Color32[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            obj.SetPixels32(arg0);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            Color32[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            obj.SetPixels32(arg0,arg1);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            Color32[] arg4 = null;
            arg4 = FCCustomParam.GetArray(ref arg4,L,4);
            obj.SetPixels32(arg0,arg1,arg2,arg3,arg4);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            Color32[] arg4 = null;
            arg4 = FCCustomParam.GetArray(ref arg4,L,4);
            int arg5 = FCLibHelper.fc_get_int(L,5);
            obj.SetPixels32(arg0,arg1,arg2,arg3,arg4,arg5);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            byte[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            obj.LoadRawTextureData(arg0);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            System.IntPtr arg0 = FCGetObj.GetObj<System.IntPtr>(FCLibHelper.fc_get_intptr(L,0));
            int arg1 = FCLibHelper.fc_get_int(L,1);
            obj.LoadRawTextureData(arg0,arg1);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            byte[] ret = obj.GetRawTextureData();
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            Color[] ret = obj.GetPixels();
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Color[] ret = obj.GetPixels(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            int arg4 = FCLibHelper.fc_get_int(L,4);
            Color[] ret = obj.GetPixels(arg0,arg1,arg2,arg3,arg4);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            Color[] ret = obj.GetPixels(arg0,arg1,arg2,arg3);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Color32[] ret = obj.GetPixels32(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            Color32[] ret = obj.GetPixels32();
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            obj.Apply(arg0,arg1);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            obj.Apply(arg0);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)(FCLibHelper.fc_get_int(L,2));
            bool arg3 = FCLibHelper.fc_get_bool(L,3);
            bool ret = obj.Resize(arg0,arg1,arg2,arg3);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            bool ret = obj.Resize(arg0,arg1);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            obj.Compress(arg0);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            UnityEngine.Texture2D[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            bool arg3 = FCLibHelper.fc_get_bool(L,3);
            Rect[] ret = obj.PackTextures(arg0,arg1,arg2,arg3);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            UnityEngine.Texture2D[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            Rect[] ret = obj.PackTextures(arg0,arg1,arg2);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            UnityEngine.Texture2D[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            Rect[] ret = obj.PackTextures(arg0,arg1);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
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
            Vector2[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            List<Rect> arg3 = null;
            arg3 = FCCustomParam.GetList(ref arg3,L,3);
            bool ret = UnityEngine.Texture2D.GenerateAtlas(arg0,arg1,arg2,arg3);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            Rect arg0 = new Rect();
            FCLibHelper.fc_get_rect(L,0,ref arg0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            bool arg3 = FCLibHelper.fc_get_bool(L,3);
            obj.ReadPixels(arg0,arg1,arg2,arg3);
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
            UnityEngine.Texture2D obj = get_obj(nThisPtr);
            Rect arg0 = new Rect();
            FCLibHelper.fc_get_rect(L,0,ref arg0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            obj.ReadPixels(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
