using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class TestD_wrap
{
    public static TestD get_obj(long L)
    {
        return FCGetObj.GetObj<TestD>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("TestD");
        FCLibHelper.fc_register_class_new(nClassName, obj_new);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_func(nClassName,"SetValue",SetValue_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadPrefab",LoadPrefab_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadPrefabObj",LoadPrefabObj_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<TestD>();
        long ret = FCLibHelper.fc_get_return_ptr(L);
        FCLibHelper.fc_set_value_wrap_objptr(ret, nPtr);
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
        TestD obj = FCGetObj.GetObj<TestD>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        TestD left  = FCGetObj.GetObj<TestD>(L);
        TestD right = FCGetObj.GetObj<TestD>(R);
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
    public static int SetValue_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            TestD obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int ret = obj.SetValue(arg0);
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
    public static int LoadPrefab_wrap(long L)
    {
        try
        {
            long nPtr = FCLibHelper.fc_await(L);
            long nRetPtr = FCLibHelper.fc_get_return_ptr(L);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            LoadPrefab_bridge(nPtr, nRetPtr, arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    static async void LoadPrefab_bridge(long nPtr, long nRetPtr,string arg0)
    {
        try
        {
            int nRes = await TestD.LoadPrefab(arg0);
            if(FCLibHelper.fc_is_valid_await(nPtr))
            {
                // 设置返回值
                FCLibHelper.fc_set_value_int(nRetPtr, nRes);
                FCLibHelper.fc_continue(nPtr); // 唤醒脚本
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadPrefabObj_wrap(long L)
    {
        try
        {
            long nPtr = FCLibHelper.fc_await(L);
            long nRetPtr = FCLibHelper.fc_get_return_ptr(L);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            LoadPrefabObj_bridge(nPtr, nRetPtr, arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    static async void LoadPrefabObj_bridge(long nPtr, long nRetPtr,string arg0)
    {
        try
        {
            GameObject nRes = await TestD.LoadPrefabObj(arg0);
            if(FCLibHelper.fc_is_valid_await(nPtr))
            {
                // 设置返回值
                long v = FCGetObj.PushObj(nRes);
                FCLibHelper.fc_set_value_wrap_objptr(nRetPtr, v);
                FCLibHelper.fc_continue(nPtr); // 唤醒脚本
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

}
