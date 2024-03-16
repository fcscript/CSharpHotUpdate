#pragma once
#include "../../FCLib/include/fc_api.h"

class FRotatorWrap
{
public:
    static void Register(fc_intptr VM);
    static int obj_new(fc_intptr L);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr LeftPtr, fc_intptr RightPtr);

    static int OperatorAdd_FRotator_FRotator_Wrap(fc_intptr L);
    static int OperatorSub_FRotator_FRotator_Wrap(fc_intptr L);

    static int IsNearlyZero_Wrap(fc_intptr L);
    static int IsZero_Wrap(fc_intptr L);

    static int Add_Wrap(fc_intptr L);
    static int GetInverse_Wrap(fc_intptr L);
    static int GridSnap_Wrap(fc_intptr L);

    static int Vector_Wrap(fc_intptr L);
    static int Quaternion_Wrap(fc_intptr L);
    static int Euler_Wrap(fc_intptr L);
    static int RotateVector_Wrap(fc_intptr L);
    static int UnrotateVector_Wrap(fc_intptr L);

    static int Clamp_Wrap(fc_intptr L);
    static int GetNormalized_Wrap(fc_intptr L);
    static int GetDenormalized_Wrap(fc_intptr L);
    static int Normalize_Wrap(fc_intptr L);

    static int ToString_Wrap(fc_intptr L);
    static int InitFromString_Wrap(fc_intptr L);
    static int MakeFromEuler_Wrap(fc_intptr L);
};