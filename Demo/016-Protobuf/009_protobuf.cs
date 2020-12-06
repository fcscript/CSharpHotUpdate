using System.Collections;

[export]
class TestProtobuf
{
    StringA m_msgBuffer;
    public void WriteProtobuf()
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

        os.print("more_data.str_value={0}", more_data.str_value);
        os.print("more_data.str_value={0}", msg.mutable_more_data().str_value);

        ar.CopyTo(m_msgBuffer);
        os.print("序列化的的数据包的长度={0}字节", m_msgBuffer.Length);
        PrintMsg(msg);
    }
    void  PrintMsg(RequestWithSimpleOneof msg)
    {
        os.print("-----------------------------");
        if (msg.has_int_data())
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
    public void ReadProtobuf()
    {
        RequestWithSimpleOneof msg = new RequestWithSimpleOneof();
        FCSerialize ar = new FCSerialize();
        ar.ReadMode(m_msgBuffer);
        msg.ReadFrom(ar);
        PrintMsg(msg);
    }
};

export void main()
{
    TestProtobuf buf = new TestProtobuf();
    buf.WriteProtobuf();
    buf.ReadProtobuf();
    
    os.print("---------------------------------------------------");
    TestPersonProto  PersonPB = new TestPersonProto();
    PersonPB.WriteProto();
    PersonPB.ReadProto();
}