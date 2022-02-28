using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct FCType
{
    public new StringA ToString()
    {
        return "";
    }
    public static implicit operator FCType(System.Type type)
    {
        return new FCType();
    }
    // 因为C#不支持=的重载，无奈提供set接口
    // 说明：参数 type ，在脚本中其实会解释成FCType, 所以不要在意，这个只是为了让VS编译器做语法检查罢了
    public void set(System.Type type)
    {
    }
    //public static bool operator ==(FCType left, FCType other)
    //{
    //    return true;
    //}
    //public static bool operator !=(FCType left, FCType other)
    //{
    //    return false;
    //}
};

public class FCObject
{
    public static FCObject Create(FCType nType)  // 通过类型实例化一个对象
    {
        return default(FCObject);
    }
    public static FCObject Create(StringA szName) // 通过类名实例化一个对象
    {
        return default(FCObject);
    }
    public static bool IsNull(FCObject obj) // 检测对象是不是空指针
    {
        return false;
    }
    public new FCType GetType()
    {
        return default(FCType);
    }
    public new StringA ToString()
    {
        return  "";
    }
    //public static implicit operator FCObject(System.Object obj)
    //{
    //    return new FCObject();
    //}
    // 说明：这个在FCScript中，set并不是模板函数，这里只是为了兼容C#语法，所以不要使用<class>(obj)这样的调用
    // 调用时，不要添加<xxx>
    public void set<_Ty>(_Ty obj)
    {
    }
    // 功能：将FCObject强转成指定的类型
    // 说明：因为C#中不支持(_Ty)的写法，只好提供了一个模板函数, 兼容脚本中(_Ty)的强制转换
    public bool get<_Ty>(out _Ty obj)
    {
        obj = default(_Ty);
        return true;
    }

    public bool IsTypeOf(FCType type)
    {
        return false;
    }
    public bool IsTypeOf(StringA name)
    {
        return false;
    }

    //public static FCObject operator= (FCObject left, System.Object other)  // 重载赋值操作符，支持自定义class, 或外部平台导入的class
    //{
    //    return null;
    //}
    //public bool operator ==(T other);
    //public bool operator !=(T other);
    //public _Ty operator()(T);   // 强制转换接口 (CTestD)obj;    
};