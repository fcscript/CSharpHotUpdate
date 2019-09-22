
// 使用 export 关键字，用于导出类名，供其他平台调用
class CTestD
{
	Delegate  m_func;

	CTestD()
	{
		m_func = new Delegate(this, TestFunc(12, "test delegate"));
	}

	void  TestFunc(int nValue, StringA szName)
	{
		print("nValue = {0}, {1}", nValue, szName);
	}
	public void  Call()
	{
		m_func.Call();
	}
};

void  GlbFunc(StringA szTips)
{
	print("{0}", szTips);
}

export void  main()
{
    print("开始测试委托");
    CTestD   d = new CTestD();
    d.Call();

	Delegate  func = new Delegate(null, GlbFunc("啦啦啦啦，我来啦"));
	func.Call();
}
