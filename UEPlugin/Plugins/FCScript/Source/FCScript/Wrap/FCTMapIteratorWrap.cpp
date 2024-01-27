#include "FCTMapIteratorWrap.h"
#include "Containers/Map.h"
#include "FCTemplateType.h"

#include "FCObjectManager.h"
#include "FCGetObj.h"

void FCTMapIteratorWrap::Register(fc_intptr VM)
{
	int nClassName = fc_get_inport_class_id(VM, "TMapIterator");
	fc_register_class_new(VM, nClassName, obj_new);
	fc_register_class_del(VM, nClassName, obj_del);
    fc_register_class_release_ref(VM, nClassName, obj_release);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);

	// 属性方法
	fc_register_class_attrib(VM, nClassName, "key", Key_wrap, nullptr);
	fc_register_class_attrib(VM, nClassName, "value", GetValue_wrap, SetValue_wrap);

	fc_register_class_func(VM, nClassName, "IsValid", IsValid_wrap);
	fc_register_class_func(VM, nClassName, "ToNext", ToNext_wrap);
	fc_register_class_func(VM, nClassName, "Reset", Reset_wrap);
}

int FCTMapIteratorWrap::obj_new(fc_intptr L)
{
	// 这个还是不要让动态构建的好了
	// 因为不管怎么样，就算是相同的，也是需要拷贝的
	fc_intptr RetPtr = fc_get_return_ptr(L);
	fc_intptr VM = fc_get_vm_ptr(L);

    //TMapIterator* Iterator = new TMapIterator();
    //fc_intptr ItInsID = FCGetObj::GetIns()->PushMapIterator(Iterator);
    //fc_intptr RetPtr = fc_get_return_ptr(L);
    //fc_set_value_wrap_objptr(VM, RetPtr, ItInsID);

	return 0;
}
int FCTMapIteratorWrap::obj_del(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->DeleteValue(nIntPtr);
	return 0;
}
int FCTMapIteratorWrap::obj_release(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->ReleaseValue(nIntPtr);
	return 0;
}

int FCTMapIteratorWrap::obj_hash(fc_intptr nIntPtr)
{
    return FCGetObj::GetIns()->GetValueHash(nIntPtr);
}

bool FCTMapIteratorWrap::obj_equal(fc_intptr L, fc_intptr R)
{
    return FCGetObj::GetIns()->EqualValue(L, R);
}

int FCTMapIteratorWrap::Key_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    if (ObjRef && ObjRef->RefType == EFCObjRefType::MapIterator)
    {
        TMapIterator* Iterator = ObjRef->GetMapIterator();
        if (Iterator)
        {
            FScriptMap* ScriptMap = FCTMapIteratorWrap::GetScriptMap(Iterator->MapInsID);
            if (ScriptMap && ScriptMap->IsValidIndex(Iterator->Index))
            {
                FCObjRef* MapRef = FCGetObj::GetIns()->FindValue(Iterator->MapInsID);

                FMapProperty* MapProperty = MapRef->DynamicProperty->SafePropertyPtr->CastMapProperty();
                FProperty* KeyProp = MapProperty->KeyProp;
                const FScriptMapLayout& MapLayout = MapProperty->MapLayout;

                FCDynamicProperty  KeyProperty;
                KeyProperty.InitProperty(KeyProp);

                fc_intptr VM = fc_get_vm_ptr(L);
                fc_intptr RetPtr = fc_get_return_ptr(L);
                uint8* PairPtr = (uint8*)ScriptMap->GetData(Iterator->Index, MapLayout);
                KeyProperty.m_WriteScriptFunc(VM, RetPtr, &KeyProperty, PairPtr, nullptr, nullptr);
            }
        }
    }
	return 0;
}
int FCTMapIteratorWrap::GetValue_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    if (ObjRef && ObjRef->RefType == EFCObjRefType::MapIterator)
    {
		TMapIterator  *Iterator = ObjRef->GetMapIterator();
		if(Iterator)
        {
            FScriptMap* ScriptMap = FCTMapIteratorWrap::GetScriptMap(Iterator->MapInsID);
			if(ScriptMap && ScriptMap->IsValidIndex(Iterator->Index))
            {
                FCObjRef* MapRef = FCGetObj::GetIns()->FindValue(Iterator->MapInsID);

                FMapProperty* MapProperty = MapRef->DynamicProperty->SafePropertyPtr->CastMapProperty();
                FProperty* ValueProp = MapProperty->ValueProp;
                const FScriptMapLayout& MapLayout = MapProperty->MapLayout;

                FCDynamicProperty  ValueProperty;
                ValueProperty.InitProperty(ValueProp);

                fc_intptr VM = fc_get_vm_ptr(L);
                fc_intptr RetPtr = fc_get_return_ptr(L);
                uint8* PairPtr = (uint8*)ScriptMap->GetData(Iterator->Index, MapLayout);
                uint8* Result = PairPtr + MapLayout.ValueOffset;
                ValueProperty.m_WriteScriptFunc(VM, RetPtr, &ValueProperty, Result, nullptr, nullptr);
			}
		}
    }
	return 0;
}
int FCTMapIteratorWrap::SetValue_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    if (ObjRef && ObjRef->RefType == EFCObjRefType::MapIterator)
    {
        TMapIterator* Iterator = ObjRef->GetMapIterator();
        if (Iterator)
        {
            FScriptMap* ScriptMap = FCTMapIteratorWrap::GetScriptMap(Iterator->MapInsID);
            if (ScriptMap && ScriptMap->IsValidIndex(Iterator->Index))
            {
                FCObjRef* MapRef = FCGetObj::GetIns()->FindValue(Iterator->MapInsID);

                FMapProperty* MapProperty = MapRef->DynamicProperty->SafePropertyPtr->CastMapProperty();
                FProperty* ValueProp = MapProperty->ValueProp;
                const FScriptMapLayout& MapLayout = MapProperty->MapLayout;

                FCDynamicProperty  ValueProperty;
                ValueProperty.InitProperty(ValueProp);

                fc_intptr VM = fc_get_vm_ptr(L);
                fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
                uint8* PairPtr = (uint8*)ScriptMap->GetData(Iterator->Index, MapLayout);
                uint8* Result = PairPtr + MapLayout.ValueOffset;
                ValueProperty.m_ReadScriptFunc(VM, ArgPtr0, &ValueProperty, Result, nullptr, nullptr);
            }
        }
    }
	return 0;
}

int FCTMapIteratorWrap::IsValid_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    bool bValid = false;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::MapIterator)
    {
        TMapIterator* Iterator = ObjRef->GetMapIterator();
        if (Iterator)
        {
            FScriptMap* ScriptMap = FCTMapIteratorWrap::GetScriptMap(Iterator->MapInsID);
            if(ScriptMap && ScriptMap->IsValidIndex(Iterator->Index))
            {
                bValid = true;
            }
        }        
    }
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_bool(RetPtr, bValid);
	return 0;
}

int FCTMapIteratorWrap::ToNext_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    bool bValid = false;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::MapIterator)
    {
        TMapIterator* Iterator = ObjRef->GetMapIterator();
        if (Iterator)
        {
            FScriptMap* ScriptMap = FCTMapIteratorWrap::GetScriptMap(Iterator->MapInsID);
            if (ScriptMap)
            {
                ++(Iterator->Index);
                Iterator->Index = FCTMapIteratorWrap::ToNextValidIterator(ScriptMap, Iterator->Index);
                bValid = ScriptMap->IsValidIndex(Iterator->Index);
            }
        }
    }
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_bool(RetPtr, bValid);
	return 0;
}

int FCTMapIteratorWrap::Reset_wrap(fc_intptr L)
{
	return 0;
}

FScriptMap* FCTMapIteratorWrap::GetScriptMap(int64 nIntPtr)
{
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nIntPtr);
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
        {
            FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
			return ScriptMap;
		}
	}
	return nullptr;
}

int32 FCTMapIteratorWrap::ToNextValidIterator(FScriptMap* ScriptMap, int32 NextIndex)
{
	int32 nMaxIndex = ScriptMap->GetMaxIndex();
	for(; NextIndex < nMaxIndex; ++NextIndex)
	{
		if(ScriptMap->IsValidIndex(NextIndex))
		{
			return NextIndex;
		}
	}
	return NextIndex;
}
