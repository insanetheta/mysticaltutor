using System;
using System.Globalization;
using System.Linq;
using DealFinder.Network.Models;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FrontPageController : MonoBehaviour
{
    public List<CardObject> CardHistory = new List<CardObject>();
    private RectTransform ScrollingText;

    private GameObject StandardFilterButton;
    private GameObject ModernFilterButton;
    private GameObject LegacyFilterButton;
    private GameObject TenFilterButton;
    private GameObject ThirtyFilterButton;
    private GameObject ThirtyPlusFilterButton;

    void Start()
    {
        ScrollingText = gameObject.GetComponent<RectTransform>().Find("ScrollingText").GetComponent<RectTransform>();
        Debug.Log(ScrollingText.name);
        OnFormatClicked = FormatButtonsUpdate;
        OnMoneyClicked = MoneyButtonsUpdate;
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(CreateList());

        ModernFilterButton = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/ModernFilter")) as GameObject;
        ModernFilterButton.transform.SetParent(this.GetComponent<RectTransform>().parent, false);
        ModernFilterButton.GetComponent<Button>().onClick.AddListener(() => { OnFormatClicked(CardDataManager.FormatFilters.Modern); });

        StandardFilterButton = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/StandardFilter")) as GameObject;
        StandardFilterButton.transform.SetParent(this.GetComponent<RectTransform>().parent, false);
        StandardFilterButton.GetComponent<Button>().onClick.AddListener(() => { OnFormatClicked(CardDataManager.FormatFilters.Standard); });

        LegacyFilterButton = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/LegacyFilter")) as GameObject;
        LegacyFilterButton.transform.SetParent(this.GetComponent<RectTransform>().parent, false);
        LegacyFilterButton.GetComponent<Button>().onClick.AddListener(() => { OnFormatClicked(CardDataManager.FormatFilters.Legacy); });

        TenFilterButton = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/10")) as GameObject;
        TenFilterButton.transform.SetParent(this.GetComponent<RectTransform>().parent, false);
        TenFilterButton.GetComponent<Button>().onClick.AddListener(() => { OnMoneyClicked(10); });

        ThirtyFilterButton = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/30")) as GameObject;
        ThirtyFilterButton.transform.SetParent(this.GetComponent<RectTransform>().parent, false);
        ThirtyFilterButton.GetComponent<Button>().onClick.AddListener(() => { OnMoneyClicked(30); });

        if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Standard)
        {
            StandardFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }
        else if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Modern)
        {
            ModernFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }
        else if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Legacy)
        {
            LegacyFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }

        if (CardDataManager.GetInstance().currentMoneyFilter == 10)
        {
            TenFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }
        else if (CardDataManager.GetInstance().currentMoneyFilter == 30)
        {
            ThirtyFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }
        else if (CardDataManager.GetInstance().currentMoneyFilter >= 30)
        {
            ThirtyPlusFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }


        /*StandardFilterButton = transform.Find("FrontPageButtons/StandardFilter").GetComponent<StandardButton>();
        StandardFilterButton.Clicked += OnFormatClicked;

        ModernFilterButton = transform.Find("FrontPageButtons/ModernFilter").GetComponent<ModernButton>();
        ModernFilterButton.Clicked += OnFormatClicked;

        LegacyFilterButton = transform.Find("FrontPageButtons/LegacyFilter").GetComponent<LegacyButton>();
        LegacyFilterButton.Clicked += OnFormatClicked;

        TenFilterButton = transform.Find("FrontPageButtons/0-10").GetComponent<TenButton>();
        TenFilterButton.Clicked += OnMoneyClicked;

        ThirtyFilterButton = transform.Find("FrontPageButtons/10-30").GetComponent<ThirtyButton>();
        ThirtyFilterButton.Clicked += OnMoneyClicked;

        ThirtyPlusFilterButton = transform.Find("FrontPageButtons/30+").GetComponent<ThirtyPlusButton>();
        ThirtyPlusFilterButton.Clicked += OnMoneyClicked;

        FormatButtonsUpdate();
        MoneyButtonsUpdate();
         */
         
    }

    public delegate void FormatClickAction(CardDataManager.FormatFilters Filter);
    public static event FormatClickAction OnFormatClicked;

    public delegate void MoneyClickAction(int Filter);
    public static event MoneyClickAction OnMoneyClicked;

    private static Color buttonSelectedColor = new Color(68f / 255f, 255f / 255f, 53f / 255f);
    
    void FormatButtonsUpdate(CardDataManager.FormatFilters Filter)
    {
        StandardFilterButton.GetComponent<Image>().color = Color.white;
        ModernFilterButton.GetComponent<Image>().color = Color.white;
        LegacyFilterButton.GetComponent<Image>().color = Color.white;

        if (CardDataManager.GetInstance().currentFormatFilter != Filter)
        {
            CardDataManager.GetInstance().ChangeFormatFilter(Filter);
        }
        else
        {
            CardDataManager.GetInstance().ChangeFormatFilter(CardDataManager.FormatFilters.None);            
        }

        if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Standard)
        {
            StandardFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }
        else if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Modern)
        {
            ModernFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }
        else if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Legacy)        
        {
            LegacyFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }

        StartCoroutine(CreateList());
    }

    void MoneyButtonsUpdate(int Filter)
    {
        ThirtyFilterButton.GetComponent<Image>().color = Color.white;
        TenFilterButton.GetComponent<Image>().color = Color.white;

        Debug.Log(Filter);

        if (CardDataManager.GetInstance().currentMoneyFilter != Filter)
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(Filter);
        }
        else
        {
            CardDataManager.GetInstance().ChangeMoneyFilter(0);
        }

        if (CardDataManager.GetInstance().currentMoneyFilter == 10)
        {
            TenFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }
        else if (CardDataManager.GetInstance().currentMoneyFilter == 30)
        {
            ThirtyFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }
        else if (CardDataManager.GetInstance().currentMoneyFilter >= 30)        
        {
            ThirtyPlusFilterButton.GetComponent<Image>().color = buttonSelectedColor;
        }

        StartCoroutine(CreateList());
    }

    IEnumerator CreateList()
    {
        foreach (RectTransform go in ScrollingText)
        {
            Destroy(go.gameObject);
        }

        yield return new WaitForSeconds(.01f);

        CardHistory = new List<CardObject>();
        List<TcgCard> filteredCards = CardDataManager.GetInstance().FilteredCards();

        if (PlayerPrefs.GetInt("SortBy", 0) == 0)
        {
            SortByDollarRatio(filteredCards);
        }
        else
        {
            SortByPercentageRatio(filteredCards);
        }

        for (int i = 0; i < filteredCards.Count; i++)
        {
            InstantiateNewCard(filteredCards[i], i);
        }
    }

    private static Color baseItemColor = new Color(208f / 255f, 208f / 255f, 208f / 255f);
    private static Color variantItemColor = new Color(171f / 255f, 171f / 255f, 171f / 255f);

    public void InstantiateNewCard(TcgCard newCard, int sortOrder)
    {
        GameObject newGO = Instantiate(Resources.Load<GameObject>("2.0/Item")) as GameObject;

        CardObject tmp = new CardObject(newCard, newGO);

        CardHistory.Add(tmp);
        newGO.name = sortOrder.ToString();
        newGO.transform.SetParent(ScrollingText);
		//newGO.GetComponent<FrontPageButton>().TheCardRef = newCard;
        newGO.transform.Find("Name").GetComponent<Text>().text = newCard.Name;
        newGO.transform.Find("Mid").GetComponent<Text>().text = "Mid: " + string.Format("{0:C}", newCard.AvgPrice);
        newGO.transform.Find("Low").GetComponent<Text>().text = "Low: " + string.Format("{0:C}", newCard.LowPrice);
        newGO.transform.Find("Ratio").GetComponent<Text>().text = "+ " + string.Format("{0:C}", newCard.AvgPrice - newCard.LowPrice);
        //newGO.transform.Find("Shadow").GetComponent<Text>().text = "+ " + string.Format("{0:C}", newCard.AvgPrice - newCard.LowPrice);
        newGO.GetComponent<Image>().color = sortOrder % 2 == 1 ? baseItemColor : variantItemColor;
    }

    /// <summary>
    /// Sort algorithms
    /// </summary>
    void SortByDollarRatio(List<TcgCard> cards)
    {
        cards.Sort((TcgCard node1, TcgCard node2) => (node2.AvgPrice - node2.LowPrice)
            .CompareTo(node1.AvgPrice - node1.LowPrice));
    }

    void SortByPercentageRatio(List<TcgCard> cards)
    {
        cards.Sort((TcgCard node1, TcgCard node2) => (node2.LowPrice / node2.AvgPrice)
            .CompareTo(node1.LowPrice / node1.AvgPrice));
    }
}

public class CardObject
{
    public int Position = 0;
    public TcgCard TheCard;
    public GameObject GO;

    public CardObject(TcgCard aCard, GameObject GORef)
    {
        TheCard = aCard;
        GO = GORef;
    }
}