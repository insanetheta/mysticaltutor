using UnityEngine;
using System.Collections;

public class ThirtyButton : MonoBehaviour
{
    public UISprite BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.Find("Background").GetComponent<UISprite>();
    }

    void OnClick()
    {
        if (CardDataManager.GetInstance().currentMoneyFilter != 30)
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(30);
        }
        else
        {
            CardDataManager.GetInstance().ChangeFormatFilter(0);
        }
        Clicked.Invoke();
    }
}
