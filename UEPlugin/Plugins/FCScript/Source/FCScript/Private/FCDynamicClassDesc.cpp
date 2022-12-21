
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
	Flags = InProperty->GetPropertyFlags();
	Property = InProperty;
	bOuter = InProperty->HasAnyPropertyFlags(CPF_OutParm);

	Type = GetScriptPropertyType(Property);

	InitDynamicPropertyWriteFunc(this, Type);
	InitDynamicPropertyReadFunc(this, Type);
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


#if OLD_UE_ENGINE
struct FFakeProperty : public UField
#else
struct FFakeProperty : public FField
#endif
{
    int32       ArrayDim;
    int32       ElementSize;
    uint64      PropertyFlags;
    uint16      RepIndex;
    TEnumAsByte<ELifetimeCondition> BlueprintReplicationCondition;
    int32       Offset_Internal;
};

#define fc_offsetof(s,m) ((int32)((size_t)&(((s*)0)->m)))

UFunction* DuplicateUFunction(UFunction *TemplateFunction, UClass *OuterClass, const FName &NewFuncName)
{
    int32 Offset = fc_offsetof(FFakeProperty, Offset_Internal);
    FArchive Ar;         // dummy archive used for FProperty::Link()

#if CLEAR_INTERNAL_NATIVE_FLAG_DURING_DUPLICATION
    FObjectDuplicationParameters DuplicationParams(TemplateFunction, OuterClass);
    DuplicationParams.DestName = NewFuncName;
    DuplicationParams.InternalFlagMask &= ~EInternalObjectFlags::Native;
    UFunction *NewFunc = Cast<UFunction>(StaticDuplicateObjectEx(DuplicationParams));
#else
    UFunction *NewFunc = DuplicateObject(TemplateFunction, OuterClass, NewFuncName);
#endif

    NewFunc->PropertiesSize = TemplateFunction->PropertiesSize;
    NewFunc->MinAlignment = TemplateFunction->MinAlignment;
    int32 NumParams = NewFunc->NumParms;
    if (NumParams > 0)
    {
        NewFunc->PropertyLink = CastField<FProperty>(GetChildProperties(NewFunc));
        FProperty *SrcProperty = CastField<FProperty>(GetChildProperties(TemplateFunction));
        FProperty *DestProperty = NewFunc->PropertyLink;
        while (true)
        {
            DestProperty->Link(Ar);
            DestProperty->RepIndex = SrcProperty->RepIndex;
            *((int32*)((uint8*)DestProperty + Offset)) = *((int32*)((uint8*)SrcProperty + Offset)); // set Offset_Internal (Offset_Internal set by DestProperty->Link(Ar) is incorrect because of incorrect Outer class)
            if (--NumParams < 1)
            {
                break;
            }
            DestProperty->PropertyLinkNext = CastField<FProperty>(DestProperty->Next);
            DestProperty = DestProperty->PropertyLinkNext;
            SrcProperty = SrcProperty->PropertyLinkNext;
        }
    }
    NewFunc->ClearInternalFlags(EInternalObjectFlags::Native);
	NewFunc->Script.Empty();
    return NewFunc;
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
		//OnRegisterStruct(Super);
	}
	m_Class = Cast<UClass>(Struct);

	OnAddStructMember(Struct, Context);

    FCScriptContext* ScriptContext = (FCScriptContext*)Context;
    UStruct* SuperStruct = Struct->GetSuperStruct();
	if(SuperStruct)
	{
		m_Super = ScriptContext->RegisterUStruct(SuperStruct);
	}
}

void  FCDynamicClassDesc::OnAddStructMember(UStruct* Struct, void* Context)
{
    // 注册成员变量
    const FProperty* PropertyLink = Struct->PropertyLink;
    const FProperty* Property = nullptr;
    for (; PropertyLink != nullptr; PropertyLink = PropertyLink->PropertyLinkNext)
    {
        Property = PropertyLink;

        FCDynamicProperty* FCProperty = new FCDynamicProperty();
        FCProperty->InitProperty(Property);
        FCProperty->PropertyIndex = m_Property.size();
        FCProperty->bOuter = false;

        m_Property.push_back(FCProperty);
        m_Name2Property[FCProperty->Name] = FCProperty;
    }
    // 注册一下原生的函数
    UField* Children = Struct->Children;
    for (; Children != nullptr; Children = Children->Next)
    {
        UFunction* Function = Cast<UFunction>(Children);
        if (Function)
        {
            FCDynamicFunction* DynamicFunction = new FCDynamicFunction();
            DynamicFunction->InitParam(Function);
            m_Functions[DynamicFunction->Name] = DynamicFunction;
        }
    }
	//UStruct *SuperStruct = Struct->GetSuperStruct();
	//if(SuperStruct)
	//{
	//	OnAddStructMember(SuperStruct, Context);
	//}
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
	//DynamicFunction->Name = pcsFuncName;
	m_Functions[DynamicFunction->Name] = DynamicFunction;

	return DynamicFunction;
}

// 功能：注册一个类的属性
FCDynamicProperty*  FCDynamicClassDesc::RegisterProperty(const char *pcsPropertyName, int nNameID)
{
	CDynamicID2Property::iterator itProperty = m_ID2Property.find(nNameID);
	if(itProperty != m_ID2Property.end())
	{
		return itProperty->second;
	}

	FCDynamicProperty  *DynamicProperty = FindAttribByName(pcsPropertyName);
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

void FCScriptContext::Clear()
{
	m_bInit = false;
	if(m_ScriptVM)
	{
		fc_release(m_ScriptVM);
		m_ScriptVM = 0;
	}
	ReleasePtrMap(m_ClassNameMap);
	m_StructMap.clear();
    m_PropeytyMap.clear();
	m_ClassIDMap.clear();
	m_TempParamPtr = 0;
	m_TempValuePtr = 0;
	m_TempParamIndex = 0;
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

void  FCContextManager::Clear()
{
	ClientDSContext.Clear();
}

FCContextManager  GContextNgr;
