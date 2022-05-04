using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;

class FBExportSetting
{
    public bool m_bExportReadWriteFunc = false;
    public bool m_bExportEnumHelp = false;
};

class PBEnumDesc
{
    public struct EnumValue
    {
        public string Name;
        public int ID;
        public string m_szHelp; // 注释
    };

    public string m_szType; // 类型
    public List<EnumValue> m_Values = new List<EnumValue>();

    public void Parse(List<pb_lex_words> aWords, int nStart, int nEnd, FBExportSetting Setting)
    {
        int nID = 0;
        EnumValue value = new EnumValue();
        value.Name = string.Empty;
        value.ID = 0;
        // SUCCESS = 0                             [(err_msg) = "Success", (err_msg_zh) = "desc tips"];
        bool bValidKey = false;
        bool bValidValue = false;
        int nRef = 0;
        for (int i = nStart; i < nEnd; ++i)
        {
            pb_lex_words words = aWords[i];

            if (words.m_type == pb_lex_words_type.lex_bracket_1)
            {
                ++nRef;
            }
            else if (words.m_type == pb_lex_words_type.lex_bracket_2)
            {
                --nRef;
            }
            if (nRef > 0)
            {
                if (Setting.m_bExportEnumHelp)
                    value.m_szHelp += words.GetString();
                continue;
            }

            if (words.m_type == pb_lex_words_type.lex_value)
            {
                if(string.IsNullOrEmpty(value.Name))
                {
                    bValidKey = true;
                    value.Name = words.GetString();
                }
            }
            else if (words.m_type == pb_lex_words_type.lex_set)
            {
                // name = id,
                if (!bValidValue && bValidKey)
                {
                    bValidValue = true;
                    pb_lex_words word_id = aWords[i + 1];
                    value.ID = PBLex.GetNumber(word_id);
                    nID = value.ID;
                    i += 1;
                }
            }
            else if (words.m_type == pb_lex_words_type.lex_semicolon || words.m_type == pb_lex_words_type.lex_comma)
            {
                // name,
                if (bValidKey && bValidValue)
                {
                    value.ID = nID++;
                    m_Values.Add(value);
                    value.Name = string.Empty;
                    value.m_szHelp = string.Empty;
                    bValidKey = false;
                    bValidValue = false;
                }
            }
        }
        if (!string.IsNullOrEmpty(value.Name))
        {
            value.ID = nID++;
            m_Values.Add(value);
            value.Name = string.Empty;
        }
    }
    public void ExportFC(ref StringBuilder szOut, int nSpaceCount)
    {
        int nEnumCount = m_Values.Count;
        szOut.Append(' ', nSpaceCount);
	    szOut.AppendFormat("enum {0}\r\n", m_szType);
	    szOut.Append(' ', nSpaceCount);
	    szOut.Append("{\r\n");
	    for(int i = 0; i<nEnumCount; ++i)
	    {
		    szOut.Append(' ', nSpaceCount + 4);

            EnumValue Value = m_Values[i];
            if (string.IsNullOrEmpty(Value.m_szHelp))
                szOut.AppendFormat("{0} = {1},\r\n", Value.Name, Value.ID);
            else
                szOut.AppendFormat("{0} = {1}, // {2} \r\n", Value.Name, Value.ID, Value.m_szHelp);
	    }
        szOut.Append(' ', nSpaceCount);
        szOut.Append("};\r\n");
    }
};

class PBMessage
{
    protected class MessageItem
    {
        public PBValueType m_item_type;
        public PBBaseValue m_key;
        public PBBaseValue m_value;
        public int m_ID;     // ID
        public string m_szName; // 变量名
        public string m_default; // 默认值
        public PBOneOfDesc m_pOneOfDesc; // 指向OneOf对象
        public bool m_bValidType = true;
    };
    protected class PBOneOfDesc
    {
        public string m_szClassName;
        public string m_szName;
        public int m_nIndex;
        public List<MessageItem> m_Childs = new List<MessageItem>();
    };
    protected List<PBMessage>   m_ChildMsgDesc = new List<PBMessage>(); // 子类的描述结束
    protected List<MessageItem> m_Member = new List<MessageItem>();       // 成员
    protected List<PBEnumDesc>  m_Enums = new List<PBEnumDesc>();        // 枚举
    protected List<PBOneOfDesc> m_OneOf = new List<PBOneOfDesc>();        // 单个对象的
    public string      m_szClassName;  // 类名
    protected string m_szCurFileName;
    protected PBOneOfDesc m_pCurOneOf;

    int FindChild(int nID)
    {
        for (int i = 0; i < m_Member.Count; ++i)
        {
            if (m_Member[i].m_ID == nID)
                return i;
        }
        return -1;
    }

    protected void Parse(List<pb_lex_words> aWords, int nStart, int nEnd, FBExportSetting Setting, CPBMessageMng pPBMessageMng)
    {        
        int nPB_ID = 0;
        int nFind = -1;
	    for(int i = nStart; i<nEnd; ++i)
	    {
		    pb_lex_words words = aWords[i];
		    if(words.m_type == pb_lex_words_type.lex_enum)
		    {
                PBEnumDesc pEnum = ParseEnum(aWords, ref i, nEnd, Setting, pPBMessageMng);
                if (pEnum != null)
                    m_Enums.Add(pEnum);
            }
		    else if(words.m_type == pb_lex_words_type.lex_message)
		    {
			    PBMessage pChild = ParseMessage(aWords, ref i, nEnd, Setting, pPBMessageMng);
			    if(pChild != null)
				    m_ChildMsgDesc.Add(pChild);
		    }
		    else if(words.m_type == pb_lex_words_type.lex_oneof)
		    {
                ParseOneOf(aWords, ref i, nEnd, Setting, pPBMessageMng);
		    }
		    else if(words.m_type == pb_lex_words_type.lex_repeated)
		    {
			    // repeated type xxx = id;
			    // List<type> xxxx = id;
			    if(i + 5 < aWords.Count
                    && aWords[i + 3].m_type == pb_lex_words_type.lex_set
                    && aWords[i + 5].m_type == pb_lex_words_type.lex_semicolon
                    )
			    {
				    pb_lex_words key_words = aWords[i + 1];
				    pb_lex_words name_words = aWords[i + 2];
				    pb_lex_words id_words = aWords[i + 4];
				    nPB_ID = PBLex.GetNumber(id_words);
                    nFind = FindChild(nPB_ID);
				    if(nFind != -1)
				    {
                        i += 5;
					    continue;
				    }

				    MessageItem Item = new MessageItem();
                    Item.m_item_type = PBValueType.Value_Array;
				    Item.m_key.m_type = PBLex.GetPBType(key_words);
                    Item.m_key.m_szType = key_words.GetString();
				    Item.m_value = Item.m_key;
				    Item.m_pOneOfDesc = m_pCurOneOf;

                    Item.m_szName = name_words.GetString();
                    Item.m_ID = nPB_ID;
                    m_Member.Add(Item);
				    if(m_pCurOneOf != null)
                    {
                        m_pCurOneOf.m_Childs.Add(Item);
                    }
                    i += 5;
                }
		    }
		    else if(words.m_type == pb_lex_words_type.lex_map)
		    {
			    // map<key, value>  xxx = id;
			    if(i + 9 < aWords.Count
                    && aWords[i + 1].m_type == pb_lex_words_type.lex_less
                    && aWords[i + 3].m_type == pb_lex_words_type.lex_comma
                    && aWords[i + 5].m_type == pb_lex_words_type.lex_greate
                    && aWords[i + 7].m_type == pb_lex_words_type.lex_set
                    && aWords[i + 9].m_type == pb_lex_words_type.lex_semicolon
				    )
			    {
				    pb_lex_words key_words = aWords[i + 2];
				    pb_lex_words value_words = aWords[i + 4];
				    pb_lex_words name_words = aWords[i + 6];
				    pb_lex_words id_words = aWords[i + 8];
				    nPB_ID = PBLex.GetNumber(id_words);
                    nFind = FindChild(nPB_ID);
				    if(nFind != -1)
				    {
                        i += 8;
					    continue;
				    }

				    MessageItem Item = new MessageItem();
                    Item.m_item_type = PBValueType.Value_Map;
				    Item.m_key.m_type = PBLex.GetPBType(key_words);
                    Item.m_key.m_szType = key_words.GetString();
				    Item.m_value = Item.m_key;

				    Item.m_value.m_type = PBLex.GetPBType(value_words);
                    Item.m_value.m_szType = value_words.GetString();
				    Item.m_pOneOfDesc = m_pCurOneOf;

                    Item.m_szName = name_words.GetString();
                    Item.m_ID = PBLex.GetNumber(id_words);
                    m_Member.Add(Item);
				    if(m_pCurOneOf != null)
                    {
                        m_pCurOneOf.m_Childs.Add(Item);
                    }
                    i += 8;
                }
		    }
		    else
		    {
			    // xxx type = xx;
                // xxx type = xx [default = xx]
			    if(i + 5 < aWords.Count && aWords[i + 2].m_type == pb_lex_words_type.lex_set)
			    {
                    // 是成员定义
                    int nSemicolon = PBLex.FindFirstWords(aWords, pb_lex_words_type.lex_none, pb_lex_words_type.lex_semicolon, i, nEnd);
                    if(nSemicolon != -1)
                    {
                        pb_lex_words key_words = aWords[i + 0];
                        pb_lex_words name_words = aWords[i + 1];
                        pb_lex_words id_words = aWords[i + 3];
                        nPB_ID = PBLex.GetNumber(id_words);
                        nFind = FindChild(nPB_ID);
                        if (nFind != -1)
                        {
                            i = nSemicolon;
                            continue;
                        }

                        MessageItem Item = new MessageItem();
                        Item.m_item_type = PBValueType.Value_Base;
                        Item.m_key.m_type = PBLex.GetPBType(key_words);
                        Item.m_key.m_szType = key_words.GetString();
                        Item.m_value = Item.m_key;
                        Item.m_pOneOfDesc = m_pCurOneOf;

                        Item.m_szName = name_words.GetString();
                        Item.m_ID = PBLex.GetNumber(id_words);

                        // 找默认值 xxx type = xx [default = xx]
                        if (i + 8 < aWords.Count
                            && aWords[i + 4].m_type == pb_lex_words_type.lex_bracket_1
                            && aWords[i + 5].m_type == pb_lex_words_type.lex_default
                            && aWords[i + 6].m_type == pb_lex_words_type.lex_set
                            && aWords[i + 8].m_type == pb_lex_words_type.lex_bracket_2
                            && aWords[i + 7].m_type == pb_lex_words_type.lex_value
                            )
                        {
                            if (Item.m_value.m_type != pb_type.pb_string)
                                Item.m_default = aWords[i + 7].GetString();
                        }
                        m_Member.Add(Item);
                        if (m_pCurOneOf != null)
                        {
                            m_pCurOneOf.m_Childs.Add(Item);
                        }
                        i = nSemicolon;
                    }
                }
		    }
	    }
    }

    protected PBEnumDesc ParseEnum(List<pb_lex_words> aWords, ref int nStart, int nEnd, FBExportSetting Setting, CPBMessageMng pPBMessageMng)
    {
        CPBMessageMng pMng = pPBMessageMng;
        int nLeft = PBLex.FindFirstWords(aWords, pb_lex_words_type.lex_left_brace, pb_lex_words_type.lex_left_brace, nStart, nEnd);
        int nRight = PBLex.FindNextWords(aWords, pb_lex_words_type.lex_left_brace, pb_lex_words_type.lex_right_brace, nStart, nEnd);
        PBEnumDesc pEnum = null;

        if (nLeft<nRight && nLeft> nStart)
	    {
		    // 找到了  enum xxx{ }
		    if(nStart + 2 == nLeft)
		    {
			    pb_lex_words name_enum = aWords[nStart + 1];
			    pEnum = new PBEnumDesc();
                pEnum.m_szType = name_enum.GetString();
                pMng.m_AllEnumNames.PushKey(pEnum.m_szType);
                pEnum.Parse(aWords, nLeft + 1, nRight + 1, Setting);
		    }
            nStart = nRight;
	    }
        return pEnum;
    }

    protected void ParseOneOf(List<pb_lex_words> aWords, ref int nStart, int nEnd, FBExportSetting Setting, CPBMessageMng pPBMessageMng)
    {
        CPBMessageMng pMng = pPBMessageMng;

        PBOneOfDesc pChild = null;
        int nLeft = PBLex.FindFirstWords(aWords, pb_lex_words_type.lex_left_brace, pb_lex_words_type.lex_left_brace, nStart, nEnd);
        int nRight = PBLex.FindNextWords(aWords, pb_lex_words_type.lex_left_brace, pb_lex_words_type.lex_right_brace, nStart, nEnd);
        if (nLeft < nRight && nLeft > nStart)
        {
            // 找到了  message xxx{ }
            if (nStart + 2 == nLeft)
            {
                pb_lex_words name_class = aWords[nStart + 1];
                pChild = new PBOneOfDesc();
                pChild.m_nIndex = m_OneOf.Count;
                pChild.m_szClassName = name_class.GetString();
                pChild.m_szName = MakeOneOfValueName(pChild.m_nIndex);

                pMng.m_AllMessageNames.PushKey(pChild.m_szClassName);

                PBOneOfDesc pOldOneOf = m_pCurOneOf;
                m_pCurOneOf = pChild;
                m_OneOf.Add(pChild);
                Parse(aWords, nLeft + 1, nRight, Setting, pPBMessageMng);
                m_pCurOneOf = pOldOneOf;
            }
            nStart = nRight;
        }
    }

    protected PBMessage ParseMessage(List<pb_lex_words> aWords, ref int nStart, int nEnd, FBExportSetting Setting, CPBMessageMng pPBMessageMng)
    {
        CPBMessageMng pMng = pPBMessageMng;

        PBMessage pChild = null;
        int nLeft = PBLex.FindFirstWords(aWords, pb_lex_words_type.lex_left_brace, pb_lex_words_type.lex_left_brace, nStart, nEnd);
        int nRight = PBLex.FindNextWords(aWords, pb_lex_words_type.lex_left_brace, pb_lex_words_type.lex_right_brace, nStart, nEnd);
        if (nLeft < nRight && nLeft > nStart)
        {
            // 找到了  message xxx{ }
            if (nStart + 2 == nLeft)
            {
                pb_lex_words name_class = aWords[nStart + 1];
                pChild = new PBMessage();
                pChild.m_szClassName = name_class.GetString();
                pMng.m_AllMessageNames.PushKey(pChild.m_szClassName);
                pChild.Parse(aWords, nLeft + 1, nRight, Setting, pPBMessageMng);
            }
            nStart = nRight;
        }
        return pChild;
    }
    string  MakeOneOfValueName(int nIndex)
    {
        return string.Format("_oneof_case_{0}", nIndex);
    }
    string GetValueTypeName( MessageItem Item)
    {
	    if(PBValueType.Value_Array == Item.m_item_type)
	    {
            return string.Format("List<{0}>", PBLex.GetBaseValueTypeName(Item.m_key));
	    }
	    else if(PBValueType.Value_Map == Item.m_item_type)
	    {
            return string.Format("map<{0},{1}>", PBLex.GetBaseValueTypeName(Item.m_key), PBLex.GetBaseValueTypeName(Item.m_value));
	    }
	    return PBLex.GetBaseValueTypeName(Item.m_key);
    }
    // -------------------------------------------------------------

    string GetValidMessageType( CPBStringMap AllMessageNames, string szType)
    {
	    int nPos = 0;
        string szRealType = szType;
	    while(nPos<szType.Length)
	    {
            int nNext = szType.IndexOf('.', nPos);
		    if(nNext != -1)
		    {
			    szRealType = szType.Substring(nNext+1);
			    nPos = nNext + 1;
		    }
		    else
		    {
			    break;
		    }
	    }
	    return szRealType;
    }

    public void ExportFCScript(ref StringBuilder szOut, int nSpaceCount, bool bExportReadWriteFunc, CPBMessageMng pPBMessageMng)
    {
        CPBMessageMng pMng = pPBMessageMng;

        int nParentSpaceCount = nSpaceCount;
        szOut.Append("\r\n");
        // 先生成枚举
        ExportFC_InnerEnum(ref szOut, nSpaceCount, pPBMessageMng);

        // 先将类内的函数，生成到类外吧
        ExportFC_InnerClass(ref szOut, nSpaceCount, bExportReadWriteFunc, pPBMessageMng);
        szOut.Append("\r\n");

        string szRealClassName = m_szClassName;
        if (pMng.m_ReplaceTypeMap.GetValue(m_szClassName, out szRealClassName))
        {
            pMng.m_AllMessageNames.PushKey(szRealClassName);
            m_szClassName = szRealClassName;
        }

        // 导出类
        szOut.Append(' ', nParentSpaceCount);
        szOut.AppendFormat("public class {0}\r\n", m_szClassName);
        szOut.Append(' ', nParentSpaceCount);
        szOut.Append("{\r\n");

        nSpaceCount += 4;
        // 生成内部的枚举
        szOut.Append(' ', nSpaceCount);
        szOut.Append("//-----------------------------\r\n");
        ExportFC_OneofEnum(ref szOut, nSpaceCount, pPBMessageMng);
        szOut.Append(' ', nSpaceCount);
        szOut.Append("//-----------------------------\r\n");

        // 生成变量
        int nSize = m_Member.Count;
        PBOneOfDesc pOneOfPtr = null;
        bool bValidType = true;
        for (int i = 0; i < nSize; ++i)
        {
            MessageItem pItem = m_Member[i];
            string szKeyType = pItem.m_key.m_szType;

            bValidType = true;
            pItem.m_bValidType = true;
            if (PBValueType.Value_Base == pItem.m_item_type || PBValueType.Value_Array == pItem.m_item_type)
            {
                if (pMng.m_ReplaceTypeMap.GetValue(pItem.m_key.m_szType, out szRealClassName))
                {
                    pMng.m_AllMessageNames.PushKey(szRealClassName);
                    pItem.m_key.m_szType = szRealClassName;
                }
                else if (szKeyType.IndexOf('.') != -1)
                {
                    pItem.m_key.m_szType = GetValidMessageType(pMng.m_AllMessageNames, szKeyType);
                }
                if (pMng.m_ReplaceNameMap.GetValue(pItem.m_szName, out szRealClassName))
                {
                    pItem.m_szName = szRealClassName;
                }

                if (pb_type.pb_object == pItem.m_key.m_type || pb_type.pb_none == pItem.m_key.m_type)
                {
                    szKeyType = pItem.m_key.m_szType;
                    bValidType = pMng.m_AllMessageNames.IsValid(szKeyType) || pMng.m_AllEnumNames.IsValid(szKeyType);
                }
                pItem.m_bValidType = bValidType;
            }
            if (pOneOfPtr != pItem.m_pOneOfDesc)
            {
                szOut.Append("    //-----------------------------\r\n");
                PBOneOfDesc pOneOf = pItem.m_pOneOfDesc;
                if (pOneOf != null)
                {
                    szOut.AppendFormat("    int  {0}; // {1}\r\n", pOneOf.m_szName, pOneOf.m_szClassName);
                }
            }
            pOneOfPtr = pItem.m_pOneOfDesc;
            ExportFC_ValueReflexTable(ref szOut, pItem, nSpaceCount);
            if(bValidType)
            {
                if (string.IsNullOrEmpty(pItem.m_default))
                    szOut.AppendFormat("    public {0}  {1};// = {2}\r\n", GetValueTypeName(pItem), pItem.m_szName, pItem.m_ID);
                else
                    szOut.AppendFormat("    public {0}  {1} = {2};// = {3}\r\n", GetValueTypeName(pItem), pItem.m_szName, pItem.m_default, pItem.m_ID);
            }
            else
            {
                if (string.IsNullOrEmpty(pItem.m_default))
                    szOut.AppendFormat("    // public {0}  {1};// = {2}\r\n", GetValueTypeName(pItem), pItem.m_szName, pItem.m_ID);
                else
                    szOut.AppendFormat("    // public {0}  {1} = {2};// = {3}\r\n", GetValueTypeName(pItem), pItem.m_szName, pItem.m_default, pItem.m_ID);
            }
        }
        if (pOneOfPtr != null)
        {
            szOut.Append("    //-----------------------------\r\n");
        }

        // 删除无效的节点
        for (int i = m_Member.Count - 1; i >= 0; --i)
        {
            MessageItem pItem = m_Member[i];
            if (!pItem.m_bValidType)
            {
                m_Member.RemoveAt(i);
            }
        }

        // 生成Set函数
        ExportFC_SetFunc(ref szOut, nSpaceCount, pPBMessageMng);

        // 生成Get函数
        ExportFC_GetFunc(ref szOut, nSpaceCount, pPBMessageMng);

        // 生成写函数
        if(bExportReadWriteFunc)
            ExportFC_WriteFunc(ref szOut, nSpaceCount);

        // 生成读函数
        if(bExportReadWriteFunc)
            ExportFC_ReadFunc(ref szOut, nSpaceCount);

        szOut.Append(' ', nParentSpaceCount);
        szOut.Append("};\r\n");
    }

    // 生成成员变量的反射标签
    void ExportFC_ValueReflexTable(ref StringBuilder szOut, MessageItem pItem, int nSpaceCount)
    {
        // [PBAttrib("Value=PB_Zip_Varint,Field=4, Case=_oneof_case_0, Def=0")]
        // [PBAttrib("Key=PB_Zip_Varint,Value=PB_Zip_Varint, Field=5")]
        int nTag = PBLex.PB_MakeTag(pItem.m_ID, pItem.m_key.m_type, pItem.m_item_type);
        if (pItem.m_pOneOfDesc != null)
        {
            szOut.AppendFormat("    [PBAttrib(\"Value = {0}, Field = {1}, Tag = {2}, Case = {3}\")]\r\n", PBLex.GetZipType(pItem.m_key.m_type), pItem.m_ID, nTag, pItem.m_pOneOfDesc.m_szName);
        }
        else if(PBValueType.Value_Map == pItem.m_item_type)
        {
            szOut.AppendFormat("    [PBAttrib(\"Key = {0}, Value = {1}, Field = {2}, Tag = {3}\")]\r\n", PBLex.GetZipType(pItem.m_key.m_type), PBLex.GetZipType(pItem.m_value.m_type), pItem.m_ID, nTag);
        }
        else
        {
            szOut.AppendFormat("    [PBAttrib(\"Value = {0}, Field = {1}, Tag = {2}\")]\r\n", PBLex.GetZipType(pItem.m_key.m_type), pItem.m_ID, nTag);
        }
    }

    void ExportFC_InnerEnum(ref StringBuilder szOut, int nSpaceCount, CPBMessageMng pPBMessageMng)
    {
        CPBMessageMng pMng = pPBMessageMng;
        CPBStringMap EnumMap = pMng.m_EnumMap;
        int nSize = m_Enums.Count;
        for (int i = 0; i < nSize; ++i)
        {
            PBEnumDesc pEnum = m_Enums[i];
            if (EnumMap.PushKey(pEnum.m_szType))
            {
                pEnum.ExportFC(ref szOut, nSpaceCount);
            }
        }
    }

    void ExportFC_OneofEnum(ref StringBuilder szOut, int nSpaceCount, CPBMessageMng pPBMessageMng)
    {
        CPBMessageMng pMng = pPBMessageMng;
        CPBStringMap EnumMap = pMng.m_EnumMap;
        int nSize = m_OneOf.Count;
        for (int i = 0; i < nSize; ++i)
        {
            PBOneOfDesc pOneOf = m_OneOf[i];
            if (!EnumMap.PushKey(pOneOf.m_szClassName))
            {
                continue;
            }
            int nEnumCount = pOneOf.m_Childs.Count;
            szOut.Append(' ', nSpaceCount);
            szOut.AppendFormat("public enum {0}Case\r\n", pOneOf.m_szClassName);
            szOut.Append(' ', nSpaceCount);
            szOut.Append("{\r\n");
            for (int j = 0; j < nEnumCount; ++j)
            {
                szOut.Append(' ', nSpaceCount + 4);
                szOut.AppendFormat("k{0} = {1},\r\n", pOneOf.m_Childs[j].m_szName, pOneOf.m_Childs[j].m_ID);
            }
            string szName = pOneOf.m_szClassName.ToUpper();
            szOut.Append(' ', nSpaceCount + 4);
            szOut.AppendFormat("{0}_NOT_SET = 0,\r\n", szName);
            szOut.Append(' ', nSpaceCount);
            szOut.Append("};\r\n");
        }
    }

    void ExportFC_InnerClass(ref StringBuilder szOut, int nSpaceCount, bool bExportReadWriteFunc, CPBMessageMng pPBMessageMng)
    {
        CPBMessageMng pPBMng = pPBMessageMng;
        CPBStringMap NameMap = pPBMng.m_NameMap;
        int nSize = m_ChildMsgDesc.Count;
        for (int i = 0; i < nSize; ++i)
        {
            PBMessage pChild = m_ChildMsgDesc[i];
            if (NameMap.PushKey(pChild.m_szClassName))
            {
                pChild.ExportFCScript(ref szOut, nSpaceCount, bExportReadWriteFunc, pPBMessageMng);
            }
        }
    }

    void ExportFC_WriteFunc(ref StringBuilder szOut, int nSpaceCount)
    {
        szOut.Append(' ', nSpaceCount);
        szOut.Append("[export]\r\n");
        szOut.Append("    public void  WriteTo(FCSerialize ar)\r\n");
        szOut.Append("    {\r\n");

        // 生成 
        int nSize = m_Member.Count;
        for (int i = 0; i < nSize; ++i)
        {
            MessageItem pItem = m_Member[i];
            if (pItem.m_pOneOfDesc != null)
                ExportFC_WriteOneOf(ref szOut, ref i, pItem.m_pOneOfDesc, nSpaceCount + 4);
            else
            {
                ExportFC_WriteChild(ref szOut, pItem, nSpaceCount + 4);
            }
        }
        szOut.Append("    }\r\n");
    }

    void ExportFC_SetFunc(ref StringBuilder szOut, int nSpaceCount, CPBMessageMng pPBMessageMng)
    {
        // 只需要生成OneOf的
        for (int i = 0; i < m_Member.Count; ++i)
        {
            MessageItem pItem = m_Member[i];
            if (pItem.m_pOneOfDesc != null)
            {
                ExportFC_OneOfFunc(ref szOut, pItem, true, nSpaceCount, pPBMessageMng);
            }
        }
    }

    void ExportFC_GetFunc(ref StringBuilder szOut, int nSpaceCount, CPBMessageMng pPBMessageMng)
    {
        // has_xxxx()
        for (int i = 0; i < m_Member.Count; ++i)
        {
            MessageItem pItem = m_Member[i];
            if (pItem.m_pOneOfDesc != null)
            {
                ExportFC_OneOfFunc(ref szOut, pItem, false, nSpaceCount, pPBMessageMng);
            }
        }
    }

    void ExportFC_OneOfFunc(ref StringBuilder szOut, MessageItem pItem, bool bSetFunc, int nSpaceCount, CPBMessageMng pPBMessageMng)
    {
        PBOneOfDesc pOneOf = pItem.m_pOneOfDesc;
	    string szOneOfValueName = pOneOf.m_szName;
        string szValueClassName = GetValueTypeName(pItem);

        CPBMessageMng pMng = pPBMessageMng;

        if (bSetFunc)
	    {
		    // set_xxxx(_Ty value)
		    // _Ty mutable_xxxx();
		    szOut.AppendFormat("    public void set_{0}({1} _{2})\r\n", pItem.m_szName, szValueClassName, pItem.m_szName);
		    szOut.Append(      "    {\r\n");
		    szOut.AppendFormat("        {0} = {1};\r\n", szOneOfValueName, pItem.m_ID);
		    szOut.AppendFormat("        {0} = _{1};\r\n", pItem.m_szName, pItem.m_szName);
		    szOut.Append(      "    }\r\n");
		
		    if(pItem.m_item_type != PBValueType.Value_Base || pItem.m_key.m_type == pb_type.pb_none)
		    {
			    szOut.AppendFormat("    public {0} mutable_{1}()\r\n", szValueClassName, pItem.m_szName);
			    szOut.Append(      "    {\r\n");
			    szOut.AppendFormat("        {0} = {1};\r\n", szOneOfValueName, pItem.m_ID);
                if (!pMng.m_AllEnumNames.IsValid(szValueClassName))
                {
                    szOut.AppendFormat("        if(null == {0})\r\n", pItem.m_szName);
                    szOut.AppendFormat("            {0} = new {1}();\r\n", pItem.m_szName, szValueClassName);
                }
			    szOut.AppendFormat("        return {0};\r\n", pItem.m_szName);
			    szOut.Append(      "    }\r\n");
		    }
	    }
	    else
	    {
		    // has_xxxx()
		    szOut.AppendFormat("    public bool has_{0}()\r\n", pItem.m_szName);
		    szOut.Append(      "    {\r\n");
		    szOut.AppendFormat("        return {0} == {1};\r\n", pItem.m_ID, szOneOfValueName);
		    szOut.Append(      "    }\r\n");
		
		    szOut.AppendFormat("    public {0} get_{1}()\r\n", szValueClassName, pItem.m_szName);
		    szOut.Append(      "    {\r\n");
		    szOut.AppendFormat("        return {0};\r\n", pItem.m_szName);
		    szOut.Append(      "    }\r\n");
	    }
    }

    void ExportFC_WriteChild(ref StringBuilder szOut, MessageItem pItem, int nSpaceCount)
    {
        szOut.Append(' ', nSpaceCount);
	    if(pItem.m_item_type == PBValueType.Value_Base)
	    {
		    szOut.AppendFormat("ar.ProtobufWrite({0}, {1}, PB_ZipType.{2});\r\n", pItem.m_szName, pItem.m_ID, PBLex.GetZipType(pItem.m_key.m_type));
	    }
	    else if(pItem.m_item_type == PBValueType.Value_Array)
	    {
		    szOut.AppendFormat("ar.ProtobufWrite({0}, {1}, PB_ZipType.{2});\r\n", pItem.m_szName, pItem.m_ID, PBLex.GetZipType(pItem.m_key.m_type));
	    }
	    else if(pItem.m_item_type == PBValueType.Value_Map)
	    {
		    // ProtobufWriteMap(map<_TyKey, _TyValue> sMap, int nFiledIndex, PB_ZipType keyZipType, PB_ZipType valueZipType)
		    szOut.AppendFormat("ar.ProtobufWriteMap({0}, {1}, PB_ZipType.{2}, PB_ZipType.{3});\r\n", pItem.m_szName, pItem.m_ID, PBLex.GetZipType(pItem.m_key.m_type), PBLex.GetZipType(pItem.m_value.m_type));
	    }
    }

    void ExportFC_WriteOneOf(ref StringBuilder szOut, ref int nStart, PBOneOfDesc pOneOf, int nSpaceCount)
    {
        szOut.AppendFormat("        switch({0})\r\n", pOneOf.m_szName);
        szOut.Append("        {\r\n");
        MessageItem pChild = null;
        for (int i = 0; i < pOneOf.m_Childs.Count; ++i)
        {
            pChild = pOneOf.m_Childs[i];
            szOut.AppendFormat("        case {0}:\r\n", pChild.m_ID);
            ExportFC_WriteChild(ref szOut, pChild, nSpaceCount + 4);
            szOut.Append("            break;\r\n");
        }
        szOut.Append("         default:\r\n");
        szOut.Append("            break;\r\n");
        szOut.Append("        }\r\n");
        nStart += pOneOf.m_Childs.Count - 1;
    }

    void ExportFC_ReadFunc(ref StringBuilder szOut, int nSpaceCount)
    {
        szOut.Append(' ', nSpaceCount);
        szOut.Append("[export]\r\n");
        szOut.Append("    public void  ReadFrom(FCSerialize ar)\r\n");
        szOut.Append("    {\r\n");

        szOut.Append("        int nTag = 0;\r\n");
        szOut.Append("        int nFiledIndex = 0;\r\n");
        szOut.Append("        bool bSucRead = false;\r\n");
        //szOut.Append("        while(nTag = ar.ProtobufReadTag())\r\n");
        szOut.Append("        //while(nTag = ar.ProtobufReadTag()) // C#不支持这样的写法\r\n");
        szOut.Append("        while((nTag = ar.ProtobufReadTag()) != 0)\r\n");
        szOut.Append("        {\r\n");
        szOut.Append("            nFiledIndex = nTag >> 3;\r\n");
        szOut.Append("            bSucRead = false;\r\n");
        szOut.Append("            switch(nFiledIndex)\r\n");
        szOut.Append("            {\r\n");

        int nSize = m_Member.Count;
        for (int i = 0; i < nSize; ++i)
        {
            MessageItem pItem = m_Member[i];
            szOut.AppendFormat("            case {0}:\r\n", pItem.m_ID);
            szOut.AppendFormat("                if(nTag == {0}) // MakeTag({1}, {2})\r\n", PBLex.PB_MakeTag(pItem.m_ID, pItem.m_key.m_type, pItem.m_item_type), pItem.m_ID, PBLex.PB_GetWireType(pItem.m_key.m_type, pItem.m_item_type));
            szOut.Append("                {\r\n");
            if (pItem.m_pOneOfDesc != null)
            {
                szOut.AppendFormat("                    {0} = {1}; // {2} \r\n", pItem.m_pOneOfDesc.m_szName, pItem.m_ID, pItem.m_szName);
            }
            if (PBValueType.Value_Map == pItem.m_item_type)
            {
                szOut.AppendFormat("                    bSucRead = ar.ProtobufReadMap({0}, {1}, PB_ZipType.{2}, PB_ZipType.{3});\r\n", pItem.m_szName, pItem.m_ID, PBLex.GetZipType(pItem.m_key.m_type), PBLex.GetZipType(pItem.m_value.m_type));
            }
            else
            {
                szOut.AppendFormat("                    bSucRead = ar.ProtobufRead({0}, {1}, PB_ZipType.{2});\r\n", pItem.m_szName, pItem.m_ID, PBLex.GetZipType(pItem.m_key.m_type));
            }
            szOut.Append("                }\r\n");
            szOut.Append("                break;\r\n");
        }

        szOut.Append("            default:\r\n");
        szOut.Append("                break;\r\n");
        szOut.Append("            }\r\n");
        szOut.Append("            if(!bSucRead)\r\n");
        szOut.Append("            {\r\n");
        szOut.Append("                ar.ProtobufSkipField(nTag);\r\n");
        szOut.Append("            }\r\n");
        szOut.Append("        }\r\n");
        szOut.Append("    }\r\n");
    }
    // -------------------------------------------------------------
};

class CPBStringMap
{
    Dictionary<string, int> m_Names = new Dictionary<string, int>();

    public bool IsValid(string szKey)
    {
        return m_Names.ContainsKey(szKey);
    }
    public bool PushKey(string szKey)
    {
        if (!m_Names.ContainsKey(szKey))
        {
            m_Names[szKey] = 1;
            return true;
        }
        return false;
    }
};

class CPBReplaceNameMap
{
    Dictionary<string, string> m_Names = new Dictionary<string, string>();

    public bool IsValid(string szKey)
	{
        return m_Names.ContainsKey(szKey);
    }
    public bool PushKeyValue(string szKey, string szValue)
    {
        if(!m_Names.ContainsKey(szKey))
        {
            m_Names[szKey] = szValue;
            return true;
        }
        return false;
    }
    public bool GetValue(string szKey, out string value)
	{
        return m_Names.TryGetValue(szKey, out value);
	}
};

class CPBMessageMng : PBMessage
{
	class PBMessageFile
    {
        public string m_szFileName = string.Empty;
        public List<PBEnumDesc> m_Enums = new List<PBEnumDesc>();
        public List<PBMessage> m_Messages = new List<PBMessage>();
    };
    List<PBMessageFile> m_MessageFiles = new List<PBMessageFile>();  // 所有的文件

    public CPBStringMap m_AllEnumNames = new CPBStringMap();
    public CPBStringMap m_AllMessageNames = new CPBStringMap();
    public CPBStringMap m_EnumMap = new CPBStringMap();
    public CPBStringMap m_NameMap = new CPBStringMap();
    public CPBReplaceNameMap m_ReplaceTypeMap = new CPBReplaceNameMap();
    public CPBReplaceNameMap m_ReplaceNameMap = new CPBReplaceNameMap();

    public CPBMessageMng()
    {
        m_ReplaceTypeMap.PushKeyValue("Vector4", "PB_Vector4");
        m_ReplaceTypeMap.PushKeyValue("Vector3", "PB_Vector3");
        m_ReplaceTypeMap.PushKeyValue("Vector2", "PB_Vector2");
        m_ReplaceNameMap.PushKeyValue("delete", "pb_delete");
    }

    string DistillFileName(string szPathName)
    {
        szPathName = szPathName.Replace('\\', '/');
        string  szFileName = System.IO.Path.GetFileName(szPathName);
        szFileName = szFileName.Replace(".proto", "");
        return szFileName;
    }
    public void ParseFile(string szPathName, FBExportSetting Setting)
    {
        if (!System.IO.File.Exists(szPathName))
            return;
        string szFileData = System.IO.File.ReadAllText(szPathName);
        if (string.IsNullOrEmpty(szFileData))
            return;
        string szFileName = DistillFileName(szPathName);
        m_szCurFileName = szFileName;
        pb_lex_words_contain contain = new pb_lex_words_contain();
        pb_lex_string_ptr fileDataPtr = new pb_lex_string_ptr(szFileData);
        PBLex.AnylseProtoFile(contain, fileDataPtr, szFileName);

        PBMessageFile pMessageFile = new PBMessageFile();
        pMessageFile.m_szFileName = szFileName;

        int nSize = contain.m_Words.Count;
        for (int i = 0; i < nSize; ++i)
        {
            pb_lex_words words = contain.m_Words[i];
            switch (words.m_type)
            {
                case pb_lex_words_type.lex_enum:
                    {
                        PBEnumDesc pEnum = ParseEnum(contain.m_Words, ref i, nSize, Setting, this);
                        if(pEnum != null)
                        {
                            pMessageFile.m_Enums.Add(pEnum);
                        }
                    }
                    break;
                case pb_lex_words_type.lex_message:
                    {
                        PBMessage pMessage = ParseMessage(contain.m_Words, ref i, nSize, Setting, this);
                        if (pMessage != null)
                            pMessageFile.m_Messages.Add(pMessage);
                    }
                    break;
                default:
                    break;
            }
        }

        if (pMessageFile.m_Messages.Count == 0 && pMessageFile.m_Enums.Count == 0)
        {
            return;
        }
        m_MessageFiles.Add(pMessageFile);
    }
    public void ParsePath(string szPath, FBExportSetting Setting)
    {
        string []all_files = System.IO.Directory.GetFiles(szPath, "*.proto", System.IO.SearchOption.AllDirectories);
        if (all_files == null)
            return;
        foreach(string szPathName in all_files)
        {
            ParseFile(szPathName, Setting);
        }
    }
    public static void SaveUTF8File(string szPathName, string szFileData)
    {
        if(string.IsNullOrEmpty(szFileData))
        {
            System.IO.File.WriteAllText(szPathName, szFileData);
            return;
        }
        byte[] fileData = UTF8Encoding.UTF8.GetBytes(szFileData);
        int nLength = fileData.Length;
        byte[] utf8Data = new byte[nLength + 3];
        Array.Copy(fileData, 0, utf8Data, 3, nLength);
        utf8Data[0] = 0xef;
        utf8Data[1] = 0xbb;
        utf8Data[2] = 0xbf;
        System.IO.File.WriteAllBytes(szPathName, utf8Data);
    }
    public void ExportFC(string szPath, bool bExportReadWriteFunc)
    {
        // 先删除该目录下的文件
        FCClassWrap.DeletePath(szPath);
        System.IO.Directory.CreateDirectory(szPath);

        StringBuilder szFileData = new StringBuilder(1024*1024*4);
        PBMessageFile pMsgFile = null;
        PBMessage pMsg = null;
        string szPathName = string.Empty, szFileName = string.Empty;
        int nFileCount = m_MessageFiles.Count;
        int nClassCount = 0;

        // 先导出PBTpye, 已经内置了，不需要导出了
        //szFileData.Append("enum PB_ZipType\r\n");
        //szFileData.Append("{\r\n");
        //szFileData.Append("    PB_Zip_Varint = 0,\r\n");
        //szFileData.Append("    PB_Zip_Fixed  = 1,\r\n");
        //szFileData.Append("    PB_Zip_ZigZag = 2,\r\n");
        //szFileData.Append("};\r\n");
        //szPathName = szPath + "FCProtobufType.cs";
        //System.IO.File.WriteAllText(szPathName, szFileData.ToString());

        for (int i = 0; i < nFileCount; ++i)
        {
            pMsgFile = m_MessageFiles[i];
            szFileData.Clear();
            for (int j = 0; j < pMsgFile.m_Enums.Count; ++j)
            {
                PBEnumDesc pEnum = pMsgFile.m_Enums[j];
                string szType = pEnum.m_szType;
                if (m_EnumMap.PushKey(szType))
                {
                    pEnum.ExportFC(ref szFileData, 0);
                }
            }
            nClassCount = pMsgFile.m_Messages.Count;
            for (int j = 0; j < nClassCount; ++j)
            {
                pMsg = pMsgFile.m_Messages[j];
                if (m_NameMap.PushKey(pMsg.m_szClassName))
                {
                    pMsg.ExportFCScript(ref szFileData, 0, bExportReadWriteFunc, this);
                }
            }
            szFileName = pMsgFile.m_szFileName;
            szFileName += ".cs";
            szPathName = szPath;
            szPathName += szFileName;

            // 保存
            //System.IO.File.WriteAllText(szPathName, szFileData.ToString());
            SaveUTF8File(szPathName, szFileData.ToString());
        }
    }
    static void ExprtProtobufToFC(bool bExportReadWriteFunc, bool bExportEnumHelp)
    {
        string szDataPath = Application.dataPath;
        string szRootPath = szDataPath.Substring(0, szDataPath.Length - 6);

        FBExportSetting Setting = new FBExportSetting();
        Setting.m_bExportReadWriteFunc = bExportReadWriteFunc;
        Setting.m_bExportEnumHelp = bExportEnumHelp;

        CPBMessageMng pb_mng = new CPBMessageMng();
        pb_mng.ParsePath(szRootPath + "Assets/Protobuf", Setting);
        pb_mng.ExportFC(szRootPath + "Script/Protobuf/", bExportReadWriteFunc);

        FCExport.InportPathToFCProj("Protobuf");

        string szExportPath = szRootPath + "Script/Protobuf/";
        szExportPath = szExportPath.Replace('/', '\\');
        System.Diagnostics.Process.Start("explorer.exe", szExportPath);
    }
    [MenuItem("FCScript/导出Protobuf", false, 5)]
    static void ExportProtobuf()
    {
        ExprtProtobufToFC(false, false);
    }
    [MenuItem("FCScript/导出Protobuf(调试)", false, 5)]
    static void ExportProtobufFull()
    {
        ExprtProtobufToFC(true, false);
    }
};