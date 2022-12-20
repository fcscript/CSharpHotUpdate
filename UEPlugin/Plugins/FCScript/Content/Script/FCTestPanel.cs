using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[export]
class FCTestPanel
{	
	UUserWidget  Self;
	bool bLockCall = false;
    //	
    // 摘要:
    //     ///
    //     ///
    public void ReceiveBeginPlay()
	{		
		os.print("[FCTestScript]FCTestPanel:ReceiveBeginPlay");
		AddButtonCallback("ButtonClose", OnButtonCloseClicked);
		AddButtonCallback("Button_0", OnButton1Clicked);
		AddButtonCallback("Button_1", OnButton2Clicked);
		AddButtonCallback("Button_2", OnButton1Clicked);		
	}
    public void ReceiveBeginDestroy()
	{
		int nRef = os.GetRef(this);
		os.print("[FCTestScript]FCTestPanel:ReceiveBeginDestroy, Ref={0}", nRef);
	}
    void  AddButtonCallback(StringA  ButtonName, delegate cb)
    {
		UObject obj = UEUtil.GetChild(Self, ButtonName);
		UButton button = (UButton)obj;
        if(button == null)
        {
		    os.print("[FCTestScript]FCTestPanel:AddButtonCallback, failed get button({0})", ButtonName);
        }
        else
        {
		    button.OnClicked.AddListener(cb);
        }
    }
	public void OnButtonCloseClicked()
	{
		os.print("[FCTestScript]FCTestPanel:OnButtonCloseClicked");
		Self.RemoveFromViewport();
	}
	public void OnButton1Clicked()
	{
		os.print("[FCTestScript]FCTestPanel:OnButton1Clicked");
		UEUtil.CallFunction(Self, "CallBlueprintFunc1", "Input Test Name, cutTime " + os.time_desc());
	}
	public void OnButton2Clicked()
	{
		os.print("[FCTestScript]FCTestPanel:OnButton2Clicked");
		int ID = UEUtil.GetChild(Self, "ID") + 1;
		UEUtil.SetChild(Self, "ID", ID);
		UEUtil.SetChild(Self, "Type", "Type" + ID);
		UEUtil.CallFunction(Self, "CallBlueprintFunc2");
		bLockCall = false;
	}
	public override FEventReply OnMouseButtonDown(FGeometry MyGeometry, FPointerEvent MouseEvent)
	{
		os.print("[FCTestScript]FCTestPanel:OnMouseButtonDown");
		UWorld world = UEUtil.GetWorld(Self);
		APlayerController localPlayerControler = UGameplayStatics.GetPlayerController(world, 0);
		if(localPlayerControler != null)
		{
			localPlayerControler.bShowMouseCursor = 1;
		}		
		return UWidgetBlueprintLibrary.Handled();
	}
	// 需要重载的接口
	public override void OnButtonEvent1(StringA Name)
	{
		os.print("[FCTestScript]FCTestPanel:OnButtonEvent1, Name={0}", Name);
	}
	// 需要重载的接口
	public override void OnButtonEvent2(int ID, StringA Type)
	{
		os.print("[FCTestScript]FCTestPanel:OnButtonEvent2, ID={0}, Type={1}", ID, Type);
		if(!bLockCall)
		{
			UEUtil.SuperCall(Self, "OnButtonEvent2", ID, Type);
		}
		bLockCall = true;
	}
}