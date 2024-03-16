#include "FQuatWrap.h"
#include "FCObjectManager.h"

void FQuatWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FQuat");
    int nSize = sizeof(FQuat);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);

    fc_register_class_func(VM, nClassName, "+_FQuat_FQuat", OperatorAdd_FQuat_FQuat_Wrap);
    fc_register_class_func(VM, nClassName, "-_FQuat_FQuat", OperatorSub_FQuat_FQuat_Wrap);
    fc_register_class_func(VM, nClassName, "*_FQuat_FQuat", OperatorMul_FQuat_FQuat_Wrap);
    fc_register_class_func(VM, nClassName, "*_FQuat_FVector", OperatorMul_FQuat_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "*_FQuat_FMatrix", OperatorMul_FQuat_FMatrix_Wrap);
    fc_register_class_func(VM, nClassName, "+=_FQuat", OperatorAddSet_FQuat_Wrap);
    fc_register_class_func(VM, nClassName, "-=_FQuat", OperatorSubSet_FQuat_Wrap);
    fc_register_class_func(VM, nClassName, "*=_FQuat", OperatorMulSet_FQuat_Wrap);
    fc_register_class_func(VM, nClassName, "-", OperatorNegative_Wrap);

    fc_register_class_func(VM, nClassName, "|_FQuat_FQuat", OperatorDot_FQuat_FQuat_Wrap);

    fc_register_class_func(VM, nClassName, "MakeFromEuler", MakeFromEuler_Wrap);
    fc_register_class_func(VM, nClassName, "Euler", Euler_Wrap);
    fc_register_class_func(VM, nClassName, "Normalize", Normalize_Wrap);
    fc_register_class_func(VM, nClassName, "GetNormalized", GetNormalized_Wrap);
    fc_register_class_func(VM, nClassName, "IsNormalized", IsNormalized_Wrap);

    fc_register_class_func(VM, nClassName, "Size", Size_Wrap);
    fc_register_class_func(VM, nClassName, "SizeSquared", SizeSquared_Wrap);
    fc_register_class_func(VM, nClassName, "GetAngle", GetAngle_Wrap);

    fc_register_class_func(VM, nClassName, "ToAxisAndAngle_FVector_double", ToAxisAndAngle_double_Wrap);
    fc_register_class_func(VM, nClassName, "ToRotationVector", ToRotationVector_Wrap);
    fc_register_class_func(VM, nClassName, "MakeFromRotationVector", MakeFromRotationVector_Wrap);

    fc_register_class_func(VM, nClassName, "ToSwingTwist", ToSwingTwist_Wrap);
    fc_register_class_func(VM, nClassName, "GetTwistAngle", GetTwistAngle_Wrap);

    fc_register_class_func(VM, nClassName, "RotateVector", RotateVector_Wrap);
    fc_register_class_func(VM, nClassName, "UnrotateVector", UnrotateVector_Wrap);
    fc_register_class_func(VM, nClassName, "Log", Log_Wrap);
    fc_register_class_func(VM, nClassName, "Exp", Exp_Wrap);
    fc_register_class_func(VM, nClassName, "Inverse", Inverse_Wrap);

    fc_register_class_func(VM, nClassName, "GetAxisX", GetAxisX_Wrap);
    fc_register_class_func(VM, nClassName, "GetAxisY", GetAxisY_Wrap);
    fc_register_class_func(VM, nClassName, "GetAxisZ", GetAxisZ_Wrap);
    fc_register_class_func(VM, nClassName, "GetForwardVector", GetForwardVector_Wrap);
    fc_register_class_func(VM, nClassName, "GetRightVector", GetRightVector_Wrap);
    fc_register_class_func(VM, nClassName, "GetUpVector", GetUpVector_Wrap);
    fc_register_class_func(VM, nClassName, "Vector", Vector_Wrap);
    fc_register_class_func(VM, nClassName, "Rotator", Rotator_Wrap);
    fc_register_class_func(VM, nClassName, "ToMatrix_void", ToMatrix_Wrap);
    fc_register_class_func(VM, nClassName, "ToMatrix_FMatrix", ToMatrix_FMatrix_Wrap);

    fc_register_class_func(VM, nClassName, "GetRotationAxis", GetRotationAxis_Wrap);
    fc_register_class_func(VM, nClassName, "AngularDistance", AngularDistance_Wrap);
    fc_register_class_func(VM, nClassName, "ContainsNaN", ContainsNaN_Wrap);

    fc_register_class_func(VM, nClassName, "ToString", ToString_Wrap);
    fc_register_class_func(VM, nClassName, "InitFromString", InitFromString_Wrap);

    fc_register_class_func(VM, nClassName, "FindBetween", FindBetween_Wrap);
    fc_register_class_func(VM, nClassName, "FindBetweenNormals", FindBetweenNormals_Wrap);
    fc_register_class_func(VM, nClassName, "FindBetweenVectors", FindBetweenVectors_Wrap);
    fc_register_class_func(VM, nClassName, "FastLerp", FastLerp_Wrap);
    fc_register_class_func(VM, nClassName, "FastBilerp", FastBilerp_Wrap);
    fc_register_class_func(VM, nClassName, "Slerp_NotNormalized", Slerp_NotNormalized_Wrap);
    fc_register_class_func(VM, nClassName, "Slerp", Slerp_Wrap);

    fc_register_class_func(VM, nClassName, "SlerpFullPath_NotNormalized", SlerpFullPath_NotNormalized_Wrap);
    fc_register_class_func(VM, nClassName, "SlerpFullPath", SlerpFullPath_Wrap);
    fc_register_class_func(VM, nClassName, "Squad", Squad_Wrap);
    fc_register_class_func(VM, nClassName, "SquadFullPath", SquadFullPath_Wrap);
    fc_register_class_func(VM, nClassName, "CalcTangents", CalcTangents_Wrap);
}
int FQuatWrap::obj_new(fc_intptr L)
{
    return 0;
}
int FQuatWrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}
int FQuatWrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}
bool FQuatWrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FQuat* A = (FQuat*)LeftPtr;
    FQuat* B = (FQuat*)RightPtr;
    return *A == *B;
}

int FQuatWrap::OperatorAdd_FQuat_FQuat_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) + (*B);
    return 0;
}
int FQuatWrap::OperatorSub_FQuat_FQuat_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) - (*B);
    return 0;
}
int FQuatWrap::OperatorMul_FQuat_FQuat_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FQuatWrap::OperatorMul_FQuat_FVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FQuatWrap::OperatorMul_FQuat_FMatrix_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FMatrix* B = (FMatrix*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FQuatWrap::OperatorAddSet_FQuat_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 0);
    *A += *B;
    return 0;
}
int FQuatWrap::OperatorSubSet_FQuat_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 0);
    *A -= *B;
    return 0;
}
int FQuatWrap::OperatorMulSet_FQuat_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 0);
    *A *= *B;
    return 0;
}
int FQuatWrap::OperatorNegative_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = -(*A);
    return 0;
}

int FQuatWrap::OperatorDot_FQuat_FQuat_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    fc_set_value_double(fc_get_return_ptr(L), (*A) | (*B));
    return 0;

}

int FQuatWrap::MakeFromEuler_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = FQuat::MakeFromEuler(*A);
    return 0;
}
int FQuatWrap::Euler_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->Euler();
    return 0;
}
int FQuatWrap::Normalize_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    A->Normalize(Tolerance);
    return 0;
}
int FQuatWrap::GetNormalized_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    *Ret = A->GetNormalized(Tolerance);
    return 0;
}
int FQuatWrap::IsNormalized_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsNormalized());
    return 0;
}

int FQuatWrap::Size_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->Size());
    return 0;
}
int FQuatWrap::SizeSquared_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->SizeSquared());
    return 0;
}
int FQuatWrap::GetAngle_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetAngle());
    return 0;
}

int FQuatWrap::ToAxisAndAngle_double_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    double Angle = 0;
    A->ToAxisAndAngle(*B, Angle);
    fc_set_value_double(fc_get_param_ptr(L, 1), Angle);
    return 0;
}
int FQuatWrap::ToRotationVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->ToRotationVector();
    return 0;
}
int FQuatWrap::MakeFromRotationVector_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = FQuat::MakeFromRotationVector(*A);
    return 0;
}

int FQuatWrap::ToSwingTwist_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    FQuat* C = (FQuat*)fc_get_wrap_objptr(L, 1);
    FQuat* D = (FQuat*)fc_get_wrap_objptr(L, 2);
    A->ToSwingTwist(*B, *C, *D);
    return 0;
}
int FQuatWrap::GetTwistAngle_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    fc_set_value_double(fc_get_param_ptr(L, 1), A->GetTwistAngle(*B));
    return 0;
}

int FQuatWrap::RotateVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->RotateVector(*B);
    return 0;
}
int FQuatWrap::UnrotateVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->UnrotateVector(*B);
    return 0;
}
int FQuatWrap::Log_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->Log();
    return 0;
}
int FQuatWrap::Exp_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->Exp();
    return 0;
}
int FQuatWrap::Inverse_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->Inverse();
    return 0;
}

int FQuatWrap::GetAxisX_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetAxisX();
    return 0;
}
int FQuatWrap::GetAxisY_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetAxisY();
    return 0;
}
int FQuatWrap::GetAxisZ_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetAxisZ();
    return 0;
}
int FQuatWrap::GetForwardVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetForwardVector();
    return 0;
}
int FQuatWrap::GetRightVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetRightVector();
    return 0;
}
int FQuatWrap::GetUpVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetUpVector();
    return 0;
}
int FQuatWrap::Vector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->Vector();
    return 0;
}
int FQuatWrap::Rotator_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->Rotator();
    return 0;
}
int FQuatWrap::ToMatrix_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->ToMatrix();
    return 0;
}
int FQuatWrap::ToMatrix_FMatrix_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FMatrix* B = (FMatrix*)fc_get_wrap_objptr(L, 0);
    A->ToMatrix(*B);
    return 0;
}

int FQuatWrap::GetRotationAxis_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetRotationAxis();
    return 0;
}
int FQuatWrap::AngularDistance_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 0);
    fc_set_value_double(fc_get_return_ptr(L), A->AngularDistance(*B));
    return 0;
}
int FQuatWrap::ContainsNaN_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    fc_set_value_bool(fc_get_return_ptr(L), A->ContainsNaN());
    return 0;
}

int FQuatWrap::ToString_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToString();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}
int FQuatWrap::InitFromString_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_inport_obj_ptr(L);
    const char* InStr = fc_cpp_get_string_a(L, 0);
    if (InStr)
    {
        FUTF8ToTCHAR  TempStr(InStr);
        FString  InitStr(TempStr.Get(), TempStr.Length());
        A->InitFromString(InitStr);
    }
    return 0;
}

int FQuatWrap::FindBetween_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = FQuat::FindBetween(*A, *B);
    return 0;
}
int FQuatWrap::FindBetweenNormals_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = FQuat::FindBetweenNormals(*A, *B);
    return 0;
}
int FQuatWrap::FindBetweenVectors_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = FQuat::FindBetweenVectors(*A, *B);
    return 0;
}
int FQuatWrap::FastLerp_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    *Ret = FQuat::FastLerp(*A, *B, fc_get_double(L, 2));
    return 0;
}
int FQuatWrap::FastBilerp_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    FQuat* C = (FQuat*)fc_get_wrap_objptr(L, 2);
    FQuat* D = (FQuat*)fc_get_wrap_objptr(L, 3);
    *Ret = FQuat::FastBilerp(*A, *B, *C, *D, fc_get_double(L, 4), fc_get_double(L, 5));
    return 0;
}
int FQuatWrap::Slerp_NotNormalized_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    *Ret = FQuat::Slerp_NotNormalized(*A, *B, fc_get_double(L, 2));
    return 0;
}
int FQuatWrap::Slerp_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    *Ret = FQuat::Slerp(*A, *B, fc_get_double(L, 2));
    return 0;
}

int FQuatWrap::SlerpFullPath_NotNormalized_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    *Ret = FQuat::SlerpFullPath_NotNormalized(*A, *B, fc_get_double(L, 2));
    return 0;
}
int FQuatWrap::SlerpFullPath_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    *Ret = FQuat::SlerpFullPath(*A, *B, fc_get_double(L, 2));
    return 0;
}
int FQuatWrap::Squad_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    FQuat* C = (FQuat*)fc_get_wrap_objptr(L, 2);
    FQuat* D = (FQuat*)fc_get_wrap_objptr(L, 3);
    *Ret = FQuat::Squad(*A, *B, *C, *D, fc_get_double(L, 4));
    return 0;
}
int FQuatWrap::SquadFullPath_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    FQuat* C = (FQuat*)fc_get_wrap_objptr(L, 2);
    FQuat* D = (FQuat*)fc_get_wrap_objptr(L, 3);
    *Ret = FQuat::SquadFullPath(*A, *B, *C, *D, fc_get_double(L, 4));
    return 0;
}
int FQuatWrap::CalcTangents_Wrap(fc_intptr L)
{
    FQuat* A = (FQuat*)fc_get_wrap_objptr(L, 0);
    FQuat* B = (FQuat*)fc_get_wrap_objptr(L, 1);
    FQuat* C = (FQuat*)fc_get_wrap_objptr(L, 2);
    FQuat* D = (FQuat*)fc_get_wrap_objptr(L, 4);
    FQuat::CalcTangents(*A, *B, *C, fc_get_double(L, 3), *D);
    return 0;
}