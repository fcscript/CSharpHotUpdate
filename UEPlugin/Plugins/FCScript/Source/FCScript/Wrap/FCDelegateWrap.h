#pragma once
#include "../../FCLib/include/fc_api.h"

class FCDelegateWrap
{
public:	
    static void Register(fc_intptr VM);
    static void RegisterDelegate(fc_intptr VM, const char *ClassName);
	static int obj_release(fc_intptr nIntPtr);
	static int AddListener_wrap(fc_intptr L);
	static int RemoveListener_wrap(fc_intptr L);
	static int ClearLinstener_wrap(fc_intptr L);
	static int Invoke_wrap(fc_intptr L);
};