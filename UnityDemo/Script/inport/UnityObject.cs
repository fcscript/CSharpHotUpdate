using System;


class  UnityObject : Object
{
    public UnityObject(){}
    public StringA name { get;  set; }
    public HideFlags hideFlags { get;  set; }
    public static void Destroy(UnityObject obj,float t){}
    public static void Destroy(UnityObject obj){}
    public static void DestroyImmediate(UnityObject obj,bool allowDestroyingAssets){}
    public static void DestroyImmediate(UnityObject obj){}
    public static List<UnityObject> FindObjectsOfType(Type type){ return null; }
    public static void DontDestroyOnLoad(UnityObject target){}
    public static void DestroyObject(UnityObject obj,float t){}
    public static void DestroyObject(UnityObject obj){}
    public StringA ToString(){ return default(StringA); }
    public int GetInstanceID(){ return default(int); }
    public int GetHashCode(){ return default(int); }
    public bool Equals(Object other){ return default(bool); }
    public static UnityObject Instantiate(UnityObject original,Vector3 position,Quaternion rotation){ return default(UnityObject); }
    public static UnityObject Instantiate(UnityObject original,Vector3 position,Quaternion rotation,Transform parent){ return default(UnityObject); }
    public static UnityObject Instantiate(UnityObject original){ return default(UnityObject); }
    public static UnityObject Instantiate(UnityObject original,Transform parent){ return default(UnityObject); }
    public static UnityObject Instantiate(UnityObject original,Transform parent,bool instantiateInWorldSpace){ return default(UnityObject); }
    public static UnityObject FindObjectOfType(Type type){ return default(UnityObject); }
};

