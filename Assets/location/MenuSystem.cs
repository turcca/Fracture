using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour
{
    public Background background;
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
        background.showOrbit();
    }

    public void showTradepost()
    {
        hideAllPages();
        market.SetActive(true);
        background.showTradepost();
    }

    public void showShipyard()
    {
        hideAllPages();
        ship.SetActive(true);
        background.showShipyard();
    }

    public void showForum()
    {
        hideAllPages();
        diplomacy.SetActive(true);
        background.showForum();
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
