using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TMap<_TyKey, _TyValue>
{
	public TMap(){}
    // 功能：取数组的长度
    public int Length
    {
        get { return 0; }
    }
	public int GetNumb(){return 0;}
	public bool Find(_TyKey key, out _TyValue value){return false;}
	public void Add(_TyKey Key, _TyValue Value){}
	public void Remove(_TyKey Key){}
	public void Clear(){}
	
	public map<_TyKey, _TyValue> ToMap(){return null;}
	public void SetMap(map<_TyKey, _TyValue> data){}
	
	public int size(){return 0;}
	public void push_back(_TyKey Key, _TyValue Value){}

    // 功能：下标引用
    public _TyValue this[_TyKey key]
    {
        get { return null; }
        set { }
    }
};