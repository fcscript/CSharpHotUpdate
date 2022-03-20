#pragma once
#include "../../FCLib/include/fc_api.h"

class TWeakObjectPtrWrap
{
public:	
    static void Register(fc_intptr VM);
	static int obj_release(fc_intptr nIntPtr);
	static int Reset_wrap(fc_intptr L);
	static int IsValid_wrap(fc_intptr L);
	static int Get_wrap(fc_intptr L);
	static int Set_wrap(fc_intptr L);
};