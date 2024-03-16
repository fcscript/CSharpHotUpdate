
struct FLinearColor
{
	float R, G, B, A;
	
	public FLinearColor(float _R, float _G, float _B, float _A);
	
	public static FLinearColor operator + (FLinearColor a, FLinearColor b);
	public static FLinearColor operator - (FLinearColor a, FLinearColor b);
	public static FLinearColor operator * (FLinearColor a, FLinearColor b);
	public static FLinearColor operator * (FLinearColor a, float b);
	public static FLinearColor operator / (FLinearColor a, FLinearColor b);
	public static FLinearColor operator / (FLinearColor a, float b);
	
	public FLinearColor operator += (FLinearColor b);
	public FLinearColor operator -= (FLinearColor b);
	public FLinearColor operator *= (FLinearColor b);
	public FLinearColor operator *= (float b);
	public FLinearColor operator /= (FLinearColor b);
	public FLinearColor operator /= (float b);
	
	public FColor ToRGBE();
	public static FLinearColor FromSRGBColor(const FColor &Color);
	public static FLinearColor FromPow22Color(const FColor &Color);
	
	public FLinearColor GetClamped(float InMin = 0, float InMax = 1);
	public static FLinearColor MakeFromHSV8(byte H, byte S, byte V);
	public FLinearColor HSVToLinearRGB();
	public FLinearColor LerpUsingHSV(const FLinearColor &B, const FLinearColor &C, float Slerp);
	public FColor ToFColorSRGB();
	public FColor ToFColor();
	public StringA ToString();
	public void InitFromString(StringA InitStr);
};