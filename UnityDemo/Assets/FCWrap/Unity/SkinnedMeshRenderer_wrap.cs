using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class SkinnedMeshRenderer_wrap
{
    public static SkinnedMeshRenderer get_obj(long L)
    {
        return FCGetObj.GetObj<SkinnedMeshRenderer>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("SkinnedMeshRenderer");
        FCLibHelper.fc_register_class_new(nClassName, obj_new);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(nClassName,"bones",get_bones_wrap,set_bones_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"quality",get_quality_wrap,set_quality_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"updateWhenOffscreen",get_updateWhenOffscreen_wrap,set_updateWhenOffscreen_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"rootBone",get_rootBone_wrap,set_rootBone_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"sharedMesh",get_sharedMesh_wrap,set_sharedMesh_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"skinnedMotionVectors",get_skinnedMotionVectors_wrap,set_skinnedMotionVectors_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"localBounds",get_localBounds_wrap,set_localBounds_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetBlendShapeWeight",GetBlendShapeWeight_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetBlendShapeWeight",SetBlendShapeWeight_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"BakeMesh",BakeMesh_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<SkinnedMeshRenderer>();
        long ret = FCLibHelper.fc_get_return_ptr(L);
        FCLibHelper.fc_set_value_intptr(ret, nPtr);
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
        SkinnedMeshRenderer obj = FCGetObj.GetObj<SkinnedMeshRenderer>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        SkinnedMeshRenderer left  = FCGetObj.GetObj<SkinnedMeshRenderer>(L);
        SkinnedMeshRenderer right = FCGetObj.GetObj<SkinnedMeshRenderer>(R);
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
    public static int get_bones_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            FCCustomParam.ReturnArray(ret.bones,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_bones_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            UnityEngine.Transform[] arg0 = null;
            arg0 = FCCustomParam.GetArray(ref arg0,L,0);
            ret.bones = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_quality_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.quality);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_quality_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            UnityEngine.SkinQuality arg0 = (UnityEngine.SkinQuality)(FCLibHelper.fc_get_int(L,0));
            ret.quality = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_updateWhenOffscreen_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.updateWhenOffscreen);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_updateWhenOffscreen_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.updateWhenOffscreen = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_rootBone_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.rootBone);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_rootBone_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            UnityEngine.Transform arg0 = FCGetObj.GetObj<UnityEngine.Transform>(FCLibHelper.fc_get_intptr(L,0));
            ret.rootBone = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_sharedMesh_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.sharedMesh);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_sharedMesh_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            UnityEngine.Mesh arg0 = FCGetObj.GetObj<UnityEngine.Mesh>(FCLibHelper.fc_get_intptr(L,0));
            ret.sharedMesh = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_skinnedMotionVectors_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.skinnedMotionVectors);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_skinnedMotionVectors_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.skinnedMotionVectors = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_localBounds_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            Bounds temp_ret = ret.localBounds;
            FCLibHelper.fc_set_value_bounds(ret_ptr, ref temp_ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_localBounds_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer ret = get_obj(nThisPtr);
            Bounds arg0 = new Bounds();
            FCLibHelper.fc_get_bounds(L,0,ref arg0);
            ret.localBounds = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetBlendShapeWeight_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            float ret = obj.GetBlendShapeWeight(arg0);
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
    public static int SetBlendShapeWeight_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            obj.SetBlendShapeWeight(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int BakeMesh_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            SkinnedMeshRenderer obj = get_obj(nThisPtr);
            UnityEngine.Mesh arg0 = FCGetObj.GetObj<UnityEngine.Mesh>(FCLibHelper.fc_get_intptr(L,0));
            obj.BakeMesh(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
