using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class AsyncOperation_wrap
{
    public static UnityEngine.AsyncOperation get_obj(long L)
    {
        return FCGetObj.GetObj<UnityEngine.AsyncOperation>(L);
    }

    public static void Register(long VM)
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id(VM, "AsyncOperation");
        FCLibHelper.fc_register_class_new(VM, nClassName, obj_new);
        FCLibHelper.fc_register_class_del(VM, nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(VM, nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(VM, nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(VM, nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"isDone",get_isDone_wrap,null);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"progress",get_progress_wrap,null);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"priority",get_priority_wrap,set_priority_wrap);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"allowSceneActivation",get_allowSceneActivation_wrap,set_allowSceneActivation_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<UnityEngine.AsyncOperation>();
        long ret = FCLibHelper.fc_get_return_ptr(L);
        long VM = FCLibHelper.fc_get_vm_ptr(L);
        FCLibHelper.fc_set_value_wrap_objptr(VM, ret, nPtr);
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
    public static int  obj_hash(long nIntPtr)
    {
        UnityEngine.AsyncOperation obj = FCGetObj.GetObj<UnityEngine.AsyncOperation>(nIntPtr);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UnityEngine.AsyncOperation left  = FCGetObj.GetObj<UnityEngine.AsyncOperation>(L);
        UnityEngine.AsyncOperation right = FCGetObj.GetObj<UnityEngine.AsyncOperation>(R);
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
    public static int get_isDone_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.AsyncOperation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.isDone);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_progress_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.AsyncOperation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_float(ret_ptr, ret.progress);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_priority_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.AsyncOperation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret.priority);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_priority_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.AsyncOperation ret = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            ret.priority = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_allowSceneActivation_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.AsyncOperation ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.allowSceneActivation);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_allowSceneActivation_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.AsyncOperation ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.allowSceneActivation = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
