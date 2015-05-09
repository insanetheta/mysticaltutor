using System;
using System.Globalization;
using System.Linq;
using DealFinder.Network.Models;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrontPageController : MonoBehaviour
{
    private Transform GridRef;
    private UIDraggablePanel ScrollingTextPanel;
    public List<CardObject> CardHistory = new List<CardObject>();

    private StandardButton StandardFilterButton;
    private ModernButton ModernFilterButton;
    private LegacyButton LegacyFilterButton;
    private TenButton TenFilterButton;
    private ThirtyButton ThirtyFilterButton;
    private ThirtyPlusButton ThirtyPlusFilterButton;

    void Start()
    {
        GridRef = transform.FindChild("ScrollingText/Grid");
        ScrollingTextPanel = transform.Find("ScrollingText").GetComponent<UIDraggablePanel>();
        OnFormatClicked = FormatButtonsUpdate;
        OnMoneyClicked = MoneyButtonsUpdate;
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(CreateList());

        StandardFilterButton = transform.Find("FrontPageButtons/StandardFilter").GetComponent<StandardButton>();
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
    }

    public delegate void ButtonClickAction();
    public static event ButtonClickAction OnFormatClicked;
    public static event ButtonClickAction OnMoneyClicked;
    
    void FormatButtonsUpdate()
    {
        Debug.Log("BLEF");
        StandardFilterButton.BackgroundColor.defaultColor = Color.white;
        ModernFilterButton.BackgroundColor.defaultColor = Color.white;
        LegacyFilterButton.BackgroundColor.defaultColor = Color.white;

        if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Standard)
        {
            StandardFilterButton.BackgroundColor.defaultColor = Color.black;
        }
        else if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Modern)
        {
            ModernFilterButton.BackgroundColor.defaultColor = Color.black;
        }
        else if (CardDataManager.GetInstance().currentFormatFilter == CardDataManager.FormatFilters.Legacy)        
        {
            LegacyFilterButton.BackgroundColor.defaultColor = Color.black;
        }
        StandardFilterButton.BackgroundColor.UpdateColor(true, true);
        ModernFilterButton.BackgroundColor.UpdateColor(true, true);
        LegacyFilterButton.BackgroundColor.UpdateColor(true, true);

        StartCoroutine(CreateList());
    }

    void MoneyButtonsUpdate()
    {
        Debug.Log("BLORF");
        TenFilterButton.BackgroundColor.defaultColor = Color.white;
        ThirtyFilterButton.BackgroundColor.defaultColor = Color.white;
        ThirtyPlusFilterButton.BackgroundColor.defaultColor = Color.white;

        if (CardDataManager.GetInstance().currentMoneyFilter == 10)
        {
            TenFilterButton.BackgroundColor.defaultColor = Color.black;
        }
        else if (CardDataManager.GetInstance().currentMoneyFilter == 30)
        {
            ThirtyFilterButton.BackgroundColor.defaultColor = Color.black;
        }
        else if (CardDataManager.GetInstance().currentMoneyFilter >= 30)        
        {
            ThirtyPlusFilterButton.BackgroundColor.defaultColor = Color.black;
        }
        TenFilterButton.BackgroundColor.UpdateColor(true, true);
        ThirtyFilterButton.BackgroundColor.UpdateColor(true, true);
        ThirtyPlusFilterButton.BackgroundColor.UpdateColor(true, true);

        StartCoroutine(CreateList());
    }

    IEnumerator CreateList()
    {
        foreach (Transform go in GridRef)
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
        //ScrollingTextPanel.ResetPosition();
        GridRef.GetComponent<UIGrid>().repositionNow = true;
    }

    public void InstantiateNewCard(TcgCard newCard, int sortOrder)
    {
        GameObject newGO = Instantiate(Resources.Load<GameObject>("FrontPage/Item")) as GameObject;

        CardObject tmp = new CardObject(newCard, newGO);

        CardHistory.Add(tmp);

        newGO.name = sortOrder.ToString();
        newGO.transform.parent = GridRef;
		newGO.GetComponent<FrontPageButton>().TheCardRef = newCard;
        newGO.transform.Find("Name").GetComponent<UILabel>().text = newCard.Name;
        newGO.transform.Find("Mid").GetComponent<UILabel>().text = "Mid: " + string.Format("{0:C}", newCard.AvgPrice);
        newGO.transform.Find("Low").GetComponent<UILabel>().text = "Low: " + string.Format("{0:C}", newCard.LowPrice);
        newGO.transform.Find("Ratio").GetComponent<UILabel>().text = "+ " + string.Format("{0:C}", newCard.AvgPrice - newCard.LowPrice);
        newGO.transform.localScale = new Vector3(1, 1, 1);
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