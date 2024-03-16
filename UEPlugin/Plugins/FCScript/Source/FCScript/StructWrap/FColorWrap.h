#pragma once
#include "../../FCLib/include/fc_api.h"

class FColorWrap
{
public:
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorNew_byte_byte_byte_byte_Wrap(fc_intptr L);
    static int OperatorNew_uint32_Wrap(fc_intptr L);

    static int OperatorAddSet_FColor_Wrap(fc_intptr L);
    static int FromRGBE_Wrap(fc_intptr L);
    static int FromHex_Wrap(fc_intptr L);
    static int ToLinnerColor_Wrap(fc_intptr L);

    static int ToPackedARGB_Wrap(fc_intptr L);
    static int ToPackedABGR_Wrap(fc_intptr L);
    static int ToPackedRGBA_Wrap(fc_intptr L);
    static int ToPackedBGRA_Wrap(fc_intptr L);

    static int White_Get_Wrap(fc_intptr L);
    static int Black_Get_Wrap(fc_intptr L);
    static int Transparent_Get_Wrap(fc_intptr L);
    static int Red_Get_Wrap(fc_intptr L);
    static int Green_Get_Wrap(fc_intptr L);
    static int Blue_Get_Wrap(fc_intptr L);
    static int Yellow_Get_Wrap(fc_intptr L);

    static int ToHex_Wrap(fc_intptr L);
    static int ToString_Wrap(fc_intptr L);
    static int InitFromString_Wrap(fc_intptr L);
};