
struct FColor
{
	byte B, G, R, A;
	
	public FColor(byte _R, byte _G, byte _B, byte _A);
	public FColor(uint32 C);
	
	public FColor operator += (FColor b);
	
	public FLinearColor FromRGBE();
	public void FromHex(StringA Hex);
	public FLinearColor FromRGBE();
	
	public uint ToPackedARGB();
	public uint ToPackedABGR();
	public uint ToPackedRGBA();
	public uint ToPackedBGRA();
	
	public FColor White{ get ; }
	public FColor Black{ get ; }
	public FColor Red{ get ; }
	public FColor Green{ get ; }
	public FColor Blue{ get ; }
	public FColor Yellow{ get ; }
	
	public StringA ToHex();
	public StringA ToString();
	public void InitFromString(StringA InitStr);
};