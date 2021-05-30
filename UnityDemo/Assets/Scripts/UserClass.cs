using System;
using UnityEngine;
using System.Threading.Tasks;

[AutoWrap]
public class UserClass
{
    public static void TestFunc1(int num, string str, Transform trans)
    {
    }
    public static void TestFunc2(int num, string str, Vector3 pos, Transform trans)
    {
        trans.position = pos;
    }
    public static bool MandelbrotCheck(float x, float y)
    {
        return (x * x + y * y) < 4.0;
    }
    // 空函数
    public void S2C_Test1()
    {
    }
    public int S2C_Test2(int Arg0, int Arg1, int Arg2, int Arg3, int Arg4)
    {
        return Arg0;
    }
    public int S2C_Test3(int Arg0, long Arg1, long Arg2, long Arg3, long Arg4)
    {
        return Arg0;
    }
    public int S2C_Test4(int Arg0, float Arg1, float Arg2, float Arg3, float Arg4)
    {
        return Arg0;
    }
    public int S2C_Test5(int Arg0, string Arg1, string Arg2, string Arg3, string Arg4)
    {
        return Arg0;
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
    [ManualWrap]
    public static Task<GameObject> WaitLoadPrefab(string szAssetName, float fWaitTime)
    {
        // 手动注册的接口
        // 功能：等待加载事件，将当前脚本挂起
        // 说明：这个函数并没有真正的实现，真正在实现在 await_script_loader.WaitLoadPrefab_wrap
        //       这个只是脚本中的一个函数声明而已

        // 注意，这里面的代码并不会执行噢，
        GameObject obj = new GameObject(szAssetName);
        Task<GameObject> r = Task.Run(() =>
        {
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