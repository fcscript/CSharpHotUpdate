using UnityEngine;
using System;
using UnityEngine.Rendering;
using System.Collections.Generic;


class  Renderer : Component
{
    public Renderer(){}
    public Bounds bounds { get; }
    public bool enabled { get;  set; }
    public bool isVisible { get; }
    public ShadowCastingMode shadowCastingMode { get;  set; }
    public bool receiveShadows { get;  set; }
    public MotionVectorGenerationMode motionVectorGenerationMode { get;  set; }
    public LightProbeUsage lightProbeUsage { get;  set; }
    public ReflectionProbeUsage reflectionProbeUsage { get;  set; }
    public StringA sortingLayerName { get;  set; }
    public int sortingLayerID { get;  set; }
    public int sortingOrder { get;  set; }
    public bool allowOcclusionWhenDynamic { get;  set; }
    public bool isPartOfStaticBatch { get; }
    public Matrix worldToLocalMatrix { get; }
    public Matrix localToWorldMatrix { get; }
    public GameObject lightProbeProxyVolumeOverride { get;  set; }
    public Transform probeAnchor { get;  set; }
    public int lightmapIndex { get;  set; }
    public int realtimeLightmapIndex { get;  set; }
    public Vector4 lightmapScaleOffset { get;  set; }
    public Vector4 realtimeLightmapScaleOffset { get;  set; }
    public Material material { get;  set; }
    public Material sharedMaterial { get;  set; }
    public List<Material> materials { get;  set; }
    public List<Material> sharedMaterials { get;  set; }
    public void SetPropertyBlock(MaterialPropertyBlock properties){}
    public void GetPropertyBlock(MaterialPropertyBlock dest){}
    public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result){}
};

