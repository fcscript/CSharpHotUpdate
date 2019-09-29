
[json]
class GoodsData
{
    public int Quality;
    public string IconName;
    public string TextName;
    public string OrigName;
    public uint Index;
};

[json]
class GoodsList
{
    public List<GoodsData> goods;
};

[json]
class ItemCfg
{
    public GoodsList list;
}

[export]
class JsonLoader
{
    public static void TestLoad(StringA szJson)
    {
        ItemCfg data = null;
        bool bSuc = os.ReadJson(data, szJson);
        StringA szData = os.WriteJson(data);
        //StringA szSub = szData.Left(100);
        //os.print(szSub);
        //StringA szLeft = szJson.Mid(10, 100);
        //os.print(szLeft);
    }
}