#include "FCDynamicClassDesc.h"

// 注册所有的反射接口

void   RunTimeRegisterScript(FCScriptContext *Context);

void   WrapNativeCallFunction(fc_intptr L, int ParamIndex, UObject *ThisObject, FCDynamicFunction  *DynamicFunc, uint8 *Buffer, int BufferSize, FNativeFuncPtr NativeFuncPtr);

void   TestDynamicObject();