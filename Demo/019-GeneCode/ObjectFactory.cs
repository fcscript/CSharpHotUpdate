
class ObjectFactory
{
	public BaseObject  CreateObject(int InInsID, int InObjType, int InExcelID)
	{
		switch(InObjType)
		{
			case ObjectType.Collection:
				return new CollectObject();
			case ObjectType.Npc:
				return new Npc();
			case ObjectType.Monster:
				return new Monster();
			default:
				break;
		}
		return null;
	}
};
