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
	public static UObject FindClass(StringA ClassName){return null;}
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
	// 功能：加载一个界面蓝图
	public static UUserWidget LoadUserWidget(UObject WorldContextObject, StringA ClassName, APlayerController OwningPlayer){return null;}
	// 功能：通过名字查找蓝图子对象
	public static auto_return GetChild(UObject Parent, StringA ChildName){return null;}
	// 功能：通过名字设置蓝图子对象
	public static void SetChild(UObject Parent, StringA ChildName, params System.Object value){return null;}	
	// 功能：调用一个UE对象的接口或蓝图接口
	public static auto_return CallFunction(UObject Object, StringA UEFuncName, params System.Object[] args){return null;}	
	// 功能：调用一个UE对象的原始接口
	public static auto_return SuperCall(UObject Object, StringA UEFuncName, params System.Object[] args){return null;}
	// 功能：得到一个UE对象绑定的脚本对象
	public static auto_return GetBindScript(UObject Object){return null;}
	// 功能：FString转StringA
	public static StringA FStringToA(FString InStr){return null;}
	// 功能：FString转StringW
	public static StringW FStringToW(FString InStr){return null;}	
	// 功能：StringA转FString
	public static FString AToFString(StringA InStr){return null;}
	// 功能：StringW转FString
	public static FString WToFString(StringW InStr){return null;}
};