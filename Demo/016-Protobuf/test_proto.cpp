
class TestPersonProto
{
    List<char> Buffer;
    int BufferLen;
    public void WriteProto()
    {
        Buffer = new List<char>();
        Buffer.resize(1024);

        Person msg = new Person();
        msg.name = "ilse";
        msg.age = 18;
        msg.address = "addr_show";
        msg.contacts = new List<Phone>();

        Phone Node = new Phone();
        Node.name = "alice";
        Node.val1 = 13;
        Node.val2 = 14;
        Node.phonenumber = 12312341234;
        Node.map_test = new map<StringA, int>();
        Node.map_test["show"] = 98;
        Node.map_test["ksi"] = 1232310;
        Node.map_test["idj"] = 933883838;
        msg.contacts.push_back(Node);

        Node = new Phone();
        Node.name = "lice";
        Node.val1 = 124;
        Node.phonenumber = 45645674567;
        msg.contacts.push_back(Node);

        Node = new Phone();
        Node.name = "lice3";
        Node.phonenumber = 723123123543;
        Node.map_test = new map<StringA, int>();
        Node.map_test["kisd"] = 1023;
        msg.contacts.push_back(Node);

        Node = new Phone();
        Node.name = "bob2";
        Node.phonenumber = 93123123543;
        Node.map_test = new map<StringA, int>();
        Node.map_test["tt1"] = 1001;
        Node.map_test["xxx"] = 1002;
        msg.contacts.push_back(Node);
        // 

        int nBufferLen = 0;
        uint nBeginTime = os.GetTickCount();
        for(int i = 0; i<100000; ++i)
        {
            FCSerialize ar = new FCSerialize();
            ar.WriteMode(Buffer, 0, 1024);
            //msg.WriteTo(ar);
			ar.ProtobufWriteObj(msg);
            nBufferLen = ar.GetPosition();
        }
        uint nEndTime = os.GetTickCount();
        os.print("write proto, cost time:{0}ms, BufferLen = {1}", (nEndTime - nBeginTime), nBufferLen);
        BufferLen = nBufferLen;
    }

    public void  ReadProto()
    {
        uint nBeginTime = os.GetTickCount();
        for (int i = 0; i < 100000; ++i)
        {
            Person msg = new Person();

            FCSerialize ar = new FCSerialize();
            ar.ReadMode(Buffer, 0, BufferLen);
            //msg.ReadFrom(ar);
			ar.ProtobufReadObj(msg);
        }
        uint nEndTime = os.GetTickCount();
        os.print("read proto, cost time:{0}ms, BufferLen = {1}", (nEndTime - nBeginTime), BufferLen);
    }
}