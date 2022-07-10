using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[export]
class FCUIEntryPoint
{	
	UObject  Self;
    //
    // 摘要:
    //     ///
    //     ///
    public void ReceiveBeginPlay()
	{
		os.print("[FCTestScript]FCUIEntryPoint:ReceiveBeginPlay");
		LoadMainPanel();
	}
    public void ReceiveBeginDestroy()
	{
		os.print("[FCTestScript]FCUIEntryPoint:ReceiveBeginDestroy");
	}
	void LoadMainPanel()
	{
		//local Widget = UWidgetBlueprintLibrary.Create(world, UClass.Load("/Game/UMG/UMG_MainPanel.UMG_MainPanel_C"))
		UWorld world = UEUtil.GetWorld(Self);
		StringA  ClassName = "/Game/UMG/UMG_MainPanel.UMG_MainPanel_C";
		UUserWidget panel = UEUtil.LoadUserWidget(world, ClassName, null);
		panel.AddToViewport(5000);
		if(panel != null)
		{
			os.print("[FCTestScript]FCUIEntryPoint:LoadMainPanel, succed load panel:{0}", ClassName);
		}
		else
		{
			os.print("[FCTestScript]FCUIEntryPoint:LoadMainPanel, failed load panel:{0}", ClassName);
		}
		//Widget:AddToViewport(5000)
		//Widget:Init()
	}
}