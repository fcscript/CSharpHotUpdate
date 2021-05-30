using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCGetObj
{
    // 基础数据类型
    enum FC_VALUE_TYPE
    {
        FC_VALUE_TYPE_Unknow,
        FC_VALUE_TYPE_bool,            // bool变量 支持(0， 1， TRUE， FALSE, true, false)
        FC_VALUE_TYPE_CHAR,            // char
        FC_VALUE_TYPE_BYTE,            // byte
        FC_VALUE_TYPE_WCHAR,           // wchar
        FC_VALUE_TYPE_SHORT,           // short
        FC_VALUE_TYPE_USHORT,          // unsigned short
        FC_VALUE_TYPE_INT,             // int
        FC_VALUE_TYPE_UINT,            // uint
        FC_VALUE_TYPE_FLOAT,           // float
        FC_VALUE_TYPE_DOUBLE,          // double
        FC_VALUE_TYPE_INT64,           // int64
        FC_VALUE_TYPE_UINT64,          // uint64
        FC_VALUE_TYPE_STRING_A,        // 字符串
        FC_VALUE_TYPE_STRING_W,        // 宽字符串
        FC_VALUE_TYPE_VOID,            // void指针(无类型)
        FC_VALUE_TYPE_OBJECT,          // class对象,  提供一个通用的FCObject关键字
        FC_VALUE_TYPE_FCTYPE,          // type类型, FCType type = typeof(xxxx);
        FC_VALUE_TYPE_FCOBJECT,        // 
        FC_VALUE_TYPE_INPORT_CLASS,    // 外部导入的class
        FC_VALUE_TYPE_SERIALIZE,       // CSerialize
        FC_VALUE_TYPE_MAP_ITERATOR,    // 迭代器iterator
        FC_VALUE_TYPE_FUNC_PTR,        // 函数指针
        FC_VALUE_TYPE_IENUMERATOR,     // IEnumerator

        FC_VALUE_TYPE_VECTOR2,         // Vector2
        FC_VALUE_TYPE_VECTOR3,         // Vector3
        FC_VALUE_TYPE_VECTOR4,         // Vector4
        FC_VALUE_TYPE_PANEL,           // Plane
        FC_VALUE_TYPE_MATRIX,          // Matrix
        FC_VALUE_TYPE_BOUNDBOX,        // BoundBox
        FC_VALUE_TYPE_RAY,             // Ray
        FC_VALUE_TYPE_FRUSTUMBOX,      // FrustumBox
        FC_VALUE_TYPE_SPHERE,          // Sphere
        FC_VALUE_TYPE_INT_RECT,        // IntRect
        FC_VALUE_TYPE_FLOAT_RECT,      // Rect{left, top, right, bottom} width;height;
        FC_VALUE_TYPE_COLOR,           // Color(r,g, b, a)
        FC_VALUE_TYPE_COLOR32,         // Color32(r,g,b,a)
        FC_VALUE_TYPE_QUATERNION,      // Quaternion
        FC_VALUE_TYPE_BEZIER2D,        // Bezier2D
        FC_VALUE_TYPE_BEZIER3D,        // Bezier3D

        FC_VALUE_TYPE_THIS,            // this 指针
        FC_VALUE_TYPE_RETURN,          // return 变量

        FC_VALUE_TYPE_INPORT_DELEGATE, // 外部导入的委托
        FC_VALUE_TYPE_TEMPLATE_PARAM,  // 模板参数

        FC_VALUE_TYPE_INT_PTR,         // 平台对象指针
    };

    // 扩展模板容器类型
    enum FC_TEMPLATE_TYPE
    {
        FC_TEMPLATE_NONE,
        FC_TEMPLATE_ARRAY,        // 数组(全部是动态可变长的数组)
        FC_TEMPLATE_HASH_MAP,     // hash_map
    };

    class FCRefObj
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

    public static void OnReloadScript()
    {
        m_AllObj.Clear();
        m_Obj2ID.Clear();
    }
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
    public static System.Object  GetSystemObj(long VM, long nIntPtr)
    {
        // 先检测这个对象是不是
        FC_TEMPLATE_TYPE nTemplateType = (FC_TEMPLATE_TYPE)FCLibHelper.fc_get_value_template_type(nIntPtr);
        FC_VALUE_TYPE nValueType = (FC_VALUE_TYPE)FCLibHelper.fc_get_value_type(nIntPtr);

        if(FC_TEMPLATE_TYPE.FC_TEMPLATE_NONE == nTemplateType)
        {
            if(nValueType == FC_VALUE_TYPE.FC_VALUE_TYPE_INPORT_CLASS)
            {
                FCRefObj ref_obj = null;
                if (m_AllObj.TryGetValue(nIntPtr, out ref_obj))
                {
                    return ref_obj.m_obj;
                }
            }
            switch(nValueType)
            {
                case FC_VALUE_TYPE.FC_VALUE_TYPE_bool:
                    return FCLibHelper.fc_get_value_bool(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_BYTE:
                    return FCLibHelper.fc_get_value_byte(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_CHAR:
                    return FCLibHelper.fc_get_value_char(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_SHORT:
                    return FCLibHelper.fc_get_value_short(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_USHORT:
                    return FCLibHelper.fc_get_value_ushort(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_INT:
                    return FCLibHelper.fc_get_value_int(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_UINT:
                    return FCLibHelper.fc_get_value_uint(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_FLOAT:
                    return FCLibHelper.fc_get_value_float(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_DOUBLE:
                    return FCLibHelper.fc_get_value_double(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_INT64:
                    return FCLibHelper.fc_get_value_int64(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_UINT64:
                    return FCLibHelper.fc_get_value_uint64(nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_STRING_A:
                    return FCLibHelper.fc_get_value_string_a(VM, nIntPtr);  // wrap接口中，其实只用到了这个
                case FC_VALUE_TYPE.FC_VALUE_TYPE_STRING_W:
                    return FCLibHelper.fc_get_value_string_a(VM, nIntPtr);
                case FC_VALUE_TYPE.FC_VALUE_TYPE_VOID:
                case FC_VALUE_TYPE.FC_VALUE_TYPE_INT_PTR:
                    return new IntPtr(nIntPtr);
                default:
                    break; // 其他的先暂不写了，代码太多了
            }
        }

        return null;
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
        if (obj == null)
            return 0;

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
