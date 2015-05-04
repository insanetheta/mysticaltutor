using UnityEngine;
using System.Collections;

public class StandardButton : MonoBehaviour
{
    public UIButton BackgroundColor;
    public event FrontPageController.ButtonClickAction Clicked;

    void Start()
    {
        BackgroundColor = transform.GetComponent<UIButton>();
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
