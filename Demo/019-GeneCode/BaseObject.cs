

class BaseObject
{
	int  ExcelId;
	int  Id;
	int  Type;
	Vector3  Pos;
	public BaseObject()
	{
	}
	
	public void InitByExcelId(int InExcelId)
	{
		ExcelId = InExcelId;
	}
	public void SetId(int InId)
	{
		Id = InId;
	}
	public int GetId()
	{
		return Id;
	}
	public void SetType(int InType)
	{
		Type = InType;
	}
	public int GetType()
	{
		return Type;
	}
	public virtual StringA  GetTypeName()
	{
		return "BaseObject";
	}
	public virtual int GetCampType()
	{
		return CampType.Friend;
	}
	public int GetExcelId()
	{
		return ExcelId;
	}
	public virtual bool IsCollectObject()
	{
		return false;
	}	
	public Vector3 GetPos()
	{
		return Pos;
	}
	public void SetPos(Vector3 InPos)
	{
		Pos = InPos;
	}
};

