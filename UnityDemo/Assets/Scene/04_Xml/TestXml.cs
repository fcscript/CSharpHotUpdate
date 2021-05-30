using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestXml : FCScriptLoader
{
    public TextAsset m_XmlData;

    // 记录脚本的LOG
    protected override bool IsRecrodLog()
    {
        return true;
    }
    void FCLoadXml()
    {
        string szFileData = m_XmlData.text;

        int nTotalCount = 1;
        long nBeginTime = DateTime.Now.Ticks / 10000;
        for (int i = 0; i < nTotalCount; ++i)
        {
            FCLibHelper.fc_push_string_a(m_VMPtr, szFileData);
            FCLibHelper.fc_call(m_VMPtr, 0, "XmlLoader.TestLoad");
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("FC Load Xml 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        print_error(szTips);
    }
    void FCSaveXml()
    {
        string szFileData = m_XmlData.text;

        int nTotalCount = 1;
        long nBeginTime = DateTime.Now.Ticks / 10000;
        for (int i = 0; i < nTotalCount; ++i)
        {
            FCLibHelper.fc_push_string_a(m_VMPtr, szFileData);
            FCLibHelper.fc_call(m_VMPtr, 0, "XmlLoader.TestWrite");
        }
        long nEndTime = DateTime.Now.Ticks / 10000;
        long nCostTime = nEndTime - nBeginTime;
        string szTips = string.Format("FC Save Xml 花费总时间={0}毫秒,平均时间={1}毫秒,总调用次数={2}", nCostTime, nCostTime / nTotalCount, nTotalCount);
        print_error(szTips);
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
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "Xml读取"))
        {
            FCLoadXml();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "Xml写入"))
        {
            FCSaveXml();
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
