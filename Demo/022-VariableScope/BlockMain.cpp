
void TestFunc(ref int i, int Numb)
{
	for(int i = 0; i<Numb; ++i)
	{
		os.print("    []i={0}", i);
	}
}

export void main()
{
	int Count = 0;
	for(int i = 0; i<3; ++i)
	{
		++Count;
		os.print("Count={0}, i={1}", Count, i);
		TestFunc(i, Count);
		os.print("Count={0}, i={1}\r\n", Count, i);
		int i = 0;
		++i;
	}
}