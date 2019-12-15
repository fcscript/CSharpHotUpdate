using System;
using System.Collections.Generic;


class  Scene : ValueType
{
    public StringA path { get; }
    public StringA name { get; }
    public bool isLoaded { get; }
    public int buildIndex { get; }
    public bool isDirty { get; }
    public int rootCount { get; }
    public bool IsValid(){ return default(bool); }
    public List<GameObject> GetRootGameObjects(){ return null; }
    public void GetRootGameObjects(List<GameObject> rootGameObjects){}
    public int GetHashCode(){ return default(int); }
    public bool Equals(Object other){ return default(bool); }
};

