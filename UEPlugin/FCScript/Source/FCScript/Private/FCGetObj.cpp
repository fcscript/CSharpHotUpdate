#include "FCGetObj.h"
#include "FCTemplateType.h"

FCGetObj* FCGetObj::s_Ins = nullptr;

FCGetObj::FCGetObj():m_nObjID(0)
{
	FCGetObj::s_Ins = this;
}

FCGetObj::~FCGetObj()
{
	if(FCGetObj::s_Ins == this)
	{
		FCGetObj::s_Ins = nullptr;
	}
}

int64  FCGetObj::PushUObject(UObject *Obj)
{
	if(!Obj)
	{
		return 0;
	}
	CScriptRefObjMap::iterator itObj = m_ObjMap.find(Obj);
	if(itObj != m_ObjMap.end())
	{
		++(itObj->second->Ref);
		return itObj->second->PtrIndex;
	}
	FCScriptContext *ScriptContext = GetScriptContext();
	FCDynamicClassDesc  *ClassDesc = ScriptContext->RegisterUStruct(Obj->GetClass());

	FCObjRef  *ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = EFCObjRefType::RefObject;
	ObjRef->Parent = Obj->GetOuter();
	ObjRef->ClassDesc = ClassDesc;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->ValuePtr = Obj;
	m_ObjMap[Obj] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushNewObject(FCDynamicClassDesc* ClassDesc, const FName& Name, UObject* Outer, fc_intptr VM, fc_intptr ScriptValuePtr)
{
	UClass  *Class = ClassDesc->m_Class;
	UObject* Obj = nullptr;

	FCObjRef* ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = EFCObjRefType::NewUObject;
	ObjRef->Parent = Outer;
	ObjRef->ClassDesc = ClassDesc;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->ValuePtr = Obj;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;

    if(ScriptValuePtr)
    {
        fc_set_value_wrap_objptr(VM, ScriptValuePtr, ObjRef->PtrIndex);
    }

    // 后创建对象
#if ENGINE_MINOR_VERSION < 26
    Obj = StaticConstructObject_Internal(Class, Outer, Name);
#else
    FStaticConstructObjectParameters ObjParams(Class);
    ObjParams.Outer = Outer;
    ObjParams.Name = Name;
    Obj = StaticConstructObject_Internal(ObjParams);
#endif

    if (Obj)
    {
        ObjRef->ValuePtr = Obj;
        m_ObjMap[Obj] = ObjRef;
    }

	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushNewStruct(FCDynamicClassDesc* ClassDesc)
{
	UStruct  *Struct = ClassDesc->m_Struct;
	int ValueSize = Struct->GetStructureSize();
	void *pValueAddr = NewStructBuffer(ValueSize);
	Struct->InitializeStruct(pValueAddr, 1);

	FCObjRef* ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = EFCObjRefType::NewUStruct;
	ObjRef->Parent = nullptr;
	ObjRef->ClassDesc = ClassDesc;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->ValuePtr = pValueAddr;
	m_ObjMap[pValueAddr] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushProperty(UObject *Parent, const FCDynamicProperty *DynamicProperty, void *pValueAddr)
{
	if(!Parent)
	{
		return 0;
	}

	CScriptRefObjMap::iterator itObj = m_ObjMap.find(pValueAddr);
	if (itObj != m_ObjMap.end())
	{
		++(itObj->second->Ref);
		return itObj->second->PtrIndex;
	}
	FStructProperty* StructProperty = (FStructProperty *)DynamicProperty->Property;
	FCScriptContext *ScriptContext = GetScriptContext();
	FCDynamicClassDesc  *ClassDesc = ScriptContext->RegisterUStruct(StructProperty->GetOwnerStruct());

	FCObjRef  *ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = EFCObjRefType::RefProperty;
	ObjRef->Parent = Parent;
	ObjRef->ClassDesc = ClassDesc;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->DynamicProperty = (FCDynamicProperty *)DynamicProperty;
	ObjRef->ValuePtr = pValueAddr;
	m_ObjMap[pValueAddr] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;

	CScriptRefObjMap::iterator itParent = m_ObjMap.find(Parent);
	if(itParent == m_ObjMap.end())
	{
		PushUObject(Parent);
		itParent = m_ObjMap.find(Parent);
		itParent->second->Ref = 0;  // 没有脚本引用
	}
    else
    {
        ++(itParent->second->Ref);
    }
	itParent->second->Childs.push_back(ObjRef);
	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushStructValue(const FCDynamicProperty *DynamicProperty, void *pValueAddr)
{
	void *SrcValueAddr = pValueAddr;
	// FStructPropertyDesc
	// 这种没有爹的，就拷贝一个吧
	FStructProperty* StructProperty = (FStructProperty *)DynamicProperty->Property;
	UStruct *Struct = StructProperty->GetOwnerStruct();
	int ValueSize = Struct->GetStructureSize();
	int ArrayDim = StructProperty->ArrayDim;
	pValueAddr = NewStructBuffer(ValueSize);
	Struct->InitializeStruct(pValueAddr, ArrayDim);
	StructProperty->CopyValuesInternal(pValueAddr, SrcValueAddr, ArrayDim);
	//StructProperty->CopyScriptStruct(pValueAddr, SrcValueAddr, ArrayDim);
	
	FCScriptContext *ScriptContext = GetScriptContext();
	FCDynamicClassDesc  *ClassDesc = ScriptContext->RegisterUStruct(Struct);

	FCObjRef  *ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = EFCObjRefType::RefStructValue;
	ObjRef->Parent = nullptr;
	ObjRef->ClassDesc = ClassDesc;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->DynamicProperty = (FCDynamicProperty *)DynamicProperty;
	ObjRef->ValuePtr = pValueAddr;
	m_ObjMap[pValueAddr] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushTemplate(const FCDynamicProperty *DynamicProperty, void *pValueAddr, EFCObjRefType RefType)
{	
	FCScriptContext *ScriptContext = GetScriptContext();

	FCObjRef  *ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = RefType;
	ObjRef->Parent = nullptr;
	ObjRef->ClassDesc = nullptr;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->DynamicProperty = (FCDynamicProperty *)DynamicProperty;
	ObjRef->ValuePtr = pValueAddr;
	m_ObjMap[pValueAddr] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef->PtrIndex;
}

void   FCGetObj::NotifyDeleteUObject(const class UObjectBase* Object, int32 Index)
{
	CScriptRefObjMap::iterator itObj = m_ObjMap.find(Object);
	if (itObj != m_ObjMap.end())
	{
		FCObjRef *ObjRef = itObj->second;
		// 先释放所有属性的引用, 不用留着了，留着也没有用，只是浪费内存
		while(ObjRef->Childs.size() >0)
		{
			FCObjRef *ChildPtr = ObjRef->Childs.front_ptr();
			ObjRef->Childs.pop_front();
			DestoryObjRef(ChildPtr);
		}
		DestoryObjRef(ObjRef);
	}
}

void   FCGetObj::DeleteValue(int64 ObjID)
{
	ReleaseValue(ObjID);
}

void   FCGetObj::ReleaseValue(int64 ObjID)
{
	CIntPtr2RefObjMap::iterator itObj = m_IntPtrMap.find(ObjID);
	if (itObj != m_IntPtrMap.end())
	{
		FCObjRef* ObjRef = itObj->second;
		ObjRef->Ref--;
		// 如果是对象属性，就需要释放父对象的引用计数
		if(EFCObjRefType::RefProperty == ObjRef->RefType)
		{
			CScriptRefObjMap::iterator itParent = m_ObjMap.find(ObjRef->Parent);
			if(itParent != m_ObjMap.end())
			{
				FCObjRef *ParentRef = itParent->second;
				ParentRef->Childs.erase(ParentRef->Childs.MakeIterator(ObjRef));
				ParentRef->Ref--;
				if(ParentRef->Ref <= 0)
				{
					DestoryObjRef(ParentRef);
				}
			}
		}
		if(ObjRef->Ref <= 0)
		{
			DestoryObjRef(ObjRef);
		}
	}
}

FCObjRef  *FCGetObj::NewObjRef()
{
	return new FCObjRef();
}

void  FCGetObj::DestoryObjRef(FCObjRef *ObjRef)
{
	if(ObjRef)	
	{
		m_ObjMap.erase(ObjRef->ValuePtr);
		m_IntPtrMap.erase(ObjRef->PtrIndex);

		switch(ObjRef->RefType)
		{
			case EFCObjRefType::NewUObject:
			{
			}
			break;
			case EFCObjRefType::NewUStruct:
			case EFCObjRefType::RefStructValue:
			{
				UStruct* Struct = ObjRef->ClassDesc->m_Struct;
				int ValueSize = Struct->GetStructureSize();
				int ArrayDim = ObjRef->DynamicProperty->Property->ArrayDim;
				Struct->DestroyStruct(ObjRef->ValuePtr, ArrayDim);
			}
			break;
			case EFCObjRefType::NewTArray:
			{
				FScriptArray *ScriptArray = (FScriptArray*)ObjRef->ValuePtr;
				FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
				FProperty *Inner = ArrayProperty->Inner;
				TArray_Clear(ScriptArray, Inner);
				delete ScriptArray;
			}
			break;
			case EFCObjRefType::NewTMap:
			{
				FScriptMap *ScriptMap = (FScriptMap*)ObjRef->ValuePtr;
				TMap_Clear(ScriptMap, (FMapProperty *)ObjRef->DynamicProperty->Property);
				delete ScriptMap;
			}
			break;
			default:
			break;
		}

		delete ObjRef;
	}
}

void  *FCGetObj::NewStructBuffer(int nBuffSize)
{
	return new char[nBuffSize];
}

void   FCGetObj::DelStructBuffer(void *pBuffer)
{
	if(pBuffer)
	{
		char *pBuf = (char *)pBuffer;
		delete []pBuf;
	}
}

static FCGetObj   s_GEtObjIns;
