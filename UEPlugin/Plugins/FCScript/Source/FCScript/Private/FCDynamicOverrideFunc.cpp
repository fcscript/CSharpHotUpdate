#include "FCDynamicOverrideFunc.h"
#include "FCDynamicClassDesc.h"
#include "FCObjectManager.h"
#include "FCCallScriptFunc.h"
#include "Engine/World.h"
#include "FCGetObj.h"

#include "../../FCLib/include/fc_api.h"


UFunction  *FirstNative(UObject* Context, FFrame& TheStack, bool &bUnpackParams)
{
	UFunction* Func = TheStack.Node;
	if (TheStack.CurrentNativeFunction)
	{
		if (Func != TheStack.CurrentNativeFunction)
		{
			Func = TheStack.CurrentNativeFunction;
#if UE_BUILD_SHIPPING || UE_BUILD_TEST
			FMemory::Memcpy(&FuncDesc, &Stack.CurrentNativeFunction->Script[1], sizeof(FuncDesc));
#endif
			bUnpackParams = true;
		}
		else
		{
			if (Func->GetNativeFunc() == (FNativeFuncPtr)&FCDynamicOverrideNative)
			{
				TheStack.SkipCode(1);      // skip EX_CallLua only when called from native func
			}
		}
	}
	return Func;
}


void FCDynamicOverrideNative(UObject* Context, FFrame& TheStack, RESULT_DECL)
{
	// 参照代码 FLuaInvoker::execCallLua
	// 第一步，根据Context查找，该Class没有关联注册

	// 查找FCScriptContext

	// 从FCScriptContext中查找到注册的Context对象, 如果有就调用对应的事件

	// 如果没有找到，就对所有的FCScriptContext广播
	UObject  *Object = TheStack.Object;

	bool bUnpackParams = false;
	UFunction* Func = FirstNative(Context, TheStack, bUnpackParams);
	FCDynamicOverrideFunction *DynamicFunction = FFCObjectdManager::GetSingleIns()->ToOverrideFunction(Object, Func, FCDynamicOverrideNative, EX_CallFCOverride);

	FCScriptContext  *ScriptContext = GetScriptContext();
	if(DynamicFunction)
	{
		if(Object)
		{
            int64 ScriptIns = FFCObjectdManager::GetSingleIns()->FindOverrideScriptIns(Object, Func);
			if (ScriptIns)
			{
				if(FCCallScriptFunc(ScriptContext, Object, ScriptIns, DynamicFunction->Name.c_str(), DynamicFunction, TheStack))
                {
                    return ;
                }
			}
		}
		else
		{
			if(FCCallScriptFunc(ScriptContext, Object, 0, DynamicFunction->Name.c_str(), DynamicFunction, TheStack))
            {
                return ;
            }
		}
		// 再调用基类的
		if(DynamicFunction->OleNativeFuncPtr != FCDynamicOverrideNative)
		{
			DynamicFunction->OleNativeFuncPtr(Context, TheStack, RESULT_PARAM);
		}
	}
}

void     FCDynamicOverrideCallback(fc_intptr L, int nClassName, fc_pcstr pcsFuncName, fc_intptr UserData1, fc_intptr UserData2)
{
	UObject  *Object = (UObject *)UserData1;
	fc_intptr ScriptIns = UserData2;
	FFCObjectdManager::GetSingleIns()->RegisterOverrideFunc(Object, ScriptIns, pcsFuncName);
}

void FCDynamicOverrideBeginBeginPlay(UObject* Context, FFrame& TheStack, RESULT_DECL)
{
	// 先检查一下有没有绑定的对象
    UObject *Object = TheStack.Object;
	FBindObjectInfo  *BindInfo = FFCObjectdManager::GetSingleIns()->FindBindObjectInfo(Object);
	// 如果有的话，就检测一下没有创建脚本, 尝试创建脚本
		//	ENetMode  NetMode = World->GetNetMode();
		//	if(NM_DedicatedServer == NetMode)

	FCScriptContext  *ScriptContext = GetScriptContext();
	// 尝试调用函数
    FNativeFuncPtr OllNativeFuncPtr = FCDynamicOverrideBeginBeginPlay;
	if(ScriptContext && BindInfo)
    {
		bool bUnpackParams = false;
		UFunction* Func = FirstNative(Context, TheStack, bUnpackParams);
		FCDynamicOverrideFunction *DynamicFunction = FFCObjectdManager::GetSingleIns()->ToOverrideFunction(Object, Func, FCDynamicOverrideBeginBeginPlay, EX_CallFCBeginPlay);
		if(DynamicFunction)
        {
            OllNativeFuncPtr = DynamicFunction->OleNativeFuncPtr;
            int64 ScriptIns = FCDynamicBindScript(Object);
			if(ScriptIns)
			{
				CallAnyScriptFunc(ScriptContext, BindInfo->m_ScriptIns, DynamicFunction->Name.c_str());
                return ;
			}
		}
    }
    // 调用基类的
    if (OllNativeFuncPtr != FCDynamicOverrideBeginBeginPlay)
    {
        OllNativeFuncPtr(Context, TheStack, RESULT_PARAM);
    }
}

int64 FCDynamicBindScript(UObject* InObject)
{
	FBindObjectInfo  *BindInfo = FFCObjectdManager::GetSingleIns()->FindBindObjectInfo(InObject);
	if(!BindInfo)
	{
		return 0;
	}
	FCScriptContext  *ScriptContext = GetScriptContext();
	if(!BindInfo->m_ScriptIns)
	{
		BindInfo->m_ScriptIns = fc_instance(ScriptContext->m_ScriptVM, BindInfo->m_ScriptName);

		// 设置一些变量吧
		if(BindInfo->m_ScriptIns)
		{
			int64 ObjID = FCGetObj::GetIns()->PushUObject(InObject);
			int64 VM = ScriptContext->m_ScriptVM;
			fc_intptr  ObjPtr = fc_get_class_value(VM, BindInfo->m_ScriptIns, "Self");
			if(!ObjPtr)
			{
				ObjPtr = fc_get_class_value(VM, BindInfo->m_ScriptIns, "gameObject");
			}
			if(ObjPtr)
			{
				fc_set_value_wrap_objptr(VM, ObjPtr, (fc_intptr)ObjID);
			}
			else
			{
				UE_LOG(LogFCScript, Warning, TEXT("not find script member: Self or gameObject,  script name:%s"), UTF8_TO_TCHAR(BindInfo->m_ScriptName));
			}			
			// 请求注册一下
			fc_require_override(VM, FCDynamicOverrideCallback, BindInfo->m_ScriptName, (fc_intptr)InObject, BindInfo->m_ScriptIns);
		}
		else
		{
			UE_LOG(LogFCScript, Warning, TEXT("failed to instance script:%s"), UTF8_TO_TCHAR(BindInfo->m_ScriptName));
		}
	}
	return BindInfo->m_ScriptIns;
}

void FCDynamicOverrideDelegate(UObject* Context, FFrame& TheStack, RESULT_DECL)
{
	// 先检查一下有没有绑定的对象
	UObject* Object = TheStack.Object;
	// 再检查一下，没有当前函数
	FCScriptContext  *ScriptContext = GetScriptContext();
	if(ScriptContext)
	{
		UClass* Class = Object->GetClass();
		bool bUnpackParams = false;
		UFunction* Func = FirstNative(Context, TheStack, bUnpackParams);
		FCDynamicDelegateList *DelegateList = FFCObjectdManager::GetSingleIns()->FindDelegateFunction(Object);
		if(!DelegateList)
		{
			return ;
		}
		int Count = DelegateList->Delegates.size();
		for( int i = 0; i<Count; ++i)
		{
			FCDelegateInfo &Info = DelegateList->Delegates[i];
			if(Info.DynamicFunc->Function == Func)
			{
				FCCallScriptDelegate(ScriptContext, Object, Info.ThisPtr, Info.ClassNameID, Info.FunctionNameID, Info.DynamicFunc, TheStack);
			}
		}
	}
}
