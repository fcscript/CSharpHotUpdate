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
		//Self.Button_0.OnClicked.AddListener(OnButtonClicked);		
		os.print("[FCTestScript]FCTestPanel:ReceiveBeginPlay");
		UButton button = GetButton("ButtonClose");
		if(button != null)
		{
			os.print("[FCTestScript]FCTestPanel:ReceiveBeginPlay, ButtonClose is valid");
		}
		else
		{
			os.print("[FCTestScript]FCTestPanel:ReceiveBeginPlay, ButtonClose is null");
		}
		button = GetButton("ButtonClose");
		button.OnClicked.AddListener(OnButtonCloseClicked);
		
		button = GetButton("Button_0");
		button.OnClicked.AddListener(OnButton1Clicked);
		
		button = GetButton("Button_1");
		button.OnClicked.AddListener(OnButton2Clicked);
		
		button = GetButton("Button_2");
		button.OnClicked.AddListener(OnButton1Clicked);
	}
    public void ReceiveBeginDestroy()
	{
		int nRef = os.GetRef(this);
		os.print("[FCTestScript]FCTestPanel:ReceiveBeginDestroy, Ref={0}", nRef);
	}
	UButton  GetButton(StringA ChildName)
	{
		UObject obj = UEUtil.GetChild(Self, ChildName);
		UButton button = (UButton)obj;
		return button;
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