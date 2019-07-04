
int CompareInt(int p1, int p2)
{
    return p2 - p1; // 降序排序
}

void  PrintList(List<int>  aID)
{
    StringA   szInfo;
	szInfo.Preallocate(aID.Length * 32);  // 预分配内存，提升性能
    for(int i = 0, nLen = aID.Length; i<nLen; ++i)
    {
        if(i > 0)
            szInfo.AppendFormat(",{0}", aID[i]);
        else
            szInfo += aID[i];
    }
    print("{0}", szInfo);
}

export void  main()
{
    // 数组初始化
    int  []a = { 1, 10, 20, 30, 40, 50 };
	PrintList(a);
    print("-----------------------------------------");
    List<int> A = new List<int>();
    for(int i = 0; i<10; ++i)
    {
        A.push_back(i * 100);
    }
	PrintList(A);
    A.RemoveAll();
    for(int i = 0; i<20; ++i)
    {
        A.push_back(System.RandInt(0, 1000));
    }

    // 默认升序排序
    A.Sort(); // 也可以使用 BubbleSort 
    print("-------------------升序排序----------------------");
	PrintList(A);

    print("-------------------自定义排序----------------------");
    A.Sort(CompareInt);
	PrintList(A);
    print("-------------------List反序----------------------");
	A.Inserve();
	PrintList(A);
}
