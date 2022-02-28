#pragma once
#include "FCGetArg.h"
#include "FCSetArg.h"

struct FCFunctionReturnVoid
{
};
struct FCFunctionReturnValue
{
};

template <class T>
struct FCFunctionReturnTraits
{
	typedef FCFunctionReturnValue  RetType;
};
template<> struct FCFunctionReturnTraits<void> { typedef FCFunctionReturnVoid  RetType; };

template<typename T>
T  FCReadScriptArg(long L, int& Index)
{
	return FCScript::GetArgValue(L, Index++, T());
}


template <typename RetType, typename... ArgType>
void  FCInvokeFunctionBase(fc_intptr L, RetType(*InFunc)(ArgType...), FCFunctionReturnVoid)
{
	int nIndex = 0;
	InFunc(FCReadScriptArg<ArgType>(L, nIndex)...);
}

template <typename RetType, typename... ArgType>
void  FCInvokeFunctionBase(fc_intptr L, RetType(*InFunc)(ArgType...), FCFunctionReturnValue)
{
	int nIndex = 0;
	RetType Ret = InFunc(FCReadScriptArg<ArgType>(L, nIndex)...);
	// 将结果写入到脚本
	fc_intptr RetPtr = fc_get_return_ptr(L);
	if (RetPtr)
	{
		FCScript::SetArgValue(L, RetPtr, Ret);
	}
}

template <typename RetType, typename... ArgType>
void  FCInvokeStaticFunction(fc_intptr L, RetType(*InFunc)(ArgType...))
{
	FCInvokeFunctionBase<RetType, ArgType...>(L, InFunc, FCFunctionReturnTraits<RetType>::RetType());
}


template <typename RetType, typename ClassType, typename... ArgType>
void  FCInvokeClassFunctionBase(fc_intptr L, ClassType* ThisPtr, RetType(ClassType::* InFunc)(ArgType...), FCFunctionReturnVoid)
{
	int nIndex = 0;
	(ThisPtr->InFunc)(FCReadScriptArg<ArgType>(L, nIndex)...);
}

template <typename RetType, typename ClassType, typename... ArgType>
void  FCInvokeClassFunctionBase(fc_intptr L, ClassType* ThisPtr, RetType(ClassType::* InFunc)(ArgType...), FCFunctionReturnValue)
{
	int nIndex = 0;
	RetType Ret = (ThisPtr->*InFunc)(FCReadScriptArg<ArgType>(L, nIndex)...);
	// 将结果写入到脚本
	fc_intptr RetPtr = fc_get_return_ptr(L);
	if(RetPtr)
	{
		FCScript::SetArgValue(L, RetPtr, Ret);
	}
}

template <typename RetType, typename ClassType, typename... ArgType>
void  FCInvokeClassFunction(fc_intptr L, ClassType* ThisPtr, RetType(ClassType::* InFunc)(ArgType...))
{
	FCInvokeClassFunctionBase<RetType, ClassType, ArgType...>(L, ThisPtr, InFunc, FCFunctionReturnTraits<RetType>::RetType());
}
