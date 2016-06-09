using UnityEngine;
using System.Collections;

public class FilterButton : MonoBehaviour 
{
    public UISprite BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.Find("Background").GetComponent<UISprite>();        
    }

    void OnClick()
    {
        Clicked.Invoke();
    }
}
