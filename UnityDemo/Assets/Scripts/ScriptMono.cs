using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScriptMono : MonoBehaviour
{
    public string ScripClassName; // 对应脚本中的类名
    private long m_nScriptInsPtr; // 脚本对象指针(一个64位整数)

    void  Start()
    {
        // 目前这个示例中，由于DLL初始化比较晚，就不在这里执行了
        m_nScriptInsPtr = 0;

        // 这时脚本还没有加载好，暂时就不在这里执行了
        //CreateScript();
    }
    void CreateScript()
    {
        if (0 != m_nScriptInsPtr)
            return;
        // 创建一个脚本
        if (string.IsNullOrEmpty(ScripClassName))
            m_nScriptInsPtr = 0;
        else
            m_nScriptInsPtr = FCLibHelper.fc_instance(ScripClassName);

        // 必要的话，调用下脚本中的Start函数
        if (m_nScriptInsPtr != 0)
        {
            FCLibHelper.fc_call(m_nScriptInsPtr, "Start");
        }
    }
    void OnDestroy()
    {
        if(m_nScriptInsPtr != 0)
        {
            FCLibHelper.fc_call(m_nScriptInsPtr, "OnDestroy"); // 实际上，脚本一般是不需要OnDestroy事件的，只需要释放脚本就可以了
            FCLibHelper.fc_relese_ins(m_nScriptInsPtr); // 释放脚本对象，如果脚本对象有析构函数，就会自动调用析构函数
            m_nScriptInsPtr = 0;
        }
    }
    public void OnButtonClicked(string szName)
    {
        CreateScript(); // 延迟执行吧

        if (m_nScriptInsPtr != 0)
        {
            FCDll.PushReturnParam(szName);  // 传点击的按钮的参数
            FCLibHelper.fc_call(m_nScriptInsPtr, "OnButtonClicked");
        }
    }
}
