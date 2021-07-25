
class Npc : public DynamicObject
{
	public StringA  GetTypeName()
	{
		return "Npc";
	}
	public override int GetCampType()
	{
		return CampType.Friend;
	}
};
