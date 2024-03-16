#pragma once
#include "../../FCLib/include/fc_api.h"

class FVectorWrap
{
public:	
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
	static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorNew_double_double_double_Wrap(fc_intptr L);

    static int Cross_Wrap(fc_intptr L);
    static int CrossProduct_Wrap(fc_intptr L);
    static int Dot_Wrap(fc_intptr L);
    static int DotProduct_Wrap(fc_intptr L);

    static int OperatorAdd_FVector_FVector_Wrap(fc_intptr L);
    static int OperatorSub_FVector_FVector_Wrap(fc_intptr L);
    static int OperatorMul_FVector_FVector_Wrap(fc_intptr L);
    static int OperatorDiv_FVector_FVector_Wrap(fc_intptr L);

    static int OperatorAdd_FVector_double_Wrap(fc_intptr L);
    static int OperatorSub_FVector_double_Wrap(fc_intptr L);
    static int OperatorMul_FVector_double_Wrap(fc_intptr L);
    static int OperatorDiv_FVector_double_Wrap(fc_intptr L);

    static int OperatorAddSet_FVector_Wrap(fc_intptr L);
    static int OperatorSubSet_FVector_Wrap(fc_intptr L);
    static int OperatorMulSet_FVector_Wrap(fc_intptr L);
    static int OperatorDivSet_FVector_Wrap(fc_intptr L);

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

    static int GetMax_Wrap(fc_intptr L);
    static int GetAbsMax_Wrap(fc_intptr L);
    static int GetMin_Wrap(fc_intptr L);
    static int GetAbsMin_Wrap(fc_intptr L);

    static int GetAbs_Wrap(fc_intptr L);
    static int Size_Wrap(fc_intptr L);
    static int Length_Wrap(fc_intptr L);
    static int SizeSquared_Wrap(fc_intptr L);
    static int SquaredLength_Wrap(fc_intptr L);
    static int Size2D_Wrap(fc_intptr L);
    static int SizeSquared2D_Wrap(fc_intptr L);

    static int IsNearlyZero_Wrap(fc_intptr L);
    static int IsZero_Wrap(fc_intptr L);
    static int IsNormalized_Wrap(fc_intptr L);
    static int Normalize_Wrap(fc_intptr L);

    static int ToOrientationRotator_Wrap(fc_intptr L);
    static int ToOrientationQuat_Wrap(fc_intptr L);
    static int Rotation_Wrap(fc_intptr L);
    static int ToString_Wrap(fc_intptr L);
    static int InitFromString_Wrap(fc_intptr L);
};