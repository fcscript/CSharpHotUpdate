using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using LitJson;

public class TestJson : FCScriptLoader
{
    public TextAsset m_ItemCfg;
    GoodsCfg m_data;

    MapData m_MapData;
    string m_szTestMapData;

    // 记录脚本的LOG
    protected override bool IsRecrodLog()
    {
        return true;
    }
    // Use this for initialization
    protected override void OnAfterLoadScriptData()
    {
        GoodsCfg data = JsonConvert.DeserializeObject<GoodsCfg>(m_ItemCfg.text);
        m_data = data;
    }

    void  NetJsonLoad()
    {
        // 测试空函数调用
        long nBeginTime = DateTime.Now.Ticks / 10000;
        int nTotalCount = 100;
        for (int i = 0; i < nTotalCount; ++i)
        {
            GoodsCfg data = JsonConvert.DeserializeObject<GoodsCfg>(m_ItemCfg.text);
            m_data = data;
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("json.net 读取, 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        m_ScriptLog.Add(szTips);
    }
    void NetJsonSave()
    {
        if (m_data == null)
        {
            m_data = JsonConvert.DeserializeObject<GoodsCfg>(m_ItemCfg.text);
        }

        // 测试空函数调用
        long nBeginTime = DateTime.Now.Ticks / 10000;
        int nTotalCount = 100;
        for (int i = 0; i < nTotalCount; ++i)
        {
            JsonConvert.SerializeObject(m_data);
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("json.net 写入, 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        m_ScriptLog.Add(szTips);
    }

    void FCLoadJson()
    {
        string szJson = m_ItemCfg.text;

        int nTotalCount = 100;
        long nBeginTime = DateTime.Now.Ticks / 10000;
        for (int i = 0; i < nTotalCount; ++i)
        {
            FCLibHelper.fc_push_string_a(szJson);
            FCLibHelper.fc_call(0, "JsonLoader.TestLoad");
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("FC Load Json 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        print_error(szTips);
    }
    void FCSaveJson()
    {
        string szJson = m_ItemCfg.text;
        long nBeginTime = DateTime.Now.Ticks / 10000;
        int nTotalCount = 100;
        FCLibHelper.fc_push_string_a(szJson);
        FCLibHelper.fc_call(0, "JsonLoader.TestWrite");
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("FC Save Json 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        print_error(szTips);
    }
    void LitJsonLoad()
    {
        // 测试空函数调用
        long nBeginTime = DateTime.Now.Ticks / 10000;
        int nTotalCount = 100;
        for (int i = 0; i < nTotalCount; ++i)
        {
            GoodsCfg data = JsonMapper.ToObject<GoodsCfg>(m_ItemCfg.text);
            m_data = data;
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("LitJson Load 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        m_ScriptLog.Add(szTips);
    }
    void LitJsonSave()
    {
        if (m_data == null)
        {
            m_data = JsonMapper.ToObject<GoodsCfg>(m_ItemCfg.text);
        }
        // 测试空函数调用
        long nBeginTime = DateTime.Now.Ticks / 10000;
        int nTotalCount = 100;
        for (int i = 0; i < nTotalCount; ++i)
        {
            JsonMapper.ToJson(m_data);
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("LitJson Save 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        m_ScriptLog.Add(szTips);
    }

    void LitJsonLoadMapData()
    {
        string szJson = MakeTestMapData();
        long nBeginTime = DateTime.Now.Ticks / 10000;
        int nTotalCount = 10;
        for (int i = 0; i < nTotalCount; ++i)
        {
            MapData data = JsonMapper.ToObject<MapData>(szJson);
            m_MapData = data;
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("LitJson Load 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        m_ScriptLog.Add(szTips);
    }

    void NetJsonLoadMapData()
    {
        // 测试空函数调用
        string szJson = MakeTestMapData();
        long nBeginTime = DateTime.Now.Ticks / 10000;
        int nTotalCount = 10;
        for (int i = 0; i < nTotalCount; ++i)
        {
            MapData data = JsonConvert.DeserializeObject<MapData>(szJson);
            m_MapData = data;
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("json.net 读取, 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        m_ScriptLog.Add(szTips);
    }

    void FCLoadMapData()
    {
        string szJson = MakeTestMapData();

        int nTotalCount = 10;
        long nBeginTime = DateTime.Now.Ticks / 10000;
        for (int i = 0; i < nTotalCount; ++i)
        {
            FCLibHelper.fc_push_string_a(szJson);
            FCLibHelper.fc_call(0, "JsonLoaderMapData.TestLoad");
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("FC Load Json 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        print_error(szTips);
    }

    string MakeTestMapData()
    {
        if(!string.IsNullOrEmpty(m_szTestMapData))
        {
            return m_szTestMapData;
        }
        MapData mapData = new MapData();
        mapData.grids = new List<MapNodeData>();
        mapData.grids.Capacity = 100000;
        MapNodeData node = null;
        for (int i = 0; i< 100000; ++i )
        {
            node = new MapNodeData();
            node.Level = UnityEngine.Random.Range(1, 10);
            node.ResType = UnityEngine.Random.Range(3, 6);
            node.Type = UnityEngine.Random.Range(1, 5);
            node.x = UnityEngine.Random.Range(1, 100);
            node.y = UnityEngine.Random.Range(1, 100);
            mapData.grids.Add(node);
        }
        string szText = JsonMapper.ToJson(mapData);
        m_szTestMapData = szText;
        return m_szTestMapData;
        //System.IO.File.WriteAllText("E:/mapdata.txt", szText);
    }

    void  FillMatrix(ref Matrix4x4 mat1)
    {
        mat1.m00 = 0; mat1.m01 = 1; mat1.m02 = 2; mat1.m03 = 3;
        mat1.m10 = 10; mat1.m11 = 11; mat1.m12 = 12; mat1.m13 = 13;
        mat1.m20 = 20; mat1.m21 = 21; mat1.m22 = 22; mat1.m23 = 23;
        mat1.m30 = 30; mat1.m31 = 31; mat1.m32 = 32; mat1.m33 = 33;
    }
    void OffsetMatrix(ref Matrix4x4 mat1, float fOffset)
    {
        mat1.m00 += fOffset; mat1.m01 += fOffset; mat1.m02 += fOffset; mat1.m02 += fOffset;
        mat1.m10 += fOffset; mat1.m11 += fOffset; mat1.m12 += fOffset; mat1.m12 += fOffset;
        mat1.m20 += fOffset; mat1.m21 += fOffset; mat1.m22 += fOffset; mat1.m22 += fOffset;
        mat1.m30 += fOffset; mat1.m31 += fOffset; mat1.m32 += fOffset; mat1.m32 += fOffset;
    }
        
    void OnGUI()
    {
        int nLeft = 200;
        int nTop = 200;
        nLeft = 200;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "清空LOG"))
        {
            m_ScriptLog.Clear();
        }
        nLeft = 200;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "json.net读"))
        {
            NetJsonLoad();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "json.net写"))
        {
            NetJsonSave();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "FC读Json"))
        {
            FCLoadJson();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "FC写Json"))
        {
            FCSaveJson();
        }
        nLeft = 200;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "LitJson读"))
        {
            LitJsonLoad();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "LitJson写"))
        {
            LitJsonSave();
        }
        nLeft = 200;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 150.0f, 30.0f), "LitJson读10W数据"))
        {
            LitJsonLoadMapData();
        }
        nLeft += 180;
        if (GUI.Button(new Rect(nLeft, nTop, 150.0f, 30.0f), "FC读10W数据"))
        {
            FCLoadMapData();
        }
        nLeft += 180;
        if (GUI.Button(new Rect(nLeft, nTop, 150.0f, 30.0f), "JsonNet读10W数据"))
        {
            NetJsonLoadMapData();
        }
        float fy = 10.0f;
        float fWidth = Screen.width - fy - 10;
        List<string> aLog = ScriptLog;
        for (int i = 0; i < aLog.Count; ++i)
        {
            GUI.Label(new Rect(10.0f, fy, fWidth, 20.0f), aLog[i]);
            fy += 25;
        }
    }
}
