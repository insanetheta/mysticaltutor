using System.Linq;
using DealFinder.Network.Models;
using UnityEngine;
using System.Collections;

public class CardDetailController : MonoBehaviour
{
    private UILabel Name;
    private UILabel HighMidLowPrices;


    void Start()
    {
        Name = transform.Find("Name").GetComponent<UILabel>();
        HighMidLowPrices = transform.Find("HighMidLowPrices").GetComponent<UILabel>();
        
        LoadCard(CardDataManager.GetInstance().Cards.First());
    }

    public void LoadCard(TcgCard theTcgCard)
    {
        Name.text = theTcgCard.Name;
        HighMidLowPrices.text = "$" + theTcgCard.HiPrice.ToString() + "\n" +
            "$" + theTcgCard.AvgPrice.ToString() + "\n" + 
            "$" + theTcgCard.LowPrice.ToString();
    }
}
