using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


class  Button : Selectable
{
    public ButtonClickedEvent onClick { get;  set; }
    public void OnPointerClick(PointerEventData eventData){}
    public void OnSubmit(BaseEventData eventData){}
};

