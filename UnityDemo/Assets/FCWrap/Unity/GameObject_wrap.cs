using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using UnityEngine.SceneManagement;

public class GameObject_wrap
{
    public static GameObject get_obj(long L)
    {
        return FCGetObj.GetObj<GameObject>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("GameObject");
        FCLibHelper.fc_register_class_func(nClassName, "GameObject", obj_new3);
        FCLibHelper.fc_register_class_new(nClassName, obj_new);
        FCLibHelper.fc_register_class_func(nClassName, "GameObject", obj_new1);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(nClassName,"transform",get_transform_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"layer",get_layer_wrap,set_layer_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"activeSelf",get_activeSelf_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"activeInHierarchy",get_activeInHierarchy_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"isStatic",get_isStatic_wrap,set_isStatic_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"tag",get_tag_wrap,set_tag_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"scene",get_scene_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"gameObject",get_gameObject_wrap,null);
        FCLibHelper.fc_register_class_func(nClassName,"CreatePrimitive",CreatePrimitive_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponent_Type",GetComponent_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponent_StringA",GetComponent1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponentInChildren_Type_bool",GetComponentInChildren_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponentInChildren_Type",GetComponentInChildren1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponentInParent_Type",GetComponentInParent_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponents_Type",GetComponents_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponents_Type_List<Component>",GetComponents1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponentsInChildren_Type",GetComponentsInChildren_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponentsInChildren_Type_bool",GetComponentsInChildren1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponentsInParent_Type",GetComponentsInParent_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetComponentsInParent_Type_bool",GetComponentsInParent1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetActive",SetActive_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CompareTag",CompareTag_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"FindGameObjectWithTag",FindGameObjectWithTag_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"FindWithTag",FindWithTag_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"FindGameObjectsWithTag",FindGameObjectsWithTag_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SendMessageUpwards_StringA_Object_SendMessageOptions",SendMessageUpwards_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SendMessageUpwards_StringA_Object",SendMessageUpwards1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SendMessageUpwards_StringA",SendMessageUpwards2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SendMessageUpwards_StringA_SendMessageOptions",SendMessageUpwards3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SendMessage_StringA_Object_SendMessageOptions",SendMessage_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SendMessage_StringA_Object",SendMessage1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SendMessage_StringA",SendMessage2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SendMessage_StringA_SendMessageOptions",SendMessage3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"BroadcastMessage_StringA_Object_SendMessageOptions",BroadcastMessage_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"BroadcastMessage_StringA_Object",BroadcastMessage1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"BroadcastMessage_StringA",BroadcastMessage2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"BroadcastMessage_StringA_SendMessageOptions",BroadcastMessage3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"AddComponent_Type",AddComponent_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Find",Find_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new3(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            System.Type[] arg1 = null;
            arg1 = FCCustomParam.GetArray(ref arg1,L,1);
            GameObject obj = new GameObject(arg0,arg1);
            long nPtr = FCGetObj.PushNewObj<GameObject>(obj);
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
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<GameObject>();
        long ret = FCLibHelper.fc_get_return_ptr(L);
        FCLibHelper.fc_set_value_intptr(ret, nPtr);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new1(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            GameObject obj = new GameObject(arg0);
            long nPtr = FCGetObj.PushNewObj<GameObject>(obj);
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
        GameObject obj = FCGetObj.GetObj<GameObject>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        GameObject left  = FCGetObj.GetObj<GameObject>(L);
        GameObject right = FCGetObj.GetObj<GameObject>(R);
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
    public static int get_transform_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.transform);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_layer_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret.layer);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_layer_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            ret.layer = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_activeSelf_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.activeSelf);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_activeInHierarchy_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.activeInHierarchy);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_isStatic_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_bool(ret_ptr, ret.isStatic);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_isStatic_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            ret.isStatic = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_tag_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_string_a(ret_ptr, ret.tag);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_tag_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            ret.tag = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_scene_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.scene);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_gameObject_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.gameObject);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CreatePrimitive_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            UnityEngine.PrimitiveType arg0 = (UnityEngine.PrimitiveType)(FCLibHelper.fc_get_int(L,0));
            GameObject ret = GameObject.CreatePrimitive(arg0);
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
    public static int GetComponent_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            Component ret = obj.GetComponent(arg0);
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
    public static int GetComponent1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            Component ret = obj.GetComponent(arg0);
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
    public static int GetComponentInChildren_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            Component ret = obj.GetComponentInChildren(arg0,arg1);
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
    public static int GetComponentInChildren1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            Component ret = obj.GetComponentInChildren(arg0);
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
    public static int GetComponentInParent_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            Component ret = obj.GetComponentInParent(arg0);
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
    public static int GetComponents_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            Component[] ret = obj.GetComponents(arg0);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetComponents1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            List<UnityEngine.Component> arg1 = null;
            arg1 = FCCustomParam.GetList(ref arg1,L,1);
            obj.GetComponents(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetComponentsInChildren_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            Component[] ret = obj.GetComponentsInChildren(arg0);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetComponentsInChildren1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            Component[] ret = obj.GetComponentsInChildren(arg0,arg1);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetComponentsInParent_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            Component[] ret = obj.GetComponentsInParent(arg0);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetComponentsInParent1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            Component[] ret = obj.GetComponentsInParent(arg0,arg1);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetActive_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            bool arg0 = FCLibHelper.fc_get_bool(L,0);
            obj.SetActive(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CompareTag_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            bool ret = obj.CompareTag(arg0);
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
    public static int FindGameObjectWithTag_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            GameObject ret = GameObject.FindGameObjectWithTag(arg0);
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
    public static int FindWithTag_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            GameObject ret = GameObject.FindWithTag(arg0);
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
    public static int FindGameObjectsWithTag_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            GameObject[] ret = GameObject.FindGameObjectsWithTag(arg0);
            FCCustomParam.ReturnArray(ret,L);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SendMessageUpwards_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            System.Object arg1 = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr(L,1));
            UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)(FCLibHelper.fc_get_int(L,2));
            obj.SendMessageUpwards(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SendMessageUpwards1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            System.Object arg1 = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr(L,1));
            obj.SendMessageUpwards(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SendMessageUpwards2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.SendMessageUpwards(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SendMessageUpwards3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SendMessageOptions arg1 = (UnityEngine.SendMessageOptions)(FCLibHelper.fc_get_int(L,1));
            obj.SendMessageUpwards(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SendMessage_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            System.Object arg1 = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr(L,1));
            UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)(FCLibHelper.fc_get_int(L,2));
            obj.SendMessage(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SendMessage1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            System.Object arg1 = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr(L,1));
            obj.SendMessage(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SendMessage2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.SendMessage(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SendMessage3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SendMessageOptions arg1 = (UnityEngine.SendMessageOptions)(FCLibHelper.fc_get_int(L,1));
            obj.SendMessage(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int BroadcastMessage_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            System.Object arg1 = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr(L,1));
            UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)(FCLibHelper.fc_get_int(L,2));
            obj.BroadcastMessage(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int BroadcastMessage1_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            System.Object arg1 = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr(L,1));
            obj.BroadcastMessage(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int BroadcastMessage2_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            obj.BroadcastMessage(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int BroadcastMessage3_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SendMessageOptions arg1 = (UnityEngine.SendMessageOptions)(FCLibHelper.fc_get_int(L,1));
            obj.BroadcastMessage(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int AddComponent_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            Component ret = obj.AddComponent(arg0);
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
    public static int Find_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            GameObject obj = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            GameObject ret = GameObject.Find(arg0);
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
