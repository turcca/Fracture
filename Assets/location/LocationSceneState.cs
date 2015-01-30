using UnityEngine;
using System.Collections;

public class LocationSceneState : MonoBehaviour
{
    public string trackedLocation = "not_set";
    public MenuSystem menu;
    public GameObject diplomacy;

    void Awake()
    {
        string playerLocation = Game.universe.player.getLocationId();
        if (Game.universe.locations.ContainsKey(playerLocation))
        {
            trackedLocation = playerLocation;
        }
        else
        {
            Tools.error("Could not find location: " + playerLocation);
        }

        try
        {
            //Application.LoadLevelAdditive(trackedLocation);
            Application.LoadLevelAdditive("default");
        }
        catch
        {
            Application.LoadLevelAdditive("default");
        }

        //Application.LoadLevelAdditive("uiScene");
    }

    void Start()
    {
        Application.LoadLevelAdditive("generalUIScene");
        // deactivate scene camera to use loaded levels main camera
        GameObject.Find("LocationCamera").GetComponent<Camera>().enabled = false;
        menu.hideAll();
        // check and trigger location events
        //Game.universe.eventManager.queryLocationEvents(new EventManager.AllDoneDelegate(eventQueryDone));
        //@note skip events
        eventQueryDone();
    }

    public void eventQueryDone()
    {
        menu.showOrbit();
    }

    public void diplomacyQueryDone()
    {
        Game.ui.hideEventWindow();
        menu.showForum();
    }

    public void startDiplomacyEvent(string faction)
    {
        if (!Game.ui.isEventWindow())
        {
            Game.universe.eventManager.queryDiplomacyEvents(faction, new EventManager.AllDoneDelegate(diplomacyQueryDone));
            Game.ui.showEventWindow();
        }
    }
}
