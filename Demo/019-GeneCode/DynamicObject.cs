
class DynamicObject : public BaseObject
{
	StringA GetTypeName()
	{
		return "DynamicObject";
	}
	public bool IsDynamicObjectect()
	{
		return true;
	}
	public override bool GetCampType()
	{
		return CampType.Friend;
	}
};
