using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Vector2
{
    public float x, y;
    //public Vector2  operator = (Vector2 other);  // 这个C#无法重写
    public Vector2(float _x, float _y)
    {
        x = _x; y = _y;
    }
    public void Set(float _x, float _y)
    {
        x = _x;  y = _y;
    }
    public float length
    {
        get
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
    }
    // 功能：返回向量的长度
    public float Length()
    {
        return (float)Math.Sqrt(x*x + y*y);
    }
    // 功能：检测是不是相似（相等）
    // 参数：fD - 最小差值
    public bool IsSimilar( Vector2 v, float fD)
    {
        return Math.Abs(v.x - x) < fD && Math.Abs(v.y - y) < fD;
    }
    // 功能：向量归一化
    public void Normalize()
    {
        float fLen = (float)Math.Sqrt(x * x + y * y);
        if(fLen != 0)
        {
            x /= fLen;
            y /= fLen;
        }
    }
    // 功能：向量置零
    public void Zero()
    {
        x = y = 0;
    }
    // 功能：向量置一
    public void One()
    {
        x = y = 1;
    }
    //public Vector2  operator += ( Vector2 v){ x += v.x; y += v.y; return this; }
    //public Vector2  operator -= ( Vector2 v) { x -= v.x; y -= v.y;  return this; }
    //public Vector2  operator *= ( float fScale) { x *= fScale; y *= fScale; return this; }
    //public Vector2  operator /= ( float fScale) { x /= fScale; y /= fScale; return this; }
    public static Vector2 operator + (Vector2 left, Vector2 right)
    {
        Vector2 v = new Vector2();
        v.x = left.x + right.x;
        v.y = left.y + right.y;
        return v;
    }
    public static Vector2 operator - (Vector2 left, Vector2 right)
    {
        Vector2 v = new Vector2();
        v.x = left.x - right.x;
        v.y = left.y - right.y;
        return v;
    }
    public static Vector2 operator *(Vector2 left, float right)
    {
        Vector2 v = new Vector2();
        v.x = left.x * right;
        v.y = left.y * right;
        return v;
    }
    public static Vector2 operator /(Vector2 left, float right)
    {
        Vector2 v = new Vector2();
        v.x = left.x / right;
        v.y = left.y / right;
        return v;
    }
    // 点乘
    public static float operator *(Vector2 left, Vector2 right)
    {
        return left.x * right.x + left.y * right.y;
    }
    // 功能：求两点之间的长度
    public static float Length(Vector2 v1, Vector2 v2)
    {
        return (float)Math.Sqrt((v1.x - v2.x)* (v1.x - v2.x) + (v1.y - v2.y)* (v1.y - v2.y));
    }
    public static float Distance(Vector2 v1, Vector2 v2)
    {
        return (float)Math.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y));
    }
    public static float GetDistSQ(Vector2 v1, Vector2 v2)
    {
        return (float)(v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y);
    }
    public static bool Equals( Vector2 v1, Vector2 v2 )
    {
        return v1.IsSimilar(v2, 0.00001f);
    }
    // 点乘
    public static float Dot(Vector2 v1, Vector2 v2 )
    {
        return v1.x * v2.x + v1.y * v2.y;
    }
    public static Vector2 Lerp(Vector2 v1, Vector2 v2, float fSlerp)
    {
        Vector2 v = new Vector2();
        v.x = v1.x + (v2.x - v1.x) * fSlerp;
        v.y = v1.y + (v2.y - v1.y) * fSlerp;
        return v;
    }
};

public struct Vector3
{
    public float x, y, z;
    public Vector3(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
    public void Set(float _x, float _y, float _z)
    {
        x = _x; y = _y; z = _z;
    }
    public float length
    {
        get
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }
    }
    public float Length()
    {
        return (float)Math.Sqrt(x * x + y * y + z * z);
    }
    // 0.00001f
    public bool IsSimilar( Vector3 v, float fD)
    {
        return Math.Abs(x - v.x) < fD && Math.Abs(y - v.y) < fD && Math.Abs(z - v.z) < fD; 
    }
    public void Normalize()
    {
        float fLength = Length();
        if(fLength != 0)
        {
            x /= fLength;
            y /= fLength;
            z /= fLength;
        }
    }
    public void Zero()
    {
        x = y = z = 0;
    }
    public void One()
    {
        x = y = z = 1;
    }
    //Vector3  &operator += ( const Vector3 &v);  // C#不支持重载
    //Vector3  &operator -= ( const Vector3 &v);  // C#不支持重载
    //Vector3  &operator *= ( float fScale );  // C#不支持重载
    //Vector3  &operator /= ( float fScale );  // C#不支持重载
    public static Vector3 operator + (Vector3 left, Vector3 right)
    {
        Vector3 v = new Vector3();
        v.x = left.x + right.x;
        v.y = left.y + right.y;
        v.z = left.z + right.z;
        return v;
    }
    public static Vector3 operator - (Vector3 left, Vector3 right)
    {
        Vector3 v = new Vector3();
        v.x = left.x - right.x;
        v.y = left.y - right.y;
        v.z = left.z - right.z;
        return v;
    }
    public static Vector3 operator * (Vector3 left, float fScale)
    {
        Vector3 v = new Vector3();
        v.x = left.x * fScale;
        v.y = left.y * fScale;
        v.z = left.z * fScale;
        return v;
    }
    public static Vector3 operator / (Vector3 left, float fScale)
    {
        Vector3 v = new Vector3();
        v.x = left.x / fScale;
        v.y = left.y / fScale;
        v.z = left.z / fScale;
        return v;
    }
    // 点乘
    public static float operator *(Vector3 left, Vector3 right)
    {
        return left.x * right.x + left.y * right.y + left.z * right.z;
    }

    public static float Length(Vector3 v1, Vector3 v2)
    {
        return (float)Math.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z));
    }
    public static float Distance(Vector3 v1, Vector3 v2)
    {
        return (float)Math.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z));
    }
    public static float GetDistSQ(Vector3 v1, Vector3 v2)
    {
        return (float)(v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z);
    }
    public static bool Equals( Vector3 v1, Vector3 v )
    {
        return v1.IsSimilar(v, 0.00001f);
    }
    // 点乘
    public static float Dot(Vector3 v1, Vector3 v2 )
    {
        return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
    }
    public static Vector3 Lerp(Vector3 v1, Vector3 v2, float fSlerp)
    {
        Vector3 v = new Vector3();
        v.x = v1.x + (v2.x - v1.x) * fSlerp;
        v.y = v1.y + (v2.y - v1.y) * fSlerp;
        v.z = v1.z + (v2.z - v1.z) * fSlerp;
        return v;
    }
    // 叉乘
    public static Vector3 Cross(Vector3 v1, Vector3 v2)
    {
        Vector3 v = new Vector3();
        v.x = v1.y * v2.z - v1.z * v2.y;
        v.y = v1.z * v2.x - v1.x * v2.z;
        v.z = v1.x * v2.y - v1.y * v2.x;
        return v;
    }
    // 功能：向量绕Y轴旋转
    public void RotationY(float fYaw)
    {
        float fSinYaw = (float)Math.Sin(fYaw);
        float fCosYaw = (float)Math.Cos(fYaw);
        float fx = x, fz = z;
        x = fx * fCosYaw - fz * fSinYaw;
        z = fx * fSinYaw + fz * fCosYaw;
    }
    // 功能：向量绕X轴旋转
    public void RotationX(float fPitch)
    {
        float fSinPitch = (float)Math.Sin(fPitch);
        float fCosPitch = (float)Math.Cos(fPitch);
        float fy = y, fz = z;
        y = fy * fCosPitch - fz * fSinPitch;
        z = fy * fSinPitch + fz * fCosPitch;
    }
    // 功能：向量绕Z轴旋转
    public void RotationZ(float fRoll)
    {
        float fSinRoll = (float)Math.Sin(fRoll);
        float fCosRoll = (float)Math.Cos(fRoll);
        float fx = x, fy = y;
        x = fx * fCosRoll - fy * fSinRoll;
        y = fx * fSinRoll + fy * fCosRoll;
    }
    // 功能：3D视投影空间变换(功能相当于D3DXVec3TransformCoord)
    public void TransformCoord( Vector3 vIn, Matrix mat )
    {

    }
    // 功能：普通的3D变换(旋转缩放位移)D3DXVec3Transform
    public void TransformNormal( Vector3 vIn, Matrix mat )
    {

    }
    // 功能：将一个3D坐标转换到屏幕坐标
    public Vector2 ToScreen( Matrix matWorldViewProj, float fScreenW, float fScreenH )
    {
        Vector2 v = new Vector2();
        return v;
    }
};

public struct Matrix
{
    float _11, _12, _13, _14;
    float _21, _22, _23, _24;
    float _31, _32, _33, _34;
    float _41, _42, _43, _44;

    //Matrix  &operator = (const Matrix &other);
    //public float[] operator[](int nIndex){return _m[nIndex];}  // C#不支持，算了
    //float operator[][](int nRow, nCol){return _m[nRow][nCol];} // C#不支持，算了    
    public void Identity()
    {
        _11 = 1.0f; _12 = 0.0f; _13 = 0.0f; _14 = 0.0f;
        _21 = 0.0f; _22 = 1.0f; _23 = 0.0f; _24 = 0.0f;
        _31 = 0.0f; _32 = 0.0f; _33 = 1.0f; _34 = 0.0f;
        _41 = 0.0f; _42 = 0.0f; _43 = 0.0f; _44 = 1.0f;
    }
    // 功能：设置正交投影(右手坐标系opgl)
    // 参数：w, h, 场景的最大宽与高(MaxX - MinX), (MaxY - MinY)
    // 说明：功能同D3DXMatrixOrthoRH
    public void OrthoRH(float w, float h, float zn, float zf)
    {
        _11 = 2.0f / w; _12 = 0.0f; _13 = 0.0f; _14 = 0.0f;
        _21 = 0.0f; _22 = 2.0f / h; _23 = 0.0f; _24 = 0.0f;
        _31 = 0.0f; _32 = 0.0f; _33 = 1.0f / (zn - zf); _34 = 0.0f;
        _41 = 0.0f; _42 = 0.0f; _43 = zn / (zn - zf); _44 = 1.0f;
    }
    // 功能：设置正交投影(左手坐标系DX)
    // 参数：w, h, 场景的最大宽与高(MaxX - MinX), (MaxY - MinY)
    // 说明：功能同D3DXMatrixOrthoLH
    public void OrthoLH(float w, float h, float zn, float zf)
    {
        _11 = 2.0f / w; _12 = 0.0f; _13 = 0.0f; _14 = 0.0f;
        _21 = 0.0f; _22 = 2.0f / h; _23 = 0.0f; _24 = 0.0f;
        _31 = 0.0f; _32 = 0.0f; _33 = 1.0f / (zf - zn); _34 = 0.0f;
        _41 = 0.0f; _42 = 0.0f; _43 = zn / (zn - zf); _44 = 1.0f;
    }
    // 功能：设置透视投影(opengl右手坐标系)
    // 参数：fovy - 可视角(这个算弧度，不是角度)
    // aspectRatio - 窗口宽高比例(宽/高)
    // zn - 近截面的距离
    // zf - 近截面的距离
    public void PerspectiveR(float fovy, float Aspect, float zn, float zf)
    {

    }
    // 功能：设置透视投影(DX左手投影变换)
    // 参数：fovy - 这个算弧度，不是角度
    public void PerspectiveL(float fovy, float Aspect, float zn, float zf)
    {

    }
    // 功能：构造移动变换距阵
    // 参数： fx   - X轴移动的距离
    //        fy   - Y轴移动的距离
    //        fZ   - y轴移动的距离
    public void Translation(float fx, float fy, float fz)
    {
        Identity();
        _41 = fx;
        _42 = fy;
        _43 = fz;
    }
    // 功能：任意轴的旋转
    // 参数： fYaw   - 绕Y轴旋转的弧度数
    //        fPitch - 绕X轴旋转的弧度数
    //        fRoll  - 绕Z轴旋转的弧度数
    // 说明： 这个函数功能等同于D3DXMatrixRotationYawPitchRoll
    public void RotationYawPitchRoll(float fYaw, float fPitch, float fRoll)
    {

    }

    // 功能：旋转 + 缩放（无视先后）
    public void RotationYawPicthRollSacle(float fYaw, float fPitch, float fRoll, float fScaleX, float fScaleY, float fScaleZ)
    {

    }
    // 功能： 先移动，后旋转
    public void MoveRotationYawPicthRoll(float fMoveX, float fMoveY, float fMoveZ, float fYaw, float fPitch, float fRoll)
    {

    }
    // 功能：先旋转，后移动
    public void RotationYawPicthRollMove(float fYaw, float fPitch, float fRoll, float fMoveX, float fMoveY, float fMoveZ)
    {

    }
    // 功能：缩放
    public void Scaling(float fScaleX, float fScaleY, float fScaleZ)
    {
        Identity();
        _11 = fScaleZ;
        _22 = fScaleZ;
        _33 = fScaleZ;
    }
    // 功能：绕Z轴旋转
    public void RotationRoll(float fRoll)
    {
    }
    // 功能：绕Z轴旋转
    public void RotationZ(float fRoll)
    {
    }
    // 功能：绕X轴旋转
    public void RotationPitch(float fPitch)
    {
    }
    // 功能：绕X轴旋转
    public void RotationX(float fYaw)
    {
    }
    // 功能：绕Y轴旋转
    public void RotationY(float fPitch)
    {
    }
    // 功能：绕Y轴旋转
    public void RotationYaw(float fYaw)
    {

    }
    // 功能：求other逆距阵，将设置给自己
    public bool Inverse( Matrix other )
    {
        return true;
    }
    // 功能：将自己逆距阵
    public bool Inverse()
    {
        return true;
    }
    // 求转置距阵
    public void Transpose( Matrix other )
    {
    }
    // 求转置距阵
    void Transpose()
    {
    }
    //Matrix   &operator += ( const Matrix &mat ); // C#不支持的重载，算了
    //Matrix   &operator -= ( const Matrix &mat ); // C#不支持的重载，算了
    //Matrix   &operator *= ( const Matrix &mat ); // C#不支持的重载，算了
    //Matrix   &operator *= ( float f) // C#不支持的重载，算了
    //Matrix   &operator /= ( float f); // C#不支持的重载，算了
    public void  Mul( Matrix mat1, Matrix mat2 ) // a = b * c
    {
        // this = mat1 * mat2; // 暂时不支持*的全局重载，那个效率比较低
    }
    // 功能：构造一个视变换距阵(同D3DXMatrixLookAtRH)
    public void LookAtRH( Vector3 pEye, Vector3 pAt, Vector3 pUp)
    {

    }
    // 功能：构造一个视变换距阵(同D3DXMatrixLookAtLH)
    public void LookAtLH( Vector3 pEye, Vector3 pAt, Vector3 pUp)
    {

    }
};

public struct Vector4
{
    public float x, y, z, w;

    public Vector4(float _x, float _y, float _z, float _w)
    {
        x = _x; y = _y; z = _z; w = _w;
    }

    //Vector4  &operator = (const Vector4 &other);
    public void Set(float _x, float _y, float _z, float _w)
    {
        x = _x; y = _y; z = _z; w = _w;
    }
    public float length
    {
        get
        {
            return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
        }
    }
    public float Length()
    {
        return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
    }
    public void Normalize()
    {
    }

    public static Vector4 operator + ( Vector4 v0, Vector4 v1 )
    {
        Vector4 v = new Vector4();
        v.x = v0.x + v1.x;
        v.y = v0.y + v1.y;
        v.z = v0.z + v1.z;
        v.w = v0.w + v1.w;
        return v;
    }
    public static Vector4 operator - ( Vector4 v0, Vector4 v1 )
    {
        Vector4 v = new Vector4();
        v.x = v0.x - v1.x;
        v.y = v0.y - v1.y;
        v.z = v0.z - v1.z;
        v.w = v0.w - v1.w;
        return v;
    }
    public static float operator *( Vector4 v0, Vector3 v1 )
    {
        return v0.x * v1.x + v0.y * v1.y + v0.z * v1.z + v0.w;
    }
    public static float operator *( Vector3 v0, Vector4 v1 )
    {
        return v0.x * v1.x + v0.y * v1.y + v0.z * v1.z + v1.w;
    }
    public static float operator *( Vector4 v0, Vector4 v1 )
    {
        return v0.x * v1.x + v0.y * v1.y + v0.z * v1.z + v0.w * v1.w;
    }
    public static Vector4  operator *( Vector4 v, float fScale)
    {
        Vector4 v1 = new Vector4();
        v1.x = v.x * fScale;
        v1.y = v.y * fScale;
        v1.z = v.z * fScale;
        v1.w = v.w * fScale;
        return v1;
    }
    //void operator *=(float fScale)
    //void operator *= ( const Matrix& mat )  // C#语法不支持噢
    //void operator += ( const Vector4 &v )
};

public struct Panel
{
    public float a, b, c, d;

    public Panel(float _a, float _b, float _c, float _d)
    {
        a = _a; b = _b; c = _c; d = _d;
    }

    //Panel  &operator = (const Panel &other);
    public void Set(float a, float b, float c, float d)
    {

    }
    public void Normalize()
    {

    }
    public Vector3 GetNormal()
    {
        Vector3 v = new Vector3();
        v.Set(a, b, c);
        return v;
    }
    public void InitFromPoints( Vector3 v1, Vector3 v2, Vector3 v3 )
    {

    }
    public void InitFromPointNormal( Vector3 vPoint, Vector3 vNormal )
    {

    }
    public bool IsFront( Vector3 vPos )
    {
        return false;
    }
    // 功能：求平面与射线的交点
    // 参数：vPos - 输出相交的点, 这里不用ref, 因为FC脚本不需要out, 只是为了语法兼容
    // 返回值：有交点返回true, 没有交点就返回false
    public bool IntersectLine(Vector3 pPos, Vector3 pBegin, Vector3 pRayDir)
    {
        return false;
    }
};

// 这个对应Unity3D中的 Bounds
public struct Bounds
{
    public Vector3 center;  // 中心点
    public Vector3 extents; // 大小
    public Bounds(Vector3 _center, Vector3 _extents)
    {
        center = _center; extents = _extents;
    }
    public void Set(Vector3 vMin, Vector3 vMax)
    {
    }
    public float Width()
    {
        return extents.x;
    }
    public float Height()
    {
        return extents.y;
    }
    public float Length()
    {
        return extents.z;
    }
    public Vector3 GetCenter()
    {
        return center;
    }
    // 功能：添加一个顶点，自动扩大包围盒
    void AddVector(Vector3 p)
    {
    }
    // 功能：添加一个包围盒，自动扩大包围盒
    void AddBoudBox( Bounds sBox )
    {
    }
    // 功能：设置一条线段，来构造一个包围盒
    void SetLineBox( Vector3 vBegin, Vector3 vEnd, float fLineSize )
    {

    }
    bool IsContain( Bounds sBox)
    {
        return false;
    }
    bool IsContain( Bounds sBox, float fStep )
    {
        return false;
    }
    bool IsContain( Vector3 sPoint )
    {
        return false;
    }
    bool IsContain( Vector3 sPoint, float fStep )
    {
        return false;
    }
    // 功能：得到射线与包围盒的相交
    // 参数: vPickPoint - 输出的交点, 这里不用out, 是因为脚本中是引用标记，但C#中引用得写ref或out, 只是为了语法兼容
    //       vRayOrg - 射线的起点
    //       vRayDir - 射线的方向
    bool GetIntersectPoint(Vector3 vPickPoint, Vector3 vRayOrg, Vector3 vRayDir )
    {
        return false;
    }
    // 功能：判断射线与包围盒是不是相交
    bool IsIntersectRay( Vector3 vRayOrg, Vector3 vRayDir, float fLineMax )
    {
        return false;
    }
    // 功能：将包围盒应用距阵变换
    void Transform(Matrix pMat)
    {
    }
};

public struct Ray
{
    public Vector3 org; // 射线的起点
    public Vector3 dir; // 射线的方向

    public Ray(Vector3 _org, Vector3 _dir)
    {
        org = _org; dir = _dir;
    }

    // 功能：求射线与三角形的交点
    // 参数：vPickPt - 这个是输出参数
    public bool GetIntersectPoint(out Vector3 vPickPt, Vector3 v0, Vector3 v1, Vector3 v2 )
    {
        vPickPt = new Vector3();
        float fDist = 0.0f, fU = 0.0f, fV = 0.0f;
        if (IntersectTriangle(v0, v1, v2, out fDist, out fU, out fV))
        {
            // V1 + U(V2 - V1) + V(V3 - V1).
            vPickPt = v0 + (v1 - v0) * fU + (v2 - v0) * fV;
            return true;
        }
        return false;
    }
    // 功能：求射线与三角形相交处的UV信息
    // 参数：v0, v1, v2, 三角形的三个顶点
    //       t - 输出交点到射线起点的距离
    //       u - 输出交点处三角形的
    public bool IntersectTriangle( Vector3 v0, Vector3 v1, Vector3 v2, out float t, out float u, out float v )
    {
        t = 0.0f;
        u = 0.0f;
        v = 0.0f;
        return true;
    }
};

public struct Color32
{
    public byte r, g, b, a;
    public Color32(byte _r, byte _g, byte _b, byte _a)
    {
        r = _r; g = _g; b = _b; a = _a;
    }
    public void Set(byte _r, byte _g, byte _b, byte _a)
    {
        r = _r; g = _g; b = _b; a = _a;
    }
    public static implicit operator Color32(uint c)
    {
        return new Color32(0, 0, 0, 0);
    }
    public uint ToInt32()
    {
        uint dwValue = a; dwValue <<= 8;
        dwValue |= r; dwValue <<= 8;
        dwValue |= g; dwValue <<= 8;
        dwValue |= b;
        return dwValue;
    }
};

public struct Color
{
    float r, g, b, a;
    public Color(float _r, float _g, float _b, float _a)
    {
        r = _r; g = _g; b = _b; a = _a;
    }
    public void Set(float _r, float _g, float _b, float _a)
    {
        r = _r; g = _g; b = _b; a = _a;
    }
    public static implicit operator Color(uint c)
    {
        return new Color(0, 0, 0, 0);
    }
    public uint ToInt32()
    {
        return 0;
    }
};

public struct Quaternion
{
    public float x, y, z, w;
    public Quaternion(float _x, float _y, float _z, float _w)
    {
        x = _x; y = _y; z = _z; w = _w;
    }
    public void Identity()
    {
    }
    public void Set(float _x, float _y, float _z, float _w)
    {
    }
    public void SetAxisAngle(Vector3 axis, float angle)
    {
    }
    public void SetEulerAngles(Vector3 Euler)
    {
    }
    public void SetEulerAngles(float fYaw, float fPitch, float fRoll)
    {
    }
    public void SetFromToRotation( Vector3 from, Vector3 to )
    {
    }
    // 功能：线性插值
    public void Lerp(Quaternion q1, Quaternion p2, float fLerp)
    {
    }
    // 功能：球型插值
    public void Slerp(Quaternion q1, Quaternion p2, float fLerp)
    {
    }
    public void ToAngleAxis(out Vector3 axis, out float angle)
    {
        axis = new Vector3();
        angle = 0;
    }
    // 功能：转换成欧拉角
    public Vector3 ToEuler()
    {
        return new Vector3();
    }
    public static Vector3 operator *(Quaternion q, Vector3 v)
    {
        return v;
    }
    public static Quaternion operator *(Quaternion q1, Quaternion p2)
    {
        return q1;
    }
};

public struct Bezier2D
{
    public Vector2 begin, end, p1, p2;
    public Bezier2D(Vector2 _begin, Vector2 _end, Vector2 _p1, Vector2 _p2)
    {
        begin = _begin; end = _end; p1 = _p1; p2 = _p2;
    }
    public void Set(Vector2 _begin, Vector2 _end, Vector2 _p1, Vector2 _p2)
    {
        begin = _begin; end = _end; p1 = _p1; p2 = _p2;
    }
    // 功能：插值
    // 参数：fLerp [0, 1]
    public Vector2 Lerp(float fLerp)
    {
        return new Vector2();
    }
};

public struct Bezier3D
{
    public Vector3 begin, end, p1, p2;
    public Bezier3D(Vector3 _begin, Vector3 _end, Vector3 _p1, Vector3 _p2)
    {
        begin = _begin; end = _end; p1 = _p1; p2 = _p2;
    }
    public void Set(Vector3 _begin, Vector3 _end, Vector3 _p1, Vector3 _p2)
    {
        begin = _begin; end = _end; p1 = _p1; p2 = _p2;
    }
    // 功能：插值
    // 参数：fLerp [0, 1]
    public Vector3 Lerp(float fLerp)
    {
        return new Vector3();
    }
};

public struct IntRect
{
    public int left, top, right, bottom;
    public IntRect(int _left, int _top, int _right, int _bottom)
    {
        left = _left; top = _top; right = _right; bottom = _bottom;
    }
    public void Set(int _left, int _top, int _right, int _bottom)
    {
        left = _left; top = _top; right = _right; bottom = _bottom;
    }
    public void AddPoint(int x, int y)
    {
    }
    public int width
    {
        get { return right - left; }
    }
    public int height
    {
        get { return bottom - top; }
    }
    public bool IsContain(int x, int y)
    {
        return true;
    }
    public bool IsContain(IntRect r)
    {
        return true;
    }
    // 功能：判断两个距形是不是相交
    public bool IsIntersect(IntRect r)
    {
        return true;
    }
};

public struct Rect
{
    public float left, top, right, bottom;
    public Rect(float _left, float _top, float _right, float _bottom)
    {
        left = _left; top = _top; right = _right; bottom = _bottom;
    }
    public void Set(float _left, float _top, float _right, float _bottom)
    {
        left = _left; top = _top; right = _right; bottom = _bottom;
    }
    public void AddPoint(int x, int y)
    {
    }
    public float width
    {
        get { return right - left; }
    }
    public float height
    {
        get { return bottom - top; }
    }
    public bool IsContain(int x, int y)
    {
        return true;
    }
    public bool IsContain(IntRect r)
    {
        return true;
    }
    // 功能：判断两个距形是不是相交
    public bool IsIntersect(IntRect r)
    {
        return true;
    }
};

public struct Plane
{
    public float a, b, c, d;
    public Plane(float _a, float _b, float _c, float _d)
    {
        a = _a; b = _b; c = _c; d = _d;
    }

    public void Set(float _a, float _b, float _c, float _d)
    {
        a = _a; b = _b; c = _c; d = _d;
    }
    public void Normalize()
    {
    }
    public Vector3 GetNormal()
    {
        return new Vector3(a, b, c);
    }
    // 功能：三点确定一个平面
    public void InitFromPoints(Vector3 v1, Vector3 v2, Vector3 v3)
    {
    }
    // 面法线+点确定一个平面
    public void InitFromPointNormal(Vector3 vPoint, Vector3 vNormal)
    {
    }
    // 检测一个点是不是在平面的前面
    public bool IsFront(Vector3 vPoint)
    {
        return true;
    }
    // 功能：通过一射线与平面求交点
    // 参数：vBegin - 射线的起点
    //       vDir - 射线的方向
    public bool IntersectLine(ref Vector3 vPickPos, Vector3 vBegin, Vector3 vDir)
    {
        return true;
    }
};

public struct Sphere
{
    public Vector3 center;
    public float radius;

    public Sphere(Vector3 _center, float _radius)
    {
        center = _center;
        radius = _radius;
    }

    public void Set(Vector3 _center, float _radius)
    {
        center = _center;
        radius = _radius;
    }
    // 功能：将一个包围盒转换成球体
    public void Set(Bounds  box)
    {
    }

    // 功能：检测射线是不是与球相交
    public bool IsIntersect(Vector3 vRayPos, Vector3 vRayDir)
    {
        return false;
    }
    // 功能：求射线与球体的交点
    // 参数: vPick - 输出选中的位置
    //       vRayPos - 射线的起点
    //       vRayDir - 射线的方向
    //       bSelectNear - 是不是选择离相机更近的交点（有可能存在两个交点）
    // 返回值：如果有交点, 返回true; 没有就返回false
    public bool GetIntersectPoint(ref Vector3 vPick, Vector3 vRayPos, Vector3 vRayDir, bool bSelectNear)
    {
        return false;
    }
}