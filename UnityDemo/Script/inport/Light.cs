using System;


class  Light : Behaviour
{
    public Light(){}
    public LightShadows shadows { get;  set; }
    public float shadowStrength { get;  set; }
    public LightShadowResolution shadowResolution { get;  set; }
    public float cookieSize { get;  set; }
    public Texture cookie { get;  set; }
    public LightRenderMode renderMode { get;  set; }
    public int commandBufferCount { get; }
    public LightType type { get;  set; }
    public float spotAngle { get;  set; }
    public Color color { get;  set; }
    public float colorTemperature { get;  set; }
    public float intensity { get;  set; }
    public float bounceIntensity { get;  set; }
    public int shadowCustomResolution { get;  set; }
    public float shadowBias { get;  set; }
    public float shadowNormalBias { get;  set; }
    public float shadowNearPlane { get;  set; }
    public float range { get;  set; }
    public Flare flare { get;  set; }
    public LightBakingOutput bakingOutput { get;  set; }
    public int cullingMask { get;  set; }
    public void AddCommandBuffer(LightEvent evt,CommandBuffer buffer){}
    public void AddCommandBuffer(LightEvent evt,CommandBuffer buffer,ShadowMapPass shadowPassMask){}
    public void RemoveCommandBuffer(LightEvent evt,CommandBuffer buffer){}
    public void RemoveCommandBuffers(LightEvent evt){}
    public void RemoveAllCommandBuffers(){}
    public List<CommandBuffer> GetCommandBuffers(LightEvent evt){ return null; }
    public static List<Light> GetLights(LightType type,int layer){ return null; }
};

