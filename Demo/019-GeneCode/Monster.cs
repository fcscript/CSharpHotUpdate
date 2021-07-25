
class Monster : public DynamicObject
{
	public StringA  GetTypeName()
	{
		return "Monster";
	}
	public override int GetCampType()
	{
		return CampType.Enemy;
	}
};
