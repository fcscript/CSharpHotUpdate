using System;
using UnityEngine;

using UnityEngine.Rendering;


public class Unity_wrap
{
    public static void Register()
    {
        Time_wrap.Register();
        UnityObject_wrap.Register();
        Component_wrap.Register();
        Transform_wrap.Register();
        Texture2D_wrap.Register();
        GameObject_wrap.Register();
        Behaviour_wrap.Register();
        MonoBehaviour_wrap.Register();
        Animation_wrap.Register();
        Transform_wrap.Register();
        Renderer_wrap.Register();
        MeshRenderer_wrap.Register();
        SkinnedMeshRenderer_wrap.Register();
        Input_wrap.Register();
        Light_wrap.Register();
        Material_wrap.Register();
        UnityEvent_wrap.Register();
    }
}
