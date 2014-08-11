using UnityEngine;
using System.Collections;

public class LocationSceneState : MonoBehaviour
{
    public string trackedLocation = "a";
    public GameObject menu;

    void Awake()
    {
        //trackedLocation = Game.getUniverse().player.getLocationId();
        Application.LoadLevelAdditive(trackedLocation);
        Application.LoadLevelAdditive("eventScene");
    }

    void Start()
    {
        // deactivate scene camera to use loaded levels main camera
        GameObject.Find("LocationCamera").GetComponent<Camera>().enabled = false;
        menu.SetActive(false);
        // check and trigger location events
        Game.getUniverse().eventManager.queryLocationEvents(new EventManager.AllDoneDelegate(eventQueryDone));
    }

    public void eventQueryDone()
    {
        // activate location ui
        menu.SetActive(true);
    }

    public void startDiplomacyEvent(string faction)
    {
        Game.getUniverse().eventManager.queryDiplomacyEvents(faction, new EventManager.AllDoneDelegate(eventQueryDone));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
