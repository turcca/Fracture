using UnityEngine;
using System.Collections;

public class UiLocationMain : MonoBehaviour
{
	GameObject tradeWindow;
    // Use this for initialization
    void Start()
    {
		tradeWindow = GameObject.Find("UItrade");
		NGUITools.SetActive(tradeWindow, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void msgButtonExit()
    {
        Application.LoadLevel(0);
    }

    public void msgButtonTrade()
    {
        NGUITools.SetActive(tradeWindow, !NGUITools.GetActive(tradeWindow));
    }
}
