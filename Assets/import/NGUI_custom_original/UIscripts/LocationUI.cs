using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class LocationUI : MonoBehaviour
{
    /*
    public static LocationUI Instance; // Gives out references to the only LocationUI script object

    private Events events;
    public eventClass locationEvent;// = new eventClass();
    public Dictionary<string, adviceClass> advice = new Dictionary<string, adviceClass>();
    public locationDatabase currentLocation;

    private Dictionary<GameObject, locationDatabase> locationDictionary;


    public UIPanel locationPanel;
    public UIPanel locationBGpanel;
    public UIPanel locationMenuPanel;

    private GameObject locationPortraits;

    private GameObject scriptHolder;
    private locationsDataCollector collector;

    private GameObject player;
    private LocationFinder finder;

    public locationState State;
    private string appointmentFaction;

    private Dictionary<locationState, UIPanel> panel;


    // Use this for initialization
    void Awake()
    {
        LocationUI.Instance = this;
        locationPortraits = GameObject.Find("LocationPortraitGrid");
        player = GameObject.FindGameObjectWithTag("Player");
        events = GameObject.Find("ScriptHolder").GetComponent<Events>();
        finder = player.gameObject.GetComponent<LocationFinder>();

        if (locationPanel == null) locationPanel = this.gameObject.GetComponent<UIPanel>();
        if (locationBGpanel == null) GameObject.Find("Location BG panel").GetComponent<UIPanel>();
        if (locationMenuPanel == null) GameObject.Find("UILocationMenuPanel").GetComponent<UIPanel>();

        scriptHolder = GameObject.Find("ScriptHolder");
        collector = scriptHolder.gameObject.GetComponent<locationsDataCollector>();
    }
    void Start()
    {
        // build locationDictionary
        locationDictionary = new Dictionary<GameObject, locationDatabase>();
        foreach (locationDatabase l in collector.locationsPlanets)
        {
            locationDictionary.Add(l.gameobject, l);
        }

        locationState State = new locationState();

        panel = new Dictionary<locationState, UIPanel>();
        // build panel
        panel.Add(locationState.Main, GameObject.Find("UImain").GetComponent<UIPanel>());
        panel.Add(locationState.Appointment, GameObject.Find("UIappointment").GetComponent<UIPanel>());
        panel.Add(locationState.Trade, GameObject.Find("UItrade").GetComponent<UIPanel>());
        panel.Add(locationState.Recruit, GameObject.Find("UIrecruit").GetComponent<UIPanel>());
        panel.Add(locationState.Shipyard, GameObject.Find("UIshipyard").GetComponent<UIPanel>());

        loadState(locationState.Exit);

    }

    // load location from external methods (collision)
    public bool loadLocation(GameObject loc)
    {
        // if a location
        if (locationDictionary.ContainsKey(loc))
        {
            // set
            currentLocation = locationDictionary[loc];

            return true;
        }
        // not a location
        else
        {
            return false;
        }
    }
    public void unloadLocation()
    {
        advice = null;

        currentLocation = null;
        locationEvent = null;

        if (locationEvent != null && events.Event.ContainsValue(locationEvent))
        {
            locationEvent.locationObject = null;	// GameObject of location
            locationEvent.location = null;	// locationDatabase to use
            locationEvent.locationName = null;	// Game location name
        }
    }

    // ******************************************************
    // ENTER

    public void enterLocation()
    {
        if (currentLocation != null)	// change to location-crafted event/advice when ready !!?!
        {

            // *************************************
            collector.sectorSimulationCycle();		// update production on all locations!
            // *************************************

            // event dictionary access point for locations
            string eventInputForLocation = "locationAdvice_" + currentLocation.gameobject.name;

            // as a location, there is a location-unique event containing relevant advice
            // If there are no location-unique advice, use default
            if (!events.Event.ContainsKey(eventInputForLocation)) eventInputForLocation = "locationAdvice";
            if (events.Event.ContainsKey(eventInputForLocation))
            {
                // save event advice
                Dictionary<string, adviceClass> savedEventAdvice = new Dictionary<string, adviceClass>(events.advice);

                // load event advice for location
                events.Event[eventInputForLocation].location = currentLocation;
                events.Event[eventInputForLocation].getAdvice();
                advice = events.advice;

                locationEvent = events.Event[eventInputForLocation];

                locationEvent.locationObject = currentLocation.gameobject;	// GameObject of location
                locationEvent.location = currentLocation;				// locationDatabase to use
                locationEvent.locationName = currentLocation.name;			// Game location name

                // restore event advice
                events.advice = savedEventAdvice;
            }
            else Debug.LogError("ERROR: location advice-event '" + eventInputForLocation + "' does not excist in Event dictionary.");


            // load available panels
            loadState(locationState.All);
            locationUI(true);
            loadState(locationState.Main);

        }
        else Debug.LogWarning("WARNING: 'enterLocation' called but 'currentLocation' was null.");
    }


    // LocationStateBtn / OnClick()
    public void loadState(locationState state, string faction = null, string e = null)
    {
        State = state;

        // on exit
        if (state == locationState.Exit) { exitLocation(); return; }

        appointmentFaction = faction;

        foreach (locationState s in panel.Keys)
        {
            // picked state
            if (state == s || state == locationState.All)
            {
                panel[s].gameObject.SetActive(true);
            }
            // other states
            else
            {
                panel[s].gameObject.SetActive(false);
            }
        }
        // on Main -state make sure location portraits are on
        locationPortraits.SetActive(true);

        // on Appointment -state
        if (state == locationState.Appointment)
        {
            //Debug.Log("appointment_"+faction);
            State = locationState.Event;
            string eventName;
            if (faction == null) eventName = "appointment_Locals";
            else eventName = "appointment_" + faction;
            events.Event[eventName].getLocation();
            StartCoroutine(startEvent(eventName));
            State = locationState.Appointment;
        }
        // on Event - state (called from eventManeger, usually inLocation triggers)
        else if (state == locationState.Event)
        {
            locationPortraits.SetActive(false);
            // at an event end, loadState(Main) will turn locationPortraits back on
        }
    }
    IEnumerator startEvent(string e)
    {
        // hide location portraits
        locationPortraits.SetActive(false);
        // set up event edvice
        //events.advice = EventUI.Instance.advice;
        // start the event -coroutine
        yield return StartCoroutine(EventManager.Instance.eventHandler(e));
        // unhide location portraits when event is done
        locationPortraits.SetActive(true);
        // return location advice
        //events.advice = advice;
        yield return null;
    }

    public void loadEventState(bool on)
    {
        if (on) loadState(locationState.Event);
        else loadState(locationState.Main);
    }


    public void exitLocation()
    {
        // on loadState(locationState.Exit);
        foreach (locationState s in panel.Keys) panel[s].gameObject.SetActive(true);	// turn all on for broadcast
        locationUI(false);																// broadcast locationUI off
        foreach (locationState s in panel.Keys) panel[s].gameObject.SetActive(false);	// turn all panels off for exit

        // *************************************
        collector.sectorSimulationCycle();		// update production on all locations!
        // *************************************

        finder.exitLocation();
    }


    // ******************************************************

    void locationUI(bool on)
    {
        // UIPanel on/off
        locationPanel.enabled = on;
        locationBGpanel.enabled = on;
        locationMenuPanel.enabled = on;

        if (on)
        {
            // on
            BroadcastMessage("enable", true, SendMessageOptions.RequireReceiver);

        }
        else
        {
            // off	
            BroadcastMessage("enable", false, SendMessageOptions.RequireReceiver);
        }
    }
    */
}
