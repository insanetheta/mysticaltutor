using UnityEngine;
using System.Collections;

public class ThirtyPlusButton : MonoBehaviour
{
    public UIButton BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.GetComponent<UIButton>();
    }

    void OnClick()
    {
        if (CardDataManager.GetInstance().currentMoneyFilter <= 30)
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(31);
        }
        else
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(0);
        }
        Clicked.Invoke();
    }
}
