
FCExcel  open_excel(StringA szPathName)
{
	StringA   szFileData;
	ReadFile(szPathName, szFileData);
	FCExcel  excel = new FCExcel();

	bool  bSuc = excel.Load(szFileData);
	if(bSuc)
		print("数据表加载成功");
	else
		print("数据表加载失败：{0}", szPathName);

	print("数据文件名：{0}, 总行数={1},总列数={2},关键列={3}", excel.ExcelName, excel.RowNumb, excel.ColNumb, excel.KeyCol);

	int  nColNum = excel.GetColNumb();
	for(int nCol=0; nCol<nColNum; ++nCol)
	{
		print("第[{0}]列名字:{1}", nCol, excel.GetColName(nCol));
	}
	return excel;
}

void  print_item_excel(StringA   szPathName)
{
	FCExcel  excel = open_excel(szPathName);
	
	int nNameCol = excel.FindColByName("name");
	print("NameCol = {0}", nNameCol);
	int  nRowNum = excel.GetRowNumb();
	int nIDCol = excel.FindColByName("id");
	for(int nRow=0; nRow<nRowNum; ++nRow)
	{
		int nID = excel.GetInt(nRow, nIDCol);
		StringA  szName = excel.GetString(nRow, nNameCol);
		bool bCanUse = excel.GetInt(nRow, 2) != 0;
		int  nOverlay = excel.GetInt(nRow, 3);
		print("ID={0}, Name={1}, bCanUse = {2}, Overlay={3}", nID, szName, bCanUse, nOverlay);
	}
}

void  print_test_data_excel(StringA   szPathName)
{
	FCExcel  excel = open_excel(szPathName);
	
	int nNameCol = excel.FindColByName("name");
	print("NameCol = {0}", nNameCol);
	int nRowNum = excel.GetRowNumb();
	int nIDCol = excel.FindColByName("id");
	int nTypeCol = excel.FindColByName("type");
	int nLifeCol = excel.FindColByName("life");
	for(int nRow=0; nRow<nRowNum; ++nRow)
	{
		int nID = excel.GetInt(nRow, nIDCol);
		StringA  szName = excel.GetString(nRow, nNameCol);
		int  nType = excel.GetInt(nRow, nTypeCol);
		int  nLife = excel.GetInt(nRow, nLifeCol);
		print("ID={0}, Name={1}, type = {2}, life={3}", nID, szName, nType, nLife);
	}
}

export void  main()
{
	StringA   szWorkPath = GetProjectPath(); // 说明：这个是调用其他平台的接口，需要注册的噢，在调试工具里面已经注册了的， 所以工具中可以直接使用，但其他平台的话，需要用户自己注册
	print("本地工作目录：{0}", szWorkPath);
	szWorkPath += "../excel/bin/";

	print_item_excel(szWorkPath + "item.bin");
	print_test_data_excel(szWorkPath + "test_data.bin");
}
