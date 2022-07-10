using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//typdef DelegateEvent  FOnButtonClickedEvent
//typdef MulticastDelegate  FOnHttpResponseMessage


public class FTestBoneAdjustItemInfo
{
	public StringW   SlotName;
	public int       ItemId;
	public StringW   BoneName;
	public FVector   Scale;
	public FVector   Offset;
};

public class FTestAvatarSystemInitParams
{
	public TArray<StringW> HideBoneWhiteList;
	public StringA MaleFaceConfigPath;
	public TArray<FTestBoneAdjustItemInfo>  BoneAdjustItemsTable;	
	public FTestBoneAdjustItemInfo   BoneAdjustItem;
	public FVector   Offset;
};

public class UFCTest : UObject
{
	public float GetHP(){return 0;}
	public void SetIDList(TArray<int> IDs){}
	public bool GetIDList(out TArray<int> IDs){}
	public void SetNameList(TArray<StringA> Names){}
	public void NotifyObject(int nType, float x, float y, float z){}
	public static int NotifyAll(int nType, Vector3 Pos){}
	public void HttpNotify(StringA MessageContent, bool bWasSuccessful){}
	public void CallClicked(){}

    public TArray<int> IDList { get; set; }
    public TArray<StringW> NameList { get; set; }

	public TWeakObjectPtr<UFCTest>  WeakPtr { get; set; }
	public TLazyObjectPtr<UFCTest>  LazyPtr { get; set; }

    public UFCTest NextPtr { get;  set; }
	public int ID { get;  set; }
	public float HP { get;  set; }
	public List<int> aID { get;  set; }
	public Vector3 Pos { get;  set; }
	public MulticastDelegateEvent OnClicked { get;  set; }
	public DelegateEvent OnResponseMessage { get;  set; }
};

pulbic class FFCTestInterface
{
	public float HP{ get; set; }
	public static FFCTestInterface Get(){ return null; }
	public float GetHP() { return 0;}
}