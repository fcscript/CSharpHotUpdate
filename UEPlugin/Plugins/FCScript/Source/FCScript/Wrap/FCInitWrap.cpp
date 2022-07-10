#include "FCInitWrap.h"
#include "FCUEUtilWrap.h"
#include "FCDelegateWrap.h"
#include "FCMulticastDelegateWrap.h"
#include "FCTArrayWrap.h"
#include "FCTMapWrap.h"
#include "TWeakObjectPtrWrap.h"
#include "TLazyObjectPtrWrap.h"
#include "TSoftObjectPtrWrap.h"
#include "FCTMapIteratorWrap.h"
#include "FCTSetWrap.h"


void FCInitWrap::Register(fc_intptr VM)
{
	FCUEUtilWrap::Register(VM);
	FCDelegateWrap::Register(VM);
	FCMulticastDelegateWrap::Register(VM);
	FCTArrayWrap::Register(VM);
	FCTMapWrap::Register(VM);
	TWeakObjectPtrWrap::Register(VM);
	TLazyObjectPtrWrap::Register(VM);
	TSoftObjectPtrWrap::Register(VM);
	FCTMapIteratorWrap::Register(VM);
	FCTSetWrap::Register(VM);
}

