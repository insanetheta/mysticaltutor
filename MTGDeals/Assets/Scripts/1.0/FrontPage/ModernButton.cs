using UnityEngine;
using System.Collections;

public class ModernButton : MonoBehaviour
{
    public UIButton BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.GetComponent<UIButton>();
    }

    void OnClick()
    {
        if (CardDataManager.GetInstance().currentFormatFilter != CardDataManager.FormatFilters.Modern)
        {
            CardDataManager.GetInstance().ChangeFormatFilter(CardDataManager.FormatFilters.Modern);
        }
        else
        {
            CardDataManager.GetInstance().ChangeFormatFilter(CardDataManager.FormatFilters.None);
        }
        Clicked.Invoke();
    }
}
