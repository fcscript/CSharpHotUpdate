#include "FCSafeProperty.h"

FCSafeProperty::FCSafeProperty():Type(FCPropertyType::FCPROPERTY_Unkonw)
    , Property(nullptr)
    , PropertyPtr(nullptr)
    , ElementSize(0)
    , ArrayDim(1)
    , OwerStruct(nullptr)
    , PropertyStruct(nullptr)
{
    
}

void FCSafeProperty::InitProperty(const FProperty* InProperty)
{
    Property = InProperty;
    PropertyPtr = InProperty;
    ElementSize = InProperty->ElementSize;
    ArrayDim = InProperty->ArrayDim;
    OwerStruct = Property->GetOwnerStruct();
    uint64 CastFlags = InProperty->GetCastFlags();
    if (CASTCLASS_FStructProperty & CastFlags)
    {
        FStructProperty* StructProperty = (FStructProperty*)InProperty;
        PropertyStruct = StructProperty->Struct;
    }
}

bool FCSafeProperty::HasAnyPropertyFlags(uint64 FlagsToCheck) const
{
    return Property->HasAnyPropertyFlags(FlagsToCheck);
}

uint64 FCSafeProperty::GetCastFlags() const
{
    return Property->GetCastFlags();
}

UStruct* FCSafeProperty::GetOwnerStruct() const
{
    return OwerStruct;
}

UFunction* FCSafeProperty::GetSignatureFunction() const
{
    FC_ASSERT(true);
    return nullptr;
}

FStructProperty* FCSafeProperty::CastStructProperty() const
{
    //return (FStructProperty*)Property;
    FC_ASSERT(true);
    return nullptr;
}

FClassProperty* FCSafeProperty::CastClassProperty() const
{
    return (FClassProperty*)Property;
}

FObjectProperty* FCSafeProperty::CastObjectProperty() const
{
    FC_ASSERT(true);
    return nullptr;
}

FArrayProperty* FCSafeProperty::CastArrayProperty() const
{
    FC_ASSERT(true);
    return nullptr;
}

FMapProperty* FCSafeProperty::CastMapProperty() const
{
    FC_ASSERT(true);
    return nullptr;
}

FSetProperty* FCSafeProperty::CastSetProperty() const
{
    FC_ASSERT(true);
    return nullptr;
}

FDelegateProperty* FCSafeProperty::CastDelegateProperty() const
{
    FC_ASSERT(true);
    return nullptr;
}

UClass* FCSafeProperty::GetPropertyMetaClass() const
{
    FC_ASSERT(true);
    return nullptr;
}

UClass* FCSafeProperty::GetPropertyClass() const
{
    FC_ASSERT(true);
    return nullptr;
}

UStruct* FCSafeProperty::GetPropertyStruct() const
{
    return PropertyStruct;
}

int64 FCSafeProperty::GetEnumValueByName(const FName& InName) const
{
    FC_ASSERT(true);
    return 0;
}

bool FCSafeProperty::IsEnumProperty() const
{
    FC_ASSERT(true);
    return false;
}

void FCSafeProperty::InitializeValue(void* ValueAddr) const
{
    Property->InitializeValue(ValueAddr);
}

void FCSafeProperty::CopySingleValue(void* Dest, void const* Src) const
{
    Property->CopySingleValue(Dest, Src);
}

void FCSafeProperty::CopyValuesInternal(void* Dest, void const* Src, int32 Count) const
{
    FC_ASSERT(true);
}

void FCSafeProperty::CopyCompleteValue(void* Dest, void const* Src) const
{
    FC_ASSERT(true);
    Property->CopyCompleteValue(Dest, Src);
}

void FCSafeProperty::DestroyValue(void* Dest) const
{
    if(PropertyPtr.IsValid())
    {
        Property->DestroyValue(Dest);
    }
}

int FCSafeProperty::GetTemplateParamNameID() const
{
    return 0;
}

//----------------------------------------------------------

// bool, int8, int16, int, float, double
struct FCSafeProperty_BaseValue : public FCSafeProperty
{

};

struct FCSafeProperty_ByteProperty : public FCSafeProperty
{
    UEnum* PropertyEnum;
    FCSafeProperty_ByteProperty() :PropertyEnum(nullptr)
    {
    }
    void InitProperty(const FProperty* InProperty) override
    {
        FCSafeProperty::InitProperty(InProperty);
        FByteProperty* ByteProperty = (FByteProperty*)InProperty;
        PropertyEnum = ByteProperty->Enum;
    }
    int64 GetEnumValueByName(const FName& InName) const override
    {
        if(PropertyEnum)
            return PropertyEnum->GetValueByName(InName);
        return 0;
    }
    bool IsEnumProperty() const override
    {
        return PropertyEnum != nullptr;
    }
};

// Vector2, Vector3, Vector4
struct FCSafeProperty_VectorX : public FCSafeProperty
{
    FStructProperty* CastStructProperty() const override
    {
        if (PropertyPtr.IsValid())
        {
            return (FStructProperty*)Property;
        }
        else
        {
            return nullptr;
        }
    }
    void InitializeValue(void* ValueAddr) const override
    {
        FMemory::Memzero(ValueAddr, ElementSize);
    }
    void CopySingleValue(void* Dest, void const* Src) const override
    {
        FMemory::Memcpy(Dest, Src, ElementSize);
    }
    void CopyValuesInternal(void* Dest, void const* Src, int32 Count) const override
    {
        FMemory::Memcpy(Dest, Src, ElementSize * Count);
    }
    void CopyCompleteValue(void* Dest, void const* Src) const override
    {
        FMemory::Memcpy(Dest, Src, ElementSize);
    }
};

// 
struct FCSafeProperty_ScriptStruct : public FCSafeProperty
{

};

// FCPROPERTY_ObjectProperty
struct FCSafeProperty_ObjectProperty : public FCSafeProperty
{
    UClass* PropertyClass;
    FCSafeProperty_ObjectProperty():PropertyClass(nullptr)
    {
    }
    void InitProperty(const FProperty* InProperty) override
    {
        FCSafeProperty::InitProperty(InProperty);
        FObjectProperty* ObjectProperty = (FObjectProperty*)InProperty;
        PropertyClass = ObjectProperty->PropertyClass;
    }
    UClass* GetPropertyClass() const override
    {
        return PropertyClass;
    }
    FObjectProperty *CastObjectProperty() const override
    {
        return (FObjectProperty*)Property;        
    }
};

//
struct FCSafeProperty_ClassProperty : public FCSafeProperty
{
    UClass* MetaClass;
    FCSafeProperty_ClassProperty() :MetaClass(nullptr)
    {

    }
    void InitProperty(const FProperty* InProperty) override
    {
        FCSafeProperty::InitProperty(InProperty);
        FClassProperty* StructProperty = (FClassProperty*)InProperty;
        MetaClass = StructProperty->MetaClass;
    }
    FClassProperty* CastClassProperty() const override
    {
        return (FClassProperty*)Property;
    }
    UClass* GetPropertyMetaClass() const override
    {
        return MetaClass;
    }
};

struct FCSafeProperty_StructProperty : public FCSafeProperty
{
    //UStruct *PropertyStruct;
    FCSafeProperty_StructProperty()//:PropertyStruct(nullptr)
    {

    }
    void InitProperty(const FProperty* InProperty) override
    {
        FCSafeProperty::InitProperty(InProperty);
        FStructProperty *StructProperty = (FStructProperty *)InProperty;
        PropertyStruct = StructProperty->Struct;
    }
    FStructProperty* CastStructProperty() const override
    {
        if (PropertyPtr.IsValid())
        {
            return (FStructProperty*)Property;
        }
        else
        {
            return nullptr;
        }
    }
    UStruct* GetPropertyStruct() const override
    {
        return PropertyStruct;
    }
    void CopyValuesInternal(void* Dest, void const* Src, int32 Count) const override
    {        
        FStructProperty* StructProperty = (FStructProperty*)Property;
        StructProperty->CopyValuesInternal(Dest, Src, Count);
    }
};

struct FCSafeProperty_Delegate : public FCSafeProperty
{
    UFunction* SignatureFunction;
    FCSafeProperty_Delegate():SignatureFunction(nullptr)
    {

    }

    void InitProperty(const FProperty* InProperty) override
    {
        FCSafeProperty::InitProperty(InProperty);

        switch (Type)
        {
        case FCPropertyType::FCPROPERTY_MulticastDelegateProperty:
        {
            FMulticastDelegateProperty* DelegateProperty = (FMulticastDelegateProperty*)Property;
            SignatureFunction = DelegateProperty->SignatureFunction;
        }
        break;
        case FCPropertyType::FCPROPERTY_DelegateProperty:
        {
            FDelegateProperty* DelegateProperty = (FDelegateProperty*)Property;
            SignatureFunction = DelegateProperty->SignatureFunction;
        }
        break;
        case FCPropertyType::FCPROPERTY_MulticastSparseDelegateProperty:
        {
            FMulticastSparseDelegateProperty* DelagateProperty = (FMulticastSparseDelegateProperty*)Property;
            SignatureFunction = DelagateProperty->SignatureFunction;
        }
        break;
        default:
            break;
        }        
    }
    UFunction* GetSignatureFunction() const override
    {
        return SignatureFunction;
    }
    UStruct* GetPropertyStruct() const override
    {
        return SignatureFunction;
    }
    FDelegateProperty* CastDelegateProperty() const override
    {
        return (FDelegateProperty*)Property;
    }
    void CopyValuesInternal(void* Dest, void const* Src, int32 Count) const override
    {
        FC_ASSERT(Count != 1);
        FStructProperty* StructProperty = ((FStructProperty*)Property);
        StructProperty->CopyValuesInternal(Dest, Src, Count);
        //Property->CopySingleValue(Dest, Src);
    }
};

struct FCSafeProperty_Array : public FCSafeProperty
{
    int  ParamsNameID = 0;
    FArrayProperty* CastArrayProperty() const override
    {
        return (FArrayProperty*)Property;
    }
    virtual void InitProperty(const FProperty* InProperty) override
    {
        FCSafeProperty::InitProperty(InProperty);
        FArrayProperty* ArrayProperty = (FArrayProperty*)Property;
        ParamsNameID = GetMapTemplateParamNameID(ArrayProperty->Inner, ArrayProperty->Inner);
    }
    void CopyValuesInternal(void* Dest, void const* Src, int32 Count) const override
    {
        FArrayProperty *ArrayProperty = (FArrayProperty *)Property;
        ArrayProperty->CopyValuesInternal(Dest, Src, Count);
    }
    int GetTemplateParamNameID() const override
    {
        return ParamsNameID;
    }
};

struct FCSafeProperty_Map : public FCSafeProperty
{
    int  ParamsNameID = 0;
    virtual void InitProperty(const FProperty* InProperty) override
    {
        FCSafeProperty::InitProperty(InProperty);
        FMapProperty* MapProperty = (FMapProperty*)Property;
        ParamsNameID = GetMapTemplateParamNameID(MapProperty->KeyProp, MapProperty->ValueProp);
    }

    FMapProperty* CastMapProperty() const override
    {
        return (FMapProperty*)Property;
    }
    void CopyCompleteValue(void* Dest, void const* Src) const override
    {
        FMapProperty *MapProperty = (FMapProperty *)Property;
        MapProperty->CopyCompleteValue(Dest, Src);
    }
    void CopyValuesInternal(void* Dest, void const* Src, int32 Count) const override
    {
        FMapProperty* MapProperty = (FMapProperty*)Property;
        MapProperty->CopyValuesInternal(Dest, Src, Count);
    }

    int GetTemplateParamNameID() const override
    {
        return ParamsNameID;
    }
};

struct FCSafeProperty_Set : public FCSafeProperty
{
    int  ParamsNameID = 0;
    FSetProperty* CastSetProperty() const override
    {
        return (FSetProperty*)Property;
    }
    virtual void InitProperty(const FProperty* InProperty) override
    {
        FCSafeProperty::InitProperty(InProperty);
        FSetProperty* SetProperty = (FSetProperty*)Property;
        ParamsNameID = GetMapTemplateParamNameID(SetProperty->ElementProp, SetProperty->ElementProp);
    }
    void CopyCompleteValue(void* Dest, void const* Src) const override
    {
        FSetProperty* SetProperty = (FSetProperty*)Property;
        SetProperty->CopyCompleteValue(Dest, Src);
    }
    void CopyValuesInternal(void* Dest, void const* Src, int32 Count) const override
    {
        FSetProperty* SetProperty = (FSetProperty*)Property;
        SetProperty->CopyValuesInternal(Dest, Src, Count);
    }
    int GetTemplateParamNameID() const override
    {
        return ParamsNameID;
    }
};

FCSafeProperty  *CreateSafeProperty(FCPropertyType Type)
{
    switch(Type)
    {
        case FCPROPERTY_BoolProperty:
        case FCPROPERTY_Int8Property:
        case FCPROPERTY_Int16Property:
        case FCPROPERTY_IntProperty:
        case FCPROPERTY_UInt32Property:
        case FCPROPERTY_Int64Property:
        case FCPROPERTY_UInt64Property:
        case FCPROPERTY_FloatProperty:
        case FCPROPERTY_DoubleProperty:
        case FCPROPERTY_NumericProperty:
            return new FCSafeProperty_BaseValue();
        case FCPROPERTY_ByteProperty:
            return new FCSafeProperty_ByteProperty;

        case FCPROPERTY_Vector2:
        case FCPROPERTY_Vector3:
        case FCPROPERTY_Vector4:
            return new FCSafeProperty_VectorX();
        case FCPROPERTY_ScriptStruct:
            return new FCSafeProperty_ScriptStruct();

        case FCPROPERTY_ObjectProperty:
            return new FCSafeProperty_ObjectProperty();
        case FCPROPERTY_ClassProperty:
            return new FCSafeProperty_ClassProperty();

        case FCPROPERTY_StructProperty:
            return new FCSafeProperty_StructProperty();
        case FCPROPERTY_DelegateProperty:
        case FCPROPERTY_MulticastDelegateProperty:
        case FCPROPERTY_MulticastSparseDelegateProperty:
            return new FCSafeProperty_Delegate();
        case FCPROPERTY_Array:
            return new FCSafeProperty_Array();
        case FCPROPERTY_Map:
            return new FCSafeProperty_Map();
        case FCPROPERTY_Set:
            return new FCSafeProperty_Set();
        default:
            break;
    }
    return new FCSafeProperty();
}

//----------------------------------------------------------

typedef  std::unordered_map<const FProperty*, FCSafeProperty*>   CSafeProperyMap; // FProperty* ==> FCSafeProperty

CSafeProperyMap  GSafePropertyMap;

FCSafeProperty* GetSafeProperty(const FProperty* InProperty)
{
    CSafeProperyMap::iterator itProperty = GSafePropertyMap.find(InProperty);
    if(itProperty != GSafePropertyMap.end())
    {
        return itProperty->second;
    }
    FCPropertyType Type = GetScriptPropertyType(InProperty);
    FCSafeProperty  *SafeProperty = CreateSafeProperty(Type);
    SafeProperty->Type = Type;
    SafeProperty->InitProperty(InProperty);
    GSafePropertyMap[InProperty] = SafeProperty;
    return SafeProperty;
}

void ClearAllSafeProperty()
{
    ReleasePtrMap(GSafePropertyMap);
}

void ClearAllNoneRefProperty()
{
    for(CSafeProperyMap::iterator itProperty = GSafePropertyMap.begin(); itProperty != GSafePropertyMap.end();)
    {
        FCSafeProperty* SafeProperty = itProperty->second;
        if(!IsRefPtr(SafeProperty) && !SafeProperty->IsValid())
        {
            itProperty = GSafePropertyMap.erase(itProperty);
            delete SafeProperty;
        }
        else
        {
            ++itProperty;
        }
    }    
}

typedef  std::unordered_map<const void*, bool>   CPtrRefFlagsMap; // const void* ==> bool
CPtrRefFlagsMap  GPtrRefFlagsMap;
void SetPtrRefFlag(const void* Ptr)
{
    if(Ptr)
    {
        GPtrRefFlagsMap[Ptr] = true;
    }
}

bool IsRefPtr(const void* Ptr)
{
    return GPtrRefFlagsMap.find(Ptr) != GPtrRefFlagsMap.end();
}

void ClearAllPtrRefFlag()
{
    GPtrRefFlagsMap.clear();
}
