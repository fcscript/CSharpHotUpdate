using System;
using System.Collections.Generic;
using System.Text;


//自动导出标签 [AutoWrap]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct)]
public class AutoWrapAttribute : System.Attribute
{
    // 自动导出标签[AutoWrap]
    public AutoWrapAttribute()
    {
    }
}

//部分导出标签 [PartWrap] - 标记这个后，只有标记这个函数的才会导出
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
public class PartWrapAttribute : System.Attribute
{
    // 部分导出标签[PartWrap]
    public PartWrapAttribute()
    {
    }
}

// 禁止导出标签[DontWrap]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
public class DontWrapAttribute : System.Attribute
{
    // 禁止导出标签[DontWrap]
    public DontWrapAttribute()
    {
    }
}

// 手动导出标签[ManualWrap]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
public class ManualWrapAttribute : System.Attribute
{
    // 手动导出标签[ManualWrap]
    public ManualWrapAttribute()
    {
    }
}