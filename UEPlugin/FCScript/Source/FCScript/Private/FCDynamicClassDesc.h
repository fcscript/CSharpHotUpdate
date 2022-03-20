#pragma once
#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "UObject/Class.h"
#include "Logging/LogCategory.h"
#include "Logging/LogMacros.h"
#include "CoreUObject.h"
#include "FCPropertyType.h"
#include "../../FCLib/include/fc_api.h"
#include "FCStringCore.h"

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
	std::string Name;        // 调试时用的(常引用)
	
	FCPropertyType    Type;       // 类型
	EPropertyFlags    Flags;      // 属性类型（用于强制转换的检测)
	const FProperty  *Property;
	bool              bRef;       // 是不是引用类型
	bool              bOuter;     // 是不是输出类型
	
	FCDynamicPropertyBase() :ElementSize(0), Offset_Internal(0), PropertyIndex(0), ScriptParamIndex(0), Type(FCPropertyType::FCPROPERTY_Unkonw), Flags(CPF_None), Property(nullptr), bRef(false), bOuter(false)
	{
	}
	bool  IsRef() const
	{
		return bRef;
	}
	bool  IsOuter() const
	{
		return bOuter;
	}
	// 功能：得到委托的触发函数
	UFunction *GetSignatureFunction() const
	{		
		if(FCPropertyType::FCPROPERTY_MulticastDelegateProperty == Type)
		{
			FMulticastDelegateProperty* DelegateProperty = (FMulticastDelegateProperty*)Property;
			return DelegateProperty->SignatureFunction;
		}
		else if(FCPROPERTY_DelegateProperty == Type)
		{
			FDelegateProperty* DelegateProperty = (FDelegateProperty*)Property;
			return DelegateProperty->SignatureFunction;
		}
		return nullptr;
	}
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
};

// 复制一个函数
UFunction* DuplicateUFunction(UFunction *TemplateFunction, UClass *OuterClass, const FName &NewFuncName);

struct  FCDynamicFunction
{
	UFunction  *Function;
	FNativeFuncPtr  OleNativeFuncPtr;  // 原始的NativeFunc函数
	int     FuncID;           // 函数ID
	int     ReturnPropertyIndex;
	int     ParmsSize;        // 参数序列化后的字节大小
	int     ParamCount;       // 函数参数个数(不包括返回值)
	bool    bOverride;
	bool    bOuter;
	bool    bRegister;        // 是不是在类中注册了
	bool    bDelegate;
	std::string Name;        // 函数名
	std::vector<FCDynamicProperty>   m_Property;
	FCDynamicFunction():Function(nullptr), OleNativeFuncPtr(nullptr), FuncID(0), ReturnPropertyIndex(-1), ParmsSize(0), ParamCount(0), bOverride(false), bOuter(false), bRegister(false), bDelegate(false)
	{
	}
	void  InitParam(UFunction *InFunction);
};

struct FCDelegateInfo
{
	FCDynamicFunction*    DynamicFunc;
	FCDynamicProperty*    DynamicProperty;
	fc_intptr     ThisPtr;  // 脚本的This指针
	int           ClassNameID; // 类名
	int           FunctionNameID; // 类的委托的函数名
	FCDelegateInfo():DynamicFunc(nullptr), DynamicProperty(nullptr), ThisPtr(0), ClassNameID(0), FunctionNameID(0){}
	FCDelegateInfo(const FCDynamicFunction *InDynamicFunc, const FCDynamicProperty *InDynamicProperty, fc_intptr InThisPtr, int InClassNameID, int InFuncNameID)
		:DynamicFunc((FCDynamicFunction*)InDynamicFunc)
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

typedef  stdext::hash_map<const char*, FCDynamicProperty*>   CDynamicName2Property;
typedef  stdext::hash_map<int, FCDynamicProperty*>   CDynamicID2Property;
typedef  std::vector<FCDynamicProperty*>   CDynamicPropertyPtrArray;

typedef  stdext::hash_map<int, FCDynamicFunction*>   CDynamicFunctionIDMap; // id == > function
typedef  stdext::hash_map<const char*, FCDynamicFunction*>   CDynamicFunctionNameMap;  // name ==> function

const char* GetUEClassName(const char* InName);

// 一个动态类的数据结构
struct FCDynamicClassDesc
{
	UStruct                     *m_Struct;
	UClass                      *m_Class;
	FCDynamicClassDesc          *m_Super;
	int                          m_nClassNameID; // 在脚本中的Wrap class ID, 唯一的
	EClassCastFlags              m_ClassFlags;  // 用于强制转换的检测
	std::string                  m_SuperName;
	std::string                  m_UEClassName; // 类名，wrap的类名
	FName                        m_ClassName;
	CDynamicPropertyPtrArray     m_Property;  // 属性
	CDynamicName2Property        m_Name2Property;  // 所有的属性

	CDynamicFunctionNameMap      m_Functions;   // 所有的函数 name ==> function
	
	// ------- 脚本中的的属性ID或函数ID
	CDynamicFunctionIDMap        m_ID2Function;    // id ==> function
	CDynamicID2Property          m_ID2Property;    // id ==> Property
	// ------- 脚本中的的属性ID或函数ID

	FCDynamicClassDesc():m_Struct(nullptr), m_Class(nullptr), m_Super(nullptr), m_nClassNameID(0), m_ClassFlags(CASTCLASS_None)
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

	// 功能：注册一个函数
	FCDynamicFunction*  RegisterUEFunc(const char *pcsFuncName);
	
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
		return nullptr;
	}
	FCDynamicProperty *FindAttribByID(INT nAttribNameID)
	{
		CDynamicID2Property::iterator itAttrib = m_ID2Property.find(nAttribNameID);
		if(itAttrib != m_ID2Property.end())
		{
			return itAttrib->second;
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
		return nullptr;
	}
	FCDynamicProperty *FindAttribByName(const char *AttribName)
	{
		CDynamicName2Property::iterator itAttrib = m_Name2Property.find(AttribName);
		if(itAttrib != m_Name2Property.end())
		{
			return itAttrib->second;
		}
		return nullptr;
	}
};

typedef stdext::hash_map<std::string, FCDynamicClassDesc*>   CDynamicClassNameMap;
typedef stdext::hash_map<int, FCDynamicClassDesc*>   CDynamicClassIDMap;
typedef stdext::hash_map<UStruct*, FCDynamicClassDesc*>   CDynamicUStructMap;

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

	FCScriptContext():m_bInit(false), m_ScriptVM(0), m_TempParamPtr(0), m_TempValuePtr(0), m_TempParamIndex(0)
	{
	}
	
	FCDynamicClassDesc*  RegisterWrapClass(const char *UEClassName, int nClassID);
	FCDynamicClassDesc*  RegisterUClass(const char *UEClassName);
	FCDynamicClassDesc*  RegisterUStruct(UStruct *Struct);
	void Clear();

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
