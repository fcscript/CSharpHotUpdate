using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using System.Collections;

public class Animation_wrap
{
    public static Animation get_obj(long L)
    {
        return FCGetObj.GetObj<Animation>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("Animation");
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(nClassName,"clip",get_clip_wrap,set_clip_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"playAutomatically",get_playAutomatically_wrap,set_playAutomatically_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"wrapMode",get_wrapMode_wrap,set_wrapMode_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"isPlaying",get_isPlaying_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"animatePhysics",get_animatePhysics_wrap,set_animatePhysics_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"cullingType",get_cullingType_wrap,set_cullingType_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"localBounds",get_localBounds_wrap,set_localBounds_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Stop",Stop_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Stop_StringA",Stop1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Rewind_StringA",Rewind_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Rewind",Rewind1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Sample",Sample_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"IsPlaying",IsPlaying_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Play",Play_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Play_PlayMode",Play1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Play_StringA_PlayMode",Play2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Play_StringA",Play3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CrossFade_StringA_float_PlayMode",CrossFade_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CrossFade_StringA_float",CrossFade1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CrossFade_StringA",CrossFade2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Blend_StringA_float_float",Blend_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Blend_StringA_float",Blend1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Blend_StringA",Blend2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CrossFadeQueued_StringA_float_QueueMode_PlayMode",CrossFadeQueued_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CrossFadeQueued_StringA_float_QueueMode",CrossFadeQueued1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CrossFadeQueued_StringA_float",CrossFadeQueued2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CrossFadeQueued_StringA",CrossFadeQueued3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"PlayQueued_StringA_QueueMode_PlayMode",PlayQueued_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"PlayQueued_StringA_QueueMode",PlayQueued1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"PlayQueued_StringA",PlayQueued2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"AddClip_AnimationClip_StringA",AddClip_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"AddClip_AnimationClip_StringA_int_int_bool",AddClip1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"AddClip_AnimationClip_StringA_int_int",AddClip2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"RemoveClip_AnimationClip",RemoveClip_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"RemoveClip_StringA",RemoveClip1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetClipCount",GetClipCount_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SyncLayer",SyncLayer_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetEnumerator",GetEnumerator_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetClip",GetClip_wrap);
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
        Animation obj = FCGetObj.GetObj<Animation>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        Animation left  = FCGetObj.GetObj<Animation>(L);
        Animation right = FCGetObj.GetObj<Animation>(R);
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
    public static int get_clip_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.clip);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_clip_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            UnityEngine.AnimationClip arg0 = FCGetObj.GetObj<UnityEngine.AnimationClip>(FCLibHelper.fc_get_intptr(L,0));
            ret.clip = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_playAutomatically_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.playAutomatically);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_playAutomatically_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.playAutomatically = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_wrapMode_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.wrapMode);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_wrapMode_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            UnityEngine.WrapMode arg0 = (UnityEngine.WrapMode)(FCLibHelper.fc_get_int(L,0));
            ret.wrapMode = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_isPlaying_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.isPlaying);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_animatePhysics_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.animatePhysics);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_animatePhysics_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.animatePhysics = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_cullingType_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.cullingType);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_cullingType_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation ret = get_obj(nThisPtr);
            UnityEngine.AnimationCullingType arg0 = (UnityEngine.AnimationCullingType)(FCLibHelper.fc_get_int(L,0));
            ret.cullingType = arg0;
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
            Animation ret = get_obj(nThisPtr);
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
            Animation ret = get_obj(nThisPtr);
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
    public static int Stop_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            obj.Stop();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Stop1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.Stop(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Rewind_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.Rewind(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Rewind1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            obj.Rewind();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Sample_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            obj.Sample();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int IsPlaying_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool ret = obj.IsPlaying(arg0);
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
    public static int Play_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            bool ret = obj.Play();
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
    public static int Play1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            UnityEngine.PlayMode arg0 = (UnityEngine.PlayMode)(FCLibHelper.fc_get_int(L,0));
            bool ret = obj.Play(arg0);
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
    public static int Play2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.PlayMode arg1 = (UnityEngine.PlayMode)(FCLibHelper.fc_get_int(L,1));
            bool ret = obj.Play(arg0,arg1);
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
    public static int Play3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool ret = obj.Play(arg0);
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
    public static int CrossFade_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            UnityEngine.PlayMode arg2 = (UnityEngine.PlayMode)(FCLibHelper.fc_get_int(L,2));
            obj.CrossFade(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CrossFade1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            obj.CrossFade(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CrossFade2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.CrossFade(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Blend_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            float arg2 = FCLibHelper.fc_get_float(L,2);
            obj.Blend(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Blend1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            obj.Blend(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Blend2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.Blend(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CrossFadeQueued_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            UnityEngine.QueueMode arg2 = (UnityEngine.QueueMode)(FCLibHelper.fc_get_int(L,2));
            UnityEngine.PlayMode arg3 = (UnityEngine.PlayMode)(FCLibHelper.fc_get_int(L,3));
            AnimationState ret = obj.CrossFadeQueued(arg0,arg1,arg2,arg3);
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
    public static int CrossFadeQueued1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            UnityEngine.QueueMode arg2 = (UnityEngine.QueueMode)(FCLibHelper.fc_get_int(L,2));
            AnimationState ret = obj.CrossFadeQueued(arg0,arg1,arg2);
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
    public static int CrossFadeQueued2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            AnimationState ret = obj.CrossFadeQueued(arg0,arg1);
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
    public static int CrossFadeQueued3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            AnimationState ret = obj.CrossFadeQueued(arg0);
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
    public static int PlayQueued_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.QueueMode arg1 = (UnityEngine.QueueMode)(FCLibHelper.fc_get_int(L,1));
            UnityEngine.PlayMode arg2 = (UnityEngine.PlayMode)(FCLibHelper.fc_get_int(L,2));
            AnimationState ret = obj.PlayQueued(arg0,arg1,arg2);
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
    public static int PlayQueued1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.QueueMode arg1 = (UnityEngine.QueueMode)(FCLibHelper.fc_get_int(L,1));
            AnimationState ret = obj.PlayQueued(arg0,arg1);
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
    public static int PlayQueued2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            AnimationState ret = obj.PlayQueued(arg0);
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
    public static int AddClip_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            UnityEngine.AnimationClip arg0 = FCGetObj.GetObj<UnityEngine.AnimationClip>(FCLibHelper.fc_get_intptr(L,0));
            string arg1 = FCLibHelper.fc_get_string_a(L,1);
            obj.AddClip(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int AddClip1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            UnityEngine.AnimationClip arg0 = FCGetObj.GetObj<UnityEngine.AnimationClip>(FCLibHelper.fc_get_intptr(L,0));
            string arg1 = FCLibHelper.fc_get_string_a(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            bool arg4 = FCLibHelper.fc_get_bool(L,4);
            obj.AddClip(arg0,arg1,arg2,arg3,arg4);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int AddClip2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            UnityEngine.AnimationClip arg0 = FCGetObj.GetObj<UnityEngine.AnimationClip>(FCLibHelper.fc_get_intptr(L,0));
            string arg1 = FCLibHelper.fc_get_string_a(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            obj.AddClip(arg0,arg1,arg2,arg3);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int RemoveClip_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            UnityEngine.AnimationClip arg0 = FCGetObj.GetObj<UnityEngine.AnimationClip>(FCLibHelper.fc_get_intptr(L,0));
            obj.RemoveClip(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int RemoveClip1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.RemoveClip(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetClipCount_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            int ret = obj.GetClipCount();
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
    public static int SyncLayer_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            obj.SyncLayer(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetEnumerator_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            IEnumerator ret = obj.GetEnumerator();
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
    public static int GetClip_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            Animation obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            AnimationClip ret = obj.GetClip(arg0);
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

}
