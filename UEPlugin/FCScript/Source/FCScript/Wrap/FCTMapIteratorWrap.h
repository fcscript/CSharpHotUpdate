#pragma once
#include "../../FCLib/include/fc_api.h"

class FCTMapIteratorWrap
{
public:	
    static void Register(fc_intptr VM);

	static int obj_new(fc_intptr L);
	static int obj_del(fc_intptr nIntPtr);
	static int obj_release(fc_intptr nIntPtr);

	static int Key_wrap(fc_intptr nIntPtr);
	static int GetValue_wrap(fc_intptr nIntPtr);
	static int SetValue_wrap(fc_intptr nIntPtr);

	static int IsValid_wrap(fc_intptr nIntPtr);
	static int ToNext_wrap(fc_intptr nIntPtr);
	static int Reset_wrap(fc_intptr nIntPtr);
};
