#include "FCTSetWrap.h"
#include "Containers/Map.h"
#include "FCTemplateType.h"

#include "FCObjectManager.h"
#include "FCGetObj.h"
#include "FTMapKeyValueBuffer.h"

void FCTSetWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "TSet");
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_del(VM, nClassName, obj_del);
    fc_register_class_release_ref(VM, nClassName, obj_release);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);
    fc_register_class_func(VM, nClassName, "GetNumb", GetNumb_wrap);
    fc_register_class_func(VM, nClassName, "Contains", Contains_wrap);  // bool Contains(_TyValue value)
    fc_register_class_func(VM, nClassName, "Add", Add_wrap);  // 去重添加 void Add(_TyValue value)
    fc_register_class_func(VM, nClassName, "Remove", Remove_wrap); // int Remove(_TyValue value)
    fc_register_class_func(VM, nClassName, "Clear", Clear_wrap);
    fc_register_class_func(VM, nClassName, "ToArray", ToArray_wrap); 
    fc_register_class_func(VM, nClassName, "SetArray", SetArray_wrap);

    // 属性方法
    fc_register_class_attrib(VM, nClassName, "Length", GetNumb_wrap, nullptr);

    // 兼容stl接口
    fc_register_class_func(VM, nClassName, "size", GetNumb_wrap);
    fc_register_class_func(VM, nClassName, "push_back", Add_wrap);

    fc_register_class_func(VM, nClassName, "GetMaxIndex", GetMaxIndex_wrap);
    fc_register_class_func(VM, nClassName, "ToNextValidIndex", ToNextValidIndex_wrap); // int ToNextValidIndex(int nIndex)
    fc_register_class_func(VM, nClassName, "IsValidIndex", IsValidIndex_wrap); // int ToNextValidIndex(int nIndex)
    fc_register_class_func(VM, nClassName, "GetAt", GetAt_wrap);  // _TyValue GetAt(int nIndex)
    fc_register_class_func(VM, nClassName, "RemoveAt", RemoveAt_wrap); // void RemoveAt(int nIndex);
}

int FCTSetWrap::obj_new(fc_intptr L)
{
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_intptr VM = fc_get_vm_ptr(L);

    FCDynamicProperty* DynamicProperty = GetTSetDynamicProperty(VM, RetPtr);
    if (DynamicProperty)
    {
        FScriptSet* ScriptMap = new FScriptSet();
        int64 ObjID = FCGetObj::GetIns()->PushTemplate(DynamicProperty, ScriptMap, EFCObjRefType::NewTSet);
        fc_set_value_wrap_objptr(VM, RetPtr, ObjID);
    }
    else
    {
        fc_set_value_wrap_objptr(VM, RetPtr, 0);
    }
    return 0;
}

int FCTSetWrap::obj_del(fc_intptr nIntPtr)
{
    FCGetObj::GetIns()->DeleteValue(nIntPtr);
    return 0;
}

int FCTSetWrap::obj_release(fc_intptr nIntPtr)
{
    FCGetObj::GetIns()->ReleaseValue(nIntPtr);
    return 0;
}

int FCTSetWrap::obj_hash(fc_intptr nIntPtr)
{
    return FCGetObj::GetIns()->GetValueHash(nIntPtr);
}

bool FCTSetWrap::obj_equal(fc_intptr L, fc_intptr R)
{
    return FCGetObj::GetIns()->EqualValue(L, R);
}

int FCTSetWrap::GetNumb_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        int Num = ScriptMap->Num();
        fc_intptr RetPtr = fc_get_return_ptr(L);
        fc_set_value_uint(RetPtr, Num);
    }
    else
    {
        fc_intptr RetPtr = fc_get_return_ptr(L);
        fc_set_value_uint(RetPtr, 0);
    }
    return 0;
}

int FCTSetWrap::Contains_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int32 FindIndex = INDEX_NONE;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FProperty* ElementProp = SetProperty->ElementProp;
        const FScriptSetLayout& SetLayout = SetProperty->SetLayout;

        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();

        fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
        FTMapKeyValueBuffer KeyBuffer(ElementProp, VM, ArgPtr0);

        FindIndex = ScriptMap->FindIndex(KeyBuffer.Buffer, SetProperty->SetLayout,
            [ElementProp](const void* ElementKey) { return ElementProp->GetValueTypeHash(ElementKey); },
            [ElementProp](const void* A, const void* B) { return ElementProp->Identical(A, B); }
        );
    }
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_bool(RetPtr, FindIndex != INDEX_NONE);
    return 0;
}

int FCTSetWrap::Add_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int32 FindIndex = INDEX_NONE;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FProperty* ElementProp = SetProperty->ElementProp;
        const FScriptSetLayout& SetLayout = SetProperty->SetLayout;

        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();

        fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
        FTMapKeyValueBuffer KeyValueBuffer(ElementProp, VM, ArgPtr0);
        void* ValueBuffer = KeyValueBuffer.Buffer;

        FindIndex = ScriptMap->FindOrAdd(ValueBuffer, SetLayout,
            [ElementProp](const void* ElementKey) { return ElementProp->GetValueTypeHash(ElementKey); },
            [ElementProp](const void* A, const void* B) { return ElementProp->Identical(A, B); },
            [ElementProp, ValueBuffer](void* NewElement)
            {
                ElementProp->InitializeValue(NewElement); 
                ElementProp->CopySingleValue(NewElement, ValueBuffer);
            }
        );
    }
    return 0;
}

int FCTSetWrap::Remove_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int32 FindIndex = INDEX_NONE;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FProperty* ElementProp = SetProperty->ElementProp;
        const FScriptSetLayout& SetLayout = SetProperty->SetLayout;

        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();

        fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
        FTMapKeyValueBuffer KeyBuffer(ElementProp, VM, ArgPtr0);

        FindIndex = ScriptMap->FindIndex(KeyBuffer.Buffer, SetProperty->SetLayout,
            [ElementProp](const void* ElementKey) { return ElementProp->GetValueTypeHash(ElementKey); },
            [ElementProp](const void* A, const void* B) { return ElementProp->Identical(A, B); }
        );

        if (FindIndex != INDEX_NONE)
        {
            uint8* Result = (uint8*)ScriptMap->GetData(FindIndex, SetLayout);
            ElementProp->DestroyValue(Result);
            ScriptMap->RemoveAt(FindIndex, SetLayout);
        }
    }

    return 0;
}

int FCTSetWrap::Clear_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int32 FindIndex = INDEX_NONE;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        TSet_Clear(ScriptMap, SetProperty);
    }
    return 0;
}

int FCTSetWrap::ToArray_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int32 FindIndex = INDEX_NONE;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        FProperty* ElementProp = SetProperty->ElementProp;
        const FScriptSetLayout& SetLayout = SetProperty->SetLayout;

        FCDynamicProperty  ValueProperty;
        ValueProperty.InitProperty(ElementProp);

        int Num = ScriptMap->Num();
        fc_set_array_size(VM, RetPtr, Num);

        int ArrayIndex = 0;
        fc_intptr  ValuePtr = 0;
        int32  MaxIndex = ScriptMap->GetMaxIndex();
        for (int32 PairIndex = 0; PairIndex < MaxIndex; ++PairIndex)
        {
            if (ScriptMap->IsValidIndex(PairIndex))
            {
                ValuePtr = fc_get_array_node_temp_ptr(VM, RetPtr, ArrayIndex);
                uint8* Result = (uint8*)ScriptMap->GetData(PairIndex, SetLayout);
                ValueProperty.m_WriteScriptFunc(VM, ValuePtr, &ValueProperty, Result, nullptr, nullptr);
                ++ArrayIndex;
            }
        }
    }
    else
    {
        fc_set_array_size(VM, RetPtr, 0);
    }
    return 0;
}

int FCTSetWrap::SetArray_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int32 FindIndex = INDEX_NONE;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        FProperty* ElementProp = SetProperty->ElementProp;
        const FScriptSetLayout& SetLayout = SetProperty->SetLayout;

        TSet_Clear(ScriptMap, SetProperty);

        FTMapKeyValueBuffer KeyValueBuffer(ElementProp);
        fc_intptr  ValuePtr = 0;

        fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
        int32 Numb = fc_get_array_size(ArgPtr0);
        for(int32 i = 0; i<Numb; ++i)
        {
            ValuePtr = fc_get_array_node_temp_ptr(VM, ArgPtr0, i);
            KeyValueBuffer.ReadScriptValue(VM, ValuePtr);
            void* ValueBuffer = KeyValueBuffer.Buffer;
            FindIndex = ScriptMap->FindOrAdd(ValueBuffer, SetLayout,
                [ElementProp](const void* ElementKey) { return ElementProp->GetValueTypeHash(ElementKey); },
                [ElementProp](const void* A, const void* B) { return ElementProp->Identical(A, B); },
                [ElementProp, ValueBuffer](void* NewElement)
            {
                ElementProp->InitializeValue(NewElement);
                ElementProp->CopySingleValue(NewElement, ValueBuffer);
            }
            );
        }
     }

    return 0;
}

int FCTSetWrap::GetMaxIndex_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int32 FindIndex = INDEX_NONE;
    int MaxIndex = 0;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        MaxIndex = ScriptMap->GetMaxIndex();
    }
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_int(RetPtr, MaxIndex);
    return 0;
}

int FCTSetWrap::ToNextValidIndex_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int ValidIndex = 0;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        fc_intptr  Arg0 = fc_get_param_ptr(L, 0);
        int32 PairIndex = fc_get_value_int(Arg0);
        int32 MaxIndex = ScriptMap->GetMaxIndex();
        ValidIndex = MaxIndex;
        for(; PairIndex <= MaxIndex; ++PairIndex)
        {
           if(ScriptMap->IsValidIndex(PairIndex))
           {
               ValidIndex = PairIndex;
               break;
           }
        }
    }
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_int(RetPtr, ValidIndex);
    return 0;
}

int FCTSetWrap::IsValidIndex_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    bool bValid = false;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        fc_intptr  Arg0 = fc_get_param_ptr(L, 0);
        int32 PairIndex = fc_get_value_int(Arg0);
        bValid = ScriptMap->IsValidIndex(PairIndex);
    }
    fc_intptr RetPtr = fc_get_return_ptr(L);
    fc_set_value_int(RetPtr, bValid);
    return 0;
}

int FCTSetWrap::GetAt_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    fc_intptr RetPtr = fc_get_return_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        FProperty* ElementProp = SetProperty->ElementProp;
        const FScriptSetLayout& SetLayout = SetProperty->SetLayout;

        FCDynamicProperty  ValueProperty;
        ValueProperty.InitProperty(ElementProp);

        fc_intptr  Arg0 = fc_get_param_ptr(L, 0);
        int32 PairIndex = fc_get_value_int(Arg0);
        if (ScriptMap->IsValidIndex(PairIndex))
        {
            uint8* Result = (uint8*)ScriptMap->GetData(PairIndex, SetLayout);
            ValueProperty.m_WriteScriptFunc(VM, RetPtr, &ValueProperty, Result, nullptr, nullptr);
            return 0;
        }
    }
    return 0;
}

int FCTSetWrap::RemoveAt_wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
    int ValidIndex = 0;
    if (ObjRef && ObjRef->RefType == EFCObjRefType::NewTSet)
    {
        FSetProperty* SetProperty = ObjRef->DynamicProperty->SafePropertyPtr->CastSetProperty();
        FScriptSet* ScriptMap = (FScriptSet*)ObjRef->GetThisAddr();
        FProperty* ElementProp = SetProperty->ElementProp;
        const FScriptSetLayout& SetLayout = SetProperty->SetLayout;
        fc_intptr  Arg0 = fc_get_param_ptr(L, 0);
        int32 PairIndex = fc_get_value_int(Arg0);
        if(ScriptMap->IsValidIndex(PairIndex))
        {
            uint8* Result = (uint8*)ScriptMap->GetData(PairIndex, SetLayout);
            ElementProp->DestroyValue(Result);
            ScriptMap->RemoveAt(PairIndex, SetLayout);
        }
    }
    return 0;
}
