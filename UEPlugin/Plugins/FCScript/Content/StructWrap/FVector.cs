
struct FVector
{
    public double X;
    public double Y;
    public double Z;
	
	public FVector(){}
	public FVector(double _X, double _Y, double _Z);
	public static FVector operator + (FVector a, FVector b);
	public static FVector operator - (FVector a, FVector b);
	public static FVector operator * (FVector a, FVector b);
	public static FVector operator / (FVector a, FVector b);
	
	public static FVector operator + (FVector a, double b);
	public static FVector operator - (FVector a, double b);
	public static FVector operator * (FVector a, double b);
	public static FVector operator / (FVector a, double b);
	
	public static FVector operator += (FVector a, FVector b);
	public static FVector operator -= (FVector a, FVector b);
	public static FVector operator *= (FVector a, FVector b);
	public static FVector operator /= (FVector a, FVector b);
	
	public static FVector operator += (FVector a, double b);
	public static FVector operator -= (FVector a, double b);
	public static FVector operator *= (FVector a, double b);
	public static FVector operator /= (FVector a, double b);
	
	public FVector operator -();
		
	public static FVector Zero{ get; }
	public static FVector One{ get; }
	
	public double this[int nIndex]{get, set};
	
	public static double operator | (FVector a, FVector b);   // DotProduct
	public static FVector operator ^ (FVector a, FVector b); // CrossProduct
	public static double operator DotProduct (FVector a, FVector b);   // DotProduct
	public static FVector operator CrossProduct (FVector a, FVector b); // CrossProduct
	
	public double Dot(FVector b);   // Dot
	public FVector CrossProduct(FVector b); // Cross
	
	public void Set(double _X, double _Y, double _Z);
	
	public FVector GetMax();
	public FVector GetAbsMax();
	public FVector GetMin();
	public FVector GetAbsMin();
	public FVector GetAbs();
	public double Size();
	public double Length();
	public double SizeSquared();
	public double SquaredLength();
	public double Size2D();
	public double SizeSquared2D();
	
	public bool IsNearlyZero(double Tolerance = UE_SMALL_NUMBER);
	public bool IsZero();
	public bool IsNormalized();
	public void Normalize(double Tolerance = UE_SMALL_NUMBER);
	
	public FRotator ToOrientationRotator();
	public FQuat ToOrientationQuat();
	public FRotator Rotation();
	public StringA ToString();
	public void InitFromString(StringA InitStr);
};