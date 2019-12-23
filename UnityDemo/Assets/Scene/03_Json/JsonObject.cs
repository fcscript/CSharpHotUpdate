using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GoodsData
{
    public string Quality = string.Empty;
    public string IconName = string.Empty;
    public string TextName = string.Empty;
    public string OrigName = string.Empty;
    public string Index = string.Empty;
};

class GoodsList
{
    public List<GoodsData> goods = new List<GoodsData>();
};

class GoodsCfg
{
    public GoodsList list = new GoodsList();
}

class MapNodeData
{
    public int Level;
    public int ResType;
    public int Type;
    public int x;
    public int y;
}
class MapData
{
    public List<MapNodeData> grids = new List<MapNodeData>();
}