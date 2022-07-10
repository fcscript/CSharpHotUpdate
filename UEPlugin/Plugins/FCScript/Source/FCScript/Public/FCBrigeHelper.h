#pragma once

#include "FCBrigeBase.h"

typedef  int (*fc_export_call_back)(fc_intptr L);

struct FCExportedItem
{
	const char* Name;
	const char* ClassName;
	FCExportedItem *Next;
	FCExportedItem():Name(nullptr), ClassName(nullptr), Next(nullptr){}
	FCExportedItem(const char *InName, const char *InClassName):Name(InName), ClassName(InClassName), Next(nullptr){}
	virtual ~FCExportedItem(){}
    virtual bool IsFunction() const { return false; }
    virtual void Read(fc_intptr L) const {}
    virtual void Write(fc_intptr L) const {}
    virtual void Invoke(fc_intptr L) const {}
};

struct FCExportProperty : public FCExportedItem
{
	int          Offset;        // 偏移
	int          Aim;           // 数组长度

	FCExportProperty() :Offset(0), Aim(0){}
	FCExportProperty(const char* InPropertyName, int InOffset, int InAim, const char *InClassName) :FCExportedItem(InPropertyName, InClassName), Offset(InOffset), Aim(InAim){}
};

struct FCExportFunction : public FCExportedItem
{
	fc_export_call_back  FuncPtr;
	FCExportFunction():FuncPtr(nullptr){}
	FCExportFunction(const char *InFuncName, const char *InClassName):FCExportedItem(InFuncName, InClassName), FuncPtr(nullptr){}

	virtual void Invoke(fc_intptr L) const
	{
		if(FuncPtr)
		{
			FuncPtr(L);
		}
    }
    virtual bool IsFunction() const 
	{
		return true; 
	}
};

struct FCFuncLib_Reg
{
	const char *FuncName;
	fc_export_call_back Func;
};

struct FCSCRIPT_API FCExportedClass
{
	static FCExportedClass  *s_pExportedIns;
	FCExportedClass(const char *InName):ClassName(InName)
		, NextClass(s_pExportedIns)
		, Propertys(nullptr)
		, Functions(nullptr)
		, ChildNumb(0)
		, ChildItemPtr(nullptr)
	{
		s_pExportedIns = this;
	}

	virtual ~FCExportedClass() {}

	const char* GetName() const
	{
		return ClassName;
	}
	static FCExportedClass* FindExportedClass(const char* InClassName);
	static void RegisterAll(fc_intptr VM);
	static void UnRegisterAll(fc_intptr VM);
	static void* GetThisPtr(fc_intptr L, const char* InClassName);
	static void ReleaseList(FCExportedItem *InList);
    static const FCExportedItem* FindChildFromList(const FCExportedItem* InListPtr, const char* InName);
    static int obj_new(fc_intptr L);
    static int obj_del(fc_intptr nIntPtr);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr L, fc_intptr R);
    static int GetAttrib_wrap(fc_intptr L);
    static int SetAttrib_wrap(fc_intptr L);
    static int Function_wrap(fc_intptr L);

	virtual void InitFunctionList() {}

	void Register(fc_intptr VM);
	void UnRegister(fc_intptr VM);

	void AddClassAttrib(FCExportProperty* InProperty)
	{
		InProperty->Next = Propertys;
		Propertys = InProperty;
	}

	void AddLibFunction(const char* InName, fc_export_call_back InFunc)
	{
		if(InName && InFunc)
		{
			FCExportFunction* Func = new FCExportFunction(ClassName, ClassName);
			Func->FuncPtr = InFunc;
			Func->Next = Functions;
			Functions = Func;
		}
	}
	void AddFunctionLibs(const FCFuncLib_Reg *lib, int InFuncCount)
	{
		for(int i = 0; i<InFuncCount; ++i)
		{
			AddLibFunction(lib[i].FuncName, lib[i].Func);
		}
	}
	void AddClassFunction(FCExportFunction *Func)
	{
		Func->Next = Functions;
		Functions = Func;
	}
	const FCExportProperty* FindClassProperty(const char* InPropertyName) const
	{
		return (const FCExportProperty*)FindChildFromList(Propertys, InPropertyName);
	}
	const FCExportFunction *FindClassFunction(const char *InFuncName) const
	{
		return (const FCExportFunction*)FindChildFromList(Functions, InFuncName);
	}

	const FCExportedItem  *GetChildItem(int InFuncName) const
	{
	    if(ChildItemPtr != nullptr && InFuncName >= 0 && InFuncName <= ChildNumb)
		{
			return ChildItemPtr[InFuncName];
		}
		return nullptr;
	}

	void  AddChildItem(const FCExportedItem *ChildItem, int InFuncName);

	const char *ClassName;
	FCExportedClass* NextClass;
	FCExportProperty* Propertys;   // 属性
	FCExportFunction *Functions;   // 成员函数+全局函数

	//-----------下面的是动态注册时自动维护的变量，请在在外面修改
	int   ChildNumb;
	FCExportedItem **ChildItemPtr;
	//----------------
};

#define GetClassPropertyOffset(PropertyName)  (int)((size_t)(&((ClassType*)0)->PropertyName))

// 成员属性
template <typename ClassType, typename T>
struct TFCExportProperty : public FCExportProperty
{
	TFCExportProperty(const char* InName, int InOffset, int InAim, const char *InClassName) :FCExportProperty(InName, InOffset, InAim, InClassName)
	{
	}
	virtual void Read(fc_intptr L) const
	{
		ClassType* ThisObj = (ClassType*)FCExportedClass::GetThisPtr(L, ClassName);
		fc_intptr RetPtr = fc_get_return_ptr(L);
		if(ThisObj && RetPtr)
		{
			uint8 *ThisAddr = (uint8 *)ThisObj;
			T *Property = (T *)(ThisAddr + Offset);
			FCScript::SetArgValue(L, RetPtr, *Property);
		}
	}
	virtual void Write(fc_intptr L) const
	{
		ClassType* ThisObj = (ClassType*)FCExportedClass::GetThisPtr(L, ClassName);
		fc_intptr value_ptr = fc_get_param_ptr(L, 0);
		if (ThisObj && value_ptr)
		{
			uint8* ThisAddr = (uint8*)ThisObj;
			T* Property = (T*)(ThisAddr + Offset);
			FCScript::GetArgValue(L, 0, *Property);
		}
	}
};

// 成员函数
template <typename ClassType, typename RetType, typename... ArgType>
struct TFCExportedMemberFunction : public FCExportFunction
{
	typedef  RetType(ClassType::* LPClassMemberFuncPtrNoraml)(ArgType...);
	typedef  RetType(ClassType::* LPClassMemberFuncPtrConst)(ArgType...) const;

	TFCExportedMemberFunction(const char* InName, RetType(ClassType::* InFunc)(ArgType...), const char* InClassName) :FCExportFunction(InName, InClassName), FuncNormal(InFunc)
	{
	}
	TFCExportedMemberFunction(const char* InName, RetType(ClassType::* InFunc)(ArgType...) const, const char* InClassName) :FCExportFunction(InName, InClassName), FuncNormal((LPClassMemberFuncPtrNoraml)InFunc)
	{
	}

	virtual void Invoke(fc_intptr L) const
	{
		ClassType *ThisObj = (ClassType *)FCExportedClass::GetThisPtr(L, ClassName);
		if(ThisObj)
        {
            FCInvokeClassFunction<RetType, ClassType, ArgType...>(L, ThisObj, FuncNormal);
		}
	}

	LPClassMemberFuncPtrNoraml FuncNormal;
};


// 全局函数
template <typename RetType, typename... ArgType>
struct TFCExportedStaticMemberFunction : public FCExportFunction
{
	typedef  RetType(* LPClassMemberFuncPtrNoraml)(ArgType...);
	TFCExportedStaticMemberFunction(const char* InName, RetType(*InFunc)(ArgType...), const char * InClassName):FCExportFunction(InName, InClassName), FuncNormal(InFunc)
	{
	}
	virtual void Invoke(fc_intptr L) const
	{
		FCInvokeStaticFunction(L, FuncNormal);
	}
	LPClassMemberFuncPtrNoraml FuncNormal;
};

template <typename ClassType, typename... CtorArgType>
struct TFCExportedClass : public FCExportedClass
{
	TFCExportedClass(const char* InName):FCExportedClass(InName)
	{
	}

	bool AddBitFieldBoolProperty(const char* InName, uint8* Buffer)
	{
		return false;
	}

	template <typename T> void AddMemberProperty(const char* InName, int InOffset, T ClassType::* Property)
	{
		AddClassAttrib(new TFCExportProperty<ClassType, T>(InName, InOffset, 1, ClassName));
	}
	template <typename T, int N> void AddMemberProperty(const char* InName, int InOffset, T(ClassType::* Property)[N])
	{
		AddClassAttrib(new TFCExportProperty<ClassType, T>(InName, InOffset, N, ClassName));
	}

	template <typename RetType, typename... ArgType> void AddMemberFunction(const char* InName, RetType(ClassType::* InFunc)(ArgType...))
	{
		AddClassFunction(new TFCExportedMemberFunction<ClassType, RetType, ArgType...>(InName, InFunc, ClassName));
	}
	template <typename RetType, typename... ArgType> void AddMemberFunction(const char* InName, RetType(ClassType::* InFunc)(ArgType...) const)
	{
		AddClassFunction(new TFCExportedMemberFunction<ClassType, RetType, ArgType...>(InName, InFunc, ClassName));
	}
	template <typename RetType, typename... ArgType> void AddStaticFunction(const char* InName, RetType(*InFunc)(ArgType...))
	{
		AddClassFunction(new TFCExportedStaticMemberFunction<RetType, ArgType...>(InName, InFunc, ClassName));
	}
};

#define BEGIN_EXPORT_CLASS(Type) \
    struct FExported##Type##Helper : public TFCExportedClass<Type> \
    { \
        typedef Type ClassType; \
        FExported##Type##Helper() : TFCExportedClass(#Type) \
		{ \
		} \
        void InitFunctionList() \
        { 

#define ADD_PROPERTY(Property) \
            this->AddMemberProperty(#Property, GetClassPropertyOffset(Property), &ClassType::Property);

#define ADD_STATIC_FUNCTION_EX(FuncName, RetType, Func) \
			this->AddStaticFunction(FuncName, &ClassType::Func);

#define ADD_STATIC_FUNCTION(Func) \
			this->AddStaticFunction(#Func, &ClassType::Func);

#define ADD_FUNCTION(Func) \
			this->AddMemberFunction(#Func, &ClassType::Func);

#define ADD_LIB(libs) \
			this->AddFunctionLibs(libs, sizeof(libs)/sizeof(FCFuncLib_Reg));

#define END_EXPORT_CLASS()  \
        } \
	};

#define IMPLEMENT_EXPORTED_CLASS(Type) \
FExported##Type##Helper SExport##Type##HelperIns;
