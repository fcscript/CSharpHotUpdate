using System;
using UnityEngine;
using System.Collections.Generic;


class  Texture2D : Texture
{
    public enum EXRFlags
    {
        None = 0,
        OutputAsFloat = 1,
        CompressZIP = 2,
        CompressRLE = 4,
        CompressPIZ = 8,
    };

    public Texture2D(int width,int height){}
    public Texture2D(int width,int height,TextureFormat format,bool mipmap){}
    public Texture2D(int width,int height,TextureFormat format,bool mipmap,bool linear){}
    public int mipmapCount { get; }
    public TextureFormat format { get; }
    public static Texture2D whiteTexture { get; }
    public static Texture2D blackTexture { get; }
    public static Texture2D CreateExternalTexture(int width,int height,TextureFormat format,bool mipmap,bool linear,IntPtr nativeTex){ return default(Texture2D); }
    public void UpdateExternalTexture(IntPtr nativeTex){}
    public void SetPixel(int x,int y,Color color){}
    public Color GetPixel(int x,int y){ return default(Color); }
    public Color GetPixelBilinear(float u,float v){ return default(Color); }
    public void SetPixels(List<Color> colors){}
    public void SetPixels(List<Color> colors,int miplevel){}
    public void SetPixels(int x,int y,int blockWidth,int blockHeight,List<Color> colors,int miplevel){}
    public void SetPixels(int x,int y,int blockWidth,int blockHeight,List<Color> colors){}
    public void SetPixels32(List<Color32> colors){}
    public void SetPixels32(List<Color32> colors,int miplevel){}
    public void SetPixels32(int x,int y,int blockWidth,int blockHeight,List<Color32> colors){}
    public void SetPixels32(int x,int y,int blockWidth,int blockHeight,List<Color32> colors,int miplevel){}
    public void LoadRawTextureData(List<byte> data){}
    public void LoadRawTextureData(IntPtr data,int size){}
    public List<byte> GetRawTextureData(){ return null; }
    public List<Color> GetPixels(){ return null; }
    public List<Color> GetPixels(int miplevel){ return null; }
    public List<Color> GetPixels(int x,int y,int blockWidth,int blockHeight,int miplevel){ return null; }
    public List<Color> GetPixels(int x,int y,int blockWidth,int blockHeight){ return null; }
    public List<Color32> GetPixels32(int miplevel){ return null; }
    public List<Color32> GetPixels32(){ return null; }
    public void Apply(bool updateMipmaps,bool makeNoLongerReadable){}
    public void Apply(bool updateMipmaps){}
    public void Apply(){}
    public bool Resize(int width,int height,TextureFormat format,bool hasMipMap){ return default(bool); }
    public bool Resize(int width,int height){ return default(bool); }
    public void Compress(bool highQuality){}
    public List<Rect> PackTextures(List<Texture2D> textures,int padding,int maximumAtlasSize,bool makeNoLongerReadable){ return null; }
    public List<Rect> PackTextures(List<Texture2D> textures,int padding,int maximumAtlasSize){ return null; }
    public List<Rect> PackTextures(List<Texture2D> textures,int padding){ return null; }
    public static bool GenerateAtlas(List<Vector2> sizes,int padding,int atlasSize,List<Rect> results){ return default(bool); }
    public void ReadPixels(Rect source,int destX,int destY,bool recalculateMipMaps){}
    public void ReadPixels(Rect source,int destX,int destY){}
};

public enum TextureFormat
{
    Alpha8 = 1,
    ARGB4444 = 2,
    RGB24 = 3,
    RGBA32 = 4,
    ARGB32 = 5,
    RGB565 = 7,
    R16 = 9,
    DXT1 = 10,
    DXT5 = 12,
    RGBA4444 = 13,
    BGRA32 = 14,
    RHalf = 15,
    RGHalf = 16,
    RGBAHalf = 17,
    RFloat = 18,
    RGFloat = 19,
    RGBAFloat = 20,
    YUY2 = 21,
    RGB9e5Float = 22,
    BC6H = 24,
    BC7 = 25,
    BC4 = 26,
    BC5 = 27,
    DXT1Crunched = 28,
    DXT5Crunched = 29,
    PVRTC_RGB2 = 30,
    PVRTC_RGBA2 = 31,
    PVRTC_RGB4 = 32,
    PVRTC_RGBA4 = 33,
    ETC_RGB4 = 34,
    ATC_RGB4 = 35,
    ATC_RGBA8 = 36,
    EAC_R = 41,
    EAC_R_SIGNED = 42,
    EAC_RG = 43,
    EAC_RG_SIGNED = 44,
    ETC2_RGB = 45,
    ETC2_RGBA1 = 46,
    ETC2_RGBA8 = 47,
    ASTC_RGB_4x4 = 48,
    ASTC_RGB_5x5 = 49,
    ASTC_RGB_6x6 = 50,
    ASTC_RGB_8x8 = 51,
    ASTC_RGB_10x10 = 52,
    ASTC_RGB_12x12 = 53,
    ASTC_RGBA_4x4 = 54,
    ASTC_RGBA_5x5 = 55,
    ASTC_RGBA_6x6 = 56,
    ASTC_RGBA_8x8 = 57,
    ASTC_RGBA_10x10 = 58,
    ASTC_RGBA_12x12 = 59,
    ETC_RGB4_3DS = 60,
    ETC_RGBA8_3DS = 61,
    RG16 = 62,
    R8 = 63,
    ETC_RGB4Crunched = 64,
    ETC2_RGBA8Crunched = 65,
    PVRTC_2BPP_RGB = -127,
    PVRTC_2BPP_RGBA = -127,
    PVRTC_4BPP_RGB = -127,
    PVRTC_4BPP_RGBA = -127,
};

