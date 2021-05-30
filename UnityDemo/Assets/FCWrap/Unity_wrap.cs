using System;
using UnityEngine;

using UnityEngine.Rendering;


public class Unity_wrap
{
    public static void Register(long VM)
    {
        Time_wrap.Register(VM);
        UnityObject_wrap.Register(VM);
        Component_wrap.Register(VM);
        Transform_wrap.Register(VM);
        Texture2D_wrap.Register(VM);
        GameObject_wrap.Register(VM);
        Behaviour_wrap.Register(VM);
        MonoBehaviour_wrap.Register(VM);
        Animation_wrap.Register(VM);
        Renderer_wrap.Register(VM);
        MeshRenderer_wrap.Register(VM);
        SkinnedMeshRenderer_wrap.Register(VM);
        Input_wrap.Register(VM);
        Light_wrap.Register(VM);
        Material_wrap.Register(VM);
        UnityEvent_wrap.Register(VM);
        AsyncOperation_wrap.Register(VM);
        Scene_wrap.Register(VM);
        SceneManager_wrap.Register(VM);
    }
}
