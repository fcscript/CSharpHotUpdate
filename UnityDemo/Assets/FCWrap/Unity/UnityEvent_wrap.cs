using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using UnityEngine.Events;

public class UnityEvent_wrap
{
    public static UnityEngine.Events.UnityEvent get_obj(long L)
    {
        return FCGetObj.GetObj<UnityEngine.Events.UnityEvent>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("UnityEvent");
        FCLibHelper.fc_register_class_new(nClassName, obj_new);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_func(nClassName,"AddListener",AddListener_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"RemoveListener",RemoveListener_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Invoke",Invoke_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<UnityEngine.Events.UnityEvent>();
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
        UnityEngine.Events.UnityEvent obj = FCGetObj.GetObj<UnityEngine.Events.UnityEvent>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UnityEngine.Events.UnityEvent left  = FCGetObj.GetObj<UnityEngine.Events.UnityEvent>(L);
        UnityEngine.Events.UnityEvent right = FCGetObj.GetObj<UnityEngine.Events.UnityEvent>(R);
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
    public static int AddListener_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.Events.UnityEvent obj = get_obj(nThisPtr);
            UnityAction_delegate func0 = FCDelegateMng.Instance.GetDelegate<UnityAction_delegate>(L,0);
            UnityEngine.Events.UnityAction arg0 = null;
            if(func0 != null)
                arg0 = func0.CallFunc;
            // 尽量不要在函数参数中传递委托指针，这个无法自动托管，要尽可能是使用get, set属性方法
            // 如果在参数中传递了委托指针，请在对应的函数中调用FCDelegateMng.Instance.RecordDelegate(delegate_func, func);
            obj.AddListener(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int RemoveListener_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.Events.UnityEvent obj = get_obj(nThisPtr);
            UnityAction_delegate func0 = FCDelegateMng.Instance.GetDelegate<UnityAction_delegate>(L,0);
            UnityEngine.Events.UnityAction arg0 = null;
            if(func0 != null)
                arg0 = func0.CallFunc;
            // 尽量不要在函数参数中传递委托指针，这个无法自动托管，要尽可能是使用get, set属性方法
            // 如果在参数中传递了委托指针，请在对应的函数中调用FCDelegateMng.Instance.RecordDelegate(delegate_func, func);
            obj.RemoveListener(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Invoke_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.Events.UnityEvent obj = get_obj(nThisPtr);
            obj.Invoke();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
