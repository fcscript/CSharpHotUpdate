using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using UnityEngine.SceneManagement;

public class Scene_wrap
{
    public static UnityEngine.SceneManagement.Scene get_obj(long L)
    {
        return FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(L);
    }

    public static void Register(long VM)
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id(VM, "Scene");
        FCLibHelper.fc_register_class_del(VM, nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(VM, nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(VM, nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(VM, nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"path",get_path_wrap,null);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"name",get_name_wrap,null);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"isLoaded",get_isLoaded_wrap,null);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"buildIndex",get_buildIndex_wrap,null);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"isDirty",get_isDirty_wrap,null);
        FCLibHelper.fc_register_class_attrib(VM, nClassName,"rootCount",get_rootCount_wrap,null);
        FCLibHelper.fc_register_class_func(VM, nClassName,"IsValid",IsValid_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"GetRootGameObjects",GetRootGameObjects_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"GetRootGameObjects_List<GameObject>",GetRootGameObjects1_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"GetHashCode",GetHashCode_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"Equals",Equals_wrap);
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
        UnityEngine.SceneManagement.Scene obj = FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(nIntPtr);
        return obj.GetHashCode();
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UnityEngine.SceneManagement.Scene left  = FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(L);
        UnityEngine.SceneManagement.Scene right = FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(R);
        return left.Equals(right);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_path_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_string_a(ret_ptr, ret.path);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_name_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_string_a(ret_ptr, ret.name);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_isLoaded_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.isLoaded);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_buildIndex_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret.buildIndex);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_isDirty_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.isDirty);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_rootCount_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret.rootCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int IsValid_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene obj = get_obj(nThisPtr);
            bool ret = obj.IsValid();
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
    public static int GetRootGameObjects_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene obj = get_obj(nThisPtr);
            UnityEngine.GameObject[] ret = obj.GetRootGameObjects();
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(VM, ret,ret_ptr);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetRootGameObjects1_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene obj = get_obj(nThisPtr);
            List<UnityEngine.GameObject> arg0 = null;
            arg0 = FCCustomParam.GetList(ref arg0,L,0);
            obj.GetRootGameObjects(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetHashCode_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene obj = get_obj(nThisPtr);
            int ret = obj.GetHashCode();
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
    public static int Equals_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityEngine.SceneManagement.Scene obj = get_obj(nThisPtr);
            System.Object arg0 = FCGetObj.GetSystemObj(VM, FCLibHelper.fc_get_param_ptr(L,0));
            bool ret = obj.Equals(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
