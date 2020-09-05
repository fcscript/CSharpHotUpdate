using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FCSerialize
{
    // 功能：设置读模式, 流的数据由外部指定
    public void ReadMode(List<char> array, int nStart, int nSize)
    {

    }
    // 功能：设置读模式, 流的数据由已知的流中获取
    // 参数：nStart - 开始的位置
    //       nSize - 数据流的长度
    public void ReadMode(FCSerialize ar, int nStart, int nSize)
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
    // 功能：返回当前流的缓冲区的最大长度
    // 说明：这个并不是指数据的有效长度，而是整个缓冲区的总长。但在读模式下，这个一般是指数据的长度
    public int GetBufferSize()
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

    // 功能：重新分配写入缓冲区的长度
    // 参数：nSize - 新的缓冲区的长度
    // 说明：这个仅在写入模式下有效
    public bool Reserve(int nSize)
    {
        return true;
    }
    // 功能：读或写一个变量
    // 参数：value - 任意数据类型（基本数据类型 + 自定义的class)
    // 说明：value默认是引用传递的，如果class，读模式下如果为NULL，就会自动创建一个       
    public void ReadWrite<_Ty>(_Ty value)
    {
    }

    // 功能：从字符串的指定位置，读取一个定长的字符串，并写入到value中
    // 参数：value - 输入并输出的字符串变量
    //       nStart - 从value的指定写入位置
    //       nSize - 读取写入的长度
    // 说明：value在这个接口中，是引用传递的，并不需要指定ref
    //       最终value会被修改
    public bool ReadFixed(StringA value, int nStart, int nSize)
    {
        return false;
    }
    // 功能：从数组指定位置，读取一个定长的数组，并写入到value中
    // 参数：value - 输入并输出的数组变量
    //       nStart - 从value的指定写入位置
    //       nSize - 读取写入的长度
    // 说明：仅支持基本的数据类型（如boo, char, byte, short, ushort, int, uint, long, float, double, StringA)
    //       不支持对象数组
    public bool ReadFixed<_Ty>(List<_Ty> value, int nStart, int nSize)
    {
        return false;
    }
    // 功能：从字符串的指定位置，读取一个定长的字符串，并写入到数据流
    // 参数：value - 字符串变量
    //       nStart - 从value的指定读取位置
    //       nSize - 写入数据的长度
    public bool WriteFixed(StringA value, int nStart, int nSize)
    {
        return false;
    }
    // 功能：从数组指定位置，读取一个定长的数组，并写入到数据流
    // 参数：value - 数组变量
    //       nStart - 从value的指定写入位置
    //       nSize - 读取写入的长度
    // 说明：仅支持基本的数据类型（如boo, char, byte, short, ushort, int, uint, long, float, double, StringA)
    //       不支持对象数组
    public bool WriteFixed<_Ty>(List<_Ty> value, int nStart, int nSize)
    {
        return false;
    }

    // 以下是关于Protobuf相关的接口

    // 功能：读取一个PB的TAG头
    public int ProtobufReadTag()
    {
        return 0;
    }
    // 功能：按Tag的头，跳过流的读取
    public void ProtobufSkipField(int nTag)
    {

    }
    // 功能：读取一个整型变量
    public bool ProtobufReadVarint<_Ty>(_Ty value)
    {
        return false;
    }

    // 功能：读取一个PB对象
    public bool ProtobufRead<_Ty>(_Ty value, int nFiledIndex, PB_ZipType keyZipType)
    {
        return false;
    }

    // 功能：写入一个PB对象
    public void ProtobufWrite<_Ty>(_Ty value, int nFieldIndex, PB_ZipType nType)
    {
    }

    // 功能：读取map对象
    // 说明：这里sMap参数是引用传递，如果你想加上ref, 就得修改导出的代码，因为代码的代码目前没有加 ref
    public bool ProtobufReadMap<_TyKey, _TyValue>(map<_TyKey, _TyValue> sMap, int nFiledIndex, PB_ZipType keyZipType, PB_ZipType valueZipType)
    {
        return false;
    }

    // 功能：写入map对象
    public void ProtobufWriteMap<_TyKey, _TyValue>(map<_TyKey, _TyValue> sMap, int nFiledIndex, PB_ZipType keyZipType, PB_ZipType valueZipType)
    {

    }
};

// 这个类型会在PB导出，也自动导出一下，所以，如果真的有导致，请删除下面的的声明，留下自动导出的
public enum PB_ZipType
{
    PB_Zip_Varint,    // 变长
    PB_Zip_Fixed,     // 定长
    PB_Zip_ZigZag,    // ZigZag压缩
};
