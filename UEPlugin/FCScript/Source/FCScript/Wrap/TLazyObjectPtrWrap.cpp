#include "TLazyObjectPtrWrap.h"
#include "FCObjectManager.h"
#include "FCGetObj.h"

void TLazyObjectPtrWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "TLazyObjectPtr");
    //fc_register_class_new(VM, nClassName, obj_new);
    //fc_register_class_del(VM, nClassName,obj_del);
    fc_register_class_release_ref(VM, nClassName,obj_release);
    //fc_register_class_hash(VM, nClassName,obj_hash);
    //fc_register_class_equal(VM, nClassName,obj_equal);
    fc_register_class_func(VM, nClassName,"Reset",Reset_wrap);
    fc_register_class_func(VM, nClassName, "ResetWeakPtr", ResetWeakPtr_wrap);
    fc_register_class_func(VM, nClassName,"IsValid",IsValid_wrap);
    fc_register_class_func(VM, nClassName, "IsPending", IsPending_wrap);
    fc_register_class_func(VM, nClassName,"Get",Get_wrap);
    fc_register_class_func(VM, nClassName,"Set",Set_wrap);
}

int TLazyObjectPtrWrap::obj_release(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->ReleaseValue(nIntPtr);
    return 0;
}

int TLazyObjectPtrWrap::Reset_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_LazyObjectPtr)
        {
			FLazyObjectPtr *ScriptPtr = (FLazyObjectPtr*)ObjRef->GetPropertyAddr();
            ScriptPtr->Reset();
        }        
    }
    return 0;
}

int TLazyObjectPtrWrap::ResetWeakPtr_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_LazyObjectPtr)
        {
            FLazyObjectPtr* ScriptPtr = (FLazyObjectPtr*)ObjRef->GetPropertyAddr();
            ScriptPtr->ResetWeakPtr();
        }
    }
    return 0;
}

int TLazyObjectPtrWrap::IsValid_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    bool bValidPtr = false;
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_LazyObjectPtr)
        {
			FLazyObjectPtr *ScriptPtr = (FLazyObjectPtr*)ObjRef->GetPropertyAddr();
            bValidPtr = ScriptPtr->IsValid();
        }        
    }
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_bool(RetPtr, bValidPtr);
    return 0;
}

int TLazyObjectPtrWrap::IsPending_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    bool bValidPtr = false;
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_LazyObjectPtr)
        {
            FLazyObjectPtr* ScriptPtr = (FLazyObjectPtr*)ObjRef->GetPropertyAddr();
            bValidPtr = ScriptPtr->IsPending();
        }
    }
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_bool(RetPtr, bValidPtr);
    return 0;
}

int TLazyObjectPtrWrap::Get_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    bool bValidPtr = false;
    int64 ObjID = 0;
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_LazyObjectPtr)
        {
            FLazyObjectPtr* ScriptPtr = (FLazyObjectPtr*)ObjRef->GetPropertyAddr();
            UObject* Obj = ScriptPtr->Get();
            ObjID = FCGetObj::GetIns()->PushUObject(Obj);
        }
    }
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_wrap_objptr(VM, RetPtr, ObjID);
    return 0;
}

int TLazyObjectPtrWrap::Set_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    bool bValidPtr = false;
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_LazyObjectPtr)
        {
            FLazyObjectPtr* ScriptPtr = (FLazyObjectPtr*)ObjRef->GetPropertyAddr();

            fc_intptr  ArgPtr0 = fc_get_param_ptr(L, 0);
            fc_intptr  Arg0 = fc_get_value_wrap_objptr(ArgPtr0);
            UObject* Obj = FCGetObj::GetIns()->GetUObject(Arg0);
            *ScriptPtr = Obj;
        }
    }
    return 0;
}

