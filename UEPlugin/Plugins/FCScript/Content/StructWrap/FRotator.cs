
struct FRotator
{
	double Pitch;
	double Yaw;
	double Roll;
	
	public static FRotator operator + (FRotator a, FRotator b);
	public static FRotator operator - (FRotator a, FRotator b);
	
	public bool IsNearlyZero(double Tolerance);
	public bool IsZero();
	public void Add(double DeltaPitch, double DeltaYaw, double DeltaRoll);
	public FRotator GetInverse();
	public FRotator GridSnap(const FRotator &RotGrid);
	public FVector Vector();
	public FQuat Quaternion();
	public FVector RotateVector(const FVector &V);
	public FVector UnrotateVector(const FVector &V);
	public FRotator Clamp();
	public FRotator GetNormalized();
	public FRotator GetDenormalized();
	public void Normalize();
	
	public StringA ToString();
	public void InitFromString(StringA);
    public static FRotator MakeFromEuler(const FVector &V);
};