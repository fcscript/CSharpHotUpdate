#pragma once
#include "../../FCLib/include/fc_api.h"

class FCTArrayWrap
{
public:	
    static void Register(fc_intptr VM);
	
	static int obj_new(fc_intptr L);
	static int obj_del(fc_intptr nIntPtr);
	static int obj_release(fc_intptr nIntPtr);
	static int obj_hash(fc_intptr nIntPtr);
	static bool obj_equal(fc_intptr L, fc_intptr R);
	static int GetNumb_wrap(fc_intptr L);
	static int SetNumb_wrap(fc_intptr L);
	static int GetAt_wrap(fc_intptr L);
	static int SetAt_wrap(fc_intptr L);
	static int GetIndex_wrap(fc_intptr L);
	static int SetIndex_wrap(fc_intptr L);
	static int Add_wrap(fc_intptr L);
	static int Remove_wrap(fc_intptr L);
	static int Clear_wrap(fc_intptr L);
	static int ToList_wrap(fc_intptr L);
	static int SetList_wrap(fc_intptr L);
};