

public class Phone
{
    //-----------------------------
    public enum valCase
    {
        kval1 = 3,
        kval2 = 4,
        VAL_NOT_SET = 0,
    };
    //-----------------------------
    [PBAttrib("Value = PB_Zip_Varint, Field = 1, Tag = 10")]
    public StringA  name;// = 1
    [PBAttrib("Value = PB_Zip_Varint, Field = 2, Tag = 16")]
    public long  phonenumber = 1885898888;// = 2
    //-----------------------------
    int  _oneof_case_0; // val
    [PBAttrib("Value = PB_Zip_Varint, Field = 3, Tag = 24, Case = _oneof_case_0")]
    public int  val1;// = 3
    [PBAttrib("Value = PB_Zip_Varint, Field = 4, Tag = 32, Case = _oneof_case_0")]
    public int  val2;// = 4
    //-----------------------------
    [PBAttrib("Key = PB_Zip_Varint, Value = PB_Zip_Varint, Field = 5, Tag = 42")]
    public map<StringA,int>  map_test;// = 5
    public void set_val1(int _val1)
    {
        _oneof_case_0 = 3;
        val1 = _val1;
    }
    public void set_val2(int _val2)
    {
        _oneof_case_0 = 4;
        val2 = _val2;
    }
    public bool has_val1()
    {
        return 3 == _oneof_case_0;
    }
    public int get_val1()
    {
        return val1;
    }
    public bool has_val2()
    {
        return 4 == _oneof_case_0;
    }
    public int get_val2()
    {
        return val2;
    }
    [export]
    public void  WriteTo(FCSerialize ar)
    {
        ar.ProtobufWrite(name, 1, PB_ZipType.PB_Zip_Varint);
        ar.ProtobufWrite(phonenumber, 2, PB_ZipType.PB_Zip_Varint);
        switch(_oneof_case_0)
        {
        case 3:
            ar.ProtobufWrite(val1, 3, PB_ZipType.PB_Zip_Varint);
            break;
        case 4:
            ar.ProtobufWrite(val2, 4, PB_ZipType.PB_Zip_Varint);
            break;
         default:
            break;
        }
        ar.ProtobufWriteMap(map_test, 5, PB_ZipType.PB_Zip_Varint, PB_ZipType.PB_Zip_Varint);
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
                    bSucRead = ar.ProtobufRead(name, 1, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 2:
                if(nTag == 16) // MakeTag(2, 0)
                {
                    bSucRead = ar.ProtobufRead(phonenumber, 2, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 3:
                if(nTag == 24) // MakeTag(3, 0)
                {
                    _oneof_case_0 = 3; // val1 
                    bSucRead = ar.ProtobufRead(val1, 3, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 4:
                if(nTag == 32) // MakeTag(4, 0)
                {
                    _oneof_case_0 = 4; // val2 
                    bSucRead = ar.ProtobufRead(val2, 4, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 5:
                if(nTag == 42) // MakeTag(5, 2)
                {
                    bSucRead = ar.ProtobufReadMap(map_test, 5, PB_ZipType.PB_Zip_Varint, PB_ZipType.PB_Zip_Varint);
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


public class Person
{
    //-----------------------------
    //-----------------------------
    [PBAttrib("Value = PB_Zip_Varint, Field = 1, Tag = 10")]
    public StringA  name;// = 1
    [PBAttrib("Value = PB_Zip_Varint, Field = 2, Tag = 16")]
    public int  age = 1;// = 2
    [PBAttrib("Value = PB_Zip_Varint, Field = 3, Tag = 26")]
    public StringA  address;// = 3
    [PBAttrib("Value = PB_Zip_Varint, Field = 4, Tag = 34")]
    public List<Phone>  contacts;// = 4
    [export]
    public void  WriteTo(FCSerialize ar)
    {
        ar.ProtobufWrite(name, 1, PB_ZipType.PB_Zip_Varint);
        ar.ProtobufWrite(age, 2, PB_ZipType.PB_Zip_Varint);
        ar.ProtobufWrite(address, 3, PB_ZipType.PB_Zip_Varint);
        ar.ProtobufWrite(contacts, 4, PB_ZipType.PB_Zip_Varint);
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
                    bSucRead = ar.ProtobufRead(name, 1, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 2:
                if(nTag == 16) // MakeTag(2, 0)
                {
                    bSucRead = ar.ProtobufRead(age, 2, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 3:
                if(nTag == 26) // MakeTag(3, 2)
                {
                    bSucRead = ar.ProtobufRead(address, 3, PB_ZipType.PB_Zip_Varint);
                }
                break;
            case 4:
                if(nTag == 34) // MakeTag(4, 2)
                {
                    bSucRead = ar.ProtobufRead(contacts, 4, PB_ZipType.PB_Zip_Varint);
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
