#pragma once
#include "FCDynamicClassDesc.h"

enum EFCObjRefType
{
	RefNone,        // 未知
	RefObject,      // UObject对象引用
	NewUObject,     // new UObject对象
	NewUStruct,     // new UStrucct对象
	RefProperty,    // UObject的属性引用
	RefStructValue, // 普通的Struct变量引用
	NewTArray,      // new TArray
	NewTMap,        // new TMap
};

struct FCObjRef
{
	FCObjRef  *m_pLast;
	FCObjRef  *m_pNext;
	UObject   *Parent; // 父节点
	FCDynamicClassDesc* ClassDesc;

	FCDynamicProperty *DynamicProperty;
	int64     PtrIndex;

	void     *ValuePtr;
	int       Ref;
	EFCObjRefType  RefType;
	CFastList<FCObjRef>  Childs;
	FCObjRef():m_pLast(NULL), m_pNext(NULL), Parent(NULL), ClassDesc(NULL), DynamicProperty(NULL), PtrIndex(0), ValuePtr(NULL), Ref(0), RefType(RefNone)
	{
	}
	UObject* GetUObject() const
	{
		if (RefType == EFCObjRefType::RefObject || RefType == EFCObjRefType::NewUObject)
			return (UObject*)ValuePtr;
		return nullptr;
	}
	FStructProperty *GetStructProperty() const
	{
		if(EFCObjRefType::RefProperty == RefType || EFCObjRefType::RefStructValue == RefType)
		{
			return (FStructProperty *)(DynamicProperty->Property);
		}
		return NULL;
	}
};

typedef  stdext::hash_map<const void*, FCObjRef*>   CScriptRefObjMap;  // ptr ==> FCObjRef
typedef  stdext::hash_map<int64, FCObjRef*>   CIntPtr2RefObjMap; // IntPtr ==> FCObjRef

class FCGetObj
{	
public:
	static FCGetObj  *s_Ins;
	FCGetObj();
	~FCGetObj();

	static FCGetObj  *GetIns()
	{
		return FCGetObj::s_Ins;
	}
public:
	// 功能：压入一个UObject对象
	int64  PushUObject(UObject* Obj);
	int64  PushNewObject(FCDynamicClassDesc* ClassDesc, const FName& Name, UObject* Outer, fc_intptr VM, fc_intptr ScriptValuePtr);
	int64  PushNewStruct(FCDynamicClassDesc* ClassDesc);
	// 功能：压入一个UObject的属性(生成周期随父对象)
	int64  PushProperty(UObject *Parent, const FCDynamicProperty *DynamicProperty, void *pValueAddr);
	// 功能：压入一个纯Struct对象(没有父对象，一般是临时的)
	int64  PushStructValue(const FCDynamicProperty *DynamicProperty, void *pValueAddr);
	int64  PushTemplate(const FCDynamicProperty *DynamicProperty, void *pValueAddr, EFCObjRefType RefType);

	// 功能：对象释放事件
	void  NotifyDeleteUObject(const class UObjectBase* Object, int32 Index);

	// 功能：删除一个对象(由脚本是通过new创建的)
	void   DeleteValue(int64 ObjID);

	// 功能：释放一个脚本对象(通过参数传入的）
	void   ReleaseValue(int64 ObjID);

	FCObjRef *FindValue(int64 ObjID)
	{
		CIntPtr2RefObjMap::iterator itObj = m_IntPtrMap.find(ObjID);
		if(itObj != m_IntPtrMap.end())
		{
			return itObj->second;
		}
		return nullptr;
	}

	UObject *GetUObject(int64 ObjID)
	{
		CIntPtr2RefObjMap::iterator itObj = m_IntPtrMap.find(ObjID);
		if(itObj != m_IntPtrMap.end())
		{
			EFCObjRefType  RefType = itObj->second->RefType;
			if(RefType == EFCObjRefType::RefObject || RefType == EFCObjRefType::NewUObject)
				return (UObject*)itObj->second->ValuePtr;
		}
		return nullptr;
	}
	void *GetPropertyAddr(int64 ObjID)
	{
		CIntPtr2RefObjMap::iterator itObj = m_IntPtrMap.find(ObjID);
		if (itObj != m_IntPtrMap.end())
		{
			if (itObj->second->RefType != EFCObjRefType::RefProperty)
				return itObj->second->ValuePtr;
		}
		return nullptr;
	}
	int  GetValueHash(int64 ObjID)
	{
		CIntPtr2RefObjMap::iterator itObj = m_IntPtrMap.find(ObjID);
		if (itObj != m_IntPtrMap.end())
		{
			FCUnitPtr  Ptr;
			Ptr.Ptr = itObj->second->ValuePtr;
			return (int)(Ptr.nPtr);
		}
		return (int)ObjID;
	}
	void  *GetValuePtr(int64 ObjID) const
	{
		CIntPtr2RefObjMap::const_iterator itObj = m_IntPtrMap.find(ObjID);
		if (itObj != m_IntPtrMap.end())
		{
			return itObj->second->ValuePtr;
		}
		return nullptr;
	}
	bool EqualValue(int64 LeftObjID, int64 RightObjID) const
	{
		void* LeftPtr = GetValuePtr(LeftObjID);
		void* RightPtr = GetValuePtr(RightObjID);
		return LeftPtr == RightPtr;
	}
protected:
	FCObjRef  *NewObjRef();
	void  DestoryObjRef(FCObjRef *ObjRef);
	void  *NewStructBuffer(int nBuffSize);
	void   DelStructBuffer(void *pBuffer);
protected:
	int64    m_nObjID;
	CScriptRefObjMap   m_ObjMap;
	CIntPtr2RefObjMap  m_IntPtrMap;
};
