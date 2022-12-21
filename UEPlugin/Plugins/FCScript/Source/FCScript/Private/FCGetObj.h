#pragma once
#include "FCDynamicClassDesc.h"

enum EFCObjRefType
{
    RefNone,        // 未知
    RefObject,      // UObject对象引用
    NewUObject,     // new UObject对象
    NewUStruct,     // new UStrucct对象
    NewProperty,    // new Property对象
    RefProperty,    // UObject的属性引用
    RefFunction,    // 引用Function
    RefStructValue, // 普通的Struct变量引用
    NewTArray,      // new TArray
    NewTMap,        // new TMap
    NewTSet,        // new TSet
    NewTLazyPtr,    // 
    NewTWeakPtr,    // 
    CppPtr,         // 全局的Cpp对象指针
    MapIterator,    // map_iterator
};

struct TMapIterator
{
    fc_intptr   MapInsPtr;
    int32       Index;
    TMapIterator() :MapInsPtr(0), Index(0) {}
};

struct FCObjRef
{
	FCObjRef  *m_pLast;
	FCObjRef  *m_pNext;
	FCObjRef  *Parent; // 父节点
	FCDynamicClassDesc* ClassDesc;
	FCDynamicProperty *DynamicProperty;  // 属性描述

	int64      PtrIndex;         // Wrap对象ID    
    uint8     *ThisObjAddr;      // 对象自己的地址
	int        Ref;              // 引用计数
	EFCObjRefType  RefType;
	CFastList<FCObjRef>  Childs;
	FCObjRef():m_pLast(NULL), m_pNext(NULL), Parent(NULL), ClassDesc(NULL), DynamicProperty(NULL), PtrIndex(0), ThisObjAddr(NULL), Ref(0), RefType(RefNone)
	{
	}
	ObjRefKey GetRefKey() const
	{
        return ObjRefKey(Parent ? Parent->ThisObjAddr : nullptr, ThisObjAddr);
	}
	UObject* GetParentObject() const
	{
		if (Parent)
		{
			return Parent->GetUObject();
		}
		return nullptr;
	}
	UObject* GetUObject() const
	{
		if (RefType == EFCObjRefType::RefObject || RefType == EFCObjRefType::NewUObject)
			return (UObject*)ThisObjAddr;
		return nullptr;
	}
	uint8*GetThisAddr()
	{
        return ThisObjAddr;
	}
	bool IsValid() const
	{
		return Parent ? (Parent->ThisObjAddr != nullptr) : (ThisObjAddr != nullptr);
	}
	uint8*GetPropertyAddr()
	{
        return ThisObjAddr;
	}
    FCPropertyType GetPropertyType() const
    {
        if (RefType != RefFunction)
            return DynamicProperty->Type;
        else
            return FCPROPERTY_Function;
    }
	FStructProperty *GetStructProperty() const
	{
		if(EFCObjRefType::RefProperty == RefType || EFCObjRefType::RefStructValue == RefType)
		{
			return (FStructProperty *)(DynamicProperty->Property);
		}
		return NULL;
	}
	TMapIterator *GetMapIterator() const
	{
		if(EFCObjRefType::MapIterator == RefType)
		{
			return (TMapIterator *)ThisObjAddr;
		}
		return nullptr;
	}
};

typedef  std::unordered_map<ObjRefKey, FCObjRef*>   CScriptRefObjMap;  // ptr ==> FCObjRef
typedef  std::unordered_map<int64, FCObjRef*>   CIntPtr2RefObjMap; // IntPtr ==> FCObjRef

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
    void Clear();
	// 功能：压入一个UObject对象
	int64  PushUObject(UObject* Obj);
	FCObjRef*  PushUObjectNoneRef(UObject* Obj);
	int64  PushNewObject(FCDynamicClassDesc* ClassDesc, const FName& Name, UObject* Outer, fc_intptr VM, fc_intptr ScriptValuePtr);
	int64  PushNewStruct(FCDynamicClassDesc* ClassDesc);
	// 功能：压入一个UObject的属性(生成周期随父对象)的引用
	int64  PushProperty(UObject *Parent, const FCDynamicProperty *DynamicProperty, void *pValueAddr);
    int64  PushNewTArray(const FCDynamicProperty* DynamicProperty, void* pValueAddr);
    int64  PushNewTMap(const FCDynamicProperty* DynamicProperty, void* pValueAddr);
    int64  PushNewTSet(const FCDynamicProperty* DynamicProperty, void* pValueAddr);
	// 功能：压入一个子属性(UObject或UStruct的成员变量)的引用
	int64  PushChildProperty(FCObjRef *Parent, const FCDynamicProperty* DynamicProperty, void* pValueAddr);
	// 功能：压入一个纯Struct对象(没有父对象，一般是临时的)
	int64  PushStructValue(const FCDynamicProperty *DynamicProperty, void *pValueAddr);
	// 功能：将一个Cpp栈上的临时变量压入到对象管理器
	int64  PushCppPropery(const FCDynamicProperty* DynamicProperty, void* pValueAddr);
	int64  PushTemplate(const FCDynamicProperty *DynamicProperty, void *pValueAddr, EFCObjRefType RefType);

	// 功能：压入一个全局指针
	int64  PushCppPtr(void *CppPtr);

    // 功能：压入一个TMap的迭代器
    int64  PushMapIterator(void* IteratorPtr);

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
			return itObj->second->GetUObject();
		}
		return nullptr;
	}
	void *GetPropertyAddr(int64 ObjID)
	{
		CIntPtr2RefObjMap::iterator itObj = m_IntPtrMap.find(ObjID);
		if (itObj != m_IntPtrMap.end())
		{
			return itObj->second->GetPropertyAddr();
		}
		return nullptr;
	}
	int  GetValueHash(int64 ObjID)
	{
		CIntPtr2RefObjMap::iterator itObj = m_IntPtrMap.find(ObjID);
		if (itObj != m_IntPtrMap.end())
		{
			FCUnitPtr  Ptr;
			Ptr.Ptr = itObj->second->GetPropertyAddr();
			return (int)(Ptr.nPtr);
		}
		return (int)ObjID;
	}
	void  *GetValuePtr(int64 ObjID) const
	{
		CIntPtr2RefObjMap::const_iterator itObj = m_IntPtrMap.find(ObjID);
		if (itObj != m_IntPtrMap.end())
		{
			return itObj->second->GetPropertyAddr();
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
	void  ReleaseObjRef(FCObjRef* ObjRef);
	void  DestroyChildRef(FCObjRef* ObjRef);
	void  DestroyObjRef(FCObjRef *ObjRef);
	void  *NewStructBuffer(int nBuffSize);
	void   DelStructBuffer(void *pBuffer);
protected:
	int64    m_nObjID;
	CScriptRefObjMap   m_ObjMap;
	CIntPtr2RefObjMap  m_IntPtrMap;
	FCDynamicProperty  m_CppProperty;
	FCDynamicProperty  m_MapIteratorProperty;
};
