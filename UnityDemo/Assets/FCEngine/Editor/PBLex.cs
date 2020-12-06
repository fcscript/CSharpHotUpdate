using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 以下代码从C++拷贝而来

enum pb_lex_words_type
{
    lex_none,
    lex_value,         // 仅是值或词组
    lex_string_a,      // 字符串(常量)

    lex_left_bracket,  // 左括号   (
    lex_right_bracket, // 右括号   )
    lex_left_brace,    // 左大括号 {
    lex_right_brace,   // 右大括号 }
    lex_bracket_1,     // [ 中括号
    lex_bracket_2,     // ] 中括号
    lex_semicolon,     // 分号;
    lex_comma,         // 逗号,
    lex_set,           // =
    lex_greate,        // >
    lex_less,          // <

    lex_syntax,        // syntax
    lex_option,        // option
    lex_package,       // package
    lex_import,        // import
    lex_service,       // service
    lex_default,       // default

    lex_enum,          // enum
    lex_message,       // message
    lex_oneof,         // oneof

    lex_repeated,      // repeated 数组
    lex_map,           // map
};

enum pb_type
{
    pb_none,

    // 变长编码的数字
    pb_int32,
    pb_uint32,
    pb_bool,
    pb_enum,
    pb_int64,
    pb_uint64,

    // 变长编码的数字, 使用 ZigZag 编码
    pb_sint32,
    pb_sint64,

    // 定长的64位数字
    pb_fixed64,
    pb_sfixed64,
    pb_double,

    // 定长的32位数字
    pb_fixed32,
    pb_sfixed32,
    pb_float,

    // 变长字符串
    pb_string,

    // 枚举

    // 对象
    pb_object,
};

struct pb_lex_string_ptr  // 兼容C++的指针,尽量不改代码
{
    string m_pcsIn;
    int m_nStart;  // 相对位置
    int m_nLen;

    public pb_lex_string_ptr(string pcsIn)
    {
        m_pcsIn = pcsIn;
        m_nStart = 0;
        m_nLen = string.IsNullOrEmpty(pcsIn) ? 0 : m_pcsIn.Length;
    }
    public pb_lex_string_ptr(string pcsIn, int nStart, int nLen)
    {
        m_pcsIn = pcsIn;
        m_nStart = nStart;
        m_nLen = nLen;
    }
    public void SetString(string pcsIn, int nStart = 0, int nLen = 0)
    {
        m_pcsIn = pcsIn;
        m_nStart = nStart;
        m_nLen = nLen;
    }
    public void NextStep()
    {
        ++m_nStart;
    }
    public void Skip(int nLen)
    {
        m_nStart += nLen;
    }
    public bool IsCanRead()
    {
        return m_nStart < m_nLen;
    }
    public int Offset
    {
        get { return m_nStart; }
    }
    public int Length
    {
        get { return m_nLen; }
    }
    public static int operator -(pb_lex_string_ptr left, pb_lex_string_ptr right)
    {
        return left.m_nStart - right.m_nStart;
    }
    public static pb_lex_string_ptr operator + (pb_lex_string_ptr left, int nLen)
    {
        pb_lex_string_ptr ptr = left;
        ptr.Skip(nLen);
        return ptr;
    }
    public char this[int nIndex]
    {
        get { return (nIndex >= 0 && nIndex < m_nLen) ? m_pcsIn[m_nStart + nIndex] : (char)0; }
    }
    public string GetString(int nLen)
    {
        return m_pcsIn.Substring(m_nStart, nLen);
    }
};

class pb_lex_words
{
    public pb_lex_string_ptr m_pcsIn;     // 字符串
    public int m_nCodeLine; // 相对于文件开头的字符位置
    public int m_nLen;

    public int m_nLine;     // 起始行
    public int m_nLineStart;// 相对于行的字符数
    public pb_lex_words_type m_type;       // 类型
    public string m_pcsFileName;
    public char  FirstChar()
    {
        return m_pcsIn[0];
    }
    public int Len()
    {
        return m_nLen;
    }
    public string  GetString()
	{
        return m_pcsIn.GetString(m_nLen);
	}
    public pb_lex_words Clone()
    {
        pb_lex_words node = new pb_lex_words();
        node.m_pcsIn = m_pcsIn;
        node.m_nCodeLine = m_nCodeLine;
        node.m_nLen = m_nLen;
        node.m_nLine = m_nLine;
        node.m_nLineStart = m_nLineStart;
        node.m_type = m_type;
        node.m_pcsFileName = m_pcsFileName;
        return node;
    }
};

class pb_lex_words_contain
{
    public List<pb_lex_words> m_Words = new List<pb_lex_words>();
    public void  push_back(pb_lex_words words)
    {
        m_Words.Add(words.Clone());
    }
    public void reserve(int nSize)
    {
        m_Words.Capacity = nSize;
    }
}

enum PBValueType
{
    Value_Base,   // 基础变量
    Value_Array,  // 数组
    Value_Map,    // map
};

struct PBBaseValue
{
    public pb_type m_type;
    public string m_szType; // 变量类型
};

enum PB_WireType
{
    WIRETYPE_VARINT = 0,   // 变长编码的数字, 适用类型 int32、int64、uint32、uint64、bool、enum, sint32, sint64
    WIRETYPE_FIXED64 = 1,   // fixed64, sfixed64, double 定长的数字
    WIRETYPE_LENGTH_DELIMITED = 2,   // 表示重复的数据类型，如数组，map之类的
    WIRETYPE_START_GROUP = 3,   // 不再使用了
    WIRETYPE_END_GROUP = 4,   // 不再使用了
    WIRETYPE_FIXED32 = 5,   // fixed32, sfixed32, float, 定长的数字
};

class PBLex
{
    public static string GetBaseValueTypeName(PBBaseValue value)
    {
	    switch(value.m_type)
	    {
	    case pb_type.pb_int32:
		    return "int";
	    case pb_type.pb_uint32:
		    return "uint";
	    case pb_type.pb_bool:
		    return "bool";
	    case pb_type.pb_int64:
		    return "long";
	    case pb_type.pb_uint64:
		    return "ulong";
	    case pb_type.pb_sint32:
		    return "int";
	    case pb_type.pb_sint64:
		    return "long";
	    case pb_type.pb_fixed64:
		    return "long";
	    case pb_type.pb_sfixed64:
		    return "long";
	    case pb_type.pb_double:
		    return "double";
	    case pb_type.pb_fixed32:
		    return "int";
	    case pb_type.pb_sfixed32:
		    return "int";
	    case pb_type.pb_float:
		    return "float";
	    case pb_type.pb_string:
		    return "StringA";
	    default:
		    break;
	    }
	    return value.m_szType;
    }
    public static string GetZipType(pb_type nType)
    {
	    switch(nType)
	    {
	    case pb_type.pb_sint32: // 压缩的变量, ZipFag压缩的
	    case pb_type.pb_sint64:
		    return "PB_Zip_ZigZag";
	    case pb_type.pb_fixed64:  // 定长的变量
	    case pb_type.pb_sfixed64:
	    case pb_type.pb_double:
	    case pb_type.pb_fixed32:
	    case pb_type.pb_sfixed32:
	    case pb_type.pb_float:
		    return "PB_Zip_Fixed";
	    case pb_type.pb_enum:
		    return "PB_Zip_Varint";
	    case pb_type.pb_string:
		    return "PB_Zip_Varint";
	    case pb_type.pb_object:
		    return "PB_Zip_Varint";
	    default:
		    return "PB_Zip_Varint";
	    }
    }

    public static int MakeTag(int nFieldIndex, PB_WireType type)
    {
        return (nFieldIndex << 3) | (int)type;
    }

    public static PB_WireType PBTypeToWriteType(pb_type nType)
    {
        switch (nType)
        {
            case pb_type.pb_int32:
            case pb_type.pb_uint32:
            case pb_type.pb_bool:
            case pb_type.pb_enum:
            case pb_type.pb_int64:
            case pb_type.pb_uint64:
            case pb_type.pb_sint32:
            case pb_type.pb_sint64:
                return PB_WireType.WIRETYPE_VARINT;
            case pb_type.pb_fixed64:
            case pb_type.pb_sfixed64:
            case pb_type.pb_double:
                return PB_WireType.WIRETYPE_FIXED64;
            case pb_type.pb_fixed32:
            case pb_type.pb_sfixed32:
            case pb_type.pb_float:
                return PB_WireType.WIRETYPE_FIXED32;
            case pb_type.pb_string:
            case pb_type.pb_object:
                return PB_WireType.WIRETYPE_LENGTH_DELIMITED;
            default:
                break;
        }
        return PB_WireType.WIRETYPE_LENGTH_DELIMITED;
    }

    public static int PB_MakeTag(int nFiledIndex, pb_type nType, PBValueType nValueType)
    {
        if (PBValueType.Value_Array == nValueType
            || PBValueType.Value_Map == nValueType)
            return MakeTag(nFiledIndex, PB_WireType.WIRETYPE_LENGTH_DELIMITED);
        else
        {
            PB_WireType nWiretType = PBTypeToWriteType(nType);
            return MakeTag(nFiledIndex, nWiretType);
        }
    }

    public static int PB_GetWireType(pb_type nType, PBValueType nValueType)
    {
        if (PBValueType.Value_Array == nValueType
            || PBValueType.Value_Map == nValueType)
            return (int)PB_WireType.WIRETYPE_LENGTH_DELIMITED;
        else
            return (int)PBTypeToWriteType(nType);
    }

    public static int FindNextWords(List<pb_lex_words> aWords, pb_lex_words_type leftType, pb_lex_words_type rightType, int nStart, int nEnd)
    {
        int nRef = 0;
        for(int i = nStart; i<nEnd; ++i)
        {
            if( aWords[i].m_type == leftType )
            {
                ++nRef;
            }
            else if(aWords[i].m_type == rightType)
            {
                if(pb_lex_words_type.lex_none != leftType )
                    --nRef;
                if(nRef == 0 )
                    return i;
            }
        }
        return -1;
    }

    public static int FindFirstWords(List<pb_lex_words> aWords, pb_lex_words_type nType1, pb_lex_words_type nType2, int nStart, int nEnd)
    {
        for (int i = nStart; i < nEnd; ++i)
        {
            if (aWords[i].m_type == nType1
                || aWords[i].m_type == nType2)
            {
                return i;
            }
        }
        return -1;
    }

    public static bool IsWords(pb_lex_string_ptr pcsIn, string pcsWords)
    {
        int i = 0, nLen2 = pcsWords.Length;
        for(; i< nLen2; ++i)
        {
            if (pcsIn[i] != pcsWords[i])
                return false;
        }
        char ch = pcsIn[i];
        if( (ch >= 'a' && ch <= 'z')
            || (ch >= 'A' && ch <= 'Z')
            || (ch >= '0' && ch <= '9')
            || ch == '_'
            )
            return false;
        return true;
    }
    public static pb_type GetPBType(pb_lex_words words)
    {
        char ch = words.FirstChar();
        switch (ch)
        {
            case 's':
                if (IsWords(words.m_pcsIn, "sint32"))
                    return pb_type.pb_sint32;
                if (IsWords(words.m_pcsIn, "sint64"))
                    return pb_type.pb_sint64;
                if (IsWords(words.m_pcsIn, "sfixed32"))
                    return pb_type.pb_sfixed32;
                if (IsWords(words.m_pcsIn, "sfixed64"))
                    return pb_type.pb_sfixed64;
                if (IsWords(words.m_pcsIn, "string"))
                    return pb_type.pb_string;
                break;
            case 'i':
                if (IsWords(words.m_pcsIn, "int32"))
                    return pb_type.pb_int32;
                if (IsWords(words.m_pcsIn, "int64"))
                    return pb_type.pb_int64;
                break;
            case 'f':
                if (IsWords(words.m_pcsIn, "float"))
                    return pb_type.pb_float;
                if (IsWords(words.m_pcsIn, "fixed32"))
                    return pb_type.pb_fixed32;
                if (IsWords(words.m_pcsIn, "fixed64"))
                    return pb_type.pb_fixed64;
                break;
            case 'u':
                if (IsWords(words.m_pcsIn, "uint32"))
                    return pb_type.pb_uint32;
                if (IsWords(words.m_pcsIn, "uint64"))
                    return pb_type.pb_uint64;
                break;
            case 'b':
                if (IsWords(words.m_pcsIn, "bool"))
                    return pb_type.pb_bool;
                if (IsWords(words.m_pcsIn, "bytes"))
                    return pb_type.pb_string;
                break;
            case 'e':
                if (IsWords(words.m_pcsIn, "enum"))
                    return pb_type.pb_enum;
                break;
            default:
                break;
        }
        return pb_type.pb_none;
    }
    public static int GetNumber(pb_lex_words words)
    {
        int nNumb = 0;
        bool bFind = false;
        int ch;
        for (int i = 0; i < words.Len(); ++i)
        {
            ch = words.m_pcsIn[i] - '0';
            if (ch >= 0 && ch <= 9)
            {
                nNumb *= 10;
                nNumb += ch;
                bFind = true;
            }
            else if (bFind)
            {
                break;
            }
        }
        return nNumb;
    }

    public static bool IsHaveUTF8(pb_lex_string_ptr pcsStr)
    {
        if( pcsStr.Offset + 3 > pcsStr.Length)
            return false;
        return pcsStr[0] == 0xef
            && pcsStr[1] == 0xbb
            && pcsStr[2] == 0xbf;
    }

    public static bool IsHaveUTF16(pb_lex_string_ptr pcsStr)
    {
        if (pcsStr.Offset + 4 > pcsStr.Length)
            return false;
        return (pcsStr[0] == 0xfe
            && pcsStr[1] == 0xff)
            || (pcsStr[0] == 0xff
            && pcsStr[1] == 0xfe);
    }

    class pb_lex_words_ex : pb_lex_words
    {
        public string m_pcsThisFileName;
        public pb_lex_string_ptr m_pcsStart;
        public int m_nStartCode;
        public pb_lex_string_ptr m_pcsCurLine;
        public int m_nCurLine;   // 当前行号
        public bool m_bEscapeChar; // 是不是有转义字符
        public pb_lex_words_contain m_pWords;
        public pb_lex_words_ex(pb_lex_string_ptr pcsStart, int nStartCode, pb_lex_words_contain pWords)
        {
            m_pcsStart = pcsStart;
            m_nStartCode = nStartCode;
            m_pWords = pWords;
            m_nCurLine = 0;
            m_bEscapeChar = false;
        }
        public void push_char(pb_lex_string_ptr pcsIn, pb_lex_words_type type, int nLen = 1)
        {
            if (m_type != type && m_nLen > 0 )
            {
                Flush();
            }
            if (m_nLen == 0)
            {
                m_nLine = m_nCurLine;
                m_nLineStart = (int)(pcsIn - m_pcsCurLine);
                m_pcsIn = pcsIn;
                m_nLen = nLen;
                m_nCodeLine = m_nStartCode + (int)(pcsIn - m_pcsStart);
                m_type = type;
            }
            else
            {
                m_nLen += nLen;
            }
        }
        // 功能：添加一个转义符
        public void push_escape_char(pb_lex_string_ptr pcsIn, char ch, pb_lex_words_type type)
        {
            if (m_type != type && m_nLen > 0)
            {
                Flush();
            }
            if (!m_bEscapeChar)
            {
                m_bEscapeChar = true;
            }
            if (m_nLen == 0)
            {
                m_nLine = m_nCurLine;
                m_nLineStart = (int)(pcsIn - m_pcsCurLine);

                m_pcsIn = pcsIn;
                m_nLen = 1;
                m_nCodeLine = m_nStartCode + (int)(pcsIn - m_pcsStart);
                m_type = type;
            }
            else
            {
                m_nLen++;
            }
        }
        // 功能:添加关键字
        public void push_single_key(pb_lex_string_ptr pcsIn, pb_lex_words_type type, int nLen = 1)
        {
            if (m_nLen > 0)
            {
                Flush();
            }
            if (m_nLen == 0)
            {
                m_nLine = m_nCurLine;
                m_nLineStart = (int)(pcsIn - m_pcsCurLine);

                m_pcsIn = pcsIn;
                m_nLen = nLen;
                m_nCodeLine = m_nStartCode + (int)(pcsIn - m_pcsStart);
                m_type = type;
            }
            else
            {
                m_nLen += nLen;
            }
            Flush();
        }
        // 功能：判断是不是可以添加关键字
        public bool IsCanPushKeyWords(pb_lex_string_ptr pcsIn, string pcsWords)
        {
            if(m_nLen > 0)
                return false;
            return IsWords(pcsIn, pcsWords);
        }
        // 功能：尝试添加关键词
        public void TryPushKeyWords(ref pb_lex_string_ptr pcsIn, string pcsWords, int nWordsLen, pb_lex_words_type type)
        {
            if (IsCanPushKeyWords(pcsIn, pcsWords))
            {
                push_char(pcsIn, type, nWordsLen);
                Flush();
                pcsIn.Skip(nWordsLen - 1);
            }
            else
            {
                push_char(pcsIn, pb_lex_words_type.lex_value);
            }
        }
        public void PushKeyWords(ref pb_lex_string_ptr pcsIn, pb_lex_words_type type, int nWordsLen)
        {
            push_char(pcsIn, type, nWordsLen);
            Flush();
            pcsIn.Skip(nWordsLen - 1);
        }
        public void Flush()
        {
            if (m_nLen > 0)
            {
                m_pcsFileName = m_pcsThisFileName;
                m_pWords.push_back(this);
                clear();
            }
        }
        public void FlushString()
        {
            if (m_nLen > 0)
            {
                Flush();
            }
            else
            {
                m_pcsFileName = m_pcsThisFileName;
                m_pWords.push_back(this);
            }
            clear();
        }
        public void SetLine(pb_lex_string_ptr pcsIn, pb_lex_words_type type)
        {
            m_nCodeLine = m_nStartCode + (int)(pcsIn - m_pcsStart);
            m_type = type;
            m_nLine = m_nCurLine;
            m_nLineStart = (int)(pcsIn - m_pcsCurLine);
            m_pcsIn = pcsIn;
        }
        public void clear()
        {
            m_nLen = 0;
            m_type = pb_lex_words_type.lex_none;
            m_bEscapeChar = false;
        }
    };

    public static void AnylseProtoFile(pb_lex_words_contain contain, pb_lex_string_ptr pcsIn, string pcsFileName)
    {
        contain.reserve(10000);

        int nStartCode = pcsIn.Offset;
        // 这个只是针对C/C++语法
        int nHelp = 0;
        pb_lex_words_type nHelpType = pb_lex_words_type.lex_value;
        pb_lex_words_ex words = new pb_lex_words_ex(pcsIn, nStartCode, contain);
        words.m_nCurLine = 1;
        words.m_pcsCurLine = pcsIn;
        words.m_pcsFileName = pcsFileName;
        words.m_pcsThisFileName = pcsFileName;

        if (IsHaveUTF8(pcsIn))
            pcsIn.Skip(3);
        else if (IsHaveUTF16(pcsIn))
            pcsIn.Skip(4);

        char ch;
        for (; pcsIn.IsCanRead(); pcsIn.NextStep())
        {
            ch = pcsIn[0];
            if (nHelp > 0)
            {
                if (nHelp == 1)  // C++风格注释
                {
                    if (ch == '\r')
                    {
                        words.m_nCurLine++;
                        if (pcsIn[1] == '\n')
                            pcsIn.NextStep();
                        words.m_pcsCurLine = pcsIn + 1;
                        nHelp = 0;
                    }
                    else if (ch == '\n')
                    {
                        words.m_nCurLine++;
                        words.m_pcsCurLine = pcsIn + 1;
                        nHelp = 0;
                    }
                }
                else if (nHelp == 2) // C 风格注释
                {
                    if (ch == '\r')
                    {
                        words.m_nCurLine++;
                        if (pcsIn[1] == '\n')
                            pcsIn.NextStep();
                        words.m_pcsCurLine = pcsIn + 1;
                    }
                    else if (ch == '\n')
                    {
                        words.m_nCurLine++;
                        words.m_pcsCurLine = pcsIn + 1;
                    }
                    else if (ch == '*' && pcsIn[1] == '/')
                    {
                        pcsIn.NextStep();
                        nHelp = 0;
                    }
                }
                else if (nHelp == 3)
                {
                    if (ch == '\"')
                    {
                        nHelp = 0;
                        words.FlushString();
                    }
                    else
                    {
                        words.push_char(pcsIn, nHelpType);
                    }
                }
                else if (nHelp == 4)
                {
                    if (ch == '\'')
                    {
                        nHelp = 0;
                        words.FlushString();
                    }
                    else
                    {
                        words.push_char(pcsIn, nHelpType);
                    }
                }
                continue;
            }
            else
            {
                switch (ch)
                {
                    case '/':
                        {
                            if (pcsIn[1] == '/')
                            {
                                nHelp = 1;
                                pcsIn.NextStep();
                            }
                            else if (pcsIn[1] == '*')
                            {
                                nHelp = 2;
                                pcsIn.NextStep();
                            }
                            else if (pcsIn[1] == '=')
                            {
                                pcsIn.NextStep();
                            }
                            else
                            {
                                //words.push_char(pcsIn, lex_div);
                            }
                        }
                        break;
                    case '\"':
                        nHelp = 3;
                        words.Flush();
                        words.SetLine(pcsIn, pb_lex_words_type.lex_string_a);
                        nHelpType = pb_lex_words_type.lex_string_a;
                        break;
                    case '\'':
                        words.Flush();
                        break;
                    case ' ':
                    case '\t':
                        {
                            if (words.m_nLen > 0)
                            {
                                words.Flush();
                            }
                        }
                        break;
                    case '\r':
                        {
                            words.m_nCurLine++;
                            if (pcsIn[1] == '\n')
                                pcsIn.NextStep();
                            words.m_pcsCurLine = pcsIn + 1;
                        }
                        break;
                    case '\n':
                        {
                            words.m_nCurLine++;
                            words.m_pcsCurLine = pcsIn + 1;
                        }
                        break;
                    case '=':  // =  ==
                        words.push_single_key(pcsIn, pb_lex_words_type.lex_set);
                        break;
                    case '<':  // <  <=  <<  <<=
                        words.push_single_key(pcsIn, pb_lex_words_type.lex_less);
                        break;
                    case '>':  // >  >=  >>  >>=
                        words.push_single_key(pcsIn, pb_lex_words_type.lex_greate);
                        break;
                    case ';':
                        {
                            words.push_single_key(pcsIn, pb_lex_words_type.lex_semicolon);
                        }
                        break;
                    case ',':
                        {
                            words.push_single_key(pcsIn, pb_lex_words_type.lex_comma);
                        }
                        break;
                    case '(':
                        {
                            words.push_single_key(pcsIn, pb_lex_words_type.lex_left_bracket);
                        }
                        break;
                    case ')':
                        {
                            words.push_single_key(pcsIn, pb_lex_words_type.lex_right_bracket);
                        }
                        break;
                    case '{':
                        {
                            words.push_single_key(pcsIn, pb_lex_words_type.lex_left_brace);
                        }
                        break;
                    case '}':
                        {
                            words.push_single_key(pcsIn, pb_lex_words_type.lex_right_brace);
                        }
                        break;
                    case '[':
                        {
                            words.push_single_key(pcsIn, pb_lex_words_type.lex_bracket_1);
                        }
                        break;
                    case ']':
                        {
                            words.push_single_key(pcsIn, pb_lex_words_type.lex_bracket_2);
                        }
                        break;
                    case 'd': // default
                        {
                            if (words.IsCanPushKeyWords(pcsIn, "default"))
                                words.PushKeyWords(ref pcsIn, pb_lex_words_type.lex_default, 7);
                            else
                                words.push_char(pcsIn, pb_lex_words_type.lex_value);
                        }
                        break;
                    case 'i':  // if
                        {
                            if (words.IsCanPushKeyWords(pcsIn, "import"))
                                words.PushKeyWords(ref pcsIn, pb_lex_words_type.lex_import, 6);
                            else
                                words.push_char(pcsIn, pb_lex_words_type.lex_value);
                        }
                        break;
                    case 'e':  // else
                        if (words.IsCanPushKeyWords(pcsIn, "enum"))
                            words.PushKeyWords(ref pcsIn, pb_lex_words_type.lex_enum, 4);
                        else
                            words.push_char(pcsIn, pb_lex_words_type.lex_value);
                        break;
                    case 'r':
                        if (words.IsCanPushKeyWords(pcsIn, "repeated"))
                            words.PushKeyWords(ref pcsIn, pb_lex_words_type.lex_repeated, 8);
                        else
                            words.push_char(pcsIn, pb_lex_words_type.lex_value);
                        break;
                    case 's':
                        if (words.IsCanPushKeyWords(pcsIn, "syntax"))
                            words.TryPushKeyWords(ref pcsIn, "syntax", 6, pb_lex_words_type.lex_syntax);
                        else if (words.IsCanPushKeyWords(pcsIn, "service"))
                            words.TryPushKeyWords(ref pcsIn, "service", 7, pb_lex_words_type.lex_service);
                        else
                            words.push_char(pcsIn, pb_lex_words_type.lex_value);
                        break;
                    case 'm':
                        if (words.IsCanPushKeyWords(pcsIn, "message"))
                            words.TryPushKeyWords(ref pcsIn, "message", 7, pb_lex_words_type.lex_message);
                        else if (words.IsCanPushKeyWords(pcsIn, "map"))
                            words.TryPushKeyWords(ref pcsIn, "map", 3, pb_lex_words_type.lex_map);
                        else
                            words.push_char(pcsIn, pb_lex_words_type.lex_value);
                        break;
                    case 'o':
                        words.TryPushKeyWords(ref pcsIn, "oneof", 5, pb_lex_words_type.lex_oneof);
                        break;
                    case 'p':
                        if (words.IsCanPushKeyWords(pcsIn, "package"))
                            words.PushKeyWords(ref pcsIn, pb_lex_words_type.lex_package, 7);
                        else
                            words.push_char(pcsIn, pb_lex_words_type.lex_value);
                        break;
                    default:
                        words.push_char(pcsIn, pb_lex_words_type.lex_value);
                        break;
                }
            }
        }
        words.Flush();
    }
}