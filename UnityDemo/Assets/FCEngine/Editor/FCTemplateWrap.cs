using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 支持模板类的导出

class FCTemplateWrap
{
    Dictionary<Type, FCValueType> m_GetTypeWrap = new Dictionary<Type, FCValueType>();  // 从脚本中获取参数
    Dictionary<Type, FCValueType> m_OutTypeWrap = new Dictionary<Type, FCValueType>();  // 向脚本输出参数
    Dictionary<Type, FCValueType> m_ReturnTypeWrap = new Dictionary<Type, FCValueType>(); // 向脚本输出返回值

    List<string> m_AllRefNameSpace = new List<string>();
    Dictionary<string, int> m_AllRefNameSpaceFlags = new Dictionary<string, int>();

    string m_szExportPath;
    StringBuilder m_szTempBuilder;

    Dictionary<string, bool> m_ExportFlags = new Dictionary<string, bool>();

    static FCTemplateWrap s_pIns;
    public static FCTemplateWrap Instance
    {
        get
        {
            return s_pIns;
        }
    }
    void ExportOutWrap()
    {
    }
    public void BeginExport(string szPath)
    {
        s_pIns = this;
        m_szExportPath = szPath;
        
        PushNameSapce("System");
        PushNameSapce("System.Collections.Generic");
        PushNameSapce("System.Text");
        PushNameSapce("UnityEngine");
        PushNameSapce("UnityObject = UnityEngine.Object");

        PushGetTypeWrap(typeof(byte[]));
        PushGetTypeWrap(typeof(int[]));
        
        PushGetTypeWrap(typeof(List<byte>));
        PushGetTypeWrap(typeof(List<int>));          

        PushGetTypeWrap(typeof(List<Vector2>));
        PushGetTypeWrap(typeof(List<Vector3>));
        PushGetTypeWrap(typeof(List<Vector4>));
        
        PushReturnTypeWrap(typeof(byte []));
        PushReturnTypeWrap(typeof(List<byte>));

        // 手动在这里添加扩展的类型
    }

    public void  EndExport(StringBuilder strBuilder)
    {
        m_szTempBuilder = strBuilder;

        m_szTempBuilder.Length = 0;
        foreach(string szNameSpace in m_AllRefNameSpace)
        {
            m_szTempBuilder.AppendFormat("using {0};\r\n", szNameSpace);
        }
        m_szTempBuilder.AppendLine();
        
        m_szTempBuilder.AppendLine("\r\nclass FCCustomParam");
        m_szTempBuilder.AppendLine("{");
        
        ExportGetWrap();
        ExportOutWrap();
        ExportReturnWap();

        m_szTempBuilder.AppendLine("}\r\n");

        string szPathName = m_szExportPath + "FCCustomParam.cs";
        File.WriteAllText(szPathName, m_szTempBuilder.ToString());
    }

    void  PushNameSapce(string szNameSpace)
    {
        if (string.IsNullOrEmpty(szNameSpace))
            return;
        if (m_AllRefNameSpaceFlags.ContainsKey(szNameSpace))
            return;
        m_AllRefNameSpaceFlags[szNameSpace] = 1;
        m_AllRefNameSpace.Add(szNameSpace);
    }

    // 功能：添加要返回值的值
    public FCValueType PushGetTypeWrap(Type nType)
    {
        FCValueType value = null;
        if (m_GetTypeWrap.TryGetValue(nType, out value))
            return value;
        value = new FCValueType(nType);
        m_GetTypeWrap[nType] = value;
        PushNameSapce(nType.Namespace);
        return value;
    }

    // 添加要输出的参数
    public FCValueType PushOutTypeWrap(Type nType)
    {
        FCValueType value = null;
        if (m_OutTypeWrap.TryGetValue(nType, out value))
            return value;
        value = new FCValueType(nType);
        m_OutTypeWrap[nType] = value;
        PushNameSapce(nType.Namespace);
        return value;
    }

    // 添加返回值的支持
    public FCValueType PushReturnTypeWrap(Type nType)
    {
        FCValueType value = null;
        if (m_ReturnTypeWrap.TryGetValue(nType, out value))
            return value;
        value = new FCValueType(nType);
        m_ReturnTypeWrap[nType] = value;
        PushNameSapce(nType.Namespace);
        return value;
    }

    bool  TrySetExportFlag(string szFuncDeclare)
    {
        if (m_ExportFlags.ContainsKey(szFuncDeclare))
            return true;
        m_ExportFlags[szFuncDeclare] = true;
        return false;
    }
    void  MakeListWrap(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;

        string szCharpName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static List<{0}> GetList(ref List<{0}> rList, long L, int nIndex)", szCharpName, szCharpName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            if (rList == null)");
        fileData.AppendFormat("                rList = new List<{0}>();\r\n", szCharpName);
        fileData.AppendLine("            else");
        fileData.AppendLine("                rList.Clear();");
        fileData.AppendLine("            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);");
        fileData.AppendLine("            int nArraySize = FCLibHelper.fc_get_array_size(ptr);");
        AddFCFuncCall(value, szCharpName, true);
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        return rList;");

        fileData.AppendLine("    }");
    }
    void  AddFCFuncCall(FCValueType value, string szCharpName, bool bList)
    {
        StringBuilder fileData = m_szTempBuilder;
        switch (value.m_nValueType)
        {
            case fc_value_type.fc_value_bool:
            case fc_value_type.fc_value_byte:
            case fc_value_type.fc_value_short:
            case fc_value_type.fc_value_ushort:
            case fc_value_type.fc_value_int:
            case fc_value_type.fc_value_uint:
            case fc_value_type.fc_value_float:
            case fc_value_type.fc_value_double:
            case fc_value_type.fc_value_vector2:
            case fc_value_type.fc_value_vector3:
            case fc_value_type.fc_value_vector4:
            case fc_value_type.fc_value_rect:
            case fc_value_type.fc_value_color32:
            case fc_value_type.fc_value_color:
                {
                    string szFuncAppend = FCValueType.GetFCLibFuncShortName(value.m_nValueType);
                    if(bList)
                    {
                        fileData.AppendFormat("            {0}[] buffer = new {1}[nArraySize];\r\n", szCharpName, szCharpName);
                        fileData.AppendFormat("            FCLibHelper.fc_get_array_{0}(ptr, buffer, 0, nArraySize);\r\n", szFuncAppend);
                        fileData.AppendLine("            rList.AddRange(buffer);");
                    }
                    else
                    {
                        fileData.AppendFormat("            FCLibHelper.fc_get_array_{0}(ptr, rList, 0, nArraySize);\r\n", szFuncAppend);
                    }
                }
                break;
            case fc_value_type.fc_value_int64:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    if(bList)
                        fileData.AppendLine("                rList.Add(FCLibHelper.fc_get_value_int64(item_ptr));");
                    else
                        fileData.AppendLine("                rList[i] = FCLibHelper.fc_get_value_int64(item_ptr);");
                    fileData.AppendLine("            }");
                }
                break;
            case fc_value_type.fc_value_plane:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    fileData.AppendLine("                Plane item = new Plane();");
                    fileData.AppendLine("                FCLibHelper.fc_get_value_plane(item_ptr, ref item);");
                    if(bList)
                        fileData.AppendLine("                rList.Add(item);");
                    else
                        fileData.AppendLine("                rList[i] = item;");
                    fileData.AppendLine("            }");
                }
                break;
            case fc_value_type.fc_value_bounds:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    fileData.AppendLine("                Bounds item = new Bounds();");
                    fileData.AppendLine("                FCLibHelper.fc_get_value_bounds(item_ptr, ref item);");
                    if (bList)
                        fileData.AppendLine("                rList.Add(item);");
                    else
                        fileData.AppendLine("                rList[i] = item;");
                    fileData.AppendLine("            }");
                }
                break;
            case fc_value_type.fc_value_matrix:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    fileData.AppendLine("                Matrix4x4 item = new Matrix4x4();");
                    fileData.AppendLine("                FCLibHelper.fc_get_value_matrix(item_ptr, ref item);");
                    if (bList)
                        fileData.AppendLine("                rList.Add(item);");
                    else
                        fileData.AppendLine("                rList[i] = item;");
                    fileData.AppendLine("            }");
                }
                break;
            case fc_value_type.fc_value_ray:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    fileData.AppendLine("                Ray item = new Ray();");
                    fileData.AppendLine("                FCLibHelper.fc_get_value_ray(item_ptr, ref item);");
                    if (bList)
                        fileData.AppendLine("                rList.Add(item);");
                    else
                        fileData.AppendLine("                rList[i] = item;");
                    fileData.AppendLine("            }");
                }
                break;
            case fc_value_type.fc_value_uint64:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    fileData.AppendLine("                long item = FCLibHelper.fc_get_value_uint64(item_ptr);");
                    if (bList)
                        fileData.AppendLine("                rList.Add(item);");
                    else
                        fileData.AppendLine("                rList[i] = item;");
                    fileData.AppendLine("            }");
                }
                break;
            case fc_value_type.fc_value_string_a:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    fileData.AppendLine("                string item = FCLibHelper.fc_get_value_string_a(item_ptr);");
                    if (bList)
                        fileData.AppendLine("                rList.Add(item);");
                    else
                        fileData.AppendLine("                rList[i] = item;");
                    fileData.AppendLine("            }");
                }
                break;
            case fc_value_type.fc_value_system_object:
            case fc_value_type.fc_value_unity_object:
            case fc_value_type.fc_value_object:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    //if(value.m_value.IsClass)
                        fileData.AppendFormat("                {0} item = FCGetObj.GetObj<{0}>(item_ptr);\r\n", szCharpName, szCharpName);
                    //else
                    //    fileData.AppendFormat("                {0} item = FCGetObj.GetStructObj<{0}>(item_ptr);\r\n", szCharpName, szCharpName);
                    if (bList)
                        fileData.AppendLine("                rList.Add(item);");
                    else
                        fileData.AppendLine("                rList[i] = item;");
                    fileData.AppendLine("            }");
                }
                break;
            case fc_value_type.fc_value_enum:
                {
                    fileData.AppendLine("            for (int i = 0; i < nArraySize; ++i)");
                    fileData.AppendLine("            {");
                    fileData.AppendLine("                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
                    fileData.AppendFormat("                {0} item = ({1})FCLibHelper.fc_get_value_int(item_ptr);\r\n", szCharpName, szCharpName);
                    if (bList)
                        fileData.AppendLine("                rList.Add(item);");
                    else
                        fileData.AppendLine("                rList[i] = item;");
                    fileData.AppendLine("            }");
                }
                break;
            default:
                break;
        }
    }
    void ExportGetWrap()
    {
        foreach (FCValueType value in m_GetTypeWrap.Values)
        {
            if (value.IsArray)
            {
                MakeArrayWrap(value);
            }
            else if (value.IsList)
            {
                MakeListWrap(value);
            }
            else if (value.IsMap)
            {
                MakeDictionary(value);
            }
        }
    }
    void MakeArrayWrap(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;

        string szCharpName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static {0}[] GetArray(ref {1}[] rList, long L, int nIndex)", szCharpName, szCharpName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);");
        fileData.AppendLine("            int nArraySize = FCLibHelper.fc_get_array_size(ptr);");
        fileData.AppendFormat("            rList = new {0}[nArraySize];\r\n", szCharpName);
        AddFCFuncCall(value, szCharpName, false);
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        return rList;");

        fileData.AppendLine("    }");
    }
    
    void MakeDictionary(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;

        string szKeyName = value.GetKeyName(true, true);
        string szValeuName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static Dictionary<{0},{1}> GetDictionary(ref Dictionary<{2},{3}> rList, long L, int nIndex)", szKeyName, szValeuName, szKeyName, szValeuName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            if (rList == null)");
        fileData.AppendFormat("                rList = new Dictionary<{0}, {1}>();\r\n", szKeyName, szValeuName);
        fileData.AppendLine("            else");
        fileData.AppendLine("                rList.Clear();");

        fileData.AppendLine("            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);");
        fileData.AppendLine("            int nCount = FCLibHelper.fc_get_map_size(ptr);");

        fileData.AppendLine("            for(; nCount > 0; --nCount)");
        fileData.AppendLine("            {");
        fileData.AppendLine("                FCLibHelper.fc_map_to_next_pair();");
        fileData.AppendLine("                long key_ptr = FCLibHelper.fc_map_get_cur_key_ptr();");
        fileData.AppendLine("                long value_ptr = FCLibHelper.fc_map_get_cur_value_ptr();");
        bool bValidKey = AddKeyValueCall(value.m_nKeyType, "key", "key_ptr", szKeyName);
        bool bValidValue = AddKeyValueCall(value.m_nValueType, "value", "value_ptr", szValeuName);
        if(bValidKey && bValidValue)
            fileData.AppendLine("                rList[key] = value;");

        fileData.AppendLine("            }");

        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("        return rList;");

        fileData.AppendLine("    }");
    }
    bool AddKeyValueCall(fc_value_type nValueType, string szLeftName, string szPtrName, string szCSharpName)
    {
        StringBuilder fileData = m_szTempBuilder;
        bool bSuc = true;
        switch (nValueType)
        {
            case fc_value_type.fc_value_bool:
            case fc_value_type.fc_value_byte:
            case fc_value_type.fc_value_short:
            case fc_value_type.fc_value_ushort:
            case fc_value_type.fc_value_int:
            case fc_value_type.fc_value_uint:
            case fc_value_type.fc_value_float:
            case fc_value_type.fc_value_double:
            case fc_value_type.fc_value_vector2:
            case fc_value_type.fc_value_vector3:
            case fc_value_type.fc_value_vector4:
            case fc_value_type.fc_value_rect:
            case fc_value_type.fc_value_color32:
            case fc_value_type.fc_value_color:
                {
                    fileData.AppendFormat("                {0} {1} = FCLibHelper.fc_get_value_{2}({3});\r\n", szCSharpName, szLeftName, szCSharpName, szPtrName);
                }
                break;
            case fc_value_type.fc_value_int64:
                {
                    fileData.AppendFormat("                {0} {1} = FCLibHelper.fc_get_value_int64({2});\r\n", szCSharpName, szLeftName, szPtrName);
                }
                break;
            case fc_value_type.fc_value_uint64:
                {
                    fileData.AppendFormat("                {0} {1} = FCLibHelper.fc_get_value_uint64({2});\r\n", szCSharpName, szLeftName, szPtrName);
                }
                break;
            case fc_value_type.fc_value_string_a:
                {
                    fileData.AppendFormat("                string {0} = FCLibHelper.fc_get_value_string_a({1});\r\n", szLeftName, szPtrName);
                }
                break;
            case fc_value_type.fc_value_system_object:
            case fc_value_type.fc_value_unity_object:
            case fc_value_type.fc_value_object:
                {
                    fileData.AppendFormat("                {0} {1} = FCGetObj.GetObj<{2}>({3});\r\n", szCSharpName, szLeftName, szCSharpName, szPtrName);                    
                }
                break;
            case fc_value_type.fc_value_enum:
                {
                    fileData.AppendFormat("                {0} {1} = ({2})FCLibHelper.fc_set_array_int({3});\r\n", szCSharpName, szLeftName, szCSharpName, szPtrName);
                }
                break;
            default:
                bSuc = false;
                break;
        }
        return bSuc;
    }    
    void MakeOutArray(FCValueType value)
    {
        switch(value.m_nValueType)
        {
            case fc_value_type.fc_value_bool:
            case fc_value_type.fc_value_byte:
            case fc_value_type.fc_value_short:
            case fc_value_type.fc_value_ushort:
            case fc_value_type.fc_value_int:
            case fc_value_type.fc_value_uint:
            case fc_value_type.fc_value_float:
            case fc_value_type.fc_value_double:
            case fc_value_type.fc_value_color32:
            case fc_value_type.fc_value_color:
            case fc_value_type.fc_value_vector2:
            case fc_value_type.fc_value_vector3:
            case fc_value_type.fc_value_vector4:
            case fc_value_type.fc_value_rect:
                MakeOutFastArray(value);
                return;
            default:
                break;
        }

        StringBuilder fileData = m_szTempBuilder;
        string szTypeName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static void OutArray({0} []rList, long L, int nIndex)", szTypeName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            int nCount = rList != null ? rList.Count : 0;");
        fileData.AppendLine("            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);");
        fileData.AppendLine("            FCLibHelper.fc_set_array_size(ptr, nCount);");
        if (FCValueType.IsRefType(value.m_nValueType))
        {
            fileData.AppendFormat("            {0} v;\r\n", value.GetValueName(true));
        }
        fileData.AppendLine("            for(int i = 0; i<nCount; ++i)");
        fileData.AppendLine("            {");
        fileData.AppendLine("                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
        MakeOutListElement(value);
        fileData.AppendLine("            }");
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");
    }
    void MakeOutFastArray(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;
        string szTypeName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static void OutArray({0} []rList, long L, int nIndex)", szTypeName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            int nCount = rList != null ? rList.Count : 0;");
        fileData.AppendLine("            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);");
        fileData.AppendFormat("            FCLibHelper.fc_set_array_{0}(ptr, rList, 0, nCount);", FCValueType.GetFCLibFuncShortName(value.m_nValueType));
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");
    }

    void MakeOutList(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;
        string szTypeName = value.GetTypeName(true, true);

        string szFuncDeclare = string.Format("    public static void OutList({0} rList, long L, int nIndex)", szTypeName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            int nCount = rList != null ? rList.Count : 0;");
        fileData.AppendLine("            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);");
        fileData.AppendLine("            FCLibHelper.fc_set_array_size(ptr, nCount);");
        if(FCValueType.IsRefType(value.m_nValueType))
        {
            fileData.AppendFormat("            {0} v;\r\n", value.GetValueName(true));
        }
        fileData.AppendLine("            for(int i = 0; i<nCount; ++i)");
        fileData.AppendLine("            {");
        fileData.AppendLine("                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
        MakeOutListElement(value);
        fileData.AppendLine("            }");
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");
    }
    void MakeOutListElement(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;
        if (fc_value_type.fc_value_system_object == value.m_nValueType
            || fc_value_type.fc_value_object == value.m_nValueType
            || fc_value_type.fc_value_unity_object == value.m_nValueType)
        {
            fileData.AppendLine("                FCLibHelper.fc_set_value_wrap_objptr(pItem, FCGetObj.PushObj(rList[i]));");
            return;
        }

        string szName = FCValueType.GetFCLibFuncShortName(value.m_nValueType);
        if (string.IsNullOrEmpty(szName))
            return;

        if (FCValueType.IsRefType(value.m_nValueType))
        {
            fileData.AppendLine("                v = rList[i];");
            fileData.AppendFormat("                FCLibHelper.fc_set_value_{0}(pItem, ref v);\r\n", szName);
        }
        else
            fileData.AppendFormat("                FCLibHelper.fc_set_value_{0}(pItem, rList[i]);\r\n", szName);
    }
    void MakeOutDictionary(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;
        
        string szKeyName = value.GetKeyName(true, true);
        string szValeuName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static void OutDictionary(Dictionary<{0}, {1}> rList, long L, int nIndex)", szKeyName, szValeuName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);");
        fileData.AppendLine("            FCLibHelper.fc_map_clear(ptr); // 先清空map");
        fileData.AppendLine("            if (rList == null) return;");
        fileData.AppendLine("            long pKey = FCLibHelper.fc_get_map_push_key_ptr(ptr);");
        fileData.AppendLine("            long pValue = FCLibHelper.fc_get_map_push_value_ptr(ptr);");
        if(FCValueType.IsRefType(value.m_nKeyType))
        {
            fileData.AppendFormat("            {0} k;\r\n", szKeyName);
        }
        if(FCValueType.IsRefType(value.m_nValueType))
        {
            fileData.AppendFormat("            {0} v;\r\n", szKeyName);
        }
        fileData.AppendLine("            foreach (var v in rList)");
        fileData.AppendLine("            {");
        MakeOutDictionaryElement(value);
        fileData.AppendLine("            }");
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");
    }
    void MakeOutDictionaryElement(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;
        string szKeyName = FCValueType.GetFCLibFuncShortName(value.m_nKeyType);
        string szValueName = FCValueType.GetFCLibFuncShortName(value.m_nValueType);
        if (string.IsNullOrEmpty(szKeyName) || string.IsNullOrEmpty(szValueName))
            return;
        if(FCValueType.IsRefType(value.m_nKeyType))
        {
            fileData.AppendFormat("                k = v.Key;", szKeyName);
            fileData.AppendFormat("                FCLibHelper.fc_set_value_{0}(pKey, ref k);", szKeyName);
        }
        else
            fileData.AppendFormat("                FCLibHelper.fc_set_value_{0}(pKey, v.Key);", szKeyName);
        if(FCValueType.IsRefType(value.m_nValueType))
        {
            fileData.AppendFormat("                v = v.Value;", szKeyName);
            fileData.AppendFormat("                FCLibHelper.fc_set_value_{0}(pKey, ref v);", szKeyName);
        }
        else
            fileData.AppendFormat("                FCLibHelper.fc_set_value_{0}(pValue, v.Value);", szValueName);
        fileData.AppendLine("                FCLibHelper.fc_map_push_key_value(ptr);");
    }
    void ExportReturnWap()
    {
        foreach (FCValueType value in m_ReturnTypeWrap.Values)
        {
            if (value.IsArray)
            {
                MakeReturnArray(value);
            }
            else if (value.IsList)
            {
                MakeReturnList(value);
            }
            else if (value.IsMap)
            {
                MakeReturnDictionary(value);
            }
        }
    }
    void MakeReturnArray(FCValueType value)
    {
        switch (value.m_nValueType)
        {
            case fc_value_type.fc_value_bool:
            case fc_value_type.fc_value_byte:
            case fc_value_type.fc_value_short:
            case fc_value_type.fc_value_ushort:
            case fc_value_type.fc_value_int:
            case fc_value_type.fc_value_uint:
            case fc_value_type.fc_value_float:
            case fc_value_type.fc_value_double:
            case fc_value_type.fc_value_color32:
            case fc_value_type.fc_value_color:
            case fc_value_type.fc_value_vector2:
            case fc_value_type.fc_value_vector3:
            case fc_value_type.fc_value_vector4:
            case fc_value_type.fc_value_rect:
                MakeReturnFastArray(value);
                return;
            default:
                break;
        }

        StringBuilder fileData = m_szTempBuilder;
        string szValueName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static void ReturnArray({0} []rList, long ptr)", szValueName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            int nCount = rList != null ? rList.Length : 0;");
        //fileData.AppendLine("            long ptr = FCLibHelper.fc_get_return_ptr(L);");
        fileData.AppendLine("            FCLibHelper.fc_set_array_size(ptr, nCount);");
        if (FCValueType.IsRefType(value.m_nValueType))
        {
            fileData.AppendFormat("            {0} v;\r\n", szValueName);
        }
        fileData.AppendLine("            for(int i = 0; i<nCount; ++i)");
        fileData.AppendLine("            {");
        fileData.AppendLine("                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
        MakeOutListElement(value);
        fileData.AppendLine("            }");
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");

    }
    void MakeReturnFastArray(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;
        string szTypeName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static void ReturnArray({0} []rList, long ptr)", szTypeName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            int nCount = rList != null ? rList.Length : 0;");
        //fileData.AppendLine("            long ptr = FCLibHelper.fc_get_return_ptr(L);");
        fileData.AppendFormat("            FCLibHelper.fc_set_array_{0}(ptr, rList, 0, nCount);\r\n", FCValueType.GetFCLibFuncShortName(value.m_nValueType));
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");
    }
    void MakeReturnList(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;
        string szValueName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static void ReturnList(List<{0}> rList, long ptr)", szValueName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        fileData.AppendLine("            int nCount = rList != null ? rList.Count : 0;");
        //fileData.AppendLine("            long ptr = FCLibHelper.fc_get_return_ptr(L);");
        fileData.AppendLine("            FCLibHelper.fc_set_array_size(ptr, nCount);");
        if (FCValueType.IsRefType(value.m_nValueType))
        {
            fileData.AppendFormat("            {0} v;\r\n", szValueName);
        }
        fileData.AppendLine("            for(int i = 0; i<nCount; ++i)");
        fileData.AppendLine("            {");
        fileData.AppendLine("                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);");
        MakeOutListElement(value);
        fileData.AppendLine("            }");
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");
    }
    void MakeReturnDictionary(FCValueType value)
    {
        StringBuilder fileData = m_szTempBuilder;

        string szKeyName = value.GetKeyName(true, true);
        string szValeuName = value.GetValueName(true, true);
        string szFuncDeclare = string.Format("    public static void ReturnDictionary(Dictionary<{0}, {1}> rList, long ptr)", szKeyName, szValeuName);
        if (TrySetExportFlag(szFuncDeclare))
            return;
        fileData.AppendLine(szFuncDeclare);
        fileData.AppendLine("    {");
        fileData.AppendLine("        try");
        fileData.AppendLine("        {");
        //fileData.AppendLine("            long ptr = FCLibHelper.fc_get_return_ptr(L);");
        fileData.AppendLine("            FCLibHelper.fc_map_clear(ptr); // 先清空map");
        fileData.AppendLine("            if (rList == null) return;");
        fileData.AppendLine("            long pKey = FCLibHelper.fc_get_map_push_key_ptr(ptr);");
        fileData.AppendLine("            long pValue = FCLibHelper.fc_get_map_push_value_ptr(ptr);");
        if (FCValueType.IsRefType(value.m_nKeyType))
        {
            fileData.AppendFormat("            {0} k;\r\n", szKeyName);
        }
        if (FCValueType.IsRefType(value.m_nValueType))
        {
            fileData.AppendFormat("            {0} v;\r\n", szKeyName);
        }
        fileData.AppendLine("            foreach (var v in rList)");
        fileData.AppendLine("            {");
        MakeOutDictionaryElement(value);
        fileData.AppendLine("            }");
        fileData.AppendLine("        }");
        fileData.AppendLine("        catch(Exception e)");
        fileData.AppendLine("        {");
        fileData.AppendLine("            Debug.LogException(e);");
        fileData.AppendLine("        }");
        fileData.AppendLine("    }");
    }
}

