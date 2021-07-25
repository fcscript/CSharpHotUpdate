
class ValueD
{
	int e = 10;
	int f = 10;
};
class ValueC
{
	ValueD d = new ValueD();
};
class ValueB
{
	ValueC c = new ValueC();
};
class ValueA
{
	ValueB b = new ValueB();
};

class Value
{
	ValueA a = new ValueA();
};


class Profiler
{
	static uint  s_nTotalTime = 0;
	static uint  s_nTestCount = 0;
	static StringA  s_nLastFunc;

	public static void  EndTest()
	{
		if(s_nTotalTime > 0 )
		{
			print("{0}花费时间：{1}秒{2}毫秒, 平均：{3}毫秒, 共{4}次", s_nLastFunc, s_nTotalTime/1000, s_nTotalTime % 1000, s_nTotalTime/s_nTestCount, s_nTestCount);
			s_nTotalTime = 0;
		}
	}

	public static void  PrintTime(StringA szFunc, uint  nTime)
	{
		if(s_nLastFunc != szFunc)
		{
			EndTest();
			s_nTotalTime = 0;
			s_nTestCount = 0;
			s_nLastFunc = szFunc;
		}
		s_nTotalTime += nTime;
		++s_nTestCount;
	}
	static int  _V0 = 0;	
		
    [inline]
	static void EmptyFunc()
	{
		_V0 = _V0 + 1;
	}

	static int  Add(int a, int b)
	{
		return a + b;
	}

	static void EmptyCall3(int a, int b, int c)
	{
	}

	[export]
	public static void  Test1()
	{
		uint  nBegin = System.GetTickCount();
		for(int i = 0; i<20000000; ++i)
		{
			EmptyFunc();
		}
		uint nEnd = System.GetTickCount();
		PrintTime("Test1", nEnd - nBegin);
		//print("_V0 = {0}", _V0);
	}

	[export]
	public static void  Test1_1()
	{
		uint  nBegin = System.GetTickCount();
		for(int i = 0; i<200000; ++i)
		{
			Add(i, i);
		}
		uint nEnd = System.GetTickCount();
		PrintTime("Test1_1", nEnd - nBegin);
	}

	[export]
	public static void  Test1_2()
	{
		uint  nBegin = System.GetTickCount();
		for(int i = 0; i<200000; ++i)
		{
			EmptyCall3(i, i, i);
		}
		uint nEnd = System.GetTickCount();
		PrintTime("Test1_2", nEnd - nBegin);
	}
	
	[export]
	public static void  Test1_3()
	{
		uint  nBegin = System.GetTickCount();
		for(int i = 0; i<2000000; ++i)
		{
		}
		uint nEnd = System.GetTickCount();
		PrintTime("Test1_3", nEnd - nBegin);
	}
	[export]
	public static void  Test2()
	{
		Monster m = new Monster();
		uint  nBegin = System.GetTickCount();
		for(int i = 0; i<200000; ++i)
		{
			m.GetCampType();
		}
		uint nEnd = System.GetTickCount();
		PrintTime("Test2", nEnd - nBegin);
	}

	[export]
	public static void  Test3()
	{
		Value m = new Value();
		int nTotal = 0;
		uint  nBegin = System.GetTickCount();
		for(int i = 0; i<200000; ++i)
		{
			nTotal = m.a.b.c.d.e + 10;
		}
		uint nEnd = System.GetTickCount();
		PrintTime("Test3", nEnd - nBegin);
	}
		
	[export]
	public static void  Test4()
	{
		Value m = new Value();
		int nTotal = 0;
		uint  nBegin = System.GetTickCount();
		for(int i = 0; i<200000; ++i)
		{
			nTotal = m.a.b.c.d.e + m.a.b.c.d.f;
		}
		uint nEnd = System.GetTickCount();
		PrintTime("Test4", nEnd - nBegin);
	}
	[export]
	public static void  TestAll()
	{		
		uint  nBeginTick = System.GetTickCount();
		for(int i = 0; i<10; ++i)
		{
			Profiler::Test1();
		}
		for(int i = 0; i<10; ++i)
		{
			Profiler::Test1_1();
		}
		for(int i = 0; i<10; ++i)
		{
			Profiler::Test1_2();
		}
		for(int i = 0; i<10; ++i)
		{
			Profiler::Test1_3();
		}
		for(int i = 0; i<10; ++i)
		{
			Profiler::Test2();
		}
		for(int i = 0; i<10; ++i)
		{
			Profiler::Test3();
		}
		for(int i = 0; i<10; ++i)
		{
			Profiler::Test4();
		}
		Profiler::EndTest();
		uint nEndTick = System.GetTickCount();
		print("all test cost time: {0}", nEndTick - nBeginTick);
	}
};
