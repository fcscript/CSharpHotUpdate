

// 字符串示例
export void  main()
{
    StringA   szText = "欢迎来到这里，要想显示文本，请将文件转存成UTF8的文件格式。";
    szText += "可以使用Notpad++转换格式噢。";
    print("{0}", szText);

    //StringW  wszText(L"您可以使用UTF16噢");  // 暂时不支持这样的写法，必须用 = 连接
    StringW  wszText = L"您可以使用UTF16噢";
    print("{0}", wszText);
    szText = wszText;
    print("转UTF8 ==> {0}", szText);

    szText = "abc" + 1234 + 6789;
    print("字符串连接 abc + 1234 + 6789={0}", szText);

    szText = "abcUItest";
    print("szText = {0}", szText);
    
    szText = "abcUItest";
    szText.MakeUpper();
    print("转换大写 szText => {0}", szText);
    
    szText = "abcUItest";
    szText.MakeLower();
    print("转换小写 szText => {0}", szText);

    szText.Delete(0, 3);
    print("删除操作 szText => {0}", szText);

    szText = "  abcUItest  ";
    szText.Trim();
    print("删除两端空格 szText => {0}", szText);

	print("-----------format---------------"); // 控制符(D, N, X, F)
	print("进制转换X");
	byte   yValue = 0xff;
	print(StringA.Format("{0}==>0X{1:X16},大写,转16进制", yValue, yValue));
	ushort  wValue = 0xffff;
	print(StringA.Format("{0}==>0X{1:X16},大写,转16进制", wValue, wValue));
	uint  nValue = 0xe3b8a5c2;
	print(StringA.Format("{0}==>0X{1:X16},大写,转16进制", nValue, nValue));
	print(StringA.Format("{0}==>0x{1:x16},小写,转16进制", nValue, nValue));
	print(StringA.Format("{0}==>0X{1:X8}, 转八进制", nValue, nValue));
	print(StringA.Format("{0}==>0X{1:X4}, 转四进制", nValue, nValue));
	print(StringA.Format("{0}==>0X{1:X2}, 转二进制", nValue, nValue));
	
	print("整数补零D");
	nValue = 1234;
	print(StringA.Format("{0}==>{1:D6}", nValue, nValue));
	print(StringA.Format("{0}==>{1:D7}", nValue, nValue));
	print(StringA.Format("{0}==>{1:D8}", nValue, nValue));

	print("整数分组N");
	nValue = 1234567890;
	print(StringA.Format("{0}==>{1:N3}", nValue, nValue));
	print(StringA.Format("{0}==>{1:N4}", nValue, nValue));
	
	print("浮点数控制小数位数F");
	double  dfValue = 1234.5678912345;	
	print(StringA.Format("{0}==>{1:F2}", dfValue, dfValue));
	print(StringA.Format("{0}==>{1:F3}", dfValue, dfValue));
	print(StringA.Format("{0}==>{1:F4}", dfValue, dfValue));
	print(StringA.Format("{0}==>{1:F5}", dfValue, dfValue));
		
	print("-----------format---------------");
}
