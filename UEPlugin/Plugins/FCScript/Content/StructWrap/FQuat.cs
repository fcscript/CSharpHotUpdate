
struct FQuat
{
    double X;
    double Y;
    double Z;
    double W;
	
	public static FQuat operator + (FQuat a, FQuat b);
	public static FQuat operator - (FQuat a, FQuat b);
	public static FQuat operator * (FQuat a, FQuat b);
	public static FVector operator * (FQuat a, FVector b);
	public static FMatrix operator * (FQuat a, FMatrix b);
	
	public static FQuat operator += (FQuat a, FQuat b);
	public static FQuat operator -= (FQuat a, FQuat b);
	public static FQuat operator *= (FQuat a, FQuat b);
	
	public FQuat operator -();
	
	public static double operator | (FQuat a, FQuat b);   // DotProduct
	
	public FQuat MakeFromEuler(const FVector &V);
	public FVector Euler();
	public void Normalize(double Tolerance = UE_SMALL_NUMBER);
	public double GetNormalized(double Tolerance = UE_SMALL_NUMBER);
	public bool IsNormalized();
	public double Size();
	public double SizeSquared();
	public double GetAngle();
	public void ToAxisAndAngle(const FVector &V, double &Angle);
	public FVector ToRotationVector();
	public static FQuat MakeFromRotationVector(const FVector &V);
	public void ToSwingTwist(const FVector &InTwistAxis, FQuat &OutSwing, FQuat &OutTwist);
	public double GetTwistAngle(const FVector &V);
	
	public FVector RotateVector(const FVector &V);
	public FVector UnrotateVector(const FVector &V);
	public FQuat Log();
	public FQuat Exp();
	public FQuat Inverse();
	public FVector GetAxisX();
	public FVector GetAxisY();
	public FVector GetAxisZ();
	public FVector GetRightVector();
	public FVector GetUpVector();
	public FVector Vector();
	public FRotator Rotator();
	public FMatrix ToMatrix();	
	public void ToMatrix(FMatrix &Mat);
	
	public FVector GetRotationAxis();
	public double AngularDistance(const FQuat &B);
	public bool ContainsNaN();
	public StringA ToString();
	public void InitFromString(StringA InitStr);
	
	public static FQuat FindBetween(const FVector &A, const FVector &B);
	public static FQuat FindBetweenNormals(const FVector &A, const FVector &B);
	public static FQuat FindBetweenVectors(const FVector &A, const FVector &B);
	public static FQuat FastLerp(const FQuat &A, const FQuat &B);
	public static FQuat FastBilerp(const FQuat &A, const FQuat &B, const FQuat &C, const FQuat &D, double FracX, double FracY);
	public static FQuat Slerp_NotNormalized(const FQuat &A, const FQuat &B, double Slerp);
	public static FQuat SlerpFullPath_NotNormalized(const FQuat &A, const FQuat &B, double Slerp);
	public static FQuat SlerpFullPath(const FQuat &A, const FQuat &B, double Slerp);
	public static FQuat Squad(const FQuat &A, const FQuat &B, const FQuat &C, const FQuat &D, double Slerp);
	public static FQuat SquadFullPath(const FQuat &A, const FQuat &B, const FQuat &C, const FQuat &D, double Slerp);
	public static FQuat CalcTangents(const FQuat &A, const FQuat &B, const FQuat &C, double Slerp, const FQuat &D);
};