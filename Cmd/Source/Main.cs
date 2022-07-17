
List<StringA>  GetUpdatePath() 
{
    List<StringA> paths = 
    {
        @"aa\bb\cc",
        @"a1\b2"
    };
    return paths;
}

List<StringA>  GetUpdateFiles()
{
    StringA  Resource = @"Game\Resource\";
    StringA  Global = @"Game\Global\";
    List<StringA> files = 
    {
        Resource + "a1.txt",
        Resource + "a2.txt",        
        Global + "b2.txt",
    };

    return files;
}

void  PrintStringList(List<StringA> sList)
{
	for(int i = 0; i<sList.size(); ++i)
	{
		os.print("{0}", sList[i]);
	}
}

export void main()
{
    os.print("hello world");
    os.print("");

    List<StringA>  files = GetUpdateFiles();
	PrintStringList(files);

    os.print("");
    os.print("");
}
