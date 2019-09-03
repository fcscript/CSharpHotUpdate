using System;
using UnityEngine;

using UnityEngine.Rendering;


public class Unity_wrap
{
    public static void Register()
    {
        UnityObject_wrap.Register();
        Transform_wrap.Register();
        Component_wrap.Register();
        Texture2D_wrap.Register();
        GameObject_wrap.Register();
        Behaviour_wrap.Register();
        Animation_wrap.Register();
        Transform_wrap.Register();
        SkinnedMeshRenderer_wrap.Register();
        Input_wrap.Register();
    }
}
