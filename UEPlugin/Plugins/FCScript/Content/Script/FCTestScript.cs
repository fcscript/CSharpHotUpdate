using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[export]
class FCTestScript
{
	UFCTest  Self;
	float    HP = 100.0f;
	
	public ~FCTestScript()
	{
		os.print("[FCTestScript]::~FCTestScript()");
	}
	
	public void CheckMe()
	{
		os.print("[FCTestScript]CheckMe()");
	}
	
    //
    // 摘要:
    //     ///
    //     ///
    public void ReceiveBeginPlay()
	{
		Self.OnClicked.AddListener(OnButtonClicked);
		Self.OnResponseMessage.AddListener(OnResponseMessage);
        //Self.aID[0] = 1;
        TArray<StringA> NameList = Self.NameList;
        NameList.push_back("aaa");
        os.print("[FCTestScript]ReceiveBeginPlay");
	}
    public void ReceiveBeginDestroy()
	{
		int nRef = os.GetRef(this);
		os.print("[FCTestScript]ReceiveBeginDestroy, Ref={0}", nRef);
	}
	public void OnButtonClicked()
	{
		os.print("[FCTestScript]OnButtonClicked");
	}
	public void OnResponseMessage(StringA MessageContent, bool bWasSuccessful)
	{
		os.print("[FCTestScript]OnResponseMessage, MessageContent:{0}, bWasSuccessful:{1}", MessageContent, bWasSuccessful);
	}
	public override void NotifyObject(int nType, float x, float y, float z)
	{
		os.print("[FCTestScript]NotifyObject override, nType={0}, x={1}, y={2}, z={3}", nType, x, y, z);
	}
	public override float GetHP()
	{
		Self.HP = 2000.0f;
		os.print("[FCTestScript]NotifyObject override, HP={0}, ,Self.HP={1}", HP, Self.HP);
		//float OleHP = UEUtil.SuperReturnCall(0.0f, Self, "GetHP");
		//os.print("[FCTestScript]NotifyObject Native GetHP, HP={0}", OleHP);
		return HP;
	}
}