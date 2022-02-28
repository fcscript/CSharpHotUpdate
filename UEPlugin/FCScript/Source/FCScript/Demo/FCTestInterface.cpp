#include "FCTestInterface.h"
#include "FCBrigeHelper.h"


FFCTestInterface* FFCTestInterface::Get()
{
	static FFCTestInterface sIns;
	return &sIns;
}

float  FFCTestInterface::GetHP() const
{
	return HP;
}

static const FCFuncLib_Reg FFCTestInterfaceLib[] =
{
	{ nullptr, nullptr }
};

BEGIN_EXPORT_CLASS(FFCTestInterface)
ADD_STATIC_FUNCTION(Get)
ADD_PROPERTY(HP)
ADD_FUNCTION(GetHP)
ADD_LIB(FFCTestInterfaceLib)
END_EXPORT_CLASS()
IMPLEMENT_EXPORTED_CLASS(FFCTestInterface)