using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TSet<_TyValue>
{
	public TSet(){}
    // 功能：取数组的长度
    public int Length
    {
        get { return 0; }
    }
	public int GetNumb(){return 0;}
	public bool Contains(_TyValue Value){return false;}
	public void Add(_TyValue Value){}
	public void Remove(_TyValue Value){}
	public void Clear(){}
	
	public List<_TyValue> ToArray(){return null;}
	public void SetArray(List<_TyValue> data){}
	
	public int size(){return 0;}
	public void push_back(_TyValue Value){}
	
	public int GetMaxIndex() {return 0;}
	public int ToNextValidIndex(int Index){return Index;}
	public bool IsValidIndex(int Index){return false;}
	public _TyValue GetAt(int Index){return nill;}
	public void RemoveAt(int Index){}
};