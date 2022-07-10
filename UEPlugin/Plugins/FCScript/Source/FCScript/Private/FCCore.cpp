
#include "FCCore.h"
#include "FCDynamicClassDesc.h"
#include "FCObjectManager.h"

UStruct* FC_FindUEClass(const char* UEClassName)
{
	const TCHAR* InName = UTF8_TO_TCHAR(UEClassName);
	const TCHAR* Name = (InName[0] == 'U' || InName[0] == 'A' || InName[0] == 'F' || InName[0] == 'E') ? InName + 1 : InName;
	UStruct* Struct = FindObject<UStruct>(ANY_PACKAGE, Name);       // find first
	if (!Struct)
	{
		Struct = LoadObject<UStruct>(nullptr, Name);                // load if not found
	}
	return Struct;
}
