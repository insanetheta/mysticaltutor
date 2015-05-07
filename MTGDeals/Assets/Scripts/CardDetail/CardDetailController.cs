using System.Linq;
using DealFinder.Network.Models;
using UnityEngine;
using System.Collections;
using System.IO;

public class CardDetailController : MonoBehaviour
{
    private UILabel Name;
    private UILabel HighMidLowPrices;
    private SpriteRenderer CardImage;
    private bool Ready = false;
	private BuyButton BuyButtonRef;

    void Start()
    {
        Name = transform.Find("Name").GetComponent<UILabel>();
        HighMidLowPrices = transform.Find("HighMidLowPrices").GetComponent<UILabel>();
        CardImage = transform.Find("Image").GetComponent<SpriteRenderer>();
		BuyButtonRef = transform.Find("BuyButton").GetComponent<BuyButton>();
        Ready = true;
    }

    public void coLoadCard(TcgCard theCard)
    {
        StartCoroutine(LoadCard(theCard));
    }

    private IEnumerator LoadCard(TcgCard theTcgCard)
    {
        while (!Ready)
        {
            yield return null;
        }
		//string setName = theTcgCard.CardSetName.Replace(" ", "-");
		string cardName = theTcgCard.Name.Replace(" ", "-");

		//Debug.Log("http://shop.tcgplayer.com/magic/" + setName + "/" + cardName);
		//BuyButtonRef.CardUrl = "http://shop.tcgplayer.com/magic/" + setName + "/" + cardName;
        Name.text = theTcgCard.Name;
        HighMidLowPrices.text = "$" + theTcgCard.HiPrice.ToString() + "\n" +
            "$" + theTcgCard.AvgPrice.ToString() + "\n" + 
            "$" + theTcgCard.LowPrice.ToString();

        Debug.Log(theTcgCard.MultiverseID);
    
        string url = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=" +
                     theTcgCard.MultiverseID +
                     "&type=card";

        WWW www = new WWW(url);
        yield return www;
        CardImage.sprite = Sprite.Create(www.texture as Texture2D, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(.5f, .5f));
    }
}
