using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCGetObj
{
    class  FCRefObj
    {
        public int m_nRef = 0;
        public int m_bNew = 0;
        public long m_nPtr = 0;
        public Type m_nType;
        public System.Object m_obj;
    }

    static Dictionary<long, FCRefObj> m_AllObj = new Dictionary<long, FCRefObj>();
    static Dictionary<System.Object, FCRefObj> m_Obj2ID = new Dictionary<object, FCRefObj>();
    static long m_nObjID = 0;

    public static _Ty  GetObj<_Ty>(long  nIntPtr)// where _Ty : class
    {
        FCRefObj ref_obj = null;
        if(m_AllObj.TryGetValue(nIntPtr, out ref_obj))
        {
            _Ty ret = (_Ty)ref_obj.m_obj;
            return ret;
        }
        return default(_Ty);
    }
    //public static _Ty GetStructObj<_Ty>(long nIntPtr)// where _Ty : struct
    //{
    //    FCRefObj ref_obj = null;
    //    if (m_AllObj.TryGetValue(nIntPtr, out ref_obj))
    //    {
    //        Type nType = typeof(_Ty);
    //        if (nType.Equals(ref_obj.m_nType))
    //        {
    //            _Ty ret = (_Ty)ref_obj.m_obj;
    //            return ret;
    //        }
    //    }
    //    return default(_Ty);
    //}
    public static long  NewObj<_Ty>() where _Ty : new() //where _Ty : class, new()
    {
        FCRefObj ref_obj = new FCRefObj();
        ref_obj.m_nType = typeof(_Ty);
        ref_obj.m_nRef = 1;
        ref_obj.m_bNew = 1;
        ref_obj.m_obj = new _Ty();
        long nPtr = ++m_nObjID;
        ref_obj.m_nPtr = nPtr;
        m_AllObj[nPtr] = ref_obj;
        m_Obj2ID[ref_obj.m_obj] = ref_obj;
        return nPtr;
    }
    // 功能：添加一个对象
    // 说明：这里并不检测容器是不是已经缓存该对象，那样效率不高，但这个接口也可能造成误用
    // 比如在脚本中每调用一次get_obj接口，就会生成一个FCRefObj对象, 如果连续调用多次，会造成瞬时内存增长
    // 解决方法是可以像ulua一样，添加一个反向列表，通过obj查找已经存在的IntPtr, 但这个会增加额外的开销
    public static long PushObj<_Ty>(_Ty  obj )// where _Ty : class
    {
        FCRefObj ref_obj;
        if (m_Obj2ID.TryGetValue(obj, out ref_obj))
        {
            ref_obj.m_nRef++;    // 增加一下引用计数
            return ref_obj.m_nPtr;
        }
        ref_obj = new FCRefObj();
        ref_obj.m_nType = obj != null ? obj.GetType() : typeof(_Ty);
        ref_obj.m_nRef = 1;
        ref_obj.m_obj = obj;
        ref_obj.m_bNew = 0;
        long nPtr = ++m_nObjID;
        ref_obj.m_nPtr = nPtr;
        m_AllObj[nPtr] = ref_obj;
        m_Obj2ID[obj] = ref_obj;
        return nPtr;
    }
    // 功能:添加一个new出来的对象
    public static long PushNewObj<_Ty>(_Ty obj)
    {
        FCRefObj ref_obj = new FCRefObj();
        ref_obj.m_nType = typeof(_Ty);
        ref_obj.m_nRef = 1;
        ref_obj.m_obj = obj;
        ref_obj.m_bNew = 1;
        long nPtr = ++m_nObjID;
        m_AllObj[nPtr] = ref_obj;
        m_Obj2ID[obj] = ref_obj;
        return nPtr;
    }
    public static long PushObj(Type nType)
    {
        FCRefObj ref_obj = new FCRefObj();
        ref_obj.m_nType = nType;
        ref_obj.m_nRef = 1;
        ref_obj.m_obj = nType;
        long nPtr = ++m_nObjID;
        m_AllObj[nPtr] = ref_obj;
        m_Obj2ID[ref_obj.m_obj] = ref_obj;
        return nPtr;
    }
    // 功能：调用delete删除对象，这个对象是由new 出来的
    public static void DelObj(long nIntPtr)
    {
        ReleaseRef(nIntPtr);
    }
    public static void ReleaseRef(long nIntPtr)
    {
        FCRefObj ref_obj = null;
        if (m_AllObj.TryGetValue(nIntPtr, out ref_obj))
        {
            ref_obj.m_nRef--;
            if(0 == ref_obj.m_nRef)
            {
                m_AllObj.Remove(nIntPtr);
                if (ref_obj.m_obj != null)
                    m_Obj2ID.Remove(ref_obj.m_obj);
                // 尝试释放
                if (ref_obj.m_bNew != 0)
                {
                    TryDestoryObject(ref_obj);
                }
                ref_obj.m_obj = null;
            }
        }
    }
    static void  TryDestoryObject(FCRefObj  ref_obj)
    {
        if(ref_obj.m_nType == typeof(GameObject))
        {
            GameObject obj = (GameObject)ref_obj.m_obj;
            if(obj != null)
                GameObject.DestroyImmediate(obj);
        }
    }
}
