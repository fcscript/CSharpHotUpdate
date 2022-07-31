using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[export]
class FCMainPanel
{	
	UUserWidget  Self;
	IEnumerator   m_coruntine;
    //	
    // 摘要:
    //     ///
    //     ///
    public void ReceiveBeginPlay()
	{
		//Self.Button_0.OnClicked.AddListener(OnButtonClicked);		
		os.print("[FCTestScript]FCMainPanel:ReceiveBeginPlay");
		UObject obj = UEUtil.GetChild(Self, "Button_0");
		UButton button = (UButton)obj;
		if(button != null)
		{
			button.OnClicked.AddListener(OnButtonClicked);		
			os.print("[FCTestScript]FCMainPanel:ReceiveBeginPlay, button is valid, bind click function");
		}
		else
		{
			os.print("[FCTestScript]FCMainPanel:ReceiveBeginPlay, button is invalid");
		}
	}
    public void ReceiveBeginDestroy()
	{
		int nRef = os.GetRef(this);
		os.print("[FCTestScript]FCMainPanel:ReceiveBeginDestroy, Ref={0}", nRef);
	}
	public void OnButtonClicked()
	{
		os.print("[FCTestScript]FCMainPanel:OnButtonClicked");
		// WidgetBlueprint'/Game/UMG/UMG_TestPanel.UMG_TestPanel'
		UWorld world = UEUtil.GetWorld(Self);
		StringA  ClassName = "/Game/UMG/UMG_TestPanel.UMG_TestPanel_C";
		UUserWidget panel = UEUtil.LoadUserWidget(world, ClassName, null);
		panel.AddToViewport(5001);
		if(panel != null)
		{
			os.print("[FCTestScript]succed load umg:{0}", ClassName);
		}
		else
		{
			os.print("[FCTestScript]failed load umg:{0}", ClassName);
		}
	}
	public override FEventReply OnMouseButtonDown(FGeometry MyGeometry, FPointerEvent MouseEvent)
	{
		os.print("OnMouseButtonDown");
		UWorld world = UEUtil.GetWorld(Self);
		APlayerController localPlayerControler = UGameplayStatics.GetPlayerController(world, 0);
		if(localPlayerControler != null)
		{
			localPlayerControler.bShowMouseCursor = 1;
		}

		os.print("world = {0}, localPlayerControler={1}, bShowMouseCursor={2}", world, localPlayerControler, localPlayerControler.bShowMouseCursor);
		
        //m_coruntine = new IEnumerator(this, DelaySetMouseCursor());
        //StartCoroutine(m_coruntine);
		return UWidgetBlueprintLibrary.Handled();
	}
	public override FEventReply OnMouseButtonUp(FGeometry MyGeometry, FPointerEvent MouseEvent)
	{
		os.print("OnMouseButtonUp");
		return UWidgetBlueprintLibrary.Handled();
	}

	IEnumerator  DelaySetMouseCursor()
	{
		UWorld world = UEUtil.GetWorld(Self);
		APlayerController localPlayerControler = UGameplayStatics.GetPlayerController(world, 0);
		localPlayerControler.bShowMouseCursor = 1;
		os.print("DelaySetMouseCursor, world={0}, bShowMouseCursor={1}", world, localPlayerControler.bShowMouseCursor);
        yield return 0;
	}
}