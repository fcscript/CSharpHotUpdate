#pragma once

#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "CoreUObject.h"

#include "../../FCLib/include/fc_api.h"

FCSCRIPT_API void* FC_GetArgValue_CppPtr(fc_intptr L, fc_intptr ParamPtr);
FCSCRIPT_API void  FC_GetArgValue_ByName(fc_intptr L, fc_intptr ParamPtr, void* ValueAddr, const char* ClassName);

namespace FCScript
{
	template<class _Ty>
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, _Ty & Value)
	{
		Value = _Ty(); // default(_Ty);
    }
    template<class _Ty>
    FORCEINLINE void GetArgValue(fc_intptr L, int Index, _Ty*& Value)
    {
		fc_intptr ParamPtr = fc_get_wrap_objptr(L, Index);
		Value = FC_GetArgValue_CppPtr(L, ParamPtr);
    }
    template<class _Ty>
    FORCEINLINE void GetArgValue(fc_intptr L, int Index, const _Ty*& Value)
    {
        fc_intptr ParamPtr = fc_get_wrap_objptr(L, Index);
        Value = FC_GetArgValue_CppPtr(L, ParamPtr);
    }

	FORCEINLINE void GetArgValue(fc_intptr L, int Index, int8 & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_byte(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, bool & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_bool(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, char & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_char(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, int16 & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_short(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, uint16 & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_ushort(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, int & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_int(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, uint32 & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_uint(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, int64 & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_int64(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, float & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_float(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, double & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		Value = fc_get_value_float(ParamPtr);
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, FString & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		fc_intptr VM = fc_get_vm_ptr(L);
		TCHAR *pStrParam = (TCHAR *)fc_cpp_get_value_string_w(VM, ParamPtr);
		Value = pStrParam ? pStrParam : TEXT("");
	}
	FORCEINLINE void GetArgValue(fc_intptr L, int Index, FName & Value)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		fc_intptr VM = fc_get_vm_ptr(L);
		TCHAR* pStrParam = (TCHAR *)fc_cpp_get_value_string_w(VM, ParamPtr);
		Value = pStrParam ? pStrParam : TEXT("");
	}
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FRotator & Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FRotator");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FQuat& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FQuat");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FTransform& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FTransform");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FLinearColor& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FLinearColor");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FColor& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FColor");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FPlane& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FPlane");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FVector& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FVector");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FVector2D& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FVector2D");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FVector4& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FVector4");
    }
    FORCEINLINE void GetArgValue(fc_intptr L, fc_intptr ParamPtr, FGuid& Value)
    {
		FC_GetArgValue_ByName(L, ParamPtr, &Value, "FGuid");
    }
}