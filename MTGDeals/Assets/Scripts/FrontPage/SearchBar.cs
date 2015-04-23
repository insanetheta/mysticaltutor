using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using System.Collections;
using System;

public class SearchBar : MonoBehaviour
{
    private UILabel CardToSearchFor;
    private TouchScreenKeyboard Keyboard;
    private bool KeyboardOn = false;

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
            StartCoroutine(BeginSearch());
        }
    }

    private IEnumerator BeginSearch()
    {
        while (KeyboardOn)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                KeyboardOn = false;
                Debug.Log(CardToSearchFor.text);
                GameObject newView = ViewController.GetInstance().CreateView("CardDetail/CardDetail");
                newView.GetComponent<CardDetailController>().coLoadCard(CardDataManager.GetInstance().FindCardByText(CardToSearchFor.text));
            }
            
            CardToSearchFor.text += Input.inputString;

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                CardToSearchFor.text = "";
            }

            yield return null;            
        }
    }
}
