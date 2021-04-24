
class TestPanel
{
	[Broadcast("OnEnterGame")]
	void  OnEnterGame(int UserID, StringA UserName)
	{
		print("优先级：0，    TestPanel::OnEnterGame(UserID = {0}, UserName = {1})", UserID, UserName);
	}
	[Broadcast("OnLeaveGame", 1000)]
	void OnLeaveGame()
	{
		print("优先级：1000， TestPanel::OnLeaveGame()");
	}
};

class UIPanelManger
{
	TestPanel  mTestPanel;
	[Broadcast("OnEnterGame", 10)]
	void  OnEnterGame(int UserID, StringA UserName)
	{
		print("优先级：10，   UIPanelManger::OnEnterGame(UserID = {0}, UserName = {1})", UserID, UserName);
		mTestPanel = new TestPanel;
	}
	[Broadcast("OnLeaveGame", 10)]
	void OnLeaveGame()
	{
		print("优先级：10，   UIPanelManger::OnLeaveGame()");
	}
};

UIPanelManger  ui_Mng;
[Broadcast("OnEnterGame", 1001)]
void  OnGlbEnterGame(int UserID, StringA UserName)
{
	print("优先级：1001， OnGlbEnterGame(UserID = {0}, UserName = {1})", UserID, UserName);
	ui_Mng = new UIPanelManger();
}

[Broadcast("OnLeaveGame", 1)]
void OnGlbLeaveGame()
{
	print("优先级：1，    OnGlbLeaveGame()");
}

export void  main()
{
	//BaseObject  obj = new BaseObject();
	print("-----------------start-------");
	System.Broadcast("OnEnterGame", 8008439, "笑看风云");
	print("-----------------leve game-------");
	System.Broadcast("OnLeaveGame", 8008439, "笑看风云");
	print("-----------------end-------");
}
