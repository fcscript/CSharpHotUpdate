using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HelloWord : FCScriptLoader
{
    protected override void OnAfterLoadScriptData()
    {
        // 在加载完脚本字码码后，才执行脚本函数吧
        Transform tfChild = transform.Find("Text");
        UnityEngine.UI.Text  text = tfChild.GetComponent<UnityEngine.UI.Text>();
        FCDll.PushCallObjectParam(text);
        FCLibHelper.fc_call(0, "HelloWord.SetText");
    }    
}