using UnityEngine;
using System.Collections;
using System;

public class BuyButton : MonoBehaviour
{
	public string CardUrl = "";

	void OnClick()
	{
		if (CardUrl != "")
		{
			Application.OpenURL(CardUrl);
		}
	}
}
