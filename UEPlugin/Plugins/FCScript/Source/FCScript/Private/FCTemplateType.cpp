#include "FCTemplateType.h"

template<class _Ty>
_Ty* NewUEProperty(UScriptStruct* ScriptStruct)
{
#if OLD_UE_ENGINE
    _Ty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) _Ty(FObjectInitializer(), EC_CppProperty, 0, CPF_HasGetValueTypeHash);
#elif ENGINE_MAJOR_VERSION >= 5 
    _Ty* Property = new _Ty(ScriptStruct, NAME_None, RF_Transient);
    Property->PropertyFlags = CPF_ZeroConstructor | CPF_IsPlainOldData | CPF_NoDestructor | CPF_HasGetValueTypeHash;
#else
	_Ty* Property = new _Ty(ScriptStruct, NAME_None, RF_Transient, 0, CPF_HasGetValueTypeHash);
#endif
    return Property;
}

FProperty  *NewUEBoolProperty(UScriptStruct* ScriptStruct)
{
#if OLD_UE_ENGINE
	// see overloaded operator new that defined in DECLARE_CLASS(...)
	UBoolProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) UBoolProperty(FObjectInitializer(), EC_CppProperty, 0, (EPropertyFlags)0, 0xFF, 1, true);
#elif ENGINE_MAJOR_VERSION >= 5 
    UECodeGen_Private::FBoolPropertyParams Params;
    FMemory::Memzero(&Params, sizeof(Params));
    Params.PropertyFlags = CPF_None;
    Params.Flags = UECodeGen_Private::EPropertyGenFlags::Bool | UECodeGen_Private::EPropertyGenFlags::NativeBool;
    Params.ObjectFlags = RF_Transient;
    Params.ArrayDim = 1;
    Params.ElementSize = sizeof(bool);
    Params.SizeOfOuter = sizeof(ScriptStruct);

    //constexpr auto Params = UECodeGen_Private::FBoolPropertyParams
    //{
    //    nullptr,
    //    nullptr,
    //    CPF_None,
    //    UECodeGen_Private::EPropertyGenFlags::Bool | UECodeGen_Private::EPropertyGenFlags::NativeBool,
    //    RF_Transient,
    //    #if ENGINE_MINOR_VERSION > 2
    //    nullptr, nullptr, 1, sizeof(bool), sizeof(ScriptStruct), nullptr,
    //    METADATA_PARAMS(0, nullptr)
    //    #else
    //    1, nullptr, nullptr, sizeof(bool), sizeof(ScriptStruct), nullptr,
    //    METADATA_PARAMS(nullptr, 0)
    //    #endif
    //};
    FBoolProperty* Property = new FBoolProperty(ScriptStruct, Params);
#else
	FBoolProperty* Property = new FBoolProperty(ScriptStruct, NAME_None, RF_Transient, 0, (EPropertyFlags)0, 0xFF, 1, true);
#endif
    return Property;
}

FProperty* NewUEStructProperty(UScriptStruct* Struct, UScriptStruct* ScriptStruct)
{
#if OLD_UE_ENGINE
	// see overloaded operator new that defined in DECLARE_CLASS(...)
    UStructProperty *Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) UStructProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_HasGetValueTypeHash, Struct);
#elif ENGINE_MAJOR_VERSION >= 5 
    UECodeGen_Private::FStructPropertyParams Params;
    FMemory::Memzero(&Params, sizeof(Params));
    Params.PropertyFlags = ScriptStruct->GetCppStructOps()
        ? ScriptStruct->GetCppStructOps()->GetComputedPropertyFlags() | CPF_HasGetValueTypeHash
        : CPF_HasGetValueTypeHash;
    Params.Flags = UECodeGen_Private::EPropertyGenFlags::Struct;
    Params.ObjectFlags = RF_Transient;
    Params.ArrayDim = 1;
    Params.Offset = 0;

    //const auto Params = UECodeGen_Private::FStructPropertyParams
    //{
    //    nullptr,
    //    nullptr,
    //    ScriptStruct->GetCppStructOps()
    //        ? ScriptStruct->GetCppStructOps()->GetComputedPropertyFlags() | CPF_HasGetValueTypeHash
    //        : CPF_HasGetValueTypeHash,
    //    UECodeGen_Private::EPropertyGenFlags::Struct,
    //    RF_Transient,

    //    #if ENGINE_MINOR_VERSION > 2
    //    nullptr, nullptr, 1, 0, nullptr,
    //    METADATA_PARAMS(0, nullptr)
    //    #else
    //    1, nullptr, nullptr, 0, nullptr,
    //    METADATA_PARAMS(nullptr, 0)
    //    #endif
    //};
    FStructProperty* Property = new FStructProperty(ScriptStruct, Params);
    Property->Struct = Struct;
    Property->ElementSize = Struct->PropertiesSize;
#else
	FStructProperty* Property = new FStructProperty(ScriptStruct, NAME_None, RF_Transient, 0, CPF_HasGetValueTypeHash, Struct);
#endif
	return Property;
}

FProperty  *NewUEClassProperty(UClass *Class, UScriptStruct* ScriptStruct)
{
#if OLD_UE_ENGINE
	// see overloaded operator new that defined in DECLARE_CLASS(...)
    //UObjectProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) UObjectProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_HasGetValueTypeHash, Class);
    UClassProperty *Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) UClassProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_HasGetValueTypeHash | CPF_UObjectWrapper, Class, nullptr);
#elif ENGINE_MAJOR_VERSION >= 5 
    UECodeGen_Private::FObjectPropertyParams Params;
    FMemory::Memzero(&Params, sizeof(Params));
    Params.PropertyFlags = CPF_HasGetValueTypeHash;
    Params.Flags = UECodeGen_Private::EPropertyGenFlags::Object;
    Params.ObjectFlags = RF_Transient;
    Params.ArrayDim = 1;

    //constexpr auto Params = UECodeGen_Private::FObjectPropertyParams
    //{
    //    nullptr,
    //    nullptr,
    //    CPF_HasGetValueTypeHash,
    //    UECodeGen_Private::EPropertyGenFlags::Object,
    //    RF_Transient,
    //    #if ENGINE_MINOR_VERSION > 2
    //    nullptr, nullptr, 1, 0, nullptr,
    //    METADATA_PARAMS(0, nullptr)
    //    #else
    //    1, nullptr, nullptr, 0, nullptr,
    //    METADATA_PARAMS(nullptr, 0)
    //    #endif
    //};
    FObjectProperty* Property = new FObjectProperty(ScriptStruct, Params);
    Property->PropertyClass = Class;
#else
	FObjectProperty* Property = new FObjectProperty(ScriptStruct, NAME_None, RF_Transient, 0, CPF_HasGetValueTypeHash, Class);
#endif
	return Property;
}

FArrayProperty  *NewUEArrayProperty(UScriptStruct* ScriptStruct)
{
#if OLD_UE_ENGINE
	FArrayProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) FArrayProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_None);
#else
	FArrayProperty* Property = new FArrayProperty(ScriptStruct, NAME_None, RF_Transient);
#endif
	return Property;
}

FMapProperty  *NewUEMapProperty(UScriptStruct* ScriptStruct)
{	
#if OLD_UE_ENGINE
	FMapProperty* Property = new (EC_InternalUseOnlyConstructor, ScriptStruct, NAME_None, RF_Transient) FMapProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_None);
#else
	FMapProperty* Property = new FMapProperty(ScriptStruct, NAME_None, RF_Transient);
#endif
	return Property;
}

FSetProperty* NewUESetProperty(UScriptStruct* ScriptStruct)
{
#if OLD_UE_ENGINE
	FSetProperty* Property = new (EC_InternalUseOnlyConstructor, (UClass*)ScriptStruct, NAME_None, RF_Transient) FSetProperty(FObjectInitializer(), EC_CppProperty, 0, CPF_None);
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
		GScriptStruct = FindObject<UScriptStruct>(ANY_PACKAGE, TEXT("FCScriptPropertyCollector"));
	}
	return GScriptStruct;
}

FProperty* CreateBaseProperty_bool(const char * InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEBoolProperty(ScriptStruct);
}
FProperty* CreateBaseProperty_char(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FByteProperty>(ScriptStruct);
}
FProperty* CreateBaseProperty_byte(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FByteProperty>(ScriptStruct);
}
FProperty* CreateBaseProperty_ushort(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FUInt16Property>(ScriptStruct);
}
FProperty* CreateBaseProperty_short(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FInt16Property>(ScriptStruct);
}
FProperty* CreateBaseProperty_int(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FIntProperty>(ScriptStruct);
}
FProperty* CreateBaseProperty_uint(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FUInt32Property>(ScriptStruct);
}
FProperty* CreateBaseProperty_float(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FFloatProperty>(ScriptStruct);
}
FProperty* CreateBaseProperty_double(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FDoubleProperty>(ScriptStruct);
}
FProperty* CreateBaseProperty_int64(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FInt64Property>(ScriptStruct);
}
FProperty* CreateBaseProperty_uint64(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FUInt64Property>(ScriptStruct);
}
FProperty* CreateBaseProperty_StringA(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FNameProperty>(ScriptStruct);
}
FProperty* CreateBaseProperty_StringW(const char* InClassName)
{
    UScriptStruct* ScriptStruct = GetGlbScriptStruct();
    return NewUEProperty<FStrProperty>(ScriptStruct);
}

typedef FProperty* (*LPCreateBasePropertyFunc)(const char *);

typedef std::unordered_map<const char *, FProperty*, FCStringHash, FCStringEqual> CTemplatePropertyNameMap;
typedef std::unordered_map<UStruct*, FCDynamicProperty*> CStructDynamicPropertyMap;
typedef std::unordered_map<FProperty*, FCDynamicProperty*> CPropertyDynamicPropertyMap;
typedef std::unordered_map<const char*, FCDynamicProperty*, FCStringHash, FCStringEqual> CCppDynamicPropertyMap;
typedef std::unordered_map<const char*, LPCreateBasePropertyFunc, FCStringHash, FCStringEqual> CCppCreateBasePropertyMap;

CTemplatePropertyNameMap GClassPropertyNameMap;
CStructDynamicPropertyMap GStructDynamicPropertyMap;
CPropertyDynamicPropertyMap GPropertyDynamicPropertyMap;
CCppDynamicPropertyMap      GCppDynamicPropertyMap;
CCppCreateBasePropertyMap   GCppBasePropertyCreatorMap;

void  InitBasePropertyCreator()
{
    if(GCppBasePropertyCreatorMap.size() > 0)
    {
        return ;
    }
    GCppBasePropertyCreatorMap["bool"] = CreateBaseProperty_bool;
    GCppBasePropertyCreatorMap["char"] = CreateBaseProperty_char;
    GCppBasePropertyCreatorMap["byte"] = CreateBaseProperty_byte;
    GCppBasePropertyCreatorMap["short"] = CreateBaseProperty_short;
    GCppBasePropertyCreatorMap["ushort"] = CreateBaseProperty_ushort;
    GCppBasePropertyCreatorMap["wchar"] = CreateBaseProperty_ushort;
    GCppBasePropertyCreatorMap["int"] = CreateBaseProperty_int;
    GCppBasePropertyCreatorMap["uint"] = CreateBaseProperty_uint;
    GCppBasePropertyCreatorMap["float"] = CreateBaseProperty_float;
    GCppBasePropertyCreatorMap["double"] = CreateBaseProperty_double;
    GCppBasePropertyCreatorMap["int64"] = CreateBaseProperty_int64;
    GCppBasePropertyCreatorMap["uint64"] = CreateBaseProperty_uint64;
    GCppBasePropertyCreatorMap["StringA"] = CreateBaseProperty_StringA;
    GCppBasePropertyCreatorMap["StringW"] = CreateBaseProperty_StringW;
}

FProperty  *CreateClassProperty(const char *InClassName)
{
	CTemplatePropertyNameMap::iterator itProperty = GClassPropertyNameMap.find(InClassName);
	if(itProperty != GClassPropertyNameMap.end())
	{
		return itProperty->second;
	}
    // 基础数据类型, bool, char, byte, short, ushort, int, uint
    InitBasePropertyCreator();
    CCppCreateBasePropertyMap::iterator itInner = GCppBasePropertyCreatorMap.find(InClassName);
    if (itInner != GCppBasePropertyCreatorMap.end())
    {
        InClassName = itInner->first;
        FProperty* Property = itInner->second(InClassName);
        GClassPropertyNameMap[InClassName] = Property;
        return Property;
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
			FProperty* Property = NewUEStructProperty((UScriptStruct *)DynamicClass->m_Struct, GetGlbScriptStruct());
			InClassName = DynamicClass->m_UEClassName;
			GClassPropertyNameMap[InClassName] = Property;
			return Property;
		}
		return nullptr;
	}
	FProperty *Property = NewUEClassProperty(DynamicClass->m_Class, GetGlbScriptStruct());
	InClassName = DynamicClass->m_UEClassName;
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
    DynamicPropery->Name = GetConstName(DynamicPropery->Name);
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
        DynamicPropery->Name = GetConstName(DynamicPropery->Name);
	}
	GPropertyDynamicPropertyMap[InProperty] = DynamicPropery;
	return DynamicPropery;
}

FProperty *QueryTempalteProperty(fc_intptr VM, fc_intptr Ptr, int Index)
{
    fc_pcstr ClassName = fc_cpp_get_wrap_template_param_class_name(VM, Ptr, Index);
    if(!ClassName)
    {
        return nullptr;
    }
	FProperty *Property = CreateClassProperty(ClassName);
	return Property;
}

FCDynamicProperty* GetDynamicPropertyByCppType(FCPropertyType InType, const char* InClassName, int InElementSize)
{
    CCppDynamicPropertyMap::iterator itProperty = GCppDynamicPropertyMap.find(InClassName);
    if (itProperty != GCppDynamicPropertyMap.end())
    {
        return itProperty->second;
    }
    FCDynamicProperty* DynamicPropery = new FCDynamicProperty();
    DynamicPropery->InitCppType(InType, InClassName, InElementSize);
    GCppDynamicPropertyMap[InClassName] = DynamicPropery;
    return DynamicPropery;
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
			//delete ArrayProperty;
		}
	}
};

typedef std::unordered_map<int, FCDynamicProperty*> CTempalteDynamicPropertyMap;
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
			// delete MapProperty;
		}
	}
};

FCDynamicProperty *GetTMapDynamicProperty(fc_intptr VM, fc_intptr Ptr)
{
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
            //delete SetProperty; //
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
	//ReleasePtrMap(GClassPropertyNameMap);

    GClassPropertyNameMap.clear();

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
    ScriptArray_Remove(ScriptArray, 0, Numb, ElementSize);
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
