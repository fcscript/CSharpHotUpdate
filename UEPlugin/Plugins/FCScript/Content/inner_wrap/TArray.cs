using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TArray<_Ty>
{
	public TArray(){}
    // 功能：取数组的长度
    public int Length
    {
        get { return 0; }
    }
	public int GetNumb(){return 0;}
	public void SetNumb(int Num){return 0;}
	public _Ty GetAt(int nIndex){ return null; }
	public void SetAt(int nIndex, _Ty Value){}
	public void Add(_Ty Value){}
	public void Remove(int nIndex){}
	public void Clear(){}
	
	public List<_Ty> ToList(){return null;}
	public void SetList(List<_Ty> data){}
	
	public int size(){return 0;}
	public void resize(int Num){}
	public void push_back(_Ty Value){}

    // 功能：下标引用
    // 参数：nIndex - 可以常数或变量
    public _Ty this[int nIndex]
    {
        get { return null; }
        set { }
    }
};