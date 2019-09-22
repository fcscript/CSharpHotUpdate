using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// hash_map专用迭代器
public class iterator<_TyKey, _TyValue>
{
    public static iterator<_TyKey, _TyValue> operator ++(iterator<_TyKey, _TyValue> o )
    {
        // 自增
        return o;
    }
                               // 功能：测试迭代器是不是有效
    public bool IsValid()
    {
        return true;
    }
    // 功能：测试迭代器是不是有效
    // 说明：直接将对象作bool变量测试, 如果为true表示有效, 为false表示无效
    public static implicit operator bool(iterator<_TyKey, _TyValue> o)
    {
        return false;
    }
    // 功能：返回迭代器指向的节点的key
    public _TyKey key;
    // 功能：返回迭代器指向的节点的value
    public _TyValue value;
};

public class map<_TyKey, _TyValue>
{
    _TyValue m_value;

    iterator<_TyKey, _TyValue> m_it;

    // 取map的节点数量
    public int Length
    {
        get { return 0; }
    }
    public int GetLength()
    {
        return 0;
    }
    // 功能：下标引用, 如果节点不存在，就插入一个节点
    public _TyValue this[_TyKey key]
    {
        get
        {
            return m_value;
        }
        set
        {
            m_value = value;
        }
    }
    // 功能：从后面有序插入
    // 说明：如果已经存在key, 就更新变量
    //       如果key节点不存在，就从末尾追加, 你可以将map理解成一个list
    public void push_back(_TyKey key, _TyValue value)
    {

    }
    // 功能：从前面有序插入
    // 说明：如果已经存在key, 就更新变量
    //       如果key节点不存在，就从前端插入, 你可以将map理解成一个list
    public void push_front(_TyKey key, _TyValue value)
    {

    }
    // 功能：从指定位置后面插入
    public void insert_back(_TyKey where, _TyKey key, _TyValue value)
    {

    }
    // 功能：从指定位置前面插入
    public void insert_front(_TyKey where, _TyKey key, _TyValue value)
    {

    }
    // 功能：删除指定的节点
    public void remove(_TyKey key)
    {

    }
    // 功能：删除所有的节点
    public void RemoveAll()
    {

    }
    // 功能：返回首节点
    public iterator<_TyKey, _TyValue> begin()
    {
        return m_it;
    }
    // 功能：查找指定KEY值的节点
    public iterator<_TyKey, _TyValue> find(_TyKey key)
    {
        return m_it;
    }
    // 功能：测试是不是存在指定Key值的节点
    public bool ContainKey(_TyKey key)
    {
        return true;
    }
    // 功能：预分配hash数组，用于性能优化
    // 说明：如果你大致知道总的节点的数量，预先设置一下总的节点数量，有利于优化插入的性能
    public void Reserve(int nSize)
    {

    }
    // 功能：插入完成的优化
    // 参数：nMaxSize - hash数组的最大长度（内存限制，以免优化后内存大副增加)
    // 说明：调用这个接口可以优化hash数组，减少冲突，提升查询性能，对于节点数量巨大且查询非常频繁的map来说，是很有效的
    public void Optimize(int nMaxSize)
    {

    }
};