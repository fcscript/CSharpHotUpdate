using System;
using System.Collections;


class  MonoBehaviour : Behaviour
{
    public MonoBehaviour(){}
    public bool useGUILayout { get;  set; }
    public void Invoke(StringA methodName,float time){}
    public void InvokeRepeating(StringA methodName,float time,float repeatRate){}
    public void CancelInvoke(){}
    public void CancelInvoke(StringA methodName){}
    public bool IsInvoking(StringA methodName){ return default(bool); }
    public bool IsInvoking(){ return default(bool); }
    public Coroutine StartCoroutine(IEnumerator routine){ return default(Coroutine); }
    public Coroutine StartCoroutine(StringA methodName,Object value){ return default(Coroutine); }
    public Coroutine StartCoroutine(StringA methodName){ return default(Coroutine); }
    public void StopCoroutine(StringA methodName){}
    public void StopCoroutine(IEnumerator routine){}
    public void StopCoroutine(Coroutine routine){}
    public void StopAllCoroutines(){}
    public static void print(Object message){}
};

