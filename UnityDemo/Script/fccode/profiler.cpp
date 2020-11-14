
// https://blog.csdn.net/u011467512/article/details/72716376/?tdsourcetag=s_pcqq_aiomsg

uint  s_nTotalTime = 0;
uint  s_nTestCount = 0;
StringA  s_nLastFunc;

void  PrintTime(StringA szFunc, uint  nTime)
{
	if(s_nLastFunc != szFunc)
	{
		s_nTotalTime = 0;
		s_nTestCount = 0;
		s_nLastFunc = szFunc;
	}
	s_nTotalTime += nTime;
	++s_nTestCount;
	print("{0}花费时间：{1}秒{2}毫秒, 平均：{3}毫秒, 共{4}次", szFunc, nTime/1000, nTime % 1000, s_nTotalTime/s_nTestCount, s_nTestCount);
}
int  _V0 = 0;
export void EmptyFunc()
{
    _V0 = _V0 + 1;
}

export void  PrintV0()
{
    print("_V0={0}", _V0);
}

export void Test0(Transform transform)
{
    uint  nBegin = System.GetTickCount();
    for(int i = 0; i<200000; ++i)
    {
        transform.position = transform.position;
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test0", nEnd - nBegin);
}

export void  Test1(Transform transform)
{
    uint  nBegin = System.GetTickCount();
    Vector3 vUp;
    vUp.Set(0, 1, 0);
    for(int i = 0; i<200000; ++i)
    {
        transform.Rotate(vUp, 1);
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test1", nEnd - nBegin);
}

export void Test2()
{
    uint  nBegin = System.GetTickCount();
    Vector3 v;
    float f;
    float x;
    float y;
    float z;
    for(int i = 0; i<200000; ++i)
    {
        f = i;
        v.Set(f, f, f);
        x = v.x;
        y = v.y;
        z = v.z;        
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test2", nEnd - nBegin);
}

export void  Test3()
{
    List<GameObject>  aTemp = new List<GameObject>();
    aTemp.resize(20000);

    uint  nBegin = System.GetTickCount();
    for(int i = 0; i<20000; ++i)
    {
        GameObject  obj = new GameObject(); // 这样写，其实是会立即释放的
        aTemp[i] = obj;
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test3", nEnd - nBegin);
}

export void Test4(Transform transform)
{
    List<GameObject>  aTemp = new List<GameObject>();
    aTemp.resize(20000);
    uint  nBegin = System.GetTickCount();
    //SkinnedMeshRenderer  nType = typeof(SkinnedMeshRenderer);
	//GameObject  obj = new GameObject();  // 这样写，其实也是会立即释放的, 需要保存起来
    for(int i = 0; i<20000; ++i)
    {
        GameObject  obj = new GameObject();  // 这样写，其实也是会立即释放的, 需要保存起来
        obj.AddComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer c = obj.GetComponent<SkinnedMeshRenderer>();
        c.receiveShadows = false;
        aTemp[i] = obj;
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test4", nEnd - nBegin);
}

export void Test5()
{
    uint  nBegin = System.GetTickCount();
    for(int i = 0; i<200000; ++i)
    {
        Vector3 v = Input.mousePosition;
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test5", nEnd - nBegin);
}

export void Test6()
{
    uint  nBegin = System.GetTickCount();
    for(int i = 0; i<200000; ++i)
    {
        Vector3 v;
        v.Set(i, i, i);
        v.Normalize();
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test6", nEnd - nBegin);
}

export void Test7()
{
    uint  nBegin = System.GetTickCount();
    Quaternion  qa;
    Quaternion  q1;
    Quaternion  q2;
    for(int i = 0; i<200000; ++i)
    {
        //q1 = Quaternion.Euler(i, i, i);
        //q2 = Quaternion.Euler(i * 2, i * 2, i * 2);
        q1.SetEulerAngles(i, i, i);
        q2.SetEulerAngles(i * 2, i * 2, i * 2);
        qa.Slerp(q1, q2, 0.5f);
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test7", nEnd - nBegin);
}

export void Test8()
{
    //print("start call =================================");
    uint  nBegin = System.GetTickCount();
    int  total = 0;
	int  i = 0;
    for(i = 0; i<1000000; ++i)
    {
        total = total + i - (i/2) * (i + 3) / (i + 5);
    }
    uint nEnd = System.GetTickCount();
    //print("end call =================================");
	//print("total = {0}, i = {1}", total, i);
    PrintTime("Test8", nEnd - nBegin);
}

export void Test9()
{
    List<int>  aNumb = new List<int>();
    for(int i = 0; i<1024; ++i)
    {
        aNumb[i] = i;
    }
    uint  nBegin = System.GetTickCount();
    int  total = 0;
    for(int i = 0; i<100000; ++i)
    {
        for(int j = 0; j<1024; ++j)
        {
            //total = total + aNumb[i];
            total += aNumb[j];
        }
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test9", nEnd - nBegin);
}

export void Test91(Transform transform)
{
    List<float>  aNumb = new List<float>();
    for(int i = 0; i<1024; ++i)
    {
        aNumb[i] = i;
    }
    uint  nBegin = System.GetTickCount();
    float  total = 0;
    for(int i = 0; i<100000; ++i)
    {
        for(int j = 0; j<1024; ++j)
        {
            //total = total + aNumb[i];
            total += aNumb[j];
        }
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test91", nEnd - nBegin);
}

export void Test10(Transform transform)
{    
    uint  nBegin = System.GetTickCount();
	
    for(int i = 0; i<200000; ++i)
    {
		UserClass.TestFunc1(1, "123", transform.position, transform);
    }		
    uint nEnd = System.GetTickCount();
    PrintTime("Test10", nEnd - nBegin);
}

export void  Test12()
{
    uint  nBegin = System.GetTickCount();
    for(int i = 0; i<200000; ++i)
    {
        EmptyFunc();
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test12", nEnd - nBegin);
}

export void  Test13()
{
    List<StringA>   array = new List<StringA>();
    array.push_back("The");
    array.push_back("quick");
    array.push_back("brown");
    array.push_back("fox");
    array.push_back("jumped");
    array.push_back("over");
    array.push_back("the");
    array.push_back("lazy");
    array.push_back("dog");
    array.push_back("at");
    array.push_back("a");
    array.push_back("restaurant");
    array.push_back("near");
    array.push_back("ther");
    array.push_back("lake");
    array.push_back("of");
    array.push_back("ab");
    array.push_back("new");
    array.push_back("era");


    map<StringA, int>   cnt = new map<StringA, int>();
	cnt.Reserve(20);

	int  nArraySize = array.size();
	for(int k = 0; k<nArraySize; ++k)
	{
		cnt[array[k]] = 0;
	}
	--nArraySize;

	uint  nBegin = System.GetTickCount();
    for(int i = 0; i<1000000; ++i)
    {
        for(int k = 0; k<nArraySize; ++k)
        {
            cnt[array[k]] = cnt[array[k+1]] + 1;
			//cnt["aa"] = 1;
        }
    }
	uint nEnd = System.GetTickCount();
	PrintTime("Test13", nEnd - nBegin);
}

export void  Test14()
{
	List<int>   array = { 1412, 6658, 984, 899, 33, 14, 678, 638, 1101, 3320, 45, 99, 102, 204, 4456, 7668, 5446, 945, 653 };

	int  nArraySize = array.size() - 1;
	print("ArraySize = {0}", nArraySize + 1);

	map<int, int>   cnt = new map<int, int>();
	cnt.Reserve(20);
	uint  nBegin = System.GetTickCount();
	for (int i = 0; i<1000000; ++i)
	{
		for (int k = 0; k<nArraySize; ++k)
		{
			cnt[array[k]] = cnt[array[k+1]] + 1;
		}
	}
	uint nEnd = System.GetTickCount();
	PrintTime("Test14", nEnd - nBegin);
}