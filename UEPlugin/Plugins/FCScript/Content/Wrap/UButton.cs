using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class UButton : UObject
{    
	/** Called when the button is clicked */
	public MulticastDelegateEvent OnClicked;

	/** Called when the button is pressed */
	public MulticastDelegateEvent OnPressed;

	/** Called when the button is released */
	public MulticastDelegateEvent OnReleased;

	public MulticastDelegateEvent OnHovered;
	public MulticastDelegateEvent OnUnhovered;
}