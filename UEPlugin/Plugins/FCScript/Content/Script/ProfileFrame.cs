
class ProfileFrame
{
   public static void GetHP(UFCTest bpObj) 
   {
        uint nStartTime = os.GetTickCount();
        float value = 0;
        for(int i = 0; i<100000; ++i)
        {
            value = bpObj.GetHP();
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[ProfileFrame]call function: GetHP, cost time:{0}, value = {1}", costTick, value);
   }
   public  static void NotifyAll(UFCTest bpObj) 
   {
        Vector3 vPos = new Vector3(1, 2, 3)
        uint nStartTime = os.GetTickCount();
        int value = 0;
        for(int i = 0; i<100000; ++i)
        {
            value = UFCTest.NotifyAll(0, vPos);
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[ProfileFrame]call function: NotifyAll, cost time:{0}, value = {1}", costTick, value);
   }
   public static void SetIDList(UFCTest bpObj)
   {
        TArray<int> ids = new TArray<int>();
        for(int i = 0; i<10; ++i)
        {
            ids.push_back(i);
        }
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            bpObj.SetIDList(ids);
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[ProfileFrame]call function: SetIDList, cost time:{0}", costTick);
   }
   public static void DoStruct(FTestAvatarSystemInitParams avatarParam)
   {
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            avatarParam.BoneAdjustItem.SlotName = "abc";
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[ProfileFrame]set struct member second, cost time:{0}, slotName={1}", costTick, avatarParam.BoneAdjustItem.SlotName);
   }
   public static void DoStructOne(FTestAvatarSystemInitParams avatarParam)
   {
        uint nStartTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            avatarParam.MaleFaceConfigPath = "abc";
        }
        uint costTick = os.GetTickCount() - nStartTime;
        os.print("[ProfileFrame]set struct member one, cost time:{0}, MaleFaceConfigPath={1}", costTick, avatarParam.MaleFaceConfigPath);
   }
};