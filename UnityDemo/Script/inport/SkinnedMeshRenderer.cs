using System;


class  SkinnedMeshRenderer : Renderer
{
    public SkinnedMeshRenderer(){}
    public List<Transform> bones { get;  set; }
    public SkinQuality quality { get;  set; }
    public bool updateWhenOffscreen { get;  set; }
    public Transform rootBone { get;  set; }
    public Mesh sharedMesh { get;  set; }
    public bool skinnedMotionVectors { get;  set; }
    public Bounds localBounds { get;  set; }
    public float GetBlendShapeWeight(int index){ return default(float); }
    public void SetBlendShapeWeight(int index,float value){}
    public void BakeMesh(Mesh mesh){}
};

