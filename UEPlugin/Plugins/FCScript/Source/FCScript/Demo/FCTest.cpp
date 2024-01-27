#include "FCTest.h"

float UFCTest::GetHP() const
{
    return HP;
}

void UFCTest::SetIDList(const TArray<int32>& IDs)
{
    IDList = IDs;
}

bool UFCTest::GetIDList(TArray<int32>& IDs)
{
    IDs = IDList;
    return true;
}

void UFCTest::SetIDMap(const TMap<int32, int32>& IDs)
{
    IDMap = IDs;
}

void UFCTest::GetIDS(TArray<int32>& OutIDS, TMap<int32, int32>& OutMap)
{
    OutIDS = IDList;
    OutMap = IDMap;
}

void UFCTest::SetIDSet(const TSet<int32>& InIDSet)
{
    IDSet = InIDSet;
}

void UFCTest::GetIDSet(TSet<int32>& OutIDSet)
{
    OutIDSet = IDSet;
}

void UFCTest::SetNameList(const TArray<FString>& Names)
{
    NameList = Names;
}

int UFCTest::NotifyAll(int nType, const FVector& Pos)
{
    return 100 + nType;
}

void UFCTest::HttpNotify(const FString& MessageContent, bool bWasSuccessful)
{
    OnResponseMessage.ExecuteIfBound(MessageContent, bWasSuccessful);
}

void UFCTest::CallClicked()
{
    OnClicked.Broadcast();
}

void UFCTest::CallDoubleClicked()
{
    OnDoubleClicked.Broadcast();
}
