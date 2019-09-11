using System;
using System.Threading.Tasks;


class  TestExport : Object
{
    public enum ValueType
    {
        value_none = 0,
        value_int = 2,
        value_float = 3,
    };

    public TestExport(){}
    public static TestCallback onPostRender { set; }
    public TestCallback onOwnPostRender { set; }
    public TestCallback2 onCallFunc2 { set; }
    public static Task<int> AsnycLoad(StringA szName){ return null; }
    public static Task<int> LoadResource(StringA szName){ return null; }
};

