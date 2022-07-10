#include "FCCallUEFunc.h"

template <class _Ty>
int  PushUEBaseValue(FCUEFunctionStack* L, const _Ty& value, bool bCopy)
{
	int  Offset = L->m_Function->m_Property[L->m_PtrIndex].Offset_Internal;
	*((_Ty*)(L->m_ParamBuffer + Offset)) = value;
	L->m_PtrIndex++;
	return 1;
}

int  PushUEValue(FCUEFunctionStack* L, const int& value, bool bCopy)
{
	return PushUEBaseValue<int>(L, value, bCopy);
}

int  PushUEValue(FCUEFunctionStack* L, const float& value, bool bCopy)
{
	return PushUEBaseValue<float>(L, value, bCopy);
}

int  PushUEValue(FCUEFunctionStack* L, const FVector& value, bool bCopy)
{
	return PushUEBaseValue<FVector>(L, value, bCopy);
}

