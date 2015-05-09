using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class EventButton : UIButton
{
    public delegate void ButtonPressedHandler();
    public ButtonPressedHandler ButtonPressed = delegate { };

    public override void OnPress(bool isPressed)
    {
        base.OnPress(isPressed);
        ButtonPressed();
    }
}

