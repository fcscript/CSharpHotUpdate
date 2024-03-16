#include "FColorWrap.h"
#include "FCObjectManager.h"

void FColorWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "FColor");
    int nSize = sizeof(FColor);
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_hash(VM, nClassName, obj_hash);
    fc_register_class_equal(VM, nClassName, obj_equal);

    fc_register_class_func(VM, nClassName, "FColor_byte_byte_byte_byte", OperatorNew_byte_byte_byte_byte_Wrap);
    fc_register_class_func(VM, nClassName, "FColor_uint32", OperatorNew_uint32_Wrap);

    fc_register_class_func(VM, nClassName, "+=_FColor", OperatorAddSet_FColor_Wrap);
    fc_register_class_func(VM, nClassName, "FromRGBE", FromRGBE_Wrap);
    fc_register_class_func(VM, nClassName, "FromHex", FromHex_Wrap);
    fc_register_class_func(VM, nClassName, "ToLinnerColor", ToLinnerColor_Wrap);

    fc_register_class_func(VM, nClassName, "ToPackedARGB", ToPackedARGB_Wrap);
    fc_register_class_func(VM, nClassName, "ToPackedABGR", ToPackedABGR_Wrap);
    fc_register_class_func(VM, nClassName, "ToPackedRGBA", ToPackedRGBA_Wrap);
    fc_register_class_func(VM, nClassName, "ToPackedBGRA", ToPackedBGRA_Wrap);

    fc_register_class_attrib_ex(VM, nClassName, "White", White_Get_Wrap, nullptr, nullptr, nullptr);
    fc_register_class_attrib_ex(VM, nClassName, "Black", Black_Get_Wrap, nullptr, nullptr, nullptr);
    fc_register_class_attrib_ex(VM, nClassName, "Transparent", Transparent_Get_Wrap, nullptr, nullptr, nullptr);
    fc_register_class_attrib_ex(VM, nClassName, "Red", Red_Get_Wrap, nullptr, nullptr, nullptr);
    fc_register_class_attrib_ex(VM, nClassName, "Green", Green_Get_Wrap, nullptr, nullptr, nullptr);
    fc_register_class_attrib_ex(VM, nClassName, "Blue", Blue_Get_Wrap, nullptr, nullptr, nullptr);
    fc_register_class_attrib_ex(VM, nClassName, "Yellow", Yellow_Get_Wrap, nullptr, nullptr, nullptr);

    fc_register_class_func(VM, nClassName, "ToHex", ToHex_Wrap);
    fc_register_class_func(VM, nClassName, "ToString", ToString_Wrap);
    fc_register_class_func(VM, nClassName, "InitFromString", InitFromString_Wrap);
}
int FColorWrap::obj_new(fc_intptr L)
{
    return 0;
}
int FColorWrap::obj_release(fc_intptr nIntPtr)
{
    return 0;
}
int FColorWrap::obj_hash(fc_intptr nIntPtr)
{
    return 0;
}
bool FColorWrap::obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr)
{
    FColor* A = (FColor*)LeftPtr;
    FColor* B = (FColor*)RightPtr;
    return *A == *B;
}

int FColorWrap::OperatorNew_byte_byte_byte_byte_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor(fc_get_byte(L, 0), fc_get_byte(L, 1), fc_get_byte(L, 2), fc_get_byte(L, 3));
    return 0;
}
int FColorWrap::OperatorNew_uint32_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor((uint32)fc_get_uint(L, 0));
    return 0;
}

int FColorWrap::OperatorAddSet_FColor_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    FColor* B = (FColor*)fc_get_wrap_objptr(L, 0);
    (*A) += (*B);
    return 0;
}
int FColorWrap::FromRGBE_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    *Ret = A->FromRGBE();
    return 0;
}
int FColorWrap::FromHex_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    const char* InStr = fc_cpp_get_string_a(L, 0);
    if (InStr)
    {
        FUTF8ToTCHAR  TempStr(InStr);
        FString  InitStr(TempStr.Get(), TempStr.Length());
        A->FromHex(InitStr);
    }
    return 0;
}
int FColorWrap::ToLinnerColor_Wrap(fc_intptr L)
{
    FLinearColor* Ret = (FLinearColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    *Ret = A->FromRGBE();
    return 0;
}

int FColorWrap::ToPackedARGB_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    fc_set_value_uint(fc_get_return_ptr(L), A->ToPackedARGB());
    return 0;
}
int FColorWrap::ToPackedABGR_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    fc_set_value_uint(fc_get_return_ptr(L), A->ToPackedABGR());
    return 0;
}
int FColorWrap::ToPackedRGBA_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    fc_set_value_uint(fc_get_return_ptr(L), A->ToPackedRGBA());
    return 0;
}
int FColorWrap::ToPackedBGRA_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    fc_set_value_uint(fc_get_return_ptr(L), A->ToPackedBGRA());
    return 0;
}

int FColorWrap::White_Get_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor::White;
    return 0;
}
int FColorWrap::Black_Get_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor::Black;
    return 0;
}
int FColorWrap::Transparent_Get_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor::Transparent;
    return 0;
}
int FColorWrap::Red_Get_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor::Red;
    return 0;
}
int FColorWrap::Green_Get_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor::Green;
    return 0;
}
int FColorWrap::Blue_Get_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor::Blue;
    return 0;
}
int FColorWrap::Yellow_Get_Wrap(fc_intptr L)
{
    FColor* Ret = (FColor*)fc_get_value_wrap_objptr(fc_get_return_ptr(L));
    *Ret = FColor::Yellow;
    return 0;
}

int FColorWrap::ToHex_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToHex();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}
int FColorWrap::ToString_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    FString  Desc = A->ToString();
    fc_set_value_string_w(fc_get_return_ptr(L), (fc_ushort_ptr)(*Desc), Desc.Len());
    return 0;
}
int FColorWrap::InitFromString_Wrap(fc_intptr L)
{
    FColor* A = (FColor*)fc_get_inport_obj_ptr(L);
    const char* InStr = fc_cpp_get_string_a(L, 0);
    if (InStr)
    {
        FUTF8ToTCHAR  TempStr(InStr);
        FString  InitStr(TempStr.Get(), TempStr.Length());
        A->InitFromString(InitStr);
    }
    return 0;
}
