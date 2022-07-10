using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// FCDelegate
public class MulticastDelegateEvent
{
    public void AddListener(UnityAction call){}
    public void RemoveListener(UnityAction call){}
	public void ClearLinstener(){}
    public void Invoke(){}
	public delegate void UnityAction();
}