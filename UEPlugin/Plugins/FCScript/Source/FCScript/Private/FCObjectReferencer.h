#pragma once
#include "Containers/Set.h"
#include "UObject/GCObject.h"

class FCObjectReferencer : public FGCObject
{
public:
    void Add(UObject* Object)
    {
        if (Object == nullptr)
            return;
        ReferencedObjects.Add(Object);
    }

    // ReSharper disable once CppParameterMayBeConstPtrOrRef
    void Remove(UObject* Object)
    {
        if (Object == nullptr)
            return;
        ReferencedObjects.Remove(Object);
    }

    void Clear()
    {
        return ReferencedObjects.Empty();
    }

    void SetName(const FString& InName)
    {
        Name = InName;
    }

    virtual void AddReferencedObjects(FReferenceCollector& Collector) override
    {
        Collector.AddReferencedObjects(ReferencedObjects);
    }

    virtual FString GetReferencerName() const override
    {
        return Name;
    }
    const TSet<UObject*> &GetReferencedObjects() const
    {
        return ReferencedObjects;
    }
private:
    TSet<UObject*> ReferencedObjects;
    FString Name = TEXT("FObjectReferencer");
};