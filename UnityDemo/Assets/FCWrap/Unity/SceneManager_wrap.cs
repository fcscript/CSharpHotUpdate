using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using UnityEngine.SceneManagement;

public class SceneManager_wrap
{
    public static UnityEngine.SceneManagement.SceneManager get_obj(long L)
    {
        return FCGetObj.GetObj<UnityEngine.SceneManagement.SceneManager>(L);
    }

    public static void Register()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id("SceneManager");
        FCLibHelper.fc_register_class_new(nClassName, obj_new);
        FCLibHelper.fc_register_class_del(nClassName,obj_del);
        FCLibHelper.fc_register_class_release_ref(nClassName,obj_release);
        FCLibHelper.fc_register_class_hash(nClassName,obj_hash);
        FCLibHelper.fc_register_class_equal(nClassName,obj_equal);
        FCLibHelper.fc_register_class_attrib(nClassName,"sceneCount",get_sceneCount_wrap,null);
        FCLibHelper.fc_register_class_attrib(nClassName,"sceneCountInBuildSettings",get_sceneCountInBuildSettings_wrap,null);
        FCLibHelper.fc_register_class_func(nClassName,"GetActiveScene",GetActiveScene_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"SetActiveScene",SetActiveScene_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetSceneByPath",GetSceneByPath_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetSceneByName",GetSceneByName_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetSceneByBuildIndex",GetSceneByBuildIndex_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"GetSceneAt",GetSceneAt_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadScene_StringA",LoadScene_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadScene_StringA_LoadSceneMode",LoadScene1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadScene_int",LoadScene2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadScene_int_LoadSceneMode",LoadScene3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadSceneAsync_StringA",LoadSceneAsync_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadSceneAsync_StringA_LoadSceneMode",LoadSceneAsync1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadSceneAsync_int",LoadSceneAsync2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"LoadSceneAsync_int_LoadSceneMode",LoadSceneAsync3_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"CreateScene",CreateScene_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"UnloadSceneAsync_int",UnloadSceneAsync_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"UnloadSceneAsync_StringA",UnloadSceneAsync1_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"UnloadSceneAsync_Scene",UnloadSceneAsync2_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"MergeScenes",MergeScenes_wrap);
        FCLibHelper.fc_register_class_func(nClassName,"MoveGameObjectToScene",MoveGameObjectToScene_wrap);
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int  obj_new(long L)
    {
        long nPtr = FCGetObj.NewObj<UnityEngine.SceneManagement.SceneManager>();
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
        UnityEngine.SceneManagement.SceneManager obj = FCGetObj.GetObj<UnityEngine.SceneManagement.SceneManager>(L);
        if(obj != null)
        {
            return obj.GetHashCode();
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_equal))]
    public static bool  obj_equal(long L, long R)
    {
        UnityEngine.SceneManagement.SceneManager left  = FCGetObj.GetObj<UnityEngine.SceneManagement.SceneManager>(L);
        UnityEngine.SceneManagement.SceneManager right = FCGetObj.GetObj<UnityEngine.SceneManagement.SceneManager>(R);
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
    public static int get_sceneCount_wrap(long L)
    {
        try
        {
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, UnityEngine.SceneManagement.SceneManager.sceneCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int get_sceneCountInBuildSettings_wrap(long L)
    {
        try
        {
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            FCLibHelper.fc_set_value_int(ret_ptr, UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetActiveScene_wrap(long L)
    {
        try
        {
            UnityEngine.SceneManagement.Scene ret = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int SetActiveScene_wrap(long L)
    {
        try
        {
            UnityEngine.SceneManagement.Scene arg0 = FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(FCLibHelper.fc_get_wrap_objptr(L,0));
            bool ret = UnityEngine.SceneManagement.SceneManager.SetActiveScene(arg0);
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
    public static int GetSceneByPath_wrap(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SceneManagement.Scene ret = UnityEngine.SceneManagement.SceneManager.GetSceneByPath(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetSceneByName_wrap(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SceneManagement.Scene ret = UnityEngine.SceneManagement.SceneManager.GetSceneByName(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetSceneByBuildIndex_wrap(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.SceneManagement.Scene ret = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetSceneAt_wrap(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.SceneManagement.Scene ret = UnityEngine.SceneManagement.SceneManager.GetSceneAt(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadScene_wrap(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SceneManagement.SceneManager.LoadScene(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadScene1_wrap(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SceneManagement.LoadSceneMode arg1 = (UnityEngine.SceneManagement.LoadSceneMode)(FCLibHelper.fc_get_int(L,1));
            UnityEngine.SceneManagement.SceneManager.LoadScene(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadScene2_wrap(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.SceneManagement.SceneManager.LoadScene(arg0);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadScene3_wrap(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.SceneManagement.LoadSceneMode arg1 = (UnityEngine.SceneManagement.LoadSceneMode)(FCLibHelper.fc_get_int(L,1));
            UnityEngine.SceneManagement.SceneManager.LoadScene(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadSceneAsync_wrap(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.AsyncOperation ret = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadSceneAsync1_wrap(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SceneManagement.LoadSceneMode arg1 = (UnityEngine.SceneManagement.LoadSceneMode)(FCLibHelper.fc_get_int(L,1));
            UnityEngine.AsyncOperation ret = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(arg0,arg1);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadSceneAsync2_wrap(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.AsyncOperation ret = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int LoadSceneAsync3_wrap(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.SceneManagement.LoadSceneMode arg1 = (UnityEngine.SceneManagement.LoadSceneMode)(FCLibHelper.fc_get_int(L,1));
            UnityEngine.AsyncOperation ret = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(arg0,arg1);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int CreateScene_wrap(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.SceneManagement.Scene ret = UnityEngine.SceneManagement.SceneManager.CreateScene(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int UnloadSceneAsync_wrap(long L)
    {
        try
        {
            int arg0 = FCLibHelper.fc_get_int(L,0);
            UnityEngine.AsyncOperation ret = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int UnloadSceneAsync1_wrap(long L)
    {
        try
        {
            string arg0 = FCLibHelper.fc_get_string_a(L,0);
            UnityEngine.AsyncOperation ret = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int UnloadSceneAsync2_wrap(long L)
    {
        try
        {
            UnityEngine.SceneManagement.Scene arg0 = FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(FCLibHelper.fc_get_wrap_objptr(L,0));
            UnityEngine.AsyncOperation ret = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(arg0);
            long ret_ptr = FCLibHelper.fc_get_return_ptr(L);
            long v = FCGetObj.PushObj(ret);
            FCLibHelper.fc_set_value_wrap_objptr(ret_ptr, v);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int MergeScenes_wrap(long L)
    {
        try
        {
            UnityEngine.SceneManagement.Scene arg0 = FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(FCLibHelper.fc_get_wrap_objptr(L,0));
            UnityEngine.SceneManagement.Scene arg1 = FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(FCLibHelper.fc_get_wrap_objptr(L,1));
            UnityEngine.SceneManagement.SceneManager.MergeScenes(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int MoveGameObjectToScene_wrap(long L)
    {
        try
        {
            UnityEngine.GameObject arg0 = FCGetObj.GetObj<UnityEngine.GameObject>(FCLibHelper.fc_get_wrap_objptr(L,0));
            UnityEngine.SceneManagement.Scene arg1 = FCGetObj.GetObj<UnityEngine.SceneManagement.Scene>(FCLibHelper.fc_get_wrap_objptr(L,1));
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(arg0,arg1);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

}
