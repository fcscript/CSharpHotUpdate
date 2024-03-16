#pragma once
#include "../../FCLib/include/fc_api.h"

class FMatrixWrap
{
public:
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorNew_FPlane_FPlane_FPlane_FPlane_Wrap(fc_intptr L);
    static int OperatorNew_FVector_FVector_FVector_FVector_Wrap(fc_intptr L);
    static int SetIdentity_Wrap(fc_intptr L);

    static int OperatorMul_FMatrix_FMatrix_Wrap(fc_intptr L);
    static int OperatorMulSet_FMatrix_Wrap(fc_intptr L);
    static int OperatorAdd_FMatrix_FMatrix_Wrap(fc_intptr L);
    static int OperatorAddSet_FMatrix_Wrap(fc_intptr L);
    static int OperatorMul_FMatrix_double_Wrap(fc_intptr L);
    static int OperatorMul_double_Wrap(fc_intptr L);

    static int TransformFVector4_Wrap(fc_intptr L);
    static int TransformPosition_Wrap(fc_intptr L);
    static int InverseTransformPosition_Wrap(fc_intptr L);
    static int TransformVector_Wrap(fc_intptr L);
    static int InverseTransformVector_Wrap(fc_intptr L);
    static int GetTransposed_Wrap(fc_intptr L);

    static int Determinant_Wrap(fc_intptr L);
    static int RotDeterminant_Wrap(fc_intptr L);
    static int InverseFast_Wrap(fc_intptr L);
    static int Inverse_Wrap(fc_intptr L);
    static int TransposeAdjoint_Wrap(fc_intptr L);

    static int RemoveScaling_Wrap(fc_intptr L);
    static int GetMatrixWithoutScale_Wrap(fc_intptr L);
    static int ExtractScaling_Wrap(fc_intptr L);
    static int GetScaleVector_Wrap(fc_intptr L);
    static int RemoveTranslation_Wrap(fc_intptr L);
    static int ConcatTranslation_Wrap(fc_intptr L);

    static int ContainsNaN_Wrap(fc_intptr L);
    static int ScaleTranslation_Wrap(fc_intptr L);
    static int GetMinimumAxisScale_Wrap(fc_intptr L);
    static int GetMaximumAxisScale_Wrap(fc_intptr L);

    static int ApplyScale_Wrap(fc_intptr L);
    static int GetOrigin_Wrap(fc_intptr L);

    static int GetColumn_Wrap(fc_intptr L);
    static int SetColumn_Wrap(fc_intptr L);
    static int Rotator_Wrap(fc_intptr L);
    static int ToQuat_Wrap(fc_intptr L);

    static int GetFrustumNearPlane_Wrap(fc_intptr L);
    static int GetFrustumFarPlane_Wrap(fc_intptr L);
    static int GetFrustumLeftPlane_Wrap(fc_intptr L);
    static int GetFrustumRightPlane_Wrap(fc_intptr L);
    static int GetFrustumTopPlane_Wrap(fc_intptr L);
    static int GetFrustumBottomPlane_Wrap(fc_intptr L);

    static int ToString_Wrap(fc_intptr L);
    static int ComputeHash_Wrap(fc_intptr L);
};