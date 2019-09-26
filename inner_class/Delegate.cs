using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 协程对象
public class FCEnumerator : System.Collections.IEnumerator
{
    public FCEnumerator(System.Object obj, params object[] args)
    {
    }
    System.Object System.Collections.IEnumerator.Current
    {
        get
        {
            return null;
        }
    }
    void Dispose()
    {
    }
    // 兼容接口，请不要调用
    [Obsolete("兼容接口，请不要调用", false)]
    public bool MoveNext()
    {
        return true;
    }
    // 兼容接口，请不要调用
    [Obsolete("兼容接口，请不要调用", false)]
    public void Reset()
    {
    }
    // 功能：启动协程
    public void Start() { }
    // 功能：停止协程
    public void Stop() { }
    // 功能：唤醒睡眠的协程
    public void Wakeup() { }

    // 功能：启动一个协程
    public static FCEnumerator StartCoroutine(FCEnumerator ins)
    {
        return ins;
    }
    // 功能：启动一个协程
    public static FCEnumerator StartCoroutine(System.Object obj, params System.Object[] args)
    {
        // 如 : FCEnumerator.StartCoroutine(ins, func(函数参数));
        FCEnumerator ins = new FCEnumerator(obj, args);
        return ins;
    }
    // 功能：停止协程
    public static void StopCoroutine(FCEnumerator ins)
    {
    }
    // 功能：通过函数名停止
    // 说明：szFuncName - 函数名(必须是字符串常量才可以的噢)
    public static void StopCoroutine(string szFuncName)
    {
    }
    // 功能：通过类名+成员函数名来停止
    // 说明：szClassName - 类名(必须是字符串常量才可以的噢)
    //       szFuncName - 函数名(必须是字符串常量才可以的噢)
    public static void StopCoroutine(string szClassName, string szFuncName)
    {
    }
    // 功能：停止所有协程
    public static void StopAllCoroutine()
    {
    }
};
public sealed class WaitForSeconds
{
    public WaitForSeconds(float fTime)
    {
    }
}

public class Delegate
{
    public Delegate(System.Object obj, params System.Object[] args)
    {
    }
    public void Release() { }
    public void Copy(Delegate other) { }
    // 功能：执行委托的函数
    public void Call() { }
    // 功能：修改委托的参数
    public void SetParam(int nIndex, System.Object param)
    {
    }
}