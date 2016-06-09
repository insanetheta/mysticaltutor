using System;
using System.Globalization;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DealFinder.Network.Models;

internal class CardDataManager : MonoBehaviour
{
    public static CardDataManager GetInstance()
    {
        if (CDM == null)
        {
            CDM = new GameObject("CardDataManager").AddComponent<CardDataManager>();
        }
        return CDM;
    }

    public List<TcgCard> CardsAll { private set; get; }
    
    private static CardDataManager CDM;
    public FormatFilters currentFormatFilter;
    public int currentMoneyFilter;

    public enum FormatFilters
    {
        None,
        Standard,
        Modern,
        Legacy
    }

    private static readonly Dictionary<int, FormatFilters> FormatFilterMap = new Dictionary<int, FormatFilters>()
    {
        {0, FormatFilters.None},
        {1, FormatFilters.Standard},
        {2, FormatFilters.Modern},
        {3, FormatFilters.Legacy}
    };

    /// <summary>
    /// Fetch Base Card Data containing all cards
    /// </summary>
    /// <returns></returns>
    public IEnumerator BaseCardsRequest()
    {
        List<BaseCard> baseCards;

        string allCardsFile = "BaseCards";

        string allCardsText = LocalStorage.LoadFileText(allCardsFile);
        
        if (allCardsText.Length < 1)
        {
            Transaction<List<BaseCard>> t = new Transaction<List<BaseCard>>();
            Debug.Log("Fetching cards http: " + Time.realtimeSinceStartup);
            //yield return StartCoroutine(t.HttpGetRequest("http://localhost:8000/static/card_data/card_data_base.json"));
            yield return StartCoroutine(t.HttpGetRequest("http://gbackdesigns.com/dealfinder/static/card_data/card_data_base.json"));
            Debug.Log("Setting String: " + Time.realtimeSinceStartup);

            LocalStorage.SaveFile(t.GetText(), allCardsFile);

            Debug.Log("converting string to deserialized objects: " + Time.realtimeSinceStartup);
            baseCards = t.GetResponse();
            Debug.Log("finished: " + Time.realtimeSinceStartup);
        }
        else
        {
            Debug.Log("Convert Cards String: " + Time.realtimeSinceStartup);
            baseCards = allCardsText.CreateFromJsonString<List<BaseCard>>();
            Debug.Log("Converted: " + Time.realtimeSinceStartup);
        }
    }

    public IEnumerator CardListRequest()
    {
        Transaction<List<TcgCard>> t = new Transaction<List<TcgCard>>();
        yield return StartCoroutine(t.HttpGetRequest("http://gbackdesigns.com/dealfinder/mobile/api"));
        //yield return StartCoroutine(t.HttpGetRequest("http://127.0.0.1:8000/dealfinder/mobile/api"));
        CardsAll = t.GetResponse();
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

    public void ChangeFormatFilter(FormatFilters target)
    {
        currentFormatFilter = target;
        PlayerPrefs.SetInt("FormatFilter", (int)target);
    }

    public void ChangeMoneyFilter(int target)
    {
        Debug.Log(target);
        currentMoneyFilter = target;
        PlayerPrefs.SetInt("MoneyFilter", currentMoneyFilter);
    }

    public List<TcgCard> FilteredCards()
    {
        List<TcgCard> filteredCards = CardsAll;
        currentFormatFilter = FormatFilterMap[PlayerPrefs.GetInt("FormatFilter", 0)];
        currentMoneyFilter = PlayerPrefs.GetInt("MoneyFilter", 0);
        //CultureInfo englishLang = CultureInfo.InvariantCulture;

        filteredCards = filteredCards.Where(card =>
            (card.LowPrice <= currentMoneyFilter || 
            currentMoneyFilter == 0) &&
            (currentFormatFilter == FormatFilters.None ||
            card.Formats.Any
                (format => 
                    format.IndexOf(currentFormatFilter.ToString(), StringComparison.OrdinalIgnoreCase) >= 0))
                ).ToList();
        return filteredCards;
    }
}