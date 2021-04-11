
class BaseObject
{
	int  ID;
	virtual int  GetType() 
	{
		return 0;
	}
	int  GetID() const
	{
		return ID;
	}
};

class Npc : public BaseObject
{
	int  GetType()
	{
		return 10;
	}
	void  PrintDesc()
	{
		ID = 101;
		int nType = BaseObject::GetType();
		int nThisType = GetType();
		int nCurType = Npc::GetType();
		int nCurID = this->ID;// BaseObject::ID;
		print("base type:{0}, this type:{1}, nCurType = {2}, ID={3}", nType, nThisType, nCurType, nCurID);
	}
};

class Monster : Npc
{
	int  GetType() 
	{
		return 20;
	}
};

export void  main()
{
	Monster  m = new Monster();
	Npc  nb = (Npc)m;
	BaseObject p = (BaseObject)nb;
	int  nType = p.GetType();
	print("nType = {0}, nb.type={1}", nType, nb.GetType());
	m.PrintDesc();
}
