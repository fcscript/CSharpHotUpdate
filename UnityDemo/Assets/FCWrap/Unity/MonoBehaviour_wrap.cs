using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using System.Collections;

public class MonoBehaviour_wrap
{
    public static UnityEngine.MonoBehaviour get_obj(long L)
    {
        return FCGetObj.GetObj<UnityEngine.MonoBehaviour>(L);
    }

    public static void Register(long VM)
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id(VM, "MonoBehaviour");
        FCLibHelper.fc_register_class_new(VM, nClassName, obj_new);
        FCLibHelper.fc_register_class_del(VM, nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(VM, nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(VM, nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(VM, nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"useGUILayout",get_useGUILayout_wrap,set_useGUILayout_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"Invoke",Invoke_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"InvokeRepeating",InvokeRepeating_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"CancelInvoke",CancelInvoke_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"CancelInvoke_StringA",CancelInvoke1_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"IsInvoking_StringA",IsInvoking_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"IsInvoking",IsInvoking1_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"StartCoroutine_IEnumerator",StartCoroutine_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"StartCoroutine_StringA_Object",StartCoroutine1_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"StartCoroutine_StringA",StartCoroutine2_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"StopCoroutine_StringA",StopCoroutine_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"StopCoroutine_IEnumerator",StopCoroutine1_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"StopCoroutine_Coroutine",StopCoroutine2_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"StopAllCoroutines",StopAllCoroutines_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"print",print_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<UnityEngine.MonoBehaviour>();
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
        UnityEngine.MonoBehaviour obj = FCGetObj.GetObj<UnityEngine.MonoBehaviour>(nIntPtr);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UnityEngine.MonoBehaviour left  = FCGetObj.GetObj<UnityEngine.MonoBehaviour>(L);
        UnityEngine.MonoBehaviour right = FCGetObj.GetObj<UnityEngine.MonoBehaviour>(R);
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
    public static int get_useGUILayout_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.useGUILayout);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_useGUILayout_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.useGUILayout = arg0;
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
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            obj.Invoke(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int InvokeRepeating_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            float arg2 = FCLibHelper.fc_get_float(L,2);
            obj.InvokeRepeating(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CancelInvoke_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            obj.CancelInvoke();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CancelInvoke1_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.CancelInvoke(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int IsInvoking_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool ret = obj.IsInvoking(arg0);
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
    public static int IsInvoking1_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            bool ret = obj.IsInvoking();
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
    public static int StartCoroutine_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            System.Collections.IEnumerator arg0 = FCGetObj.GetObj<System.Collections.IEnumerator>(FCLibHelper.fc_get_wrap_objptr(L,0));
            UnityEngine.Coroutine ret = obj.StartCoroutine(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(VM, ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int StartCoroutine1_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            System.Object arg1 = FCGetObj.GetSystemObj(VM, FCLibHelper.fc_get_param_ptr(L,1));
            UnityEngine.Coroutine ret = obj.StartCoroutine(arg0,arg1);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(VM, ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int StartCoroutine2_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.Coroutine ret = obj.StartCoroutine(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(VM, ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int StopCoroutine_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.StopCoroutine(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int StopCoroutine1_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            System.Collections.IEnumerator arg0 = FCGetObj.GetObj<System.Collections.IEnumerator>(FCLibHelper.fc_get_wrap_objptr(L,0));
            obj.StopCoroutine(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int StopCoroutine2_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            UnityEngine.Coroutine arg0 = FCGetObj.GetObj<UnityEngine.Coroutine>(FCLibHelper.fc_get_wrap_objptr(L,0));
            obj.StopCoroutine(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int StopAllCoroutines_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.MonoBehaviour obj = get_obj(nThisPtr);
            obj.StopAllCoroutines();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int print_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            System.Object arg0 = FCGetObj.GetSystemObj(VM, FCLibHelper.fc_get_param_ptr(L,0));
            UnityEngine.MonoBehaviour.print(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
