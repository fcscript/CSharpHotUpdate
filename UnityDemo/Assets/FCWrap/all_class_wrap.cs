using System;
using UnityEngine;

using UnityEngine.Rendering;


public class all_class_wrap
{
    public static void Register()
    {
        Unity_wrap.Register();
        Custom_wrap.Register();
        AutoClass_wrap.Register();
    }
}
