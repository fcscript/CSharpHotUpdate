using System;
using UnityEngine;

using UnityEngine.Rendering;


public class UnityUI_wrap
{
    public static void Register()
    {
        ButtonClickedEvent_wrap.Register();
        Button_wrap.Register();
        Text_wrap.Register();
    }
}
