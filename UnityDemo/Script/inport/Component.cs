using UnityEngine;
using System;
using System.Collections.Generic;


class  Component : UnityObject
{
    public Component(){}
    public Transform transform { get; }
    public GameObject gameObject { get; }
    public StringA tag { get;  set; }
    public Component GetComponent(Type type){ return default(Component); }
    public T GetComponent<T>(){ return default(T); }
    public Component GetComponent(StringA type){ return default(Component); }
    public Component GetComponentInChildren(Type t,bool includeInactive){ return default(Component); }
    public Component GetComponentInChildren(Type t){ return default(Component); }
    public List<Component> GetComponentsInChildren(Type t){ return null; }
    public List<Component> GetComponentsInChildren(Type t,bool includeInactive){ return null; }
    public Component GetComponentInParent(Type t){ return default(Component); }
    public List<Component> GetComponentsInParent(Type t){ return null; }
    public List<Component> GetComponentsInParent(Type t,bool includeInactive){ return null; }
    public List<Component> GetComponents(Type type){ return null; }
    public void GetComponents(Type type,List<Component> results){}
    public bool CompareTag(StringA tag){ return default(bool); }
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
};

