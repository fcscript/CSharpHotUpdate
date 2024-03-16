#pragma once
#include "../../FCLib/include/fc_api.h"


class FVector2DWrap
{
public:
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorNew_double_double_Wrap(fc_intptr L);

    static int OperatorAdd_FVector2D_FVector2D_Wrap(fc_intptr L);
    static int OperatorSub_FVector2D_FVector2D_Wrap(fc_intptr L);
    static int OperatorMul_FVector2D_FVector2D_Wrap(fc_intptr L);
    static int OperatorDiv_FVector2D_FVector2D_Wrap(fc_intptr L);

    static int OperatorAdd_FVector2D_double_Wrap(fc_intptr L);
    static int OperatorSub_FVector2D_double_Wrap(fc_intptr L);
    static int OperatorMul_FVector2D_double_Wrap(fc_intptr L);
    static int OperatorDiv_FVector2D_double_Wrap(fc_intptr L);

    static int OperatorAddSet_FVector2D_Wrap(fc_intptr L);
    static int OperatorSubSet_FVector2D_Wrap(fc_intptr L);
    static int OperatorMulSet_FVector2D_Wrap(fc_intptr L);
    static int OperatorDivSet_FVector2D_Wrap(fc_intptr L);

    static int OperatorAddSet_double_Wrap(fc_intptr L);
    static int OperatorSubSet_double_Wrap(fc_intptr L);
    static int OperatorMulSet_double_Wrap(fc_intptr L);
    static int OperatorDivSet_double_Wrap(fc_intptr L);

    static int Zero_Wrap(fc_intptr L);
    static int One_Wrap(fc_intptr L);
    static int UnitX_Wrap(fc_intptr L);
    static int UnitY_Wrap(fc_intptr L);

    static int OperatorNegative_Wrap(fc_intptr L);

    static int GetIndex_wrap(fc_intptr L);
    static int SetIndex_wrap(fc_intptr L);
    static int Set_Wrap(fc_intptr L);

    static int Cross_Wrap(fc_intptr L);
    static int CrossProduct_Wrap(fc_intptr L);
    static int Dot_Wrap(fc_intptr L);
    static int DotProduct_Wrap(fc_intptr L);

    static int DistSquared_Wrap(fc_intptr L);
    static int Distance_Wrap(fc_intptr L);

    static int Max_Wrap(fc_intptr L);
    static int Min_Wrap(fc_intptr L);
    static int GetMax_Wrap(fc_intptr L);
    static int GetAbsMax_Wrap(fc_intptr L);
    static int GetMin_Wrap(fc_intptr L);

    static int Size_Wrap(fc_intptr L);
    static int Length_Wrap(fc_intptr L);
    static int SizeSquared_Wrap(fc_intptr L);

    static int GetRotated_Wrap(fc_intptr L);
    static int GetSafeNormal_Wrap(fc_intptr L);
    static int Normalize_Wrap(fc_intptr L);
    static int IsNearlyZero_Wrap(fc_intptr L);
    static int IsZero_Wrap(fc_intptr L);

    static int GetAbs_Wrap(fc_intptr L);
    static int ToString_Wrap(fc_intptr L);
};