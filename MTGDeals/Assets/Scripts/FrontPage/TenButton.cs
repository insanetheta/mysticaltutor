using UnityEngine;
using System.Collections;

public class TenButton : MonoBehaviour
{
    public UISprite BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.Find("Background").GetComponent<UISprite>();
    }

    void OnClick()
    {
        if (CardDataManager.GetInstance().currentMoneyFilter != 10)
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(10);
        }
        else
        {
            CardDataManager.GetInstance().ChangeFormatFilter(0);
        }
        Clicked.Invoke();
    }
}
