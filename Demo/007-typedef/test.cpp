
typedef  map<int32, CStringA>   CID2Name;
typedef  uint      int32;
typedef  StringA   CStringA;

void  PrintMap(CID2Name aID)
{
	print("--------------------------------------");
	print("aID.Length = {0}", aID.Length);
	print("key, value:");
	StringA   szText;
	szText.Preallocate(1024); // 优化，预先分配1K内存
    for(iterator it = aID.begin(); it; ++it)
    {
		szText.AppendFormat("({0},{1})", it.key, it.value);
    }
	print("{0}", szText);
}


export void  main()
{
	CID2Name     aID2Name = new CID2Name();
	aID2Name[1] = "test1";
	aID2Name[2] = "test2";
	aID2Name[3] = "test3";
	aID2Name[4] = "test4";

	PrintMap(aID2Name);
}
