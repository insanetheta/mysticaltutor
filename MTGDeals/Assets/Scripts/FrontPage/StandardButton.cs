using UnityEngine;
using System.Collections;

public class StandardButton : MonoBehaviour
{
    public UISprite BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.Find("Background").GetComponent<UISprite>();
    }

    void OnClick()
    {
        if (CardDataManager.GetInstance().currentFormatFilter != CardDataManager.FormatFilters.Standard)
        {
            CardDataManager.GetInstance().ChangeFormatFilter(CardDataManager.FormatFilters.Standard);            
        }
        else
        {
            CardDataManager.GetInstance().ChangeFormatFilter(CardDataManager.FormatFilters.None);            
        }
        Clicked.Invoke();
    }
}
