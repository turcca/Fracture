using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour
{
    public GameObject orbit;
    public GameObject market;
    public GameObject diplomacy;
    public GameObject ship;

    void Start()
    {
        //hideAllPages();
    }

    private void hideAllPages()
    {
        orbit.SetActive(false);
        market.SetActive(false);
        diplomacy.SetActive(false);
        ship.SetActive(false);
    }

    void Update()
    {

    }

    public void showOrbit()
    {
        hideAllPages();
        orbit.SetActive(true);
    }

    public void showTradepost()
    {
        hideAllPages();
        market.SetActive(true);
    }

    public void showShipyard()
    {
        hideAllPages();
        ship.SetActive(true);
    }

    public void showForum()
    {
        hideAllPages();
        diplomacy.SetActive(true);
    }

    public void exit()
    {
        Application.LoadLevel(0);
    }

    public void hideAll()
    {
        hideAllPages();
    }
}
