using System;
using System.Collections.Generic;


class  Material : UnityObject
{
    public Material(Shader shader){}
    public Material(Material source){}
    public Shader shader { get;  set; }
    public Color color { get;  set; }
    public Texture mainTexture { get;  set; }
    public Vector2 mainTextureOffset { get;  set; }
    public Vector2 mainTextureScale { get;  set; }
    public int passCount { get; }
    public int renderQueue { get;  set; }
    public List<StringA> shaderKeywords { get;  set; }
    public MaterialGlobalIlluminationFlags globalIlluminationFlags { get;  set; }
    public bool enableInstancing { get;  set; }
    public bool doubleSidedGI { get;  set; }
    public bool HasProperty(StringA propertyName){ return default(bool); }
    public bool HasProperty(int nameID){ return default(bool); }
    public StringA GetTag(StringA tag,bool searchFallbacks,StringA defaultValue){ return default(StringA); }
    public StringA GetTag(StringA tag,bool searchFallbacks){ return default(StringA); }
    public void SetOverrideTag(StringA tag,StringA val){}
    public void SetShaderPassEnabled(StringA passName,bool enabled){}
    public bool GetShaderPassEnabled(StringA passName){ return default(bool); }
    public void Lerp(Material start,Material end,float t){}
    public bool SetPass(int pass){ return default(bool); }
    public StringA GetPassName(int pass){ return default(StringA); }
    public int FindPass(StringA passName){ return default(int); }
    public void CopyPropertiesFromMaterial(Material mat){}
    public void EnableKeyword(StringA keyword){}
    public void DisableKeyword(StringA keyword){}
    public bool IsKeywordEnabled(StringA keyword){ return default(bool); }
    public void SetFloat(StringA name,float value){}
    public void SetFloat(int nameID,float value){}
    public void SetInt(StringA name,int value){}
    public void SetInt(int nameID,int value){}
    public void SetColor(StringA name,Color value){}
    public void SetColor(int nameID,Color value){}
    public void SetVector(StringA name,Vector4 value){}
    public void SetVector(int nameID,Vector4 value){}
    public void SetMatrix(StringA name,Matrix value){}
    public void SetMatrix(int nameID,Matrix value){}
    public void SetTexture(StringA name,Texture value){}
    public void SetTexture(int nameID,Texture value){}
    public void SetBuffer(StringA name,ComputeBuffer value){}
    public void SetBuffer(int nameID,ComputeBuffer value){}
    public void SetTextureOffset(StringA name,Vector2 value){}
    public void SetTextureOffset(int nameID,Vector2 value){}
    public void SetTextureScale(StringA name,Vector2 value){}
    public void SetTextureScale(int nameID,Vector2 value){}
    public void SetFloatArray(StringA name,List<float> values){}
    public void SetFloatArray(int nameID,List<float> values){}
    public void SetColorArray(StringA name,List<Color> values){}
    public void SetColorArray(int nameID,List<Color> values){}
    public void SetVectorArray(StringA name,List<Vector4> values){}
    public void SetVectorArray(int nameID,List<Vector4> values){}
    public void SetMatrixArray(StringA name,List<Matrix> values){}
    public void SetMatrixArray(int nameID,List<Matrix> values){}
    public float GetFloat(StringA name){ return default(float); }
    public float GetFloat(int nameID){ return default(float); }
    public int GetInt(StringA name){ return default(int); }
    public int GetInt(int nameID){ return default(int); }
    public Color GetColor(StringA name){ return default(Color); }
    public Color GetColor(int nameID){ return default(Color); }
    public Vector4 GetVector(StringA name){ return default(Vector4); }
    public Vector4 GetVector(int nameID){ return default(Vector4); }
    public Matrix GetMatrix(StringA name){ return default(Matrix); }
    public Matrix GetMatrix(int nameID){ return default(Matrix); }
    public void GetFloatArray(StringA name,List<float> values){}
    public void GetFloatArray(int nameID,List<float> values){}
    public List<float> GetFloatArray(StringA name){ return null; }
    public List<float> GetFloatArray(int nameID){ return null; }
    public void GetVectorArray(StringA name,List<Vector4> values){}
    public void GetVectorArray(int nameID,List<Vector4> values){}
    public List<Color> GetColorArray(StringA name){ return null; }
    public List<Color> GetColorArray(int nameID){ return null; }
    public void GetColorArray(StringA name,List<Color> values){}
    public void GetColorArray(int nameID,List<Color> values){}
    public List<Vector4> GetVectorArray(StringA name){ return null; }
    public List<Vector4> GetVectorArray(int nameID){ return null; }
    public void GetMatrixArray(StringA name,List<Matrix> values){}
    public void GetMatrixArray(int nameID,List<Matrix> values){}
    public List<Matrix> GetMatrixArray(StringA name){ return null; }
    public List<Matrix> GetMatrixArray(int nameID){ return null; }
    public Texture GetTexture(StringA name){ return default(Texture); }
    public Texture GetTexture(int nameID){ return default(Texture); }
    public Vector2 GetTextureOffset(StringA name){ return default(Vector2); }
    public Vector2 GetTextureOffset(int nameID){ return default(Vector2); }
    public Vector2 GetTextureScale(StringA name){ return default(Vector2); }
    public Vector2 GetTextureScale(int nameID){ return default(Vector2); }
};

