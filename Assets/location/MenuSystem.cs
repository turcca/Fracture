using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour
{
    public GameObject menu;
    public GameObject market;
    public GameObject diplomacy;
    public GameObject ship;

    void Start()
    {
        hideAllPages();
    }

    private void hideAllPages()
    {
        market.SetActive(false);
        diplomacy.SetActive(false);
        ship.SetActive(false);
    }

    void Update()
    {

    }

    public void show(GameObject page)
    {
        hideAllPages();
        page.SetActive(true);
    }

    public void exit()
    {
        Application.LoadLevel(0);
    }


    public void hideAll()
    {
        hideAllPages();
        menu.SetActive(false);
    }

    public void showMain()
    {
        menu.SetActive(true);
    }
}
