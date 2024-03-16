#pragma once

#include "CoreMinimal.h"
#include "Delegates/DelegateCombinations.h"
#include "FCTest.generated.h"

DECLARE_DYNAMIC_MULTICAST_DELEGATE(FOnButtonClickedEvent);
DECLARE_DYNAMIC_DELEGATE_TwoParams(FOnHttpResponseMessage, FString, MessageContent, bool, bWasSuccessful);

USTRUCT(BlueprintType)
struct FTestBoneAdjustItemInfo
{
public:
	GENERATED_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	FString   SlotName;
	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	int32     ItemId;
	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	FName     BoneName;
	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	FVector   Scale;
	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	FVector   Offset;
};

USTRUCT(BlueprintType)
struct FTestAvatarSystemInitParams
{
	GENERATED_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	TArray<FString> HideBoneWhiteList;
	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	FString MaleFaceConfigPath;
	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	TArray<FTestBoneAdjustItemInfo>  BoneAdjustItemsTable;
	
	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	FTestBoneAdjustItemInfo   BoneAdjustItem;

	UPROPERTY(EditAnywhere, BlueprintReadWrite)
	FVector   Offset;
};

UENUM(BlueprintType)
enum EFCTestEnum
{
    One,
    Two,
    Three,
    Wan = 100,  // 测试发现，UE不支持255以上的默认参数
};

UCLASS()
class UFCTest : public UObject
{
	GENERATED_BODY()
public:
	UFUNCTION(BlueprintCallable, Category = "Attributes")
	float GetHP() const;
	
	UFUNCTION(BlueprintCallable, Category = "Attributes")
	void SetIDList(const TArray<int32> &IDs);
	
	UFUNCTION(BlueprintCallable, Category = "Attributes")
	bool GetIDList(TArray<int32> &IDs);

    UFUNCTION(BlueprintCallable, Category = "Attributes")
    void SetIDMap(const TMap<int32, int32> &IDs);

    UFUNCTION(BlueprintCallable, Category = "Attributes")
    void GetIDS(TArray<int32>& OutIDS, TMap<int32, int32> &OutMap);

    UFUNCTION(BlueprintCallable, Category = "Attributes")
    void SetIDSet(const TSet<int32> &InIDSet);

    UFUNCTION(BlueprintCallable, Category = "Attributes")
    void GetIDSet(TSet<int32>& OutIDSet);

	UFUNCTION(BlueprintCallable, Category = "Attributes")
	void SetNameList(const TArray<FString> &Names);

	UFUNCTION(BlueprintImplementableEvent, meta = (DisplayName = "NotifyObject"))
	void NotifyObject(int nType, float x, float y, float z);

	UFUNCTION(BlueprintCallable, meta = (DisplayName = "NotifyAll"))
	static int NotifyAll(int nType, const FVector &Pos);
	
	UFUNCTION(BlueprintCallable, meta = (DisplayName = "HttpNotify"))
	void HttpNotify(const FString &MessageContent, bool bWasSuccessful);
	
	UFUNCTION(BlueprintCallable, meta = (DisplayName = "CallClicked"))
	void CallClicked();

    UFUNCTION(BlueprintCallable, meta = (DisplayName = "CallDoubleClicked"))
    void CallDoubleClicked();
	
	
	//UPROPERTY()
	//TSharedPtr<UFCTest> SharedPtr;  //

	UPROPERTY()
	TWeakObjectPtr<UFCTest>   WeakPtr;

	UPROPERTY()
	TLazyObjectPtr<UFCTest>  LazyPtr;
	
	UPROPERTY()
	TSoftObjectPtr<UObject>  ResPtr;

    UPROPERTY(BlueprintReadWrite)
    TSoftClassPtr<UClass> TSoftClassPtrVar;

    UPROPERTY(BlueprintReadWrite)
    TSubclassOf<UObject> TSubclassOfVar;

    UPROPERTY(BlueprintReadWrite)
    TSubclassOf<UFCTest> TSubclassOfTest;

    UPROPERTY(BlueprintReadWrite)
    TEnumAsByte<enum EFCTestEnum> EnumAsByteVar;

	UPROPERTY()
	class UFCTest* NextPtr;
	UPROPERTY()
	int ID;
	UPROPERTY()
	float HP;
	UPROPERTY()
	int aID[3];
	UPROPERTY()
	FVector Pos;

    UPROPERTY(EditAnywhere, BlueprintReadWrite)
    TArray<FString> NameList;
	
    UPROPERTY(EditAnywhere, BlueprintReadWrite)
    TArray<int32> IDList;

    UPROPERTY(EditAnywhere, BlueprintReadWrite)
    TMap<int32, int32> IDMap;

    UPROPERTY(EditAnywhere, BlueprintReadWrite)
    TSet<int32> IDSet;

	UPROPERTY(BlueprintAssignable, Category = "Button|Event")
	FOnButtonClickedEvent OnClicked;

    UPROPERTY(BlueprintAssignable, Category = "Button|Event")
    FOnButtonClickedEvent OnDoubleClicked;
	
	UPROPERTY(BlueprintReadWrite, EditAnywhere)
	FOnHttpResponseMessage OnResponseMessage;

    UPROPERTY(BlueprintReadWrite, EditAnywhere)
    FTestAvatarSystemInitParams   AvatarParams;
};