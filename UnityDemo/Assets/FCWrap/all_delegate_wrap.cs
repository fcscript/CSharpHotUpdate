using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class TestCallback_deletate : FCDelegateBase
{
    public void  CallFunc(int arg0)
    {
        try
        {
            FCDll.PushCallParam(arg0);
            FCLibHelper.fc_call(m_nThisPtr, m_szFuncName);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}

class TestCallback2_deletate : FCDelegateBase
{
    public void  CallFunc(int arg0,string arg1,Vector2 arg2)
    {
        try
        {
            FCDll.PushCallParam(arg0);
            FCDll.PushCallParam(arg1);
            FCDll.PushCallParam(ref arg2);
            FCLibHelper.fc_call(m_nThisPtr, m_szFuncName);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}
