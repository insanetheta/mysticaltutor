using UnityEngine;
using System.Collections;

public class TenButton : MonoBehaviour
{
    public UIButton BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.GetComponent<UIButton>();
    }

    void OnClick()
    {
        if (CardDataManager.GetInstance().currentMoneyFilter != 10)
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(10);
        }
        else
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(0);
        }
        Clicked.Invoke();
    }
}
