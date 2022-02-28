#pragma once
#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "CoreUObject.h"

enum
{
    EX_CallFCBeginPlay = EX_Max - 22,
    EX_CallFCOverride = EX_Max - 21,
    EX_CallFCDelegate = EX_Max - 20,
};

// 动态覆盖普通的函数
void FCDynamicOverrideNative(UObject* Context, FFrame& TheStack, RESULT_DECL);

// 动态覆盖ReceiveBeginPlay函数
void FCDynamicOverrideBeginBeginPlay(UObject* Context, FFrame& TheStack, RESULT_DECL);

// 功能:动态绑定脚本
int64 FCDynamicBindScript(UObject* InObject);

// 动态覆盖委托
void FCDynamicOverrideDelegate(UObject* Context, FFrame& TheStack, RESULT_DECL);