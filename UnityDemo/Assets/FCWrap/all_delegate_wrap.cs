using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class UnityAction_deletate : FCDelegateBase
{
    public void  CallFunc()
    {
        try
        {
            FCLibHelper.fc_call(m_nThisPtr, m_szFuncName);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}
