using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;


public enum  fc_value_type
{
    fc_value_unknow,
    fc_value_bool,
    fc_value_char,
    fc_value_byte,
    fc_value_short,
    fc_value_ushort,
    fc_value_int,
    fc_value_uint,
    fc_value_float,
    fc_value_double,
    fc_value_int64,
    fc_value_uint64,
    fc_value_string_a,
    fc_value_void,
    fc_value_vector2,
    fc_value_vector3,
    fc_value_vector4,
    fc_value_plane,   // Plane
    fc_value_bounds,  // Bounds
    fc_value_matrix,  // Matrix4x4
    fc_value_ray,  // Ray
    fc_value_sphere,  // Sphere
    fc_value_quaternion, // Quaternion
    fc_value_color32,  // Color32
    fc_value_color,  // Color
    fc_value_intrect,  // IntRect
    fc_value_rect,  // Rect
    
    fc_value_system_object, //
    fc_value_unity_object,  // Unity.Object
    fc_value_object, //

    fc_value_enum,   //
    fc_value_int_ptr, // IntPtr
    
    fc_value_delegate,  //
    fc_value_action,    // Action
    fc_value_unity_action, // UnityAction
    fc_value_task,         // Task<>
};

public enum fc_value_tempalte_type
{
    template_none,  //
    template_array, //
    template_list,  // list
    template_map,   // map/Dictionary
    template_task,  // task<Type>
    template_ienumerable, // IEnumerable<Type>
};

public class FCValueType
{
    public fc_value_tempalte_type m_nTemplateType;
    public fc_value_type m_nKeyType;   //
    public fc_value_type m_nValueType; //
    public Type m_key;
    public Type m_value;
    public bool m_bRef;

    public FCValueType()
    {
        m_nTemplateType = fc_value_tempalte_type.template_none;
        m_nKeyType = fc_value_type.fc_value_unknow;
        m_nValueType = fc_value_type.fc_value_unknow;
        m_bRef = false;
    }
    public FCValueType(Type nType)
    {
        SetType(nType);
    }
    public bool IsArray
    {
        get { return fc_value_tempalte_type.template_array == m_nTemplateType; }
    }
    public bool IsList
    {
        get { return fc_value_tempalte_type.template_list == m_nTemplateType; }
    }
    public bool IsMap
    {
        get { return fc_value_tempalte_type.template_map == m_nTemplateType; }
    }
    public bool IsRef
    {
        get { return m_bRef; }
    }
    public void SetType(Type nType)
    {
        m_bRef = nType.IsByRef;
        if(m_bRef)
        {
            Type nRealType = nType.GetElementType();
            if (nRealType != null)
            {
                nType = nRealType;
            }
        }

        if(nType.IsArray)
        {
            m_nTemplateType = fc_value_tempalte_type.template_array;
            m_key = m_value = nType.GetElementType();
            m_nKeyType = m_nValueType = GetBaseFCType(m_value);
            return;
        }

        string szTypeName = nType.Name;
        string szSpaceName = nType.Namespace;
        if (szTypeName == "List`1"
            || szTypeName == "List`1&")
        {
            Type[] argtypes = nType.GetGenericArguments();
            m_nTemplateType = fc_value_tempalte_type.template_list;
            m_key = m_value = argtypes[0];
            m_nKeyType = m_nValueType = GetBaseFCType(m_value);
            return;
        }
        if (szTypeName == "Dictionary`2"
            || szTypeName == "Dictionary`2&")
        {
            Type[] argtypes = nType.GetGenericArguments();
            m_nTemplateType = fc_value_tempalte_type.template_map;
            m_key = argtypes[0];
            m_value = argtypes[1];
            m_nKeyType = GetBaseFCType(m_key);
            m_nValueType = GetBaseFCType(m_value);
            return;
        }
        if(szTypeName == "Task`1")
        {
            Type[] argtypes = nType.GetGenericArguments();
            m_nTemplateType = fc_value_tempalte_type.template_task;
            m_key = m_value = argtypes[0];
            m_nKeyType = m_nValueType = GetBaseFCType(m_value);
            return;
        }
        if(szTypeName == "IEnumerable`1")
        {
            Type[] argtypes = nType.GetGenericArguments();
            m_nTemplateType = fc_value_tempalte_type.template_ienumerable;
            m_key = m_value = argtypes[0];
            m_nKeyType = m_nValueType = GetBaseFCType(m_value);
            return;
        }
        m_nTemplateType = fc_value_tempalte_type.template_none;
        m_key = m_value = nType;
        m_nKeyType = m_nValueType = GetBaseFCType(m_value);
    }
    public string GetKeyName(bool bCSharp, bool bFullName = false)
    {
        return GetBaseValueTypeName(m_nKeyType, m_key, bCSharp, bFullName);
    }
    public string GetValueName(bool bCSharp, bool bFullName = false)
    {
        if (s_TypeFullName != null)
        {
            string szFullName = string.Empty;
            if (s_TypeFullName.TryGetValue(m_value, out szFullName))
                return szFullName;
        }
        return GetBaseValueTypeName(m_nValueType, m_value, bCSharp, bFullName);
    }
    public string GetTypeName(bool bSharp, bool bFullName = false)
    {
        switch(m_nTemplateType)
        {
            case fc_value_tempalte_type.template_array:
                {
                    if (bSharp)
                        return GetValueName(bSharp, bFullName) + "[]";
                    else
                        return string.Format("List<{0}>", GetKeyName(bSharp, bFullName));
                }
            case fc_value_tempalte_type.template_list:
                return string.Format("List<{0}>", GetKeyName(bSharp, bFullName));
            case fc_value_tempalte_type.template_map:
                if(bSharp)
                    return string.Format("Dictionary<{0},{1}>", GetKeyName(bSharp, bFullName), GetValueName(bSharp, bFullName));
                else
                    return string.Format("map<{0},{1}>", GetKeyName(bSharp, bFullName), GetValueName(bSharp, bFullName));
            case fc_value_tempalte_type.template_task:
                return string.Format("Task<{0}>", GetKeyName(bSharp, bFullName));
            case fc_value_tempalte_type.template_ienumerable:
                return string.Format("IEnumerable<{0}>", GetKeyName(bSharp, bFullName));
        }
        return GetBaseValueTypeName(m_nValueType, m_value, bSharp, bFullName);
    }
    public string GetDelegateName(bool bSharp)
    {
        return GetDelegateTypeName(m_value, bSharp);
    }
    public static string GetClassName(Type nType, bool bCSharp = false)
    {
        if (nType == typeof(UnityEngine.Object))
            return "UnityObject";
        else
        {
            if(bCSharp)
            {
                string szName = nType.FullName;
                szName = szName.Replace('+', '.');
                return szName;
            }
            return nType.Name;
        }
    }
    static string GetDelegateTypeName(Type nType, bool bSharp)
    {
        string szName = nType.Name;
        switch (szName)
        {
            case "Action":
            case "UnityAction":
                return szName;
            default:
                break;
        }
        int nPos = szName.IndexOf('`');
        if (nPos != -1)
        {
            szName = szName.Replace("`", "");
        }
        string szCSharpName = szName.Replace('+', '.');
        return szCSharpName;
    }

    static Dictionary<Type, string> s_TypeFullName;
    public static void RegisterTypeFullName(Type nType ,string szFullName)
    {
        if (s_TypeFullName == null)
            s_TypeFullName = new Dictionary<Type, string>();
        s_TypeFullName[nType] = szFullName;
    }
    static Dictionary<Type, FCValueType> s_TransType;
    public static FCValueType TransType(Type nType)
    {
        if (s_TransType == null)
            s_TransType = new Dictionary<Type, FCValueType>();
        FCValueType value = null;
        if (s_TransType.TryGetValue(nType, out value))
            return value;
        value = new FCValueType(nType);
        s_TransType[nType] = value;
        return value;
    }

    static Dictionary<Type, fc_value_type> s_BaseTypeFinder;
    static void  InitBaseTypeFinder()
    {
        if (s_BaseTypeFinder != null)
            return;
        s_BaseTypeFinder = new Dictionary<Type, fc_value_type>();
        s_BaseTypeFinder[typeof(byte)] = fc_value_type.fc_value_byte;
        s_BaseTypeFinder[typeof(char)] = fc_value_type.fc_value_char;
        s_BaseTypeFinder[typeof(bool)] = fc_value_type.fc_value_bool;
        s_BaseTypeFinder[typeof(Boolean)] = fc_value_type.fc_value_bool;
        s_BaseTypeFinder[typeof(short)] = fc_value_type.fc_value_short;
        s_BaseTypeFinder[typeof(Int16)] = fc_value_type.fc_value_short;
        s_BaseTypeFinder[typeof(ushort)] = fc_value_type.fc_value_ushort;
        s_BaseTypeFinder[typeof(UInt16)] = fc_value_type.fc_value_ushort;
        s_BaseTypeFinder[typeof(int)] = fc_value_type.fc_value_int;
        s_BaseTypeFinder[typeof(Int32)] = fc_value_type.fc_value_int;
        s_BaseTypeFinder[typeof(uint)] = fc_value_type.fc_value_uint;
        s_BaseTypeFinder[typeof(UInt32)] = fc_value_type.fc_value_uint;
        s_BaseTypeFinder[typeof(float)] = fc_value_type.fc_value_float;
        s_BaseTypeFinder[typeof(Single)] = fc_value_type.fc_value_float;
        s_BaseTypeFinder[typeof(double)] = fc_value_type.fc_value_double;
        s_BaseTypeFinder[typeof(Double)] = fc_value_type.fc_value_double;
        s_BaseTypeFinder[typeof(long)] = fc_value_type.fc_value_int64;
        s_BaseTypeFinder[typeof(Int64)] = fc_value_type.fc_value_int64;
        s_BaseTypeFinder[typeof(ulong)] = fc_value_type.fc_value_uint64;
        s_BaseTypeFinder[typeof(UInt64)] = fc_value_type.fc_value_uint64;
        s_BaseTypeFinder[typeof(void)] = fc_value_type.fc_value_void;
        s_BaseTypeFinder[typeof(string)] = fc_value_type.fc_value_string_a;
        s_BaseTypeFinder[typeof(String)] = fc_value_type.fc_value_string_a;

        s_BaseTypeFinder[typeof(Vector2)] = fc_value_type.fc_value_vector2;
        s_BaseTypeFinder[typeof(Vector3)] = fc_value_type.fc_value_vector3;
        s_BaseTypeFinder[typeof(Vector4)] = fc_value_type.fc_value_vector4;
        s_BaseTypeFinder[typeof(Plane)] = fc_value_type.fc_value_plane;
        s_BaseTypeFinder[typeof(Bounds)] = fc_value_type.fc_value_bounds;
        s_BaseTypeFinder[typeof(Matrix4x4)] = fc_value_type.fc_value_matrix;
        s_BaseTypeFinder[typeof(Ray)] = fc_value_type.fc_value_ray;
        s_BaseTypeFinder[typeof(Sphere)] = fc_value_type.fc_value_sphere;
        s_BaseTypeFinder[typeof(Quaternion)] = fc_value_type.fc_value_quaternion;
        s_BaseTypeFinder[typeof(Color32)] = fc_value_type.fc_value_color32;
        s_BaseTypeFinder[typeof(Color)] = fc_value_type.fc_value_color;
        s_BaseTypeFinder[typeof(IntRect)] = fc_value_type.fc_value_intrect;
        s_BaseTypeFinder[typeof(Rect)] = fc_value_type.fc_value_rect;
        s_BaseTypeFinder[typeof(System.Object)] = fc_value_type.fc_value_system_object;
        s_BaseTypeFinder[typeof(UnityEngine.Object)] = fc_value_type.fc_value_unity_object;
        s_BaseTypeFinder[typeof(Task)] = fc_value_type.fc_value_task;
    }

    public static fc_value_type GetBaseFCType(Type nType)
    {
        if(s_BaseTypeFinder == null)
        {
            InitBaseTypeFinder();
        }
        fc_value_type nFCType = fc_value_type.fc_value_unknow;
        if(s_BaseTypeFinder.TryGetValue(nType, out nFCType))
        {
            return nFCType;
        }
        if (nType.IsEnum)
            return fc_value_type.fc_value_enum;
        if(typeof(System.MulticastDelegate).IsAssignableFrom(nType)
            || nType == typeof(System.MulticastDelegate))
        {
            return fc_value_type.fc_value_delegate;
        }        
        return fc_value_type.fc_value_object;
    }
    public static bool  IsRefType(fc_value_type nType)
    {
        switch(nType)
        {
            case fc_value_type.fc_value_vector2:
            case fc_value_type.fc_value_vector3:
            case fc_value_type.fc_value_vector4:
            case fc_value_type.fc_value_plane:
            case fc_value_type.fc_value_bounds:
            case fc_value_type.fc_value_matrix:
            case fc_value_type.fc_value_ray:
            case fc_value_type.fc_value_sphere:
            case fc_value_type.fc_value_quaternion:
            case fc_value_type.fc_value_color32:
            case fc_value_type.fc_value_color:
            case fc_value_type.fc_value_intrect:
            case fc_value_type.fc_value_rect:
                return true;
            default:
                break;                
        }
        return false;
    }
        
    public static void PushReturnValue(StringBuilder fileData, string szLeftEmpty, FCValueType value, string Ptr, string szValueName, bool bAttrib)
    {
        // fc_push_return_intptr(L, pPtr.nPtr);
        if(value.IsArray)
        {
            fileData.AppendFormat("{0}FCCustomParam.ReturnArray({1},{2});\r\n", szLeftEmpty, szValueName, Ptr);
            return;
        }
        else if(value.IsList)
        {
            fileData.AppendFormat("{0}FCCustomParam.ReturnList({1},{2});\r\n", szLeftEmpty, szValueName, Ptr);
            return;
        }
        else if(value.IsMap)
        {
            fileData.AppendFormat("{0}FCCustomParam.ReturnDictionary({1},{2});\r\n", szLeftEmpty, szValueName, Ptr);
            return;
        }        
        string szType = value.GetTypeName(true, true);
        //fileData.AppendFormat("{0}long ret_ptr = FCLibHelper.fc_get_return_ptr(L);\r\n", szLeftEmpty);
        if (IsBaseType(value.m_nValueType))
        {
            string szFuncAddr = GetFCLibFuncShortName(value.m_nValueType);
            if (IsRefType(value.m_nValueType))
            {
                fileData.AppendFormat("{0}{1} temp_ret = {2};\r\n", szLeftEmpty, szType, szValueName);
                fileData.AppendFormat("{0}FCLibHelper.fc_set_value_{1}({2}, ref temp_ret);\r\n", szLeftEmpty, szFuncAddr, Ptr);
                return;
            }
            fileData.AppendFormat("{0}FCLibHelper.fc_set_value_{1}({2}, {3});\r\n", szLeftEmpty, szFuncAddr, Ptr, szValueName);
            return;
        }
        fileData.AppendFormat("{0}long v = FCGetObj.PushObj({1});\r\n", szLeftEmpty, szValueName);
        fileData.AppendFormat("{0}FCLibHelper.fc_set_value_intptr({1}, v);\r\n", szLeftEmpty, Ptr);
    }
    public static void OutputRefScriptParam(StringBuilder fileData, string szLeftEmpty, FCValueType value, string szLeftName, string Ptr, string szIndex, bool bTempValue)
    {
        if(value.IsArray)
        {
            fileData.AppendFormat("{0}FCCustomParam.OutArray({1}, {2}, {3});\r\n", szLeftEmpty, szLeftName, Ptr, szIndex);
            return;
        }
        else if(value.IsList)
        {
            fileData.AppendFormat("{0}FCCustomParam.OutList({1}, {2}, {3});\r\n", szLeftEmpty, szLeftName, Ptr, szIndex);
            return;
        }
        else if(value.IsMap)
        {
            fileData.AppendFormat("{0}FCCustomParam.OutDictionary({1}, {2}, {3});\r\n", szLeftEmpty, szLeftName, Ptr, szIndex);
            return;
        }
        // 
        string szOutPtr = string.Format("nOutPtr{0}", szIndex);
        string szValueName = string.Format("arg{0}", szIndex);
        fileData.AppendFormat("{0}long {1} = FCLibHelper.fc_get_param_ptr({2}, {3});\r\n", szLeftEmpty, szOutPtr, Ptr, szIndex);
        //string szType = value.GetTypeName(true, true);
        if (IsBaseType(value.m_nValueType))
        {
            string szFuncAddr = GetFCLibFuncShortName(value.m_nValueType);
            if (IsRefType(value.m_nValueType))
            {
                fileData.AppendFormat("{0}FCLibHelper.fc_set_value_{1}({2}, ref {3});\r\n", szLeftEmpty, szFuncAddr, szOutPtr, szValueName);
            }
            else
                fileData.AppendFormat("{0}FCLibHelper.fc_set_value_{1}({2}, {3});\r\n", szLeftEmpty, szFuncAddr, szOutPtr, szValueName);
            return;
        }
        fileData.AppendFormat("{0}FCLibHelper.fc_set_value_intptr({1}, FCGetObj.PushObj({2}));\r\n", szLeftEmpty, szOutPtr, szValueName);
    }

    public static bool IsBaseType(fc_value_type nValueType)
    {
        return nValueType < fc_value_type.fc_value_system_object;
    }

    public static bool IsGraphicType(fc_value_type nValueType)
    {
        return nValueType != fc_value_type.fc_value_object && nValueType >= fc_value_type.fc_value_vector2;
    }
    public static bool IsObjectType(fc_value_type nValueType)
    {
        return nValueType == fc_value_type.fc_value_system_object || nValueType == fc_value_type.fc_value_object || nValueType == fc_value_type.fc_value_unity_object;
    }    
    
    public static string GetBaseValueTypeName(fc_value_type nFCType, Type nType, bool bCSharp, bool bFullName)
    {
        if(nType.IsByRef)
        {
            Type nRealType = nType.GetElementType();
            if (nRealType != null)
            {
                FCValueType v = TransType(nRealType);
                nFCType = v.m_nValueType;
                nType = nRealType;
            }
        }

        switch(nFCType)
        {
            case fc_value_type.fc_value_bool:
                return "bool";
            case fc_value_type.fc_value_byte:
                return "byte";
            case fc_value_type.fc_value_char:
                return "char";
            case fc_value_type.fc_value_short:
                return "short";
            case fc_value_type.fc_value_ushort:
                return "ushort";
            case fc_value_type.fc_value_int:
                return "int";
            case fc_value_type.fc_value_uint:
                return "uint";
            case fc_value_type.fc_value_float:
                return "float";
            case fc_value_type.fc_value_double:
                return "double";
            case fc_value_type.fc_value_int64:
                return bCSharp ? "long": "long";
            case fc_value_type.fc_value_uint64:
                return bCSharp ? "ulong" : "ulong";
            case fc_value_type.fc_value_string_a:
                return bCSharp ? "string" : "StringA";
            case fc_value_type.fc_value_void:
                return "void";
            case fc_value_type.fc_value_vector2:
                return "Vector2";
            case fc_value_type.fc_value_vector3:
                return "Vector3";
            case fc_value_type.fc_value_vector4:
                return "Vector4";
            case fc_value_type.fc_value_plane:
                return "Plane";
            case fc_value_type.fc_value_bounds:
                return "Bounds";
            case fc_value_type.fc_value_matrix:
                return bCSharp ? "Matrix4x4" : "Matrix";
            case fc_value_type.fc_value_ray:
                return "Ray";
            case fc_value_type.fc_value_sphere:
                return "Sphere";
            case fc_value_type.fc_value_quaternion:
                return "Quaternion";
            case fc_value_type.fc_value_color32:
                return "Color32";
            case fc_value_type.fc_value_color:
                return "Color";
            case fc_value_type.fc_value_intrect:
                return "IntRect";
            case fc_value_type.fc_value_rect:
                return "Rect";
            case fc_value_type.fc_value_system_object:
                return bCSharp ? "System.Object" : "Object";
            case fc_value_type.fc_value_unity_object:
                return bCSharp ? "UnityObject" : "UnityObject";
            //return bCSharp ? "UnityEngine.Object" : "UnityObject";
            case fc_value_type.fc_value_delegate:
                {
                    return GetDeleteExportName(nType, bCSharp, bFullName);
                }
            case fc_value_type.fc_value_task:
                return "Task";
            default:
                break;
        }
        string szCSharpName = bFullName ? nType.FullName : nType.Name;
        if(string.IsNullOrEmpty(szCSharpName))
        {
            return string.Empty;
        }
        if(!bFullName && !bCSharp)
        {            
            if(nType.IsNested && nType.IsEnum)
            {
                string szFullName = nType.FullName;
                int nIndex = szFullName.LastIndexOf('.');
                if(nIndex != -1)
                {
                    szCSharpName = szFullName.Substring(nIndex + 1);
                }
            }
        }

        szCSharpName = szCSharpName.Replace('+', '.');
        szCSharpName = szCSharpName.Replace("&", "");
        return szCSharpName;
    }

    static string  GetDeleteExportName(Type nType, bool bCSharp, bool bFullName)
    {
        string szName = bFullName ? nType.FullName : nType.Name;
        bool bBaseDelegate = false;
        bool bFuncDelegate = false;
        if (szName.IndexOf("Action`") != -1)
        {
            bBaseDelegate = true;
            szName = "Action";
        }
        else if (szName.IndexOf("UnityAction`") != -1)
        {
            bBaseDelegate = true;
            szName = "UnityAction";
        }
        else if (szName.IndexOf("Func`") != -1)
        {
            bFuncDelegate = true;
            bBaseDelegate = true;
            szName = "Func";
        }
        if (bBaseDelegate)
        {
            MethodInfo method = nType.GetMethod("Invoke");
            ParameterInfo[] allParams = method.GetParameters();
            if (allParams != null)
            {
                szName += "<";
                for (int i = 0; i < allParams.Length; ++i)
                {
                    FCValueType value_param = TransType(allParams[i].ParameterType);
                    if (i > 0)
                        szName += ",";
                    szName += value_param.GetTypeName(true, true);
                }
                if(bFuncDelegate)
                {
                    if(allParams.Length > 0)
                        szName += ",";
                    FCValueType value_param = TransType(method.ReturnType);
                    szName += value_param.GetTypeName(true, true);
                }
                szName += ">";
            }
        }
        szName = szName.Replace('+', '.');
        return szName;
    }
    
    public static string GetFCLibFuncShortName(fc_value_type nType)
    {
        switch(nType)
        {
            case fc_value_type.fc_value_bool:
                return "bool";
            case fc_value_type.fc_value_char:
                return "char";
            case fc_value_type.fc_value_byte:
                return "byte";
            case fc_value_type.fc_value_short:
                return "short";
            case fc_value_type.fc_value_ushort:
                return "ushort";
            case fc_value_type.fc_value_int:
                return "int";
            case fc_value_type.fc_value_uint:
                return "uint";
            case fc_value_type.fc_value_float:
                return "float";
            case fc_value_type.fc_value_double:
                return "double";
            case fc_value_type.fc_value_int64:
                return "int64";
            case fc_value_type.fc_value_uint64:
                return "uint64";
            case fc_value_type.fc_value_string_a:
                return "string_a";
            case fc_value_type.fc_value_vector2:
                return "vector2";
            case fc_value_type.fc_value_vector3:
                return "vector3";
            case fc_value_type.fc_value_vector4:
                return "vector4";
            case fc_value_type.fc_value_plane:
                return "plane";
            case fc_value_type.fc_value_ray:
                return "ray";
            case fc_value_type.fc_value_matrix:
                return "matrix";
            case fc_value_type.fc_value_sphere:
                return "sphere";
            case fc_value_type.fc_value_quaternion:
                return "quaternion";
            case fc_value_type.fc_value_bounds:
                return "bounds";
            case fc_value_type.fc_value_rect:
                return "rect";
            case fc_value_type.fc_value_intrect:
                return "intrect";
            case fc_value_type.fc_value_color:
                return "color";
            case fc_value_type.fc_value_color32:
                return "color32";
            //case fc_value_type.fc_value_system_object:
            //case fc_value_type.fc_value_object:
            //    return "intptr";
            default:
                break;
        }
        return string.Empty;
    }
    public static string GetFCLibFuncShortName(Type nType)
    {
        return GetFCLibFuncShortName(GetBaseFCType(nType));
    }
    public static FieldInfo[] GetFields(Type nType, bool bOnlyThisAPI)
    {
        if (bOnlyThisAPI)
            return nType.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
        else
            return nType.GetFields();
    }
    public static PropertyInfo[] GetProperties(Type nType, bool bOnlyThisAPI)
    {
        if (bOnlyThisAPI)
            return nType.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        else
            return nType.GetProperties();
    }
    public static MethodInfo[] GetMethods(Type nType, bool bOnlyThisAPI)
    {
        if (bOnlyThisAPI)
            return nType.GetMethods(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        else
            return nType.GetMethods();
    }
    public static string GetMethodDeclare(MethodInfo method)
    {
        string szFullName = method.Name;
        ParameterInfo[] allParams = method.GetParameters();
        string szCallParam = string.Empty;
        if (allParams != null)
        {
            Type nParamType;
            for (int i = 0; i < allParams.Length; ++i)
            {
                nParamType = allParams[i].ParameterType;
                if (i > 0)
                {
                    szCallParam += ',';
                }
                FCValueType value = TransType(nParamType);
                szCallParam += value.GetTypeName(false);
                szCallParam += " ";
                szCallParam += allParams[i].Name;
            }
        }
        return szFullName + szCallParam;
    }
    static bool  IsCanReplaceMethod(MethodInfo method_old, MethodInfo method_new)
    {
        ParameterInfo[] allParams1 = method_old.GetParameters();
        ParameterInfo[] allParams2 = method_new.GetParameters();
        Type nParamType1;
        Type nParamType2;
        for (int i = 0; i<allParams1.Length; ++i)
        {
            nParamType1 = allParams1[i].ParameterType;
            nParamType2 = allParams2[i].ParameterType;
            FCValueType value1 = TransType(nParamType1);
            FCValueType value2 = TransType(nParamType2);
            if(value1.IsArray && value2.IsList)
            {
                return true;
            }
        }
        return false;
    }
    public static void  ReplaceMethod(Dictionary<string, MethodInfo> methodFlags, List<MethodInfo> curMethods, string szDeclareName, MethodInfo method)
    {
        MethodInfo old = null;
        if(methodFlags.TryGetValue(szDeclareName, out old))
        {
            if(IsCanReplaceMethod(old, method))
            {
                for(int i = 0; i<curMethods.Count; ++i)
                {
                    if(curMethods[i] == old)
                    {
                        curMethods[i] = method;
                    }
                }
            }
        }
    }
    public static string GetDelegateName(Type nParamType)
    {
        FCValueType v = TransType(nParamType);
        string szName = v.GetValueName(true);
        return "UnityAction_" + szName + "_delegate";
    }
    public static Type GetDelegeteType(Type nClassType, Type nParamType)
    {
        FCValueType v = TransType(nParamType);
        switch (v.m_nValueType)
        {
            case fc_value_type.fc_value_bool:
                return typeof(UnityEngine.Events.UnityAction<bool>);
            case fc_value_type.fc_value_byte:
                return typeof(UnityEngine.Events.UnityAction<byte>);
            case fc_value_type.fc_value_char:
                return typeof(UnityEngine.Events.UnityAction<char>);
            case fc_value_type.fc_value_short:
                return typeof(UnityEngine.Events.UnityAction<short>);
            case fc_value_type.fc_value_ushort:
                return typeof(UnityEngine.Events.UnityAction<ushort>);
            case fc_value_type.fc_value_int:
                return typeof(UnityEngine.Events.UnityAction<int>);
            case fc_value_type.fc_value_uint:
                return typeof(UnityEngine.Events.UnityAction<uint>);
            case fc_value_type.fc_value_int64:
                return typeof(UnityEngine.Events.UnityAction<long>);
            case fc_value_type.fc_value_uint64:
                return typeof(UnityEngine.Events.UnityAction<ulong>);
            case fc_value_type.fc_value_float:
                return typeof(UnityEngine.Events.UnityAction<float>);
            case fc_value_type.fc_value_double:
                return typeof(UnityEngine.Events.UnityAction<double>);
            case fc_value_type.fc_value_string_a:
                return typeof(UnityEngine.Events.UnityAction<string>);
            case fc_value_type.fc_value_vector2:
                return typeof(UnityEngine.Events.UnityAction<Vector2>);
            case fc_value_type.fc_value_vector3:
                return typeof(UnityEngine.Events.UnityAction<Vector3>);
            default:
                break;
        }
        return nClassType;
    }
}
