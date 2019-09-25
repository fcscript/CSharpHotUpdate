using System;
using UnityEngine;

using UnityEngine.Rendering;


public class AutoClass_wrap
{
    public static void Register()
    {
        UserClass_wrap.Register();
        TestD_wrap.Register();
        TestPart_wrap.Register();
    }
}
