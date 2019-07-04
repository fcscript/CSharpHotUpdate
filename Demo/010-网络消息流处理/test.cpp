
class MsgBase
{
	ushort   nMsgType;  // 消息的类型
};

class MsgC2S_Login : public MsgBase
{
	StringA    szUser;      // 账号
	StringA    szPassword;  // 
};

enum  MsgType
{
	MSG_LOGIN = 1,
};

// 以后扩展一个数据类型，直接提供byte指针，可以少一次内存拷贝, 直接由C#调用脚本接口
export void  OnRecvServerMsg(StringA szMsg)
{
	CSerialize   ar;
	ar.ReadMode(szMsg);  // 这里没有拷贝，只是引用, 所以CSerialize对象不可长久保存

	OnSerializeServerMsg(ar);
}

// 可以在C#中调用 fc_serialize_msg_call(fc_intptr pInsPtr, fc_pcstr pcsFuncName, fc_byte_ptr msgPtr, int nStart, int nLen)
// 如 fc_serialize_msg_call(0, OnSerializeServerMsg, msgPtr, 0, msgPtr.Length);
export void  OnSerializeServerMsg(CSerialize ar)
{
	ushort   nMsgType;
	ar.ReadWrite(nMsgType);
	ar.Seek(0);
	switch(nMsgType)
	{
	case MsgType.MSG_LOGIN:
		{
			MsgC2S_Login  msg = new MsgC2S_Login();
			ar.ReadWrite(msg);
			print("账号：{0}", msg.szUser);
			print("密码：{0}", msg.szPassword);
		}
		break;
	default:
		print("不能识别的消息ID({0})", nMsgType);
		break;
	}
}

export void  main()
{
	MsgC2S_Login   msg = new MsgC2S_Login();
	msg.nMsgType = 1;//MsgType::MSG_LOGIN;
	msg.szUser = "kuser85456";
	msg.szPassword = "123456";

	//print("账号：{0}, 密码：{1}", msg.szUser, msg.szPassword);

	CSerialize  ar;
	ar.OwnWriteMode(128);
	ar.ReadWrite(msg);

	StringA  szData;
	ar.CopyTo(szData);

	OnRecvServerMsg(szData);
}
