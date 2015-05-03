using UnityEngine;
using System.Collections;

public class LegacyButton : MonoBehaviour
{
    public UISprite BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.Find("Background").GetComponent<UISprite>();
    }

    void OnClick()
    {
        if (CardDataManager.GetInstance().currentFormatFilter != CardDataManager.FormatFilters.Legacy)
        {
            CardDataManager.GetInstance().ChangeFormatFilter(CardDataManager.FormatFilters.Legacy);
        }
        else
        {
            CardDataManager.GetInstance().ChangeFormatFilter(CardDataManager.FormatFilters.None);
        }
        Clicked.Invoke();
    }
}
