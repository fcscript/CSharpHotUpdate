using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GoodsData
{
    public string Quality;
    public string IconName;
    public string TextName;
    public string OrigName;
    public string Index;
};

class GoodsList
{
    public List<GoodsData> goods;
};

class GoodsCfg
{
    public GoodsList list;
}