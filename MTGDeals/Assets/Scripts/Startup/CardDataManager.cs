using System;
using System.Globalization;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DealFinder.Network.Models;

internal class CardDataManager : MonoBehaviour
{
    private static CardDataManager CDM;

    public static CardDataManager GetInstance()
    {
        if (CDM == null)
        {
            CDM = new GameObject("CardDataManager").AddComponent<CardDataManager>();
        }
        return CDM;
    }

    private List<TcgCard> CardsAll;

    public IEnumerator CardListRequest()
    {
        Transaction<List<TcgCard>> t = new Transaction<List<TcgCard>>();
        yield return StartCoroutine(t.HttpGetRequest("http://gbackdesigns.com/dealfinder/mobile/api"));
        //yield return StartCoroutine(t.HttpGetRequest("http://127.0.0.1:8000/dealfinder/mobile/api"));
        CardsAll = t.GetResponse();
        Debug.Log(CardsAll.Count);
        yield return null;
    }

    public TcgCard FindCardByText(string text)
    {
        foreach (TcgCard card in CardsAll)
        {
            Debug.Log(card.Name);
            if (card.Name == text)
            {
                return card;
            }
        }
        Debug.Log("SDLFKJIJSDOFIJSs");
        return null;
    }

    private FormatFilters currentFormatFilter;

    private int currentMoneyFilter;

    private static readonly Dictionary<int, FormatFilters> FormatFilterMap = new Dictionary<int, FormatFilters>()
    {
        {0, FormatFilters.None},
        {1, FormatFilters.Standard},
        {2, FormatFilters.Modern},
        {3, FormatFilters.Legacy}
    };

    public List<TcgCard> FilteredCards()
    {
        List<TcgCard> filteredCards = CardsAll;
        currentFormatFilter = FormatFilterMap[PlayerPrefs.GetInt("FormatFilter", 0)];
        currentMoneyFilter = PlayerPrefs.GetInt("MoneyFilter", 0);
        CultureInfo englishLang = CultureInfo.InvariantCulture;

        filteredCards = filteredCards.Where(card => 
            card.LowPrice <= currentMoneyFilter &&
            card.Formats.Any
                (format => 
                    format.IndexOf(currentFormatFilter.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();

        return filteredCards;
    }

    enum FormatFilters
    {
        None,
        Standard,
        Modern,
        Legacy
    }
}