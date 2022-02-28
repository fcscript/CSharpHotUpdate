
#pragma once

#include "UObject/Interface.h"
#include "FCScriptInterface.generated.h"

/**
 * Interface for binding UCLASS and Lua module
 */
UINTERFACE()
class FCSCRIPT_API UFCScriptInterface : public UInterface
{
    GENERATED_BODY()
};

class FCSCRIPT_API IFCScriptInterface
{
    GENERATED_BODY()
public:
    UFUNCTION(BlueprintNativeEvent)
    FString GetScriptClassName() const;
};
