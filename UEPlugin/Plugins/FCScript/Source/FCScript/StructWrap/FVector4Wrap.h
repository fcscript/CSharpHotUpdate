#pragma once
#include "../../FCLib/include/fc_api.h"


class FVector4Wrap
{
public:
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorNew_double_double_double_double_Wrap(fc_intptr L);

    static int OperatorAdd_FVector4_FVector4_Wrap(fc_intptr L);
    static int OperatorSub_FVector4_FVector4_Wrap(fc_intptr L);
    static int OperatorMul_FVector4_FVector4_Wrap(fc_intptr L);
    static int OperatorDiv_FVector4_FVector4_Wrap(fc_intptr L);

    static int OperatorAdd_FVector4_double_Wrap(fc_intptr L);
    static int OperatorSub_FVector4_double_Wrap(fc_intptr L);
    static int OperatorMul_FVector4_double_Wrap(fc_intptr L);
    static int OperatorDiv_FVector4_double_Wrap(fc_intptr L);

    static int OperatorAddSet_FVector4_Wrap(fc_intptr L);
    static int OperatorSubSet_FVector4_Wrap(fc_intptr L);
    static int OperatorMulSet_FVector4_Wrap(fc_intptr L);
    static int OperatorDivSet_FVector4_Wrap(fc_intptr L);

    static int OperatorAddSet_double_Wrap(fc_intptr L);
    static int OperatorSubSet_double_Wrap(fc_intptr L);
    static int OperatorMulSet_double_Wrap(fc_intptr L);
    static int OperatorDivSet_double_Wrap(fc_intptr L);

    static int OperatorNegative_Wrap(fc_intptr L);

    static int Zero_Wrap(fc_intptr L);
    static int One_Wrap(fc_intptr L);

    static int GetIndex_wrap(fc_intptr L);
    static int SetIndex_wrap(fc_intptr L);
    static int Set_Wrap(fc_intptr L);

    static int Cross_Wrap(fc_intptr L);
    static int CrossProduct_Wrap(fc_intptr L);
    static int Dot_Wrap(fc_intptr L);
    static int DotProduct_Wrap(fc_intptr L);

    static int GetSafeNormal_Wrap(fc_intptr L);

    static int ToString_Wrap(fc_intptr L);
};