#include "FCBrigeHelper.h"
#include "FCGetObj.h"

FCExportedClass* FCExportedClass::s_pExportedIns = nullptr;

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
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if(ObjRef)
	{
		
	}
	return nullptr;
}

void FCExportedClass::Register(fc_intptr VM)
{
	if(Propertys || Functions)
	{
		return ;
	}
	InitFunctionList();
}

void FCExportedClass::UnRegister(fc_intptr VM)
{
	ReleaseList(Propertys);
	ReleaseList(Functions);
	Propertys = nullptr;
	Functions = nullptr;
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
