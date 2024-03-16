#include "FMatrixWrap.h"
#include "FCObjectManager.h"

void FMatrixWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FMatrix");
    int nSize = sizeof(FMatrix);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);

    fc_register_class_func(VM, nClassName, "FMatrix_FPlane_FPlane_FPlane_FPlane", OperatorNew_FPlane_FPlane_FPlane_FPlane_Wrap);
    fc_register_class_func(VM, nClassName, "FMatrix_FVector_FVector_FVector_FVector", OperatorNew_FVector_FVector_FVector_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "SetIdentity", SetIdentity_Wrap);

    fc_register_class_func(VM, nClassName, "*_FMatrix_FMatrix", OperatorMul_FMatrix_FMatrix_Wrap);
    fc_register_class_func(VM, nClassName, "*=_FMatrix", OperatorMulSet_FMatrix_Wrap);
    fc_register_class_func(VM, nClassName, "+_FMatrix_FMatrix", OperatorAdd_FMatrix_FMatrix_Wrap);
    fc_register_class_func(VM, nClassName, "+=_FMatrix", OperatorAddSet_FMatrix_Wrap);
    fc_register_class_func(VM, nClassName, "*_FMatrix_double", OperatorMul_FMatrix_double_Wrap);
    fc_register_class_func(VM, nClassName, "*=_double", OperatorMul_double_Wrap);

    fc_register_class_func(VM, nClassName, "TransformFVector4", TransformFVector4_Wrap);
    fc_register_class_func(VM, nClassName, "TransformPosition", TransformPosition_Wrap);
    fc_register_class_func(VM, nClassName, "InverseTransformPosition", InverseTransformPosition_Wrap);
    fc_register_class_func(VM, nClassName, "TransformVector", TransformVector_Wrap);
    fc_register_class_func(VM, nClassName, "InverseTransformVector", InverseTransformVector_Wrap);
    fc_register_class_func(VM, nClassName, "GetTransposed", GetTransposed_Wrap);

    fc_register_class_func(VM, nClassName, "Determinant", Determinant_Wrap);
    fc_register_class_func(VM, nClassName, "RotDeterminant", RotDeterminant_Wrap);
    fc_register_class_func(VM, nClassName, "InverseFast", InverseFast_Wrap);
    fc_register_class_func(VM, nClassName, "Inverse", Inverse_Wrap);
    fc_register_class_func(VM, nClassName, "TransposeAdjoint", TransposeAdjoint_Wrap);

    fc_register_class_func(VM, nClassName, "RemoveScaling", RemoveScaling_Wrap);
    fc_register_class_func(VM, nClassName, "GetMatrixWithoutScale", GetMatrixWithoutScale_Wrap);
    fc_register_class_func(VM, nClassName, "ExtractScaling", ExtractScaling_Wrap);
    fc_register_class_func(VM, nClassName, "GetScaleVector", GetScaleVector_Wrap);
    fc_register_class_func(VM, nClassName, "RemoveTranslation", RemoveTranslation_Wrap);
    fc_register_class_func(VM, nClassName, "ConcatTranslation", ConcatTranslation_Wrap);

    fc_register_class_func(VM, nClassName, "ContainsNaN", ContainsNaN_Wrap);
    fc_register_class_func(VM, nClassName, "ScaleTranslation", ScaleTranslation_Wrap);
    fc_register_class_func(VM, nClassName, "GetMinimumAxisScale", GetMinimumAxisScale_Wrap);
    fc_register_class_func(VM, nClassName, "GetMaximumAxisScale", GetMaximumAxisScale_Wrap);

    fc_register_class_func(VM, nClassName, "ApplyScale", ApplyScale_Wrap);
    fc_register_class_func(VM, nClassName, "GetOrigin", GetOrigin_Wrap);

    fc_register_class_func(VM, nClassName, "GetColumn", GetColumn_Wrap);
    fc_register_class_func(VM, nClassName, "SetColumn", SetColumn_Wrap);
    fc_register_class_func(VM, nClassName, "Rotator", Rotator_Wrap);
    fc_register_class_func(VM, nClassName, "ToQuat", ToQuat_Wrap);

    fc_register_class_func(VM, nClassName, "GetFrustumNearPlane", GetFrustumNearPlane_Wrap);
    fc_register_class_func(VM, nClassName, "GetFrustumFarPlane", GetFrustumFarPlane_Wrap);
    fc_register_class_func(VM, nClassName, "GetFrustumLeftPlane", GetFrustumLeftPlane_Wrap);
    fc_register_class_func(VM, nClassName, "GetFrustumRightPlane", GetFrustumRightPlane_Wrap);
    fc_register_class_func(VM, nClassName, "GetFrustumTopPlane", GetFrustumTopPlane_Wrap);
    fc_register_class_func(VM, nClassName, "GetFrustumBottomPlane", GetFrustumBottomPlane_Wrap);

    fc_register_class_func(VM, nClassName, "ToString", ToString_Wrap);
    fc_register_class_func(VM, nClassName, "ComputeHash", ComputeHash_Wrap);
}

int FMatrixWrap::obj_new(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    return 0;
}
int FMatrixWrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}
int FMatrixWrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}
bool FMatrixWrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FMatrix* A = (FMatrix*)LeftPtr;
    FMatrix* B = (FMatrix*)RightPtr;
    return *A == *B;
}

int FMatrixWrap::OperatorNew_FPlane_FPlane_FPlane_FPlane_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_wrap_objptr(L, 0);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 1);
    FPlane* C = (FPlane*)fc_get_wrap_objptr(L, 2);
    FPlane* D = (FPlane*)fc_get_wrap_objptr(L, 3);
    *Ret = FMatrix(*A, *B, *C, *D);
    return 0;
}
int FMatrixWrap::OperatorNew_FVector_FVector_FVector_FVector_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    FVector* C = (FVector*)fc_get_wrap_objptr(L, 2);
    FVector* D = (FVector*)fc_get_wrap_objptr(L, 3);
    *Ret = FMatrix(*A, *B, *C, *D);
    return 0;
}
int FMatrixWrap::SetIdentity_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    A->SetIdentity();
    return 0;
}

int FMatrixWrap::OperatorMul_FMatrix_FMatrix_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_wrap_objptr(L, 0);
    FMatrix* B = (FMatrix*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FMatrixWrap::OperatorMulSet_FMatrix_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FMatrix* B = (FMatrix*)fc_get_wrap_objptr(L, 0);
    (*A) *= (*B);
    return 0;
}
int FMatrixWrap::OperatorAdd_FMatrix_FMatrix_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_wrap_objptr(L, 0);
    FMatrix* B = (FMatrix*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) + (*B);
    return 0;
}
int FMatrixWrap::OperatorAddSet_FMatrix_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FMatrix* B = (FMatrix*)fc_get_wrap_objptr(L, 0);
    (*A) += (*B);
    return 0;
}
int FMatrixWrap::OperatorMul_FMatrix_double_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    *Ret = (*A) * B;
    return 0;
}
int FMatrixWrap::OperatorMul_double_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    (*A) *= B;
    return 0;
}

int FMatrixWrap::TransformFVector4_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 0);
    *Ret = A->TransformFVector4(*B);
    return 0;
}
int FMatrixWrap::TransformPosition_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->TransformPosition(*B);
    return 0;
}
int FMatrixWrap::InverseTransformPosition_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->InverseTransformPosition(*B);
    return 0;
}
int FMatrixWrap::TransformVector_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->TransformVector(*B);
    return 0;
}
int FMatrixWrap::InverseTransformVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->InverseTransformVector(*B);
    return 0;
}
int FMatrixWrap::GetTransposed_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetTransposed();
    return 0;
}

int FMatrixWrap::Determinant_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->Determinant());
    return 0;
}
int FMatrixWrap::RotDeterminant_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->RotDeterminant());
    return 0;
}
int FMatrixWrap::InverseFast_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->InverseFast();
    return 0;
}
int FMatrixWrap::Inverse_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->Inverse();
    return 0;
}
int FMatrixWrap::TransposeAdjoint_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->TransposeAdjoint();
    return 0;
}

int FMatrixWrap::RemoveScaling_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    A->RemoveScaling(Tolerance);
    return 0;
}
int FMatrixWrap::GetMatrixWithoutScale_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    *Ret = A->GetMatrixWithoutScale(Tolerance);
    return 0;

}
int FMatrixWrap::ExtractScaling_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    *Ret = A->ExtractScaling(Tolerance);
    return 0;
}
int FMatrixWrap::GetScaleVector_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    *Ret = A->GetScaleVector(Tolerance);
    return 0;
}
int FMatrixWrap::RemoveTranslation_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->RemoveTranslation();
    return 0;
}
int FMatrixWrap::ConcatTranslation_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->ConcatTranslation(*B);
    return 0;

}

int FMatrixWrap::ContainsNaN_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    fc_set_value_bool(fc_get_return_ptr(L), A->ContainsNaN());
    return 0;
}
int FMatrixWrap::ScaleTranslation_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    A->ScaleTranslation(*B);
    return 0;
}
int FMatrixWrap::GetMinimumAxisScale_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetMinimumAxisScale());
    return 0;
}
int FMatrixWrap::GetMaximumAxisScale_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetMaximumAxisScale());
    return 0;
}

int FMatrixWrap::ApplyScale_Wrap(fc_intptr L)
{
    FMatrix* Ret = (FMatrix*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->ApplyScale(fc_get_double(L, 0));
    return 0;
}
int FMatrixWrap::GetOrigin_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetOrigin();
    return 0;
}

int FMatrixWrap::GetColumn_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetColumn(fc_get_int(L, 0));
    return 0;
}
int FMatrixWrap::SetColumn_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    A->SetColumn(fc_get_int(L, 0), *B);
    return 0;
}
int FMatrixWrap::Rotator_Wrap(fc_intptr L)
{
    FRotator* Ret = (FRotator*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->Rotator();
    return 0;
}
int FMatrixWrap::ToQuat_Wrap(fc_intptr L)
{
    FQuat* Ret = (FQuat*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    *Ret = A->ToQuat();
    return 0;
}

int FMatrixWrap::GetFrustumNearPlane_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->GetFrustumNearPlane(*B));
    return 0;
}
int FMatrixWrap::GetFrustumFarPlane_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->GetFrustumFarPlane(*B));
    return 0;
}
int FMatrixWrap::GetFrustumLeftPlane_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->GetFrustumLeftPlane(*B));
    return 0;
}
int FMatrixWrap::GetFrustumRightPlane_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->GetFrustumRightPlane(*B));
    return 0;
}
int FMatrixWrap::GetFrustumTopPlane_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->GetFrustumTopPlane(*B));
    return 0;
}
int FMatrixWrap::GetFrustumBottomPlane_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->GetFrustumBottomPlane(*B));
    return 0;
}

int FMatrixWrap::ToString_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToString();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}
int FMatrixWrap::ComputeHash_Wrap(fc_intptr L)
{
    FMatrix* A = (FMatrix*)fc_get_inport_obj_ptr(L);
    fc_set_value_uint(fc_get_return_ptr(L), A->ComputeHash());
    return 0;
}