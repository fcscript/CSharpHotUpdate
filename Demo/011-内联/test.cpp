
class  CVector3
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
// 但简单的函数也可能减少代码体变小
export void  main()
{
	CVector3   v = new CVector3();
	v.Set(1f, 2f,3f);
	print("v = ({0},{1},{2}), Length={3}", v.x, v.y, v.z, v.Length());
}
