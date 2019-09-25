using System;
using UnityEngine;

using UnityEngine.Rendering;


public class all_class_wrap
{
    public static void Register()
    {
        Unity_wrap.Register();
        UnityUI_wrap.Register();
        AutoClass_wrap.Register();
    }
}
