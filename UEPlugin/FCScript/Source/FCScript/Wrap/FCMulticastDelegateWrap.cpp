#include "FCMulticastDelegateWrap.h"
#include "FCDelegateWrap.h"
#include "FCObjectManager.h"
#include "FCGetObj.h"


void FCMulticastDelegateWrap::Register(fc_intptr VM)
{
	FCDelegateWrap::RegisterDelegate(VM, "MulticastDelegateEvent");
}
