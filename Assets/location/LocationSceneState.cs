using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocationSceneState : MonoBehaviour
{
    public string trackedLocation = "not_set";
    public MenuSystem menu;
    public GameObject diplomacy;

    void Awake()
    {
        string playerLocation = Root.game.player.getLocationId();
        if (Root.game.locations.ContainsKey(playerLocation))
        {
            trackedLocation = playerLocation;
        }
        else
        {
            Tools.error("Could not find location: " + playerLocation);
            trackedLocation = "test";
        }

        //try
        //{
        //    //Application.LoadLevelAdditive(trackedLocation);
        //    Application.LoadLevelAdditive("default");
        //}
        //catch
        //{
        //    Application.LoadLevelAdditive("default");
        //}

        //Application.LoadLevelAdditive("uiScene");
    }

    void Start()
    {
        //Application.LoadLevelAdditive("generalUIScene");
        // deactivate scene camera to use loaded levels main camera
        GameObject.Find("LocationCamera").GetComponent<Camera>().enabled = false;



        menu.hideAll();
        // check and trigger inLocation events --> handled in .game
        //Game.universe.eventManager.queryLocationEvents(new EventManager.AllDoneDelegate(eventQueryDone));
        //@note skip events
        eventQueryDone();

        Root.game.events.loadLocationAdvice();
    }
    void Update()
    {
        // tick the inLocationEvent
        Root.game.events.tick(Time.deltaTime);
        // inLocation Events
        if (GameState.isState(GameState.State.Location))
        {
            EventBase e = Root.game.events.queryInLocationEvents();
            if (e != null)
                Root.game.eventStart(e);
        }
    }

    public void eventQueryDone()
    {
        menu.showOrbit();
    }

    public void diplomacyQueryDone()
    {
        Root.ui.hideEventWindow();
        menu.showForum();
    }

    public void startDiplomacyEvent(string faction)
    {
        if (!Root.ui.isEventWindow())
        {
            Root.game.events.loadDiplomacyEvents(faction, new EventManager.AllDoneDelegate(diplomacyQueryDone));
            Root.ui.showEventWindow();
        }
    }
}
