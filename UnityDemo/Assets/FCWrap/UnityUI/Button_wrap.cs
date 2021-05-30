using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_wrap
{
    public static UnityEngine.UI.Button get_obj(long L)
    {
        return FCGetObj.GetObj<UnityEngine.UI.Button>(L);
    }

    public static void Register(long VM)
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id(VM, "Button");
        FCLibHelper.fc_register_class_del(VM, nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(VM, nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(VM, nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(VM, nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"onClick",get_onClick_wrap,set_onClick_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"OnPointerClick",OnPointerClick_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"OnSubmit",OnSubmit_wrap);
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
        UnityEngine.UI.Button obj = FCGetObj.GetObj<UnityEngine.UI.Button>(nIntPtr);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UnityEngine.UI.Button left  = FCGetObj.GetObj<UnityEngine.UI.Button>(L);
        UnityEngine.UI.Button right = FCGetObj.GetObj<UnityEngine.UI.Button>(R);
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
    public static int get_onClick_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.UI.Button ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long v = FCGetObj.PushObj(ret.onClick);
            FCLibHelper.fc_set_value_wrap_objptr(VM, ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_onClick_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.UI.Button ret = get_obj(nThisPtr);
            UnityEngine.UI.Button.ButtonClickedEvent arg0 = FCGetObj.GetObj<UnityEngine.UI.Button.ButtonClickedEvent>(FCLibHelper.fc_get_wrap_objptr(L,0));
            ret.onClick = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int OnPointerClick_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.UI.Button obj = get_obj(nThisPtr);
            UnityEngine.EventSystems.PointerEventData arg0 = FCGetObj.GetObj<UnityEngine.EventSystems.PointerEventData>(FCLibHelper.fc_get_wrap_objptr(L,0));
            obj.OnPointerClick(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int OnSubmit_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.UI.Button obj = get_obj(nThisPtr);
            UnityEngine.EventSystems.BaseEventData arg0 = FCGetObj.GetObj<UnityEngine.EventSystems.BaseEventData>(FCLibHelper.fc_get_wrap_objptr(L,0));
            obj.OnSubmit(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
