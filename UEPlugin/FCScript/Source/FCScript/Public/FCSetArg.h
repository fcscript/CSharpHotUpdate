#pragma once

#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "CoreUObject.h"

#include "../../FCLib/include/fc_api.h"

namespace FCScript
{
	template<class _Ty>
	FORCEINLINE void SetArgValue(fc_intptr L, fc_intptr ParamPtr, _Ty)
	{
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
}