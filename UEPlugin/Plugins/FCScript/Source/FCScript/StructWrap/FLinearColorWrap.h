#pragma once
#include "../../FCLib/include/fc_api.h"

class FLinearColorWrap
{
public:
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorNew_float_float_float_float_Wrap(fc_intptr L);

    static int ToRGBE_Wrap(fc_intptr L);
    static int FromSRGBColor_Wrap(fc_intptr L);
    static int FromPow22Color_Wrap(fc_intptr L);

    static int OperatorAdd_FLinearColor_FLinearColor_Wrap(fc_intptr L);
    static int OperatorSub_FLinearColor_FLinearColor_Wrap(fc_intptr L);
    static int OperatorMul_FLinearColor_FLinearColor_Wrap(fc_intptr L);
    static int OperatorMul_FLinearColor_float_Wrap(fc_intptr L);
    static int OperatorDiv_FLinearColor_FLinearColor_Wrap(fc_intptr L);
    static int OperatorDiv_FLinearColor_float_Wrap(fc_intptr L);

    static int OperatorAddSet_FLinearColor_FLinearColor_Wrap(fc_intptr L);
    static int OperatorSubSet_FLinearColor_FLinearColor_Wrap(fc_intptr L);
    static int OperatorMulSet_FLinearColor_FLinearColor_Wrap(fc_intptr L);
    static int OperatorMulSet_FLinearColor_float_Wrap(fc_intptr L);
    static int OperatorDivSet_FLinearColor_FLinearColor_Wrap(fc_intptr L);
    static int OperatorDivSet_FLinearColor_float_Wrap(fc_intptr L);

    static int GetClamped_Wrap(fc_intptr L);
    static int MakeFromHSV8_Wrap(fc_intptr L);
    static int HSVToLinearRGB_Wrap(fc_intptr L);
    static int LerpUsingHSV_Wrap(fc_intptr L);
    static int ToFColorSRGB_Wrap(fc_intptr L);
    static int ToFColor_Wrap(fc_intptr L);

    static int ToString_Wrap(fc_intptr L);
    static int InitFromString_Wrap(fc_intptr L);
};