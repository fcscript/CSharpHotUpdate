#include "FLinearColorWrap.h"
#include "FCObjectManager.h"

void FLinearColorWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FLinearColor");
    int nSize = sizeof(FLinearColor);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);
        
    fc_register_class_func(VM, nClassName, "FLinearColor_float_float_float_float", OperatorNew_float_float_float_float_Wrap);

    fc_register_class_func(VM, nClassName, "ToRGBE", ToRGBE_Wrap);
    fc_register_class_func(VM, nClassName, "FromSRGBColor", FromSRGBColor_Wrap);
    fc_register_class_func(VM, nClassName, "FromPow22Color", FromPow22Color_Wrap);

    fc_register_class_func(VM, nClassName, "+_FLinearColor_FLinearColor", OperatorAdd_FLinearColor_FLinearColor_Wrap);
    fc_register_class_func(VM, nClassName, "-_FLinearColor_FLinearColor", OperatorSub_FLinearColor_FLinearColor_Wrap);
    fc_register_class_func(VM, nClassName, "*_FLinearColor_FLinearColor", OperatorMul_FLinearColor_FLinearColor_Wrap);
    fc_register_class_func(VM, nClassName, "*_FLinearColor_float", OperatorMul_FLinearColor_float_Wrap);
    fc_register_class_func(VM, nClassName, "/_FLinearColor_FLinearColor", OperatorDiv_FLinearColor_FLinearColor_Wrap);
    fc_register_class_func(VM, nClassName, "/_FLinearColor_float", OperatorDiv_FLinearColor_float_Wrap);

    fc_register_class_func(VM, nClassName, "+=_FLinearColor_FLinearColor", OperatorAddSet_FLinearColor_FLinearColor_Wrap);
    fc_register_class_func(VM, nClassName, "-=_FLinearColor_FLinearColor", OperatorSubSet_FLinearColor_FLinearColor_Wrap);
    fc_register_class_func(VM, nClassName, "*=_FLinearColor_FLinearColor", OperatorMulSet_FLinearColor_FLinearColor_Wrap);
    fc_register_class_func(VM, nClassName, "*=_FLinearColor_float", OperatorMulSet_FLinearColor_float_Wrap);
    fc_register_class_func(VM, nClassName, "/=_FLinearColor_FLinearColor", OperatorDivSet_FLinearColor_FLinearColor_Wrap);
    fc_register_class_func(VM, nClassName, "/=_FLinearColor_float", OperatorDivSet_FLinearColor_float_Wrap);

    fc_register_class_func(VM, nClassName, "GetClamped", GetClamped_Wrap);
    fc_register_class_func(VM, nClassName, "MakeFromHSV8", MakeFromHSV8_Wrap);
    fc_register_class_func(VM, nClassName, "HSVToLinearRGB", HSVToLinearRGB_Wrap);
    fc_register_class_func(VM, nClassName, "LerpUsingHSV", LerpUsingHSV_Wrap);
    fc_register_class_func(VM, nClassName, "ToFColorSRGB", ToFColorSRGB_Wrap);
    fc_register_class_func(VM, nClassName, "ToFColor", ToFColor_Wrap);

    fc_register_class_func(VM, nClassName, "ToString", ToString_Wrap);
    fc_register_class_func(VM, nClassName, "InitFromString", InitFromString_Wrap);
}
int FLinearColorWrap::obj_new(fc_intptr L)
{
    return 0;
}
int FLinearColorWrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}
int FLinearColorWrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}
bool FLinearColorWrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FLinearColor* A = (FLinearColor*)LeftPtr;
    FLinearColor* B = (FLinearColor*)RightPtr;
    return *A == *B;
}

int FLinearColorWrap::OperatorNew_float_float_float_float_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FLinearColor(fc_get_float(L, 0), fc_get_float(L, 1), fc_get_float(L, 2), fc_get_float(L, 3));
    return 0;
}

int FLinearColorWrap::ToRGBE_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    *Ret = A->ToRGBE();
    return 0;
}
int FLinearColorWrap::FromSRGBColor_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FColor* A = (FColor*)fc_get_wrap_objptr(L, 0);
    *Ret = FLinearColor::FromSRGBColor(*A);
    return 0;
}
int FLinearColorWrap::FromPow22Color_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FColor* A = (FColor*)fc_get_wrap_objptr(L, 0);
    *Ret = FLinearColor::FromPow22Color(*A);
    return 0;
}

int FLinearColorWrap::OperatorAdd_FLinearColor_FLinearColor_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) + (*B);
    return 0;
}
int FLinearColorWrap::OperatorSub_FLinearColor_FLinearColor_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) - (*B);
    return 0;
}
int FLinearColorWrap::OperatorMul_FLinearColor_FLinearColor_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) * (*B);
    return 0;
}
int FLinearColorWrap::OperatorMul_FLinearColor_float_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    float B = fc_get_float(L, 1);
    *Ret = (*A) * B;
    return 0;
}
int FLinearColorWrap::OperatorDiv_FLinearColor_FLinearColor_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 1);
    *Ret = (*A) / (*B);
    return 0;
}
int FLinearColorWrap::OperatorDiv_FLinearColor_float_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    float B = fc_get_float(L, 1);
    *Ret = (*A) / B;
    return 0;
}

int FLinearColorWrap::OperatorAddSet_FLinearColor_FLinearColor_Wrap(fc_intptr L)
{
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    (*A) += (*B);
    return 0;
}
int FLinearColorWrap::OperatorSubSet_FLinearColor_FLinearColor_Wrap(fc_intptr L)
{
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    (*A) -= (*B);
    return 0;
}
int FLinearColorWrap::OperatorMulSet_FLinearColor_FLinearColor_Wrap(fc_intptr L)
{
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    (*A) *= (*B);
    return 0;
}
int FLinearColorWrap::OperatorMulSet_FLinearColor_float_Wrap(fc_intptr L)
{
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    (*A) *= fc_get_float(L, 0);
    return 0;
}
int FLinearColorWrap::OperatorDivSet_FLinearColor_FLinearColor_Wrap(fc_intptr L)
{
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    (*A) /= (*B);
    return 0;
}
int FLinearColorWrap::OperatorDivSet_FLinearColor_float_Wrap(fc_intptr L)
{
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    (*A) /= fc_get_float(L, 0);
    return 0;
}

int FLinearColorWrap::GetClamped_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    *Ret = A->GetClamped(fc_get_float(L, 0), fc_get_float(L, 1));
    return 0;
}
int FLinearColorWrap::MakeFromHSV8_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FLinearColor::MakeFromHSV8(fc_get_byte(L, 0), fc_get_byte(L, 1), fc_get_byte(L, 2));
    return 0;
}
int FLinearColorWrap::HSVToLinearRGB_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    *Ret = A->HSVToLinearRGB();
    return 0;
}
int FLinearColorWrap::LerpUsingHSV_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* B = (FLinearColor*)fc_get_wrap_objptr(L, 0);
    FLinearColor* C = (FLinearColor*)fc_get_wrap_objptr(L, 1);
    *Ret = FLinearColor::LerpUsingHSV(*B, *C, fc_get_float(L, 2));
    return 0;
}
int FLinearColorWrap::ToFColorSRGB_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    *Ret = A->ToFColorSRGB();
    return 0;
}
int FLinearColorWrap::ToFColor_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    *Ret = A->ToFColor(fc_get_bool(L, 0));
    return 0;
}

int FLinearColorWrap::ToString_Wrap(fc_intptr L)
{
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToString();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}
int FLinearColorWrap::InitFromString_Wrap(fc_intptr L)
{
    FLinearColor* A = (FLinearColor*)fc_get_inport_obj_ptr(L);
    const char* InStr = fc_cpp_get_string_a(L, 0);
    if (InStr)
    {
        FUTF8ToTCHAR  TempStr(InStr);
        FString  InitStr(TempStr.Get(), TempStr.Length());
        A->InitFromString(InitStr);
    }
    return 0;
}