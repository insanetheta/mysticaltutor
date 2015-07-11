using System.Linq;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections;
using System;

public class SearchBar : MonoBehaviour
{
    private UILabel CardToSearchFor;
    private TouchScreenKeyboard Keyboard;
    private bool KeyboardOn = false;
    private ScrollingTextField TextField;

    void Start()
    {
        CardToSearchFor = transform.Find("Label").GetComponent<UILabel>();
    }

    void OnClick()
    {
        CardToSearchFor.text = "";
        Keyboard = TouchScreenKeyboard.Open(CardToSearchFor.text, TouchScreenKeyboardType.Default, false, false, false);
        
        if (!KeyboardOn)
        {
            KeyboardOn = true;
            TextField = new GameObject("TextField").AddComponent<ScrollingTextField>();
            TextField.Initialize(transform, "FrontPage/SearchItem", 30, 300);
            StartCoroutine(BeginSearch());
        }
    }

    private IEnumerator BeginSearch()
    {
        while (KeyboardOn)
        {
            bool doOnce = false;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                KeyboardOn = false;
                Debug.Log(CardToSearchFor.text);
                GameObject newView = ViewController.GetInstance().CreateView("CardDetail/CardDetail");
                newView.GetComponent<CardDetailController>().coLoadCard(CardDataManager.GetInstance().FindCardByText(CardToSearchFor.text));
            }

            if (Input.inputString != "")
            {
                CardToSearchFor.text += Input.inputString;
                doOnce = true;
            }

            //if (CardToSearchFor.text.Count() > 2 && doOnce)
            if (doOnce)
            {
                yield return StartCoroutine(TextField.CreateList(CardToSearchFor.text));
                //doOnce = false;
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                CardToSearchFor.text = "";
            }

            yield return null;            
        }
    }
}
