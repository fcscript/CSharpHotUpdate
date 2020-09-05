using System.Collections;

[export]
class TestProtobuf
{
    Transform transform;
    Text m_text;
    Button m_button2;
    Button m_button3;
    int m_nClickCount = 0;

    StringA m_msgBuffer;
    public void Start()
    {
        m_text = transform.Find("Text").GetComponent<Text>();
        m_button2 = transform.Find("Button").GetComponent<Button>();
        m_button2.onClick.AddListener(OnClieckButton);
        m_text.text = "脚本界面初化完成";
        m_button3 = transform.Find("Button3").GetComponent<Button>();
        m_button3.onClick.AddListener(OnClieckButton3);
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

    void OnClieckButton()
    {
        ++m_nClickCount;
        WriteProtobuf();
        m_text.text = "Protobuf数据已经写入." + m_nClickCount + ",Time:" + os.time_desc();
    }
    void OnClieckButton3()
    {
        ReadProtobuf();
        m_text.text = "Protobuf数据已经读取，Time:" + os.time_desc();
    }
};