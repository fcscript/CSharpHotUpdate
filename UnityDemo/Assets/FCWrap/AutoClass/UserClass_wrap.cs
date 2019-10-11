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

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("UserClass");
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_func(nClassName,"TestFunc1",TestFunc1_wrap);
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
        UserClass obj = FCGetObj.GetObj<UserClass>(L);
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
            int arg0 = FCLibHelper.fc_get_int(L,0);
            string arg1 = FCLibHelper.fc_get_string_a(L,1);
            Vector3 arg2 = new Vector3();
            FCLibHelper.fc_get_vector3(L,2,ref arg2);
            UnityEngine.Transform arg3 = FCGetObj.GetObj<UnityEngine.Transform>(FCLibHelper.fc_get_intptr(L,3));
            UserClass.TestFunc1(arg0,arg1,arg2,arg3);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
