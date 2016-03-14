using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager
{
    EventUI eventUI;

    public delegate void AllDoneDelegate();

    private AllDoneDelegate allDoneCallback;

    public delegate void ConsequtiveEvent(string loc, AllDoneDelegate callback);
    private ConsequtiveEvent consequtiveEvent;

    private List<EventBase> eventPool = new List<EventBase>();
    private List<EventBase> triggerEventPool = new List<EventBase>();
    //private List<EventBase> eventQueue = new List<EventBase>();

    private float daysSinceLastEvent = 0;
    private float eventInterval = 13.0f;
    private float timePow = 2.0f;
    private float starmapEventBuffer = 0f;
    private float starmapAtlocationEventBuffer = 1f;
    private float starmapInlocationEventBuffer = 1f;
    

    //List<KeyValuePair<int, EventBase>> eventQueue;

    public EventManager()
    {
        //System.Activator.CreateInstance(System.Type.GetType("Event_1"));
    }

    public void createAllEvents()
    {
        Debug.Log("Create all events");
        for (int i = 1; System.Type.GetType("Event_" + i) != null; ++i)
        {
            System.Activator.CreateInstance(System.Type.GetType("Event_" + i));
        }
    }

    public void addEventToPool(EventBase e)
    {
        if (e.triggerEvent == EventBase.trigger.None)
        {
            //Debug.Log("Adding event (pool): " + e.name);
            eventPool.Add(e);
        }
        else
        {
            if (e.filters.ContainsKey("LOC_advice")) e.outcome = 1; // eventEdit organizes advice-events as root events for location trigger events - set advice outcome to 1 so they are only cosmetically attached (advice event's outcomes are never changed)
            //Debug.Log("Adding event (trigger pool: "+e.triggerEvent.ToString()+"): " + e.name);
            triggerEventPool.Add(e);
        }
    }

    public void tick(float days)
    {
        if (GameState.isState(GameState.State.Location))
            starmapInlocationEventBuffer += days;
        else
        {
            daysSinceLastEvent += days;
            starmapEventBuffer += days;
            starmapAtlocationEventBuffer += days;
        }
    }

    public void loadLocationAdvice()
    {
        eventUI = GameObject.Find("MainContent").GetComponent<EventUI>();
        if (eventUI == null)
        {
            Debug.LogWarning ("location advice eventUI not found, it's supposed to be on 'MainContent', contrary to actual events"); 
            return;
        }

        string eventName = "loc_advice_"+Root.game.player.getLocationId().ToUpper();

        foreach (EventBase e in eventPool)
        {
            if (e.name == eventName)
            {
                eventUI.loadLocationAdviceEvent(e);
                return;
            }
        }

        Debug.Log ("TODO: location has no advice-event: '"+eventName+"'");

        foreach (EventBase e in eventPool)
        {
            if (e.name == "loc_advice")
            {
                eventUI.loadLocationAdviceEvent(e);
            }
        } 
    }


    public EventBase queryStarmapEvents()
    {
        if (starmapEventBuffer >= 0.25f)
        {
            starmapEventBuffer = 0;

            if (GameState.isState(GameState.State.Starmap))
            {
                float probability = Mathf.Pow(daysSinceLastEvent / (eventInterval*1.35f), timePow) / (eventInterval*1.35f);
                //float probability = d * 1.0f; // todo other mods
                                              //Debug.Log("event prob = " + probability);

                float roll = Random.value;

                if (roll < probability)
                {
                    return pickEvent();
                }
            }
        }
        return null;
    }

    public EventBase queryInLocationEvents()
    {
        if (starmapInlocationEventBuffer >= 0.15f)
        {
            starmapInlocationEventBuffer = 0;
            eventUI = GameObject.Find("SideWindow").GetComponent<EventUI>();
            return pickTriggerEvent(EventBase.trigger.inLocation);
        }
        return null;
    }
    public EventBase queryAtLocationEvents()
    {
        if (starmapAtlocationEventBuffer >= 0.15f)
        {
            starmapAtlocationEventBuffer = 0;
            return pickTriggerEvent(EventBase.trigger.atLocation);
        }
        return null;
    }


    public void loadDiplomacyEvents(string faction, AllDoneDelegate callback)
    {
        allDoneCallback = callback;
        bool eventFound = false;
        string eventName = "contact_" + faction;
        foreach (EventBase e in eventPool)
        {
            if (e.name == eventName)
            {
                Root.ui.showEventWindow();
                eventUI = GameObject.Find("SideWindow").GetComponent<EventUI>();
                handleEvent(e, callback);
                eventFound = true;
                break;
            }
        }
        if (!eventFound)
        {
            Tools.debug("Event not found: " + eventName);
            eventDone();
        }
    }
    public EventBase pickTriggerEvent(EventBase.trigger trigger)
    {
        if (trigger != EventBase.trigger.None)
        {
            foreach (EventBase e in triggerEventPool)
            {
                if (e.available &&
                    e.triggerEvent == trigger)
                {
                    Character character = e.getEventCharacter();
                    string location = e.getEventLocationID();

                    // at Location necessities - debug
                    //Debug.Log("event: " + e.name + " ("+e.triggerEvent.ToString()+")");
                    //if ((!e.locationRequired) || (e.locationRequired && location != null)) Debug.Log("1 - location pre-matching ok!");
                    //if (trigger == e.triggerEvent && Root.game.player.getLocationId() == location) { Debug.Log("2 - locations match!  e: " + location + "  palyerLoc: " + Root.game.player.getLocationId()); }
                    //if (character == null || character.isActive) Debug.Log("3 - character ok!");
                    //if (e.calculateProbability() > 0.0f) Debug.Log("4 - prob ok!");

                    if (((!e.locationRequired) || (e.locationRequired && location != null)) &&                      // no location needed, or it's not null
                        trigger == e.triggerEvent && Root.game.player.getLocationId() == location &&  // player location same as events
                        (character == null || character.isActive) &&                                                // if character, needs to be active
                        (e.calculateProbability() > 0.0f)                                                           // has positive probability
                        )
                    {
                        //Debug.Log("--> YES: " + e.name);
                        e.initPre();
                        return e;
                    }
                }
            }
        }
        return null;
    }

    public EventBase pickEvent()
    {
        List<EventBase> availableEvents = new List<EventBase>();
        float combinedProbability = 0.0f;

        foreach (EventBase e in eventPool)
        {
            if (e.available &&
                !e.hasFilter("LOC_advice")
                && !e.hasFilter("contact_factions")
                && !e.hasFilter("intro")
                )
            {
                Character character = e.getEventCharacter();
                string location = e.getEventLocationID();

                if ((character == null || character.isActive) &&
                    ((e.locationRequired && location != null) || (!e.locationRequired)) &&
                    (e.calculateProbability() > 0.0f)
                    )
                {
                    availableEvents.Add(e);
                    e.initPre();
                    combinedProbability += e.lastProbability;
                }
            }
        }
        
        if (availableEvents.Count == 0)
        {
            Debug.Log("zero events picked!");
            return null;
        }
        else
        {
            // Influence pool weights by event frequencies
            // only if more than one event picked
            int count = availableEvents.Count;
            if (count > 1) {
                float rareCap     = combinedProbability * 0.25f;
                float elevatedMin = combinedProbability * 0.25f;
                float probableMin = combinedProbability * 0.55f;
                // go through available events
                for (int i = 0; i < count; i++) {
                    // see if special frequency
                    if (availableEvents[i].getFrequency() != EventBase.freq.Default) {
                        // if Rare
                        if (availableEvents[i].getFrequency() == EventBase.freq.Rare) {
                            // if adjustment is needed
                            if (availableEvents[i].lastProbability > rareCap) {
                                combinedProbability = combinedProbability - availableEvents[i].lastProbability + rareCap;
                                availableEvents[i].lastProbability = rareCap;
                            }
                        }
                        // if Elevated
                        else if (availableEvents[i].getFrequency() == EventBase.freq.Elevated) {
                            // if adjustment is needed
                            if (availableEvents[i].lastProbability < elevatedMin) {
                                combinedProbability = combinedProbability - availableEvents[i].lastProbability + elevatedMin;
                                availableEvents[i].lastProbability = elevatedMin;
                            }
                        }
                        // if Probable
                        else if (availableEvents[i].getFrequency() == EventBase.freq.Probable) {
                            // if adjustment is needed
                            if (availableEvents[i].lastProbability < probableMin) {
                                combinedProbability = combinedProbability - availableEvents[i].lastProbability + probableMin;
                                availableEvents[i].lastProbability = probableMin;
                            }
                        }
                    }
                }
            }
            // random picking process
            float roll = Random.value * combinedProbability;
            combinedProbability = 0.0f;
            foreach (EventBase availableEvent in availableEvents)
            {
                combinedProbability += availableEvent.lastProbability;
                if (roll < combinedProbability)
                {
                    daysSinceLastEvent = 0;
                    return availableEvent;
                }
            }
        }
        return null;
    }

    public void handleEvent(EventBase e, AllDoneDelegate callback)
    {
        allDoneCallback = callback;
        if (e == null)
        {
            eventDone();
            return;
        }
        Debug.Log("handing event: " + e.name + " (available " + e.available + ")");

        if (eventUI == null) eventUI = GameObject.Find("SideWindow").GetComponent<EventUI>();
        eventUI.setEvent(e, new EventUI.EventDoneDelegate(eventDone));
    }

    public void eventDone()
    {
        // loop through all events in queue?
        if (allDoneCallback != null)
        {
            allDoneCallback();
            allDoneCallback = null;
        }
    }

    public EventBase getEventByName (string name)
    {
        foreach (EventBase e in eventPool)
        {
            if (e.name == name) return e;
        }
        foreach (EventBase e in triggerEventPool)
        {
            if (e.name == name) return e;
        }
        Debug.LogError ("getEvent: event not found: '"+name+"'"); 
        return null;
    }
}
