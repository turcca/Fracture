using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager
{
    EventUI eventUI;

    public delegate void AllDoneDelegate();

    private AllDoneDelegate allDoneCallback;

    private List<EventBase> eventPool = new List<EventBase>();
    private List<EventBase> triggerEventPool = new List<EventBase>();
    private List<EventBase> eventQueue = new List<EventBase>();

    private float daysSinceLastEvent = 0;
    private float eventInterval = 13.0f;
    private float timePow = 2.0f;

    //List<KeyValuePair<int, EventBase>> eventQueue;

    public EventManager()
    {
        //System.Activator.CreateInstance(System.Type.GetType("Event_1"));
    }

    public void createAllEvents()
    {
        for (int i = 1; System.Type.GetType("Event_" + i) != null; ++i)
        {
            System.Activator.CreateInstance(System.Type.GetType("Event_" + i));
        }
    }

    public void addEventToPool(EventBase e)
    {
        if (!e.locationEvent)
        {
            //Debug.Log("Adding event (pool): " + e.name);
            eventPool.Add(e);
        }
        else
        {
            //Debug.Log("Adding event (trigger pool): " + e.name);
            triggerEventPool.Add(e);
        }
    }

    public void tick(float days)
    {
        daysSinceLastEvent += days;
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
        Debug.Log ("eventPool size: "+eventPool.Count+ "    triggerEventPool size: "+triggerEventPool.Count);

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
        if (GameState.isState(GameState.State.Starmap))
        {
            float d = Mathf.Pow(daysSinceLastEvent / (eventInterval*1.35f), timePow) / (eventInterval*1.35f);
            float probability = d * 1.0f; // todo other mods
                                          //Debug.Log("event prob = " + probability);

            float roll = Random.value;

            if (roll < probability)
            {
                return pickEvent();
            }
        }
        return null;
    }

    public void queryLocationEvents(string locationId, AllDoneDelegate callback)
    {
        //@note test location events
        Debug.Log ("todo: load location events");

        handleEvent(pickEvent(), callback);
        //handleEvent(null);
    }

    public void queryDiplomacyEvents(string faction, AllDoneDelegate callback)
    {
        allDoneCallback = callback;
        bool eventFound = false;
        string eventName = "appointment_" + faction;
        foreach (EventBase e in eventPool)
        {
            if (e.name == eventName)
            {
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
                    Debug.Log ("pickEvent: "+availableEvent.name+ "("+Mathf.Round (roll *100.0f)/100.0f+"/"+Mathf.Round (combinedProbability *100.0f)/100.0f+")");
                    daysSinceLastEvent = 0;
                    return availableEvent;
                }
            }
        }
        return null;
    }

    public void handleEvent(EventBase e, AllDoneDelegate callback)
    {
        Debug.Log ("handing event: "+ e.name+" (available "+e.available+")");
        allDoneCallback = callback;
        if (e == null)
        {
            eventDone();
        }

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
        Debug.LogError ("getEvent: event not found: '"+name+"'"); 
        return null;
    }
}
