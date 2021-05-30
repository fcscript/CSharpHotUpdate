using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class UserClass_wrap
{
    public static UserClass get_obj(long L)
    {
        return FCGetObj.GetObj<UserClass>(L);
    }

    public static void Register(long VM)
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id(VM, "UserClass");
        FCLibHelper.fc_register_class_new(VM, nClassName, obj_new);
        FCLibHelper.fc_register_class_del(VM, nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(VM, nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(VM, nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(VM, nClassName,obj_equal);
        FCLibHelper.fc_register_class_func(VM, nClassName,"TestFunc1",TestFunc1_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"TestFunc2",TestFunc2_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"MandelbrotCheck",MandelbrotCheck_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"S2C_Test1",S2C_Test1_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"S2C_Test2",S2C_Test2_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"S2C_Test3",S2C_Test3_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"S2C_Test4",S2C_Test4_wrap);
        FCLibHelper.fc_register_class_func(VM, nClassName,"S2C_Test5",S2C_Test5_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<UserClass>();
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
        UserClass obj = FCGetObj.GetObj<UserClass>(nIntPtr);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UserClass left  = FCGetObj.GetObj<UserClass>(L);
        UserClass right = FCGetObj.GetObj<UserClass>(R);
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
    public static int TestFunc1_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            string arg1 = FCLibHelper.fc_get_string_a(L,1);
            UnityEngine.Transform arg2 = FCGetObj.GetObj<UnityEngine.Transform>(FCLibHelper.fc_get_wrap_objptr(L,2));
            UserClass.TestFunc1(arg0,arg1,arg2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int TestFunc2_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            string arg1 = FCLibHelper.fc_get_string_a(L,1);
            Vector3 arg2 = new Vector3();
            FCLibHelper.fc_get_vector3(L,2,ref arg2);
            UnityEngine.Transform arg3 = FCGetObj.GetObj<UnityEngine.Transform>(FCLibHelper.fc_get_wrap_objptr(L,3));
            UserClass.TestFunc2(arg0,arg1,arg2,arg3);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int MandelbrotCheck_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            float arg0 = FCLibHelper.fc_get_float(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            bool ret = UserClass.MandelbrotCheck(arg0,arg1);
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
    public static int S2C_Test1_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UserClass obj = get_obj(nThisPtr);
            obj.S2C_Test1();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int S2C_Test2_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UserClass obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            int arg1 = FCLibHelper.fc_get_int(L,1);
            int arg2 = FCLibHelper.fc_get_int(L,2);
            int arg3 = FCLibHelper.fc_get_int(L,3);
            int arg4 = FCLibHelper.fc_get_int(L,4);
            int ret = obj.S2C_Test2(arg0,arg1,arg2,arg3,arg4);
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
    public static int S2C_Test3_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UserClass obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            long arg1 = FCLibHelper.fc_get_int64(L,1);
            long arg2 = FCLibHelper.fc_get_int64(L,2);
            long arg3 = FCLibHelper.fc_get_int64(L,3);
            long arg4 = FCLibHelper.fc_get_int64(L,4);
            int ret = obj.S2C_Test3(arg0,arg1,arg2,arg3,arg4);
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
    public static int S2C_Test4_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UserClass obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            float arg1 = FCLibHelper.fc_get_float(L,1);
            float arg2 = FCLibHelper.fc_get_float(L,2);
            float arg3 = FCLibHelper.fc_get_float(L,3);
            float arg4 = FCLibHelper.fc_get_float(L,4);
            int ret = obj.S2C_Test4(arg0,arg1,arg2,arg3,arg4);
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
    public static int S2C_Test5_wrap(long L)
    {
        try
        {
            long VM = FCLibHelper.fc_get_vm_ptr(L);
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            UserClass obj = get_obj(nThisPtr);
            int arg0 = FCLibHelper.fc_get_int(L,0);
            string arg1 = FCLibHelper.fc_get_string_a(L,1);
            string arg2 = FCLibHelper.fc_get_string_a(L,2);
            string arg3 = FCLibHelper.fc_get_string_a(L,3);
            string arg4 = FCLibHelper.fc_get_string_a(L,4);
            int ret = obj.S2C_Test5(arg0,arg1,arg2,arg3,arg4);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, ret);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
