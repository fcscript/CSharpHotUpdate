using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class Material_wrap
{
    public static Material get_obj(long L)
    {
        return FCGetObj.GetObj<Material>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("Material");
        FCLibHelper.fc_register_class_func(nClassName, "Material", obj_new3);
        FCLibHelper.fc_register_class_func(nClassName, "Material", obj_new2);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(nClassName,"shader",get_shader_wrap,set_shader_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"color",get_color_wrap,set_color_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"mainTexture",get_mainTexture_wrap,set_mainTexture_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"mainTextureOffset",get_mainTextureOffset_wrap,set_mainTextureOffset_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"mainTextureScale",get_mainTextureScale_wrap,set_mainTextureScale_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"passCount",get_passCount_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"renderQueue",get_renderQueue_wrap,set_renderQueue_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"shaderKeywords",get_shaderKeywords_wrap,set_shaderKeywords_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"globalIlluminationFlags",get_globalIlluminationFlags_wrap,set_globalIlluminationFlags_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"enableInstancing",get_enableInstancing_wrap,set_enableInstancing_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"doubleSidedGI",get_doubleSidedGI_wrap,set_doubleSidedGI_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"HasProperty_StringA",HasProperty_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"HasProperty_int",HasProperty1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetTag_StringA_bool_StringA",GetTag_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetTag_StringA_bool",GetTag1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetOverrideTag",SetOverrideTag_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetShaderPassEnabled",SetShaderPassEnabled_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetShaderPassEnabled",GetShaderPassEnabled_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Lerp",Lerp_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetPass",SetPass_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetPassName",GetPassName_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"FindPass",FindPass_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CopyPropertiesFromMaterial",CopyPropertiesFromMaterial_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"EnableKeyword",EnableKeyword_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"DisableKeyword",DisableKeyword_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"IsKeywordEnabled",IsKeywordEnabled_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetFloat_StringA_float",SetFloat_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetFloat_int_float",SetFloat1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetInt_StringA_int",SetInt_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetInt_int_int",SetInt1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetColor_StringA_Color",SetColor_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetColor_int_Color",SetColor1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetVector_StringA_Vector4",SetVector_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetVector_int_Vector4",SetVector1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetMatrix_StringA_Matrix",SetMatrix_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetMatrix_int_Matrix",SetMatrix1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetTexture_StringA_Texture",SetTexture_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetTexture_int_Texture",SetTexture1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetBuffer_StringA_ComputeBuffer",SetBuffer_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetBuffer_int_ComputeBuffer",SetBuffer1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetTextureOffset_StringA_Vector2",SetTextureOffset_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetTextureOffset_int_Vector2",SetTextureOffset1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetTextureScale_StringA_Vector2",SetTextureScale_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetTextureScale_int_Vector2",SetTextureScale1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetFloatArray_StringA_List<float>",SetFloatArray_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetFloatArray_int_List<float>",SetFloatArray1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetColorArray_StringA_List<Color>",SetColorArray_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetColorArray_int_List<Color>",SetColorArray1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetVectorArray_StringA_List<Vector4>",SetVectorArray_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetVectorArray_int_List<Vector4>",SetVectorArray1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetMatrixArray_StringA_List<Matrix>",SetMatrixArray_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetMatrixArray_int_List<Matrix>",SetMatrixArray1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetFloat_StringA",GetFloat_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetFloat_int",GetFloat1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetInt_StringA",GetInt_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetInt_int",GetInt1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetColor_StringA",GetColor_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetColor_int",GetColor1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetVector_StringA",GetVector_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetVector_int",GetVector1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetMatrix_StringA",GetMatrix_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetMatrix_int",GetMatrix1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetFloatArray_StringA_List<float>",GetFloatArray_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetFloatArray_int_List<float>",GetFloatArray1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetFloatArray_StringA",GetFloatArray2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetFloatArray_int",GetFloatArray3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetVectorArray_StringA_List<Vector4>",GetVectorArray_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetVectorArray_int_List<Vector4>",GetVectorArray1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetColorArray_StringA",GetColorArray_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetColorArray_int",GetColorArray1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetColorArray_StringA_List<Color>",GetColorArray2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetColorArray_int_List<Color>",GetColorArray3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetVectorArray_StringA",GetVectorArray2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetVectorArray_int",GetVectorArray3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetMatrixArray_StringA_List<Matrix>",GetMatrixArray_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetMatrixArray_int_List<Matrix>",GetMatrixArray1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetMatrixArray_StringA",GetMatrixArray2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetMatrixArray_int",GetMatrixArray3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetTexture_StringA",GetTexture_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetTexture_int",GetTexture1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetTextureOffset_StringA",GetTextureOffset_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetTextureOffset_int",GetTextureOffset1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetTextureScale_StringA",GetTextureScale_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetTextureScale_int",GetTextureScale1_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new3(long L)
    {
        try
        {
            UnityEngine.Material arg0 = FCGetObj.GetObj<UnityEngine.Material>(FCLibHelper.fc_get_intptr(L,0));
            Material obj = new Material(arg0);
            long nPtr = FCGetObj.PushNewObj<Material>(obj);
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
            UnityEngine.Shader arg0 = FCGetObj.GetObj<UnityEngine.Shader>(FCLibHelper.fc_get_intptr(L,0));
            Material obj = new Material(arg0);
            long nPtr = FCGetObj.PushNewObj<Material>(obj);
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
        Material obj = FCGetObj.GetObj<Material>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        Material left  = FCGetObj.GetObj<Material>(L);
        Material right = FCGetObj.GetObj<Material>(R);
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
    public static int get_shader_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.shader);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_shader_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            UnityEngine.Shader arg0 = FCGetObj.GetObj<UnityEngine.Shader>(FCLibHelper.fc_get_intptr(L,0));
            ret.shader = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_color_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Color temp_ret = ret.color;
            FCLibHelper.fc_set_value_color(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_color_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            Color arg0 = new Color();
            FCLibHelper.fc_get_color(L,0,ref arg0);
            ret.color = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_mainTexture_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.mainTexture);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_mainTexture_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            UnityEngine.Texture arg0 = FCGetObj.GetObj<UnityEngine.Texture>(FCLibHelper.fc_get_intptr(L,0));
            ret.mainTexture = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_mainTextureOffset_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Vector2 temp_ret = ret.mainTextureOffset;
            FCLibHelper.fc_set_value_vector2(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_mainTextureOffset_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            Vector2 arg0 = new Vector2();
            FCLibHelper.fc_get_vector2(L,0,ref arg0);
            ret.mainTextureOffset = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_mainTextureScale_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Vector2 temp_ret = ret.mainTextureScale;
            FCLibHelper.fc_set_value_vector2(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_mainTextureScale_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            Vector2 arg0 = new Vector2();
            FCLibHelper.fc_get_vector2(L,0,ref arg0);
            ret.mainTextureScale = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_passCount_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret.passCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_renderQueue_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret.renderQueue);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_renderQueue_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            ret.renderQueue = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_shaderKeywords_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret.shaderKeywords,ret_ptr);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_shaderKeywords_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            string[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            ret.shaderKeywords = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_globalIlluminationFlags_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.globalIlluminationFlags);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_globalIlluminationFlags_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            UnityEngine.MaterialGlobalIlluminationFlags arg0 = (UnityEngine.MaterialGlobalIlluminationFlags)(FCLibHelper.fc_get_int(L,0));
            ret.globalIlluminationFlags = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_enableInstancing_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.enableInstancing);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_enableInstancing_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.enableInstancing = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_doubleSidedGI_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.doubleSidedGI);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_doubleSidedGI_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.doubleSidedGI = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int HasProperty_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool ret = obj.HasProperty(arg0);
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
    public static int HasProperty1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            bool ret = obj.HasProperty(arg0);
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
    public static int GetTag_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            string arg2 = FCLibHelper.fc_get_string_a(L,2);
            string ret = obj.GetTag(arg0,arg1,arg2);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_string_a(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetTag1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            string ret = obj.GetTag(arg0,arg1);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_string_a(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetOverrideTag_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            string arg1 = FCLibHelper.fc_get_string_a(L,1);
            obj.SetOverrideTag(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetShaderPassEnabled_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            obj.SetShaderPassEnabled(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetShaderPassEnabled_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool ret = obj.GetShaderPassEnabled(arg0);
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
    public static int Lerp_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            UnityEngine.Material arg0 = FCGetObj.GetObj<UnityEngine.Material>(FCLibHelper.fc_get_intptr(L,0));
            UnityEngine.Material arg1 = FCGetObj.GetObj<UnityEngine.Material>(FCLibHelper.fc_get_intptr(L,1));
            float arg2 = FCLibHelper.fc_get_float(L,2);
            obj.Lerp(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetPass_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            bool ret = obj.SetPass(arg0);
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
    public static int GetPassName_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            string ret = obj.GetPassName(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_string_a(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int FindPass_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            int ret = obj.FindPass(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CopyPropertiesFromMaterial_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            UnityEngine.Material arg0 = FCGetObj.GetObj<UnityEngine.Material>(FCLibHelper.fc_get_intptr(L,0));
            obj.CopyPropertiesFromMaterial(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int EnableKeyword_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.EnableKeyword(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int DisableKeyword_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.DisableKeyword(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int IsKeywordEnabled_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool ret = obj.IsKeywordEnabled(arg0);
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
    public static int SetFloat_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            obj.SetFloat(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetFloat1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            obj.SetFloat(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetInt_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            obj.SetInt(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetInt1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            obj.SetInt(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetColor_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Color arg1 = new Color();
            FCLibHelper.fc_get_color(L,1,ref arg1);
            obj.SetColor(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetColor1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Color arg1 = new Color();
            FCLibHelper.fc_get_color(L,1,ref arg1);
            obj.SetColor(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetVector_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Vector4 arg1 = new Vector4();
            FCLibHelper.fc_get_vector4(L,1,ref arg1);
            obj.SetVector(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetVector1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Vector4 arg1 = new Vector4();
            FCLibHelper.fc_get_vector4(L,1,ref arg1);
            obj.SetVector(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetMatrix_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Matrix4x4 arg1 = new Matrix4x4();
            FCLibHelper.fc_get_matrix(L,1,ref arg1);
            obj.SetMatrix(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetMatrix1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Matrix4x4 arg1 = new Matrix4x4();
            FCLibHelper.fc_get_matrix(L,1,ref arg1);
            obj.SetMatrix(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetTexture_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.Texture arg1 = FCGetObj.GetObj<UnityEngine.Texture>(FCLibHelper.fc_get_intptr(L,1));
            obj.SetTexture(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetTexture1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.Texture arg1 = FCGetObj.GetObj<UnityEngine.Texture>(FCLibHelper.fc_get_intptr(L,1));
            obj.SetTexture(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetBuffer_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.ComputeBuffer arg1 = FCGetObj.GetObj<UnityEngine.ComputeBuffer>(FCLibHelper.fc_get_intptr(L,1));
            obj.SetBuffer(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetBuffer1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.ComputeBuffer arg1 = FCGetObj.GetObj<UnityEngine.ComputeBuffer>(FCLibHelper.fc_get_intptr(L,1));
            obj.SetBuffer(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetTextureOffset_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Vector2 arg1 = new Vector2();
            FCLibHelper.fc_get_vector2(L,1,ref arg1);
            obj.SetTextureOffset(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetTextureOffset1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Vector2 arg1 = new Vector2();
            FCLibHelper.fc_get_vector2(L,1,ref arg1);
            obj.SetTextureOffset(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetTextureScale_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Vector2 arg1 = new Vector2();
            FCLibHelper.fc_get_vector2(L,1,ref arg1);
            obj.SetTextureScale(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetTextureScale1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Vector2 arg1 = new Vector2();
            FCLibHelper.fc_get_vector2(L,1,ref arg1);
            obj.SetTextureScale(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetFloatArray_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            List<float> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.SetFloatArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetFloatArray1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            List<float> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.SetFloatArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetColorArray_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            List<Color> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.SetColorArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetColorArray1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            List<Color> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.SetColorArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetVectorArray_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            List<Vector4> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.SetVectorArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetVectorArray1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            List<Vector4> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.SetVectorArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetMatrixArray_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            List<Matrix4x4> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.SetMatrixArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetMatrixArray1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            List<Matrix4x4> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.SetMatrixArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetFloat_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float ret = obj.GetFloat(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_float(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetFloat1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            float ret = obj.GetFloat(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_float(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetInt_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            int ret = obj.GetInt(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetInt1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int ret = obj.GetInt(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetColor_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Color ret = obj.GetColor(arg0);
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
    public static int GetColor1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Color ret = obj.GetColor(arg0);
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
    public static int GetVector_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Vector4 ret = obj.GetVector(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Vector4 temp_ret = ret;
            FCLibHelper.fc_set_value_vector4(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetVector1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Vector4 ret = obj.GetVector(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Vector4 temp_ret = ret;
            FCLibHelper.fc_set_value_vector4(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetMatrix_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Matrix4x4 ret = obj.GetMatrix(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Matrix4x4 temp_ret = ret;
            FCLibHelper.fc_set_value_matrix(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetMatrix1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Matrix4x4 ret = obj.GetMatrix(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Matrix4x4 temp_ret = ret;
            FCLibHelper.fc_set_value_matrix(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetFloatArray_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            List<float> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetFloatArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetFloatArray1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            List<float> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetFloatArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetFloatArray2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float[] ret = obj.GetFloatArray(arg0);
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
    public static int GetFloatArray3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            float[] ret = obj.GetFloatArray(arg0);
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
    public static int GetVectorArray_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            List<Vector4> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetVectorArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetVectorArray1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            List<Vector4> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetVectorArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetColorArray_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Color[] ret = obj.GetColorArray(arg0);
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
    public static int GetColorArray1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Color[] ret = obj.GetColorArray(arg0);
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
    public static int GetColorArray2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            List<Color> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetColorArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetColorArray3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            List<Color> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetColorArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetVectorArray2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Vector4[] ret = obj.GetVectorArray(arg0);
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
    public static int GetVectorArray3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Vector4[] ret = obj.GetVectorArray(arg0);
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
    public static int GetMatrixArray_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            List<Matrix4x4> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetMatrixArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetMatrixArray1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            List<Matrix4x4> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetMatrixArray(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetMatrixArray2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Matrix4x4[] ret = obj.GetMatrixArray(arg0);
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
    public static int GetMatrixArray3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Matrix4x4[] ret = obj.GetMatrixArray(arg0);
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
    public static int GetTexture_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Texture ret = obj.GetTexture(arg0);
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
    public static int GetTexture1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Texture ret = obj.GetTexture(arg0);
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
    public static int GetTextureOffset_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Vector2 ret = obj.GetTextureOffset(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Vector2 temp_ret = ret;
            FCLibHelper.fc_set_value_vector2(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetTextureOffset1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Vector2 ret = obj.GetTextureOffset(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Vector2 temp_ret = ret;
            FCLibHelper.fc_set_value_vector2(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetTextureScale_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Vector2 ret = obj.GetTextureScale(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Vector2 temp_ret = ret;
            FCLibHelper.fc_set_value_vector2(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetTextureScale1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Material obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            Vector2 ret = obj.GetTextureScale(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Vector2 temp_ret = ret;
            FCLibHelper.fc_set_value_vector2(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
