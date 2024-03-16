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

#include "../StructWrap/FVector2DWrap.h"
#include "../StructWrap/FVectorWrap.h"
#include "../StructWrap/FVector4Wrap.h"
#include "../StructWrap/FRotatorWrap.h"
#include "../StructWrap/FQuatWrap.h"
#include "../StructWrap/FPlaneWrap.h"
#include "../StructWrap/FMatrixWrap.h"
#include "../StructWrap/FColorWrap.h"
#include "../StructWrap/FLinearColorWrap.h"


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

    FVector2DWrap::Register(VM);
    FVectorWrap::Register(VM);
    FVector4Wrap::Register(VM);
    FRotatorWrap::Register(VM);
    FQuatWrap::Register(VM);
    FRotatorWrap::Register(VM);
    FPlaneWrap::Register(VM);
    FMatrixWrap::Register(VM);
    FColorWrap::Register(VM);
    FLinearColorWrap::Register(VM);
}

