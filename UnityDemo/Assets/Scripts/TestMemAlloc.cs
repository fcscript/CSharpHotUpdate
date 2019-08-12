using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SpriteVectorCache
{
    public Vector2[] xy = new Vector2[4];
    public Vector2[] uv = new Vector2[4];
    static SpriteVectorCache s_pIns;
    public static SpriteVectorCache Instance
    {
        get
        {
            if (s_pIns == null)
                s_pIns = new SpriteVectorCache();
            return s_pIns;
        }
    }
}

class TestMemAlloc : MonoBehaviour
{
    public enum TestType
    {
        TestNone,
        TestVector3,
    }

    public TestType testType = TestType.TestNone;

    void  Update()
    {
        switch(testType)
        {
            case TestType.TestVector3:
                {
                    float x0  = 0, y0 = 0;
                    float x1 = 1, y1 = 1;
                    float u0 = 0, v0 = 0;
                    float u1 = 1, v1 = 1;

                    //Vector2[] xy = new Vector2[4];// { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };
                    //Vector2[] uv = new Vector2[4];// { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };
                    Vector2[] xy = SpriteVectorCache.Instance.xy;
                    Vector2[] uv = SpriteVectorCache.Instance.uv;

                    xy[0] = new Vector2(x1, y0);
                    xy[1] = new Vector2(x1, y1);
                    xy[2] = new Vector2(x0, y1);
                    xy[3] = new Vector2(x0, y0);

                    uv[0] = new Vector2(u1, v1);
                    uv[1] = new Vector2(u1, v0);
                    uv[2] = new Vector2(u0, v0);
                    uv[3] = new Vector2(u0, v1);
                }
                break;
            default:
                break;
        }
    }
}
