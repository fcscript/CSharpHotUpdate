using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class ButtonEvent : MonoBehaviour
{
    public ScriptMono UIMono;
    void Start()
    {
        if(UIMono == null)
        {
            UIMono = transform.GetComponentInParent<ScriptMono>();
        }
    }
    public void OnClick()
    {
        if (UIMono != null)
            UIMono.OnButtonClicked(name);
    }
}
