#pragma once
#include "../../FCLib/include/fc_api.h"

class FQuatWrap
{
public:
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorAdd_FQuat_FQuat_Wrap(fc_intptr L);
    static int OperatorSub_FQuat_FQuat_Wrap(fc_intptr L);
    static int OperatorMul_FQuat_FQuat_Wrap(fc_intptr L);
    static int OperatorMul_FQuat_FVector_Wrap(fc_intptr L);
    static int OperatorMul_FQuat_FMatrix_Wrap(fc_intptr L);
    static int OperatorAddSet_FQuat_Wrap(fc_intptr L);
    static int OperatorSubSet_FQuat_Wrap(fc_intptr L);
    static int OperatorMulSet_FQuat_Wrap(fc_intptr L);
    static int OperatorNegative_Wrap(fc_intptr L);

    static int OperatorDot_FQuat_FQuat_Wrap(fc_intptr L);

    static int MakeFromEuler_Wrap(fc_intptr L);
    static int Euler_Wrap(fc_intptr L);
    static int Normalize_Wrap(fc_intptr L);
    static int GetNormalized_Wrap(fc_intptr L);
    static int IsNormalized_Wrap(fc_intptr L);

    static int Size_Wrap(fc_intptr L);
    static int SizeSquared_Wrap(fc_intptr L);
    static int GetAngle_Wrap(fc_intptr L);

    static int ToAxisAndAngle_double_Wrap(fc_intptr L);
    static int ToRotationVector_Wrap(fc_intptr L);
    static int MakeFromRotationVector_Wrap(fc_intptr L);

    static int ToSwingTwist_Wrap(fc_intptr L);
    static int GetTwistAngle_Wrap(fc_intptr L);

    static int RotateVector_Wrap(fc_intptr L);
    static int UnrotateVector_Wrap(fc_intptr L);
    static int Log_Wrap(fc_intptr L);
    static int Exp_Wrap(fc_intptr L);
    static int Inverse_Wrap(fc_intptr L);

    static int GetAxisX_Wrap(fc_intptr L);
    static int GetAxisY_Wrap(fc_intptr L);
    static int GetAxisZ_Wrap(fc_intptr L);
    static int GetForwardVector_Wrap(fc_intptr L);
    static int GetRightVector_Wrap(fc_intptr L);
    static int GetUpVector_Wrap(fc_intptr L);
    static int Vector_Wrap(fc_intptr L);
    static int Rotator_Wrap(fc_intptr L);
    static int ToMatrix_Wrap(fc_intptr L);
    static int ToMatrix_FMatrix_Wrap(fc_intptr L);

    static int GetRotationAxis_Wrap(fc_intptr L);
    static int AngularDistance_Wrap(fc_intptr L);
    static int ContainsNaN_Wrap(fc_intptr L);

    static int ToString_Wrap(fc_intptr L);
    static int InitFromString_Wrap(fc_intptr L);

    static int FindBetween_Wrap(fc_intptr L);
    static int FindBetweenNormals_Wrap(fc_intptr L);
    static int FindBetweenVectors_Wrap(fc_intptr L);
    static int FastLerp_Wrap(fc_intptr L);
    static int FastBilerp_Wrap(fc_intptr L);
    static int Slerp_NotNormalized_Wrap(fc_intptr L);
    static int Slerp_Wrap(fc_intptr L);

    static int SlerpFullPath_NotNormalized_Wrap(fc_intptr L);
    static int SlerpFullPath_Wrap(fc_intptr L);
    static int Squad_Wrap(fc_intptr L);
    static int SquadFullPath_Wrap(fc_intptr L);
    static int CalcTangents_Wrap(fc_intptr L);
};
