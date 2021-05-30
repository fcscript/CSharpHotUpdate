using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class await_script_loader : FCScriptLoader
{
    class ScriptInfo
    {
        public long nPtr;
        public long nReturnPtr;
    }
    class LoadTask
    {
        public string szAssetName;
        public float fStartTime;
        public float fWaitTime;
        public List<ScriptInfo> AwaitPtr; // 需要唤醒的脚本对象
    };
    List<LoadTask> m_AwaitTask = new List<LoadTask>();

    static await_script_loader s_pLoader;
    public static await_script_loader  Instance
    {
        get
        {
            return s_pLoader;
        }
    }

    private void Awake()
    {
        s_pLoader = this;        
    }

    protected override void OnAfterLoadScriptData()
    {
        // 先注册一个用户自己的wrap接口
        OwnRegister();

        base.OnAfterLoadScriptData();
    }

    void Update()
    {
        for(int i = m_AwaitTask.Count - 1; i>=0; --i)
        {
            LoadTask task = m_AwaitTask[i];
            if(task.fStartTime + task.fWaitTime < Time.time)
            {
                // 这里只是模拟加载，所以只是等待了一下时间而已，时间到了就算任务完成
                // 这里算任务完成了
                m_AwaitTask.RemoveAt(i);
                ContinueScript(task.AwaitPtr, task.szAssetName);
            }
        }
    }
    void  ContinueScript(List<ScriptInfo> AwaitPtr, string szAssetName)
    {
        if (AwaitPtr == null)
            return;
        GameObject obj = null;
        long v = 0;

        for (int i = 0; i< AwaitPtr.Count; ++i )
        {
            ScriptInfo info = AwaitPtr[i];
            if (FCLibHelper.fc_is_valid_await(m_VMPtr, info.nPtr))
            {
                //if(obj == null) 这里不能共用一个Obj，因为脚本中的引用计算一一对应的
                {
                    obj = new GameObject(szAssetName);  // 自动创建一个对象，算是模拟加载的后对象
                    v = FCGetObj.PushObj(obj);
                }
                // 必要的话，在这里设置返回值
                FCLibHelper.fc_set_value_wrap_objptr(m_VMPtr, info.nReturnPtr, v);

                FCLibHelper.fc_continue(m_VMPtr, info.nPtr); // 唤醒脚本
            }
        }
    }

    // 特殊的接口，比如加载
    void  PushLoadTask(long nAwaitPtr, long nRetPtr, string szAssetName, float fWaitTime)
    {
        ScriptInfo info = new ScriptInfo();
        info.nPtr = nAwaitPtr;
        info.nReturnPtr = nRetPtr;

        for (int i = 0; i<m_AwaitTask.Count; ++i)
        {
            if(m_AwaitTask[i].szAssetName == szAssetName)
            {
                m_AwaitTask[i].AwaitPtr.Add(info);
                return;
            }
        }
        LoadTask rTask = new LoadTask();
        rTask.fStartTime = Time.time;
        rTask.fWaitTime = fWaitTime;
        rTask.szAssetName = szAssetName;
        rTask.AwaitPtr = new List<ScriptInfo>();
        rTask.AwaitPtr.Add(info);

        m_AwaitTask.Add(rTask);
    }
    void OwnRegister()
    {
        int nClassName = FCLibHelper.fc_get_inport_class_id(m_VMPtr, "TestD");
        FCLibHelper.fc_register_class_func(m_VMPtr, nClassName, "WaitLoadPrefab", WaitLoadPrefab_wrap);
    }
    
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int WaitLoadPrefab_wrap(long L)
    {
        try
        {
            long nPtr = FCLibHelper.fc_await(L);
            long nRetPtr = FCLibHelper.fc_get_return_ptr(L);
            string arg0 = FCLibHelper.fc_get_string_a(L, 0);
            float arg1 = FCLibHelper.fc_get_float(L, 1);

            // 手动调用这个接口
            Instance.PushLoadTask(nPtr, nRetPtr, arg0, arg1);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }
}
