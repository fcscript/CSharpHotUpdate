using System;
using UnityEngine;

using UnityEngine.Rendering;


public class UnityUI_wrap
{
    public static void Register(long VM)
    {
        ButtonClickedEvent_wrap.Register(VM);
        Button_wrap.Register(VM);
        Text_wrap.Register(VM);
    }
}
