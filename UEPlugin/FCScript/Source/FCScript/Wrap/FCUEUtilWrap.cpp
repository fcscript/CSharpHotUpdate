#include "FCUEUtilWrap.h"
#include "FCObjectManager.h"
#include "FCGetObj.h"
#include "FCRunTimeRegister.h"


void FCUEUtilWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "UEUtil");
    fc_register_class_func(VM, nClassName, "GetName",GetName_wrap);
    fc_register_class_func(VM, nClassName, "GetOuter",GetOuter_wrap);
    fc_register_class_func(VM, nClassName, "GetClass",GetClass_wrap);
    fc_register_class_func(VM, nClassName, "GetWorld",GetWorld_wrap);
    fc_register_class_func(VM, nClassName, "GetMainGameInstance",GetMainGameInstance_wrap);
    fc_register_class_func(VM, nClassName, "AddToRoot",AddToRoot_wrap);
    fc_register_class_func(VM, nClassName, "RemoveFromRoot",RemoveFromRoot_wrap);
    fc_register_class_func(VM, nClassName, "NewObject",NewObject_wrap);
    fc_register_class_func(VM, nClassName, "SpawActor", SpawActor_wrap);
    fc_register_class_func(VM, nClassName, "SuperCall", SuperCall_wrap);
    fc_register_class_func(VM, nClassName, "GetBindScript", GetBindScript_wrap);
}

int FCUEUtilWrap::GetName_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
	fc_intptr ObjPtr = fc_get_param_ptr(L, 0); //
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjPtr);
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
	fc_intptr ObjPtr = fc_get_param_ptr(L, 0);
	fc_intptr RetPtr = fc_get_return_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjPtr);
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
	fc_intptr ObjPtr = fc_get_param_ptr(L, 0);
	fc_intptr RetPtr = fc_get_return_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjPtr);
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
int FCUEUtilWrap::GetWorld_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
	fc_intptr ObjPtr = fc_get_param_ptr(L, 0);
	fc_intptr RetPtr = fc_get_return_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjPtr);
	UObject *Object = ObjRef ? ObjRef->GetUObject() : nullptr;
	if(Object)
	{
		fc_intptr value = FCGetObj::GetIns()->PushUObject(Object->GetWorld());
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
	fc_intptr ObjPtr = fc_get_param_ptr(L, 0);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjPtr);
	UObject *Object = ObjRef ? ObjRef->GetUObject() : nullptr;
	if(Object)
	{
		Object->AddToRoot();
	}
	return 0;
}
int FCUEUtilWrap::RemoveFromRoot_wrap(fc_intptr L)
{
	fc_intptr ObjPtr = fc_get_param_ptr(L, 0);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjPtr);
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
	fc_intptr Arg0 = fc_get_param_ptr(L, 0);
	fc_intptr Arg1 = fc_get_param_ptr(L, 1);
	fc_intptr Arg2 = fc_get_param_ptr(L, 2);
	fc_intptr Arg3 = fc_get_param_ptr(L, 3);

	fc_intptr OuterObjID = fc_get_value_wrap_objptr(Arg0);
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
int FCUEUtilWrap::SuperCall_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr Arg0 = fc_get_param_ptr(L, 0);
    fc_intptr Arg1 = fc_get_param_ptr(L, 1);
    int ParamCount = fc_get_param_count(L);

    fc_intptr ObjID = fc_get_value_wrap_objptr(Arg0);
    UObject *Object = FCGetObj::GetIns()->GetUObject(ObjID);
    if(Object)
    {
        FCDynamicClassDesc *DynamicClassDesc = GetScriptContext()->RegisterUStruct(Object->GetClass());
        if(DynamicClassDesc)
        {
            fc_pcstr FuncName = fc_cpp_get_value_string_a(VM, Arg1);
            FCDynamicFunction *DynamicFunc = DynamicClassDesc->FindFunctionByName(FuncName);
            if(DynamicFunc && DynamicFunc->OleNativeFuncPtr)
            {
                uint8   Buffer[256];
                WrapNativeCallFunction(L, 2, Object, DynamicFunc, Buffer, sizeof(Buffer), DynamicFunc->OleNativeFuncPtr);
            }
        }
    }    
    return 0;
}

int FCUEUtilWrap::GetBindScript_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr Arg0 = fc_get_param_ptr(L, 0);
    fc_intptr ObjID = fc_get_value_wrap_objptr(Arg0);
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
	fc_set_value_script_instance(L, RetPtr, 0);  // ц╩спур╣╫
	return 0;
}

