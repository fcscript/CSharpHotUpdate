
#include "FCDynamicClassDesc.h"
#include "UObject/Class.h"
#include "FCCallScriptFunc.h"
#include "FCDynamicOverrideFunc.h"

void  FCDynamicProperty::InitProperty(const FProperty *InProperty)
{
	Name = TCHAR_TO_UTF8(*(InProperty->GetName()));
    Name = GetConstName(Name);
	ElementSize = InProperty->ElementSize;
	Offset_Internal = InProperty->GetOffset_ForInternal();
    SafePropertyPtr = GetSafeProperty(InProperty);
	bOuter = InProperty->HasAnyPropertyFlags(CPF_OutParm);

	Type = GetScriptPropertyType(InProperty);
    ClassName = GetScriptPropertyClassName(Type, InProperty);

	InitDynamicPropertyWriteFunc(this, Type);
	InitDynamicPropertyReadFunc(this, Type);

#ifdef UE_BUILD_DEBUG
    FCStringBuffer128   TempBuffer;
    TempBuffer << ClassName << ':' << Name;
    DebugDesc = GetConstName(TempBuffer.GetString());
#endif
}

void  FCDynamicProperty::InitCppType(FCPropertyType InType, const char* InClassName, int InElementSize)
{
    Name = "";
    ElementSize = InElementSize;
    Offset_Internal = 0;
    SafePropertyPtr = nullptr;
    Type = InType;
    ClassName = GetConstName(InClassName);

    InitDynamicPropertyWriteFunc(this, Type);
    InitDynamicPropertyReadFunc(this, Type);

#ifdef UE_BUILD_DEBUG
    FCStringBuffer128   TempBuffer;
    TempBuffer << ClassName << ':' << Name << ":CppType";
    DebugDesc = GetConstName(TempBuffer.GetString());
#endif
}

void  FCDynamicFunction::InitParam(UFunction *InFunction)
{
    Name = TCHAR_TO_UTF8(*(InFunction->GetName()));
    Name = GetConstName(Name);
	Function = InFunction;
	ParmsSize = InFunction->ParmsSize;
	m_Property.resize(InFunction->NumParms);
	ReturnPropertyIndex = -1;
	bOuter = false;
	int Index = 0;
	ParamCount = 0;
	for (TFieldIterator<FProperty> It(InFunction); It && (It->PropertyFlags & CPF_Parm); ++It, ++Index)
	{
		FProperty* Property = *It;

		FCDynamicProperty* FCProperty = &(m_Property[Index]);
		FCProperty->InitProperty(Property);
		FCProperty->PropertyIndex = Index;
		FCProperty->ScriptParamIndex = Index;

		if(FCProperty->bOuter)
		{
			bOuter = true;
		}
		if(Property->HasAnyPropertyFlags(CPF_ReturnParm))
		{
			ReturnPropertyIndex = Index;
			FCProperty->ScriptParamIndex = 0;
		}
		else
		{
			++ParamCount;
		}
	}
	if(ReturnPropertyIndex != -1)
	{
		int RealReturnIndex = m_Property.size() - 1;
		if(ReturnPropertyIndex != RealReturnIndex)
		{
            std::swap(m_Property[ReturnPropertyIndex], m_Property[RealReturnIndex]);
			ReturnPropertyIndex = RealReturnIndex;
		}
	}
}

int FCDynamicDelegateList::FindDelegate(const FCDelegateInfo &Info) const
{
	for(int i = 0; i<Delegates.size(); ++i)
	{
		if(Delegates[i] == Info)
		{
			return i;
		}
	}
	return -1;
}

bool  FCDynamicDelegateList::AddScriptDelegate(const FCDelegateInfo &Info)
{
	int Index = FindDelegate(Info);
	if( Index != -1)
	{
		return false;
	}
	Delegates.push_back(Info);
	return true;
}

bool  FCDynamicDelegateList::DelScriptDelegate(const FCDelegateInfo &Info)
{
	int Index = FindDelegate(Info);
	if( Index != -1)
	{
        Delegates.erase(Delegates.begin()+Index);
		return true;
	}
	return false;
}

FCDynamicClassDesc::FCDynamicClassDesc(const FCDynamicClassDesc &other):m_Super(nullptr), m_ClassFlags(CASTCLASS_None)
{
	CopyDesc(other);
}

FCDynamicClassDesc::~FCDynamicClassDesc()
{
	Clear();
}

void FCDynamicClassDesc::Clear()
{
	for (int i = m_Property.size() - 1; i >= 0; --i)
	{
		delete (m_Property[i]);
	}
	m_Property.clear();
	m_Name2Property.clear();

	ReleasePtrMap(m_Functions);
	m_ID2Function.clear();
	m_ID2Property.clear();
}

FCDynamicClassDesc &FCDynamicClassDesc::CopyDesc(const FCDynamicClassDesc &other)
{
	if(this == &other)
	{
		return *this;
	}
	Clear();
	m_Struct = other.m_Struct;
	m_Class = other.m_Class;
    m_ScriptStruct = other.m_ScriptStruct;
	m_Super = other.m_Super;
	m_nClassNameID = other.m_nClassNameID;
	m_ClassFlags = other.m_ClassFlags;
	m_SuperName = other.m_SuperName;
	m_UEClassName = other.m_UEClassName;

	m_Property.resize(other.m_Property.size());
	m_Name2Property.clear();
	m_ID2Function.clear();
	m_ID2Property.clear();
	for(int i = 0; i<m_Property.size(); ++i)
	{
		FCDynamicProperty *FCProperty = new FCDynamicProperty(*(other.m_Property[i]));
		m_Property[i] = FCProperty;
		m_Name2Property[FCProperty->Name] = FCProperty;
	}
	m_Functions.clear();
	for(CDynamicFunctionNameMap::const_iterator it = other.m_Functions.begin(); it != other.m_Functions.end(); ++it)
	{
		FCDynamicFunction *Func = new FCDynamicFunction(*(it->second));
		m_Functions[Func->Name] = Func;
	}
	return *this;
}

void  FCDynamicClassDesc::OnRegisterStruct(UStruct *Struct, void *Context)
{
	// 先注册父对象	
	m_Struct = Struct;
	UStruct  *Super = Struct->GetSuperStruct();
	if(Super)
	{
		m_SuperName = TCHAR_TO_UTF8(*(Super->GetName()));
        m_SuperName = GetConstName(m_SuperName);
	}
	m_Class = Cast<UClass>(Struct);
    m_ScriptStruct = Cast<UScriptStruct>(m_Struct);

    FCScriptContext* ScriptContext = (FCScriptContext*)Context;
    UStruct* SuperStruct = Struct->GetSuperStruct();
	if(SuperStruct)
	{
		m_Super = ScriptContext->RegisterUStruct(SuperStruct);
	}
}

int   FCDynamicClassDesc::GetMemSize() const
{
    int MemSize = sizeof(FCDynamicClassDesc);
    for (CDynamicPropertyPtrArray::const_iterator itProperty = m_Property.begin(); itProperty != m_Property.end(); ++itProperty)
    {
        MemSize += (*itProperty)->GetMemSize();
    }
    for (CDynamicFunctionNameMap::const_iterator itFunc = m_Functions.begin(); itFunc != m_Functions.end(); ++itFunc)
    {
        MemSize += itFunc->second->GetMemSize();
    }
    return MemSize;
}

FCDynamicFunction*  FCDynamicClassDesc::RegisterUEFunc(const char *pcsFuncName)
{
	if(!m_Class)
	{
		return nullptr;
	}
	// UE的反射并不支持同名函数重载
	CDynamicFunctionNameMap::iterator itFunction = m_Functions.find(pcsFuncName);
	if (itFunction != m_Functions.end())
	{
		return itFunction->second;
	}

	FName  Name(pcsFuncName);
	UFunction* Function = m_Class->FindFunctionByName(Name);
	if (!Function)
	{
		if(m_Super)
			return m_Super->RegisterUEFunc(pcsFuncName);
		else
			return nullptr;
	}
	FCDynamicFunction* DynamicFunction = new FCDynamicFunction();
	DynamicFunction->InitParam(Function);
	m_Functions[DynamicFunction->Name] = DynamicFunction;

	return DynamicFunction;
}

// 功能：注册一个类的属性
FCDynamicProperty* FCDynamicClassDesc::RegisterProperty(const char* InPropertyName)
{
    CDynamicName2Property::iterator itAttrib = m_Name2Property.find(InPropertyName);
    if (itAttrib != m_Name2Property.end())
    {
        return itAttrib->second;
    }
    FName InFieldName(InPropertyName);
    const FProperty* Property = m_Struct->FindPropertyByName(InFieldName);
    if (Property)
    {
        FCDynamicProperty* FCProperty = new FCDynamicProperty();
        FCProperty->InitProperty(Property);
        FCProperty->PropertyIndex = m_Property.size();
        FCProperty->bOuter = false;

        const char* FieldName = FCProperty->Name;
        m_Property.push_back(FCProperty);
        m_Name2Property[FieldName] = FCProperty;
        return FCProperty;
    }
    if (m_Super)
    {
        FCDynamicProperty * FCProperty = m_Super->RegisterProperty(InPropertyName);
        if(FCProperty)
        {
            const char* FieldName = FCProperty->Name;
            m_Name2Property[FieldName] = FCProperty;
            return FCProperty;
        }
    }
    return nullptr;

}

// 功能：注册一个类的属性
FCDynamicProperty*  FCDynamicClassDesc::RegisterProperty(const char *pcsPropertyName, int nNameID)
{
	CDynamicID2Property::iterator itProperty = m_ID2Property.find(nNameID);
	if(itProperty != m_ID2Property.end())
	{
		return itProperty->second;
	}

	FCDynamicProperty  *DynamicProperty = RegisterProperty(pcsPropertyName);
	m_ID2Property[nNameID] = DynamicProperty;
	return DynamicProperty;
}
// 功能：注册一个函数
FCDynamicFunction*  FCDynamicClassDesc::RegisterFunc(const char *pcsFuncName, int nFuncID)
{
	CDynamicFunctionIDMap::iterator itFunc = m_ID2Function.find(nFuncID);
	if(itFunc != m_ID2Function.end())
	{
		return itFunc->second;
	}
	FCDynamicFunction *DynamicFunction = RegisterUEFunc(pcsFuncName);
	if (DynamicFunction)
	{
		DynamicFunction->FuncID = nFuncID;
	}
	else
	{
		UE_LOG(LogFCScript, Warning, TEXT("failed register function: %s, class name: %s"), UTF8_TO_TCHAR(pcsFuncName), UTF8_TO_TCHAR(m_UEClassName));
	}
	m_ID2Function[nFuncID] = DynamicFunction;
	return DynamicFunction;
}

//---------------------------------------------------------------------------

FCDynamicClassDesc* FCScriptContext::RegisterWrapClass(const char *UEClassName, int nClassID)
{
	CDynamicClassIDMap::iterator itIDClass = m_ClassIDMap.find(nClassID);
	if(itIDClass != m_ClassIDMap.end())
	{
		// 这个是一个错误
		return itIDClass->second;
	}	
	FCDynamicClassDesc  *DynamicClass = RegisterUClass(UEClassName);
	if (DynamicClass)
	{
		DynamicClass->m_nClassNameID = nClassID;
	}
	else
	{
		UE_LOG(LogFCScript, Warning, TEXT("failed register UE class, class name: %s"), UTF8_TO_TCHAR(UEClassName));
	}
	m_ClassIDMap[nClassID] = DynamicClass;
	return DynamicClass;
}

const char* GetUEClassName(const char* InName)
{
	const char* Name = (InName[0] == 'U' || InName[0] == 'A' || InName[0] == 'F' || InName[0] == 'E') ? InName + 1 : InName;
	return Name;
}

FCDynamicClassDesc*  FCScriptContext::RegisterUClass(const char *UEClassName)
{
	CDynamicClassNameMap::iterator itClass = m_ClassNameMap.find(UEClassName);
	if( itClass != m_ClassNameMap.end())
	{
		return itClass->second;
	}
	const TCHAR *InName = UTF8_TO_TCHAR(UEClassName);
	const TCHAR *Name = (InName[0] == 'U' || InName[0] == 'A' || InName[0] == 'F' || InName[0] == 'E') ? InName + 1 : InName;
	UStruct *Struct = FindObject<UStruct>(ANY_PACKAGE, Name);       // find first
	if (!Struct)
	{
		Struct = LoadObject<UStruct>(nullptr, Name);                // load if not found
	}
    UEClassName = GetConstName(UEClassName);
	if (!Struct)
	{
        m_ClassNameMap[UEClassName] = nullptr;
		return nullptr;
	}

	FCDynamicClassDesc *ScriptClassDesc = new FCDynamicClassDesc();
	ScriptClassDesc->m_UEClassName = UEClassName;
	ScriptClassDesc->OnRegisterStruct(Struct, this);

	m_ClassNameMap[ScriptClassDesc->m_UEClassName] = ScriptClassDesc;
	m_StructMap[Struct] = ScriptClassDesc;

	return ScriptClassDesc;
}

FCDynamicClassDesc*  FCScriptContext::RegisterUStruct(UStruct *Struct)
{
	CDynamicUStructMap::iterator itStruct = m_StructMap.find(Struct);
	if(itStruct != m_StructMap.end())
	{
		return itStruct->second;
	}
	FCScriptContext *ScriptContext = GetScriptContext();

	FString   UEClassName(TEXT("U"));
	UEClassName += Struct->GetName();
	FCDynamicClassDesc *ScriptClassDesc = new FCDynamicClassDesc();
	ScriptClassDesc->m_UEClassName = TCHAR_TO_UTF8(*UEClassName);
    ScriptClassDesc->m_UEClassName = GetConstName(ScriptClassDesc->m_UEClassName);
	ScriptClassDesc->OnRegisterStruct(Struct, this);

	m_ClassNameMap[ScriptClassDesc->m_UEClassName] = ScriptClassDesc;
	m_StructMap[Struct] = ScriptClassDesc;
	return ScriptClassDesc;
}

FCDynamicClassDesc* FCScriptContext::RegisterByProperty(FProperty* Property)
{
    CDynamicPropertyMap::iterator itClass = m_PropeytyMap.find(Property);
    if (itClass != m_PropeytyMap.end())
    {
        return itClass->second;
    }
    FFieldClass* FieldClass = Property->GetClass();
    FString  Name = FieldClass->GetName();

    FCDynamicClassDesc* ClassDesc = RegisterUClass(TCHAR_TO_UTF8(*Name));
    m_PropeytyMap[Property] = ClassDesc;

    return ClassDesc;
}

int FCScriptContext::GetMemSize() const
{
    int MemSize = 0;
    for (CDynamicClassNameMap::const_iterator itClass = m_ClassNameMap.begin(); itClass != m_ClassNameMap.end(); ++itClass)
    {
        MemSize += itClass->second->GetMemSize();
    }
    return MemSize;
}

int FCScriptContext::GetClassMemSize(const char* InClassName) const
{
    CDynamicClassNameMap::const_iterator itClass = m_ClassNameMap.find(InClassName);
    if (itClass != m_ClassNameMap.end())
    {
        return itClass->second->GetMemSize();
    }
    return 0;
}

void FCScriptContext::Init()
{
    if (!m_ManualObjectReference)
    {
        m_ManualObjectReference = new FCObjectReferencer();
        m_ManualObjectReference->SetName("FCScript_ManualReference");
    }
}

void FCScriptContext::Clear()
{
	m_bInit = false;
	if(m_ScriptVM)
	{
		fc_release(m_ScriptVM);
		m_ScriptVM = 0;
	}
    ClearAllOvrridenFunction();
	ReleasePtrMap(m_ClassNameMap);
	m_StructMap.clear();
    m_PropeytyMap.clear();
	m_ClassIDMap.clear();
	m_TempParamPtr = 0;
	m_TempValuePtr = 0;
	m_TempParamIndex = 0;

    if (m_ManualObjectReference)
    {
        m_ManualObjectReference->Clear();
        delete m_ManualObjectReference;
        m_ManualObjectReference = nullptr;
    }
}

void FCScriptContext::AddOverridenFunction(UClass* InClass, UFunction* Func)
{
    FNativeOverridenFnctionInfo Info;
    Info.Class = InClass;
    Info.Function = Func;
    m_OveridenFunctionList.push_back(Info);
    m_ManualObjectReference->Add(Func);
}

void FCScriptContext::ClearAllOvrridenFunction()
{
    for (int i = 0; i < m_OveridenFunctionList.size(); ++i)
    {
        FNativeOverridenFnctionInfo& Info = m_OveridenFunctionList[i];
        Info.Class->RemoveFunctionFromFunctionMap(Info.Function);
    }
    m_OveridenFunctionList.clear();
}

void FCScriptContext::RemoveOverideFunction(UClass* InClass, UFunction* Func)
{
    // 不能直接删除这个函数，需要删除全局的DynamicFunction,再去除GC引用
    //InClass->RemoveFunctionFromFunctionMap(Func);
}

//--------------------------------------------------------

FCContextManager* FCContextManager::ConextMgrInsPtr = nullptr;
FCContextManager::FCContextManager()
{
	ConextMgrInsPtr = this;
}

FCContextManager::~FCContextManager()
{
	ConextMgrInsPtr = nullptr;
	Clear();
}

void  FCContextManager::Init()
{
    ClientDSContext.Init();
}

void  FCContextManager::Clear()
{
	ClientDSContext.Clear();
}

FCContextManager  GContextNgr;
