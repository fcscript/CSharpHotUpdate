[export]
class FCProfile
{	
	UUserWidget  Self;
    public void ReceiveBeginPlay()
	{
		//Self.Button_0.OnClicked.AddListener(OnButtonClicked);		
		os.print("[FCTestScript]FCProfile:ReceiveBeginPlay");
		UButton button = GetButton("ButtonClose");
		button.OnClicked.AddListener(OnClickButtonClose);

        button = GetButton("ReadStr");
		button.OnClicked.AddListener(OnClickReadStr);        
        button = GetButton("ReadVector");
		button.OnClicked.AddListener(OnClickReadVector);        
        button = GetButton("ReadInt");
		button.OnClicked.AddListener(OnClickReadInt);
        
        button = GetButton("WriteStr");
		button.OnClicked.AddListener(OnClickWriteStr);
        button = GetButton("WriteVector");
		button.OnClicked.AddListener(OnClickWriteVector);
        button = GetButton("WriteInt");
		button.OnClicked.AddListener(OnClickWriteInt);
        
        button = GetButton("ArrayInt");
		button.OnClicked.AddListener(OnClickArrayInt);
        button = GetButton("ArrayByte");
		button.OnClicked.AddListener(OnClickArrayByte);
        button = GetButton("ArrayStr");
		button.OnClicked.AddListener(OnClickArrayStr);    
	}
    UButton GetButton(StringA Name)
    {
		UObject obj = UEUtil.GetChild(Self, Name);
		UButton button = (UButton)obj;
        if(button == null)
        {
		os.print("[FCTestScript]FCProfile:GetButton, failed get button({0})", Name);
        }
        return button;
    }
    void OnClickButtonClose()
    {
		Self.RemoveFromViewport();
    }
    void OnClickReadStr()
    {
        StringA  value;
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            value = UEUtil.GetChild(Self, "StrValue");
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Profile ReadStr, cost time:{0}, value={1}", costTick, value);
    }
    void OnClickReadVector()
    {
        Vector3  value;
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            value = UEUtil.GetChild(Self, "VectorValue");
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Profile ReadStr, cost time:{0}, value({1},{2},{3})", costTick, value.x, value.y, value.z);
    }
    void OnClickReadInt()
    {
        int  value;
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            value = UEUtil.GetChild(Self, "IntValue");
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Profile ReadInt, cost time:{0}, value={1}", costTick, value);
    }
    void OnClickWriteStr()
    {
        StringA  value = "aaa";
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            UEUtil.SetChild(Self, "StrValue", value);
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Profile WrtieStr, cost time:{0}, value={1}", costTick, value);
    }
    void OnClickWriteVector()
    {
        Vector3  value = new Vector3(2, 30, 5);
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            UEUtil.SetChild(Self, "VectorValue", value);
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Profile WriteVector, cost time:{0}, value({1},{2},{3})", costTick, value.x, value.y, value.z);
    }
    void OnClickWriteInt()
    {
        int  value = 10003;
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            UEUtil.SetChild(Self, "IntValue", value);
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Profile Writent, cost time:{0}, value={1}", costTick, value);
    }
    void OnClickArrayInt()
    {

    }
    void OnClickArrayByte()
    {

    }
    void OnClickArrayStr()
    {

    }
};