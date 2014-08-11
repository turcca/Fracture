using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMenuSystem : MonoBehaviour
{
    public List<GameObject> pages;
    public GameObject characterSelectDialog;

    // Use this for initialization
    void Start()
    {
        hideAllPages();
        characterSelectDialog.SetActive(false);
    }

    private void hideAllPages()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void show(GameObject page)
    {
        hideAllPages();
        page.SetActive(true);
    }
}
