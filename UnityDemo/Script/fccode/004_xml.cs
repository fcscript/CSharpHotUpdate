using System.Xml.Serialization;

class TestXml
{
    [XmlElementAttribute("NameID")]
    int NameID = 100 * 2 + 5;
    [XmlElementAttribute("Tips")]
    StringA Tips;
    [XmlElementAttribute("BUFF")]
    map<int, BuffInfo> BUFF = new map<int, BuffInfo>();

    void PrintXml()
    {
        os.print("NameID={0}", NameID);
        os.print("Tips={0}", Tips);
    }
};

class BuffInfo
{
    [XmlElementAttribute("ID")]
    int ID;
    [XmlElementAttribute("AddHP")]
    float AddHP;
    [XmlElementAttribute("Country")]
    int Country;
    [XmlElementAttribute("ARRAY_ID")]
    List<int> ARRAY_ID;
    [XmlElementAttribute("MAP_ID")]
    map<int, int> MAP_ID;
    [XmlElementAttribute("EFECT")]
    List<EffectNode> EFECT;
};

class EffectNode
{
    [XmlElementAttribute("ID")]
    int ID;
    [XmlElementAttribute("BindPos")]
    int BindPos;
};


[export]
class XmlLoader
{
    public static void TestLoad(StringA szFileData)
    {
        TestXml pXmlNode = new TestXml();
        bool bSuc = os.ReadXml(pXmlNode, "ROOT", szFileData);
        if (bSuc)
            os.print("xml 读取成功, 文件大小={0}", szFileData.GetLength());
        else
            os.print("xml 读取失败, 文件大小={0}", szFileData.GetLength());
    }
    public static void TestWrite(StringA szFileData)
    {
        TestXml pXmlNode = new TestXml();
        bool bSuc = os.ReadXml(pXmlNode, "ROOT", szFileData);
        
        StringA szSaveData;
        os.WriteXml(pXmlNode, "ROOT", szSaveData);
        szSaveData.Replace("\r\n", "");
        os.print("{0}", szSaveData);
    }
}