using System;
using System.Collections.Generic;
using System.Text;

public struct StringA
{
    // 功能：字符串与基础数据类型的连接
    // 参数：base_type - 基础数据类型
    //public static StringT a + b + c + ... + n ; // 多个字符串连接 a, b 其中一必须是字符串类型，(c, ... n为任意基础数据类型)

    public static implicit operator StringA(string _str)
    {
        return new StringA();
    }
    
    public static implicit operator StringA(int _str)
    {
        return new StringA();
    }
    public static implicit operator StringA(float _str)
    {
        return new StringA();
    }
    public static implicit operator StringA(uint _str)
    {
        return new StringA();
    }
    public static implicit operator StringA(byte _str)
    {
        return new StringA();
    }
    public static implicit operator StringA(short _str)
    {
        return new StringA();
    }
    public static implicit operator StringA(ushort _str)
    {
        return new StringA();
    }
    public static implicit operator StringA(Int64 _str)
    {
        return new StringA();
    }
    public static implicit operator StringA(UInt64 _str)
    {
        return new StringA();
    }
    public static implicit operator StringA(double _str)
    {
        return new StringA();
    }

    public static StringA operator + (StringA left, int right)
    {
        return left;
    }
    public static StringA operator +(StringA left, char right)
    {
        return left;
    }
    public static StringA operator +(StringA left, byte right)
    {
        return left;
    }
    public static StringA operator +(StringA left, short right)
    {
        return left;
    }
    public static StringA operator +(StringA left, ushort right)
    {
        return left;
    }
    public static StringA operator +(StringA left, float right)
    {
        return left;
    }
    public static StringA operator +(StringA left, double right)
    {
        return left;
    }
    public static StringA operator +(StringA left, Int64 right)
    {
        return left;
    }
    public static StringA operator +(StringA left, UInt64 right)
    {
        return left;
    }

    public StringA Clone() // 克隆自己
    {
        return this;
    }
    
    public char this[int nIndex]
    {
        get { return (char) 0; }
    }
    public int GetLength()
    {
        return 0;
    }
    public int Length         // 取字符串的长度
    {
        get { return 0; }
    }
    public char GetLastChar()  // 得到最后一个字符
    {
        return (char) 0;
    }
    public void SetAt(int nIndex, char ch) // 设置字符
    {

    }
    public bool IsEmpty()
    {
        return false;
    }
    public void Empty()
    {

    }
    public void Swap(StringA other)
    {

    }
    public void Preallocate(int nSize) // 预分配内存
    {

    }
    public void Reserve(int nSize)  // 预分配内存
    {

    }
    public void Format(StringA format, params object[] args) // Format("{0},{1}", i, i); // 参考C#的String.Format
    {

    }
    public void AppendFormat(StringA format, params object[] args) // Format("{0},{1}", i, i); // 参考C#的String.Format
    {

    }
    public void SetString(StringA str, int iStart, int nLen)
    {

    }
//    public void SetString(StringW str, int iStart, int nLen)
//    {
//
//    }
    public void AppendChar(char ch, int nCount)
    {

    }
    public void Append(StringA str, int iStart, int nLen)
    {

    }
    public int ToInt()
    {
        return 0;
    }
    public Int64 ToInt64()
    {
        return 0;
    }
    public float ToFloat()
    {
        return 0;
    }
    public double ToDouble()
    {
        return 0;
    }

    public StringA Left(int nCount)
    {
        return this;
    }
    public StringA Right(int nCount)
    {
        return this;
    }
    public StringA Mid(int iStart)
    {
        return this;
    }
    public StringA Mid(int iStart, int nCount)
    {
        return this;
    }
    public void TrimRight() // 删除右侧的空格
    {

    }
    public void TrimLeft()  // 删除左侧的空格
    {

    }
    public void Trim()  // 删除两端的空格
    {

    }
    public StringA Tokenize(StringA szTokens, ref int iStart )  // 分隔符查找
    {
        return this;
    }
    public void Delete(int iIndex, int nCount)
    {

    }
    public void DelMiddle(int lowCh, int hightCh, int nBegin, int nEnd)
    {

    }
    public int Insert(int iIndex, char ch, int nCount)
    {
        return 0;
    }
    public int Insert(int iIndex, char ch)
    {
        return 0;
    }
    public int Insert(int iIndex, StringA psz)
    {
        return 0;
    }
    public int Replace(char chOld, char chNew)
    {
        return 0;
    }
    public int Replace(StringA szOld, StringA szNew)
    {
        return 0;
    }
    public int Remove(char chRemove)
    {
        return 0;
    }
    public void MakeUpper() // 转换成大写
    {

    }
    public void MakeLower() // 转换成小写
    {

    }
    public int Compare(StringA other)  // 比较字符串, 返回-1是小于，0是相等，1是大于
    {
        return 0;
    }
    public int CompareNoCase(StringA other) // 比较字符串，忽略大小写
    {
        return 0;
    }
    public bool CompareWithWildcard(StringA szWildcard, bool bNoCase) // 使用通配符判断两个字符串是不是相似, bNoCase : true忽略大小写; false比较大小写
    {
        return false;
    }
    public int Find(char ch, int iStart)  // 从前向后查找指定的字符
    {
        return 0;
    }
    public int Find(StringA substr, int iStart)
    {
        return 0;
    }
    public int FindNoCase(StringA substr, int iStart)
    {
        return 0;
    }
    public int ReverseFind(char ch) // 从最后一个字符开始向前查找
    {
        return 0;
    }
    public int ReverseFind(char ch, int nStart)  // 从nStart位置向前查找
    {
        return 0;
    }
    public void Inserve() // 将字符串反序
    {
    }

    public static bool operator ==(StringA left, StringA other)
    {
        return false;
    }
    public static bool operator !=(StringA left, StringA other)
    {
        return false;
    }
    public static bool operator >(StringA left, StringA other)
    {
        return false;
    }
    public static bool operator <(StringA left, StringA other)
    {
        return false;
    }
    public static bool operator >=(StringA left, StringA other)
    {
        return false;
    }
    public static bool operator <=(StringA left, StringA other)
    {
        return false;
    }
}

