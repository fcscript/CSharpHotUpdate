
struct FVector2D
{
    public double X;
    public double Y;
	
	public FVector2D(){}
	public FVector2D(double _X, double _Y);
	
	public static FVector2D operator + (FVector2D a, FVector2D b);
	public static FVector2D operator - (FVector2D a, FVector2D b);
	public static FVector2D operator * (FVector2D a, FVector2D b);
	public static FVector2D operator / (FVector2D a, FVector2D b);
	
	public static FVector2D operator + (FVector2D a, double b);
	public static FVector2D operator - (FVector2D a, double b);
	public static FVector2D operator * (FVector2D a, double b);
	public static FVector2D operator / (FVector2D a, double b);
	
	public static FVector2D operator += (FVector2D a, FVector2D b);
	public static FVector2D operator -= (FVector2D a, FVector2D b);
	public static FVector2D operator *= (FVector2D a, FVector2D b);
	public static FVector2D operator /= (FVector2D a, FVector2D b);
	
	public static FVector2D operator += (FVector2D a, double b);
	public static FVector2D operator -= (FVector2D a, double b);
	public static FVector2D operator *= (FVector2D a, double b);
	public static FVector2D operator /= (FVector2D a, double b);
	
	public FVector2D operator -();
			
	public static FVector2D Zero{ get; }
	public static FVector2D One{ get; }
	public static FVector2D UnitX{ get; }
	public static FVector2D UnitY{ get; }
		
	public double this[int nIndex]{get, set};
	public void Set(double _X, double _Y);
	
	public static double operator | (FVector2D a, FVector2D b);   // DotProduct
	public static FVector2D operator ^ (FVector2D a, FVector2D b); // CrossProduct
	public static double operator DotProduct (FVector2D a, FVector2D b);   // DotProduct
	public static FVector2D operator CrossProduct (FVector2D a, FVector2D b); // CrossProduct
	
	public double Dot(FVector2D b);   // Dot
	public FVector2D CrossProduct(FVector2D b); // Cross
			
	public static double DistSquared(FVector2D a, FVector2D b);
	public static double Distance(FVector2D a, FVector2D b);
	public static FVector2D Max(FVector2D a, FVector2D b);
	public static FVector2D Min(FVector2D a, FVector2D b);
	
	public double GetMax();
	public double GetMin();
	public double GetAbsMax();
	public double GetAbsMin();
	
	public double Size();
	public double Length();	
	public double SizeSquared();
	
	public FVector2D GetRotated(double AngleDeg);
	public FVector2D GetSafeNormal(double Tolerance = UE_SMALL_NUMBER);
	public bool Normalize(double Tolerance = UE_SMALL_NUMBER);
	public bool IsNearlyZero(double Tolerance = UE_SMALL_NUMBER);
	public bool IsZero();
	
	public FVector2D GetAbs();
	public StringA ToString();
};