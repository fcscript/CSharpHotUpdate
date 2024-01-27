#include "FCTestB.h"

#include "FCObjectManager.h"
#include "FCGetObj.h"
#include "FCTemplateType.h"

//--------------------------测试代码-------------------------------------
int64  TestCreateObject(const char* ClassName, int nClassName)
{
	FCDynamicClassDesc* ClassDesc = GetScriptContext()->FindClassByID(nClassName);
	if (!ClassDesc)
	{
		ClassDesc = GetScriptContext()->RegisterUClass(ClassName);
	}
	int64 ObjID = 0;
	if (ClassDesc)
	{
		if (ClassDesc->m_Class)
		{
			UObject* Outer = (UObject*)GetTransientPackage();
			ObjID = FCGetObj::GetIns()->PushNewObject(ClassDesc, NAME_None, Outer, 0, 0);
		}
		else if (ClassDesc->m_Struct)
		{
			ObjID = FCGetObj::GetIns()->PushNewStruct(ClassDesc);
		}
	}
	return ObjID;
}

FCDynamicProperty* GetObjAttrib(FCObjRef*& ObjRef, int64 ObjID, const char* AttribName)
{
	ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	if (!ObjRef)
	{
		return nullptr;
	}
	FCDynamicClassDesc* ClassDesc = ObjRef->ClassDesc;
	FCDynamicProperty* DynamicProperty = nullptr;
	if (ObjRef->RefType == EFCObjRefType::NewUObject)
	{
		DynamicProperty = ClassDesc->FindAttribByName(AttribName);
	}
	else if (ObjRef->RefType == EFCObjRefType::NewUStruct)
	{
		DynamicProperty = ClassDesc->FindAttribByName(AttribName);
	}
	return DynamicProperty;
}

uint8* GetObjAttribAddr(int64 ObjID, const char* AttribName)
{
	FCObjRef* ObjRef = nullptr;
	FCDynamicProperty* DynamicProperty = GetObjAttrib(ObjRef, ObjID, AttribName);
	if (DynamicProperty)
	{
		uint8* ObjAddr = (uint8*)(ObjRef->GetPropertyAddr());
		uint8* ValueAddr = ObjAddr + DynamicProperty->Offset_Internal;
		return ValueAddr;
	}
	return nullptr;
}

template <class _Ty>
void   WriteAny(int64 ObjID, const char* AttribName, const _Ty& v)
{
	uint8* ValueAddr = GetObjAttribAddr(ObjID, AttribName);
	if (ValueAddr)
	{
		*((_Ty*)ValueAddr) = v;
	}
}

void   WriteVec3(int64 ObjID, const char* AttribName, const FVector& v)
{
	WriteAny(ObjID, AttribName, v);
}

void   WriteUObject(int64 ObjID, const char* AttribName, UObject* v)
{
	FCObjRef* ObjRef = nullptr;
	FCDynamicProperty* DynamicProperty = GetObjAttrib(ObjRef, ObjID, AttribName);
	if (DynamicProperty)
	{
		uint8* ObjAddr = (uint8*)(ObjRef->GetPropertyAddr());
		if (!ObjAddr)
		{
			return;
		}
		uint8* ValueAddr = ObjAddr + DynamicProperty->Offset_Internal;
		UStruct* Struct = DynamicProperty->SafePropertyPtr->GetOwnerStruct();
		UClass* InClass = UObject::StaticClass();
		if (Struct == InClass || Struct->IsChildOf(InClass))
		{
			*((UObject**)ValueAddr) = v;
		}
		else
		{
			*((UObject**)ValueAddr) = nullptr;
		}
	}
}
void   WriteIntArray(int64 ObjID, const char* AttribName, int* aInt, int Numb)
{
	uint8* ValueAddr = GetObjAttribAddr(ObjID, AttribName);
	if (ValueAddr)
	{
		memcpy(ValueAddr, aInt, sizeof(int) * Numb);
	}
}

template <class _Ty>
void   ReadAny(int64 ObjID, const char* AttribName, _Ty& v)
{
	uint8* ValueAddr = GetObjAttribAddr(ObjID, AttribName);
	if (ValueAddr)
	{
		v = *((_Ty*)ValueAddr);
	}
}

void   ReadVec3(int64 ObjID, const char* AttribName, FVector& v)
{
	ReadAny(ObjID, AttribName, v);
}

void   ReadUObject(int64 ObjID, const char* AttribName, UObject*& v)
{
	FCObjRef* ObjRef = nullptr;
	FCDynamicProperty* DynamicProperty = GetObjAttrib(ObjRef, ObjID, AttribName);
	if (DynamicProperty)
	{
		uint8* ObjAddr = (uint8*)(ObjRef->GetPropertyAddr());
		if (!ObjAddr)
		{
			v = nullptr;
			return;
		}
		uint8* ValueAddr = ObjAddr + DynamicProperty->Offset_Internal;
		UStruct* Struct = DynamicProperty->SafePropertyPtr->GetOwnerStruct();
		if (v)
		{
			if (Struct == v->GetClass() || Struct->IsChildOf(v->GetClass()))
			{
				v = *((UObject**)ValueAddr);
			}
			else
			{
				v = nullptr;
			}
		}
		else
		{
			v = nullptr;
		}
	}
}

void  BindScriptCallback(int64 ObjID, const char* AttribName)
{
	FCObjRef* ObjRef = nullptr;
	FCDynamicProperty* DynamicProperty = GetObjAttrib(ObjRef, ObjID, AttribName);
	if (DynamicProperty)
	{
		UObject* Object = ObjRef->GetUObject();
		FFCObjectdManager::GetSingleIns()->RegisterScriptDelegate(Object, DynamicProperty, ObjID, 0, 0);
	}
}

void  PushStringToArray(int64 ObjID, const char* AttribName, const char* InStr)
{
	FString InName = UTF8_TO_TCHAR(InStr);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	if (ObjRef)
	{
		//FArrayProperty  *ArrayProperty = 
		int ElementSize = sizeof(FString);
		FScriptArray* ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
		int Index = ScriptArray->Num();
        ScriptArray_Add(ScriptArray, 1, ElementSize);
		uint8* ObjAddr = (uint8*)ScriptArray->GetData();
		uint8* ValudAddr = ObjAddr + Index * ElementSize;
		new(ValudAddr) FString(InName);
	}
}

void   TestDynamicObject()
{
	int64 ObjID1 = TestCreateObject("UFCTest", 100);
	int64 ObjID2 = TestCreateObject("FTestBoneAdjustItemInfo", 200);
	int64 ObjID3 = TestCreateObject("FTestAvatarSystemInitParams", 300);
	int64 ObjID4 = TestCreateObject("AActor", 400);
	int64 ObjID5 = TestCreateObject("UFCTestB", 500);
	int64 ObjID6 = TestCreateObject("UFCTest", 100);

	UObject* Obj1 = FCGetObj::GetIns()->GetUObject(ObjID1);
	UObject* Obj11 = FCGetObj::GetIns()->GetUObject(ObjID4);
	UObject* Obj12 = FCGetObj::GetIns()->GetUObject(ObjID5);
	UObject* Obj13 = FCGetObj::GetIns()->GetUObject(ObjID6);

	FTestBoneAdjustItemInfo* Obj2 = (FTestBoneAdjustItemInfo*)FCGetObj::GetIns()->GetValuePtr(ObjID2);
	FTestAvatarSystemInitParams* Obj3 = (FTestAvatarSystemInitParams*)FCGetObj::GetIns()->GetValuePtr(ObjID3);

	PushStringToArray(ObjID3, "HideBoneWhiteList", "abcd");
	PushStringToArray(ObjID3, "HideBoneWhiteList", "1234");

	FString ObjName = Obj1->GetClass()->GetName();  // FCTest 少了一个U

	FVector  v1, v2, v3, v4;
	int aID[3] = { 2, 4, 8 };
	WriteVec3(ObjID1, "Pos", FVector(10, 2, 3));
	WriteVec3(ObjID2, "Scale", FVector(2, 2, 2));
	WriteVec3(ObjID3, "Offset", FVector(20, 20, 20));
	WriteUObject(ObjID1, "NextPtr", Obj1);
	WriteUObject(ObjID1, "NextPtr", Obj11);
	WriteUObject(ObjID1, "NextPtr", Obj12);
	WriteIntArray(ObjID1, "aID", aID, 3);

	UObject* Ptr5 = nullptr;

	ReadVec3(ObjID1, "Pos", v1);
	ReadVec3(ObjID2, "Scale", v2);
	ReadVec3(ObjID3, "Offset", v3);
	ReadUObject(ObjID1, "NextPtr", Ptr5);

	UFCTest* Test1 = Cast<UFCTest>(Obj1);
	UFCTest* Test2 = Cast<UFCTest>(Obj13);
	BindScriptCallback(ObjID1, "OnClicked");
	BindScriptCallback(ObjID6, "OnClicked");
	Test1->OnClicked.Broadcast();
	Test2->OnClicked.Broadcast();

	BindScriptCallback(ObjID1, "OnResponseMessage");
	BindScriptCallback(ObjID6, "OnResponseMessage");
	Test1->HttpNotify(TEXT("ABCDEF"), true);
	Test2->HttpNotify(TEXT("Welcome to here"), true);

	//FVector  v(1.0f, 2.0f, 3.0f);
	//FCScriptContext *Context = GetClientScriptContext();
	//FCDynamicClassDesc *DynamicClass = Context->RegisterUClass("UFCTest");
	//if(DynamicClass)
	//{
	//	FCDynamicFunction *DynamicFunction = DynamicClass->RegisterUEFunc("NotifyAll");
	//	if(DynamicFunction)
	//	{
	//		UFunction *Function = DynamicFunction->Function;
	//		FCUEFunctionStack FuncionStack(DynamicFunction);
	//		char   buffer[64];
	//		FuncionStack.m_ParamBuffer = buffer;
	//		PushAnyUEParam(&FuncionStack, 1, v);
	//		FFrame  Stack(nullptr, Function, buffer, nullptr, nullptr);
	//		Function->Invoke(nullptr, Stack, nullptr);
	//	}
	//}

	int iiii = 0;
}
