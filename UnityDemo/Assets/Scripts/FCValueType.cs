using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
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
    fc_value_color32,  // Color32
    fc_value_color,  // Color
    fc_value_intrect,  // IntRect
    fc_value_rect,  // Rect
    
    fc_value_system_object, // System.Object与
    fc_value_object, // 普通的类

    fc_value_enum,   // 枚举
    fc_value_int_ptr, // IntPtr

    fc_value_system_list,
    fc_value_system_map,  // Dictionary
    fc_value_system_array, // 原生数组
};

public enum fc_value_tempalte_type
{
    template_none,  // 普通变量
    template_array, // 数组
    template_list,  // list
    template_map,   // map/Dictionary
};

public class FCValueType
{
    public fc_value_tempalte_type m_nTemplateType;
    public fc_value_type m_nKeyType;   // key类型
    public fc_value_type m_nValueType; // value类型
    public Type m_key;
    public Type m_value;

    public FCValueType()
    {
        m_nTemplateType = fc_value_tempalte_type.template_none;
        m_nKeyType = fc_value_type.fc_value_unknow;
        m_nValueType = fc_value_type.fc_value_unknow;
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
    // 功能：分析类型
    public void SetType(Type nType)
    {
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
            Type[] argtypes = nType.GetGenericArguments(); // 模板的参数
            m_nTemplateType = fc_value_tempalte_type.template_list;
            m_key = m_value = argtypes[0];
            m_nKeyType = m_nValueType = GetBaseFCType(m_value);
            return;
        }
        if (szTypeName == "Dictionary`2"
            || szTypeName == "Dictionary`2&")
        {
            Type[] argtypes = nType.GetGenericArguments(); // 模板的参数
            m_nTemplateType = fc_value_tempalte_type.template_map;
            m_key = argtypes[0];
            m_value = argtypes[1];
            m_nKeyType = GetBaseFCType(m_key);
            m_nValueType = GetBaseFCType(m_value);
            return;
        }
        m_nTemplateType = fc_value_tempalte_type.template_none;
        m_key = m_value = nType;
        m_nKeyType = m_nValueType = GetBaseFCType(m_value);
    }
    public string GetKeyName(bool bCSharp, bool bFullName = false)
    {
        return GetBaseValueTypeName(m_nKeyType, bFullName ? m_key.FullName : m_key.Name, bCSharp);
    }
    public string GetValueName(bool bCSharp, bool bFullName = false)
    {
        return GetBaseValueTypeName(m_nValueType, bFullName ? m_value.FullName : m_value.Name, bCSharp);
    }
    // 得到类型的名字
    public string GetTypeName(bool bSharp, bool bFullName = false)
    {
        switch(m_nTemplateType)
        {
            case fc_value_tempalte_type.template_array:
                {
                    if (bSharp)
                        return bFullName ? (m_value.FullName + "[]") : (m_value.Name + "[]");
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
        }
        return GetBaseValueTypeName(m_nValueType, bFullName ? m_value.FullName : m_value.Name, bSharp);
    }
    public static fc_value_type GetBaseFCType(Type nType)
    {
        if (nType.Equals(typeof(int)) || nType.Equals(typeof(Int32)))
            return fc_value_type.fc_value_int;
        if (nType.Equals(typeof(float)))
            return fc_value_type.fc_value_float;
        if (nType.Equals(typeof(byte)))
            return fc_value_type.fc_value_byte;
        if (nType.Equals(typeof(char)))
            return fc_value_type.fc_value_char;
        if (nType.Equals(typeof(bool)))
            return fc_value_type.fc_value_bool;
        if (nType.Equals(typeof(short)) || nType.Equals(typeof(Int16)))
            return fc_value_type.fc_value_short;
        if (nType.Equals(typeof(ushort)) || nType.Equals(typeof(UInt16)))
            return fc_value_type.fc_value_ushort;
        if (nType.Equals(typeof(uint)) || nType.Equals(typeof(UInt32)))
            return fc_value_type.fc_value_uint;
        if (nType.Equals(typeof(long)) || nType.Equals(typeof(Int64)))
            return fc_value_type.fc_value_int64;
        if (nType.Equals(typeof(ulong)) || nType.Equals(typeof(UInt64)))
            return fc_value_type.fc_value_uint64;
        if (nType.Equals(typeof(double)))
            return fc_value_type.fc_value_double;
        if (nType.Equals(typeof(void)))
            return fc_value_type.fc_value_void;
        if (nType.Equals(typeof(string)))
            return fc_value_type.fc_value_string_a;
        if (nType.Equals(typeof(Vector2)))
            return fc_value_type.fc_value_vector2;
        if (nType.Equals(typeof(Vector3)))
            return fc_value_type.fc_value_vector3;
        if (nType.Equals(typeof(Vector4)))
            return fc_value_type.fc_value_vector4;
        if (nType.Equals(typeof(Plane)))
            return fc_value_type.fc_value_plane;
        if (nType.Equals(typeof(Bounds)))
            return fc_value_type.fc_value_bounds;
        if (nType.Equals(typeof(Matrix4x4)))
            return fc_value_type.fc_value_matrix;
        if (nType.Equals(typeof(Ray)))
            return fc_value_type.fc_value_ray;
        if (nType.Equals(typeof(Sphere)))
            return fc_value_type.fc_value_sphere;
        if (nType.Equals(typeof(Color32)))
            return fc_value_type.fc_value_color32;
        if (nType.Equals(typeof(Color)))
            return fc_value_type.fc_value_color;
        if (nType.Equals(typeof(IntRect)))
            return fc_value_type.fc_value_intrect;
        if (nType.Equals(typeof(Rect)))
            return fc_value_type.fc_value_rect;
        if (nType.Equals(typeof(System.Object)))
            return fc_value_type.fc_value_system_object;
        if (nType.IsEnum)
            return fc_value_type.fc_value_enum;
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

    // 功能：添加返回值
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
        if(IsBaseType(value.m_nValueType))
        {
            if(IsRefType(value.m_nValueType))
            {
                fileData.AppendFormat("{0}{1} temp_ret = {2};\r\n", szLeftEmpty, szType, szValueName);
                fileData.AppendFormat("{0}FCDll.PushReturnParam(ref temp_ret);\r\n", szLeftEmpty);
                return;
            }
            fileData.AppendFormat("{0}FCDll.PushReturnParam({1});\r\n", szLeftEmpty, szValueName);
            return;
        }
        fileData.AppendFormat("{0}long v = FCGetObj.PushObj({1});\r\n", szLeftEmpty, szValueName);
        fileData.AppendFormat("{0}FCDll.PushReturnParam(v);\r\n", szLeftEmpty);
    }
    // 功能：获取脚本的第N个参数
    public static string SetMemberValue(string szLeftEmpty, FCValueType value, string szLeftName, string Ptr, string szIndex, bool bTempValue, bool bOut)
    {
        // szLeftName = fc_get_bool(L, 0);
        string szFuncAppend = FCValueType.GetFCLibFuncShortName(value.m_nValueType);
        string szCSharpName = value.GetTypeName(true, true);
        string szDefine = string.Empty;
        if (bTempValue)
            szDefine = szCSharpName + " ";
        if(value.IsArray)
        {
            szDefine = szLeftEmpty + string.Format("{0} {1} = null;\r\n", szCSharpName, szLeftName);
            return szDefine + szLeftEmpty + string.Format("{0} = FCCustomParam.GetArray(ref {1},{2},{3});\r\n", szLeftName, szLeftName, Ptr, szIndex);
        }
        else if(value.IsList)
        {
            szDefine = szLeftEmpty + string.Format("{0} {1} = null;\r\n", szCSharpName, szLeftName);
            return szDefine + szLeftEmpty + string.Format("{0} = FCCustomParam.GetList(ref {1},{2},{3});\r\n", szLeftName, szLeftName, Ptr, szIndex);
        }
        else if(value.IsMap)
        {
            szDefine = szLeftEmpty + string.Format("{0} {1} = null;\r\n", szCSharpName, szLeftName);
            return szDefine + szLeftEmpty + string.Format("{0} = FCCustomParam.GetDictionary(ref {1},{2},{3});\r\n", szLeftName, szLeftName, Ptr, szIndex);
        }
        if(string.IsNullOrEmpty(szFuncAppend))
        {
            if (value.m_nValueType == fc_value_type.fc_value_enum)
            {
                return szLeftEmpty + string.Format("{0}{1} = ({2})(FCLibHelper.fc_get_intptr({3},{4}));\r\n", szDefine, szLeftName, szCSharpName, Ptr, szIndex);
            }
            if (value.m_nValueType == fc_value_type.fc_value_int_ptr)
            {
                return szLeftEmpty + string.Format("{0}{1} = ({2})(FCLibHelper.fc_get_intptr({3},{4}));\r\n", szDefine, szLeftName, szCSharpName, Ptr, szIndex);
            }
            if (value.m_nValueType == fc_value_type.fc_value_system_object)
            {
                return szLeftEmpty + string.Format("{0}{1} = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr({2},{3}));\r\n", szDefine, szLeftName, Ptr, szIndex);
            }
        }
        else
        {
            if(IsGraphicType(value.m_nValueType))
            {
                szDefine = szLeftEmpty + string.Format("{0} {1} = new {2}();\r\n", szCSharpName, szLeftName, szCSharpName);
                return szDefine + szLeftEmpty + string.Format("FCLibHelper.fc_get_{0}({1},{2},ref {3});\r\n", szFuncAppend, Ptr, szIndex, szLeftName);
            }
            else
            {
                return szLeftEmpty + string.Format("{0}{1} = FCLibHelper.fc_get_{2}({3},{4});\r\n", szDefine, szLeftName, szFuncAppend, Ptr, szIndex);
            }
        }        
        return szLeftEmpty + string.Format("{0}{1} = FCGetObj.GetObj<{2}>(FCLibHelper.fc_get_intptr({3},{4}));\r\n", szDefine, szLeftName, szCSharpName, Ptr, szIndex);
    }    
    public static string ModifyScriptCallParam(string szLeftEmpty, FCValueType value, string szLeftName, string Ptr, string szIndex, bool bTempValue)
    {
        if(value.IsArray)
        {
            return szLeftEmpty + string.Format("FCCustomParam.OutArray({0}, {1}, {2});\r\n", szLeftName, Ptr, szIndex);
        }
        else if(value.IsList)
        {
            return szLeftEmpty + string.Format("FCCustomParam.OutList({0}, {1}, {2});\r\n", szLeftName, Ptr, szIndex);
        }
        else if(value.IsMap)
        {
            return szLeftEmpty + string.Format("FCCustomParam.OutDictionary({0}, {1}, {2});\r\n", szLeftName, Ptr, szIndex);
        }
        return string.Empty;
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
        return nValueType == fc_value_type.fc_value_system_object || nValueType == fc_value_type.fc_value_object;
    }    

    // 功能：得到变量的类型名
    public static string GetBaseValueTypeName(fc_value_type nFCType, string szCSharpName, bool bCSharp)
    {
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
                return bCSharp ? "long": "int64";
            case fc_value_type.fc_value_uint64:
                return bCSharp ? "ulong" : "uint64";
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
                return "Matrix4x4";
            case fc_value_type.fc_value_ray:
                return "Ray";
            case fc_value_type.fc_value_sphere:
                return "Sphere";
            case fc_value_type.fc_value_color32:
                return "Color32";
            case fc_value_type.fc_value_color:
                return "Color";
            case fc_value_type.fc_value_intrect:
                return "IntRect";
            case fc_value_type.fc_value_rect:
                return "Rect";
            case fc_value_type.fc_value_system_object:
                return "System.Object";
            default:
                break;
        }
        szCSharpName = szCSharpName.Replace('+', '.');
        return szCSharpName;
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
                return "martix";
            case fc_value_type.fc_value_sphere:
                return "sphere";
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
}
