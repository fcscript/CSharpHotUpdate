
#include "FCSetArg.h"
#include "FCDynamicClassDesc.h"
#include "FCTemplateType.h"
#include "FCGetObj.h"

void FC_SetArgValue_CppPtr(fc_intptr L, fc_intptr ParamPtr, const void* CppPtr)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    int64 PtrID = FCGetObj::GetIns()->PushCppPtr((void*)CppPtr);
    fc_set_value_wrap_objptr(VM, ParamPtr, PtrID);
}

void FC_SetArgValue_ByName(fc_intptr L, fc_intptr ParamPtr, const void *ValueAddr, const char *ClassName)
{
    FCDynamicProperty *DynamicProperty = GetCppDynamicProperty(ClassName);
    if(DynamicProperty)
    {
        fc_intptr VM = fc_get_vm_ptr(L);
        DynamicProperty->m_WriteScriptFunc(VM, ParamPtr, DynamicProperty, (uint8*)ValueAddr, nullptr, nullptr);
    }
}

void* FC_GetArgValue_CppPtr(fc_intptr L, fc_intptr ParamPtr)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    int64 PtrID = fc_get_value_wrap_objptr(ParamPtr);
    FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(PtrID);
    if(ObjRef && ObjRef->RefType == EFCObjRefType::CppPtr)
    {
        return ObjRef->GetThisAddr();
    }
    return nullptr;
}

void  FC_GetArgValue_ByName(fc_intptr L, fc_intptr ParamPtr, void* ValueAddr, const char* ClassName)
{
    FCDynamicProperty* DynamicProperty = GetCppDynamicProperty(ClassName);
    if(DynamicProperty)
    {
        fc_intptr VM = fc_get_vm_ptr(L);
        DynamicProperty->m_ReadScriptFunc(VM, ParamPtr, DynamicProperty, (uint8*)ValueAddr, nullptr, nullptr);
    }
}
