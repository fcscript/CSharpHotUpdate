#include "FCTemplateType.h"


#if ENGINE_MINOR_VERSION < 25 
template<class _Ty>
_Ty * NewUEProperty(UScriptStruct* ScriptStruct)
{
	_Ty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) _Ty(FObjectInitializer(), EC_CppProperty, 0, CPF_HasGetValueTypeHash);
	return Property;
}
#else 
template<class _Ty>
_Ty* NewUEProperty(UScriptStruct* ScriptStruct)
{
	_Ty* Property = new _Ty(ScriptStruct, NAME_None, RF_Transient, 0, CPF_HasGetValueTypeHash);
	return Property;
}
#endif 

FProperty  *NewUEBoolProperty(UScriptStruct* ScriptStruct)
{
#if ENGINE_MINOR_VERSION < 25
	// see overloaded operator new that defined in DECLARE_CLASS(...)
	UBoolProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) UBoolProperty(FObjectInitializer(), EC_CppProperty, 0, (EPropertyFlags)0, 0xFF, 1, true);
#else
	FBoolProperty* Property = new FBoolProperty(ScriptStruct, NAME_None, RF_Transient, 0, (EPropertyFlags)0, 0xFF, 1, true);
#endif
	return Property;
}

FProperty  *NewUEClassProperty(UClass *Class, UScriptStruct* ScriptStruct)
{
#if ENGINE_MINOR_VERSION < 25
	// see overloaded operator new that defined in DECLARE_CLASS(...)
	UObjectProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) UObjectProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_HasGetValueTypeHash, Class);
#else
	FObjectProperty* Property = new FObjectProperty(ScriptStruct, NAME_None, RF_Transient, 0, CPF_HasGetValueTypeHash, Class);
#endif
	return Property;
}

FArrayProperty  *NewUEArrayProperty(UScriptStruct* ScriptStruct)
{
#if ENGINE_MINOR_VERSION < 25
	FArrayProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) FArrayProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_None);
#else
	FArrayProperty* Property = new FArrayProperty(ScriptStruct, NAME_None, RF_Transient);
#endif
	return Property;
}

FMapProperty  *NewUEMapProperty(UScriptStruct* ScriptStruct)
{	
#if ENGINE_MINOR_VERSION < 25
	FMapProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) FMapProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_None);
#else
	FMapProperty* Property = new FMapProperty(ScriptStruct, NAME_None, RF_Transient);
#endif
	return Property;
}

UScriptStruct   *GScriptStruct = nullptr;

UScriptStruct   *GetGlbScriptStruct()
{
	if(!GScriptStruct)
	{
		GScriptStruct = FindObject<UScriptStruct>(ANY_PACKAGE, TEXT("PropertyCollector"));
	}
	return GScriptStruct;
}

FProperty  *CreateBaseProperty(FCInnerBaseType InBaseType)
{
	UScriptStruct* ScriptStruct = GetGlbScriptStruct();
	switch(InBaseType)
	{
		case FC_VALUE_TYPE_bool:
			return NewUEBoolProperty(ScriptStruct);
		case FC_VALUE_TYPE_CHAR:
			return NewUEProperty<FByteProperty>(ScriptStruct);
		case FC_VALUE_TYPE_BYTE:
			return NewUEProperty<FByteProperty>(ScriptStruct);
		case FC_VALUE_TYPE_WCHAR:
		case FC_VALUE_TYPE_USHORT:
			return NewUEProperty<FUInt16Property>(ScriptStruct);
		case FC_VALUE_TYPE_SHORT:
			return NewUEProperty<FInt16Property>(ScriptStruct);
		case FC_VALUE_TYPE_INT:
			return NewUEProperty<FIntProperty>(ScriptStruct);
		case FC_VALUE_TYPE_UINT:
			return NewUEProperty<FUInt32Property>(ScriptStruct);
		case FC_VALUE_TYPE_FLOAT:
			return NewUEProperty<FFloatProperty>(ScriptStruct);
		case FC_VALUE_TYPE_DOUBLE:
			return NewUEProperty<FDoubleProperty>(ScriptStruct);
		case FC_VALUE_TYPE_INT64:
			return NewUEProperty<FInt64Property>(ScriptStruct);
		case FC_VALUE_TYPE_UINT64:
			return NewUEProperty<FUInt64Property>(ScriptStruct);
		case FC_VALUE_TYPE_STRING_A:
		case FC_VALUE_TYPE_STRING_W:
			return NewUEProperty<FStrProperty>(ScriptStruct);
		case FC_VALUE_TYPE_VECTOR2:
			return CreateClassProperty("Vector2");
		case FC_VALUE_TYPE_VECTOR3:
			return CreateClassProperty("Vector3");
		case FC_VALUE_TYPE_VECTOR4:
			return CreateClassProperty("Vector4");
		case FC_VALUE_TYPE_COLOR:
			return CreateClassProperty("Color");
		case FC_VALUE_TYPE_COLOR32:
			return CreateClassProperty("Color32");
		default:
			break;
	}
	return nullptr;
}

typedef stdext::hash_map<int, FProperty*> CTemplatePropertyIDMap;
typedef stdext::hash_map<const char *, FProperty*> CTemplatePropertyNameMap;
CTemplatePropertyIDMap   GBasePropertyIDMap;
CTemplatePropertyNameMap GClassPropertyNameMap;

FProperty  *CreateClassProperty(const char *InClassName)
{
	CTemplatePropertyNameMap::iterator itProperty = GClassPropertyNameMap.find(InClassName);
	if(itProperty != GClassPropertyNameMap.end())
	{
		return itProperty->second;
	}
	const FCDynamicClassDesc *DynamicClass = GetScriptContext()->RegisterUClass(InClassName);
	if(!DynamicClass)
	{
		return nullptr;
	}
	if(!DynamicClass->m_Class)
	{
		return nullptr;
	}
	FProperty *Property = NewUEClassProperty(DynamicClass->m_Class, GetGlbScriptStruct());
	InClassName = DynamicClass->m_UEClassName.c_str();
	GClassPropertyNameMap[InClassName] = Property;
	return Property;

}

FProperty  *GetBaseProperty(FCInnerBaseType InBaseType)
{
	CTemplatePropertyIDMap::iterator itProperty = GBasePropertyIDMap.find(InBaseType);
	if(itProperty != GBasePropertyIDMap.end())
	{
		return itProperty->second;
	}
	FProperty  *Property = CreateBaseProperty(InBaseType);
	GBasePropertyIDMap[InBaseType] = Property;
	return Property;
}

FProperty *QueryTempalteProperty(fc_intptr VM, fc_intptr Ptr, int Index)
{
	int BaseType = fc_get_wrap_template_param_type(VM, Ptr, Index);

	FProperty *Property = nullptr;
	if(FC_VALUE_TYPE_INPORT_CLASS == BaseType)
	{
		fc_pcstr ClassName = fc_cpp_get_wrap_template_param_class_name(VM, Ptr, Index);
		Property = CreateClassProperty(ClassName);
	}
	else
	{
		Property = GetBaseProperty((FCInnerBaseType)BaseType);
	}
	return Property;
}

FArrayProperty* CreateTArrayProperty(fc_intptr VM, fc_intptr Ptr)
{
	FProperty *Property = QueryTempalteProperty(VM, Ptr, 0);
	if(!Property)
	{
		return nullptr;
	}
	FArrayProperty *ArrayProperty = NewUEArrayProperty(GetGlbScriptStruct());
	ArrayProperty->Inner = Property;	
	return ArrayProperty;
}

struct FCTArrayDynamicProperty : public FCDynamicProperty
{
	FArrayProperty  *ArrayProperty;
	FCTArrayDynamicProperty(FArrayProperty *InArrayProperty):ArrayProperty{InArrayProperty}
	{
	}
	~FCTArrayDynamicProperty()
	{
		if(ArrayProperty)
		{
			delete ArrayProperty;
		}
	}
};

typedef stdext::hash_map<int, FCDynamicProperty*> CTempalteDynamicPropertyMap;
CTempalteDynamicPropertyMap   GTempalteDynamicPropertyMap;

FCDynamicProperty* GetTArrayDynamicProperty(fc_intptr VM, fc_intptr Ptr)
{
	int TemplateID = fc_get_wrap_template_param_id(VM, Ptr);
	CTempalteDynamicPropertyMap::iterator itProperty = GTempalteDynamicPropertyMap.find(TemplateID);
	if(itProperty != GTempalteDynamicPropertyMap.end())
	{
		return itProperty->second;
	}
	FArrayProperty* ArrayProperty = CreateTArrayProperty(VM, Ptr);
	if(!ArrayProperty)
	{
		GTempalteDynamicPropertyMap[TemplateID] = nullptr;
		return nullptr;
	}
	FCTArrayDynamicProperty  *DynamicProperty = new FCTArrayDynamicProperty(ArrayProperty);
	DynamicProperty->InitProperty(ArrayProperty);
	GTempalteDynamicPropertyMap[TemplateID] = DynamicProperty;
	return DynamicProperty;
}

//---------------------------------------------------------------------

FMapProperty* CreateTMapProperty(fc_intptr VM, fc_intptr Ptr)
{
	FProperty *KeyProperty = QueryTempalteProperty(VM, Ptr, 0);
	FProperty *ValueProperty = QueryTempalteProperty(VM, Ptr, 1);
	if(!KeyProperty || !ValueProperty)
	{
		return nullptr;
	}
	FMapProperty  *MapProperty = NewUEMapProperty(GetGlbScriptStruct());
	MapProperty->KeyProp = KeyProperty;
	MapProperty->ValueProp = ValueProperty;

	int ValueSize = ValueProperty->ElementSize * ValueProperty->ArrayDim;
	int AlignKeySize = (KeyProperty->ElementSize + 7)/8*8;
	int AlignValueSize = (ValueSize + 7)/8*8;

	MapProperty->MapLayout = FScriptMap::GetScriptLayout(KeyProperty->ElementSize, AlignKeySize, ValueSize, AlignValueSize);

	return MapProperty;
}

struct FCTMapDynamicProperty : public FCDynamicProperty
{
	FMapProperty  *MapProperty;
	FCTMapDynamicProperty(FMapProperty *InMapProperty):MapProperty{InMapProperty}
	{
	}
	~FCTMapDynamicProperty()
	{
		if(MapProperty)
		{
			delete MapProperty;
		}
	}
};

FCDynamicProperty *GetTMapDynamicProperty(fc_intptr VM, fc_intptr Ptr)
{
	// 说明，由于TMap与TArray的参数不一样，所以不会存在相同的TemplateID, 这里共用一个模板列表吧
	int TemplateID = fc_get_wrap_template_param_id(VM, Ptr);
	CTempalteDynamicPropertyMap::iterator itProperty = GTempalteDynamicPropertyMap.find(TemplateID);
	if(itProperty != GTempalteDynamicPropertyMap.end())
	{
		return itProperty->second;
	}
	FMapProperty  *MapProperty = CreateTMapProperty(VM, Ptr);
	if(!MapProperty)
	{
		GTempalteDynamicPropertyMap[TemplateID] = nullptr;
		return nullptr;
	}
	FCTMapDynamicProperty  *DynamicProperty = new FCTMapDynamicProperty(MapProperty);
	DynamicProperty->InitProperty(MapProperty);
	GTempalteDynamicPropertyMap[TemplateID] = DynamicProperty;

	return DynamicProperty;
}

void ReleaseTempalteProperty()
{
	ReleasePtrMap(GTempalteDynamicPropertyMap);
	ReleasePtrMap(GBasePropertyIDMap);
	ReleasePtrMap(GClassPropertyNameMap);
	GScriptStruct = nullptr;
}

void TArray_Clear(FScriptArray *ScriptArray, FProperty *Inner)
{
	int ElementSize = Inner->GetSize();
	int Numb = ScriptArray->Num();

	uint8 *ObjAddr = (uint8 *)ScriptArray->GetData();
	uint8 *ValueAddr = ObjAddr;
	for (int32 Index = 0; Index < Numb; ++Index)
	{
		ValueAddr = ObjAddr + Index * ElementSize;
		Inner->DestroyValue(ValueAddr);
	}
	ScriptArray->Remove(0, Numb, ElementSize);
}

void TMap_Clear(FScriptMap* ScriptMap, FMapProperty* MapProperty)
{
	FProperty* KeyProp = MapProperty->KeyProp;
	FProperty* ValueProp = MapProperty->ValueProp;
	const FScriptMapLayout &MapLayout = MapProperty->MapLayout;

	int32  MaxIndex = ScriptMap->GetMaxIndex();
	for(int32 PairIndex = 0; PairIndex < MaxIndex; ++PairIndex)
	{
		if(ScriptMap->IsValidIndex(PairIndex))
		{
			uint8* PairPtr = (uint8*)ScriptMap->GetData(PairIndex, MapLayout);
			uint8* Result  = PairPtr + MapLayout.ValueOffset;
			KeyProp->DestroyValue(PairPtr);
			ValueProp->DestroyValue(Result);
		}
	}
	ScriptMap->Empty(0, MapLayout);
}
