
struct FMatrix
{
	double    _11,_12, _13, _14;
	double    _21,_22, _23, _24;
	double    _31,_32, _33, _34;
	double    _41,_42, _43, _44;
	
	public FMatrix(const FPlane &X, const FPlane &Y, const FPlane &Z, const FPlane &W);
	public FMatrix(const FVector &X, const FVector &Y, const FVector &Z, const FVector &W);
	public void SetIdentity();
	
	public static FMatrix operator + (FMatrix a, FMatrix b);
	public static FMatrix operator * (FMatrix a, FMatrix b);
	public static FMatrix operator * (FMatrix a, double b);
	
	public FMatrix operator += (FMatrix a, FMatrix b);
	public FMatrix operator *= (FMatrix a, FMatrix b);
	public FMatrix operator *= (FMatrix a, double b);
	
	public FVector4 TransformFVector4(const FVector4 &V);
	public FVector4 TransformPosition(const FVector &V);
	public FVector InverseTransformPosition(const FVector &V);
	public FVector TransformVector(const FVector &V);
	public FVector InverseTransformVector(const FVector &V);
	
	public FMatrix GetTransposed();
	public double Determinant();
	public double RotDeterminant();
	public FMatrix InverseFast();
	public FMatrix Inverse();
	public FMatrix TransposeAdjoint();
	public void RemoveScaling(double Tolerance = UE_SMALL_NUMBER);
	public FMatrix GetMatrixWithoutScale(double Tolerance = UE_SMALL_NUMBER);
	public FMatrix ExtractScaling(double Tolerance = UE_SMALL_NUMBER);
	public FMatrix GetScaleVector(double Tolerance = UE_SMALL_NUMBER);
	
	public FMatrix RemoveTranslation();
	public FMatrix ConcatTranslation(const FVector &V);
	public bool ContainsNaN();
	public void ScaleTranslation(const FVector &V);
	public double GetMinimumAxisScale();
	public double GetMaximumAxisScale();
	
	public FMatrix ApplyScale(double Scale);
	public FVector GetOrigin();
	public FVector GetColumn(float Col);
	public void SetColumn(int Col, const FVector &V);
	
	public FRotator Rotator();
	public FQuat ToQuat();
	
	public bool GetFrustumNearPlane(FPlane &OutPlane);
	public bool GetFrustumFarPlane(FPlane &OutPlane);
	public bool GetFrustumLeftPlane(FPlane &OutPlane);
	public bool GetFrustumRightPlane(FPlane &OutPlane);
	public bool GetFrustumTopPlane(FPlane &OutPlane);
	public bool GetFrustumBottomPlane(FPlane &OutPlane);
	
	public StringA ToString();
	public uint ComputeHash();	
};