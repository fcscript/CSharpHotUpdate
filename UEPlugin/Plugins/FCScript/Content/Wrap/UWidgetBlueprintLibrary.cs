using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TSubclassOf<_Ty>
{
	
};

public class UWidgetBlueprintLibrary
{	
	//public static UUserWidget Create(UObject WorldContextObject, TSubclassOf<class UUserWidget> WidgetType, APlayerController* OwningPlayer){ return null; }
	public static UUserWidget Create(UObject WorldContextObject, UClass WidgetType, APlayerController OwningPlayer){ return null; }

	public static FEventReply Handled(){return null;}
	public static FEventReply Unhandled(){return null;}
};