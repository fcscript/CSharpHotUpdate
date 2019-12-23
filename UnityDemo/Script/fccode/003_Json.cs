﻿
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
        //StringA szData = os.WriteJson(data);
        //StringA szSub = szData.Left(100);
        //os.print(szSub);
        //StringA szLeft = szJson.Mid(10, 100);
        //os.print(szLeft);
    }
    public static void TestWrite(StringA szJson)
    {
        ItemCfg data = null;
        bool bSuc = os.ReadJson(data, szJson);

        StringA szData;
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100; ++i)
        {
            szData = os.WriteJson(data, 400*1024);
        }
        uint nEndTime = os.GetTickCount();
        uint nTotalTime = nEndTime - nStartTime;
        //os.print("WriteJson , Total Time:{0}, av:{1}", nTotalTime, nTotalTime / 100);
        //StringA szLeft = szData.Mid(0, 100);
        //os.print(szLeft);
    }
}

[json]
class MapNodeData
{
    // {"l":9,"rt":4,"t":1,"x":300,"y":300}
    public int Level;
    public int ResType;
    public int Type;
    public int x;
    public int y;
}
[json]
class MapData
{
    public List<MapNodeData> grids;
}
[export]
class JsonLoaderMapData
{
    public static void TestLoad(StringA szJson)
    {
        MapData data = null;
        bool bSuc = os.ReadJson(data, szJson);
        //if(data != null && data.grids != null)
        //{
        //    MapNodeData node = data.grids[0];
        //    os.print("Level:{0},ResType={1},Type:{2},x:{3}, y{4}", node.Level, node.ResType, node.Type, node.x, node.y);
        //    os.print("size = {0}", data.grids.Length);
        //}
    }
}