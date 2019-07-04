
int CompareInt(int p1, int p2)
{
    return p2 - p1; // 降序排序
}

void  SortList(List<int>  aID)
{
	int nCount = aID.Length;
	int  i = nCount - 1;
	int  j = 0;
	int  nSwapIndex = 0;
	int  nTotle = 0;
    while(i>0)
	{
		nSwapIndex = 0;
		for(j=0; j<i; ++j)
		{
			if( aID[j+1] < aID[j] )
			{//
				nTemp = aID[j+1];
				aID[j+1] = aID[j];
				aID[j] = nTemp;
				nSwapIndex = j;//记录交换下标
                //++nTotle;
			}
		}
        i = nSwapIndex;
	}
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

void  RandomArray(List<int>  aID, int nCount)
{
	aID.RemoveAll();
	aID.resize(nCount);
	for(int i = 0; i<nCount; ++i)
	{
		aID[i] = System.RandInt(0, 1000);
	}
}

class  CCompare
{
	int Compare(int p1, int p2)
	{
		return p2 - p1; // 降序排序
	}
};

export void  main()
{
    // 数组初始化
    int  []a = { 1, 10, 20, 30, 40, 50 };  // 这个其实也是List<int>，只是写法不一样
	PrintList(a);
    print("-----------------------------------------");
    List<int> A = new List<int>();
    for(int i = 0; i<10; ++i)
    {
        A.push_back(i * 100);
    }
	PrintList(A);

    print("-------------------随机数组------------------");
	System.srand(9567745); // 设置一个随机种子
	RandomArray(A, 20);
	PrintList(A);

    // 默认升序排序
    A.Sort(); // 也可以使用 BubbleSort 
    print("-------------------升序排序----------------------");
	PrintList(A);

    print("-------------------使用全局脚本函数排序----------------------");
    A.Sort(CompareInt);
	PrintList(A);
    print("-------------------List反序----------------------");
	A.Inserve();
	PrintList(A);
    print("-------------------随机数组----------------------");
	System.srand(9567745); // 设置一个随机种子
	RandomArray(A, 20);
	PrintList(A);
    print("-------------------脚本内冒泡排序------------------");
	SortList(A);
	PrintList(A);
	
    print("-------------------使用类成员函数排序------------------");
	System.srand(9567745); // 设置一个随机种子
	RandomArray(A, 20);
	CCompare  ins = new CCompare();
	A.Sort(ins, Compare);
	PrintList(A);
}
