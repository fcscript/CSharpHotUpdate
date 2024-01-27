#pragma once
#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "CoreUObject.h"
#include "FCStringCore.h"
#include "FCPropertyType.h"

struct FCSafeProperty
{
    FCPropertyType    Type;       // 类型
    const FProperty* Property;
    TWeakFieldPtr<FProperty> PropertyPtr;
    int ElementSize;
    int ArrayDim;
    UStruct  *OwerStruct;
    UStruct* PropertyStruct;

    FCSafeProperty();
    virtual ~FCSafeProperty(){}
    bool IsValid() const
    {
        return PropertyPtr.IsValid();
    }
    virtual void InitProperty(const FProperty* InProperty);

    bool HasAnyPropertyFlags(uint64 FlagsToCheck) const;
    uint64 GetCastFlags() const;
    const FProperty * GetProperty() const
    {
        return Property;
    }

    virtual UStruct *GetOwnerStruct() const;
    virtual UFunction* GetSignatureFunction() const;
    virtual FStructProperty *CastStructProperty() const;
    virtual FClassProperty *CastClassProperty() const;
    virtual FObjectProperty *CastObjectProperty() const;
    virtual FArrayProperty *CastArrayProperty() const;
    virtual FMapProperty *CastMapProperty() const;
    virtual FSetProperty *CastSetProperty() const;
    virtual FDelegateProperty* CastDelegateProperty() const;
    virtual UClass *GetPropertyMetaClass() const;
    virtual UClass *GetPropertyClass() const;
    virtual UStruct *GetPropertyStruct() const;
    virtual int64 GetEnumValueByName(const FName &InName) const;
    virtual bool IsEnumProperty() const;
    virtual void InitializeValue(void* ValueAddr) const ;
    virtual void CopySingleValue(void* Dest, void const* Src) const;
    virtual void CopyValuesInternal(void* Dest, void const* Src, int32 Count) const;
    virtual void CopyCompleteValue(void* Dest, void const* Src) const;
    virtual void DestroyValue(void* Dest) const;
    virtual int GetTemplateParamNameID() const;
};

FCSafeProperty  *GetSafeProperty(const FProperty* InProperty);
void ClearAllSafeProperty();
void ClearAllNoneRefProperty();
void SetPtrRefFlag(const void *Ptr);
bool IsRefPtr(const void *Ptr);
void ClearAllPtrRefFlag();
