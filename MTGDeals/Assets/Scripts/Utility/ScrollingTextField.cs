using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;
using DealFinder.Network.Models;

public class ScrollingTextField : MonoBehaviour
{
    protected float YBounds;
    protected float XBounds;
    private string AddressOfItemToFillWith;
    private Transform GridRef;

    public void Initialize(Transform Parent, string addressOfItemToFillWith, float ysize, float xsize)
    {
        transform.parent = Parent;
        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.localPosition = new Vector3(0,0,0);
        GameObject TextField = Instantiate(Resources.Load<GameObject>("FrontPage/ScrollingText")) as GameObject;
        TextField.transform.parent = this.transform;
        TextField.transform.localScale = new Vector3(1, 1, 1);
        TextField.transform.localPosition = new Vector3(0, 0, 0);
        YBounds = ysize;
        XBounds = xsize;
        AddressOfItemToFillWith = addressOfItemToFillWith;
        GridRef = TextField.transform.FindChild("Grid");
    }

    private void InstantiateNewCard(TcgCard newCard)
    {
        GameObject newGO = Instantiate(Resources.Load<GameObject>(AddressOfItemToFillWith)) as GameObject;
        CardObject tmp = new CardObject(newCard, newGO);

        newGO.name = newCard.Name;
        newGO.transform.parent = GridRef;
        newGO.transform.Find("Name").GetComponent<UILabel>().text = newCard.Name;
        newGO.transform.localScale = new Vector3(1, 1, 1);
    }

    public IEnumerator CreateList(string searchTarget)
    {
        foreach (Transform go in GridRef)
        {
            Destroy(go.gameObject);
        }

        yield return new WaitForSeconds(.01f);
        List<TcgCard> filterList = new List<TcgCard>();

        foreach (TcgCard card in CardDataManager.GetInstance().CardsAll)
        {
            if (card.Name.Contains(searchTarget))
            {
                filterList.Add(card);
            }
        }

        foreach (TcgCard card in filterList)
        {
            Debug.Log(card.Name);
            InstantiateNewCard(card);
        }

        GridRef.GetComponent<UIGrid>().repositionNow = true;
    }
}
