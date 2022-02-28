using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class UnityAction_delegate : FCDelegateBase
{
    public void  CallFunc()
    {
        long CallKey = FCLibHelper.QueryCallKey();
        long VM = m_VMPtr;
        try
        {
            long L_Param = FCLibHelper.fc_prepare_ue_fast_call(VM, m_nThisPtr, m_nClassName, m_nFuncName, CallKey);
            long L_Ret = FCLibHelper.fc_ue_call(VM, CallKey);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        FCLibHelper.fc_end_ue_call(VM, CallKey);
    }
}
