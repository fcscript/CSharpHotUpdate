
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
