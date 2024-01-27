#include "TSoftObjectPtrWrap.h"
#include "FCObjectManager.h"
#include "FCGetObj.h"


void TSoftObjectPtrWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "TSoftObjectPtr");
    //fc_register_class_new(VM, nClassName, obj_new);
    //fc_register_class_del(VM, nClassName,obj_del);
    fc_register_class_release_ref(VM, nClassName,obj_release);
    //fc_register_class_hash(VM, nClassName,obj_hash);
    //fc_register_class_equal(VM, nClassName,obj_equal);
    fc_register_class_func(VM, nClassName,"LoadSynchronous",LoadSynchronous_wrap);
    fc_register_class_func(VM, nClassName,"GetAssetName",GetAssetName_wrap);
}

int TSoftObjectPtrWrap::obj_release(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->ReleaseValue(nIntPtr);
    return 0;
}

int TSoftObjectPtrWrap::LoadSynchronous_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int64 ObjID = 0;
	if (ObjRef && ObjRef->DynamicProperty)
	{
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_SoftObjectReference
            || ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_SoftClassReference)
        {
			FSoftObjectPtr *ScriptPtr = (FSoftObjectPtr*)ObjRef->GetPropertyAddr();
			FSoftObjectProperty  *Property = (FSoftObjectProperty *)ObjRef->DynamicProperty->SafePropertyPtr->GetProperty();
            UClass *ObjClass = Property->PropertyClass;
            UObject *Obj = ScriptPtr->LoadSynchronous();
            ObjID = FCGetObj::GetIns()->PushUObject(Obj);
        }        
    }
    fc_set_value_wrap_objptr(VM, VM, ObjID);
    return 0;
}

int TSoftObjectPtrWrap::GetAssetName_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_SoftObjectReference
            || ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_SoftClassReference)
        {
            FSoftObjectPtr* ScriptPtr = (FSoftObjectPtr*)ObjRef->GetPropertyAddr();
            FString  AssetName = ScriptPtr->GetAssetName();
            fc_set_value_string_w(RetPtr, (fc_ushort_ptr)(*AssetName), AssetName.Len());
            return 0;
        }
    }
    fc_set_value_string(RetPtr, "");
    return 0;
}
