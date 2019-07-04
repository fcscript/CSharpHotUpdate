
// 目前，所有Class的变量与函数都默认是Public， 暂时不区分权限问题
class CTestA
{
	int      m_nValue;
	Vector3  m_vPos;
	CTestA()
	{
		m_nValue = 100;
		m_vPos.One();
		print("CTest 构造函数执行");
	}
	~CTestA()
	{
		print("CTest 析构函数执行");
	}
	void   Print()
	{
		print("m_nValue = {0}, m_vPos = ({1},{2},{3})", m_nValue, m_vPos.x, m_vPos.y,m_vPos.z);
	}
};

void   TestFunc()
{
	CTestA   obj = new CTestA();
	obj.m_nValue = 1000;
	obj.m_vPos.Set(30.f, 40.5f, 60.8f);
	obj.Print();
}

CTestA   s_pIns; // 类外定义的都是单例，class 默认是空指针，不需要初始化为NULL

export void  main()
{
	print("开始调用TestFunc");
	TestFunc();
	print("结束调用TestFunc");

	print("构造一个全局单例");
	s_pIns = new CTestA();
	s_pIns.Print();
}
