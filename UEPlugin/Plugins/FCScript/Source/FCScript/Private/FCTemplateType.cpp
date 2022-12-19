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

FProperty* NewUEStructProperty(UScriptStruct* Struct, UScriptStruct* ScriptStruct)
{
#if ENGINE_MINOR_VERSION < 25
	// see overloaded operator new that defined in DECLARE_CLASS(...)
	FStructProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) UObjectProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_HasGetValueTypeHash, Struct);
#else
	FStructProperty* Property = new FStructProperty(ScriptStruct, NAME_None, RF_Transient, 0, CPF_HasGetValueTypeHash, Struct);
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

FSetProperty* NewUESetProperty(UScriptStruct* ScriptStruct)
{
#if ENGINE_MINOR_VERSION < 25
	FSetProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) FMapProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_None);
#else
	FSetProperty* Property = new FSetProperty(ScriptStruct, NAME_None, RF_Transient);
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
			return NewUEProperty<FNameProperty>(ScriptStruct);
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
typedef stdext::hash_map<UStruct*, FCDynamicProperty*> CStructDynamicPropertyMap;
typedef stdext::hash_map<FProperty*, FCDynamicProperty*> CPropertyDynamicPropertyMap;
typedef stdext::hash_map<const char*, FCDynamicProperty*> CCppDynamicPropertyMap;
CTemplatePropertyIDMap   GBasePropertyIDMap;
CTemplatePropertyNameMap GClassPropertyNameMap;
CStructDynamicPropertyMap GStructDynamicPropertyMap;
CPropertyDynamicPropertyMap GPropertyDynamicPropertyMap;
CCppDynamicPropertyMap      GCppDynamicPropertyMap;

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
		FProperty* Property = nullptr;
		if(strcmp(InClassName, "FString") == 0)
		{
			Property = NewUEProperty<FStrProperty>(GetGlbScriptStruct());
			GClassPropertyNameMap["FString"] = Property;
		}
		else if (strcmp(InClassName, "FName") == 0)
		{
			Property = NewUEProperty<FNameProperty>(GetGlbScriptStruct());
			GClassPropertyNameMap["FName"] = Property;
		}
		return Property;
	}
	if(!DynamicClass->m_Class)
	{
		if (DynamicClass->m_Struct)
		{
			// 注明一下，这里的Struct一定是UScriptStruct
			FProperty* Property = NewUEStructProperty((UScriptStruct *)DynamicClass->m_Struct, GetGlbScriptStruct());
			InClassName = DynamicClass->m_UEClassName.c_str();
			GClassPropertyNameMap[InClassName] = Property;
			return Property;
		}
		return nullptr;
	}
	FProperty *Property = NewUEClassProperty(DynamicClass->m_Class, GetGlbScriptStruct());
	InClassName = DynamicClass->m_UEClassName.c_str();
	GClassPropertyNameMap[InClassName] = Property;
	return Property;
}

FCDynamicProperty* GetCppDynamicProperty(const char* InClassName)
{
	CCppDynamicPropertyMap::iterator itProperty = GCppDynamicPropertyMap.find(InClassName);
	if(itProperty != GCppDynamicPropertyMap.end())
	{
		return itProperty->second;
    }
	FProperty* Property = CreateClassProperty(InClassName);
	if(Property)
    {
        FCDynamicProperty* DynamicPropery = new FCDynamicProperty();
		DynamicPropery->InitProperty(Property);
		GCppDynamicPropertyMap[InClassName] = DynamicPropery;
		return DynamicPropery;
	}
	else
	{
		GCppDynamicPropertyMap[InClassName] = nullptr;
		return nullptr;
	}
}

FCDynamicProperty* GetStructDynamicProperty(UStruct* Struct)
{
	CStructDynamicPropertyMap::iterator itProperty = GStructDynamicPropertyMap.find(Struct);
	if (itProperty != GStructDynamicPropertyMap.end())
	{
		return itProperty->second;
	}
	FCDynamicProperty* DynamicPropery = new FCDynamicProperty();
	FProperty* Property = NewUEStructProperty((UScriptStruct*)Struct, GetGlbScriptStruct());
	DynamicPropery->InitProperty(Property);
	DynamicPropery->Name = TCHAR_TO_UTF8(*(Struct->GetName()));
	GStructDynamicPropertyMap[Struct] = DynamicPropery;
	return DynamicPropery;
}

FCDynamicProperty* GetDynamicPropertyByUEProperty(FProperty* InProperty)
{
	CPropertyDynamicPropertyMap::iterator itProperty = GPropertyDynamicPropertyMap.find(InProperty);
	if (itProperty != GPropertyDynamicPropertyMap.end())
	{
		return itProperty->second;
	}
	FCDynamicProperty* DynamicPropery = new FCDynamicProperty();
	DynamicPropery->InitProperty(InProperty);
	if(FCPropertyType::FCPROPERTY_StructProperty == DynamicPropery->Type)
	{
		FStructProperty* StructProperty = (FStructProperty*)InProperty;
		UStruct* Struct = StructProperty->Struct;
		DynamicPropery->Name = TCHAR_TO_UTF8(*(Struct->GetName()));
	}
	GPropertyDynamicPropertyMap[InProperty] = DynamicPropery;
	return DynamicPropery;
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
	FCTArrayDynamicProperty(FArrayProperty *InArrayProperty):ArrayProperty(InArrayProperty)
	{
	}
	~FCTArrayDynamicProperty()
	{
		if(ArrayProperty)
		{
			//delete ArrayProperty;  // UE会有GC，不要删除吧
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
    int AlignKeySize = KeyProperty->GetMinAlignment();
    int AlignValueSize = ValueProperty->GetMinAlignment();

	MapProperty->MapLayout = FScriptMap::GetScriptLayout(KeyProperty->ElementSize, AlignKeySize, ValueSize, AlignValueSize);

	return MapProperty;
}

struct FCTMapDynamicProperty : public FCDynamicProperty
{
	FMapProperty  *MapProperty;
	FCTMapDynamicProperty(FMapProperty *InMapProperty):MapProperty(InMapProperty)
	{
	}
	~FCTMapDynamicProperty()
	{
		if(MapProperty)
		{
			// delete MapProperty; // UE会有GC，不要删除吧
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

struct FCTSetDynamicProperty : public FCDynamicProperty
{
    FSetProperty* SetProperty;
	FCTSetDynamicProperty(FSetProperty* InSetProperty) :SetProperty(InSetProperty)
    {		
    }
    ~FCTSetDynamicProperty()
    {
        if (SetProperty)
        {
            //delete SetProperty; // UE会有GC，不要删除吧
        }
    }
};

//-----------------------------------------------
FSetProperty* CreateTSetProperty(fc_intptr VM, fc_intptr Ptr)
{
    FProperty* ElementProp = QueryTempalteProperty(VM, Ptr, 0);
    if (!ElementProp)
    {
        return nullptr;
    }
    FSetProperty* SetProperty = NewUESetProperty(GetGlbScriptStruct());
	SetProperty->ElementProp = ElementProp;

    int ValueSize = ElementProp->ElementSize * ElementProp->ArrayDim;
    int AlignValueSize = ElementProp->GetMinAlignment();

	SetProperty->SetLayout = FScriptSet::GetScriptLayout(ValueSize, AlignValueSize);

    return SetProperty;
}

FCDynamicProperty* GetTSetDynamicProperty(fc_intptr VM, fc_intptr Ptr)
{
    // 说明，由于TMap与TArray的参数不一样，所以不会存在相同的TemplateID, 这里共用一个模板列表吧
    int TemplateID = fc_get_wrap_template_param_id(VM, Ptr);
    CTempalteDynamicPropertyMap::iterator itProperty = GTempalteDynamicPropertyMap.find(TemplateID);
    if (itProperty != GTempalteDynamicPropertyMap.end())
    {
        return itProperty->second;
    }
    FSetProperty* SetProperty = CreateTSetProperty(VM, Ptr);
    if (!SetProperty)
    {
        GTempalteDynamicPropertyMap[TemplateID] = nullptr;
        return nullptr;
    }
    FCTSetDynamicProperty* DynamicProperty = new FCTSetDynamicProperty(SetProperty);
    DynamicProperty->InitProperty(SetProperty);
    GTempalteDynamicPropertyMap[TemplateID] = DynamicProperty;

    return DynamicProperty;
}

void ReleaseTempalteProperty()
{
	// 说明：UProperty对象不能释放，这个只能是全局管理的，由UE释放
	//ReleasePtrMap(GBasePropertyIDMap);
	//ReleasePtrMap(GClassPropertyNameMap);

    GBasePropertyIDMap.clear(); // UE会自动释放，所以不能留
    GClassPropertyNameMap.clear(); // UE会自动释放，所以不能留

	ReleasePtrMap(GTempalteDynamicPropertyMap);
	ReleasePtrMap(GStructDynamicPropertyMap);
	ReleasePtrMap(GPropertyDynamicPropertyMap);
	ReleasePtrMap(GCppDynamicPropertyMap);

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

void TSet_Clear(FScriptSet* ScriptMap, FSetProperty* SetProperty)
{
    FProperty* ElementProp = SetProperty->ElementProp;
    const FScriptSetLayout& SetLayout = SetProperty->SetLayout;
    int32  MaxIndex = ScriptMap->GetMaxIndex();
    for (int32 PairIndex = 0; PairIndex < MaxIndex; ++PairIndex)
    {
        if (ScriptMap->IsValidIndex(PairIndex))
        {
            uint8* Result = (uint8*)ScriptMap->GetData(PairIndex, SetLayout);
			ElementProp->DestroyValue(Result);
        }
    }
    ScriptMap->Empty(0, SetLayout);
}
