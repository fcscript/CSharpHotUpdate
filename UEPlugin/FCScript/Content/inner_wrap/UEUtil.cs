using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UEUtil
{
	public static StringA GetName(UObject Object){return ""}
	public static UObject GetOuter(UObject Object){return null;}
	public static UObject GetClass(UObject Object){return null;}
	public static UWorld  GetWorld(UObject Object){return null;}
	public static UObject GetMainGameInstance(){ return null; }
	// 功能：将一个UObject添加到Root节点
	// 说明：由于UObject会自动GC,加到Root可以避免自动GC
	public static void  AddToRoot(UObject Object){}
	// 功能：将一个UObject从Root节点移除
	public static void  RemoveFromRoot(UObject Object){}
	// 功能：创建一个UObject, 并绑定一个脚本
	public static UObject NewObject(UObject Outer, StringA UEClassName, StringA ScriptClassName, StringA ObjectName){return null;}
	// 功能：创建一个UActor, 并绑定一个脚本
	public static UObject SpawActor(UObject Outer, StringA UEClassName, StringA ScriptClassName, StringA ObjectName){return null;}
	// 功能：调用一个UE对象的原始接口
	public static auto_return SuperCall(UObject Object, StringA UEFuncName, params System.Object[] args)){return null;}
	// 功能：得到一个UE对象绑定的脚本对象
	public static auto_return GetBindScript(UObject Object){return null;}
};