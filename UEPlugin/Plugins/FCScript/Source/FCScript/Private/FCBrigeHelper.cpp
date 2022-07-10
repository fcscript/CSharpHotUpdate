#include "FCBrigeHelper.h"
#include "FCGetObj.h"

typedef stdext::hash_map<int, FCExportedClass*>   CExportClassIDMap;
FCExportedClass* FCExportedClass::s_pExportedIns = nullptr;
CExportClassIDMap  sExportClassMap;

FCExportedClass* FCExportedClass::FindExportedClass(const char* InClassName)
{
	FCExportedClass  *ClassPtr = s_pExportedIns;
	while(ClassPtr)
	{
		if(ClassPtr->ClassName == InClassName || strcmp(ClassPtr->ClassName, InClassName) == 0)
		{
			return ClassPtr;
		}
		ClassPtr = ClassPtr->NextClass;
	}
	return nullptr;
}

void FCExportedClass::RegisterAll(fc_intptr VM)
{
	FCExportedClass* ClassPtr = s_pExportedIns;
	while (ClassPtr)
	{
		ClassPtr->Register(VM);
		ClassPtr = ClassPtr->NextClass;
	}
}

void FCExportedClass::UnRegisterAll(fc_intptr VM)
{
	FCExportedClass* ClassPtr = s_pExportedIns;
	while (ClassPtr)
	{
		ClassPtr->UnRegister(VM);
		ClassPtr = ClassPtr->NextClass;
	}
}

void* FCExportedClass::GetThisPtr(fc_intptr L, const char* InClassName)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	fc_intptr testPtr = fc_get_value_wrap_objptr(L); // 这个应该是非法的调用
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if(ObjRef && ObjRef->RefType == EFCObjRefType::CppPtr)
	{
		return ObjRef->GetThisAddr();
	}
	return nullptr;
}

void FCExportedClass::Register(fc_intptr VM)
{
	if(!Propertys && !Functions)
    {
        InitFunctionList();
    }
    int nClassName = fc_get_inport_class_id(VM, ClassName);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_del(VM, nClassName,obj_del);
    fc_register_class_release_ref(VM, nClassName, obj_release);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);
    // 注册属性
	ChildNumb = 0;
	ChildItemPtr = nullptr;
	FCExportProperty  *Propety = Propertys;
	while(Propety)
    {
        ++ChildNumb;
        fc_register_class_attrib(VM, nClassName, Propety->Name, GetAttrib_wrap, SetAttrib_wrap);
		Propety = (FCExportProperty*)Propety->Next;
	}

	// 注册方法
	FCExportFunction *Function = Functions;
	while(Function)
    {
        ++ChildNumb;
		fc_register_class_func(VM, nClassName, Function->Name, Function_wrap);
		Function = (FCExportFunction *)Function->Next;
	}
}

void FCExportedClass::UnRegister(fc_intptr VM)
{
	ReleaseList(Propertys);
	ReleaseList(Functions);
	Propertys = nullptr;
	Functions = nullptr;
	ChildNumb = 0;
	if(ChildItemPtr)
	{
		delete []ChildItemPtr;
		ChildItemPtr = nullptr;
	}
}

void  FCExportedClass::AddChildItem(const FCExportedItem* ChildItem, int InFuncName)
{
    if (!ChildItemPtr && ChildNumb > 0)
    {
		int nMaxNumb = ChildNumb + 1;
		ChildItemPtr = new FCExportedItem* [nMaxNumb];
		for(int i = 0; i< nMaxNumb; ++i)
		{
			ChildItemPtr[i] = nullptr;
		}
    }
    if (ChildItemPtr != nullptr && InFuncName >= 0 && InFuncName <= ChildNumb)
    {
        ChildItemPtr[InFuncName] = (FCExportedItem *)ChildItem;
    }
}

void FCExportedClass::ReleaseList(FCExportedItem* InList)
{
	FCExportedItem *Item = nullptr;
	while(InList)
	{
		Item = InList;
		InList = InList->Next;
		delete Item;
	}
}

const FCExportedItem* FCExportedClass::FindChildFromList(const FCExportedItem* InListPtr, const char* InName)
{
	while (InListPtr)
	{
		if (InListPtr->Name == InName || strcmp(InListPtr->Name, InName) == 0)
		{
			return InListPtr;
		}
		InListPtr = InListPtr->Next;
	}
	return nullptr;
}

FCExportedClass  *FindClassByScriptStack(fc_intptr L)
{
    int nClassName = fc_get_current_call_class_name_id(L);
    FCExportedClass* ExportClass = sExportClassMap[nClassName];
    if (!ExportClass)
    {
        const char* ClassName = fc_cpp_get_current_call_class_name(L);  // 这个只是单纯的UEClass，不是脚本的，这里创建的也是UE对象
        ExportClass = FCExportedClass::FindExportedClass(ClassName);
        sExportClassMap[nClassName] = ExportClass;
    }
	return ExportClass;
}

int FCExportedClass::obj_new(fc_intptr L)
{
    return 0;
}

int FCExportedClass::obj_del(fc_intptr nIntPtr)
{
    FCGetObj::GetIns()->DeleteValue(nIntPtr);
	return 0;
}

int FCExportedClass::obj_release(fc_intptr nIntPtr)
{
    FCGetObj::GetIns()->ReleaseValue(nIntPtr);
    return 0;
}

int FCExportedClass::obj_hash(fc_intptr nIntPtr)
{
    return FCGetObj::GetIns()->GetValueHash(nIntPtr);
}

bool FCExportedClass::obj_equal(fc_intptr L, fc_intptr R)
{
    return FCGetObj::GetIns()->EqualValue(L, R);
}

const FCExportedItem*FindExportChild(FCExportedClass* ExportClass, fc_intptr L, bool bFunction)
{
    int nFuncName = fc_get_current_call_class_function_name_id(L);
    const FCExportedItem* ChildItem = ExportClass->GetChildItem(nFuncName);
    if (!ChildItem)
    {
        const char* FunctionName = fc_cpp_get_current_call_class_function_name(L);
		if(bFunction)
			ChildItem = ExportClass->FindClassFunction(FunctionName);
		else
			ChildItem = ExportClass->FindClassProperty(FunctionName);
        ExportClass->AddChildItem(ChildItem, nFuncName);
    }
	return ChildItem;
}

int FCExportedClass::GetAttrib_wrap(fc_intptr L)
{
    FCExportedClass* ExportClass = FindClassByScriptStack(L);
	if(ExportClass)
    {
		const FCExportedItem* Property = FindExportChild(ExportClass, L, false);
		if(Property)
		{
			Property->Read(L);
		}
	}
    return 0;
}

int FCExportedClass::SetAttrib_wrap(fc_intptr L)
{
    FCExportedClass* ExportClass = FindClassByScriptStack(L);
    if (ExportClass)
    {
        const FCExportedItem* Property = FindExportChild(ExportClass, L, false);
        if (Property)
        {
            Property->Write(L);
        }
    }
	return 0;
}

int FCExportedClass::Function_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    int nFuncName = fc_get_current_call_class_function_name_id(L);
	FCExportedClass* ExportClass = FindClassByScriptStack(L);
	if(ExportClass)
    {
        const FCExportedItem* Function = FindExportChild(ExportClass, L, true);
		if(Function)
			Function->Invoke(L);
	}
	return 0;
}
