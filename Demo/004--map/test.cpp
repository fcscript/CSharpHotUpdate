
void  PrintMap(map<int, int> aID)
{
	print("--------------------------------------");
	print("aID.Length = {0}", aID.Length);
	print("key, value:");
	StringA   szText;
	szText.Preallocate(1024); // 优化，预先分配1K内存
    for(iterator it = aID.begin(); it; ++it)
    {
		szText.AppendFormat("({0},{1})", it.key, it.value);
        //print("key={0}, value={1}", it.key, it.value);
    }
	print("{0}", szText);
}

export void  main()
{
    map<int, int>  aID = new map<int, int>();
	System.srand(4536678);
    for(int i = 0; i<5; ++i)
    {
        aID[i] = i * 100;// System.RandInt(0, 10000);  // 通过下标插入
    }
	PrintMap(aID);

	// map是有序的，与插入顺序一致
	aID.push_front(12, 12);  // 指定从头插入
	aID.push_front(11, 11);
	aID.push_front(10, 10);
	aID.push_back(100, 100); // 指定从尾部追加
	PrintMap(aID);

	// 可以在遍历过程中删除，不会影响遍历
	for(iterator it = aID.begin(); it; ++it)
	{
		if(it.value > 100)
		{
			print("remove:({0},{1})", it.key, it.value);
			aID.remove(it.key);
		}
	}
	PrintMap(aID);
}
