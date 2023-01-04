#pragma once
#include "../../FCLib/include/fc_api.h"
#include "FCDynamicClassDesc.h"

class FCTMapIteratorWrap
{
public:	
    static void Register(fc_intptr VM);

	static int obj_new(fc_intptr L);
	static int obj_del(fc_intptr nIntPtr);
    static int obj_release(fc_intptr nIntPtr);
    static int obj_hash(fc_intptr nIntPtr);
    static bool obj_equal(fc_intptr L, fc_intptr R);

	static int Key_wrap(fc_intptr L);
	static int GetValue_wrap(fc_intptr L);
	static int SetValue_wrap(fc_intptr L);

	static int IsValid_wrap(fc_intptr L);
	static int ToNext_wrap(fc_intptr L);
	static int Reset_wrap(fc_intptr L);

	static FScriptMap *GetScriptMap(int64 nIntPtr);

	static int32 ToNextValidIterator(FScriptMap* ScriptMap, int32 NextIndex);
};
