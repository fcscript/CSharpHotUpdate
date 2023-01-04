#include "FCUEUtilWrap.h"
#include "FCObjectManager.h"
#include "FCGetObj.h"
#include "FCRunTimeRegister.h"
#include "FCTemplateType.h"
#include "FCCore.h"

#include "Blueprint/WidgetBlueprintLibrary.h"


void FCUEUtilWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "UEUtil");
    fc_register_class_func(VM, nClassName, "GetName",GetName_wrap);
    fc_register_class_func(VM, nClassName, "GetOuter",GetOuter_wrap);
    fc_register_class_func(VM, nClassName, "GetClass", GetClass_wrap);
    fc_register_class_func(VM, nClassName, "FindClass", FindClass_wrap);
    fc_register_class_func(VM, nClassName, "GetWorld",GetWorld_wrap);
    fc_register_class_func(VM, nClassName, "GetMainGameInstance",GetMainGameInstance_wrap);
    fc_register_class_func(VM, nClassName, "AddToRoot",AddToRoot_wrap);
    fc_register_class_func(VM, nClassName, "RemoveFromRoot",RemoveFromRoot_wrap);
    fc_register_class_func(VM, nClassName, "NewObject",NewObject_wrap);
    fc_register_class_func(VM, nClassName, "SpawActor", SpawActor_wrap);
    fc_register_class_func(VM, nClassName, "LoadUserWidget", LoadUserWidget_wrap);
    fc_register_class_func(VM, nClassName, "GetChild", GetChild_Wrap);
    fc_register_class_func(VM, nClassName, "SetChild", SetChild_Wrap);
    fc_register_class_func(VM, nClassName, "CallFunction", CallFunction_Wrap);
    fc_register_class_func(VM, nClassName, "SuperCall", SuperCall_wrap);
    fc_register_class_func(VM, nClassName, "GetBindScript", GetBindScript_wrap);
    fc_register_class_func(VM, nClassName, "AToFString", String2FString_wrap);
    fc_register_class_func(VM, nClassName, "WToFString", String2FString_wrap);
    fc_register_class_func(VM, nClassName, "FStringToA", FString2String_wrap);
    fc_register_class_func(VM, nClassName, "FStringToW", FString2String_wrap);
    fc_register_class_func(VM, nClassName, "GetObjRefSize", GetObjRefSize_wrap);
    fc_register_class_func(VM, nClassName, "GetClassDescMemSize", GetClassDescMemSize_wrap);
}

int FCUEUtilWrap::GetName_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	if(ObjRef)
	{
		UObject *Object = ObjRef->GetUObject();
		if(Object)
		{
			FString Name = Object->GetName();
			fc_intptr RetPtr = fc_get_return_ptr(L);
			fc_set_value_string_w(RetPtr, (fc_ushort_ptr)(*Name), Name.Len());
		}
	}
	return 0;
}
int FCUEUtilWrap::GetOuter_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
	fc_intptr RetPtr = fc_get_return_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	UObject *Object = ObjRef ? ObjRef->GetUObject() : nullptr;
	if(Object)
	{
		fc_intptr value = FCGetObj::GetIns()->PushUObject(Object->GetOuter());
		fc_set_value_wrap_objptr(VM, RetPtr, value);
	}
	else
	{
		fc_set_value_wrap_objptr(VM, RetPtr, 0);
	}
	return 0;
}
int FCUEUtilWrap::GetClass_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
	fc_intptr RetPtr = fc_get_return_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	UObject *Object = ObjRef ? ObjRef->GetUObject() : nullptr;
	if(Object)
	{
		fc_intptr value = FCGetObj::GetIns()->PushUObject(Object->GetClass());
		fc_set_value_wrap_objptr(VM, RetPtr, value);
	}
	else
	{
		fc_set_value_wrap_objptr(VM, RetPtr, 0);
	}
	return 0;
}
int FCUEUtilWrap::FindClass_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr Arg0 = fc_get_param_ptr(L, 0);
    fc_intptr RetPtr = fc_get_return_ptr(L);
	const char *ClassName = fc_cpp_get_value_string_a(VM, Arg0); // ClassName 必须是带 _C 结尾的, 如 "/Game/CMShow/GitCMShow/Test/CMShowDomo/UMG/CMShowDemoUI.CMShowDemoUI_C"

	UStruct *Struct = FC_FindUEClass(ClassName);
    if (Struct)
    {
        fc_intptr value = FCGetObj::GetIns()->PushUObject(Struct);
        fc_set_value_wrap_objptr(VM, RetPtr, value);
    }
    else
    {
        fc_set_value_wrap_objptr(VM, RetPtr, 0);
    }
	return 0;
}
int FCUEUtilWrap::GetWorld_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_intptr ContextObjID = fc_get_wrap_objptr(L, 0);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ContextObjID);
	UObject *Object = ObjRef ? ObjRef->GetUObject() : nullptr;
	if(Object)
	{
        UWorld *Wrold = Object->GetWorld();
		fc_intptr value = FCGetObj::GetIns()->PushUObject(Wrold);
		fc_set_value_wrap_objptr(VM, RetPtr, value);
	}
	else
	{
		fc_set_value_wrap_objptr(VM, RetPtr, 0);
	}
	return 0;
}
int FCUEUtilWrap::GetMainGameInstance_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
	fc_intptr RetPtr = fc_get_return_ptr(L);
	
	UGameInstance *GameInstance = GEngine->GameViewport ? GEngine->GameViewport->GetGameInstance() : nullptr;
	fc_intptr value = FCGetObj::GetIns()->PushUObject(GameInstance);
	fc_set_value_wrap_objptr(VM, RetPtr, value);
	return 0;
}
int FCUEUtilWrap::AddToRoot_wrap(fc_intptr L)
{
    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	UObject *Object = ObjRef ? ObjRef->GetUObject() : nullptr;
	if(Object)
	{
		Object->AddToRoot();
	}
	return 0;
}
int FCUEUtilWrap::RemoveFromRoot_wrap(fc_intptr L)
{
    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	UObject *Object = ObjRef ? ObjRef->GetUObject() : nullptr;
	if(Object)
	{
		Object->RemoveFromRoot();
	}
	return 0;
}
int FCUEUtilWrap::NewObject_wrap(fc_intptr L)
{
	fc_intptr VM = fc_get_vm_ptr(L);
	fc_intptr Arg1 = fc_get_param_ptr(L, 1);
	fc_intptr Arg2 = fc_get_param_ptr(L, 2);
	fc_intptr Arg3 = fc_get_param_ptr(L, 3);

	fc_intptr OuterObjID = fc_get_wrap_objptr(L, 0);
	FCObjRef *OuterRef = FCGetObj::GetIns()->FindValue(OuterObjID);
	UObject *Outer = OuterRef ? OuterRef->GetUObject() : nullptr;
	if(!Outer)
	{
		Outer = GetTransientPackage();
	}
	fc_pcstr UEClassName = fc_cpp_get_value_string_a(VM, Arg1);
	fc_pcstr ScriptClassName = fc_cpp_get_value_string_a(VM, Arg2);
	fc_pcwstr ObjectName = fc_cpp_get_value_string_w(VM, Arg3);
	FCDynamicClassDesc *ClassDesc = GetScriptContext()->RegisterUClass(UEClassName);
	if(ClassDesc)
	{
		FName  Name(NAME_None);
		if(ObjectName && ObjectName[0] != 0)
		{
			Name = FName((const TCHAR*)ObjectName);
        }
        fc_intptr RetPtr = fc_get_return_ptr(L);
		fc_intptr ObjID = FCGetObj::GetIns()->PushNewObject(ClassDesc, Name, Outer, VM, RetPtr);
        UObject *Obj = FCGetObj::GetIns()->GetUObject(ObjID);
        if(Obj)
        {
            FFCObjectdManager::GetSingleIns()->CallBindScript(Obj, ScriptClassName);
        }
	}

	return 0;
}
int FCUEUtilWrap::SpawActor_wrap(fc_intptr L)
{
	return NewObject_wrap(L);
}

int FCUEUtilWrap::LoadUserWidget_wrap(fc_intptr L)
{
    // 加载一个蓝图对象 UClass *LoadUserWiget(UObject* WorldContextObject, const char *ClassName, APlayerController* OwningPlayer);
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_intptr Arg1 = fc_get_param_ptr(L, 1);

    fc_intptr WroldObjID = fc_get_wrap_objptr(L, 0);
    fc_intptr OwningPlayerObjID = fc_get_wrap_objptr(L, 2);

    UObject* WorldContextObject = FCGetObj::GetIns()->GetUObject(WroldObjID);
    const char* ClassName = fc_cpp_get_value_string_a(VM, Arg1);  // ClassName 必须是带 _C 结尾的, 如 "/Game/CMShow/GitCMShow/Test/CMShowDomo/UMG/CMShowDemoUI.CMShowDemoUI_C"
    UStruct* uiAsset = FC_FindUEClass(ClassName);
    UObject* OwningPlayer = FCGetObj::GetIns()->GetUObject(OwningPlayerObjID);

	UUserWidget  *UserWidget = UWidgetBlueprintLibrary::Create(WorldContextObject, Cast<UWidgetBlueprintGeneratedClass>(uiAsset), Cast< APlayerController>(OwningPlayer));
    if (UserWidget)
    {
        fc_intptr value = FCGetObj::GetIns()->PushUObject(UserWidget);
        fc_set_value_wrap_objptr(VM, RetPtr, value);
    }
    else
    {
        fc_set_value_wrap_objptr(VM, RetPtr, 0);
    }
	return 0;
}

int FCUEUtilWrap::GetChild_Wrap(fc_intptr L)
{
    // auto_return GetChild(UObject *Obj, const char *ChildName);
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_intptr Arg1 = fc_get_param_ptr(L, 1);
    const char* ChildName = fc_cpp_get_value_string_a(VM, Arg1);

    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
    if (ObjRef)
    {
        FCDynamicClassDesc* ClassDesc = ObjRef->ClassDesc;
        FCDynamicProperty* DynamicProperty = ClassDesc->FindAttribByName(ChildName);
        if (DynamicProperty && ObjRef->IsValid())
        {
            uint8* ObjAddr = (uint8*)(ObjRef->GetPropertyAddr());
            uint8* ValueAddr = ObjAddr + DynamicProperty->Offset_Internal;
            //void* PropertyAddr = DynamicProperty->Property->ContainerPtrToValuePtr<void>(ObjAddr);
            UObject* ThisObj = ObjRef->GetUObject();
            DynamicProperty->m_WriteScriptFunc(VM, RetPtr, DynamicProperty, ValueAddr, ThisObj, ObjRef);
        }
    }
    return 0;
}

int FCUEUtilWrap::SetChild_Wrap(fc_intptr L)
{
    // void SetChild(UObject *Obj, const char *ChildName, _Ty value);
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_intptr Arg1 = fc_get_param_ptr(L, 1);
    fc_intptr Arg2 = fc_get_param_ptr(L, 2);
    const char* ChildName = fc_cpp_get_value_string_a(VM, Arg1);

    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
    if (ObjRef)
    {
        FCDynamicClassDesc* ClassDesc = ObjRef->ClassDesc;
        FCDynamicProperty* DynamicProperty = ClassDesc->FindAttribByName(ChildName);
        if (DynamicProperty && ObjRef->IsValid())
        {
            uint8* ObjAddr = (uint8*)(ObjRef->GetPropertyAddr());
            uint8* ValueAddr = ObjAddr + DynamicProperty->Offset_Internal;
            UObject* ThisObj = ObjRef->GetUObject();
            DynamicProperty->m_ReadScriptFunc(VM, Arg2, DynamicProperty, ValueAddr, ThisObj, ObjRef);
        }
    }
    return 0;
}

int FCUEUtilWrap::CallFunction_Wrap(fc_intptr L)
{
    // auto_ret CallFunction(UObject Obj, StringA FuncName, Params [])
    return SuperCall_wrap(L);
}

int FCUEUtilWrap::SuperCall_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr Arg1 = fc_get_param_ptr(L, 1);
    int ParamCount = fc_get_param_count(L);

    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
    UObject *Object = FCGetObj::GetIns()->GetUObject(ObjID);
    if(Object)
    {
        FCDynamicClassDesc *DynamicClassDesc = GetScriptContext()->RegisterUStruct(Object->GetClass());
        if(DynamicClassDesc)
        {
            fc_pcstr FuncName = fc_cpp_get_value_string_a(VM, Arg1);
            FCDynamicFunction *DynamicFunc = DynamicClassDesc->FindFunctionByName(FuncName);
            if(DynamicFunc)
            {
                FFCObjectdManager::GetSingleIns()->NativeCall(Object, DynamicFunc, L, 2);
            }
        }
    }    
    return 0;
}

int FCUEUtilWrap::GetBindScript_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
    UObject *Object = FCGetObj::GetIns()->GetUObject(ObjID);
	fc_intptr RetPtr = fc_get_return_ptr(L);
    if(Object)
    {
		FBindObjectInfo *BindInfo = FFCObjectdManager::GetSingleIns()->FindBindObjectInfo(Object);
		if(BindInfo)
		{
			fc_set_value_script_instance(L, RetPtr, BindInfo->m_ScriptIns);
			return 0;
		}
	}
	fc_set_value_script_instance(L, RetPtr, 0);  // 没有找到
	return 0;
}

int FCUEUtilWrap::FString2String_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr ObjID = fc_get_wrap_objptr(L, 0);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (FCPropertyType::FCPROPERTY_StrProperty == ObjRef->DynamicProperty->Type)
        {
            const FString& value = *((const FString*)ObjRef->GetThisAddr());
            fc_set_value_string_w(RetPtr, (fc_ushort_ptr)*value, value.Len());
            return 0;
        }
        else if (FCPROPERTY_NameProperty == ObjRef->DynamicProperty->Type)
        {
            const FName& valueName = *((const FName*)ObjRef->GetThisAddr());
            FString value = valueName.ToString();
            fc_set_value_string_w(RetPtr, (fc_ushort_ptr)*value, value.Len());
            return 0;
        }
    }
    fc_set_value_string(RetPtr, nullptr);
	return 0;
}

int FCUEUtilWrap::String2FString_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr Arg0 = fc_get_param_ptr(L, 0);
	fc_pcwstr InArgStr = fc_cpp_get_value_string_w(VM, Arg0);
    fc_intptr ObjID = 0;
    fc_intptr RetPtr = fc_get_return_ptr(L);

	FCDynamicProperty* PropertyDesc = GetCppDynamicProperty("FString");
	if(PropertyDesc)
	{
		FString value(InArgStr);
		ObjID = FCGetObj::GetIns()->PushCppPropery(PropertyDesc, &value);
	}
	fc_set_value_wrap_objptr(VM, RetPtr, ObjID);
    return 0;
}

int FCUEUtilWrap::GetObjRefSize_wrap(fc_intptr L)
{
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_int(RetPtr, sizeof(FCObjRef));
    return 0;
}

int FCUEUtilWrap::GetClassDescMemSize_wrap(fc_intptr L)
{
    fc_intptr RetPtr = fc_get_return_ptr(L);
    const char * ClassName = fc_cpp_get_string_a(L, 0);
    if(ClassName)
    {
        FCDynamicClassDesc* DynamicClassDesc = GetScriptContext()->RegisterUClass(ClassName);
        fc_set_value_int(RetPtr, DynamicClassDesc ? DynamicClassDesc->GetMemSize() : 0);
    }
    else
    {
        fc_set_value_int(RetPtr, GetScriptContext()->GetMemSize());
    }
    return 0;
}