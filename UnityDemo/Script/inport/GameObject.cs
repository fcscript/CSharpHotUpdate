using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


class  GameObject : UnityObject
{
    public GameObject(StringA name){}
    public GameObject(){}
    public GameObject(StringA name,List<Type> components){}
    public Transform transform { get; }
    public int layer { get;  set; }
    public bool activeSelf { get; }
    public bool activeInHierarchy { get; }
    public bool isStatic { get;  set; }
    public StringA tag { get;  set; }
    public Scene scene { get; }
    public GameObject gameObject { get; }
    public static GameObject CreatePrimitive(PrimitiveType type){ return default(GameObject); }
    public Component GetComponent(Type type){ return default(Component); }
    public Component GetComponent(StringA type){ return default(Component); }
    public Component GetComponentInChildren(Type type,bool includeInactive){ return default(Component); }
    public Component GetComponentInChildren(Type type){ return default(Component); }
    public Component GetComponentInParent(Type type){ return default(Component); }
    public List<Component> GetComponents(Type type){ return null; }
    public void GetComponents(Type type,List<Component> results){}
    public List<Component> GetComponentsInChildren(Type type){ return null; }
    public List<Component> GetComponentsInChildren(Type type,bool includeInactive){ return null; }
    public List<Component> GetComponentsInParent(Type type){ return null; }
    public List<Component> GetComponentsInParent(Type type,bool includeInactive){ return null; }
    public void SetActive(bool value){}
    public bool CompareTag(StringA tag){ return default(bool); }
    public static GameObject FindGameObjectWithTag(StringA tag){ return default(GameObject); }
    public static GameObject FindWithTag(StringA tag){ return default(GameObject); }
    public static List<GameObject> FindGameObjectsWithTag(StringA tag){ return null; }
    public void SendMessageUpwards(StringA methodName,Object value,SendMessageOptions options){}
    public void SendMessageUpwards(StringA methodName,Object value){}
    public void SendMessageUpwards(StringA methodName){}
    public void SendMessageUpwards(StringA methodName,SendMessageOptions options){}
    public void SendMessage(StringA methodName,Object value,SendMessageOptions options){}
    public void SendMessage(StringA methodName,Object value){}
    public void SendMessage(StringA methodName){}
    public void SendMessage(StringA methodName,SendMessageOptions options){}
    public void BroadcastMessage(StringA methodName,Object parameter,SendMessageOptions options){}
    public void BroadcastMessage(StringA methodName,Object parameter){}
    public void BroadcastMessage(StringA methodName){}
    public void BroadcastMessage(StringA methodName,SendMessageOptions options){}
    public Component AddComponent(Type componentType){ return default(Component); }
    public static GameObject Find(StringA name){ return default(GameObject); }
};

