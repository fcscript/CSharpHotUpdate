﻿using System;
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
    public static Int64 GetTickCount()
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

    // 功能：读取一个XML
    // 参数：pXmlRoot - 这个必须是class, 并在成员变量声明中添加[XmlElementAttribute]字段
    //       szRootName - XML表根节点的名字
    //       szFileData - XML文件数据
    public bool ReadXml<_Ty>(_Ty pXmlRoot, StringA szRootName, StringA szFileData)
    {
        return true;
    }
    // 功能：将XML写入到内存
    // 参数：pXmlRoot - 这个必须是class, 并在成员变量声明中添加[XmlElementAttribute]字段
    //       szRootName - XML表根节点的名字
    //       szFileData - XML输出数据
    public bool WriteXml<_Ty>(_Ty pXmlRoot,StringA szRootName, StringA szFileData)
    {
        return true;
    }
    // 功能：从一个二进制流中读取XML配置
    // 说明：这个目前写的是测试指令，测试发现这个速度比ReadXml要慢，而且文件还比XML要大，所以这个二进制流的接口是没有意义的，请不要使用
    public bool ReadBin<_Ty>(_Ty pXmlRoot, StringA szFileData)
    {
        return true;
    }
    // 功能：将XML配置按二进制流的格式写入到文件
    // 说明：这个目前写的是测试指令，测试发现这个速度比ReadXml要慢，而且文件还比XML要大，所以这个二进制流的接口是没有意义的，请不要使用
    public bool XmlToBin<_Ty>(_Ty pXmlRoot, StringA szFileData)
    {
        return true;
    }
};
