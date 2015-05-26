using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{
    public GameObject orbit;
    public GameObject tradepost;
    public GameObject shipyard;
    public GameObject forum;

    // Use this for initialization
    void Start()
    {
        showOrbit();
    }

    internal void showOrbit()
    {
        hideAll();
        orbit.SetActive(true);
    }

    private void hideAll()
    {
        orbit.SetActive(false);
        tradepost.SetActive(false);
        shipyard.SetActive(false);
        forum.SetActive(false);
    }

    internal void showTradepost()
    {
        hideAll();
        tradepost.SetActive(true);
    }

    internal void showShipyard()
    {
        hideAll();
        shipyard.SetActive(true);
    }

    internal void showForum()
    {
        hideAll();
        forum.SetActive(true);
    }
}
