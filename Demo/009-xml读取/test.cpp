
export void  main()
{
	StringA   szWorkPath = GetWorkPath(); // 说明：这个是调用其他平台的接口，需要注册的噢，在调试工具里面已经注册了的， 所以工具中可以直接使用，但其他平台的话，需要用户自己注册
	print("本地工作目录：{0}", szWorkPath);
	szWorkPath = GetProjectPath();
	print("工程目录：{0}", szWorkPath);
	szWorkPath += "..\xml_data\\";
	StringA   szXmlPath = szWorkPath + "test_xml.xml";

	print("xml目录：{0}", GetSkillPathName());

	StringA  szFileData;
    ReadFile(szXmlPath, szFileData);  // 这个也是跨平台注册的回调接口
    TestXml  pXmlNode = new TestXml();
    bool bSuc = System.ReadXml(pXmlNode, "ROOT", szFileData);
	if(bSuc)
		print("xml 读取成功, 文件大小={0}", szFileData.GetLength());
	else
		print("xml 读取失败, 文件大小={0}", szFileData.GetLength());

	print("---------------------------------------");

	pXmlNode.PrintXml();

	print("---------------------------------------");

	StringA   szSaveData;
	System.WriteXml(pXmlNode, "ROOT", szSaveData);
	print("{0}", szSaveData);
	
	// WriteFile(szWorkPath + "save_xml_2.xml", szSaveData);  // 这个保存

	//------------------------------------
	//PrintSkillXml();  // 加载技能表并打印到屏幕
}

void   PrintSkillXml()
{
	//------------------------------------
	print("---------------------------------------");
	StringA   szWorkPath = GetWorkPath();
	StringA   szXmlPath = szWorkPath + "Skill_SL.xml";
	StringA   szFileData;
    ReadFile(szXmlPath, szFileData);  // 这个也是跨平台注册的回调接口
	Skill_Configs  pSkillNode = new Skill_Configs();
	System.ReadXml(pSkillNode, "SkillDesc", szFileData);
	StringA   szSaveData;
	System.WriteXml(pSkillNode, "SkillDesc", szSaveData);
	print("{0}", szSaveData);
}

StringA   GetSkillPathName()
{
	StringA   szXmlPath = GetProjectPath() + "../xml_data/Skill_SL.xml";
	return szXmlPath;
}

void  SkillXmlToSerialize()
{
	StringA   szWorkPath = GetWorkPath();

    StringA   szFileData;
    szFileData.Preallocate(1024*1024*2);
    ReadFile(GetSkillPathName(), szFileData);
    Skill_Configs  pSkillNode = new Skill_Configs();
    System.ReadXml(pSkillNode, "SkillDesc", szFileData);
    
    CSerialize   ar;// = new CSerialize();
    ar.OwnWriteMode(1024*1024*2);
    ar.ReadWrite(pSkillNode);
    ar.CopyTo(szFileData);

    WriteFile(szWorkPath + "skill_to_sr.bin", szFileData);
}

void  TestSerializeSkill()
{
	StringA   szWorkPath = GetWorkPath();
    StringA   szFileData;
    szFileData.Preallocate(1024*1024*2);
    ReadFile(szWorkPath + "skill_to_sr.bin", szFileData);

    int64   nBeginTime = System.GetTickCount();
    Skill_Configs  []pArray = new Skill_Configs[100];
    CSerialize   ar;// = new CSerialize();
    for(int i = 0; i<100; ++i)
    {
        ar.ReadMode(szFileData);
        ar.ReadWrite(pArray[i]);
    }
    int64   nEndTime = System.GetTickCount();
    print("serialize_xml read cost time:{0}", nEndTime - nBeginTime);
}

void  SkillXmlToBin()
{
	StringA   szWorkPath = GetWorkPath();
    StringA   szFileData;
    szFileData.Preallocate(1024*1024*2);
    ReadFile(szWorkPath + "xml_data\\Skill_SL.xml", szFileData);
    Skill_Configs  pSkillNode = new Skill_Configs();
    System.ReadXml(pSkillNode, "SkillDesc", szFileData);
    
    System.XmlToBin(pSkillNode, szFileData);
    WriteFile(szWorkPath + "skill_to_bin.bin", szFileData);
}

void  TestReadSkillBin()
{
	StringA   szWorkPath = GetWorkPath();
    StringA   szFileData;
    szFileData.Preallocate(1024*1024*2);
    ReadFile(szWorkPath + "skill_to_bin.bin", szFileData);
    
    int64   nBeginTime = System.GetTickCount();
    Skill_Configs  []pArray = new Skill_Configs[100];
    for(int i = 0; i<100; ++i)
    {
        pArray[i] = new Skill_Configs();
        System.ReadBin(pArray[i], szFileData);
    }
    int64   nEndTime = System.GetTickCount();
    print("bin_xml read cost time:{0}", nEndTime - nBeginTime);
}
