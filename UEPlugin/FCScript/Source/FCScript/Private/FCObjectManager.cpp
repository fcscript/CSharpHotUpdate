#include "FCObjectManager.h"
#include "FCDynamicOverrideFunc.h"
#include "FCCallScriptFunc.h"
#include "../../FCLib/include/fc_api.h"

extern uint8 GRegisterNative(int32 NativeBytecodeIndex, const FNativeFuncPtr& Func);

FFCObjectdManager::FFCObjectdManager():m_pCurrentBindClass(nullptr)
{
    // 先注册一下全局Native函数
    GRegisterNative(EX_CallFCBeginPlay, FCDynamicOverrideBeginBeginPlay);
    GRegisterNative(EX_CallFCOverride, FCDynamicOverrideNative);
    GRegisterNative(EX_CallFCDelegate, FCDynamicOverrideDelegate);
}

FFCObjectdManager::~FFCObjectdManager()
{

}

static FFCObjectdManager  ObjectMgrIns;
FFCObjectdManager  *FFCObjectdManager::GetSingleIns()
{
	return &ObjectMgrIns;
}

void  FFCObjectdManager::Clear()
{
	m_DynamicBindClassInfo.clear();
	m_pCurrentBindClass = nullptr;
	m_ScriptsClassName = nullptr;
	m_BindObjects.clear();
	m_NamePtrMap.clear();

    m_OverrideFunctionScriptInsMap.clear();
    m_OverrideObjectFunctionMap.clear();
    m_OverrideRefMap.clear();

	//-------------------------------	
	ClearAllDynamicFunction();
}

void  FFCObjectdManager::BindScript(const class UObjectBaseUtility *Object, UClass *Class, const FString &ScriptClassName)
{
	const char *Name = TCHAR_TO_UTF8(*ScriptClassName);
	return BindToScript(Object, Class, Name);
}

void  FFCObjectdManager::BindToScript(const class UObjectBaseUtility* Object, UClass* Class, const char* ScriptClassName)
{
	ScriptClassName = NameToName(ScriptClassName);
	FBindObjectInfo &Info = m_BindObjects[Object];
	Info.Set(Object, Object->GetLinkerIndex(), ScriptClassName);
	RegisterReceiveBeginPlayFunction((UObject*)Object, Class);
}

void  FFCObjectdManager::CallBindScript(UObject *InObject, const char *ScriptClassName)
{
    ScriptClassName = NameToName(ScriptClassName);
    FBindObjectInfo &Info = m_BindObjects[InObject];
    Info.Set(InObject, InObject->GetLinkerIndex(), ScriptClassName);
    Info.m_ScriptIns = FCDynamicBindScript(InObject);
    FCScriptContext  *ScriptContext = GetScriptContext();
    CallAnyScriptFunc(ScriptContext, Info.m_ScriptIns, "ReceiveBeginPlay");
}

void  FFCObjectdManager::DynamicBind(const class UObjectBaseUtility *Object, UClass *Class)
{
	if(Class == m_pCurrentBindClass && m_ScriptsClassName)
	{
		BindToScript(Object, Class, m_ScriptsClassName);
	}
}

void  FFCObjectdManager::NotifyDeleteUObject(const class UObjectBase* Object, int32 Index)
{
	CBindObjectInfoMap::iterator itBind = m_BindObjects.find(Object);
	if(itBind != m_BindObjects.end())
	{
		FCScriptContext  *ScriptContext = GetScriptContext();
		int64  VM = ScriptContext->m_ScriptVM;
		FBindObjectInfo  &BindInfo = itBind->second;

		CallAnyScriptFunc(GetScriptContext(), BindInfo.m_ScriptIns, "ReceiveBeginDestroy");

		fc_relese_ins(VM, BindInfo.m_ScriptIns);
		BindInfo.m_ScriptIns = 0;
		m_BindObjects.erase(itBind);

        // 释放
        RemoveOverrideRefByObject(Object);
	}
	ClearObjectDelegate(Object);
}

void  FFCObjectdManager::PushDynamicBindClass(UClass* Class, const char *ScriptClassName)
{
	ScriptClassName = NameToName(ScriptClassName);
	FDynmicBindClassInfo  Info = {Class, ScriptClassName};
	m_DynamicBindClassInfo.push_back(Info);
	m_pCurrentBindClass = Class;
	m_ScriptsClassName = ScriptClassName;
}

void  FFCObjectdManager::PopDynamicBindClass()
{
	m_DynamicBindClassInfo.pop_back();
	if(m_DynamicBindClassInfo.size() > 0)
	{
		const FDynmicBindClassInfo  &Info = m_DynamicBindClassInfo.back();
		m_pCurrentBindClass = Info.Class;
		m_ScriptsClassName = Info.ScriptClassName;
	}
	else
	{
		m_pCurrentBindClass = nullptr;
		m_ScriptsClassName = nullptr;
	}
}

bool  FFCObjectdManager::IsDynamicBindClass(UClass *Class)
{
	return m_pCurrentBindClass == Class;
}

//----------------------------------------------------------------------

FCDynamicFunction  *FFCObjectdManager::RegisterReceiveBeginPlayFunction(UObject *InObject, UClass* Class)
{
	FCDynamicClassDesc *ClassDesc = GetScriptContext()->RegisterUStruct(InObject->GetClass());
	if(!ClassDesc)
	{
		return nullptr;
	}
	FCDynamicFunction *ClassFunc = ClassDesc->FindFunctionByName("ReceiveBeginPlay");
	if(ClassFunc)
	{
		FNativeFuncPtr OleNativeFuncPtr = ClassFunc->Function->GetNativeFunc();
		ClassFunc->Function->SetNativeFunc(ClassFunc->OleNativeFuncPtr);
		FCDynamicFunction *DynamicFunc = ToOverrideFunction(InObject, ClassFunc->Function, FCDynamicOverrideBeginBeginPlay, EX_CallFCBeginPlay);
		ClassFunc->Function->SetNativeFunc(OleNativeFuncPtr);
		return DynamicFunc;
	}
	else
	{
		// 该类没有ReceiveBeginPlay，直接调用脚本中的
		int64 ScriptIns = FCDynamicBindScript(InObject);
		CallAnyScriptFunc(GetScriptContext(), ScriptIns, "ReceiveBeginPlay");
	}
	return nullptr;
}

FCDynamicFunction  *FFCObjectdManager::RegisterOverrideFunc(UObject *InObject, fc_intptr InScriptPtr, const char *InFuncName)
{
	FCDynamicClassDesc *ClassDesc = GetScriptContext()->RegisterUStruct(InObject->GetClass());
	if(!ClassDesc)
	{
		return nullptr;
	}
	FCDynamicFunction *ClassFunc = ClassDesc->FindFunctionByName(InFuncName);
	if(ClassFunc)
	{
        UFunction  *Function = ClassFunc->Function;
		FCDynamicFunction *DynamicFunc = ToOverrideFunction(InObject, Function, FCDynamicOverrideNative, EX_CallFCOverride);

        FScriptOverrideKey  Key(InObject, Function);
        COverrideFunction2ScriptInsMap::iterator itOverride = m_OverrideFunctionScriptInsMap.find(Key);
        if(itOverride == m_OverrideFunctionScriptInsMap.end())
        {
            m_OverrideFunctionScriptInsMap[Key] = InScriptPtr;
            CScriptFunctionList &FuncList = m_OverrideObjectFunctionMap[InObject];
            FuncList.push_back(Function);
            ++(m_OverrideRefMap[Function]);
        }
		return DynamicFunc;
	}
	return nullptr;
}

FCDynamicFunction  *FFCObjectdManager::ToOverrideFunction(UObject *InObject, UFunction *InFunction, FNativeFuncPtr InFuncPtr, int InNativeBytecodeIndex)
{
	COverrideFunctionMap::iterator itFunc = m_OverrideFunctionMap.find(InFunction);
	if(itFunc != m_OverrideFunctionMap.end())
	{
		return itFunc->second;
	}
	FCDynamicFunction  *DynamicFunc = new FCDynamicFunction();
	DynamicFunc->InitParam(InFunction);
	m_OverrideFunctionMap[InFunction] = DynamicFunc;
	InFunction->SetNativeFunc(InFuncPtr);

    if(InFunction->Script.Num() < 1)
    {
        InFunction->Script.Add(InNativeBytecodeIndex);
        InFunction->Script.Add(EX_Return);
        InFunction->Script.Add(EX_Nothing);
        InFunction->Script.Add(EX_Return);
        InFunction->Script.Add(EX_Nothing);
    }
	return DynamicFunc;
}

FCDynamicFunction  *FFCObjectdManager::FindOverrideFunction(UObject *InObject, UFunction *InFunction)
{
	COverrideFunctionMap::iterator itFunc = m_OverrideFunctionMap.find(InFunction);
	if(itFunc != m_OverrideFunctionMap.end())
	{
		return itFunc->second;
	}
	return NULL;
}

int64 FFCObjectdManager::FindOverrideScriptIns(UObject *InObject, UFunction *InFunction)
{
    FScriptOverrideKey  Key(InObject, InFunction);
    COverrideFunction2ScriptInsMap::iterator itOverride = m_OverrideFunctionScriptInsMap.find(Key);
    if(itOverride != m_OverrideFunctionScriptInsMap.end())
    {
        return itOverride->second;
    }
    return 0;
}

FCDynamicDelegateList  *FFCObjectdManager::FindDelegateFunction(UObject *InObject)
{
	CObjectDelegateMap::iterator itFunc = m_ObjectDelegateMap.find(InObject);
	if(itFunc != m_ObjectDelegateMap.end())
	{
		return &(itFunc->second);
	}
	return nullptr;
}

void  FFCObjectdManager::RegisterScriptDelegate(UObject *InObject, const FCDynamicProperty* InDynamicProperty, fc_intptr InScriptThisPtr, int InClassNameID, int InFuncNameID)
{
	if(!InObject || !InDynamicProperty)
	{
		return ;
	}

	UFunction* Func = InDynamicProperty->GetSignatureFunction();
	if(!Func)
	{
		return ;
	}

	FCDynamicFunction  *DynamicFunc = this->ToOverrideFunction(InObject, Func, FCDynamicOverrideDelegate, EX_CallFCDelegate);

	FCDynamicDelegateList  &DelegateList = m_ObjectDelegateMap[InObject];
	FCDelegateInfo  Info(DynamicFunc, InDynamicProperty, InScriptThisPtr, InClassNameID, InFuncNameID);
	if(!DelegateList.AddScriptDelegate(Info))
	{
		return ;
	}
	int Ref = m_DelegateRefMap[Func];
	m_DelegateRefMap[Func] = Ref + 1;
	if(0 == Ref)
	{
		// 添加引用吧
		AddDelegateToClass(DynamicFunc, InObject->GetClass());
	}
	
	uint8* ObjAddr = (uint8 *)InObject;
	uint8* ValueAddr = ObjAddr + InDynamicProperty->Offset_Internal;
	if(InDynamicProperty->Type == FCPropertyType::FCPROPERTY_MulticastDelegateProperty)
	{
		FMulticastDelegateProperty* DelegateProperty = (FMulticastDelegateProperty*)InDynamicProperty->Property;		
		FScriptDelegate DynamicDelegate;
		DynamicDelegate.BindUFunction(InObject, Func->GetFName());

		FMulticastScriptDelegate& MulticastDelegate = (*(FMulticastScriptDelegate*)ValueAddr);
		MulticastDelegate.AddUnique(MoveTemp(DynamicDelegate));
	}
	else if(FCPROPERTY_DelegateProperty == InDynamicProperty->Type)
	{
		FDelegateProperty* DelegateProperty = (FDelegateProperty*)InDynamicProperty->Property;
		FScriptDelegate& ScriptDelegate = (*(FScriptDelegate*)ValueAddr);
		ScriptDelegate.BindUFunction(InObject, Func->GetFName());
	}
}

void  FFCObjectdManager::RemoveScriptDelegate(UObject *InObject, const FCDynamicProperty* InDynamicProperty, fc_intptr InScriptThisPtr, int InClassNameID, int InFuncNameID)
{
	if(!InObject || !InDynamicProperty)
	{
		return ;
	}
	CObjectDelegateMap::iterator itDelegateList = m_ObjectDelegateMap.find(InObject);
	if(itDelegateList == m_ObjectDelegateMap.end())
	{
		return ;
	}	
	UFunction* Func = InDynamicProperty->GetSignatureFunction();
	if(!Func)
	{
		return ;
	}

	FCDynamicFunction  *DynamicFunc = this->FindOverrideFunction(InObject, Func);
	if(!DynamicFunc)
	{
		return ;
	}
	FCDynamicDelegateList  &DelegateList = itDelegateList->second;
	FCDelegateInfo  Info(DynamicFunc, InDynamicProperty, InScriptThisPtr, InClassNameID, InFuncNameID);
	if(!DelegateList.DelScriptDelegate(Info))
	{
		return ;
	}
	int Ref = m_DelegateRefMap[Func];
	m_DelegateRefMap[Func] = Ref - 1;
	if(0 == Ref)
	{
		// 释放引用吧
		RemoveDelegateFromClass(DynamicFunc, InObject->GetClass());
	}

	// 统计一下数量
	int nDelegateCount = 0;
	for(int i = 0; i<DelegateList.Delegates.size(); ++i)
	{
		if(DelegateList.Delegates[i].DynamicFunc == DynamicFunc)
		{
			++nDelegateCount;
		}
	}
	
	if(nDelegateCount == 0)
	{
		RemoveObjectDelegate(InObject, InDynamicProperty);
	}
}

void  FFCObjectdManager::ClearScriptDelegate(UObject* InObject, const FCDynamicProperty* InDynamicProperty)
{
	if (!InObject || !InDynamicProperty)
	{
		return;
	}
	CObjectDelegateMap::iterator itDelegateList = m_ObjectDelegateMap.find(InObject);
	if (itDelegateList == m_ObjectDelegateMap.end())
	{
		return;
	}
	UFunction* Func = InDynamicProperty->GetSignatureFunction();
	if (!Func)
	{
		return;
	}
	FCDynamicFunction* DynamicFunc = this->FindOverrideFunction(InObject, Func);
	if (!DynamicFunc)
	{
		return;
	}
	FCDynamicDelegateList& DelegateList = itDelegateList->second;
	for (int i = DelegateList.Delegates.size() - 1; i >= 0; --i)
	{
		const FCDelegateInfo& DelegateInfo = DelegateList.Delegates[i];
		if (DelegateInfo.DynamicFunc == DynamicFunc)
		{
			Func = DelegateInfo.DynamicFunc->Function;
			int Ref = m_DelegateRefMap[Func];
			m_DelegateRefMap[Func] = Ref - 1;
			if (0 == Ref)
			{
				// 释放引用吧
				RemoveDelegateFromClass(DynamicFunc, InObject->GetClass());
			}
			DelegateList.Delegates.erase(DelegateList.Delegates.begin() + i);
		}
	}
	RemoveDelegateFromClass(DynamicFunc, InObject->GetClass());
	RemoveObjectDelegate(InObject, InDynamicProperty);
}

void  FFCObjectdManager::ClearObjectDelegate(const class UObjectBase *Object)
{
	CObjectDelegateMap::iterator itDelegateList = m_ObjectDelegateMap.find((UObject*)Object);
	if(itDelegateList != m_ObjectDelegateMap.end())
	{
		UObject *InObject = (UObject *)Object;
		UClass  *Class = InObject->GetClass();
		FCDynamicDelegateList  &DelegateList = itDelegateList->second;
		for(int i = DelegateList.Delegates.size() - 1; i>=0; --i)
		{
			FCDelegateInfo &Info = DelegateList.Delegates[i];
			UFunction* Func = Info.DynamicFunc->Function;
			int Ref = m_DelegateRefMap[Func] - 1;
			m_DelegateRefMap[Func] = Ref;			
			RemoveObjectDelegate(InObject, Info.DynamicProperty);

			if(Ref <= 0)
			{
				// 没有地方引用了, 需要还原NativeFuncPtr
				Func->SetNativeFunc(Info.DynamicFunc->OleNativeFuncPtr);

				RemoveDelegateFromClass(Info.DynamicFunc, Class);
				m_OverrideFunctionMap.erase(Func);
				delete Info.DynamicFunc;
			}
		}
		m_ObjectDelegateMap.erase(itDelegateList);
	}
}

void  FFCObjectdManager::AddDelegateToClass(FCDynamicFunction *InDynamicFunc, UClass *InClass)
{
	UFunction  *Function = InDynamicFunc->Function;
	if(InDynamicFunc->OleNativeFuncPtr != FCDynamicOverrideDelegate)
	{
		Function->SetNativeFunc(FCDynamicOverrideDelegate);
	}
    if(Function->Script.Num() < 1)
    {
        Function->Script.Add(EX_CallFCDelegate);
        Function->Script.Add(EX_Return);
        Function->Script.Add(EX_Nothing);
    }
			
	InClass->AddFunctionToFunctionMap(Function, Function->GetFName());
}

void  FFCObjectdManager::RemoveDelegateFromClass(FCDynamicFunction *InDynamicFunc, UClass *InClass)
{
	UFunction  *Function = InDynamicFunc->Function;
	Function->SetNativeFunc(InDynamicFunc->OleNativeFuncPtr);
	InClass->RemoveFunctionFromFunctionMap(Function);
}

void  FFCObjectdManager::RemoveObjectDelegate(UObject *InObject, const FCDynamicProperty* InDynamicProperty)
{	
	uint8* ObjAddr = (uint8 *)InObject;
	uint8* ValueAddr = ObjAddr + InDynamicProperty->Offset_Internal;
	if(FCPropertyType::FCPROPERTY_MulticastDelegateProperty == InDynamicProperty->Type)
	{		
		FMulticastScriptDelegate& MulticastDelegate = (*(FMulticastScriptDelegate*)ValueAddr);
		MulticastDelegate.Clear();
	}
	else if(FCPROPERTY_DelegateProperty == InDynamicProperty->Type)
	{
		FScriptDelegate& ScriptDelegate = (*(FScriptDelegate*)ValueAddr);
		ScriptDelegate.Clear();
	}
}

void  FFCObjectdManager::RemoveOverrideRefByObject(const class UObjectBase *Object)
{
    COverrideObjectFunctionMap::iterator itFuncList = m_OverrideObjectFunctionMap.find((UObjectBase*)Object);
    if (itFuncList != m_OverrideObjectFunctionMap.end())
    {
        const CScriptFunctionList &FuncList = itFuncList->second;
        FScriptOverrideKey Key;
        Key.Object = Object;
        for (int i = 0; i < FuncList.size(); ++i)
        {
            Key.Function = FuncList[i];
            m_OverrideFunctionScriptInsMap.erase(Key);
            CFunctionRefMap::iterator itRef = m_OverrideRefMap.find(Key.Function);
            if (itRef != m_OverrideRefMap.end())
            {
                --(itRef->second);
                if (itRef->second <= 0)
                {
                    m_OverrideRefMap.erase(itRef);
                }
            }
        }
        m_OverrideObjectFunctionMap.erase(itFuncList);
    }
}

void  FFCObjectdManager::ClearAllDynamicFunction()
{
	// 还原函数指针
	for (COverrideFunctionMap::iterator itOverride = m_OverrideFunctionMap.begin(); itOverride != m_OverrideFunctionMap.end(); ++itOverride)
	{
		itOverride->second->Function->SetNativeFunc(itOverride->second->OleNativeFuncPtr);
	}
	ReleasePtrMap(m_OverrideFunctionMap);

	m_ObjectDelegateMap.clear();
	m_DelegateRefMap.clear();
}

void  FFCObjectdManager::CheckGC()
{
	// Debug 测试
	int InvalidCount = 0;
	for(COverrideFunctionMap::iterator itFunc = m_OverrideFunctionMap.begin(); itFunc != m_OverrideFunctionMap.end(); ++itFunc)
	{
		if(!IsValid(itFunc->first))
		{
			++InvalidCount;
		}
	}
}
//----------------------------------------------------------------------
