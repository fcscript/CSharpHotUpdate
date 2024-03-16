#pragma once
#include "../../FCLib/include/fc_api.h"

class FPlaneWrap
{
public:
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorNew_FVector4_Wrap(fc_intptr L);
    static int OperatorNew_double_double_double_double_Wrap(fc_intptr L);
    static int OperatorNew_FVector_double_Wrap(fc_intptr L);
    static int OperatorNew_FVector_FVector_Wrap(fc_intptr L);
    static int OperatorNew_FVector_FVector_FVector_Wrap(fc_intptr L);

    static int OperatorAdd_FPlane_FPlane_Wrap(fc_intptr L);
    static int OperatorSub_FPlane_FPlane_Wrap(fc_intptr L);
    static int OperatorMul_FPlane_FPlane_Wrap(fc_intptr L);
    static int OperatorMul_FPlane_double_Wrap(fc_intptr L);
    static int OperatorDiv_FPlane_double_Wrap(fc_intptr L);
    static int OperatorAddSet_FPlane_Wrap(fc_intptr L);
    static int OperatorSubSet_FPlane_Wrap(fc_intptr L);
    static int OperatorMulSet_FPlane_Wrap(fc_intptr L);
    static int OperatorMulSet_double_Wrap(fc_intptr L);

    static int OperatorDot_FPlane_FPlane_Wrap(fc_intptr L);

    static int IsValid_Wrap(fc_intptr L);
    static int GetOrigin_Wrap(fc_intptr L);
    static int GetNormal_Wrap(fc_intptr L);
    static int PlaneDot_Wrap(fc_intptr L);
    static int Normalize_Wrap(fc_intptr L);
    static int Flip_Wrap(fc_intptr L);
    static int TransformBy_Wrap(fc_intptr L);
    static int TransformByUsingAdjointT_Wrap(fc_intptr L);
    static int TranslateBy_Wrap(fc_intptr L);
};