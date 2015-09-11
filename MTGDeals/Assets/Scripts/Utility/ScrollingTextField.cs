using System.Collections.Generic;
using System.Net.Mime;
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
    private GameObject Border;
    private GameObject TextField;

    public void Initialize(Transform Parent, string addressOfItemToFillWith, float ysize, float xsize)
    {
        transform.parent = Parent;
        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.localPosition = new Vector3(0, -20, 0);
        TextField = Instantiate(Resources.Load<GameObject>("FrontPage/ScrollingText")) as GameObject;
        TextField.transform.parent = this.transform;
        TextField.transform.localPosition = new Vector3(0, 0, 0);
        TextField.transform.localScale = new Vector3(1, 1, 1);

        YBounds = ysize;
        XBounds = xsize;
        AddressOfItemToFillWith = addressOfItemToFillWith;
        GridRef = TextField.transform.FindChild("Grid");
        GridRef.GetComponent<UIGrid>().cellHeight = YBounds;
        Border = TextField.transform.Find("Border").gameObject;
        Border.transform.localScale = new Vector3(TextField.GetComponent<UIPanel>().clipRange.z + 7, TextField.GetComponent<UIPanel>().clipRange.w+7, 1);
        Border.transform.localPosition = new Vector3(0, -(TextField.GetComponent<UIPanel>().clipRange.w - 14)/2, 0);
        Border.transform.parent = transform.parent;

    }

    private void InstantiateNewCard(TcgCard newCard, Color backgroundColor)
    {
        GameObject newGO = Instantiate(Resources.Load<GameObject>(AddressOfItemToFillWith)) as GameObject;
        Border.GetComponent<UISprite>().depth = newGO.transform.Find("Background").GetComponent<UISprite>().depth-1;
        newGO.name = newCard.Name;
        newGO.transform.parent = GridRef;
        newGO.transform.Find("Name").GetComponent<UILabel>().text = newCard.Name;
        newGO.transform.Find("Background").GetComponent<UISprite>().color = backgroundColor;
        newGO.transform.Find("Background").transform.localScale = new Vector3(TextField.GetComponent<UIPanel>().clipRange.z,
            newGO.transform.Find("Background").transform.localScale.y, newGO.transform.Find("Background").transform.localScale.z);
        newGO.GetComponent<FrontPageButton>().TheCardRef = newCard;
        newGO.transform.localScale = new Vector3(1, 1, 1);
    }

    private static Color baseItemColor = new Color(208f / 255f, 208f / 255f, 208f / 255f);
    private static Color variantItemColor = new Color(171f / 255f, 171f / 255f, 171f / 255f);

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
            string cardNameLowerCase = card.Name.ToLower();
            if (cardNameLowerCase.Contains(searchTarget.ToLower()))
            {
                filterList.Add(card);
            }
        }

        for (int i = 0; i < filterList.Count; i++)
        {
            Debug.Log(filterList[i].Name);
            InstantiateNewCard(filterList[i], i % 2 == 1 ? baseItemColor : variantItemColor);
        }

        GridRef.GetComponent<UIGrid>().repositionNow = true;
    }
}
