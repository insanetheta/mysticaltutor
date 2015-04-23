using UnityEngine;
using System.Collections;
using DealFinder.Network.Models;


public class FrontPageButton : MonoBehaviour
{
	public TcgCard TheCardRef;

	void OnClick ()
	{
		GameObject newView = ViewController.GetInstance().CreateView("CardDetail/CardDetail");
		newView.GetComponent<CardDetailController>().coLoadCard(CardDataManager.GetInstance().FindCardByText(TheCardRef.Name));
	}
}
