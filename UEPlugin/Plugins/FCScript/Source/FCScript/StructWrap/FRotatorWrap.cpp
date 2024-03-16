#include "FRotatorWrap.h"
#include "FCObjectManager.h"

void FRotatorWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FRotator");
    int nSize = sizeof(FRotator);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);

    fc_register_class_func(VM, nClassName, "+_FRotator_FRotator", OperatorAdd_FRotator_FRotator_Wrap);
    fc_register_class_func(VM, nClassName, "-_FRotator_FRotator", OperatorSub_FRotator_FRotator_Wrap);

    fc_register_class_func(VM, nClassName, "IsNearlyZero", IsNearlyZero_Wrap);
    fc_register_class_func(VM, nClassName, "IsZero", IsZero_Wrap);

    fc_register_class_func(VM, nClassName, "Add", Add_Wrap);
    fc_register_class_func(VM, nClassName, "GetInverse", GetInverse_Wrap);
    fc_register_class_func(VM, nClassName, "GridSnap", GridSnap_Wrap);

    fc_register_class_func(VM, nClassName, "Vector", Vector_Wrap);
    fc_register_class_func(VM, nClassName, "Quaternion", Quaternion_Wrap);
    fc_register_class_func(VM, nClassName, "Euler", Euler_Wrap);
    fc_register_class_func(VM, nClassName, "RotateVector", RotateVector_Wrap);
    fc_register_class_func(VM, nClassName, "UnrotateVector", UnrotateVector_Wrap);

    fc_register_class_func(VM, nClassName, "Clamp", Clamp_Wrap);
    fc_register_class_func(VM, nClassName, "GetNormalized", GetNormalized_Wrap);
    fc_register_class_func(VM, nClassName, "GetDenormalized", GetDenormalized_Wrap);
    fc_register_class_func(VM, nClassName, "Normalize", Normalize_Wrap);

    fc_register_class_func(VM, nClassName, "ToString", ToString_Wrap);
    fc_register_class_func(VM, nClassName, "InitFromString", InitFromString_Wrap);
    fc_register_class_func(VM, nClassName, "MakeFromEuler", MakeFromEuler_Wrap);
}
int FRotatorWrap::obj_new(fc_intptr L)
{
    return 0;
}
int FRotatorWrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}
int FRotatorWrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}
bool FRotatorWrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FRotator* A = (FRotator*)LeftPtr;
    FRotator* B = (FRotator*)RightPtr;
    return *A == *B;
}

int FRotatorWrap::OperatorAdd_FRotator_FRotator_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_wrap_objptr(L, 0);
    FRotator* B = (FRotator*)fc_get_wrap_objptr(L, 1);
    *Ret = *A + *B;
    return 0;
}
int FRotatorWrap::OperatorSub_FRotator_FRotator_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_wrap_objptr(L, 0);
    FRotator* B = (FRotator*)fc_get_wrap_objptr(L, 1);
    *Ret = *A - *B;
    return 0;
}

int FRotatorWrap::IsNearlyZero_Wrap(fc_intptr L)
{
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsNearlyZero(Tolerance));
    return 0;
}
int FRotatorWrap::IsZero_Wrap(fc_intptr L)
{
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsZero());
    return 0;
}

int FRotatorWrap::Add_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    double DeltaPitch = fc_get_double(L, 0);
    double DeltaYaw = fc_get_double(L, 0);
    double DeltaRoll = fc_get_double(L, 0);
    *Ret = A->Add(DeltaPitch, DeltaYaw, DeltaRoll);
    return 0;
}
int FRotatorWrap::GetInverse_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetInverse();
    return 0;
}
int FRotatorWrap::GridSnap_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    FRotator* B = (FRotator*)fc_get_wrap_objptr(L, 0);
    *Ret = A->GridSnap(*B);
    return 0;
}

int FRotatorWrap::Vector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    *Ret = A->Vector();
    return 0;
}
int FRotatorWrap::Quaternion_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    *Ret = A->Quaternion();
    return 0;
}
int FRotatorWrap::Euler_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    *Ret = A->Euler();
    return 0;
}
int FRotatorWrap::RotateVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->RotateVector(*B);
    return 0;

}
int FRotatorWrap::UnrotateVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->UnrotateVector(*B);
    return 0;
}

int FRotatorWrap::Clamp_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    *Ret = A->Clamp();
    return 0;
}
int FRotatorWrap::GetNormalized_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetNormalized();
    return 0;
}
int FRotatorWrap::GetDenormalized_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetDenormalized();
    return 0;
}
int FRotatorWrap::Normalize_Wrap(fc_intptr L)
{
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    A->Normalize();
    return 0;
}

int FRotatorWrap::ToString_Wrap(fc_intptr L)
{
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToString();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}
int FRotatorWrap::InitFromString_Wrap(fc_intptr L)
{
    FRotator* A = (FRotator*)fc_get_inport_obj_ptr(L);
    const char *InStr = fc_cpp_get_string_a(L, 0);
    if(InStr)
    {
        FUTF8ToTCHAR  TempStr(InStr);
        FString  InitStr(TempStr.Get(), TempStr.Length());
        A->InitFromString(InitStr);
    }
    return 0;
}
int FRotatorWrap::MakeFromEuler_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = FRotator::MakeFromEuler(*A);
    return 0;
}
