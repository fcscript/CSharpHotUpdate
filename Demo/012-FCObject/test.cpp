
export class  CVector3
{
	float  x;
	float  y;
	float  z;
	inline  void  Set(float _x, float _y, float _z)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	inline  float Length()
	{
		return System.sqrt(x*x + y*y + z*z);
	}
};

// 内联可以减少函数调用的开销，提升性能，但有可能导致代码膨胀
// 但简单的函数也可能减少代码体积
export void  main()
{
    print("-----------------------");
    FCType nType = typeof(CVector3);
    print("nType is {0}", nType.ToString());
    FCObject obj = FCObject.Create(typeof(CVector3));
    CVector3 v1 = (CVector3)obj;
	v1.Set(1f, 2f,3f);
    bool  b1 = obj == null;
    print("obj == null is {0}", b1);
    print("v1 = ({0},{1},{2}), Length={3}, obj == v1 is {4}", v1.x, v1.y, v1.z, v1.Length(), obj == v1);

    obj = FCObject.Create("CVector3");
    CVector3 v2 = (CVector3)obj;
	v2.Set(2f, 3f,4f);
    print("v2 = ({0},{1},{2}), Length={3}, obj == v1 is {4}", v2.x, v2.y, v2.z, v2.Length(), obj == v1);

    map<FCObject, int>  aObjs = new map<FCObject, int>();
    aObjs[v1] = 1;
    aObjs[v2] = 2;

    PrintMap(aObjs);
}

void  PrintMap(map<FCObject, int> aObjs)
{
	print("--------------------------------------");
	print("aObjs.Length = {0}", aObjs.Length);
	print("key, value:");
	StringA   szText;
	szText.Preallocate(1024); // 优化，预先分配1K内存
    for(iterator it = aObjs.begin(); it; ++it)
    {
		szText.AppendFormat("({0},{1})", it.key.ToString(), it.value);
    }
	print("{0}", szText);
}