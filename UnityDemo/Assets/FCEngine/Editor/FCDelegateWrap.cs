using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


class FCDelegateWrap
{
    string m_szExportPath;
    StringBuilder m_szTempBuilder;

    Dictionary<Type, string> m_DelegateTypes = new Dictionary<Type, string>(); // 临时转换的
    Dictionary<string, Type> m_NameToType = new Dictionary<string, Type>();
    Dictionary<string, string> m_FullToShortName = new Dictionary<string, string>(); // 长名字转短名字


    public void BeginExport(string szPath)
    {
        m_szExportPath = szPath;
    }
    public void EndExport(StringBuilder strBuilder)
    {
        m_szTempBuilder = strBuilder;

        m_szTempBuilder.Length = 0;
        m_szTempBuilder.AppendLine("using System;");
        m_szTempBuilder.AppendLine("using System.Collections.Generic;");
        m_szTempBuilder.AppendLine("using System.Text;");
        m_szTempBuilder.AppendLine("using UnityEngine;\r\n");

        foreach (var v in m_DelegateTypes)
        {
            MakeDelegetClassWrap(v.Key, v.Value);
        }

        string szPathName = m_szExportPath + "all_delegate_wrap.cs";
        File.WriteAllText(szPathName, m_szTempBuilder.ToString());
    }
    string  GetDelegateWrapName(Type nType)
    {
        // 第一个，按
        string szName = nType.Name;
        szName = szName.Replace("`", "");

        switch(szName)
        {
            case "Action":
            case "UnityAction":
            case "Func":
            case "Comparer":
                break;
            default:
                szName = "Custom";
                break;
        }

        MethodInfo method = nType.GetMethod("Invoke");
        ParameterInfo[] allParams = method.GetParameters();  // 函数参数
        int nParamCount = allParams != null ? allParams.Length : 0;
        for (int i = 0; i<nParamCount; ++i)
        {
            FCValueType value_param = FCValueType.TransType(allParams[i].ParameterType);
            szName += "_";
            szName += value_param.GetTypeName(true, true);
        }
        FCValueType ret_value = FCValueType.TransType(method.ReturnType);
        if(ret_value.m_nValueType != fc_value_type.fc_value_void)
        {
            string szRetName = ret_value.GetValueName(true, true);
            szName = szName + "__" + szRetName;
        }
        return szName;
    }
    bool  IsSameDelegateType(Type t1, Type t2)
    {
        if (t1 == t2)
            return true;

        MethodInfo method1 = t1.GetMethod("Invoke");
        MethodInfo method2 = t2.GetMethod("Invoke");
        ParameterInfo[] allParams1 = method1.GetParameters();  // 函数参数
        ParameterInfo[] allParams2 = method2.GetParameters();  // 函数参数
        int nParamCount1 = allParams1 != null ? allParams1.Length : 0;
        int nParamCount2 = allParams2 != null ? allParams2.Length : 0;
        if (nParamCount1 != nParamCount2)
            return false;
        if (method1.ReturnType != method2.ReturnType)
            return false;
        for(int i = 0; i<nParamCount1; ++i)
        {
            Type nParam1 = allParams1[i].ParameterType;
            Type nParam2 = allParams2[i].ParameterType;
            if (nParam1 != nParam2)
                return false;
        }
        return true;
    }

    public string PushDelegateWrap(Type nType, string szCustomName)
    {
        if(m_DelegateTypes.ContainsKey(nType))
        {
            return m_DelegateTypes[nType];
        }
        string szFullDelegateName = GetDelegateWrapName(nType);
        if(m_FullToShortName.ContainsKey(szFullDelegateName))
        {
            string szShortName = m_FullToShortName[szFullDelegateName];
            if(m_NameToType.ContainsKey(szShortName))
            {
                Type nRealType = m_NameToType[szShortName];
                if (IsSameDelegateType(nRealType, nType))
                    return szShortName;
            }
        }

        if (m_NameToType.ContainsKey(szCustomName))
        {
            Type nOldType = m_NameToType[szCustomName];
            if (IsSameDelegateType(nOldType, nType))
                return szCustomName;
            
            string szTempName = string.Empty;
            for(int i = 0; i<100;++i)
            {
                szTempName = 'd' + i.ToString() + '_' + szCustomName;
                if(m_NameToType.ContainsKey(szTempName))
                {
                    nOldType = m_NameToType[szTempName];
                    if (IsSameDelegateType(nOldType, nType))
                        return szTempName;
                    continue;
                }
                else
                {
                    szCustomName = szTempName;
                    m_DelegateTypes[nType] = szCustomName;
                    m_NameToType[szCustomName] = nType;
                    m_FullToShortName[szFullDelegateName] = szCustomName;
                    return szCustomName;
                }
            }
            szCustomName = szFullDelegateName;
            szCustomName = szCustomName.Replace('.', '_');
        }        
        m_DelegateTypes[nType] = szCustomName;
        m_NameToType[szCustomName] = nType;
        m_FullToShortName[szFullDelegateName] = szCustomName;
        return szCustomName;
    }
    void  MakeDelegetClassWrap(Type nType, string szClassWrapName)
    {
        m_szTempBuilder.AppendFormat("\r\nclass {0} : FCDelegateBase\r\n", szClassWrapName);
        m_szTempBuilder.AppendLine("{");
        // 添加委托函数
        MakeDeleteCallFunc(nType);
        m_szTempBuilder.AppendLine("}");        
    }
    void  MakeDeleteCallFunc(Type nClassType)
    {
        // 得到委托的参数
        MethodInfo method = nClassType.GetMethod("Invoke");
        ParameterInfo[] allParams = method.GetParameters();  // 函数参数
        FCValueType ret_value = FCValueType.TransType(method.ReturnType);
        
        string szCallDesc = string.Empty;
        int nParamCount = allParams != null ? allParams.Length : 0;
        for (int i = 0; i < nParamCount; ++i)
        {
            FCValueType  value_param = FCValueType.TransType(allParams[i].ParameterType);
            if ( i  > 0 )
            {
                szCallDesc += ",";
            }
            szCallDesc += string.Format("{0} arg{1}", value_param.GetTypeName(true, true), i);
        }
        m_szTempBuilder.AppendFormat("    public {0}  CallFunc({1})\r\n", ret_value.GetTypeName(true, true), szCallDesc);
        m_szTempBuilder.AppendLine("    {");
        if (ret_value.m_nValueType != fc_value_type.fc_value_void)
        {
            string szName = ret_value.GetTypeName(true, true);
            if (ret_value.m_nValueType == fc_value_type.fc_value_string_a)
                m_szTempBuilder.AppendFormat("        {0} ret = string.Empty;\r\n", szName);
            else
                m_szTempBuilder.AppendFormat("        {0} ret = default({1});\r\n", szName, szName);
        }

        m_szTempBuilder.AppendLine("        try");
        m_szTempBuilder.AppendLine("        {");
        m_szTempBuilder.AppendLine("            long  VM = m_VMPtr;");
        string szArg = string.Empty;
        for(int i = 0; i<nParamCount; ++i)
        {
            szArg = string.Format("arg{0}", i);
            FCValueType value_param = FCValueType.TransType(allParams[i].ParameterType);
            if(value_param.m_nTemplateType != fc_value_tempalte_type.template_none)
            {
                if(value_param.m_nTemplateType == fc_value_tempalte_type.template_array)
                {
                    if(value_param.m_nValueType == fc_value_type.fc_value_byte)
                    {
                        m_szTempBuilder.AppendFormat("            FCDll.PushCallParam(VM, {0});\r\n", szArg);
                        continue;
                    }
                }
                Debug.LogError(nClassType.FullName + "参数不支持模板");
                continue;
            }
            if(FCValueType.IsRefType(value_param.m_nValueType))
                m_szTempBuilder.AppendFormat("            FCDll.PushCallParam(VM, ref {0});\r\n", szArg);
            else
                m_szTempBuilder.AppendFormat("            FCDll.PushCallParam(VM, {0});\r\n", szArg);
        }
        m_szTempBuilder.AppendLine("            FCLibHelper.fc_call(VM, m_nThisPtr, m_szFuncName);");

        // 如果委托是有返回值的，添加返回值
        if(ret_value.m_nValueType != fc_value_type.fc_value_void)
        {
            AddReturnCmd(ret_value);
        }
        m_szTempBuilder.AppendLine("        }");
        m_szTempBuilder.AppendLine("        catch(Exception e)");
        m_szTempBuilder.AppendLine("        {");
        m_szTempBuilder.AppendLine("            Debug.LogException(e);");
        m_szTempBuilder.AppendLine("        }");
        if (ret_value.m_nValueType != fc_value_type.fc_value_void)
            m_szTempBuilder.AppendLine("        return ret;");
        m_szTempBuilder.AppendLine("    }");
    }

    void  AddReturnCmd(FCValueType ret_value)
    {
        // 目前不是所有的类型都支持返回
        switch (ret_value.m_nValueType)
        {
            case fc_value_type.fc_value_bool:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_bool(VM);");
                break;
            case fc_value_type.fc_value_char:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_char(VM);");
                break;
            case fc_value_type.fc_value_byte:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_byte(VM);");
                break;
            case fc_value_type.fc_value_short:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_short(VM);");
                break;
            case fc_value_type.fc_value_ushort:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_ushort(VM);");
                break;
            case fc_value_type.fc_value_int:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_int(VM);");
                break;
            case fc_value_type.fc_value_uint:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_uint(VM);");
                break;
            case fc_value_type.fc_value_float:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_float(VM);");
                break;
            case fc_value_type.fc_value_double:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_double(VM);");
                break;
            case fc_value_type.fc_value_int64:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_int64(VM);");
                break;
            case fc_value_type.fc_value_uint64:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_uint64(VM);");
                break;
            case fc_value_type.fc_value_string_a:
                m_szTempBuilder.AppendLine("            ret = FCLibHelper.fc_get_return_string_a(VM);");
                break;
            case fc_value_type.fc_value_vector2:
                m_szTempBuilder.AppendLine("            FCLibHelper.fc_get_return_vector2(VM, ref ret);");
                break;
            case fc_value_type.fc_value_vector3:
                m_szTempBuilder.AppendLine("            FCLibHelper.fc_get_return_vector3(VM, ref ret);");
                break;
            case fc_value_type.fc_value_vector4:
                m_szTempBuilder.AppendLine("            FCLibHelper.fc_get_return_vector4(VM, ref ret);");
                break;
            case fc_value_type.fc_value_system_object:
                {
                    string szName = ret_value.GetTypeName(true, true);
                    m_szTempBuilder.AppendLine("            long  nWrapObj = FCLibHelper.fc_get_return_wrap_objptr(VM);");
                    m_szTempBuilder.AppendFormat("            ret = FCGetObj.GetSystemObj<{0}>(VM, nWrapObj);", szName);
                }
                break;
            case fc_value_type.fc_value_unity_object:
            case fc_value_type.fc_value_object:
                {
                    string szName = ret_value.GetTypeName(true, true);
                    m_szTempBuilder.AppendLine("            long  nWrapObj = FCLibHelper.fc_get_return_wrap_objptr(VM);");
                    m_szTempBuilder.AppendFormat("            ret = FCGetObj.GetObj<{0}>(nWrapObj);", szName);
                }
                break;
            case fc_value_type.fc_value_int_ptr:
                m_szTempBuilder.AppendLine("            ret = new IntPtr(FCLibHelper.fc_get_return_void_ptr(VM));");
                break;
        }
        m_szTempBuilder.AppendLine("            return ret;");
    }
}
