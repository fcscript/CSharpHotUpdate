﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test02 : ScriptMono
{
    // Use this for initialization
    protected override void OnCreateScript()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_nScriptInsPtr != 0)
        {
            FCCallHelper.fc_void_call(m_VMPtr, m_nScriptInsPtr, "Update");
        }
    }
}
