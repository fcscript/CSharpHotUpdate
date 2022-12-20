[export]
class FCProfile
{
	UUserWidget  Self;
    public void ReceiveBeginPlay()
	{
		os.print("[FCTestScript]FCProfile:ReceiveBeginPlay");
        AddButtonCallback("ButtonClose", OnClickButtonClose);
        AddButtonCallback("ReadStr", OnClickReadStr);
        AddButtonCallback("ReadVector", OnClickReadVector);
        AddButtonCallback("ReadInt", OnClickReadInt);

        AddButtonCallback("WriteStr", OnClickWriteStr);
        AddButtonCallback("WriteVector", OnClickWriteVector);
        AddButtonCallback("WriteInt", OnClickWriteInt);
        
        AddButtonCallback("ArrayInt", OnClickArrayInt);
        AddButtonCallback("ArrayByte", OnClickArrayByte);
        AddButtonCallback("ArrayStr", OnClickArrayStr);
        AddButtonCallback("Other", OnClickOther);        	
	}
    void  AddButtonCallback(StringA  ButtonName, delegate cb)
    {
		UObject obj = UEUtil.GetChild(Self, ButtonName);
		UButton button = (UButton)obj;
        if(button == null)
        {
		    os.print("[FCTestScript]FCProfile:AddButtonCallback, failed get button({0})", ButtonName);
        }
        else
        {
		    button.OnClicked.AddListener(cb);
        }
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
        TArray<int>  testArray = new TArray<int>();
        for(int i = 0; i<10; ++i)
        {
            testArray.Add(i);
        }
        int len = testArray.Length;        
        int  value = 0;

        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            int index = i%len;
            value = testArray[index];
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Array read int, cost time:{0}, value={1}", costTick, value);
    }
    void OnClickArrayByte()
    {
        TArray<byte>  testArray = new TArray<byte>();
        for(int i = 0; i<10; ++i)
        {
            testArray.Add(i);
        }
        int len = testArray.Length;        
        byte  value = 0;

        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            int index = i%len;
            value = testArray[index];
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Array read byte, cost time:{0}, value={1}", costTick, value);
    }
    void OnClickArrayStr()
    {
        TArray<StringA>  testArray = new TArray<StringA>();
        for(int i = 0; i<10; ++i)
        {
            testArray.Add(StringA.Format("{0}", i));
        }
        int len = testArray.Length;        
        StringA  value;

        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            int index = i%len;
            value = testArray[index];
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[FCTestScript]Array read byte, cost time:{0}, value={1}", costTick, value);
    }
    void OnClickOther()
    {
        os.print("[FCTestScript]OnClickOther-----------------");
        UFCTest obj = UEUtil.NewObject(null, "UFCTest", "", "");
        FTestAvatarSystemInitParams objAvatar = obj.AvatarParams;
        os.print("[FCTestScript]OnClickOther, obj={0}, AvatarParams={1}", obj, objAvatar);

        ProfileFrame.DoStruct(objAvatar);
        ProfileFrame.DoStructOne(objAvatar);

        obj.HP = 101;
        ProfileFrame.GetHP(obj);
        ProfileFrame.NotifyAll(obj);
        ProfileFrame.SetIDList(obj);
    }
};