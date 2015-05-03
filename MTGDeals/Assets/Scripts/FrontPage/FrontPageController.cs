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
    public List<CardObject> CardHistory = new List<CardObject>();

    private Transform StandardFilterButton,
        ModernFilterButton,
        LegacyFilterButton,
        TenButton,
        ThirtyButton,
        ThirtyPlusButton;

    void Start()
    {
        GridRef = transform.FindChild("Grid");
        
        foreach (TcgCard card in CardDataManager.GetInstance().FilteredCards())
        {
            InstantiateNewCard(card);
        }

        if (PlayerPrefs.GetInt("SortBy", 0) == 0)
        {
            SortByDollarRatio();
        }
        else
        {
            SortByPercentageRatio();
        }

        CreateList();
        OnFormatClicked += FormatButtonsUpdate;
        OnMoneyClicked += MoneyButtonsUpdate;

        StandardFilterButton = transform.Find("FrontPageButtons/StandardFilter");
        StandardFilterButton.GetComponent<StandardButton>().Clicked += OnFormatClicked;

        ModernFilterButton = transform.Find("FrontPageButtons/ModernFilter");
        ModernFilterButton.GetComponent<FilterButton>().Clicked += OnFormatClicked;

        LegacyFilterButton = transform.Find("FrontPageButtons/LegacyFilter");
        LegacyFilterButton.GetComponent<FilterButton>().Clicked += OnFormatClicked;

        TenButton = transform.Find("FrontPageButtons/0-10");
        TenButton.GetComponent<FilterButton>().Clicked += OnMoneyClicked;

        ThirtyButton = transform.Find("FrontPageButtons/10-30");
        ThirtyButton.GetComponent<FilterButton>().Clicked += OnMoneyClicked;

        ThirtyPlusButton = transform.Find("FrontPageButtons/30+");
        ThirtyPlusButton.GetComponent<FilterButton>().Clicked += OnMoneyClicked;
    }

    public delegate void ButtonClickAction();
    public static event ButtonClickAction OnFormatClicked;
    public static event ButtonClickAction OnMoneyClicked;
    
    void FormatButtonsUpdate()
    {
        Debug.Log("BLEF");
        StandardFilterButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(1f, 1f, 1f);
        ModernFilterButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(1f, 1f, 1f);
        LegacyFilterButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(1f, 1f, 1f);

        if (PlayerPrefs.GetInt("FormatFilter") == 1)
        {
            StandardFilterButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(0f, 0f, 0f);            
        }
        else if (PlayerPrefs.GetInt("FormatFilter") == 1)
        {
            ModernFilterButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(0f, 0f, 0f);            
        }
        else if (PlayerPrefs.GetInt("FormatFilter") == 1)
        {
            LegacyFilterButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(0f, 0f, 0f);            
        }
    }

    void MoneyButtonsUpdate()
    {
        Debug.Log("BLORF");
        TenButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(1f, 1f, 1f);
        ThirtyButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(1f, 1f, 1f);
        ThirtyPlusButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(1f, 1f, 1f);

        if (PlayerPrefs.GetInt("MoneyFilter") <= 10)
        {
            TenButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(0f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("MoneyFilter") <= 30)
        {
            ThirtyButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(0f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("MoneyFilter") > 30)
        {
            ThirtyPlusButton.GetComponent<FilterButton>().BackgroundColor.color = new Color(0f, 0f, 0f);
        }
    }

    void CreateList()
    {
        int count = 0;
        foreach (CardObject x in CardHistory)
        {
            count++;
            x.Position = count;
            if (x.Position >= 10)
            {
                x.GO.name = "00" + x.Position;
            }
            else if (x.Position >= 100)
            {
                x.GO.name = "0" + x.Position;
            }
            else if (x.Position >= 1000)
            {
                x.GO.name = x.Position.ToString();
            }
            else if (x.Position >= 10000)
            {
                CardHistory.Remove(x);
            }
            else
            {
                x.GO.name = "000" + x.Position;
            }
        }
        GridRef.GetComponent<UIGrid>().repositionNow = true;
    }

    public void InstantiateNewCard(TcgCard newCard)
    {
        GameObject newGO = Instantiate(Resources.Load<GameObject>("FrontPage/Item")) as GameObject;

        CardObject tmp = new CardObject(newCard, newGO);

        CardHistory.Add(tmp);

        newGO.name = "000" + 0;
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
    void SortByDollarRatio()
    {
        CardHistory.Sort((CardObject node1, CardObject node2) => (node2.TheCard.AvgPrice - node2.TheCard.LowPrice)
            .CompareTo(node1.TheCard.AvgPrice - node1.TheCard.LowPrice));
    }

    void SortByPercentageRatio()
    {
        CardHistory.Sort((CardObject node1, CardObject node2) => (node2.TheCard.LowPrice / node2.TheCard.AvgPrice)
            .CompareTo(node1.TheCard.LowPrice / node1.TheCard.AvgPrice));
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