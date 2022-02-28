#pragma once
#include "../../FCLib/include/fc_api.h"

class FCUEUtilWrap
{
public:	
    static void Register(fc_intptr VM);

	static int GetName_wrap(fc_intptr L);
	static int GetOuter_wrap(fc_intptr L);
	static int GetClass_wrap(fc_intptr L);
	static int GetWorld_wrap(fc_intptr L);
	static int GetMainGameInstance_wrap(fc_intptr L);
	static int AddToRoot_wrap(fc_intptr L);
	static int RemoveFromRoot_wrap(fc_intptr L);
	static int NewObject_wrap(fc_intptr L);
    static int SpawActor_wrap(fc_intptr L);
    static int SuperCall_wrap(fc_intptr L);
    static int GetBindScript_wrap(fc_intptr L);
};