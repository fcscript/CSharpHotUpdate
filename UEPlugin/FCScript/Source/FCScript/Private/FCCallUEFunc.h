#pragma once
#include "FCDynamicClassDesc.h"
#include "UObject/Class.h"


struct FCUEFunctionStack
{
	FCDynamicFunction   *m_Function;
	char        *m_ParamBuffer;
	int          m_PtrIndex;
	FCUEFunctionStack(FCDynamicFunction *InFunction):m_Function(InFunction), m_ParamBuffer(nullptr), m_PtrIndex(0)
	{
	}
	~FCUEFunctionStack()
	{
	}
};

int  PushUEValue(FCUEFunctionStack* L, const int& value, bool bCopy = false);
int  PushUEValue(FCUEFunctionStack* L, const float& value, bool bCopy = false);
int  PushUEValue(FCUEFunctionStack* L, const FVector& value, bool bCopy = false);

template <bool bCopy, typename T1, typename... T2>
int PushUEArgs(FCUEFunctionStack* L, T1&& V1, T2&&... V2)
{
	return PushUEValue(L, Forward<T1>(V1), bCopy) + PushUEArgs<bCopy>(L, Forward<T2>(V2)...);
}

template<bool bCopy> int PushUEArgs(FCUEFunctionStack* L)
{
	return 0;
}

template <typename... T>
void  PushAnyUEParam(FCUEFunctionStack* L, T&&... Args)
{
	int NumArgs = PushUEArgs<false>(L, Forward<T>(Args)...);
}
