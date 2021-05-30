using System;
using UnityEngine;

using UnityEngine.Rendering;


public class all_class_wrap
{
    public static void Register(long VM)
    {
        Unity_wrap.Register(VM);
        UnityUI_wrap.Register(VM);
        AutoClass_wrap.Register(VM);
    }
}
