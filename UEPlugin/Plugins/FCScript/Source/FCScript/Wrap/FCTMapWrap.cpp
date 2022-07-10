#include "FCTMapWrap.h"
#include "Containers/Map.h"
#include "FCTemplateType.h"

#include "FCObjectManager.h"
#include "FCGetObj.h"
#include "FCTMapIteratorWrap.h"
#include "FTMapKeyValueBuffer.h"

void FCTMapWrap::Register(fc_intptr VM)
{
	int nClassName = fc_get_inport_class_id(VM, "TMap");
	fc_register_class_new(VM, nClassName, obj_new);
	fc_register_class_del(VM, nClassName, obj_del);
	fc_register_class_release_ref(VM, nClassName, obj_release);
	fc_register_class_hash(VM, nClassName, obj_hash);
	fc_register_class_equal(VM, nClassName, obj_equal);
	fc_register_class_func(VM, nClassName, "GetNumb", GetNumb_wrap);
	fc_register_class_func(VM, nClassName, "TryGetValue", TryGetValue_wrap);  // bool TryGetValue(_TyKey key, out _TyValue value)
	fc_register_class_func(VM, nClassName, "Add", Add_wrap);
	fc_register_class_func(VM, nClassName, "Remove", Remove_wrap);
	fc_register_class_func(VM, nClassName, "Clear", Clear_wrap);
	fc_register_class_func(VM, nClassName, "ToMap", ToMap_wrap);
	fc_register_class_func(VM, nClassName, "SetMap", SetMap_wrap);

	// 属性方法
	fc_register_class_attrib(VM, nClassName, "[]", GetIndex_wrap, SetIndex_wrap);
	fc_register_class_attrib(VM, nClassName, "Length", GetNumb_wrap, nullptr);

	// 兼容stl接口
	fc_register_class_func(VM, nClassName, "size", GetNumb_wrap);
	fc_register_class_func(VM, nClassName, "push_back", Add_wrap);
    fc_register_class_func(VM, nClassName, "begin", begin_wrap);
    fc_register_class_func(VM, nClassName, "find", find_wrap);
}

int FCTMapWrap::obj_new(fc_intptr L)
{
	// FScriptArray *ScriptArray = new FScriptArray;
	// 这个还是不要让动态构建的好了
	// 因为不管怎么样，就算是相同的，也是需要拷贝的
	fc_intptr RetPtr = fc_get_return_ptr(L);
	fc_intptr VM = fc_get_vm_ptr(L);

	FCDynamicProperty *DynamicProperty = GetTMapDynamicProperty(VM, RetPtr);
	if(DynamicProperty)
	{
		FScriptMap *ScriptMap = new FScriptMap();
		int64 ObjID = FCGetObj::GetIns()->PushTemplate(DynamicProperty, ScriptMap, EFCObjRefType::NewTMap);
		fc_set_value_wrap_objptr(VM, RetPtr, ObjID);
	}
	else
	{
		fc_set_value_wrap_objptr(VM, RetPtr, 0);
	}

	return 0;
}
int FCTMapWrap::obj_del(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->DeleteValue(nIntPtr);
	return 0;
}
int FCTMapWrap::obj_release(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->ReleaseValue(nIntPtr);
	return 0;
}
int FCTMapWrap::obj_hash(fc_intptr nIntPtr)
{
	return FCGetObj::GetIns()->GetValueHash(nIntPtr);
}
bool FCTMapWrap::obj_equal(fc_intptr L, fc_intptr R)
{
	return FCGetObj::GetIns()->EqualValue(L, R);
}
int FCTMapWrap::GetNumb_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTMap)
	{
		FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
		int Num = ScriptMap->Num();
		fc_intptr RetPtr = fc_get_return_ptr(L);
		fc_set_value_uint(RetPtr, Num);
	}
	else
	{
		fc_intptr RetPtr = fc_get_return_ptr(L);
		fc_set_value_uint(RetPtr, 0);
	}
	return 0;
}

bool TMap_GetAt(FCObjRef* ObjRef, fc_intptr L, fc_intptr KeyPtr, fc_intptr ValuePtr)
{
	fc_intptr VM = fc_get_vm_ptr(L);

	FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
	FMapProperty* MapProperty = (FMapProperty*)ObjRef->DynamicProperty->Property;
	FProperty* KeyProp = MapProperty->KeyProp;

	FTMapKeyValueBuffer KeyBuffer(KeyProp, VM, KeyPtr);
	
	uint8 *ValueAddr = ScriptMap->FindValue(KeyBuffer.Buffer, MapProperty->MapLayout,			
			[KeyProp](const void* ElementKey) { return KeyProp->GetValueTypeHash(ElementKey); },
			[KeyProp](const void* A, const void* B) { return KeyProp->Identical(A, B); }
			);
	if(ValueAddr)
	{
		FCDynamicProperty* ElementProperty = GetDynamicPropertyByUEProperty(MapProperty->ValueProp);
		ElementProperty->m_WriteScriptFunc(VM, ValuePtr, ElementProperty, ValueAddr, nullptr, nullptr);
		return true;
	}
	return false;
}

void TMap_AddBase(FCObjRef* ObjRef, fc_intptr L, FTMapKeyValueBuffer &Key, FTMapKeyValueBuffer &Value)
{
	FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
	FMapProperty* MapProperty = (FMapProperty*)ObjRef->DynamicProperty->Property;
	FProperty* KeyProp = MapProperty->KeyProp;
	FProperty* ValueProp = MapProperty->ValueProp;
	
	void *KeyBuffer = Key.Buffer;
	void *ValueBuffer = Value.Buffer;
		
	ScriptMap->Add(Key.Buffer, Value.Buffer, MapProperty->MapLayout,
			[KeyProp](const void* ElementKey) { return KeyProp->GetValueTypeHash(ElementKey); },
			[KeyProp](const void* A, const void* B) { return KeyProp->Identical(A, B); }, // KeyEqualityFn
			[KeyProp, KeyBuffer](void* Dest) { KeyProp->InitializeValue(Dest); KeyProp->CopySingleValue(Dest, KeyBuffer); },      // KeyConstructAndAssignFn
			[ValueProp, ValueBuffer](void* Dest) { ValueProp->InitializeValue(Dest); ValueProp->CopySingleValue(Dest, ValueBuffer); },  // ValueConstructAndAssignFn
			[ValueProp, ValueBuffer](void* Dest) { ValueProp->CopySingleValue(Dest, ValueBuffer); },  // ValueAssignFn 
			[KeyProp](void* Dest) { KeyProp->DestroyValue(Dest); },			// DestructKeyFn
			[ValueProp](void* Dest) { ValueProp->DestroyValue(Dest); }		// DestructValueFn
			);
}

void TMap_Add(FCObjRef* ObjRef, fc_intptr L, fc_intptr KeyPtr, fc_intptr ValuePtr)
{
	fc_intptr VM = fc_get_vm_ptr(L);
	FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
	FMapProperty* MapProperty = (FMapProperty*)ObjRef->DynamicProperty->Property;
	FTMapKeyValueBuffer Key(MapProperty->KeyProp, VM, KeyPtr);
	FTMapKeyValueBuffer Value(MapProperty->ValueProp, VM, ValuePtr);

	TMap_AddBase(ObjRef, L, Key, Value);
}

int FCTMapWrap::TryGetValue_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
		{  // bool Find(_TyKey key, _TyValue &value);
			fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
			fc_intptr ArgPtr1 = fc_get_param_ptr(L, 1);
			fc_intptr RetPtr = fc_get_return_ptr(L);
			bool bSuc = TMap_GetAt(ObjRef, L, ArgPtr0, ArgPtr1);
			fc_set_value_bool(RetPtr, bSuc);
			return 0;
		}
	}
	fc_intptr RetPtr = fc_get_return_ptr(L);
	fc_set_value_bool(RetPtr, false);
	return 0;
}

int FCTMapWrap::GetIndex_wrap(fc_intptr L)
{
    // value = map[key]
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
		{
			fc_intptr KeyPtr = fc_get_param_ptr(L, 0);
			fc_intptr ValuePtr = fc_get_return_ptr(L);
			TMap_GetAt(ObjRef, L, KeyPtr, ValuePtr);
		}
	}
	return 0;
}

int FCTMapWrap::SetIndex_wrap(fc_intptr L)
{
    // map[key] = value
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
		{
			fc_intptr KeyPtr = fc_get_param_ptr(L, 0);
			fc_intptr ValuePtr = fc_get_return_ptr(L);
			TMap_Add(ObjRef, L, KeyPtr, ValuePtr);
		}
	}
	return 0;
}

int FCTMapWrap::Add_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
		{
			fc_intptr KeyPtr = fc_get_param_ptr(L, 0);
			fc_intptr ValuePtr = fc_get_param_ptr(L, 1);
			TMap_Add(ObjRef, L, KeyPtr, ValuePtr);
		}
	}
	return 0;
}

int FCTMapWrap::Remove_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
		{
			fc_intptr KeyPtr = fc_get_param_ptr(L, 0);
						
			fc_intptr VM = fc_get_vm_ptr(L);

			FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
			FMapProperty* MapProperty = (FMapProperty*)ObjRef->DynamicProperty->Property;
			FProperty* KeyProp = MapProperty->KeyProp;
			FProperty* ValueProp = MapProperty->ValueProp;
	
			FTMapKeyValueBuffer Key(KeyProp, VM, KeyPtr);
			void *KeyBuffer = Key.Buffer;
			const FScriptMapLayout &MapLayout = MapProperty->MapLayout;

			int32 PairIndex = ScriptMap->FindPairIndex(KeyBuffer, MapLayout,
				[KeyProp](const void* ElementKey) { return KeyProp->GetValueTypeHash(ElementKey); }, // GetKeyHash
				[KeyProp](const void* A, const void* B) { return KeyProp->Identical(A, B); } // KeyEqualityFn
				);
			if(ScriptMap->IsValidIndex(PairIndex))
			{
				uint8* PairPtr = (uint8*)ScriptMap->GetData(PairIndex, MapLayout);
				uint8* Result  = PairPtr + MapLayout.ValueOffset;
				KeyProp->DestroyValue(PairPtr);
				ValueProp->DestroyValue(Result);
				
				ScriptMap->RemoveAt(PairIndex, MapProperty->MapLayout);
			}
		}
	}
	return 0;
}

int FCTMapWrap::Clear_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
		{
			FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
			FMapProperty* MapProperty = (FMapProperty*)ObjRef->DynamicProperty->Property;
			TMap_Clear(ScriptMap, MapProperty);
		}
	}
	return 0;
}

int FCTMapWrap::ToMap_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
		{
			FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
			FMapProperty* MapProperty = (FMapProperty*)ObjRef->DynamicProperty->Property;
			FProperty* KeyProp = MapProperty->KeyProp;
			FProperty* ValueProp = MapProperty->ValueProp;
			const FScriptMapLayout &MapLayout = MapProperty->MapLayout;

			int32  MaxIndex = ScriptMap->GetMaxIndex();
			fc_intptr MapPtr = fc_get_return_ptr(L);
			fc_intptr VM = fc_get_vm_ptr(L);
			fc_map_clear(VM, MapPtr);
			fc_intptr KeyPtr = fc_get_map_push_key_ptr(VM, MapPtr);
			fc_intptr ValuePtr = fc_get_map_push_value_ptr(VM, MapPtr);
			
			FCDynamicProperty  KeyProperty, ValueProperty;
			KeyProperty.InitProperty(KeyProp);
			ValueProperty.InitProperty(ValueProp);

			for(int32 PairIndex = 0; PairIndex < MaxIndex; ++PairIndex)
			{
				if(ScriptMap->IsValidIndex(PairIndex))
				{
					uint8* PairPtr = (uint8*)ScriptMap->GetData(PairIndex, MapLayout);
					uint8* Result  = PairPtr + MapLayout.ValueOffset;
					KeyProperty.m_WriteScriptFunc(VM, KeyPtr, &KeyProperty, PairPtr, nullptr, nullptr);
					ValueProperty.m_WriteScriptFunc(VM, ValuePtr, &ValueProperty, Result, nullptr, nullptr);
					fc_map_push_key_value(VM, MapPtr);
				}
			}
		}
	}
	return 0;
}

int FCTMapWrap::SetMap_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
		{
			FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
			FMapProperty* MapProperty = (FMapProperty*)ObjRef->DynamicProperty->Property;
			TMap_Clear(ScriptMap, MapProperty);
									
			fc_intptr VM = fc_get_vm_ptr(L);
			FTMapKeyValueBuffer Key(MapProperty->KeyProp);
			FTMapKeyValueBuffer Value(MapProperty->ValueProp);
						
			fc_intptr MapPtr = fc_get_param_ptr(L, 0);
			if(fc_map_prepare_view(VM, MapPtr))
			{
				while(fc_map_to_next_pair(VM))
				{
					fc_intptr KeyPtr = fc_map_get_cur_key_ptr(VM);
					fc_intptr ValuePtr = fc_map_get_cur_value_ptr(VM);
					Key.ReadScriptValue(VM, KeyPtr);
					Value.ReadScriptValue(VM, ValuePtr);
					TMap_AddBase(ObjRef, L, Key, Value);
				}
			}
		}
	}
	return 0;
}

int FCTMapWrap::begin_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
        {
            FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
			fc_intptr VM = fc_get_vm_ptr(L);
			TMapIterator  *Iterator = new TMapIterator();
			Iterator->MapInsPtr = nThisPtr;
			Iterator->Index = FCTMapIteratorWrap::ToNextValidIterator(ScriptMap, 0);
			fc_intptr ItInsID = FCGetObj::GetIns()->PushMapIterator(Iterator);
            fc_intptr RetPtr = fc_get_return_ptr(L);
			fc_set_value_wrap_objptr(VM, RetPtr, ItInsID);
		}
	}
	return 0;
}

int FCTMapWrap::find_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    fc_intptr ItInsID = 0;
    fc_intptr VM = fc_get_vm_ptr(L);
    if (ObjRef && ObjRef->DynamicProperty)
    {
        if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
        {
            fc_intptr KeyPtr = fc_get_param_ptr(L, 0);

            FScriptMap* ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
            FMapProperty* MapProperty = (FMapProperty*)ObjRef->DynamicProperty->Property;
            FProperty* KeyProp = MapProperty->KeyProp;

            FTMapKeyValueBuffer Key(KeyProp, VM, KeyPtr);
            void* KeyBuffer = Key.Buffer;
            const FScriptMapLayout& MapLayout = MapProperty->MapLayout;

            int32 PairIndex = ScriptMap->FindPairIndex(KeyBuffer, MapLayout,
                [KeyProp](const void* ElementKey) { return KeyProp->GetValueTypeHash(ElementKey); }, // GetKeyHash
                [KeyProp](const void* A, const void* B) { return KeyProp->Identical(A, B); } // KeyEqualityFn
            );
            if (ScriptMap->IsValidIndex(PairIndex))
            {
                TMapIterator* Iterator = new TMapIterator();
                Iterator->MapInsPtr = nThisPtr;
                Iterator->Index = FCTMapIteratorWrap::ToNextValidIterator(ScriptMap, 0);
                ItInsID = FCGetObj::GetIns()->PushMapIterator(Iterator);
            }
        }
    }
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_wrap_objptr(VM, RetPtr, ItInsID);

	return 0;
}