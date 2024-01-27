#pragma once
#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "UObject/Class.h"
#include "Logging/LogCategory.h"
#include "Logging/LogMacros.h"
#include "CoreUObject.h"
#include "FCPropertyType.h"
#include "FCStringBuffer.h"
#include "FCObjectReferencer.h"
#include "../../FCLib/include/fc_api.h"
#include "FCStringCore.h"
#include "FCSafeProperty.h"

DECLARE_LOG_CATEGORY_EXTERN(LogFCScript, Log, All);

struct FCUnitPtr
{
	union
	{
		const void  *Ptr;
		int64  nPtr;
	};
};

// 动态属性(反射)
struct FCDynamicPropertyBase
{
	int		ElementSize;      // 元素所点字节
	int		Offset_Internal;  // 相对偏移
	int     PropertyIndex;    // 参数或属性索引(序号)
	int     ScriptParamIndex; // 脚本参数索引号
	const char* Name;        // 变量名,调试时用的(常引用)
    const char* ClassName;   // 类名
	
	FCPropertyType    Type;       // 类型
	//const FProperty  *Property;
    const FCSafeProperty* SafePropertyPtr;
	bool              bRef;       // 是不是引用类型
	bool              bOuter;     // 是不是输出类型
    bool              bTempNeedRef;  // 临时的上下拷贝参数标记
    bool              bTempRealRef;  // 

#ifdef UE_BUILD_DEBUG
    const char* DebugDesc;
#endif
	
	FCDynamicPropertyBase() :ElementSize(0), Offset_Internal(0), PropertyIndex(0), ScriptParamIndex(0), Name(nullptr), ClassName(nullptr),Type(FCPropertyType::FCPROPERTY_Unkonw), SafePropertyPtr(nullptr), bRef(false), bOuter(false), bTempNeedRef(false), bTempRealRef(false)
	{
#ifdef UE_BUILD_DEBUG
        DebugDesc = nullptr;
#endif
	}
	bool  IsRef() const
	{
		return bRef;
	}
	bool  IsOuter() const
	{
		return bOuter;
	}
	const char* GetFieldName() const
	{
		return Name;
	}
	const char* GetClassName() const
	{
		return ClassName;
	}
	// 功能：得到委托的触发函数
	UFunction *GetSignatureFunction() const
	{
        return SafePropertyPtr->GetSignatureFunction();
	}
	int GetMemSize() const { return sizeof(FCDynamicPropertyBase); }
};

typedef  void(*LPPushScriptValueFunc)(int64  VM, int64 ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void *ObjRefPtr);
typedef  void(*LPOuterScriptValueFunc)(int64  L, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);

// 动态属性(反射)
struct FCDynamicProperty : public FCDynamicPropertyBase
{
	LPPushScriptValueFunc   m_WriteScriptFunc;   // 将UE变量写入到脚本
	LPPushScriptValueFunc   m_ReadScriptFunc;    // 将脚本变量写入到UE对象

	FCDynamicProperty():m_WriteScriptFunc(nullptr), m_ReadScriptFunc(nullptr)
	{
	}

	void  InitProperty(const FProperty *InProperty);
    void  InitCppType(FCPropertyType InType, const char* InClassName, int InElementSize);
    int GetMemSize() const { return sizeof(FCDynamicProperty); }
};

struct  FCDynamicFunction
{
	UFunction  *Function;
	int     FuncID;           // 函数ID
	int     ReturnPropertyIndex;
	int     ParmsSize;        // 参数序列化后的字节大小
	int     ParamCount;       // 函数参数个数(不包括返回值)
	bool    bOverride;
	bool    bOuter;
	bool    bRegister;        // 是不是在类中注册了
	bool    bDelegate;
    const char *Name;        // 函数名
	std::vector<FCDynamicProperty>   m_Property;
	FCDynamicFunction():Function(nullptr), FuncID(0), ReturnPropertyIndex(-1), ParmsSize(0), ParamCount(0), bOverride(false), bOuter(false), bRegister(false), bDelegate(false), Name(nullptr)
	{
	}
	void  InitParam(UFunction *InFunction);
    int GetMemSize() const { return sizeof(FCDynamicFunction); }
};

struct FCDynamicOverrideFunction : public FCDynamicFunction
{
	FNativeFuncPtr   OleNativeFuncPtr;  // 原始的NativeFunc函数
	FNativeFuncPtr   CurOverrideFuncPtr;
	int              m_NativeBytecodeIndex;
	bool             m_bLockCall;
	TArray<uint8>    m_NativeScript;
	FCDynamicOverrideFunction() : OleNativeFuncPtr(nullptr), CurOverrideFuncPtr(nullptr), m_NativeBytecodeIndex(0), m_bLockCall(false)
	{
	}
    int GetMemSize() const { return sizeof(FCDynamicOverrideFunction); }
};

struct FCDelegateInfo
{
	FCDynamicOverrideFunction*    DynamicFunc;
	FCDynamicProperty*    DynamicProperty;
	fc_intptr     ThisPtr;  // 脚本的This指针
	int           ClassNameID; // 类名
	int           FunctionNameID; // 类的委托的函数名
	FCDelegateInfo():DynamicFunc(nullptr), DynamicProperty(nullptr), ThisPtr(0), ClassNameID(0), FunctionNameID(0){}
	FCDelegateInfo(const FCDynamicOverrideFunction *InDynamicFunc, const FCDynamicProperty *InDynamicProperty, fc_intptr InThisPtr, int InClassNameID, int InFuncNameID)
		:DynamicFunc((FCDynamicOverrideFunction*)InDynamicFunc)
		,DynamicProperty((FCDynamicProperty*)InDynamicProperty)
		,ThisPtr(InThisPtr)
		,ClassNameID(InClassNameID)
		,FunctionNameID(InFuncNameID)
	{
	}
	bool operator == (const FCDelegateInfo &Other) const
	{
		return DynamicFunc == Other.DynamicFunc
			&& DynamicProperty == Other.DynamicProperty
			&& ThisPtr == Other.ThisPtr
			&& ClassNameID == Other.ClassNameID
			&& FunctionNameID == Other.FunctionNameID;
	}
};

struct FCDynamicDelegateList
{
	std::vector<FCDelegateInfo>      Delegates;  // 脚本中绑定的委托方法
	
	FCDynamicDelegateList()
	{
	}
	int FindDelegate(const FCDelegateInfo &Info) const;
		
	// 功能：添加一个委托
	bool  AddScriptDelegate(const FCDelegateInfo &Info);
	bool  DelScriptDelegate(const FCDelegateInfo &Info);
};

typedef  std::unordered_map<const char*, FCDynamicProperty*, FCStringHash, FCStringEqual>   CDynamicName2Property;
typedef  std::unordered_map<int, FCDynamicProperty*>   CDynamicID2Property;
typedef  std::vector<FCDynamicProperty*>   CDynamicPropertyPtrArray;

typedef  std::unordered_map<int, FCDynamicFunction*>   CDynamicFunctionIDMap; // id == > function
typedef  std::unordered_map<const char*, FCDynamicFunction*, FCStringHash, FCStringEqual>   CDynamicFunctionNameMap;  // name ==> function

const char* GetUEClassName(const char* InName);

// 一个动态类的数据结构
struct FCDynamicClassDesc
{
	UStruct                     *m_Struct;
	UClass                      *m_Class;
    UScriptStruct               *m_ScriptStruct;
	FCDynamicClassDesc          *m_Super;
	int                          m_nClassNameID; // 在脚本中的Wrap class ID, 唯一的
	EClassCastFlags              m_ClassFlags;  // 用于强制转换的检测
	const char*                  m_SuperName;
	const char*                  m_UEClassName; // 类名，wrap的类名
	CDynamicPropertyPtrArray     m_Property;  // 属性
	CDynamicName2Property        m_Name2Property;  // 所有的属性

	CDynamicFunctionNameMap      m_Functions;   // 所有的函数 name ==> function
	
	// ------- 脚本中的的属性ID或函数ID
	CDynamicFunctionIDMap        m_ID2Function;    // id ==> function
	CDynamicID2Property          m_ID2Property;    // id ==> Property
	// ------- 脚本中的的属性ID或函数ID

	FCDynamicClassDesc():m_Struct(nullptr), m_Class(nullptr), m_ScriptStruct(nullptr), m_Super(nullptr), m_nClassNameID(0), m_ClassFlags(CASTCLASS_None), m_SuperName(nullptr), m_UEClassName(nullptr)
	{
	}
	~FCDynamicClassDesc();
	FCDynamicClassDesc(const FCDynamicClassDesc &other);
	FCDynamicClassDesc &operator = (const FCDynamicClassDesc &other)
	{
		return CopyDesc(other);
	}

	void Clear();
	FCDynamicClassDesc &CopyDesc(const FCDynamicClassDesc &other);

	void  OnRegisterStruct(UStruct *Struct, void *Context);
    int   GetMemSize() const;

	// 功能：注册一个函数
	FCDynamicFunction*  RegisterUEFunc(const char *pcsFuncName);

    // 功能：注册一个类的属性
    FCDynamicProperty* RegisterProperty(const char* InPropertyName);
	
	// 功能：注册一个类的属性
	FCDynamicProperty*  RegisterProperty(const char *pcsPropertyName, int nNameID);
	// 功能：注册一个函数
	FCDynamicFunction*  RegisterFunc(const char *pcsFuncName, int nFuncID);


	FCDynamicFunction  *FindFunctionByID(int nFuncID)
	{
		CDynamicFunctionIDMap::iterator itFunction = m_ID2Function.find(nFuncID);
		if(itFunction != m_ID2Function.end())
		{
			return itFunction->second;
        }
        if (m_Super)
        {
            return m_Super->FindFunctionByID(nFuncID);
        }
		return nullptr;
	}
	FCDynamicProperty *FindAttribByID(INT nAttribNameID)
	{
		CDynamicID2Property::iterator itAttrib = m_ID2Property.find(nAttribNameID);
		if(itAttrib != m_ID2Property.end())
		{
			return itAttrib->second;
        }
        if (m_Super)
        {
            return m_Super->FindAttribByID(nAttribNameID);
        }
		return nullptr;
	}

	FCDynamicFunction  *FindFunctionByName(const char *FuncName)
	{
		CDynamicFunctionNameMap::iterator itFunction = m_Functions.find(FuncName);
		if(itFunction != m_Functions.end())
		{
			return itFunction->second;
        }

        // 没有找到，就动态注册一个
        FCDynamicFunction *Function = RegisterUEFunc(FuncName);
        if(Function)
        {
            return Function;
        }
        // 如果还是没有找到，就是设置一个空的, 避免反复查找
        FuncName = GetConstName(FuncName);
        m_Functions[FuncName] = nullptr;
        return nullptr;
	}
	FCDynamicProperty *FindAttribByName(const char *AttribName)
	{
		CDynamicName2Property::iterator itAttrib = m_Name2Property.find(AttribName);
		if(itAttrib != m_Name2Property.end())
		{
			return itAttrib->second;
		}

        // 没有找到，就动态注册一个
        FCDynamicProperty *Property = RegisterProperty(AttribName);
        if(Property)
        {
            return Property;
        }
        // 如果还是没有找到，就是设置一个空的, 避免反复查找
        AttribName = GetConstName(AttribName);
        m_Name2Property[AttribName] = nullptr;
		return nullptr;
	}
};

struct FNativeOverridenFnctionInfo
{
    UClass* Class;
    UFunction* Function;
    FNativeOverridenFnctionInfo() :Class(nullptr), Function(nullptr) {}
};

typedef std::unordered_map<const char*, FCDynamicClassDesc*, FCStringHash, FCStringEqual>   CDynamicClassNameMap;
typedef std::unordered_map<int, FCDynamicClassDesc*>   CDynamicClassIDMap;
typedef std::unordered_map<UStruct*, FCDynamicClassDesc*>   CDynamicUStructMap;
typedef std::unordered_map<FProperty*, FCDynamicClassDesc*>   CDynamicPropertyMap;
typedef std::vector<FNativeOverridenFnctionInfo>  COverridenFunctionList;

struct FCScriptContext
{
	bool                  m_bInit;
	int64                 m_ScriptVM;
	int64                 m_TempParamPtr;
	int64                 m_TempValuePtr;
	int                   m_TempParamIndex;

	CDynamicClassNameMap  m_ClassNameMap;   // name == > class ptr
	CDynamicUStructMap    m_StructMap;      // UStruct* ==> class ptr
	CDynamicClassIDMap    m_ClassIDMap;     // wrap class id ==> class ptr
    CDynamicPropertyMap   m_PropeytyMap;    // FPropery ==> class ptr

    FCObjectReferencer* m_ManualObjectReference;
    COverridenFunctionList   m_OveridenFunctionList;

	FCScriptContext():m_bInit(false), m_ScriptVM(0), m_TempParamPtr(0), m_TempValuePtr(0), m_TempParamIndex(0), m_ManualObjectReference(nullptr)
	{
	}
	
	FCDynamicClassDesc*  RegisterWrapClass(const char *UEClassName, int nClassID);
	FCDynamicClassDesc*  RegisterUClass(const char *UEClassName);
	FCDynamicClassDesc*  RegisterUStruct(UStruct *Struct);
    FCDynamicClassDesc*  RegisterByProperty(FProperty* Property);
    int GetMemSize() const;
    int GetClassMemSize(const char* InClassName) const;
    void Init();
	void Clear();
    void AddOverridenFunction(UClass* InClass, UFunction* Func);
    void ClearAllOvrridenFunction();
    void RemoveOverideFunction(UClass* InClass, UFunction* Func);

	FCDynamicClassDesc  *FindClassByName(const char *ScriptClassName)
	{
		CDynamicClassNameMap::iterator itClass = m_ClassNameMap.find(ScriptClassName);
		if(itClass != m_ClassNameMap.end())
		{
			return itClass->second;
		}
		return nullptr;
	}
	FCDynamicClassDesc  *FindClassByID(int nClassID)
	{
		CDynamicClassIDMap::iterator itClass = m_ClassIDMap.find(nClassID);
		if(itClass != m_ClassIDMap.end())
		{
			return itClass->second;
		}
		return nullptr;
	}
};

struct FCContextManager
{
	FCScriptContext   ClientDSContext;  // Client + DS, 不再区分DS
	static  FCContextManager  *ConextMgrInsPtr;
	FCContextManager();
	~FCContextManager();
    void Init();
	void Clear();
};

inline FCContextManager *GetContextManger()
{
	return FCContextManager::ConextMgrInsPtr;
}

inline FCScriptContext  *GetClientScriptContext()
{
	return &(FCContextManager::ConextMgrInsPtr->ClientDSContext);
}

inline FCScriptContext  *GetScriptContext()
{
	return &(FCContextManager::ConextMgrInsPtr->ClientDSContext);
}
