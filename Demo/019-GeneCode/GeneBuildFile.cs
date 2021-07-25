
class GeneBuildFile
{
	int  m_nClassID = 0;
	int  m_nFuncID = 0;
	int  m_nValueID = 0;
	int  m_nAllClassCount = 0;
	int  m_nAllFuncCount = 0;
	int  m_nAllCodeLine = 0;
    int m_nNextClassIndex = 0;
	StringA   m_szFileData;
    StringA   m_szClassName;
	StringA   m_szFileName;
	map<StringA, int>  m_ClassFlag = new map<StringA, int>();
    List<StringA> m_ClassList = new List<StringA>();
    List<StringA>  m_TypeList = new List<StringA>();
	List<StringA>  m_LocalValue = new List<StringA>();
	map<StringA, int> m_ValueFlags = new map<StringA, int>();
	
	public GeneBuildFile()
	{
		m_TypeList.push_back("int");
		m_TypeList.push_back("int");
		m_TypeList.push_back("float");
		m_TypeList.push_back("float");
		m_TypeList.push_back("StringA");
		m_TypeList.push_back("StringA");
		m_TypeList.push_back("Vector3");
		m_TypeList.push_back("double");
	}
	
	public StringA  RandomName(int nMinNumb, int nMaxNumb)
	{
		int nNameLen = os.RandInt(nMinNumb, nMaxNumb);		
		StringA  szName;
		for(int i = 0; i<nNameLen; ++i)
		{
			char  ch = 'a' + os.RandInt(0, 23);
			szName += ch;
		}
		return szName;
	}
	public StringA  GeneClassName()
	{
		// 随机名字？	
		++m_nAllClassCount;
		StringA  szName = RandomName(5, 10);
		if(os.RandInt(0, 100) < 50)
			szName.MakeUpper();
		szName.AppendFormat("{0}", ++m_nClassID);
		return szName;
	}
    public StringA  GeneNextClass()
    {
        int nIndex = m_nNextClassIndex++;
        return m_ClassList[nIndex];
    }
	public StringA  GeneFuncName()
	{
		// 随机名字？	
		StringA  szName = RandomName(5, 10);
		if(os.RandInt(0, 100) < 50)
			szName.MakeUpper();
		szName.AppendFormat("{0}", ++m_nFuncID);
		return szName;
	}
	public StringA GeneValueName()
	{	
		StringA  szName = RandomName(5, 10);
		if(os.RandInt(0, 100) < 50)
			szName.MakeUpper();
		szName.AppendFormat("{0}", ++m_nValueID);
		return szName;
	}
	public void  GeneSource(int nFileCount)
	{
        // 先生成
        int nAllClassCount = nFileCount * 5;
        m_ClassFlag.Reserve(nAllClassCount);
        m_ClassList.Reserve(nAllClassCount);
        for (int i = 0; i<nAllClassCount; ++i)
        {
            StringA szClassName = GeneClassName();
            m_ClassFlag[szClassName] = i;
            m_ClassList.push_back(szClassName);
        }

        uint nStartTime = os.GetTickCount();
		for(int i = 0; i<nFileCount; ++i)
		{
			GeneSourceFile();
		}
		uint nEndTime = os.GetTickCount();
		uint nCostTime = nEndTime - nStartTime;
		print("生成{2}个文件，费时{0}秒{1}豪秒", nCostTime/100, nCostTime%1000, nFileCount);
	}
	public void  GeneSourceFile()
	{
        m_szClassName = GeneClassName();
        m_szFileName = GeneClassName() + ".cs";
		//print("szClassName = {0}, m_nClassID = {1}", m_szClassName, m_nClassID);
		
		// 生成class内容
		m_szFileData.Empty();
		m_szFileData.Preallocate(1024*1024);
		GeneFileData();
		SaveClass();
	}
	public void GeneEnmuData()
	{
		m_szFileData += "\r\n";
		m_szFileData.AppendFormat("enum {0} \r\n{\r\n", m_szClassName);
		
		int  nNumb = os.RandInt(5, 10);
		StringA   szValue;
		for(int i = 0; i<nNumb; ++i)
		{
			szValue = RandomName(5, 10);
			m_szFileData.AppendFormat("    {0} = {1}, \r\n", szValue, i);
		}		
		m_szFileData += "};\r\n";
	}
	void  AddFor()
	{
        m_szFileData += "        int nTotal = 0;\r\n";
        m_szFileData += "        for(int i = 0; i<10; ++i)\r\n";
        m_szFileData += "        {\r\n";
        m_szFileData += "            nTotal += i;\r\n";
        m_szFileData += "        }\r\n";

        m_szFileData += "        int nValue = 0;\r\n";
        m_szFileData += "        switch(nValue)\r\n";
        m_szFileData += "        {\r\n";
        m_szFileData += "            case 0:\r\n";
        m_szFileData += "            ++nTotal;\r\n";
        m_szFileData += "            break;\r\n";
        m_szFileData += "            case 1:\r\n";
        m_szFileData += "            ++nTotal;\r\n";
        m_szFileData += "            break;\r\n";
        m_szFileData += "            default:\r\n";
        m_szFileData += "            break;\r\n";
        m_szFileData += "        }\r\n";
    }
	StringA MakeCallParam()
	{
		StringA  szCallParam;
		int nParamCount = os.RandInt(0, 5);
		if(nParamCount == 0)
		{
			return szCallParam;
		}
		//List<StringA>  TypeList = {"int", "float", "Vector3", "double", "StringA"};
		for(int i = 0; i<nParamCount; ++i)
		{
			if(i > 0)
				szCallParam += ",";
			int nIndex = os.RandInt(0, m_TypeList.size());
			szCallParam.AppendFormat("{0} {1}", m_TypeList[nIndex], GeneValueName());
		}
		
		return szCallParam;
	}
    void AddMemberValue()
    {
        for(int i = 0; i<10; ++i)
        {
            int nIndex = os.RandInt(0, this->m_ClassList.size());
            m_szFileData.AppendFormat("    {0} {1};\r\n", m_ClassList[nIndex], this->GeneValueName());
        }
        for(int i = 0; i<5; ++i)
        {
            m_szFileData.AppendFormat("    int {0};\r\n", GeneValueName());
            m_szFileData.AppendFormat("    float {0};\r\n", GeneValueName());
            m_szFileData.AppendFormat("    StringA {0};\r\n", GeneValueName());
        }
    }
	void  AddFunc()
	{
		++m_nAllFuncCount;
	    StringA  szFuncName = GeneFuncName();
		StringA  szCallParam = MakeCallParam();
		int nCodeLine = os.RandInt(50, 100);
		m_szFileData.AppendFormat("    public int {0}({1})\r\n", szFuncName, szCallParam);
		m_szFileData += "    {\r\n";

        AddFor();

        m_szFileData += "        return 0;\r\n";
        m_szFileData += "    }\r\n";
		
	}
	void  AddClass(StringA  szClassName, int nFuncNumbMin, int nFuncNumbMax)
	{
		m_nFuncID = 0;
		m_szFileData += "\r\n";
        m_szFileData.AppendFormat("class {0} \r\n{\r\n", szClassName);
		
		m_LocalValue.RemoveAll();
		m_ValueFlags.RemoveAll();

        // 添加成员变量
        AddMemberValue();

        int  nFuncCount = os.RandInt(nFuncNumbMin, nFuncNumbMax);
		for(int i = 0; i<nFuncCount; ++i)
		{
			AddFunc();
		}
		m_szFileData += "};";
	}
	public void GeneFileData()
	{
		GeneEnmuData();

		// 添加类
        for(int i = 0; i<5; ++i)
        {
            m_szClassName = GeneNextClass();
            AddClass(m_szClassName, 10, 50);
        }
	}
	void SaveClass()
	{
		StringA  szPathName = "K:/temp/Gene/" + m_szFileName;
		WriteFile(szPathName, m_szFileData);
	}
};