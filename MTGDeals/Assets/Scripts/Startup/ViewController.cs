using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour 
{
    private static ViewController VC;
    private static Stack<Transform> Views;

    public static ViewController GetInstance()
    {
        if (VC == null)
        {
            VC = new GameObject("ViewController").AddComponent<ViewController>();
        }
        return VC;
    }

    public void Initialize(Transform anchorRef)
    {
        // Create The FrontPage
        Views = new Stack<Transform>();
        Views.Push(anchorRef);
        GameObject FrontPage = Instantiate(Resources.Load<GameObject>("FrontPage/FrontPage")) as GameObject;
        PushView(FrontPage.transform);
        //GameObject FrontPage = Instantiate(Resources.Load<GameObject>("CardDetail/CardDetail")) as GameObject;
    }

    public void PushView(Transform NewView)
    {
        NewView.parent = Views.Peek();
        NewView.localScale = new Vector3(1, 1, 1);
        NewView.localPosition = new Vector3(0, 0, 0);

        Views.Push(NewView);
    }
}
