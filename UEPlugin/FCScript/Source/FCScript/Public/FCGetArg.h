#pragma once

#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "CoreUObject.h"

#include "../../FCLib/include/fc_api.h"

namespace FCScript
{
	template<class _Ty>
	FORCEINLINE _Ty GetArgValue(fc_intptr L, int Index, _Ty)
	{
		return _Ty(); // default(_Ty);
	}
	FORCEINLINE int8 GetArgValue(fc_intptr L, int Index, int8)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_byte(ParamPtr);
	}
	FORCEINLINE bool GetArgValue(fc_intptr L, int Index, bool)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_bool(ParamPtr);
	}
	FORCEINLINE char GetArgValue(fc_intptr L, int Index, char)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_char(ParamPtr);
	}
	FORCEINLINE int16 GetArgValue(fc_intptr L, int Index, int16)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_short(ParamPtr);
	}
	FORCEINLINE uint16 GetArgValue(fc_intptr L, int Index, uint16)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_ushort(ParamPtr);
	}
	FORCEINLINE int GetArgValue(fc_intptr L, int Index, int)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_int(ParamPtr);
	}
	FORCEINLINE uint32 GetArgValue(fc_intptr L, int Index, uint32)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_uint(ParamPtr);
	}
	FORCEINLINE int64 GetArgValue(fc_intptr L, int Index, int64)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_int64(ParamPtr);
	}
	FORCEINLINE float GetArgValue(fc_intptr L, int Index, float)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_float(ParamPtr);
	}
	FORCEINLINE double GetArgValue(fc_intptr L, int Index, double)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		return fc_get_value_float(ParamPtr);
	}
	FORCEINLINE FString GetArgValue(fc_intptr L, int Index, FString)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		fc_intptr VM = fc_get_vm_ptr(L);
		TCHAR *pStrParam = (TCHAR *)fc_cpp_get_value_string_w(VM, ParamPtr);
		return FString(pStrParam ? pStrParam : TEXT(""));
	}
	FORCEINLINE FName GetArgValue(fc_intptr L, int Index, FName)
	{
		fc_intptr ParamPtr = fc_get_param_ptr(L, Index);
		fc_intptr VM = fc_get_vm_ptr(L);
		TCHAR* pStrParam = (TCHAR *)fc_cpp_get_value_string_w(VM, ParamPtr);
		return FName(pStrParam ? pStrParam : TEXT(""));
	}
}