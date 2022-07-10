using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TWeakObjectPtr<_Ty>
{
	public TWeakObjectPtr(){}
	
	public void Reset(){}
	public bool IsValid(){ return false; }
	public _Ty  Get(){ return null; }
	public void Set(_Ty Obj){}
};

public class TLazyObjectPtr<_Ty>
{
	public TLazyObjectPtr(){}
	
	public void Reset(){}
	public bool IsValid(){ return false; }
	public _Ty  Get(){ return null; }
	public void Set(_Ty Obj) {}
};