#include "FVector2DWrap.h"
#include "FCObjectManager.h"

void FVector2DWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FVector2D");
    int nValueSize = sizeof(FVector2D);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);

    fc_register_class_func(VM, nClassName, "FVector4_double_double", OperatorNew_double_double_Wrap);

    fc_register_class_func(VM, nClassName, "+_FVector2D_FVector2D", OperatorAdd_FVector2D_FVector2D_Wrap);
    fc_register_class_func(VM, nClassName, "-_FVector2D_FVector2D", OperatorSub_FVector2D_FVector2D_Wrap);
    fc_register_class_func(VM, nClassName, "*_FVector2D_FVector2D", OperatorMul_FVector2D_FVector2D_Wrap);
    fc_register_class_func(VM, nClassName, "/_FVector2D_FVector2D", OperatorDiv_FVector2D_FVector2D_Wrap);

    fc_register_class_func(VM, nClassName, "+_FVector2D_double", OperatorAdd_FVector2D_double_Wrap);
    fc_register_class_func(VM, nClassName, "-_FVector2D_double", OperatorSub_FVector2D_double_Wrap);
    fc_register_class_func(VM, nClassName, "*_FVector2D_double", OperatorMul_FVector2D_double_Wrap);
    fc_register_class_func(VM, nClassName, "/_FVector2D_double", OperatorDiv_FVector2D_double_Wrap);

    fc_register_class_func(VM, nClassName, "+=_FVector2D", OperatorAddSet_FVector2D_Wrap);
    fc_register_class_func(VM, nClassName, "-=_FVector2D", OperatorSubSet_FVector2D_Wrap);
    fc_register_class_func(VM, nClassName, "*=_FVector2D", OperatorMulSet_FVector2D_Wrap);
    fc_register_class_func(VM, nClassName, "/=_FVector2D", OperatorDivSet_FVector2D_Wrap);

    fc_register_class_func(VM, nClassName, "+=_double", OperatorAddSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "-=_double", OperatorSubSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "*=_double", OperatorMulSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "/=_double", OperatorDivSet_double_Wrap);

    fc_register_class_func(VM, nClassName, "Zero", Zero_Wrap);
    fc_register_class_func(VM, nClassName, "One", One_Wrap);
    fc_register_class_func(VM, nClassName, "UnitX", UnitX_Wrap);
    fc_register_class_func(VM, nClassName, "UnitY", UnitY_Wrap);

    fc_register_class_func(VM, nClassName, "-", OperatorNegative_Wrap);

    fc_register_class_attrib(VM, nClassName, "[]", GetIndex_wrap, SetIndex_wrap);
    fc_register_class_func(VM, nClassName, "Set", Set_Wrap);

    fc_register_class_func(VM, nClassName, "^_FVector2D_FVector2D", CrossProduct_Wrap);
    fc_register_class_func(VM, nClassName, "Cross", Cross_Wrap);
    fc_register_class_func(VM, nClassName, "CrossProduct", CrossProduct_Wrap);
    fc_register_class_func(VM, nClassName, "|_FVector2D_FVector2D", DotProduct_Wrap);
    fc_register_class_func(VM, nClassName, "Dot", Dot_Wrap);
    fc_register_class_func(VM, nClassName, "DotProduct", DotProduct_Wrap);

    fc_register_class_func(VM, nClassName, "DistSquared", DistSquared_Wrap);
    fc_register_class_func(VM, nClassName, "Distance", Distance_Wrap);

    fc_register_class_func(VM, nClassName, "Max", Max_Wrap);
    fc_register_class_func(VM, nClassName, "Min", Min_Wrap);
    fc_register_class_func(VM, nClassName, "GetMax", GetMax_Wrap);
    fc_register_class_func(VM, nClassName, "GetAbsMax", GetAbsMax_Wrap);
    fc_register_class_func(VM, nClassName, "GetMin", GetMin_Wrap);

    fc_register_class_func(VM, nClassName, "Size", Size_Wrap);
    fc_register_class_func(VM, nClassName, "Length", Length_Wrap);
    fc_register_class_func(VM, nClassName, "SizeSquared", SizeSquared_Wrap);
    fc_register_class_func(VM, nClassName, "SquaredLength", SizeSquared_Wrap);

    fc_register_class_func(VM, nClassName, "GetRotated", GetRotated_Wrap);
    fc_register_class_func(VM, nClassName, "GetSafeNormal", GetSafeNormal_Wrap);
    fc_register_class_func(VM, nClassName, "Normalize", Normalize_Wrap);
    fc_register_class_func(VM, nClassName, "IsNearlyZero", IsNearlyZero_Wrap);
    fc_register_class_func(VM, nClassName, "IsZero", IsZero_Wrap);

    fc_register_class_func(VM, nClassName, "GetAbs", GetAbs_Wrap);
    fc_register_class_func(VM, nClassName, "ToString", ToString_Wrap);
}

int FVector2DWrap::obj_new(fc_intptr L)
{
    return 0;
}

int FVector2DWrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}

int FVector2DWrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}

bool FVector2DWrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FVector2D* A = (FVector2D*)LeftPtr;
    FVector2D* B = (FVector2D*)RightPtr;
    return *A == *B;
}

int FVector2DWrap::OperatorNew_double_double_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    Ret->X = fc_get_double(L, 0);
    Ret->Y = fc_get_double(L, 1);
    return 0;
}

int FVector2DWrap::OperatorAdd_FVector2D_FVector2D_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    *Ret = *A + *B;
    return 0;
}
int FVector2DWrap::OperatorSub_FVector2D_FVector2D_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    *Ret = *A - *B;
    return 0;
}
int FVector2DWrap::OperatorMul_FVector2D_FVector2D_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FVector2DWrap::OperatorDiv_FVector2D_FVector2D_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) / (*B);
    return 0;
}

int FVector2DWrap::OperatorAdd_FVector2D_double_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    Ret->X = A->X + B;
    Ret->Y = A->Y + B;
    return 0;

}
int FVector2DWrap::OperatorSub_FVector2D_double_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    Ret->X = A->X - B;
    Ret->Y = A->Y - B;
    return 0;
}
int FVector2DWrap::OperatorMul_FVector2D_double_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    Ret->X = A->X * B;
    Ret->Y = A->Y * B;
    return 0;
}
int FVector2DWrap::OperatorDiv_FVector2D_double_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    if(B != 0)
    {
        Ret->X = A->X / B;
        Ret->Y = A->Y / B;
    }
    return 0;
}

int FVector2DWrap::OperatorAddSet_FVector2D_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 0);
    *A += *B;
    return 0;
}
int FVector2DWrap::OperatorSubSet_FVector2D_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 0);
    *A -= *B;
    return 0;
}
int FVector2DWrap::OperatorMulSet_FVector2D_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 0);
    *A *= *B;
    return 0;
}
int FVector2DWrap::OperatorDivSet_FVector2D_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 0);
    *A /= *B;
    return 0;
}

int FVector2DWrap::OperatorAddSet_double_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X += B;
    A->Y += B;
    return 0;
}
int FVector2DWrap::OperatorSubSet_double_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X -= B;
    A->Y -= B;
    return 0;
}
int FVector2DWrap::OperatorMulSet_double_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X *= B;
    A->Y *= B;
    return 0;
}
int FVector2DWrap::OperatorDivSet_double_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    if(B != 0)
    {
        A->X /= B;
        A->Y /= B;
    }
    return 0;
}

int FVector2DWrap::Zero_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector2D::Zero();
    return 0;
}
int FVector2DWrap::One_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector2D::One();
    return 0;
}
int FVector2DWrap::UnitX_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector2D::UnitX();
    return 0;
}
int FVector2DWrap::UnitY_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector2D::UnitY();
    return 0;
}

int FVector2DWrap::OperatorNegative_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    *Ret = -(*A);
    return 0;
}

int FVector2DWrap::GetIndex_wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    int Index = fc_get_int(L, 0);
    if (Index >= 0 && Index < 2)
    {
        fc_set_value_double(fc_get_return_ptr(L), (*A)[Index]);
    }
    else
    {
        fc_set_value_double(fc_get_return_ptr(L), 0);
    }
    
    return 0;
}
int FVector2DWrap::SetIndex_wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    int Index = fc_get_int(L, 0);
    double Value = fc_get_double(L, 1);
    if (Index >= 0 && Index < 2)
    {
        (*A)[Index] = Value;
    }
    return 0;
}
int FVector2DWrap::Set_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    A->X = fc_get_double(L, 0);
    A->Y = fc_get_double(L, 1);
    return 0;
}

int FVector2DWrap::Cross_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 0);
    fc_set_value_double(fc_get_return_ptr(L), FVector2D::CrossProduct(*A, *B));
    return 0;
}

int FVector2DWrap::CrossProduct_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    fc_set_value_double(fc_get_return_ptr(L), FVector2D::CrossProduct(*A, *B));
    return 0;
}

int FVector2DWrap::Dot_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 0);
    fc_set_value_double(fc_get_return_ptr(L), A->Dot(*B));
    return 0;
}

int FVector2DWrap::DotProduct_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    fc_set_value_double(fc_get_return_ptr(L), FVector2D::DotProduct(*A, *B));
    return 0;
}

int FVector2DWrap::DistSquared_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    fc_set_value_double(fc_get_return_ptr(L), FVector2D::DistSquared(*A, *B));
    return 0;
}
int FVector2DWrap::Distance_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    fc_set_value_double(fc_get_return_ptr(L), FVector2D::Distance(*A, *B));
    return 0;
}

int FVector2DWrap::Max_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    *Ret = FVector2D::Max(*A, *B);
    return 0;
}
int FVector2DWrap::Min_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_wrap_objptr(L, 0);
    FVector2D* B = (FVector2D*)fc_get_wrap_objptr(L, 1);
    *Ret = FVector2D::Min(*A, *B);
    return 0;
}
int FVector2DWrap::GetMax_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetMax());
    return 0;
}
int FVector2DWrap::GetAbsMax_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetAbsMax());
    return 0;
}
int FVector2DWrap::GetMin_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->GetMin());
    return 0;
}

int FVector2DWrap::Size_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->Size());
    return 0;
}
int FVector2DWrap::Length_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->Length());
    return 0;
}
int FVector2DWrap::SizeSquared_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    fc_set_value_double(fc_get_return_ptr(L), A->SizeSquared());
    return 0;
}

int FVector2DWrap::GetRotated_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    double AngleDeg = fc_get_double(L, 0);
    *Ret = A->GetRotated(AngleDeg);
    return 0;
}
int FVector2DWrap::GetSafeNormal_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    *Ret = A->GetSafeNormal(Tolerance);
    return 0;
}
int FVector2DWrap::Normalize_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->Normalize(Tolerance));
    return 0;
}
int FVector2DWrap::IsNearlyZero_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsNearlyZero(Tolerance));
    return 0;
}
int FVector2DWrap::IsZero_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    fc_set_value_bool(fc_get_return_ptr(L), A->IsZero());
    return 0;
}

int FVector2DWrap::GetAbs_Wrap(fc_intptr L)
{
    FVector2D* Ret = (FVector2D*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetAbs();
    return 0;
}
int FVector2DWrap::ToString_Wrap(fc_intptr L)
{
    FVector2D* A = (FVector2D*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToString();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}