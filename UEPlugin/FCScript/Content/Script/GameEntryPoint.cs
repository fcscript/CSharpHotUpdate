using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[export]
class GameEntryPoint
{
    //
    // 摘要:
    //     ///
    //     ///
    public static void OnGameStartup()
	{
		os.print("[FCTestScript]OnGameStartup:Start");
		UFCTest obj = UEUtil.NewObject(null, "UFCTest", "FCTestScript", "");
		os.print("[FCTestScript]OnGameStartup:Start CallClicked");
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
	static void TestList(UFCTest obj)
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
	static void TestMap()
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
	public static void OnPreLoadMap(StringA szMapName)
	{
		os.print("[FCTestScript]OnPreLoadMap:{0}", szMapName);
	}
}