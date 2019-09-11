
// 使用 export 关键字，用于导出类名，供其他平台调用
export class CTestD
{
    IEnumerator   m_coruntine;

    IEnumerator   func1(int nValue)
    {
        nValue++;
        print("开始执行协程函数func1(int nValue), nValue={0}, 等待一秒", nValue);
        yield wait 1.0f;
        print("一秒之后");
        //StopCoroutine(m_coruntine);
        yield return 0;
        //Stop();
        //StopAllCoroutine();
        //StopCoroutine(func1);
        print("准备启动协程函数func2(int nValue)");
        yield return StartCoroutine(this, func2(1));
        StartCoroutine(this, func2(1));
        print("结束协程函数func1(int nValue)");
    }
    IEnumerator   func1(int nValue, float fTime)
    {
        nValue++;
		print("开始执行协程函数func1(int nValue, float fTime), nValue={0}, fTime={1}, 等待{2}秒", nValue, fTime, fTime);
        yield wait fTime;
		print("{0}秒之后", fTime);
        yield return StartCoroutine(this, func1(1));
        print("结束协程函数func1(int nValue, fTime)");
    }
    IEnumerator  func2(int nValue)
    {
        print("----开始执行协程函数func2(int nValue)");
        yield return 0;
        print("----结束协程函数func2");
    }
    export void  Start()
    {
        m_coruntine = new IEnumerator(this, func1(1, 1.0f));
        //m_coruntine.Start();
        StartCoroutine(m_coruntine);
        //StartCoroutine(this, func1(1));
    }
    export void  Stop()
    {
        //StopCoroutine(func1);
        StopCoroutine(CTestD, func1);
    }
};

export void  csharp2fc_set_vector3(Vector3 v)
{
    print("C#传参 v=({0},{1},{2})", v.x, v.y, v.z);
    fc2csharp_set_vector3(v);
}

export void  csharp2fc_set_vector4(Vector4 v)
{
    print("C#传参 v=({0},{1},{2},{3})", v.x, v.y, v.z, v.w);
    fc2csharp_set_vector4(v);
}

export void  csharp2fc_set_string(StringA v)
{
    print("C#传参 v={0}", v);
    fc2csharp_set_string(v);
}

// 测试C#中的await功能
export void  TestAwait()
{
	print("test await.....");
	fc2csharp_loadtask();
	print("after await.....");
}


//
//void  PrintTime(StringA szFunc, uint  nTime)
//{
//    print("{0}花费时间：{1}秒{2}毫秒", szFunc, nTime/1000, nTime % 1000);
//}
//
//void Test8()
//{
//    print("start call =================================");
//    uint  nBegin = System.GetTickCount();
//    uint  nTotal = 0;
//    for(int i = 0; i<1000000; ++i)
//    {
//        nTotal = nTotal + i - (i/2) * (i + 3) / (i + 5);
//    }
//    uint nEnd = System.GetTickCount();
//    print("end call =================================");
//    PrintTime("Test8", nEnd - nBegin);
//}
//
//void Test9()
//{
//    List<int>  aNumb = new List<int>();
//    aNumb.resize(1024);
//    for(int i = 0; i<1024; ++i)
//    {
//        aNumb[i] = i;
//    }
//    uint  nBegin = System.GetTickCount();
//    int  nTotal = 0;
//    for(int i = 0; i<100000; ++i)
//    {
//        for(int j = 0; j<1024; ++j)
//        {
////            nTotal = nTotal + aNumb[i];
//            nTotal += aNumb[j];
//        }
//    }
//    uint nEnd = System.GetTickCount();
//    PrintTime("Test9", nEnd - nBegin);
//}

export void  main()
{
    print("开始测试协程");
	
    CTestD   d = new CTestD();
    d.Start();
    Test8();
    Test9();
}
