using UnityEngine;
using System.Collections;

public class ThirtyPlusButton : MonoBehaviour
{
    public UISprite BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.Find("Background").GetComponent<UISprite>();
    }

    void OnClick()
    {
        if (CardDataManager.GetInstance().currentMoneyFilter != 31)
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(31);
        }
        else
        {
            CardDataManager.GetInstance().ChangeFormatFilter(0);
        }
        Clicked.Invoke();
    }
}
