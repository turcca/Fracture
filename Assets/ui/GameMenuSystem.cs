using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMenuSystem : MonoBehaviour
{
    public List<GameObject> pages;
    public GameObject characterSelectDialog;
    public GameObject locationEntryDialog;

    // floating panel
    public NPCshipInfo shipInfo;

    // Use this for initialization
    void Start()
    {
        hideAllPages();
        characterSelectDialog.SetActive(false);
        locationEntryDialog.SetActive(false);

        shipInfo.gameObject.SetActive(false);

        //ToolTipLibrary.format();
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

    void notifyChange()
    {
        BroadcastMessage("updateView");
    }

    public void showLocationEntryDialog()
    {
        locationEntryDialog.SetActive(true);
    }

    public void hideLocationEntryDialog()
    {
        locationEntryDialog.SetActive(false);
    }

    public void showNPCshipInfo(Simulation.NPCShip ship, bool makeVisible)
    {
        shipInfo.gameObject.SetActive(makeVisible);
        if (makeVisible) shipInfo.setVisibility(ship, makeVisible);
    }
}
