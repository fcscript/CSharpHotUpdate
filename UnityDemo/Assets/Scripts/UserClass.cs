using UnityEngine;

[AutoWrap]
public class UserClass
{
    static int s_num;
    static string s_str;
    public static void TestFunc1(int num, string str, Vector3 pos, Transform trans)
    {
        s_num = num;
        s_str = str;

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