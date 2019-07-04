
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

export void  main()
{
    print("开始测试协程");
    CTestD   d = new CTestD();
    d.Start();
}
