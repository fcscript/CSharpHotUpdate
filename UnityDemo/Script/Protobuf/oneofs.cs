

public class RequestWithSimpleOneof
{
    //-----------------------------
    public enum dataCase
    {
        kstr_data = 5,
        kint_data = 6,
        kmessage_data = 7,
        kmore_data = 8,
        DATA_NOT_SET = 0,
    };
    public enum thrid_dataCase
    {
        kstr_data3 = 9,
        kint_data3 = 10,
        kmessage_data3 = 11,
        kmore_data3 = 12,
        THRID_DATA_NOT_SET = 0,
    };
    //-----------------------------
    [PBAttrib("Value = PB_Zip_Varint, Field = 1, Tag = 10")]
    public StringA  value;// = 1
    [PBAttrib("Value = PB_Zip_Varint, Field = 2, Tag = 18")]
    public StringA  optional_bytes;// = 2
    [PBAttrib("Value = PB_Zip_Varint, Field = 3, Tag = 26")]
    public MoreData  optional_data;// = 3
    [PBAttrib("Value = PB_Zip_Varint, Field = 4, Tag = 34")]
    public Data  second_data;// = 4
    //-----------------------------
    int  _oneof_case_0; // data
    [PBAttrib("Value = PB_Zip_Varint, Field = 5, Tag = 42, Case = _oneof_case_0")]
    public StringA  str_data;// = 5
    [PBAttrib("Value = PB_Zip_Varint, Field = 6, Tag = 48, Case = _oneof_case_0")]
    public int  int_data;// = 6
    [PBAttrib("Value = PB_Zip_Varint, Field = 7, Tag = 58, Case = _oneof_case_0")]
    public Data  message_data;// = 7
    [PBAttrib("Value = PB_Zip_Varint, Field = 8, Tag = 66, Case = _oneof_case_0")]
    public MoreData  more_data;// = 8
    //-----------------------------
    int  _oneof_case_1; // thrid_data
    [PBAttrib("Value = PB_Zip_Varint, Field = 9, Tag = 74, Case = _oneof_case_1")]
    public StringA  str_data3;// = 9
    [PBAttrib("Value = PB_Zip_Varint, Field = 10, Tag = 80, Case = _oneof_case_1")]
    public int  int_data3;// = 10
    [PBAttrib("Value = PB_Zip_Varint, Field = 11, Tag = 90, Case = _oneof_case_1")]
    public Data  message_data3;// = 11
    [PBAttrib("Value = PB_Zip_Varint, Field = 12, Tag = 98, Case = _oneof_case_1")]
    public MoreData  more_data3;// = 12
    //-----------------------------
    public void set_str_data(StringA _str_data)
    {
        _oneof_case_0 = 5;
        str_data = _str_data;
    }
    public void set_int_data(int _int_data)
    {
        _oneof_case_0 = 6;
        int_data = _int_data;
    }
    public void set_message_data(Data _message_data)
    {
        _oneof_case_0 = 7;
        message_data = _message_data;
    }
    public Data mutable_message_data()
    {
        _oneof_case_0 = 7;
        if(null == message_data)
            message_data = new Data();
        return message_data;
    }
    public void set_more_data(MoreData _more_data)
    {
        _oneof_case_0 = 8;
        more_data = _more_data;
    }
    public MoreData mutable_more_data()
    {
        _oneof_case_0 = 8;
        if(null == more_data)
            more_data = new MoreData();
        return more_data;
    }
    public void set_str_data3(StringA _str_data3)
    {
        _oneof_case_1 = 9;
        str_data3 = _str_data3;
    }
    public void set_int_data3(int _int_data3)
    {
        _oneof_case_1 = 10;
        int_data3 = _int_data3;
    }
    public void set_message_data3(Data _message_data3)
    {
        _oneof_case_1 = 11;
        message_data3 = _message_data3;
    }
    public Data mutable_message_data3()
    {
        _oneof_case_1 = 11;
        if(null == message_data3)
            message_data3 = new Data();
        return message_data3;
    }
    public void set_more_data3(MoreData _more_data3)
    {
        _oneof_case_1 = 12;
        more_data3 = _more_data3;
    }
    public MoreData mutable_more_data3()
    {
        _oneof_case_1 = 12;
        if(null == more_data3)
            more_data3 = new MoreData();
        return more_data3;
    }
    public bool has_str_data()
    {
        return 5 == _oneof_case_0;
    }
    public StringA get_str_data()
    {
        return str_data;
    }
    public bool has_int_data()
    {
        return 6 == _oneof_case_0;
    }
    public int get_int_data()
    {
        return int_data;
    }
    public bool has_message_data()
    {
        return 7 == _oneof_case_0;
    }
    public Data get_message_data()
    {
        return message_data;
    }
    public bool has_more_data()
    {
        return 8 == _oneof_case_0;
    }
    public MoreData get_more_data()
    {
        return more_data;
    }
    public bool has_str_data3()
    {
        return 9 == _oneof_case_1;
    }
    public StringA get_str_data3()
    {
        return str_data3;
    }
    public bool has_int_data3()
    {
        return 10 == _oneof_case_1;
    }
    public int get_int_data3()
    {
        return int_data3;
    }
    public bool has_message_data3()
    {
        return 11 == _oneof_case_1;
    }
    public Data get_message_data3()
    {
        return message_data3;
    }
    public bool has_more_data3()
    {
        return 12 == _oneof_case_1;
    }
    public MoreData get_more_data3()
    {
        return more_data3;
    }
    [export]
    public void  WriteTo(FCSerialize ar)
    {
        ar.ProtobufWrite(value, 1, PB_ZipType.PB_Zip_Varint);
        ar.ProtobufWrite(optional_bytes, 2, PB_ZipType.PB_Zip_Varint);
        ar.ProtobufWrite(optional_data, 3, PB_ZipType.PB_Zip_Varint);
        ar.ProtobufWrite(second_data, 4, PB_ZipType.PB_Zip_Varint);
        switch(_oneof_case_0)
        {
        case 5:
            ar.ProtobufWrite(str_data, 5, PB_ZipType.PB_Zip_Varint);
            break;
        case 6:
            ar.ProtobufWrite(int_data, 6, PB_ZipType.PB_Zip_Varint);
            break;
        case 7:
            ar.ProtobufWrite(message_data, 7, PB_ZipType.PB_Zip_Varint);
            break;
        case 8:
            ar.ProtobufWrite(more_data, 8, PB_ZipType.PB_Zip_Varint);
            break;
         default:
            break;
        }
        switch(_oneof_case_1)
        {
        case 9:
            ar.ProtobufWrite(str_data3, 9, PB_ZipType.PB_Zip_Varint);
            break;
        case 10:
            ar.ProtobufWrite(int_data3, 10, PB_ZipType.PB_Zip_Varint);
            break;
        case 11:
            ar.ProtobufWrite(message_data3, 11, PB_ZipType.PB_Zip_Varint);
            break;
        case 12:
            ar.ProtobufWrite(more_data3, 12, PB_ZipType.PB_Zip_Varint);
            break;
         default:
            break;
        }
    }
    [export]
    public void  ReadFrom(FCSerialize ar)
    {
        int nTag = 0;
        int nFiledIndex = 0;
        bool bSucRead = false;
        //while(nTag = ar.ProtobufReadTag()) // C#不支持这样的写法
        while((nTag = ar.ProtobufReadTag()) != 0)
        {
            nFiledIndex = nTag >> 3;
            bSucRead = false;
            switch(nFiledIndex)
            {
            case 1:
                if(nTag == 10) // MakeTag(1, 2)
                {
                    bSucRead = ar.ProtobufRead(value, 1, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 2:
                if(nTag == 18) // MakeTag(2, 2)
                {
                    bSucRead = ar.ProtobufRead(optional_bytes, 2, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 3:
                if(nTag == 26) // MakeTag(3, 2)
                {
                    bSucRead = ar.ProtobufRead(optional_data, 3, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 4:
                if(nTag == 34) // MakeTag(4, 2)
                {
                    bSucRead = ar.ProtobufRead(second_data, 4, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 5:
                if(nTag == 42) // MakeTag(5, 2)
                {
                    _oneof_case_0 = 5; // str_data 
                    bSucRead = ar.ProtobufRead(str_data, 5, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 6:
                if(nTag == 48) // MakeTag(6, 0)
                {
                    _oneof_case_0 = 6; // int_data 
                    bSucRead = ar.ProtobufRead(int_data, 6, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 7:
                if(nTag == 58) // MakeTag(7, 2)
                {
                    _oneof_case_0 = 7; // message_data 
                    bSucRead = ar.ProtobufRead(message_data, 7, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 8:
                if(nTag == 66) // MakeTag(8, 2)
                {
                    _oneof_case_0 = 8; // more_data 
                    bSucRead = ar.ProtobufRead(more_data, 8, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 9:
                if(nTag == 74) // MakeTag(9, 2)
                {
                    _oneof_case_1 = 9; // str_data3 
                    bSucRead = ar.ProtobufRead(str_data3, 9, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 10:
                if(nTag == 80) // MakeTag(10, 0)
                {
                    _oneof_case_1 = 10; // int_data3 
                    bSucRead = ar.ProtobufRead(int_data3, 10, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 11:
                if(nTag == 90) // MakeTag(11, 2)
                {
                    _oneof_case_1 = 11; // message_data3 
                    bSucRead = ar.ProtobufRead(message_data3, 11, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 12:
                if(nTag == 98) // MakeTag(12, 2)
                {
                    _oneof_case_1 = 12; // more_data3 
                    bSucRead = ar.ProtobufRead(more_data3, 12, PB_ZipType.PB_Zip_Varint);
                }
                break;
            default:
                break;
            }
            if(!bSucRead)
            {
                ar.ProtobufSkipField(nTag);
            }
        }
    }
};


public class Data
{
    //-----------------------------
    //-----------------------------
    [PBAttrib("Value = PB_Zip_Varint, Field = 1, Tag = 8")]
    public int  data_value;// = 1
    [export]
    public void  WriteTo(FCSerialize ar)
    {
        ar.ProtobufWrite(data_value, 1, PB_ZipType.PB_Zip_Varint);
    }
    [export]
    public void  ReadFrom(FCSerialize ar)
    {
        int nTag = 0;
        int nFiledIndex = 0;
        bool bSucRead = false;
        //while(nTag = ar.ProtobufReadTag()) // C#不支持这样的写法
        while((nTag = ar.ProtobufReadTag()) != 0)
        {
            nFiledIndex = nTag >> 3;
            bSucRead = false;
            switch(nFiledIndex)
            {
            case 1:
                if(nTag == 8) // MakeTag(1, 0)
                {
                    bSucRead = ar.ProtobufRead(data_value, 1, PB_ZipType.PB_Zip_Varint);
                }
                break;
            default:
                break;
            }
            if(!bSucRead)
            {
                ar.ProtobufSkipField(nTag);
            }
        }
    }
};


public class MoreData
{
    //-----------------------------
    //-----------------------------
    [PBAttrib("Value = PB_Zip_Varint, Field = 1, Tag = 10")]
    public StringA  str_value;// = 1
    [export]
    public void  WriteTo(FCSerialize ar)
    {
        ar.ProtobufWrite(str_value, 1, PB_ZipType.PB_Zip_Varint);
    }
    [export]
    public void  ReadFrom(FCSerialize ar)
    {
        int nTag = 0;
        int nFiledIndex = 0;
        bool bSucRead = false;
        //while(nTag = ar.ProtobufReadTag()) // C#不支持这样的写法
        while((nTag = ar.ProtobufReadTag()) != 0)
        {
            nFiledIndex = nTag >> 3;
            bSucRead = false;
            switch(nFiledIndex)
            {
            case 1:
                if(nTag == 10) // MakeTag(1, 2)
                {
                    bSucRead = ar.ProtobufRead(str_value, 1, PB_ZipType.PB_Zip_Varint);
                }
                break;
            default:
                break;
            }
            if(!bSucRead)
            {
                ar.ProtobufSkipField(nTag);
            }
        }
    }
};


public class Response
{
    //-----------------------------
    //-----------------------------
    [PBAttrib("Value = PB_Zip_Varint, Field = 1, Tag = 10")]
    public StringA  value;// = 1
    [export]
    public void  WriteTo(FCSerialize ar)
    {
        ar.ProtobufWrite(value, 1, PB_ZipType.PB_Zip_Varint);
    }
    [export]
    public void  ReadFrom(FCSerialize ar)
    {
        int nTag = 0;
        int nFiledIndex = 0;
        bool bSucRead = false;
        //while(nTag = ar.ProtobufReadTag()) // C#不支持这样的写法
        while((nTag = ar.ProtobufReadTag()) != 0)
        {
            nFiledIndex = nTag >> 3;
            bSucRead = false;
            switch(nFiledIndex)
            {
            case 1:
                if(nTag == 10) // MakeTag(1, 2)
                {
                    bSucRead = ar.ProtobufRead(value, 1, PB_ZipType.PB_Zip_Varint);
                }
                break;
            default:
                break;
            }
            if(!bSucRead)
            {
                ar.ProtobufSkipField(nTag);
            }
        }
    }
};
