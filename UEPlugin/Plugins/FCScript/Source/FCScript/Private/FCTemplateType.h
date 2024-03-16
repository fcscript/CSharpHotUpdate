#pragma once
#include "FCDynamicClassDesc.h"
#include "UObject/UObjectGlobals.h"
#include "Containers/ScriptArray.h"
#include "Containers/Map.h"

#if ENGINE_MAJOR_VERSION >=5
#define ScriptArray_Add(ScriptArray, Count, ElementSize)  ScriptArray->Add(Count, ElementSize, __STDCPP_DEFAULT_NEW_ALIGNMENT__)
#define ScriptArray_Insert(ScriptArray, Index, Count, ElementSize) ScriptArray->Insert(Index, Count, ElementSize, __STDCPP_DEFAULT_NEW_ALIGNMENT__)
#define ScriptArray_Remove(ScriptArray, Index, Count, ElementSize)  ScriptArray->Remove(Index, Count, ElementSize, __STDCPP_DEFAULT_NEW_ALIGNMENT__)
#define ScriptArray_Empty(ScriptArray, Size, ElementSize)  ScriptArray->Empty(Size, ElementSize, __STDCPP_DEFAULT_NEW_ALIGNMENT__)
#else
#define ScriptArray_Add(ScriptArray, Count, ElementSize)  ScriptArray->Add(Count, ElementSize)
#define ScriptArray_Insert(ScriptArray, Index, Count, ElementSize) ScriptArray->Insert(Index, Count, ElementSize)
#define ScriptArray_Remove(ScriptArray, Index, Count, ElementSize)  ScriptArray->Remove(Index, Count, ElementSize)
#define ScriptArray_Empty(ScriptArray, Size, ElementSize)  ScriptArray->Empty(Size, ElementSize)
#endif

FProperty  *CreateClassProperty(const char *InClassName);
FCDynamicProperty *GetCppDynamicProperty(const char *InClassName);
FCDynamicProperty* GetStructDynamicProperty(UStruct* Struct);
FCDynamicProperty* GetDynamicPropertyByUEProperty(FProperty* InProperty);
FCDynamicProperty* GetDynamicPropertyByCppType(FCPropertyType InType, const char* InClassName, int InElementSize);

FArrayProperty* CreateTArrayProperty(fc_intptr VM, fc_intptr Ptr);

FCDynamicProperty *GetTArrayDynamicProperty(fc_intptr VM, fc_intptr Ptr);

FCDynamicProperty *GetTMapDynamicProperty(fc_intptr VM, fc_intptr Ptr);

FCDynamicProperty* GetTSetDynamicProperty(fc_intptr VM, fc_intptr Ptr);

void ReleaseTempalteProperty();

void TArray_Clear(FScriptArray *ScriptArray, FProperty *Inner);

void TMap_Clear(FScriptMap* ScriptMap, FMapProperty* MapProperty);

void TSet_Clear(FScriptSet* ScriptMap, FSetProperty* SetProperty);