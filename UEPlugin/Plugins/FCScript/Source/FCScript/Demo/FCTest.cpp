#include "FCTest.h"

float UFCTest::GetHP() const
{
	return HP;
}

void UFCTest::SetIDList(const TArray<int32> &IDs)
{
	IDList = IDs;
}

bool UFCTest::GetIDList(TArray<int32> &IDs)
{
	IDs = IDList;
	return true;
}

void UFCTest::SetNameList(const TArray<FString> &Names)
{
	NameList = Names;
}

int UFCTest::NotifyAll(int nType, const FVector &Pos)
{
	return 100 + nType;
}

void UFCTest::HttpNotify(const FString &MessageContent, bool bWasSuccessful)
{
	OnResponseMessage.ExecuteIfBound(MessageContent, bWasSuccessful);
}

void UFCTest::CallClicked()
{
	OnClicked.Broadcast();
}
