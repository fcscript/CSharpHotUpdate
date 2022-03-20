#include "FCGetObj.h"
#include "FCTemplateType.h"
#include "FCCallScriptFunc.h"

FCGetObj* FCGetObj::s_Ins = nullptr;

FCGetObj::FCGetObj():m_nObjID(0)
{
	FCGetObj::s_Ins = this;

	m_CppProperty.ElementSize = sizeof(void*);
	m_CppProperty.m_ReadScriptFunc = PushScriptCppPtr;
	m_CppProperty.m_WriteScriptFunc = ReadScriptCppPtr;
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
	FCObjRef  *ObjRef = PushUObjectNoneRef(Obj);
	if(ObjRef)
	{
		++(ObjRef->Ref);
		return ObjRef->PtrIndex;
	}
	else
	{
		return 0;
	}
}

FCObjRef* FCGetObj::PushUObjectNoneRef(UObject* Obj)
{
	if (!Obj)
	{
		return nullptr;
	}
	ObjRefKey  ObjKey((uint8*)Obj, 0);
	CScriptRefObjMap::iterator itObj = m_ObjMap.find(ObjKey);
	if (itObj != m_ObjMap.end())
	{
		return itObj->second;
	}
	FCScriptContext* ScriptContext = GetScriptContext();
	FCDynamicClassDesc* ClassDesc = ScriptContext->RegisterUStruct(Obj->GetClass());

	FCObjRef* ObjRef = NewObjRef();
	ObjRef->Ref = 0;
	ObjRef->RefType = EFCObjRefType::RefObject;
	ObjRef->Parent = nullptr;
	ObjRef->ClassDesc = ClassDesc;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->ThisObjAddr = (uint8 *)Obj;
	m_ObjMap[ObjKey] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef;
}

int64  FCGetObj::PushNewObject(FCDynamicClassDesc* ClassDesc, const FName& Name, UObject* Outer, fc_intptr VM, fc_intptr ScriptValuePtr)
{
	UClass  *Class = ClassDesc->m_Class;
	UObject* Obj = nullptr;

	FCObjRef* ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = EFCObjRefType::NewUObject;
	//ObjRef->Parent = Outer;
	ObjRef->Parent = nullptr;
	ObjRef->ClassDesc = ClassDesc;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->ThisObjAddr = (uint8 *)Obj;
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
		ObjRefKey  ObjKey((uint8*)Obj, 0);
        ObjRef->ThisObjAddr = (uint8 *)Obj;
        m_ObjMap[ObjKey] = ObjRef;
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
	ObjRef->ThisObjAddr = (uint8 *)pValueAddr;
	ObjRef->DynamicProperty = GetStructDynamicProperty(Struct);

	ObjRefKey  ObjKey((uint8*)pValueAddr, 0);
	m_ObjMap[ObjKey] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushProperty(UObject *Parent, const FCDynamicProperty *DynamicProperty, void *pValueAddr)
{
	if(!Parent)
	{
		return 0;
	}
	FCObjRef *ParentRef = PushUObjectNoneRef(Parent);
	return PushChildProperty(ParentRef, DynamicProperty, pValueAddr);
}

int64  FCGetObj::PushChildProperty(FCObjRef* Parent, const FCDynamicProperty* DynamicProperty, void* pValueAddr)
{
	uint8  *ParentAddr = Parent->GetPropertyAddr();
	uint8  *CurValueAddr = (uint8 *)pValueAddr;
	ObjRefKey  ObjKey(ParentAddr, (int)(CurValueAddr - ParentAddr));

	CScriptRefObjMap::iterator itObj = m_ObjMap.find(ObjKey);
	if (itObj != m_ObjMap.end())
	{
		++(itObj->second->Ref);
		return itObj->second->PtrIndex;
	}
	FStructProperty* StructProperty = (FStructProperty*)DynamicProperty->Property;
	FCScriptContext* ScriptContext = GetScriptContext();
	FCDynamicClassDesc* ClassDesc = ScriptContext->RegisterUStruct(StructProperty->Struct);

	FCObjRef* ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = EFCObjRefType::RefProperty;
	ObjRef->Parent = nullptr;
	ObjRef->ClassDesc = ClassDesc;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->DynamicProperty = (FCDynamicProperty*)DynamicProperty;
	ObjRef->PropertyOffset = ObjKey.Offset;
	m_ObjMap[ObjKey] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;

	++(Parent->Ref);

	ObjRef->Parent = Parent;
	Parent->Childs.push_back(ObjRef);
	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushStructValue(const FCDynamicProperty *DynamicProperty, void *pValueAddr)
{
	ObjRefKey  ObjKey((uint8 *)pValueAddr, 0);
	CScriptRefObjMap::iterator itObj = m_ObjMap.find(ObjKey);
	if (itObj != m_ObjMap.end())
	{
		++(itObj->second->Ref);
		return itObj->second->PtrIndex;
	}
	void *SrcValueAddr = pValueAddr;
	// FStructPropertyDesc
	// 这种没有爹的，就拷贝一个吧
	FStructProperty* StructProperty = (FStructProperty *)DynamicProperty->Property;
	UStruct* Struct = StructProperty->Struct;
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
	ObjRef->ThisObjAddr = (uint8* )pValueAddr;
	m_ObjMap[ObjKey] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushTemplate(const FCDynamicProperty *DynamicProperty, void *pValueAddr, EFCObjRefType RefType)
{	
	ObjRefKey  ObjKey((uint8*)pValueAddr, 0);
	CScriptRefObjMap::iterator itObj = m_ObjMap.find(ObjKey);
	if (itObj != m_ObjMap.end())
	{
		++(itObj->second->Ref);
		return itObj->second->PtrIndex;
	}

	FCScriptContext *ScriptContext = GetScriptContext();

	FCObjRef  *ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = RefType;
	ObjRef->Parent = nullptr;
	ObjRef->ClassDesc = nullptr;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->DynamicProperty = (FCDynamicProperty *)DynamicProperty;
	ObjRef->ThisObjAddr = (uint8 *)pValueAddr;
	m_ObjMap[ObjKey] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef->PtrIndex;
}

int64  FCGetObj::PushCppPtr(void* CppPtr)
{
	ObjRefKey  ObjKey((uint8*)CppPtr, 0);
	CScriptRefObjMap::iterator itObj = m_ObjMap.find(ObjKey);
	if (itObj != m_ObjMap.end())
	{
		++(itObj->second->Ref);
		return itObj->second->PtrIndex;
	}

	FCScriptContext* ScriptContext = GetScriptContext();

	FCObjRef* ObjRef = NewObjRef();
	ObjRef->Ref = 1;
	ObjRef->RefType = EFCObjRefType::CppPtr;
	ObjRef->Parent = nullptr;
	ObjRef->ClassDesc = nullptr;
	ObjRef->PtrIndex = ++m_nObjID;
	ObjRef->DynamicProperty = &m_CppProperty;
	ObjRef->ThisObjAddr = (uint8*)CppPtr;
	m_ObjMap[ObjKey] = ObjRef;
	m_IntPtrMap[ObjRef->PtrIndex] = ObjRef;
	return ObjRef->PtrIndex;
}

void   FCGetObj::NotifyDeleteUObject(const class UObjectBase* Object, int32 Index)
{
	ObjRefKey  ObjKey((uint8*)Object, 0);
	CScriptRefObjMap::iterator itObj = m_ObjMap.find(ObjKey);
	if (itObj != m_ObjMap.end())
	{
		FCObjRef *ObjRef = itObj->second;
		DestroyChildRef(ObjRef);
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
		ReleaseObjRef(ObjRef);
	}
}

FCObjRef  *FCGetObj::NewObjRef()
{
	return new FCObjRef();
}

void  FCGetObj::ReleaseObjRef(FCObjRef* ObjRef)
{
    ObjRef->Ref--;
    if (ObjRef->Ref <= 0)
    {
        FCObjRef* ParentRef = ObjRef->Parent;
        if (ParentRef)
        {
            ParentRef->Childs.erase(ParentRef->Childs.MakeIterator(ObjRef));
        }
        DestroyChildRef(ObjRef);

		// 减少父对象的引用计数(递归)
		if(ParentRef)
		{
			ReleaseObjRef(ParentRef);
		}
    }
}

void  FCGetObj::DestroyChildRef(FCObjRef* ObjRef)
{
	// 父节点没有，儿子也没有必要留下, 儿子的儿子也不能留, 斩草要除根, 一脉不留存
	while (ObjRef->Childs.size() > 0)
	{
		FCObjRef* ChildPtr = ObjRef->Childs.front_ptr();
		ObjRef->Childs.pop_front();
		DestroyChildRef(ChildPtr);
	}
	DestroyObjRef(ObjRef);
}

void  FCGetObj::DestroyObjRef(FCObjRef *ObjRef)
{
	if(ObjRef)	
	{
		ObjRefKey ObjKey = ObjRef->GetRefKey();
		m_ObjMap.erase(ObjKey);
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
				int ArrayDim = ObjRef->DynamicProperty ? ObjRef->DynamicProperty->Property->ArrayDim : 1;
				Struct->DestroyStruct(ObjRef->GetPropertyAddr(), ArrayDim);
			}
			break;
			case EFCObjRefType::NewTArray:
			{
				FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
				FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
				FProperty *Inner = ArrayProperty->Inner;
				TArray_Clear(ScriptArray, Inner);
				delete ScriptArray;
			}
			break;
			case EFCObjRefType::NewTMap:
			{
				FScriptMap *ScriptMap = (FScriptMap*)ObjRef->GetThisAddr();
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
