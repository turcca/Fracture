using UnityEngine;
using System.Collections;

public class LocationSceneState : MonoBehaviour
{
    public string trackedLocation = "not_set";
    public MenuSystem menu;
    public GameObject diplomacy;

    void Awake()
    {
        //trackedLocation = Game.Universe.player.getLocationId();
        try
        {
            //Application.LoadLevelAdditive(trackedLocation);
            Application.LoadLevelAdditive("default");
        }
        catch
        {
            Application.LoadLevelAdditive("default");
        }

        Application.LoadLevelAdditive("eventScene");
        //Application.LoadLevelAdditive("uiScene");
    }

    void Start()
    {
        // deactivate scene camera to use loaded levels main camera
        GameObject.Find("LocationCamera").GetComponent<Camera>().enabled = false;
        menu.hideAll();
        // check and trigger location events
        Game.universe.eventManager.queryLocationEvents(new EventManager.AllDoneDelegate(eventQueryDone));
    }

    public void eventQueryDone()
    {
        menu.showMain();
    }

    public void diplomacyQueryDone()
    {
        menu.showMain();
        menu.show(diplomacy);
    }

    public void startDiplomacyEvent(string faction)
    {
        menu.hideAll();
        Game.universe.eventManager.queryDiplomacyEvents(faction, new EventManager.AllDoneDelegate(diplomacyQueryDone));
    }
}
