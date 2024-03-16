
struct FVector4
{
    public double X;
    public double Y;
    public double Z;
    public double W;
	
	public FVector4(){}
	public FVector4(double _X, double _Y, double _Z, double _W);
	
	public static FVector4 operator + (FVector4 a, FVector4 b);
	public static FVector4 operator - (FVector4 a, FVector4 b);
	public static FVector4 operator * (FVector4 a, FVector4 b);
	public static FVector4 operator / (FVector4 a, FVector4 b);
	
	public static FVector4 operator + (FVector4 a, double b);
	public static FVector4 operator - (FVector4 a, double b);
	public static FVector4 operator * (FVector4 a, double b);
	public static FVector4 operator / (FVector4 a, double b);
	
	public static FVector4 operator += (FVector4 a, FVector4 b);
	public static FVector4 operator -= (FVector4 a, FVector4 b);
	public static FVector4 operator *= (FVector4 a, FVector4 b);
	public static FVector4 operator /= (FVector4 a, FVector4 b);
	
	public static FVector4 operator += (FVector4 a, double b);
	public static FVector4 operator -= (FVector4 a, double b);
	public static FVector4 operator *= (FVector4 a, double b);
	public static FVector4 operator /= (FVector4 a, double b);
	
	public FVector operator -();
	
	public static FVector4 Zero{ get; }
	public static FVector4 One{ get; }
	
	public double this[int nIndex]{get, set};
	
	public static double operator | (FVector4 a, FVector4 b);   // DotProduct
	public static FVector4 operator ^ (FVector4 a, FVector4 b); // CrossProduct
	public static double operator DotProduct (FVector4 a, FVector4 b);   // DotProduct
	public static FVector4 operator CrossProduct (FVector4 a, FVector4 b); // CrossProduct
	
	public double Dot(FVector4 b);   // Dot
	public FVector4 CrossProduct(FVector4 b); // Cross
		
	public void Set(double _X, double _Y, double _Z, double _W);
	
	public FVector GetSafeNormal(double Tolerance = UE_SMALL_NUMBER);
	public StringA ToString();
};