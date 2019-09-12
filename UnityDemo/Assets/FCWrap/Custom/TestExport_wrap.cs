using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class TestExport_wrap
{
    public static TestExport get_obj(long L)
    {
        return FCGetObj.GetObj<TestExport>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("TestExport");
        FCLibHelper.fc_register_class_new(nClassName, obj_new);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(nClassName,"onPostRender",null,set_onPostRender_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"onOwnPostRender",null,set_onOwnPostRender_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"onCallFunc2",null,set_onCallFunc2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"AsnycLoad",AsnycLoad_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadResource",LoadResource_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<TestExport>();
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
        TestExport obj = FCGetObj.GetObj<TestExport>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        TestExport left  = FCGetObj.GetObj<TestExport>(L);
        TestExport right = FCGetObj.GetObj<TestExport>(R);
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
    public static int set_onPostRender_wrap(long L)
    {
        try
        {
            TestCallback_deletate func = FCDelegateMng.Instance.GetDelegate<TestCallback_deletate>(L);
            if(func != null)
                TestExport.onPostRender = func.CallFunc;
            else
                TestExport.onPostRender = null;
            FCDelegateMng.Instance.RecordDelegate(TestExport.onPostRender, func);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_onOwnPostRender_wrap(long L)
    {
        try
        {
            TestCallback_deletate func = FCDelegateMng.Instance.GetDelegate<TestCallback_deletate>(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            TestExport ret = get_obj(nThisPtr);
            if(func != null)
                ret.onOwnPostRender = func.CallFunc;
            else
                ret.onOwnPostRender = null;
            FCDelegateMng.Instance.RecordDelegate(ret.onOwnPostRender, func);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_onCallFunc2_wrap(long L)
    {
        try
        {
            TestCallback2_deletate func = FCDelegateMng.Instance.GetDelegate<TestCallback2_deletate>(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            TestExport ret = get_obj(nThisPtr);
            if(func != null)
                ret.onCallFunc2 = func.CallFunc;
            else
                ret.onCallFunc2 = null;
            FCDelegateMng.Instance.RecordDelegate(ret.onCallFunc2, func);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int AsnycLoad_wrap(long L)
    {
        try
        {
            long nPtr = FCLibHelper.fc_await(L);
            long nRetPtr = FCLibHelper.fc_get_return_ptr(L);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            AsnycLoad_bridge(nPtr, nRetPtr, arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    static async void AsnycLoad_bridge(long nPtr, long nRetPtr,string arg0)
    {
        try
        {
            int nRes = await TestExport.AsnycLoad(arg0);
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
    public static int LoadResource_wrap(long L)
    {
        try
        {
            long nPtr = FCLibHelper.fc_await(L);
            long nRetPtr = FCLibHelper.fc_get_return_ptr(L);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            LoadResource_bridge(nPtr, nRetPtr, arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    static async void LoadResource_bridge(long nPtr, long nRetPtr,string arg0)
    {
        try
        {
            int nRes = await TestExport.LoadResource(arg0);
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

}
