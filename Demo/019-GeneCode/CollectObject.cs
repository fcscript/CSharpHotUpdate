
class CollectObject : public BaseObject
{	
	int CollectNumb = 0;
	int MaxCollectNumb = 1;
	public CollectObject()
	{
	}
	
	public  StringA  GetTypeName()
	{
		return "CollectObject";
	}
	public override bool IsCollectObject()
	{
		return true;
	}
	public int GetCollecNumb()
	{
		return CollectNumb;
	}
	public void SetCollecNumb(int InNumb)
	{
		CollectNumb = InNumb;
	}
	public void SetMaxCollecNumb(int InMaxNumb)
	{
		MaxCollectNumb = InMaxNumb;
	}
	public bool IsCanCollect()
	{
		return CollectNumb < MaxCollectNumb;
	}
};
