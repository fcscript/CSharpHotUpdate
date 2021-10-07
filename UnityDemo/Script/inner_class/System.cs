using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

// 原本这个类名，叫System, 但这个关键字被C#本身占用了，所以用os也是一样的
class os
{
    // 功能：返回当前系统时钟，单位是微秒(1000微秒 = 1毫秒)
    public static Int64 clock()
    {
        return 0;
    }
    // 功能：返回当前系统时钟，单位是毫秒(1000毫秒 = 1秒)
    public static uint GetTickCount()
    {
        return 0;
    }
    // 功能：设置随机函数的初始种子,相当于C函数的srand
    public static void srand(uint nRandSeed)
    {

    }
    // 功能：随机一个整数, 区间[nMin, nMax)
    public static int RandInt(int nMin, int nMax)
    {
        return 0;
    }
    // 功能：随机一个浮点数,区间[fMin, fMax)
    public static float RandFloat(float fMin, float fMax)
    {
        return 0.0f;
    }
    // 功能：随机一个双精浮点数,区间[min, max)
    public static double RandDouble(double min, double max)
    {
        return 0.0;
    }
    // 功能：取utc时间
    public static Int64 time()
    {
        return 0;
    }
    // 功能：取时间描述的字符串，如 xxxx-xx-xx xx:xx:xx
    // 说明：将当前utc时间转换成字符串，格式如：xxxx-xx-xx xx:xx:xx
    public static StringA time_desc()
    {
        return "";
    }
    // 功能：将当前时间的六个分量拷贝到一个INT数组
    //       a[0] = year; a[1] = month; a[2] = day; a[3] = hour; a[4] = minute; a[5] = second;
    public static void TimeToArray(List<int> array)
    {
    }
    // 功能：返回class实例ins的引用计数,调试用的接口
    public static int GetRef<_Ty>(_Ty ins)
    {
        return 0;
    }
    // 功能：返回class实例ins的弱引用计数,调试用的接口
    public static int GetWeakRef<_Ty>(_Ty ins)
    {
        return 0;
    }
    // 功能：开方
    public static double sqrt(double fValue)
    {
        return 0;
    }
    public static float sqrtf(float fValue)
    {
        return 0;
    }
    public static double sin(double fValue)
    {
        return 0;
    }
    public static float sinf(float fValue)
    {
        return 0;
    }
    public static double cos(double fValue)
    {
        return 0;
    }
    public static float cosf(float fValue)
    {
        return 0;
    }
    public static double tan(double fValue)
    {
        return 0;
    }
    public static float tanf(float fValue)
    {
        return 0;
    }
    public static double atan(double fValue)
    {
        return 0;
    }
    public static float atanf(float fValue)
    {
        return 0;
    }
    public static double atan2(double y, double x)
    {
        return 0;
    }
    public static float atan2f(float y, float x)
    {
        return 0;
    }
    public static double asin(double fValue)
    {
        return 0;
    }
    public static float asinf(float fValue)
    {
        return 0;
    }
    public static double acos(double fValue)
    {
        return 0.0f;
    }
    public static float acosf(float fValue)
    {
        return 0.0f;
    }

    // 功能：读取一个文本格式的XML到脚本对象
    // 参数：pXmlRoot - 这个必须是class, 并在成员变量声明中添加[XmlElementAttribute]字段
    //       szRootName - XML表根节点的名字
    //       szFileData - XML文件数据
    public static bool ReadXml<_Ty>(_Ty pXmlRoot, StringA szRootName, StringA szFileData)
    {
        return true;
    }
    // 功能：将脚本对象转换成文本格式的XML
    // 参数：pXmlRoot - 这个必须是class, 并在成员变量声明中添加[XmlElementAttribute]字段
    //       szRootName - XML表根节点的名字
    //       szFileData - XML输出数据
    public static bool WriteXml<_Ty>(_Ty pXmlRoot,StringA szRootName, StringA szFileData)
    {
        return true;
    }
    // 功能：从一个二进制流中读取XML脚本对象
    // 说明：这个目前写的是测试指令，测试发现这个速度比ReadXml要慢，而且文件还比XML要大，所以这个二进制流的接口是没有意义的，请不要使用
    public static bool ReadBin<_Ty>(_Ty pXmlRoot, StringA szFileData)
    {
        return true;
    }
    // 功能：将XML脚本对象写入到二进制流
    // 说明：这个目前写的是测试指令，测试发现这个速度比ReadXml要慢，而且文件还比XML要大，所以这个二进制流的接口是没有意义的，请不要使用
    public static bool XmlToBin<_Ty>(_Ty pXmlRoot, StringA szFileData)
    {
        return true;
    }
    // 功能：读取一个Json配置
    // 说明：
    public static bool ReadJson<_Ty>(_Ty pJsonRoot, StringA szJson)
    {
        return true;
    }
    // 功能：将Json对象转换成Json字符串
    public static StringA  WriteJson<_Ty>(_Ty pJsonRoot)
    {
        return "";
    }
    // 功能：将Json对象转换成Json字符串
    // 参数：nPrepareJsonSize - 要转换的Json字符串预分配的大小，指定这个可以提升性能
    // 没有指定的话，默认按1K预分配
    public static StringA WriteJson<_Ty>(_Ty pJsonRoot, int nPrepareJsonSize)
    {
        return "";
    }

    // 功能：打印到命令行
    public static void print(StringA szFormat, params System.Object[] args)
    {

    }

    // 功能：得到对象的数据地址
    // 说明：如果对象是List，就返回数组的首地址
    //       如果对象是字符串，就返回字符串的首地址
    // 使用这个方法，可以将List数组快速与其他平台的接口做数据交互
    public static IntPtr GetDataPtr<_Ty>(_Ty obj)
    {
        return new IntPtr();
    }
    // 功能：得到对象自已的地址
    // 说明：这个接口一般用于调试，没有实际用途
    //       
    public static IntPtr GetObjPtr<_Ty>(_Ty obj)
    {
        return new IntPtr();
    }

    // 功能：系统广播调用
    public static void  Broadcast(StringA szGroupName, params System.Object[] args)
    {

    }
    // 功能：获取需要广播的类列表
    public static void  GetBroadstList(ref List<FCType> ClassList, StringA szFuncName)
    {

    }
    // 功能：动态调用任意一个函数
    // 参数：obj - 类变量指针, 如果为nill表示调用全局函数
    //       szFuncName - 函数名字(该名字必须使用export关键标记导出的)
    //       args - 函数参数
    // System.Call(Obj, funcName, arg0, arg1, ...)
    public static void  Call<_Ty>(_Ty obj, StringA szFuncName, params System.Object[] args)
    {

    }
};

// 只是为了兼容UnityEngine.Events.UnityAction
class UnityAction<T>
{
};

//自动导出标签 [export]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Method)]
public class exportAttribute : System.Attribute
{
    // 自动导出标签[export]
    public exportAttribute()
    {
    }
}

//内联标记 [inline]
[AttributeUsage(AttributeTargets.Method)]
public class inlineAttribute : System.Attribute
{
    // 内联标记[inline]
    public inlineAttribute()
    {
    }
}

//Json对象标签 [json]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct)]
public class jsonAttribute : System.Attribute
{
    // Json对象[json]
    public jsonAttribute()
    {
    }
}

//Json对象标签 [PBAttrib]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Field)]
public class PBAttribAttribute : System.Attribute
{
    // Json对象[json]
    public PBAttribAttribute(string Meta)
    {
    }
}

//广播标记 [Broadcast]
[AttributeUsage(AttributeTargets.Method)]
public class BroadcastAttribute : System.Attribute
{
    // 内联标记[inline]
    public BroadcastAttribute()
    {
    }
}

struct IntPtr { }