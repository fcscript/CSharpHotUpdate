using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class UnityObject_wrap
{
    public static UnityObject get_obj(long L)
    {
        return FCGetObj.GetObj<UnityObject>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("UnityObject");
        FCLibHelper.fc_register_class_new(nClassName, obj_new);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(nClassName,"name",get_name_wrap,set_name_wrap);
        FCLibHelper.fc_register_class_attrib(nClassName,"hideFlags",get_hideFlags_wrap,set_hideFlags_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Destroy_UnityObject_float",Destroy_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Destroy_UnityObject",Destroy1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"DestroyImmediate_UnityObject_bool",DestroyImmediate_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"DestroyImmediate_UnityObject",DestroyImmediate1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"FindObjectsOfType_Type",FindObjectsOfType_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"DontDestroyOnLoad",DontDestroyOnLoad_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"DestroyObject_UnityObject_float",DestroyObject_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"DestroyObject_UnityObject",DestroyObject1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"ToString",ToString_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetInstanceID",GetInstanceID_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetHashCode",GetHashCode_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Equals",Equals_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Instantiate_UnityObject_Vector3_Quaternion",Instantiate_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Instantiate_UnityObject_Vector3_Quaternion_Transform",Instantiate1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Instantiate_UnityObject",Instantiate2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Instantiate_UnityObject_Transform",Instantiate3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"Instantiate_UnityObject_Transform_bool",Instantiate4_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"FindObjectOfType_Type",FindObjectOfType_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<UnityObject>();
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
        UnityObject obj = FCGetObj.GetObj<UnityObject>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UnityObject left  = FCGetObj.GetObj<UnityObject>(L);
        UnityObject right = FCGetObj.GetObj<UnityObject>(R);
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
    public static int get_name_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_string_a(ret_ptr, ret.name);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_name_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityObject ret = get_obj(nThisPtr);
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            ret.name = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_hideFlags_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityObject ret = get_obj(nThisPtr);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret.hideFlags);
            FCLibHelper.fc_set_value_intptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int set_hideFlags_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityObject ret = get_obj(nThisPtr);
            UnityEngine.HideFlags arg0 = (UnityEngine.HideFlags)(FCLibHelper.fc_get_int(L,0));
            ret.hideFlags = arg0;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Destroy_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            float arg1 = FCLibHelper.fc_get_float(L,1);
            UnityObject.Destroy(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Destroy1_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            UnityObject.Destroy(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int DestroyImmediate_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            bool arg1 = FCLibHelper.fc_get_bool(L,1);
            UnityObject.DestroyImmediate(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int DestroyImmediate1_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            UnityObject.DestroyImmediate(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int FindObjectsOfType_wrap(long L)
    {
        try
        {
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            UnityObject[] ret = UnityObject.FindObjectsOfType(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCCustomParam.ReturnArray(ret,ret_ptr);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int DontDestroyOnLoad_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            UnityObject.DontDestroyOnLoad(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int DestroyObject_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            float arg1 = FCLibHelper.fc_get_float(L,1);
            UnityObject.DestroyObject(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int DestroyObject1_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            UnityObject.DestroyObject(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int ToString_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityObject obj = get_obj(nThisPtr);
            string ret = obj.ToString();
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_string_a(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetInstanceID_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityObject obj = get_obj(nThisPtr);
            int ret = obj.GetInstanceID();
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
    public static int GetHashCode_wrap(long L)
    {
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityObject obj = get_obj(nThisPtr);
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
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UnityObject obj = get_obj(nThisPtr);
            System.Object arg0 = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr(L,0));
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

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int Instantiate_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            Vector3 arg1 = new Vector3();
            FCLibHelper.fc_get_vector3(L,1,ref arg1);
            Quaternion arg2 = new Quaternion();
            FCLibHelper.fc_get_quaternion(L,2,ref arg2);
            UnityObject ret = UnityObject.Instantiate(arg0,arg1,arg2);
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
    public static int Instantiate1_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            Vector3 arg1 = new Vector3();
            FCLibHelper.fc_get_vector3(L,1,ref arg1);
            Quaternion arg2 = new Quaternion();
            FCLibHelper.fc_get_quaternion(L,2,ref arg2);
            UnityEngine.Transform arg3 = FCGetObj.GetObj<UnityEngine.Transform>(FCLibHelper.fc_get_intptr(L,3));
            UnityObject ret = UnityObject.Instantiate(arg0,arg1,arg2,arg3);
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
    public static int Instantiate2_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            UnityObject ret = UnityObject.Instantiate(arg0);
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
    public static int Instantiate3_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            UnityEngine.Transform arg1 = FCGetObj.GetObj<UnityEngine.Transform>(FCLibHelper.fc_get_intptr(L,1));
            UnityObject ret = UnityObject.Instantiate(arg0,arg1);
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
    public static int Instantiate4_wrap(long L)
    {
        try
        {
            UnityObject arg0 = FCGetObj.GetObj<UnityObject>(FCLibHelper.fc_get_intptr(L,0));
            UnityEngine.Transform arg1 = FCGetObj.GetObj<UnityEngine.Transform>(FCLibHelper.fc_get_intptr(L,1));
            bool arg2 = FCLibHelper.fc_get_bool(L,2);
            UnityObject ret = UnityObject.Instantiate(arg0,arg1,arg2);
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
    public static int FindObjectOfType_wrap(long L)
    {
        try
        {
            System.Type arg0 = FCGetObj.GetObj<System.Type>(FCLibHelper.fc_get_intptr(L,0));
            UnityObject ret = UnityObject.FindObjectOfType(arg0);
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
