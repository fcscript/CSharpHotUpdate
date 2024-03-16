
struct FPlane
{
	double X;
	double Y;
	double Z;
	double W;
	
	public FPlane(const FVector4 &V);
	public FPlane(double _X, double _Y, double _Z, double _W);	
	public FPlane(const FVector &V, double _W);
	public FPlane(const FVector &A, const FVector &B, const FVector &C);
	
	public static FPlane operator + (FPlane a, FPlane b);
	public static FPlane operator - (FPlane a, FPlane b);
	public static FPlane operator * (FPlane a, FPlane b);
	public static FPlane operator * (FPlane a, double b);
	public static FPlane operator / (FPlane a, double b);
	
	public FPlane operator += (const FPlane &b);
	public FPlane operator -= (const FPlane &b);
	public FPlane operator *= (const FPlane &b);
	public FPlane operator /= (double b);
	
	public static double operator | (FPlane a, FPlane b);   // DotProduct
	
	public bool IsValid();
	public FVector GetOrigin();
	public FVector GetNormal();
	public double PlaneDot(const FVector &V);
	public bool Normalize(double Tolerance = UE_SMALL_NUMBER);
	
	public FPlane Flip();
	public FPlane TransformBy(const FMatrix &M);
	public FPlane TransformByUsingAdjointT(const FMatrix &B, double Slerp, const FMatrix &C);
	public FPlane TranslateBy(const FVector &V);
};