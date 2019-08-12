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
    fc_value_object, // Object

    fc_value_enum,   // 枚举
    fc_value_int_ptr, // IntPtr

    fc_value_system_list,
    fc_value_system_map,  // Dictionary
    fc_value_system_array, // 原生数组
};

public class FCValueType
{
    // 功能：添加返回值
    public static string PushReturnValue(string szLeftEmpty, Type nValueType, string Ptr, string szValueName, bool bAttrib)
    {
        // fc_push_return_intptr(L, pPtr.nPtr);
        fc_value_type nFCType = fc_value_type.fc_value_object;
        string szType = GetTypeDescEx(nValueType, ref nFCType);
        if(IsBaseType(nFCType))
        {
            switch(nFCType)
            {
                case fc_value_type.fc_value_vector2:
                case fc_value_type.fc_value_vector3:
                case fc_value_type.fc_value_vector4:
                case fc_value_type.fc_value_plane:
                case fc_value_type.fc_value_bounds:
                case fc_value_type.fc_value_matrix:
                case fc_value_type.fc_value_ray:
                case fc_value_type.fc_value_sphere:
                case fc_value_type.fc_value_color:
                case fc_value_type.fc_value_intrect:
                case fc_value_type.fc_value_rect:
                    {
                        string szDefine = szLeftEmpty + string.Format("{0} temp_ret = {1};\r\n", szType, szValueName);
                        return szDefine + szLeftEmpty + "FCDll.PushReturnParam(ref temp_ret);";
                    }
                default:
                    break;
            }
            return szLeftEmpty + string.Format("FCDll.PushReturnParam({0});", szValueName);
        }
        
        if (IsObjectType(nFCType))
        {
            return szLeftEmpty + string.Format("FCLibHelper.fc_push_return_intptr(L, FCGetObj.PushObj({0}));", szValueName);
        }

        if (nFCType == fc_value_type.fc_value_system_list)
        {
            return szLeftEmpty + string.Format("FCCustomParam.PushReturnList({0});", szValueName);
        }
        if (nFCType == fc_value_type.fc_value_system_map)
        {
            return szLeftEmpty + string.Format("FCCustomParam.PushReturnDictionary({0});", szValueName);
        }
        return string.Empty;
    }
    // 功能：获取脚本的第N个参数
    public static string SetMemberValue(string szLeftEmpty, Type nValueType, string szLeftName, string Ptr, string szIndex, bool bTempValue)
    {
        // szLeftName = fc_get_bool(L, 0);
        fc_value_type nFCType = fc_value_type.fc_value_object;
        string szType = GetTypeDescEx(nValueType, ref nFCType);
        string szCSharpName = GetCSharpTypeName(nValueType);
        string szDefine = string.Empty;
        if (bTempValue)
            szDefine = szCSharpName + " ";
        if (IsBaseType(nFCType))
        {
            if (nFCType == fc_value_type.fc_value_string_a)
            {
                return szLeftEmpty + string.Format("{0}{1} = FCLibHelper.fc_get_string_a({2}, {3});\r\n", szDefine, szLeftName, Ptr, szIndex);
            }
            szType = szType.ToLower();
            if(IsGraphicType(nFCType))
            {
                szDefine = szLeftEmpty + string.Format("{0} {1} = new {2}();\r\n", szCSharpName, szLeftName, szCSharpName);
                return szDefine + szLeftEmpty + string.Format("FCLibHelper.fc_get_{0}({1},{2},ref {3});\r\n", szType, Ptr, szIndex, szLeftName);
            }
            else
                return szLeftEmpty + string.Format("{0}{1} = FCLibHelper.fc_get_{2}({3},{4});\r\n", szDefine, szLeftName, szType, Ptr, szIndex);
        }
        if(nFCType == fc_value_type.fc_value_enum)
        {
            return szLeftEmpty + string.Format("{0}{1} = ({2})(FCLibHelper.fc_get_intptr({3},{4}));\r\n", szDefine, szLeftName, szCSharpName, Ptr, szIndex);
        }
        if(nFCType == fc_value_type.fc_value_int_ptr)
        {
            return szLeftEmpty + string.Format("{0}{1} = ({2})(FCLibHelper.fc_get_intptr({3},{4}));\r\n", szDefine, szLeftName, szCSharpName, Ptr, szIndex);
        }
        if(nFCType == fc_value_type.fc_value_system_object)
        {
            return szLeftEmpty + string.Format("{0}{1} = FCGetObj.GetObj<System.Object>(FCLibHelper.fc_get_intptr({2},{3}));\r\n", szDefine, szLeftName, Ptr, szIndex);
        }
        if(nFCType == fc_value_type.fc_value_system_list)
        {
            szDefine = szLeftEmpty + string.Format("{0} {1} = null;\r\n", szCSharpName, szLeftName);
            return szDefine + szLeftEmpty + string.Format("{0} = FCCustomParam.GetList(ref {1},{2},{3});\r\n", szLeftName, szLeftName, Ptr, szIndex);
        }
        if (nFCType == fc_value_type.fc_value_system_map)
        {
            szDefine = szLeftEmpty + string.Format("{0} {1} = null;\r\n", szCSharpName, szLeftName);
            return szDefine + szLeftEmpty + string.Format("{0} = FCCustomParam.GetDictionary(ref {1},{2},{3});\r\n", szLeftName, szLeftName, Ptr, szIndex);
        }
        if(nFCType == fc_value_type.fc_value_system_array)
        {
            szDefine = szLeftEmpty + string.Format("{0} {1} = null;\r\n", szCSharpName, szLeftName);
            return szDefine + szLeftEmpty + string.Format("{0} = FCCustomParam.GetArray(ref {1},{2},{3});\r\n", szLeftName, szLeftName, Ptr, szIndex);
        }
                
        return szLeftEmpty + string.Format("{0}{1} = FCGetObj.GetObj<{2}>(FCLibHelper.fc_get_intptr({3},{4}));\r\n", szDefine, szLeftName, szCSharpName, Ptr, szIndex);
    }    

    // 功能：得到脚本中对应的类型名字
    public static string GetTypeDesc(Type nType)
    {
        fc_value_type nFCType = fc_value_type.fc_value_object;
        return GetTypeDescEx(nType, ref nFCType);
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

    // 功能：得到C#变量的类型名字
    public static string GetCSharpTypeName(Type nType)
    {
        if (nType.Equals(typeof(System.Object)))
            return "System.Object";
        string szTypeName = nType.Name;
        string szSpaceName = nType.Namespace;
        if (!string.IsNullOrEmpty(szSpaceName))
            szSpaceName = szSpaceName + '.';
        if (szTypeName == "List`1")
        {
            Type[] argtypes = nType.GetGenericArguments(); // 模板的参数
            fc_value_type subtype = fc_value_type.fc_value_unknow;
            string szSubName = GetTypeDescEx(argtypes[0], ref subtype);
            return string.Format("List<{0}>", szSubName);
        }
        if (szTypeName == "Dictionary`2")
        {
            Type[] argtypes = nType.GetGenericArguments(); // 模板的参数
            string szKeyName = GetCSharpTypeName(argtypes[0]);
            string szValueName = GetCSharpTypeName(argtypes[1]);
            return string.Format("Dictionary<{0}, {1}>", szKeyName, szValueName);
        }
        if(nType.IsEnum)
        {
            Type RefType = nType.ReflectedType;
            if(RefType != null)
            {
                szSpaceName = RefType.FullName + '.';
            }
            return szSpaceName + szTypeName;
        }
        if (nType.IsArray)
        {
            Type nElementType = nType.GetElementType();
            string szElementName = GetCSharpTypeName(nElementType);
            return string.Format("{0}[]", szElementName);
        }
        // 自定义的模板暂时不支持的噢

        fc_value_type nFCType = fc_value_type.fc_value_object;
        GetTypeDescEx(nType, ref nFCType);
        return GetBaseValueTypeName(nFCType, nType.Name, true);
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
            default:
                break;
        }
        return szCSharpName;
    }

    // 功能：得到脚本中的数据类型与名字
    public static string GetTypeDescEx(Type nType, ref fc_value_type nFCType)
    {
        string szType = nType.ToString();
        if (nType.Equals(typeof(int)))
        {
            nFCType = fc_value_type.fc_value_int;
            return "int";
        }
        if (nType.Equals(typeof(float)))
        {
            nFCType = fc_value_type.fc_value_float;
            return "float";
        }
        if (nType.Equals(typeof(byte)))
        {
            nFCType = fc_value_type.fc_value_byte;
            return "byte";
        }
        if (nType.Equals(typeof(char)))
        {
            nFCType = fc_value_type.fc_value_char;
            return "char";
        }
        if (nType.Equals(typeof(bool)))
        {
            nFCType = fc_value_type.fc_value_bool;
            return "bool";
        }
        if (nType.Equals(typeof(short)))
        {
            nFCType = fc_value_type.fc_value_short;
            return "short";
        }
        if (nType.Equals(typeof(ushort)))
        {
            nFCType = fc_value_type.fc_value_ushort;
            return "ushort";
        }
        if (nType.Equals(typeof(uint)))
        {
            nFCType = fc_value_type.fc_value_uint;
            return "uint";
        }
        if (nType.Equals(typeof(long)))
        {
            nFCType = fc_value_type.fc_value_int64;
            return "int64";
        }
        if (nType.Equals(typeof(ulong)))
        {
            nFCType = fc_value_type.fc_value_uint64;
            return "uint64";
        }
        if (nType.Equals(typeof(double)))
        {
            nFCType = fc_value_type.fc_value_double;
            return "double";
        }
        if (nType.Equals(typeof(void)))
        {
            nFCType = fc_value_type.fc_value_void;
            return "void";
        }
        if (nType.Equals(typeof(string)))
        {
            nFCType = fc_value_type.fc_value_string_a;
            return "StringA";
        }
        if (nType.Equals(typeof(Vector2)))
        {
            nFCType = fc_value_type.fc_value_vector2;
            return "Vector2";
        }
        if (nType.Equals(typeof(Vector3)))
        {
            nFCType = fc_value_type.fc_value_vector3;
            return "Vector3";
        }
        if (nType.Equals(typeof(Vector4)))
        {
            nFCType = fc_value_type.fc_value_vector4;
            return "Vector4";
        }
        if (nType.Equals(typeof(Plane)))
        {
            nFCType = fc_value_type.fc_value_plane;
            return "Plane";
        }
        if (nType.Equals(typeof(Bounds)))
        {
            nFCType = fc_value_type.fc_value_bounds;
            return "Bounds";
        }
        if (nType.Equals(typeof(Matrix4x4)))
        {
            nFCType = fc_value_type.fc_value_matrix;
            return "Matrix";
        }
        if (nType.Equals(typeof(Ray)))
        {
            nFCType = fc_value_type.fc_value_ray;
            return "Ray";
        }
        if (nType.Equals(typeof(Sphere)))
        {
            nFCType = fc_value_type.fc_value_sphere;
            return "Sphere";
        }
        if (nType.Equals(typeof(Color32)))
        {
            nFCType = fc_value_type.fc_value_color32;
            return "Color32";
        }
        if (nType.Equals(typeof(Color)))
        {
            nFCType = fc_value_type.fc_value_color;
            return "Color";
        }
        if (nType.Equals(typeof(IntRect)))
        {
            nFCType = fc_value_type.fc_value_intrect;
            return "IntRect";
        }
        if (nType.Equals(typeof(Rect)))
        {
            nFCType = fc_value_type.fc_value_rect;
            return "Rect";
        }
        if (nType.Equals(typeof(System.Object)))
        {
            nFCType = fc_value_type.fc_value_system_object;
            return "System.Object";
        }
        if(nType.IsEnum)
        {
            nFCType = fc_value_type.fc_value_enum;
            return nType.Name;
        }
        if (nType.IsArray)
        {
            Type nElementType = nType.GetElementType();
            fc_value_type value_type = fc_value_type.fc_value_unknow;
            string szElementType = GetTypeDescEx(nElementType, ref value_type);
            nFCType = fc_value_type.fc_value_system_array;
            return string.Format("List<{0}>", szElementType);
        }
        if(nType.Equals(typeof(IntPtr)))
        {
            nFCType = fc_value_type.fc_value_int_ptr;
            return "IntPtr";
        }
        string szTypeName = nType.Name;
        if(szTypeName == "List`1")
        {
            nFCType = fc_value_type.fc_value_system_list;
            Type[] argtypes = nType.GetGenericArguments(); // 模板的参数
            fc_value_type subtype = fc_value_type.fc_value_unknow;
            string szSubName = GetTypeDescEx(argtypes[0], ref subtype);
            return string.Format("List<{0}>", szSubName);
        }
        if (szTypeName == "Dictionary`2")
        {
            nFCType = fc_value_type.fc_value_system_map;
            Type[] argtypes = nType.GetGenericArguments(); // 模板的参数
            fc_value_type key_type = fc_value_type.fc_value_unknow;
            fc_value_type value_type = fc_value_type.fc_value_unknow;
            string szKeyName = GetTypeDescEx(argtypes[0], ref key_type);
            string szValueName = GetTypeDescEx(argtypes[1], ref value_type);
            return string.Format("map<{0}, {1}>", szKeyName, szValueName);
        }
        nFCType = fc_value_type.fc_value_object;
        return szTypeName;
    }
}
