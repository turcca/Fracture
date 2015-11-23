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

    //AsyncOperation op;

    void Start()
    {
        //hideAllPages();
        GameState.requestState(GameState.State.Location);
        //startLoadingLevel(); //async
    }

    private void hideAllPages()
    {
        orbit.SetActive(false);
        market.SetActive(false);
        shipyard.SetActive(false);
        diplomacy.SetActive(false);
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
        //Debug.Log("Leaving location");
        GameState.requestState(GameState.State.Starmap);
        Application.LoadLevel(0); // switch to async loading! Somehow using this, it doesn't mess up with UI elements and still loads fast.
        //op.allowSceneActivation = true;
    }

    public void hideAll()
    {
        hideAllPages();
    }

    /* async loading
    public void startLoadingLevel()
    {
        StartCoroutine("load");
    }
    IEnumerator load()
    {
        if (op == null)
        {
            yield return null;
            Debug.Log("start async loading");
            op = Application.LoadLevelAsync("starmapScene");
            op.allowSceneActivation = false;
        }
        while(!op.isDone)
        {
            //Debug.Log("starmapScene loading: " + op.progress+ " (allow loading: " +op.allowSceneActivation + ")");
            yield return null;
        }
        Debug.Log("starmapScene loaded");
        yield return op;
    }
    */
}
