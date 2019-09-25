using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CSerialize
{
    // 功能：设置读模式, 流的数据由外部指定
    public void ReadMode(List<char> array, int nStart, int nSize)
    {

    }
    // 功能：设置读模式, 流的数据由外部指定
    // 参数：szData - 二进流数据
    public void ReadMode(StringA szData)
    {

    }
    // 功能：设置写模式, 流的内存外部指定
    // 参数：array - 要输出的数据流
    //       nStart - 开始写入的位置
    //       nSize - 要写入的长度
    // 说明：将数据流写入到array中
    public void WriteMode(List<char> array, int nStart, int nSize)
    {

    }
    // 功能：设置写模式, 流的内存由自己维护
    public void OwnWriteMode(int nSize)
    {

    }
    // 功能：测试写模式，不会发生真正的数据写入，用于求对象序列化后的长度
    public void TestWriteMode()
    {

    }
    // 功能：跳过指定字节
    public void Skip(int nSkipSize)
    {

    }
    // 功能：设置相对于首地址的偏移
    public void Seek(int nPos)
    {

    }
    // 功能：将序列化后的数据拷贝到数组
    public void CopyTo(List<char> aOut)
    {

    }
    // 功能：将序列化后的数据拷贝到字符串
    // 说明：szOut实际并不是一个真正的字符串，而是一个二进制数据
    public void CopyTo(StringA szOut)
    {
    }
    // 功能：取当前流的指针位置
    public int Position()
    {
        return 0;
    }
    // 功能：测试当前流是不是读模式
    public bool IsReadMode()
    {
        return true;
    }
    // 功能：测试是不是可以读取指定字节大小的数据
    //       测试当前是不是有指定大小数据可读
    public bool IsCanRead(int nReadSize)
    {
        return true;
    }
    // 功能：将当前指针跳到流数据末尾
    public void SkipToEnd()
    {

    }
    // 功能：读或写一个变量
    // 参数：value - 任意数据类型（基本数据类型 + 自定义的class)
    // 说明：value默认是引用传递的，如果class，读模式下如果为NULL，就会自动创建一个       
    public void ReadWrite<_Ty>(_Ty value)
    {
    }
};