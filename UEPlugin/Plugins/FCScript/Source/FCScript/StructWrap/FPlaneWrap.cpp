#include "FPlaneWrap.h"
#include "FCObjectManager.h"

void FPlaneWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FPlane");
    int nSize = sizeof(FPlane);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);

    fc_register_class_func(VM, nClassName, "FPlane_FVector4", OperatorNew_FVector4_Wrap);
    fc_register_class_func(VM, nClassName, "FPlane_double_double_double_double", OperatorNew_double_double_double_double_Wrap);
    fc_register_class_func(VM, nClassName, "FPlane_FVector_double", OperatorNew_FVector_double_Wrap);
    fc_register_class_func(VM, nClassName, "FPlane_FVector_FVector", OperatorNew_FVector_FVector_Wrap);
    fc_register_class_func(VM, nClassName, "FPlane_FVector_FVector_FVector", OperatorNew_FVector_FVector_FVector_Wrap);

    fc_register_class_func(VM, nClassName, "+_FPlane_FPlane", OperatorAdd_FPlane_FPlane_Wrap);
    fc_register_class_func(VM, nClassName, "-_FPlane_FPlane", OperatorSub_FPlane_FPlane_Wrap);
    fc_register_class_func(VM, nClassName, "*_FPlane_FPlane", OperatorMul_FPlane_FPlane_Wrap);
    fc_register_class_func(VM, nClassName, "*_FPlane_double", OperatorMul_FPlane_double_Wrap);
    fc_register_class_func(VM, nClassName, "/_FPlane_double", OperatorDiv_FPlane_double_Wrap);
    fc_register_class_func(VM, nClassName, "+=_FPlane", OperatorAddSet_FPlane_Wrap);
    fc_register_class_func(VM, nClassName, "-=_FPlane", OperatorSubSet_FPlane_Wrap);
    fc_register_class_func(VM, nClassName, "*=_FPlane", OperatorMulSet_FPlane_Wrap);
    fc_register_class_func(VM, nClassName, "/=_double", OperatorMulSet_double_Wrap);

    fc_register_class_func(VM, nClassName, "|=_FPlane_FPlane", OperatorDot_FPlane_FPlane_Wrap);

    fc_register_class_func(VM, nClassName, "IsValid", IsValid_Wrap);
    fc_register_class_func(VM, nClassName, "GetOrigin", GetOrigin_Wrap);
    fc_register_class_func(VM, nClassName, "GetNormal", GetNormal_Wrap);
    fc_register_class_func(VM, nClassName, "PlaneDot", PlaneDot_Wrap);
    fc_register_class_func(VM, nClassName, "Normalize", Normalize_Wrap);
    fc_register_class_func(VM, nClassName, "Flip", Flip_Wrap);
    fc_register_class_func(VM, nClassName, "TransformBy", TransformBy_Wrap);
    fc_register_class_func(VM, nClassName, "TransformByUsingAdjointT", TransformByUsingAdjointT_Wrap);
    fc_register_class_func(VM, nClassName, "TranslateBy", TranslateBy_Wrap);
}

int FPlaneWrap::obj_new(fc_intptr L)
{
    return 0;
}
int FPlaneWrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}
int FPlaneWrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}
bool FPlaneWrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FPlane* A = (FPlane*)LeftPtr;
    FPlane* B = (FPlane*)RightPtr;
    return *A == *B;
}

int FPlaneWrap::OperatorNew_FVector4_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    *Ret = FPlane(*A);
    return 0;
}
int FPlaneWrap::OperatorNew_double_double_double_double_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FPlane(fc_get_double(L, 0), fc_get_double(L, 1), fc_get_double(L, 2), fc_get_double(L, 3));
    return 0;
}
int FPlaneWrap::OperatorNew_FVector_double_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = FPlane(*A, fc_get_double(L, 1));
    return 0;
}
int FPlaneWrap::OperatorNew_FVector_FVector_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    *Ret = FPlane(*A, *B);
    return 0;
}
int FPlaneWrap::OperatorNew_FVector_FVector_FVector_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector* A = (FVector*)fc_get_wrap_objptr(L, 0);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 1);
    FVector* C = (FVector*)fc_get_wrap_objptr(L, 2);
    *Ret = FPlane(*A, *B, *C);
    return 0;
}

int FPlaneWrap::OperatorAdd_FPlane_FPlane_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_wrap_objptr(L, 0);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 1);
    *Ret = *A + *B;
    return 0;
}
int FPlaneWrap::OperatorSub_FPlane_FPlane_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_wrap_objptr(L, 0);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 1);
    *Ret = *A - *B;
    return 0;
}
int FPlaneWrap::OperatorMul_FPlane_FPlane_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_wrap_objptr(L, 0);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FPlaneWrap::OperatorMul_FPlane_double_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    *Ret = (*A) * B;
    return 0;
}
int FPlaneWrap::OperatorDiv_FPlane_double_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    if(B != 0)
    {
        *Ret = (*A) / B;
    }
    return 0;
}
int FPlaneWrap::OperatorAddSet_FPlane_Wrap(fc_intptr L)
{
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    (*A) += (*B);
    return 0;
}
int FPlaneWrap::OperatorSubSet_FPlane_Wrap(fc_intptr L)
{
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    (*A) -= (*B);
    return 0;
}
int FPlaneWrap::OperatorMulSet_FPlane_Wrap(fc_intptr L)
{
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 0);
    (*A) *= (*B);
    return 0;
}
int FPlaneWrap::OperatorMulSet_double_Wrap(fc_intptr L)
{
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    (*A) *= B;
    return 0;
}

int FPlaneWrap::OperatorDot_FPlane_FPlane_Wrap(fc_intptr L)
{
    FPlane* A = (FPlane*)fc_get_wrap_objptr(L, 0);
    FPlane* B = (FPlane*)fc_get_wrap_objptr(L, 1);
    fc_set_value_double(fc_get_return_ptr(L), (*A) | (*B));
    return 0;
}

int FPlaneWrap::IsValid_Wrap(fc_intptr L)
{
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsValid());
    return 0;
}
int FPlaneWrap::GetOrigin_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetOrigin();
    return 0;
}
int FPlaneWrap::GetNormal_Wrap(fc_intptr L)
{
    FVector* Ret = (FVector*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetNormal();
    return 0;
}
int FPlaneWrap::PlaneDot_Wrap(fc_intptr L)
{
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    fc_set_value_double(fc_get_return_ptr(L), A->PlaneDot(*B));
    return 0;
}
int FPlaneWrap::Normalize_Wrap(fc_intptr L)
{
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->Normalize(Tolerance));
    return 0;
}
int FPlaneWrap::Flip_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    *Ret = A->Flip();
    return 0;
}
int FPlaneWrap::TransformBy_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    FMatrix* B = (FMatrix*)fc_get_wrap_objptr(L, 0);
    *Ret = A->TransformBy(*B);
    return 0;
}
int FPlaneWrap::TransformByUsingAdjointT_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    FMatrix* B = (FMatrix*)fc_get_wrap_objptr(L, 0);
    double C = fc_get_double(L, 1);
    FMatrix* D = (FMatrix*)fc_get_wrap_objptr(L, 2);
    *Ret = A->TransformByUsingAdjointT(*B, C, *D);
    return 0;
}
int FPlaneWrap::TranslateBy_Wrap(fc_intptr L)
{
    FPlane* Ret = (FPlane*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FPlane* A = (FPlane*)fc_get_inport_obj_ptr(L);
    FVector* B = (FVector*)fc_get_wrap_objptr(L, 0);
    *Ret = A->TranslateBy(*B);
    return 0;
}
