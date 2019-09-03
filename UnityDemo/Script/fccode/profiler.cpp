
// https://blog.csdn.net/u011467512/article/details/72716376/?tdsourcetag=s_pcqq_aiomsg

void  PrintTime(StringA szFunc, uint  nTime)
{
    print("{0}花费时间：{1}秒{2}毫秒", szFunc, nTime/1000, nTime % 1000);
}

export void Test0(Transform transform)
{
    print("start call =================================");
    uint  nBegin = System.GetTickCount();

    for(int i = 0; i<200000; ++i)
    {
        transform.position = transform.position;
    }
    uint nEnd = System.GetTickCount();
    print("end  call =================================");
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
    uint  nBegin = System.GetTickCount();
    for(int i = 0; i<200000; ++i)
    {
        GameObject  obj = new GameObject(); // 这样写，其实是会立即释放的
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test3", nEnd - nBegin);
}

export void Test4()
{
    uint  nBegin = System.GetTickCount();
    //SkinnedMeshRenderer  nType = typeof(SkinnedMeshRenderer);
    for(int i = 0; i<200000; ++i)
    {
        GameObject  obj = new GameObject();  // 这样写，其实也是会立即释放的, 需要保存起来
        obj.AddComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer c = obj.GetComponent<SkinnedMeshRenderer>();
        c.receiveShadows = false;
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
    print("start call =================================");
    uint  nBegin = System.GetTickCount();
    int  nTotal = 0;
    for(int i = 0; i<1000000; ++i)
    {
        total = total + i - (i/2) * (i + 3) / (i + 5);
    }
    uint nEnd = System.GetTickCount();
    print("end call =================================");
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
            total += aNumb[i];
        }
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test9", nEnd - nBegin);
}

export void Test10(Transform transform)
{
    uint  nBegin = System.GetTickCount();
    for(int i = 0; i<200000; ++i)
    {
        //UserClass.TestFunc1(1, "123", transform.position, transform);
    }
    uint nEnd = System.GetTickCount();
    PrintTime("Test10", nEnd - nBegin);
}