#pragma once
#include "FCDynamicClassDesc.h"
#include "UObject/Class.h"

//---------------------------------------------------------------------------------
void  PushScriptDefault(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptBool(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptInt8(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptInt16(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptInt32(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptInt64(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptFloat(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptDouble(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptFString(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptFName(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptFVector(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptFVector2D(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptFVector4D(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptStruct(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptUObject(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  PushScriptCppPtr(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr);
void  PushScriptMapIterator(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr);

void  InitDynamicPropertyWriteFunc(FCDynamicProperty *DynamicProperty, FCPropertyType Flag);

//---------------------------------------------------------------------------------

void  ReadScriptDefault(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptBool(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptInt8(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptInt16(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptInt32(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptInt64(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptFloat(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptDouble(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptFString(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptFName(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptFVector(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptFVector2D(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptFVector4D(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptStruct(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptUObject(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr);
void  ReadScriptCppPtr(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr);
void  ReadScriptMapIterator(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr);

void  InitDynamicPropertyReadFunc(FCDynamicProperty *DynamicProperty, FCPropertyType Flag);

//---------------------------------------------------------------------------------
//---------------------------------------------------------------------------------

void  PushScriptValue(FCScriptContext* Context, const bool& value);
void  PushScriptValue(FCScriptContext* Context, const int8& value);
void  PushScriptValue(FCScriptContext* Context, const int16& value);
void  PushScriptValue(FCScriptContext* Context, const int32& value);
void  PushScriptValue(FCScriptContext* Context, const int64& value);
void  PushScriptValue(FCScriptContext* Context, const float& value);
void  PushScriptValue(FCScriptContext* Context, const double& value);
void  PushScriptValue(FCScriptContext* Context, const char * value);
void  PushScriptValue(FCScriptContext* Context, const FString& value);
void  PushScriptValue(FCScriptContext* Context, const FName& value);
void  PushScriptValue(FCScriptContext* Context, const FVector& value);
void  PushScriptValue(FCScriptContext* Context, const FVector2D& value);
void  PushScriptValue(FCScriptContext* Context, const FVector4& value);
void  PushScriptValue(FCScriptContext* Context, const UObject* value);

//---------------------------------------------------------------------------------

fc_intptr  QueryCallKey();

//---------------------------------------------------------------------------------

inline void PushScriptArgs(FCScriptContext* Context)
{
}
template <typename T1, typename... T2>
void PushScriptArgs(FCScriptContext* Context, T1&& V1, T2&&... V2)
{
	Context->m_TempValuePtr = fc_get_script_param(Context->m_TempParamPtr, Context->m_TempParamIndex);
	++(Context->m_TempParamIndex);
	PushScriptValue(Context, Forward<T1>(V1));
	PushScriptArgs(Context, Forward<T2>(V2)...);
}

// 将不定参数压入当前脚本虚拟机
template <typename... T>
void  PushAnyScriptParam(FCScriptContext* Context, T&&... Args)
{
	PushScriptArgs(Context, Forward<T>(Args)...);
}

// C++调用脚本函数(不定参数)
template <typename... T>
void  CallAnyScriptFunc(FCScriptContext* Context, int64 ScriptIns, const char *ScriptFuncName, T&&... Args)
{
	fc_intptr VM = Context->m_ScriptVM;
	if(VM)
	{
        fc_intptr Callkey = QueryCallKey();
		Context->m_TempParamPtr = fc_prepare_ue_call(VM, ScriptIns, ScriptFuncName, Callkey);
		Context->m_TempParamIndex = 0;
		if(Context->m_TempParamPtr)
		{
			PushScriptArgs(Context, Forward<T>(Args)...);
			fc_intptr  ReturnPtr = fc_ue_call(VM, Callkey);
			fc_end_ue_call(VM, Callkey);
		}
		else
		{
			UE_LOG(LogFCScript, Error, TEXT("Failed call script function:%s"), UTF8_TO_TCHAR(ScriptFuncName));
		}
	}
}

bool  FCCallScriptFunc(FCScriptContext *Context, UObject *Object, int64 ScriptIns, const char *ScriptFuncName, FCDynamicFunction* DynamicFunction, FFrame& TheStack);
void  FCCallScriptDelegate(FCScriptContext *Context, UObject *Object, int64 ScriptIns, int nClassNameID, int nFuncNameID, FCDynamicFunction* DynamicFunction, FFrame& TheStack);
