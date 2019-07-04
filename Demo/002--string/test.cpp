

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
}
