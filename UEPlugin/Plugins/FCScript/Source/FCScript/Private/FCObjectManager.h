#pragma once
#include "FCDynamicClassDesc.h"

// -- 脚本对象与UObject对象的关联管理器
struct FBindObjectInfo
{
	const UObjectBaseUtility  *Object;
	int32 Index;
	int64    m_ScriptIns;
	const char*  m_ScriptName;
	FBindObjectInfo() :Object(nullptr), Index(0), m_ScriptIns(0)
	{
	}
	FBindObjectInfo(const UObjectBaseUtility *InObject, int32 InIndex, const char *InScriptName) :Object(InObject), Index(InIndex), m_ScriptIns(0), m_ScriptName(InScriptName)
	{
	}
	void  Set(const UObjectBaseUtility *InObject, int32 InIndex, const char *InScriptName)
	{
		Object = InObject;
		Index = InIndex;
		m_ScriptIns = 0;
		m_ScriptName = InScriptName;
	}
};

struct FScriptOverrideKey
{
    const UObjectBase*   Object;    // 绑定的对象
    UFunction* Function;  // 绑定的函数
    FScriptOverrideKey() :Object(nullptr), Function(nullptr) {}
    FScriptOverrideKey(const UObjectBase *InObject, UFunction *InFunction):Object(InObject), Function(InFunction){}
};

template<> struct stdext::hash_compare<FScriptOverrideKey>
{
    enum
    {	// parameters for hash table
        bucket_size = 1		// 0 < bucket_size
    };
    size_t operator()(const FScriptOverrideKey& Key) const
    {	// hash _Keyval to size_t value by pseudorandomizing transform
        FCUnitPtr Ptr1;
        Ptr1.Ptr = Key.Object;
        FCUnitPtr Ptr2;
        Ptr2.Ptr = Key.Function;
        size_t HashValue = (size_t)(Ptr1.nPtr + Ptr2.nPtr);
        return HashValue;
    }

    bool operator()(const FScriptOverrideKey& key1, const FScriptOverrideKey& key2) const
    {	// test if _Keyval1 ordered before _Keyval2
        if(key1.Object != key2.Object)
        {
            return key1.Object < key2.Object;
        }
        return key1.Function < key2.Function;
    }
};

class FFCObjectdManager
{
public:
	FFCObjectdManager();
	~FFCObjectdManager();
public:
	static FFCObjectdManager  *GetSingleIns();
public:
	void  Clear();

	void  BindScript(const class UObjectBaseUtility* Object, UClass* Class, const FString& ScriptClassName);
	void  BindToScript(const class UObjectBaseUtility* Object, UClass* Class, const char *ScriptClassName);
    void  CallBindScript(UObject *InObject, const char *ScriptClassName);
	void  DynamicBind(const class UObjectBaseUtility *Object, UClass *Class);

	// 功能：对象释放事件
	void  NotifyDeleteUObject(const class UObjectBase *Object, int32 Index);

	void  PushDynamicBindClass(UClass *Class, const char *ScriptClassName);
	void  PopDynamicBindClass();
	// 返回是不是当前动态绑定的对象
	bool  IsDynamicBindClass(UClass *Class);

	bool  IsBindScript(const class UObjectBaseUtility *Object)
	{
        CBindObjectInfoMap::const_iterator itFind = m_BindObjects.find(Object);
        return itFind != m_BindObjects.end();
	}
	FBindObjectInfo*  FindBindObjectInfo(const class UObjectBaseUtility *Object)
	{
		CBindObjectInfoMap::iterator itFind = m_BindObjects.find(Object);
		if(itFind != m_BindObjects.end())
		{
			return &(itFind->second);
		}
		return nullptr;
	}	
public:
	FCDynamicOverrideFunction*RegisterReceiveBeginPlayFunction(UObject *InObject, UClass* Class);

	// 功能：覆盖函数
	FCDynamicOverrideFunction *RegisterOverrideFunc(UObject *InObject, fc_intptr InScriptPtr, const char *InFuncName);
	FCDynamicOverrideFunction *ToOverrideFunction(UObject *InObject, UFunction *InFunction, FNativeFuncPtr InFuncPtr, int InNativeBytecodeIndex);
	FCDynamicOverrideFunction *FindOverrideFunction(UObject *InObject, UFunction *InFunction);
    int64 FindOverrideScriptIns(UObject *InObject, UFunction *InFunction);

	// 执行原生的调用
	void  NativeCall(UObject* InObject, FCDynamicFunction* DynamicFunc, fc_intptr L, int nStart);

	FCDynamicDelegateList  *FindDelegateFunction(UObject *InObject);
	void  RegisterScriptDelegate(UObject *InObject, const FCDynamicProperty* InDynamicProperty, fc_intptr InScriptThisPtr, int InClassNameID, int InFuncNameID);
	void  RemoveScriptDelegate(UObject *InObject, const FCDynamicProperty* InDynamicProperty, fc_intptr InScriptThisPtr, int InClassNameID, int InFuncNameID);
	void  ClearScriptDelegate(UObject* InObject, const FCDynamicProperty* InDynamicProperty);

	void  CheckGC();
protected:
	void  ClearObjectDelegate(const class UObjectBase *Object);
	void  ClearAllDynamicFunction();
	void  AddDelegateToClass(FCDynamicOverrideFunction *InDynamicFunc, UClass *InClass);
	void  RemoveDelegateFromClass(FCDynamicOverrideFunction *InDynamicFunc, UClass *InClass);
	void  RemoveObjectDelegate(UObject *InObject, const FCDynamicProperty* InDynamicProperty);
    void  RemoveOverrideRefByObject(const class UObjectBase *Object);
protected:
	const char *NameToName(const char *InName)
	{
		CScriptNamePtrMap::iterator itName = m_NamePtrMap.find(InName);
		if(itName != m_NamePtrMap.end())
		{
			return itName->first.c_str();
		}
        m_NamePtrMap[InName] = true;
		itName = m_NamePtrMap.find(InName);
		return itName->first.c_str();
	}
protected:
	struct FDynmicBindClassInfo
	{
		UClass  *Class;
		const char *ScriptClassName;
	};
	struct FBindReceiveBeginPlayInfo
	{
		int  Ref;
		FNativeFuncPtr  NativeFuncPtr;
		FBindReceiveBeginPlayInfo():Ref(0), NativeFuncPtr(nullptr){}
	};
	typedef  stdext::hash_map<const UObjectBase*, FBindObjectInfo>   CBindObjectInfoMap;
	typedef  stdext::hash_map<const UClass*, FBindReceiveBeginPlayInfo>   CBindReceiveBeginPlayRefMap;
	typedef  stdext::hash_map<std::string, bool>   CScriptNamePtrMap;

	std::vector<FDynmicBindClassInfo>   m_DynamicBindClassInfo;
	UClass*             m_pCurrentBindClass;
	const char *        m_ScriptsClassName;
 
	CBindObjectInfoMap  m_BindObjects;
	CScriptNamePtrMap   m_NamePtrMap;

	// ------------------------------	
	typedef stdext::hash_map<UFunction*, FCDynamicOverrideFunction*> COverrideFunctionMap;
	typedef stdext::hash_map<UObject *, FCDynamicDelegateList>   CObjectDelegateMap;
	typedef stdext::hash_map<UFunction*, int>                    CFunctionRefMap;

	COverrideFunctionMap         m_OverrideFunctionMap;  // OverrideFunction
	CObjectDelegateMap           m_ObjectDelegateMap;    // 对象委托列表	
	CFunctionRefMap              m_DelegateRefMap;       // 委托引用计数
    // ------------------------------	

    // ------------------------------	函数重载记录 -------------------------
    typedef std::vector<UFunction*>  CScriptFunctionList;
    typedef stdext::hash_map<FScriptOverrideKey, int64> COverrideFunction2ScriptInsMap;
    typedef stdext::hash_map<UObjectBase*, CScriptFunctionList> COverrideObjectFunctionMap;
    COverrideFunction2ScriptInsMap   m_OverrideFunctionScriptInsMap;  // 重载的脚本实例
    COverrideObjectFunctionMap       m_OverrideObjectFunctionMap;    //  
    CFunctionRefMap                  m_OverrideRefMap;
    // ------------------------------	

	//-----------------延迟ReceiveBeginPlay
	// 
	typedef std::vector<UObject*>     CDelayCallBeginPlayList;
	CDelayCallBeginPlayList        m_DelayCallBeginPlayList;

	//-------------------------------------
};