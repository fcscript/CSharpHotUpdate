
class  TestExport
{
    public enum ValueType
    {
        value_none = 0,
        value_int = 2,
        value_float = 3,
    };

    public TestExport();
    public TestCallback onPostRender { get;  set; }
    public void GetIntArray(List<int> pList){}
    public List<int> GetRefList(List<int> pList){ return null; }
    public T GetChild(StringA szName){ return default(T); }
    public T GetChild(){ return default(T); }
    public bool Equals(Object obj){ return default(bool); }
    public int GetHashCode(){ return default(int); }
    public Type GetType(){ return default(Type); }
    public StringA ToString(){ return default(string); }
};

