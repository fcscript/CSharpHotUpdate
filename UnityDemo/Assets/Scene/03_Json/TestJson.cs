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
        string szFileData;
        long nBeginTime = DateTime.Now.Ticks / 10000;
        int nTotalCount = 100;
        for (int i = 0; i < nTotalCount; ++i)
        {
            szFileData = JsonConvert.SerializeObject(m_data);
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
            string szData = JsonMapper.ToJson(m_data);
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("LitJson Save 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        m_ScriptLog.Add(szTips);
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
