using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class FCDemoSample
{
	public static void  TestSample()
	{
		os.print("[FCTestScript]OnGameStartup:Start");
		TestStruct2();
		os.print("[FCTestScript]OnGameStartup:End");		
	}
	public static void  TestOther()
	{
		os.print("[FCTestScript]OnGameStartup:Start");
		UFCTest obj = UEUtil.NewObject(null, "UFCTest", "FCTestScript", "");
		os.print("[FCTestScript]OnGameStartup:Start CallClicked");
		obj.WeakPtr.Reset();
		obj.CallClicked();
		os.print("[FCTestScript]OnGameStartup:Start HttpNotify");
		obj.HttpNotify("Script Notify", true);
		os.print("[FCTestScript]OnGameStartup, override NotifyObject");
		obj.NotifyObject(4, 198, 388, 566);
		float HP = obj.GetHP();
		float ObjHP = UEUtil.SuperCall(obj, "GetHP");
		os.print("[FCTestScript]OnGameStartup, override HP, obj.HP = {0}, ObjHP={1}", HP, ObjHP);
		FCTestScript ScriptIns = UEUtil.GetBindScript(obj);
		ScriptIns.CheckMe();
		TestList(obj);
		obj.IDList.push_back(10);
		obj.IDList.push_back(20);
		TArray<int>  aID = new TArray<int>();
		aID.push_back(10);
		obj.SetIDList(aID);
		os.print("[FCTestScript]aID.Length={0}", aID.Length);
		TestMap()
		os.print("[FCTestScript]OnGameStartup:End");
	}
	public static void  TestTArray_String()
	{
		TArray<StringW>   NameList = new TArray<StringW>();
		NameList.push_back("中国万岁");
		NameList.push_back("FC老牛比了");
		for(int i = 0; i<NameList.size(); ++i)
		{
			os.print("[FCTestScript]NameList[{0}]:{1}", i, NameList[i]);
		}
	}
	public static void  TestFString()
	{
		FString  Name = new FString();
		Name = "123456";
		os.print("[FCTestScript]Name:{0}", Name);
	}
	public static void  TestStruct1()
    {
		FTestBoneAdjustItemInfo info = new FTestBoneAdjustItemInfo();
		info.SlotName = "123";
		os.print("[FCTestScript]info.SlotName:{0}", info.SlotName);
	}
	public static void  TestStruct2()
	{
		FTestAvatarSystemInitParams  AvatarParams = new FTestAvatarSystemInitParams();
		FTestBoneAdjustItemInfo BoneAdjustItem = AvatarParams.BoneAdjustItem;
		BoneAdjustItem.SlotName = "123";
		os.print("[FCTestScript]BoneAdjustItem.SlotName:{0}", BoneAdjustItem.SlotName);
	}
	public static void  TestFuncCall(UFCTest obj)
	{
		os.print("[FCTestScript]OnGameStartup:Start HttpNotify");
		obj.HttpNotify("Script Notify", true);
		obj.NotifyObject(4, 198, 388, 566);
	}

	// 测试委托回调
	public static void  TestDelegate(UFCTest obj)
	{
		obj.CallClicked();
	}
	// 测试函数Override
	public static void  TestOverrideCall(UFCTest obj)
	{
		float HP = obj.GetHP();
	    float ObjHP = UEUtil.SuperCall(obj, "GetHP");
		os.print("[FCTestScript]OnGameStartup, override HP, obj.HP = {0}, ObjHP={1}", HP, ObjHP);
	}
	// 测试属性访问
	public static void  TestProperty(UFCTest obj)	
	{
		obj.ID = 100;
		obj.HP = 5000;
		os.print("[FCTestScript]obj.ID={0}, obj.HP={1}", obj.ID, obj.HP);
	}

	// 测试WeakPtr
	public static void  TestWeakPt(UFCTest obj)
	{
		obj.WeakPtr.Reset();
		bool bValidPtr = obj.WeakPtr.IsValid();
		if(bValidPtr)
			os.print("obj.WeakPtr is valid");
		else
			os.print("obj.WeakPtr is unvalid");
	}
	
	// 测试Int数组列表TArray<int>
	public static void  TestTArray_Int(UFCTest obj)
	{
		obj.IDList.push_back(10);
		obj.IDList.push_back(20);
		TArray<int>  aID1 = new TArray<int>();
		bool Suc = obj.GetIDList(aID1);
		
		os.print("[FCTestScript]aID1.Length={0}, Suc={1}", aID1.Length, Suc);
		for(int i = 0; i<aID1.size(); ++i)
		{
			os.print("[FCTestScript]aID1[{0}]={1}, obj.IDList[{2}]={3}", i, aID1[i], i, obj.IDList[i]);
		}
		
		TArray<int>  aID2 = new TArray<int>();
		aID2.push_back(100);
		aID2.push_back(101);
		aID2.push_back(103);
		obj.SetIDList(aID2);
		
		os.print("[FCTestScript]obj.IDList.Length={0}, aID2.Length={1}", obj.IDList.Length, aID2.Length);
		int nSize = aID2.size();
		for(int i = 0; i<nSize; ++i)
		{
			os.print("[FCTestScript]aID2[{0}]={1}, obj.IDList[{2}]={3}", i, aID2[i], i, obj.IDList[i]);
		}
	}
	public static void  TestTArray_String(UFCTest obj)
	{
		TArray<StringA>  nameList = obj.NameList;
		nameList.push_back("001");
		nameList.push_back("002");
		nameList.push_back("003");
		for(int i = 0; i<nameList.size(); ++i)
		{
			os.print("[FCTestScript]nameList[{0}]:{1}", i, nameList[i]);
		}
	}
	public static void  TestTArray_Vector()
	{
        TArray<FVector> vecList = new TArray<FVector>();
        for (int i = 0; i < 5; ++i)
        {
            FVector v = new FVector();
            v.Y = v.Z = v.X = i + 10;
            vecList.push_back(v);
        }
		for(int i = 0; i<vecList.size(); ++i)
		{
			FVector v = vecList[i];
			os.print("[FCTestScript]vecList[{0}]:{1}, {2}, {3}", i, v.X, v.Y, v.Z);
		}
    }
	
	// key : int, value : int
	public void  TestTMap_int_int()
	{
		TMap<int, int> aID = new TMap<int, int>();
		aID[1] = 1;
		aID[2] = 2;
		aID[3] = 3;
		int value = 0;
		if(aID.Find(2, value))
		{
			os.print("[FCTestScript]find key={0}, value={1}, aID.Length={2}", 2, value, aID.Length);
		}
		else
		{
			os.print("[FCTestScript]find find key={0}, aID.Length={1}", 2, aID.Length);
		}
		map<int, int> b = aID.ToMap();
		for(iterator it = b.begin(); it; ++it)
		{
			print("[FCTestScript]key={0}, value={1}", it.key, it.value);
		}
		b.RemoveAll();
		b[1] = 10;
		b[2] = 20;
		b[3] = 30;
		b[4] = 40;
		aID.SetMap(b);
		os.print("[FCTestScript]aID[1]={0}, aID[2]={1}, aID[3]={2}, aID.Length={3}", aID[1], aID[2], aID[3], aID.Length);
	}
	// key : int, value : string
	public void  TestTMap_int_string()
	{
		TMap<int, StringA> aIDName = new TMap<int, StringA>();
		aIDName[11] = "abc011";
		aIDName[20] = "abc020";
		aIDName[40] = "abc040";
		aIDName[33] = "abc033";
	}
};