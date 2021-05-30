using System;
using UnityEngine;

using UnityEngine.Rendering;


public class AutoClass_wrap
{
    public static void Register(long VM)
    {
        UserClass_wrap.Register(VM);
        TestD_wrap.Register(VM);
        TestPart_wrap.Register(VM);
    }
}
