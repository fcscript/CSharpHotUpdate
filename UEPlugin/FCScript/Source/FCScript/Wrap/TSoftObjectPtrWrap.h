#pragma once
#include "../../FCLib/include/fc_api.h"

class TSoftObjectPtrWrap
{
public:	
    static void Register(fc_intptr VM);
	static int obj_release(fc_intptr nIntPtr);
	static int LoadSynchronous_wrap(fc_intptr L);
	static int GetAssetName_wrap(fc_intptr L);
};