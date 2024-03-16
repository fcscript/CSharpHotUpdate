#include "FVector4Wrap.h"
#include "FCObjectManager.h"

void FVector4Wrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FVector4");
    int nValueSize = sizeof(FVector4);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);

    fc_register_class_func(VM, nClassName, "FVector4_double_double_double_double", OperatorNew_double_double_double_double_Wrap);

    fc_register_class_func(VM, nClassName, "+_FVector4_FVector4", OperatorAdd_FVector4_FVector4_Wrap);
    fc_register_class_func(VM, nClassName, "-_FVector4_FVector4", OperatorSub_FVector4_FVector4_Wrap);
    fc_register_class_func(VM, nClassName, "*_FVector4_FVector4", OperatorMul_FVector4_FVector4_Wrap);
    fc_register_class_func(VM, nClassName, "/_FVector4_FVector4", OperatorDiv_FVector4_FVector4_Wrap);

    fc_register_class_func(VM, nClassName, "+_FVector4_double", OperatorAdd_FVector4_double_Wrap);
    fc_register_class_func(VM, nClassName, "-_FVector4_double", OperatorSub_FVector4_double_Wrap);
    fc_register_class_func(VM, nClassName, "*_FVector4_double", OperatorMul_FVector4_double_Wrap);
    fc_register_class_func(VM, nClassName, "/_FVector4_double", OperatorDiv_FVector4_double_Wrap);

    fc_register_class_func(VM, nClassName, "+=_FVector4", OperatorAddSet_FVector4_Wrap);
    fc_register_class_func(VM, nClassName, "-=_FVector4", OperatorSubSet_FVector4_Wrap);
    fc_register_class_func(VM, nClassName, "*=_FVector4", OperatorMulSet_FVector4_Wrap);
    fc_register_class_func(VM, nClassName, "/=_FVector4", OperatorDivSet_FVector4_Wrap);

    fc_register_class_func(VM, nClassName, "+=_double", OperatorAddSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "-=_double", OperatorSubSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "*=_double", OperatorMulSet_double_Wrap);
    fc_register_class_func(VM, nClassName, "/=_double", OperatorDivSet_double_Wrap);

    fc_register_class_func(VM, nClassName, "-", OperatorNegative_Wrap);

    fc_register_class_func(VM, nClassName, "Zero", Zero_Wrap);
    fc_register_class_func(VM, nClassName, "One", One_Wrap);

    fc_register_class_attrib(VM, nClassName, "[]", GetIndex_wrap, SetIndex_wrap);
    fc_register_class_func(VM, nClassName, "Set", Set_Wrap);

    fc_register_class_func(VM, nClassName, "^_FVector4_FVector4", CrossProduct_Wrap);
    fc_register_class_func(VM, nClassName, "Cross", Cross_Wrap);
    fc_register_class_func(VM, nClassName, "CrossProduct", CrossProduct_Wrap);
    fc_register_class_func(VM, nClassName, "|_FVector4_FVector4", DotProduct_Wrap);
    fc_register_class_func(VM, nClassName, "Dot", Dot_Wrap);
    fc_register_class_func(VM, nClassName, "DotProduct", DotProduct_Wrap);

    fc_register_class_func(VM, nClassName, "GetSafeNormal", GetSafeNormal_Wrap);

    fc_register_class_func(VM, nClassName, "ToString", ToString_Wrap);
}

int FVector4Wrap::obj_new(fc_intptr L)
{
    return 0;
}

int FVector4Wrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}

int FVector4Wrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}

bool FVector4Wrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FVector4* A = (FVector4*)LeftPtr;
    FVector4* B = (FVector4*)RightPtr;
    return *A == *B;
}

int FVector4Wrap::OperatorNew_double_double_double_double_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    Ret->X = fc_get_double(L, 0);
    Ret->Y = fc_get_double(L, 1);
    Ret->Z = fc_get_double(L, 2);
    Ret->W = fc_get_double(L, 3);
    return 0;
}

int FVector4Wrap::OperatorAdd_FVector4_FVector4_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 1);
    *Ret = *A + *B;
    return 0;
}
int FVector4Wrap::OperatorSub_FVector4_FVector4_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 1);
    *Ret = *A - *B;
    return 0;
}
int FVector4Wrap::OperatorMul_FVector4_FVector4_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FVector4Wrap::OperatorDiv_FVector4_FVector4_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) / (*B);
    return 0;
}

int FVector4Wrap::OperatorAdd_FVector4_double_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    Ret->X = A->X + B;
    Ret->Y = A->Y + B;
    Ret->Z = A->Z + B;
    Ret->W = A->W + B;
    return 0;

}
int FVector4Wrap::OperatorSub_FVector4_double_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    Ret->X = A->X - B;
    Ret->Y = A->Y - B;
    Ret->Z = A->Z - B;
    Ret->W = A->W - B;
    return 0;
}
int FVector4Wrap::OperatorMul_FVector4_double_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    *Ret = *A * B;
    return 0;
}
int FVector4Wrap::OperatorDiv_FVector4_double_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    double B = fc_get_double(L, 1);
    if(B != 0)
    {
        *Ret = *A / B;
    }
    return 0;
}

int FVector4Wrap::OperatorAddSet_FVector4_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 0);
    *A += *B;
    return 0;
}
int FVector4Wrap::OperatorSubSet_FVector4_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 0);
    *A -= *B;
    return 0;
}
int FVector4Wrap::OperatorMulSet_FVector4_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 0);
    *A *= *B;
    return 0;
}
int FVector4Wrap::OperatorDivSet_FVector4_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 0);
    *A /= *B;
    return 0;
}

int FVector4Wrap::OperatorAddSet_double_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X += B;
    A->Y += B;
    A->Z += B;
    A->W += B;
    return 0;
}
int FVector4Wrap::OperatorSubSet_double_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X -= B;
    A->Y -= B;
    A->Z -= B;
    A->W -= B;
    return 0;
}
int FVector4Wrap::OperatorMulSet_double_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    A->X *= B;
    A->Y *= B;
    A->Z *= B;
    A->W *= B;
    return 0;
}
int FVector4Wrap::OperatorDivSet_double_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    double B = fc_get_double(L, 0);
    if(B != 0)
    {
        A->X /= B;
        A->Y /= B;
        A->Z /= B;
        A->W /= B;
    }
    return 0;
}

int FVector4Wrap::OperatorNegative_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    *Ret = -(*A);
    return 0;
}

int FVector4Wrap::Zero_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector4::Zero();
    return 0;
}
int FVector4Wrap::One_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FVector4::One();
    return 0;
}

int FVector4Wrap::GetIndex_wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    int Index = fc_get_int(L, 0);
    if (Index >= 0 && Index < 4)
    {
        fc_set_value_double(fc_get_return_ptr(L), (*A)[Index]);
    }
    else
    {
        fc_set_value_double(fc_get_return_ptr(L), 0);
    }
    
    return 0;
}
int FVector4Wrap::SetIndex_wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    int Index = fc_get_int(L, 0);
    double Value = fc_get_double(L, 1);
    if (Index >= 0 && Index < 4)
    {
        (*A)[Index] = Value;
    }
    return 0;
}
int FVector4Wrap::Set_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    A->X = fc_get_double(L, 0);
    A->Y = fc_get_double(L, 1);
    A->Z = fc_get_double(L, 2);
    A->W = fc_get_double(L, 3);
    return 0;
}

int FVector4Wrap::Cross_Wrap(fc_intptr L)
{
    // ^ 
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 0);
    *Ret = (*A) ^ (*B);
    return 0;
}

int FVector4Wrap::CrossProduct_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) ^ (*B);
    return 0;
}

int FVector4Wrap::Dot_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 0);
    fc_set_value_double(fc_get_return_ptr(L), Dot4(*A, *B));
    return 0;
}

int FVector4Wrap::DotProduct_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_wrap_objptr(L, 0);
    FVector4* B = (FVector4*)fc_get_wrap_objptr(L, 1);
    fc_set_value_double(fc_get_return_ptr(L), Dot4(*A, *B));
    return 0;
}

int FVector4Wrap::GetSafeNormal_Wrap(fc_intptr L)
{
    FVector4* Ret = (FVector4*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    double Tolerance = fc_get_double(L, 0);
    *Ret = A->GetSafeNormal(Tolerance);
    return 0;
}
int FVector4Wrap::ToString_Wrap(fc_intptr L)
{
    FVector4* A = (FVector4*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToString();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}
