using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TMapIterator<_TyKey, _TyValue>
{
	public TMapIterator(){}
	public bool IsValid()
	{
		return false;
	}
	public bool ToNext()
	{
		return false;
	}
	public void Reset()
	{
	}
	public _TyKey key
	{
		get{ return null; }
	}
	public _TyValue value
	{
		get{ return null; }
		set{ }
	}
};

public class TMap<_TyKey, _TyValue>
{
	public TMap(){}
    // 功能：取数组的长度
    public int Length
    {
        get { return 0; }
    }
	public int GetNumb(){return 0;}
	public bool TryGetValue(_TyKey key, out _TyValue value){return false;}
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
	
	// stl 
	public TMapIterator<_TyKey, _TyValue>  begin()
	{
		return null;
	}
	public TMapIterator<_TyKey, _TyValue> find(_TyKey key)
	{
		return null;
	}
};