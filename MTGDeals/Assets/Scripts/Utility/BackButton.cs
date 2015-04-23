using System.Reflection;
using UnityEngine;
using System.Collections;
using System;

public class BackButton : UIButton
{
    public GameObject TargetToDestroy;

    public override void OnPress(bool isPressed)
    {
        ViewController.GetInstance().CreateView("FrontPage/FrontPage");
    }
}

