using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour
{
    public Background background;
    public GameObject orbit;
    public GameObject market;
    public GameObject shipyard;
    public GameObject diplomacy;

    void Start()
    {
        //hideAllPages();
        GameState.requestState(GameState.State.Location);
    }

    private void hideAllPages()
    {
        orbit.SetActive(false);
        market.SetActive(false);
        shipyard.SetActive(false);
        diplomacy.SetActive(false);
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
        shipyard.SetActive(true);
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
        GameState.requestState(GameState.State.Starmap);
        Application.LoadLevel(0);
    }

    public void hideAll()
    {
        hideAllPages();
    }
}
