using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface CompareFunc<_Ty>
{
    // 功能：排序函数
    // 参数：p1, p2是要比较的对象
    // 返回值：-1表示p1 < p2; 0是相等;1表示p1 > p2         
    int Compare(_Ty p1, _Ty p2);
};

public class List<_Ty>
{
    // 功能：取数组的长度
    _Ty []m_pData;

    public int Length
    {
        get { return 0; }
    }
    public int GetLength()
    {
        return 0;
    }
    // 功能：指定数组的长度
    // 说明：首次分配时自动置零
    public void resize(int nSize)
    {

    }
    // 功能：预分配
    public void reserve(int nSize)
    {

    }
    // 功能：将所有的节点都填充成指定的值
    public void Fill(_Ty value)
    {

    }
    // 功能：反序
    public void Inserve()
    {

    }
    // 功能：插入一个节点
    public void Insert(int nIndex, _Ty value)
    {

    }
    // 功能：移除值相等的节点
    public void Remove(_Ty value)
    {

    }
    // 功能：删除指定下标的节点
    public void RemoveAt(int nIndex)
    {

    }
    // 功能：删除指定下标开始，指定数量的节点
    public void RemoveAt(int nIndex, int nCount)
    {

    }
    // 功能：删除所有的节点
    public void RemoveAll()
    {

    }
    // 功能：添加一个变量
    public void push_back(_Ty value)
    {

    }
    // 功能：数组连接（添加一个数组）
    public void push_back(List<_Ty> other)
    {

    }

    // 功能：默认的排序(升序)
    public void Sort()
    {

    }

    public delegate int LPCompare(_Ty a, _Ty b);
    // 功能：指定排序实例与排序函数
    // 参数：pIns - 自定义类实例
    //       func_name : _TyOther类的成员函数, 必须是 CompareFunc::Compare声明样式
    public void Sort(LPCompare comparer)
    {

    }
    public void Sort(System.Object obj, LPCompare comparer)
    {
    }

    // 功能：使用冒泡排序(升序)
    public void BubbleSort()
    {
    }
    public void BubbleSort(LPCompare compare)
    {
    }
    public void BubbleSort(System.Object obj, LPCompare comparer)
    {
    }

    // 功能：下标引用
    // 参数：nIndex - 可以常数或变量
    public _Ty this[int nIndex]
    {
        get { return m_pData[nIndex]; }
        set
        {
            m_pData[nIndex] = value;
        }
    }
	
	// 功能：串连两个数组
	public void operator += (List<_Ty> other){}
	
	// 功能：克隆一个新的数组 
	public List<_Ty> Clone(){return null;}
};