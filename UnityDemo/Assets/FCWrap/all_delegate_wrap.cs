using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class UnityAction_delegate : FCDelegateBase
{
    public void  CallFunc()
    {
        try
        {
            long  VM = m_VMPtr;
            FCLibHelper.fc_call(VM, m_nThisPtr, m_szFuncName);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}
