
export void  main()
{
	CrashDemo1();
	CrashDemo2();
	FillPersonMsg();
}

class Phone
{
	long  phonenumber;
	~Phone()
	{
		os.print("---Phone release------");
	}
};

class Person
{
	List<Phone>  contacts;
	~Person()
	{
		os.print("---person release------");
	}
};

void CarshFunc1(Person msg)
{
	List<Phone>  aa = new List<Phone>();  // ref = 1, weakRef = 1
	msg.contacts = aa;

	Phone Node = new Phone();  // ref = 1, weakRef = 0
	msg.contacts.push_back(Node); // Node ==> ref = 1, weakRef = 1
}

void  CrashDemo1()
{
	Person msg = new Person();
	CarshFunc1(msg);
}
Person glb_msg;

void  CrashDemo2()
{
	glb_msg = new Person();
	CarshFunc2();
	glb_msg = NULL;
}

void CarshFunc2()
{
	Person msg = glb_msg;
	List<Phone>  aa = new List<Phone>();  // ref = 1, weakRef = 1
	msg.contacts = aa;

	Phone Node = new Phone();  // ref = 1, weakRef = 0
	msg.contacts.push_back(Node); // Node ==> ref = 1, weakRef = 1
}

Person FillPersonMsg()
{
	Person msg = new Person();
	//List<Phone>  aa;
	//msg.contacts = aa = new List<Phone>();  // ref = 1, weakRef = 1
	List<Phone>  aa = new List<Phone>();  // ref = 1, weakRef = 1
	msg.contacts = aa;

	Phone Node = new Phone();  // ref = 1, weakRef = 0
	msg.contacts.push_back(Node); // Node ==> ref = 1, weakRef = 1
	
	return msg;
}
