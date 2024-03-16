#include "FVectorWrap.h"
#include "FCObjectManager.h"
#include "FCGetObj.h"


void FVectorWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FVector");
    int nValueSize = sizeof(FVector);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName,obj_hash);
    fc_register_class_equal(VM, nClassName,obj_equal);

    fc_register_class_func(VM, nClassName, "FVector_double_double_double", OperatorNew_double_double_double_Wrap);

    fc_register_class_func(VM, nClassName, "^_FVector_FVector", CrossProduct_Wrap);
    fc_register_class_func(VM, nClassName, "Cross", Cross_Wrap);
    fc_register_class_func(VM, nClassName, "CrossProduct", CrossProduct_Wrap);
    fc_register_class_func(VM, nClassName, "|_FVector_FVector", DotProduct_Wrap);
    fc_register_class_func(VM, nClassName, "Dot", Dot_Wrap);
    fc_register_class_func(VM, nClassName, "DotProduct", DotProduct_Wrap);

    fc_register_class_func(VM, nClassName, "+_FVector_FVector", OperatorAdd_FVector_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "-_FVector_FVector", OperatorSub_FVector_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "*_FVector_FVector", OperatorMul_FVector_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "/_FVector_FVector", OperatorDiv_FVector_FVector_Wrap);

    fc_register_class_func(VM, nClassName, "+_FVector_double", OperatorAdd_FVector_double_Wrap);
    fc_register_class_func(VM, nClassName, "-_FVector_double", OperatorSub_FVector_double_Wrap);
    fc_register_class_func(VM, nClassName, "*_FVector_double", OperatorMul_FVector_double_Wrap);
    fc_register_class_func(VM, nClassName, "/_FVector_double", OperatorDiv_FVector_double_Wrap);

    fc_register_class_func(VM, nClassName, "+=_FVector", OperatorAddSet_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "-=_FVector", OperatorSubSet_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "*=_FVector", OperatorMulSet_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "/=_FVector", OperatorDivSet_FVector_Wrap);

    fc_register_class_func(VM, nClassName, "+=_double", OperatorAddSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "-=_double", OperatorSubSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "*=_double", OperatorMulSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "/=_double", OperatorDivSet_double_Wrap);

    fc_register_class_func(VM, nClassName, "-", OperatorNegative_Wrap);

    fc_register_class_func(VM, nClassName, "Zero", Zero_Wrap);
    fc_register_class_func(VM, nClassName, "One", One_Wrap);

    fc_register_class_attrib(VM, nClassName, "[]", GetIndex_wrap, SetIndex_wrap);
    fc_register_class_func(VM, nClassName, "Set", Set_Wrap);

    fc_register_class_func(VM, nClassName, "GetMax", GetMax_Wrap);
    fc_register_class_func(VM, nClassName, "GetAbsMax", GetAbsMax_Wrap);
    fc_register_class_func(VM, nClassName, "GetMin", GetMin_Wrap);
    fc_register_class_func(VM, nClassName, "GetAbsMin", GetAbsMin_Wrap);

    fc_register_class_func(VM, nClassName, "GetAbs", GetAbs_Wrap);
    fc_register_class_func(VM, nClassName, "Size", Size_Wrap);
    fc_register_class_func(VM, nClassName, "Length", Length_Wrap);
    fc_register_class_func(VM, nClassName, "SizeSquared", SizeSquared_Wrap);
    fc_register_class_func(VM, nClassName, "SquaredLength", SquaredLength_Wrap);
    fc_register_class_func(VM, nClassName, "Size2D", Size2D_Wrap);
    fc_register_class_func(VM, nClassName, "SizeSquared2D", SizeSquared2D_Wrap);

    fc_register_class_func(VM, nClassName, "IsNearlyZero", IsNearlyZero_Wrap);
    fc_register_class_func(VM, nClassName, "IsZero", IsZero_Wrap);
    fc_register_class_func(VM, nClassName, "IsNormalized", IsNormalized_Wrap);
    fc_register_class_func(VM, nClassName, "Normalize", Normalize_Wrap);

    fc_register_class_func(VM, nClassName, "ToOrientationRotator", ToOrientationRotator_Wrap);
    fc_register_class_func(VM, nClassName, "ToOrientationQuat", ToOrientationQuat_Wrap);
    fc_register_class_func(VM, nClassName, "Rotation", Rotation_Wrap);
    fc_register_class_func(VM, nClassName, "ToString", ToString_Wrap);
    fc_register_class_func(VM, nClassName, "InitFromString", InitFromString_Wrap);
}

int FVectorWrap::obj_new(fc_intptr L)
{
    return 0;
}

int FVectorWrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}

int FVectorWrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}

bool FVectorWrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FVector* A = (FVector*)LeftPtr;
    FVector* B = (FVector*)RightPtr;
    return *A == *B;
}

int FVectorWrap::OperatorNew_double_double_double_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector(fc_get_double(L, 0), fc_get_double(L, 1), fc_get_double(L, 2));
    return 0;
}

int FVectorWrap::Cross_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->Cross(*B);
    return 0;
}

int FVectorWrap::CrossProduct_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));

    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = FVector::CrossProduct(*A, *B);
    return 0;
}

int FVectorWrap::Dot_Wrap(fc_intptr L)
{
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
    FVector* A = (FVector*)nThisPtr;
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    double Ret = A->Dot(*B);
    fc_set_value_double(fc_get_return_ptr(L), Ret);
    return 0;
}

int FVectorWrap::DotProduct_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    double Ret = A->Dot(*B);
    fc_set_value_double(fc_get_return_ptr(L), Ret);
    return 0;
}

int FVectorWrap::OperatorAdd_FVector_FVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = *A + *B;
    return 0;
}
int FVectorWrap::OperatorSub_FVector_FVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = *A - *B;
    return 0;
}
int FVectorWrap::OperatorMul_FVector_FVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FVectorWrap::OperatorDiv_FVector_FVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) / (*B);
    return 0;
}

int FVectorWrap::OperatorAdd_FVector_double_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    *Ret = (*A) + B;
    return 0;
}
int FVectorWrap::OperatorSub_FVector_double_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    *Ret = (*A) - B;
    return 0;
}
int FVectorWrap::OperatorMul_FVector_double_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    *Ret = (*A) * B;
    return 0;
}
int FVectorWrap::OperatorDiv_FVector_double_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    if(B != 0)
    {
        *Ret = (*A) / B;
    }
    return 0;
}

int FVectorWrap::OperatorAddSet_FVector_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *A += *B;
    return 0;
}
int FVectorWrap::OperatorSubSet_FVector_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *A -= *B;
    return 0;
}
int FVectorWrap::OperatorMulSet_FVector_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *A *= *B;
    return 0;
}
int FVectorWrap::OperatorDivSet_FVector_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *A /= *B;
    return 0;
}

int FVectorWrap::OperatorAddSet_double_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X += B;
    A->Y += B;
    A->Z += B;
    return 0;
}
int FVectorWrap::OperatorSubSet_double_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X -= B;
    A->Y -= B;
    A->Z -= B;
    return 0;
}
int FVectorWrap::OperatorMulSet_double_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X *= B;
    A->Y *= B;
    A->Z *= B;
    return 0;
}
int FVectorWrap::OperatorDivSet_double_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    if(B != 0)
    {
        A->X /= B;
        A->Y /= B;
        A->Z /= B;
    }
    return 0;
}

int FVectorWrap::OperatorNegative_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    *Ret = -(*A);
    return 0;
}

int FVectorWrap::Zero_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector::Zero();
    return 0;
}
int FVectorWrap::One_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector::One();
    return 0;
}

int FVectorWrap::GetIndex_wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    int Index = fc_get_int(L, 0);
    if(Index >= 0 && Index < 3)
    {
        fc_set_value_double(fc_get_return_ptr(L), (*A)[Index]);
    }
    else
    {
        fc_set_value_double(fc_get_return_ptr(L), 0);
    }
    return 0;
}

int FVectorWrap::SetIndex_wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    int Index = fc_get_int(L, 0);
    double Value = fc_get_double(L, 1);
    if (Index >= 0 && Index < 3)
    {
        (*A)[Index] = Value;
    }
    return 0;
}

int FVectorWrap::Set_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    A->Set(fc_get_double(L, 0), fc_get_double(L, 1), fc_get_double(L, 2));
    return 0;
}

int FVectorWrap::GetMax_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetMax());
    return 0;
}
int FVectorWrap::GetAbsMax_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetAbsMax());
    return 0;
}
int FVectorWrap::GetMin_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetMin());
    return 0;
}
int FVectorWrap::GetAbsMin_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetAbsMin());
    return 0;
}

int FVectorWrap::GetAbs_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetAbs();
    return 0;
}
int FVectorWrap::Size_Wrap(fc_intptr L)
{
    fc_intptr RetPtr = fc_get_return_ptr(L);
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(RetPtr, A->Size());
    return 0;
}
int FVectorWrap::Length_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->Length());
    return 0;
}
int FVectorWrap::SizeSquared_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->SizeSquared());
    return 0;
}
int FVectorWrap::SquaredLength_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->SquaredLength());
    return 0;
}
int FVectorWrap::Size2D_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->Size2D());
    return 0;
}
int FVectorWrap::SizeSquared2D_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->SizeSquared2D());
    return 0;
}

int FVectorWrap::IsNearlyZero_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsNearlyZero(Tolerance));
    return 0;
}
int FVectorWrap::IsZero_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsZero());
    return 0;
}
int FVectorWrap::IsNormalized_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsNormalized());
    return 0;
}
int FVectorWrap::Normalize_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->Normalize(Tolerance));
    return 0;
}

int FVectorWrap::ToOrientationRotator_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    *Ret = A->ToOrientationRotator();
    return 0;
}
int FVectorWrap::ToOrientationQuat_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    *((FQuat*)fc_get_return_ptr(L)) = A->ToOrientationQuat();
    return 0;
}
int FVectorWrap::Rotation_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    *Ret = A->Rotation();
    return 0;
}
int FVectorWrap::ToString_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToString();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}
int FVectorWrap::InitFromString_Wrap(fc_intptr L)
{
    FVector* A = (FVector*)fc_get_inport_obj_ptr(L);
    const char* InStr = fc_cpp_get_string_a(L, 0);
    if (InStr)
    {
        FUTF8ToTCHAR  TempStr(InStr);
        FString  InitStr(TempStr.Get(), TempStr.Length());
        A->InitFromString(InitStr);
    }
    return 0;
}