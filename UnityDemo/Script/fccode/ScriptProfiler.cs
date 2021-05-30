using System.Collections;

class ScriptAttrib
{
    public int HP;
    public int MP;
}

class ScriptItem
{
    public int ID;
    public StringA Name;
    public ScriptAttrib Attrib = new ScriptAttrib();
}

class ScriptObj
{
    public ScriptItem Item = new ScriptItem();
    public static void Func1()
    {

    }
}

class Monster : ScriptObj
{
    public const int Numb = 10;
    public static void Func1()
    {

    }
}

//------------------脚本调用C#侧接口
[export]
class ScriptProfiler
{
    static uint s_nTotalTime = 0;
    static uint s_nTestCount = 0;
    static StringA s_nLastFunc;
    public static void PrintTime(StringA szFunc, uint nTime)
    {
        if (s_nLastFunc != szFunc)
        {
            s_nTotalTime = 0;
            s_nTestCount = 0;
            s_nLastFunc = szFunc;
        }
        s_nTotalTime += nTime;
        ++s_nTestCount;
        os.print("{0}花费时间：{1}秒{2}毫秒, 平均：{3}毫秒, 共{4}次", szFunc, nTime / 1000, nTime % 1000, s_nTotalTime / s_nTestCount, s_nTestCount);
    }

    //------------------脚本调用C#侧接口
    public static void S2C_Test1()
    {
        UserClass obj = new UserClass();
        uint nBegin = os.GetTickCount();
        for (int i = 0; i < 200000; ++i)
        {
            obj.S2C_Test1();
        }
        uint nEnd = os.GetTickCount();
        PrintTime("S2C_Test1", nEnd - nBegin);
    }

    public void S2C_Test2()
    {
        UserClass obj = new UserClass();
        uint nBegin = os.GetTickCount();
        for (int i = 0; i < 200000; ++i)
        {
            obj.S2C_Test2(i, i, i, i, i);
        }
        uint nEnd = os.GetTickCount();
        PrintTime("S2C_Test2", nEnd - nBegin);
    }

    public void S2C_Test3()
    {
        UserClass obj = new UserClass();
        long k = 100;
        uint nBegin = os.GetTickCount();
        for (int i = 0; i < 200000; ++i)
        {
            obj.S2C_Test3(i, k, k, k, k);
        }
        uint nEnd = os.GetTickCount();
        PrintTime("S2C_Test3", nEnd - nBegin);
    }

    public void S2C_Test4()
    {
        UserClass obj = new UserClass();
        float k = 100.0f;
        uint nBegin = os.GetTickCount();
        for (int i = 0; i < 200000; ++i)
        {
            obj.S2C_Test4(i, k, k, k, k);
        }
        uint nEnd = os.GetTickCount();
        PrintTime("S2C_Test4", nEnd - nBegin);
    }

    public void S2C_Test5()
    {
        UserClass obj = new UserClass();
        string k = "fkwejgfiejtgkkdfjig";
        uint nBegin = os.GetTickCount();
        for (int i = 0; i < 200000; ++i)
        {
            obj.S2C_Test5(i, k, k, k, k);
        }
        uint nEnd = os.GetTickCount();
        PrintTime("S2C_Test5", nEnd - nBegin);
    }
    //------------------脚本调用C#侧接口

    //------------------C#侧调用脚本接口
    public int C2S_Test1()
    {
        return 0;
    }
    public int C2S_Test2(int Arg0, int Arg1, int Arg2, int Arg3, int Arg4)
    {
        return Arg0;
    }
    public int C2S_Test3(int Arg0, long Arg1, long Arg2, long Arg3, long Arg4)
    {
        return Arg0;
    }
    public int S2C_Test4(int Arg0, float Arg1, float Arg2, float Arg3, float Arg4)
    {
        return Arg0;
    }
    public int S2C_Test5(int Arg0, StringA Arg1, StringA Arg2, StringA Arg3, StringA Arg4)
    {
        return Arg0;
    }
    //------------------C#侧调用脚本接口

    //------------------测试面向对象，成员变量访问性能
    public void ObjectRead()
    {
        ScriptObj Obj = new ScriptObj();
        StringA name;
        int nTotal = 0;
        uint nBegin = os.GetTickCount();
        for (int i = 0; i < 200000; ++i)
        {
            name = Obj.Item.Name;
            nTotal += Obj.Item.Attrib.MP + Obj.Item.Attrib.HP;
        }
        uint nEnd = os.GetTickCount();
        PrintTime("ObjectRead", nEnd - nBegin);
    }
    public void ObjectWrite()
    {
        ScriptObj Obj = new ScriptObj();
        StringA name = "Obj";
        uint nBegin = os.GetTickCount();
        for (int i = 0; i < 200000; ++i)
        {
            Obj.Item.Name = name;
            Obj.Item.Attrib.MP = i;
            Obj.Item.Attrib.HP = i;
        }
        uint nEnd = os.GetTickCount();
        PrintTime("ObjectWrite", nEnd - nBegin);
    }

    //------------------测试多态，面向对象接口
}
