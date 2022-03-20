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
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_WeakObjectPtr)
        {
			FSoftObjectPtr *ScriptPtr = (FSoftObjectPtr*)ObjRef->GetPropertyAddr();
			FSoftObjectProperty  *Property = (FSoftObjectProperty *)ObjRef->DynamicProperty->Property;
            UClass *ObjClass = Property->PropertyClass;
            int iiii = 0;
        }        
    }
    return 0;
}

int TSoftObjectPtrWrap::GetAssetName_wrap(fc_intptr L)
{
    return 0;
}
