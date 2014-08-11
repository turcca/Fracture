using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour
{
    public GameObject market;
    public GameObject diplomacy;

    void Start()
    {
        hideAllPages();
    }

    private void hideAllPages()
    {
        market.SetActive(false);
        diplomacy.SetActive(false);
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
}
