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
    public void  PushDelegateWrap(Type nType, string szClassWrapName)
    {
        m_DelegateTypes[nType] = szClassWrapName;
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
        string szArg = string.Empty;
        for(int i = 0; i<nParamCount; ++i)
        {
            szArg = string.Format("arg{0}", i);
            FCValueType value_param = FCValueType.TransType(allParams[i].ParameterType);
            if(value_param.m_nTemplateType != fc_value_tempalte_type.template_none)
            {
                Debug.LogError(nClassType.FullName + "参数不支持模板");
                continue;
            }
            if(FCValueType.IsRefType(value_param.m_nValueType))
                m_szTempBuilder.AppendFormat("            FCDll.PushCallParam(ref {0});\r\n", szArg);
            else
                m_szTempBuilder.AppendFormat("            FCDll.PushCallParam({0});\r\n", szArg);
        }
        m_szTempBuilder.AppendLine("            FCLibHelper.fc_call(m_nThisPtr, m_szFuncName);");

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
            case fc_value_type.fc_value_char:
            case fc_value_type.fc_value_byte:
            case fc_value_type.fc_value_short:
            case fc_value_type.fc_value_ushort:
            case fc_value_type.fc_value_int:
            case fc_value_type.fc_value_uint:
            case fc_value_type.fc_value_float:
            case fc_value_type.fc_value_double:
            case fc_value_type.fc_value_int64:
            case fc_value_type.fc_value_uint64:
            case fc_value_type.fc_value_string_a:
            case fc_value_type.fc_value_vector2:
            case fc_value_type.fc_value_vector3:
            case fc_value_type.fc_value_vector4:
                m_szTempBuilder.AppendLine("            FCDll.GetReturnParam(ref ret);");
                break;
        }
        m_szTempBuilder.AppendLine("            return ret;");
    }
}
