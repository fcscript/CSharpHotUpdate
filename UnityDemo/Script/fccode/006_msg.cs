using System.Collections;

[export]
class ItemMsg
{
    public int ID; // 道具的ID
    public string Name; // 道具的名字
    public List<int> Attribs; // 属性ID

    public void PrintMsg()
    {
        os.print("ID:{0}", ID);
        os.print("Name:{0}", Name);
        StringA szTemp = new StringA();
        for (int i = 0; i<Attribs.Length; ++i)
        {
            if (i > 0)
                szTemp += ',';
            szTemp += Attribs[i];
        }
        os.print("Attribs:{0}", szTemp);
    }
};

[export]
class ItemPackMsg
{
    public List<ItemMsg> Items;
    public void PrintMsg()
    {
        os.print("Item count:{0}", Items.Length);
        for(int i = 0; i<Items.Length; ++i)
        {
            Items[i].PrintMsg();
        }
    }
}

[export]
class LoginMsg
{
    public StringA szUserName;// 账号
    public StringA szPassword;// 密码
    public int nPlatType; // 平台类型    
    public StringA szPlatName; // 平台名字

    public void PrintMsg()
    {
        os.print("UserName:{0}", szUserName);
        os.print("Password:{0}", szPassword);
        os.print("PlatType:{0}", nPlatType);
        os.print("PlatName:{0}", szPlatName);
    }
};

[export]
class TestSerialize
{
    public static void ReceiveLoginMsg(CSerialize ar)
    {
        LoginMsg msg = new LoginMsg();
        ar.ReadWrite(msg);
        msg.PrintMsg();// 显示到屏幕
    }
    public static void ReceiveItemMsg(CSerialize ar)
    {
        ItemPackMsg msg = new ItemPackMsg();
        ar.ReadWrite(msg);
        msg.PrintMsg();// 显示到屏幕
    }
}
