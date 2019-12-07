using System;
using UnityEngine;
using System.Threading.Tasks;

[AutoWrap]
public class UserClass
{
    public static void TestFunc1(int num, string str, Vector3 pos, Transform trans)
    {
        trans.position = pos;
    }
}

[AutoWrap]
public class TestD  // 测试导出功能的类，没有实际意义
{
    [DontWrap]
    public int m_nValue;
    public int SetValue(int nValue)
    {
        return m_nValue;
    }
    [DontWrap]
    public void Update()
    {

    }
    public static Task<int>  LoadPrefab(string szAssetName)
    {
        Task<int> r = Task.Run(() =>
        {
            System.Threading.Thread.Sleep(3000);
            return 1;
        });
        return r;
    }
    public static Task<GameObject> LoadPrefabObj(string szAssetName)
    {
        GameObject obj = new GameObject(szAssetName);
        Task<GameObject> r = Task.Run(() =>
        {
            System.Threading.Thread.Sleep(3000);
            return obj;
        });
        return r;
    }
}

[PartWrap]
public class TestPart // 测试导出功能的类，没有实际意义
{
    public int m_nValue;
    [PartWrap]
    public void SetValue(int nValue)
    {
        m_nValue = nValue;
    }
}

public class TestFunc
{
    public void WaitWhile(Func<int> predicate)
    {
    }
}