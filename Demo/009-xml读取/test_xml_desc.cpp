
class TestXml
{
    [XmlElementAttribute("NameID")]
    int   NameID = 100 * 2 + 5;
    [XmlElementAttribute("Tips")]
    StringA   Tips;
    [XmlElementAttribute("BUFF")]
    map<int, BuffInfo> BUFF = new map<int, BuffInfo>();

	void  PrintXml()
	{
		print("NameID={0}", NameID);
		print("Tips={0}", Tips);
	}
};

class BuffInfo
{
    [XmlElementAttribute("ID")]
    int   ID;
    [XmlElementAttribute("AddHP")]
    float   AddHP;
    [XmlElementAttribute("Country")]
    int   Country;
    [XmlElementAttribute("ARRAY_ID")]
    list<int>  ARRAY_ID;
    [XmlElementAttribute("MAP_ID")]
    map<int, int>  MAP_ID;
    [XmlElementAttribute("EFECT")]
    list<EffectNode>  EFECT;
};

class EffectNode
{
    [XmlElementAttribute("ID")]
    int   ID;
    [XmlElementAttribute("BindPos")]
    int   BindPos;
};
