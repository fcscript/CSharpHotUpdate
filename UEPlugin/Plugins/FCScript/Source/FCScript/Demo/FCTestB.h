#pragma once

#include "FCTest.h"
#include "FCTestB.generated.h"


UCLASS()
class UFCTestB : public UFCTest
{
	GENERATED_BODY()
public:
	UPROPERTY()
	class UObject* BasePtr;
};