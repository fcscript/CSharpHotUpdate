#pragma once

#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "CoreUObject.h"

#include "../../FCLib/include/fc_api.h"

FCSCRIPT_API void FC_SetArgValue_CppPtr(fc_intptr L, fc_intptr ParamPtr, const void* CppPtr);
FCSCRIPT_API void FC_SetArgValue_ByName(fc_intptr L, fc_intptr ParamPtr, const void* ValueAddr, const char* ClassName);

namespace FCScript
{
	template<class _Ty>
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, _Ty)
	{
    }
    template<class _Ty>
    inline void SetArgValue(fc_intptr L, fc_intptr ParamPtr, _Ty* value)
    {
		FC_SetArgValue_CppPtr(L, ParamPtr, value);
    }
    template<class _Ty>
    inline void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const _Ty* value)
    {
        FC_SetArgValue_CppPtr(L, ParamPtr, value);
    }
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, int8 value)
	{
		fc_set_value_byte(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, bool value)
	{
		fc_set_value_bool(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, char value)
	{
		fc_set_value_char(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, int16 value)
	{
		fc_set_value_short(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, uint16 value)
	{
		fc_set_value_ushort(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, int value)
	{
		fc_set_value_int(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, uint32 value)
	{
		fc_set_value_uint(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, int64 value)
	{
		fc_set_value_int64(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, float value)
	{
		fc_set_value_float(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, double value)
	{
		fc_set_value_float(ParamPtr, value);
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FString &value)
	{
		fc_set_value_string_w(ParamPtr, (fc_ushort_ptr)(*value), value.Len());
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FName &value)
	{
		FString Name(value.ToString());
		fc_set_value_string_w(ParamPtr, (fc_ushort_ptr)(*Name), Name.Len());
	}

	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FRotator& value)
	{
		FC_SetArgValue_ByName(L, ParamPtr, &value, "FRotator");
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FQuat& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FQuat");
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FTransform& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FTransform");
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FLinearColor& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FLinearColor");
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FColor& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FColor");
	}
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FPlane& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FPlane");
	}
    FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FVector& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FVector");
    }
    FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FVector2D& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FVector2D");
    }
    FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FVector4& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FVector4");
    }
    FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, const FGuid& value)
    {
        FC_SetArgValue_ByName(L, ParamPtr, &value, "FGuid");
    }
}