using System.Collections;

[export]
class TestProtobuf
{
    Transform transform;
    Text m_text;
    Button m_button2;
    Button m_button3;
    int m_nClickCount = 0;

    Button m_button4;
    Button m_button5;

    Button m_button6;
    Button m_button7;

    StringA m_msgBuffer;
    TestPersonProto PersonProto;
    public void Start()
    {
        m_text = transform.Find("Text").GetComponent<Text>();
        m_button2 = transform.Find("Button").GetComponent<Button>();
        m_button2.onClick.AddListener(OnClickButton);
        m_text.text = "脚本界面初化完成";
        m_button3 = transform.Find("Button3").GetComponent<Button>();
        m_button3.onClick.AddListener(OnClickButton3);

        m_button4 = transform.Find("Button4").GetComponent<Button>();
        m_button4.onClick.AddListener(OnClickButton4);
        m_button5 = transform.Find("Button5").GetComponent<Button>();
        m_button5.onClick.AddListener(OnClickButton5);

        m_button6 = transform.Find("Button6").GetComponent<Button>();
        m_button6.onClick.AddListener(OnClickButton6);
        m_button7 = transform.Find("Button7").GetComponent<Button>();
        m_button7.onClick.AddListener(OnClickButton7);
    }
    void  WriteProtobuf()
    {
        RequestWithSimpleOneof msg = new RequestWithSimpleOneof();
        msg.set_int_data(11);
        msg.set_str_data("abcdef");

        MoreData  more_data = msg.mutable_more_data();
        more_data.str_value = "moredata_str_value";

        Data  data = msg.mutable_message_data3();
        data.data_value = 12;
        FCSerialize ar = new FCSerialize();
        ar.OwnWriteMode(1024);
        msg.WriteTo(ar);

        ar.CopyTo(m_msgBuffer);
        os.print("序列化的的数据包的长度={0}字节", m_msgBuffer.Length);
        PrintMsg(msg);
    }
    void  PrintMsg(RequestWithSimpleOneof msg)
    {
        os.print("-----------------------------");
        if(msg.has_int_data())
            os.print("int_data = {0}", msg.get_int_data());
        else
            os.print("没有int_data");
        if (msg.has_more_data())
        {
            os.print("more_data.str_value={0}", msg.mutable_more_data().str_value);
        }
        else
        {
            os.print("没有more_data");
        }
        if(msg.has_message_data3())
        {
            os.print("message_data3.data_value={0}", msg.mutable_message_data3().data_value);
        }
        else
        {
            os.print("没有message_data3");
        }
        os.print("-----------------------------");
    }
    void ReadProtobuf()
    {
        RequestWithSimpleOneof msg = new RequestWithSimpleOneof();
        FCSerialize ar = new FCSerialize();
        ar.ReadMode(m_msgBuffer);
        msg.ReadFrom(ar);
        PrintMsg(msg);
    }

    void OnClickButton()
    {
        ++m_nClickCount;
        WriteProtobuf();
        m_text.text = "Protobuf数据已经写入." + m_nClickCount + ",Time:" + os.time_desc();
    }
    void OnClickButton3()
    {
        ReadProtobuf();
        m_text.text = "Protobuf数据已经读取，Time:" + os.time_desc();
    }
    void OnClickButton4()
    {
        if(PersonProto == null)
        {
            PersonProto = new TestPersonProto();
        }
        uint  nCostTime = PersonProto.WriteProto();
        StringA szTips = new StringA();
        szTips.Format("10W次反射写入，费时:{0}ms", nCostTime);
        m_text.text = szTips;
    }
    void OnClickButton5()
    {
        if(PersonProto != null)
        {
            uint nCostTime = PersonProto.ReadProto();
            StringA szTips = new StringA();
            szTips.Format("10W次反射读取，费时:{0}ms", nCostTime);
            m_text.text = szTips;
        }
        else
        {
            m_text.text = "请先执行写入操作, 注意看控制台日志";
        }
    }
    void OnClickButton6()
    {
        if (PersonProto == null)
        {
            PersonProto = new TestPersonProto();
        }
        uint nCostTime = PersonProto.NormalWriteProto();
        StringA szTips = new StringA();
        szTips.Format("10W次写入，费时:{0}ms", nCostTime);
        m_text.text = szTips;
    }
    void OnClickButton7()
    {
        if (PersonProto != null)
        {
            uint nCostTime = PersonProto.NormalReadProto();
            StringA szTips = new StringA();
            szTips.Format("10W次读取，费时:{0}ms", nCostTime);
            m_text.text = szTips;
        }
        else
        {
            m_text.text = "请先执行写入操作, 注意看控制台日志";
        }
    }
};

class TestPersonProto
{
    List<char> Buffer;
    int BufferLen;

    Person FillPersonMsg()
    {
        if (Buffer == null)
        {
            Buffer = new List<char>();
            Buffer.resize(1024);
        }
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

        return msg;
    }

    public uint ReadProto()
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
        return nEndTime - nBeginTime;
    }

    public uint WriteProto()
    {
        Person msg = FillPersonMsg();
        
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
        return nEndTime - nBeginTime;
    }

    public uint  NormalWriteProto()
    {
        Person msg = FillPersonMsg();

        int nBufferLen = 0;
        uint nBeginTime = os.GetTickCount();
        for (int i = 0; i < 100000; ++i)
        {
            FCSerialize ar = new FCSerialize();
            ar.WriteMode(Buffer, 0, 1024);
            msg.WriteTo(ar);
            nBufferLen = ar.GetPosition();
        }
        uint nEndTime = os.GetTickCount();
        os.print("write proto, cost time:{0}ms, BufferLen = {1}", (nEndTime - nBeginTime), nBufferLen);
        BufferLen = nBufferLen;
        return nEndTime - nBeginTime;
    }

    public uint  NormalReadProto()
    {
        uint nBeginTime = os.GetTickCount();
        for (int i = 0; i < 100000; ++i)
        {
            Person msg = new Person();

            FCSerialize ar = new FCSerialize();
            ar.ReadMode(Buffer, 0, BufferLen);
            msg.ReadFrom(ar);
        }
        uint nEndTime = os.GetTickCount();
        os.print("normal read proto, cost time:{0}ms, BufferLen = {1}", (nEndTime - nBeginTime), BufferLen);
        return nEndTime - nBeginTime;
    }    
}