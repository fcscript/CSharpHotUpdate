#include "FCInitWrap.h"
#include "FCUEUtilWrap.h"
#include "FCDelegateWrap.h"
#include "FCMulticastDelegateWrap.h"
#include "FCTArrayWrap.h"
#include "FCTMapWrap.h"


void FCInitWrap::Register(fc_intptr VM)
{
	FCUEUtilWrap::Register(VM);
	FCDelegateWrap::Register(VM);
	FCMulticastDelegateWrap::Register(VM);
	FCTArrayWrap::Register(VM);
	FCTMapWrap::Register(VM);
}

