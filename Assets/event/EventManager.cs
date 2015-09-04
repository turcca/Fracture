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

    public void addEventToPool(EventBase e)
    {
        Debug.Log ("Adding event: "+e.name);
        if (!e.locationEvent)
        {
            eventPool.Add(e);
        }
        else
        {
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
            else Debug.Log ("not matching event: '"+e.name+"'");
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

    public void queryStarmapEvents()
    {
        float d = Mathf.Pow(daysSinceLastEvent / (eventInterval*1.35f), timePow) / (eventInterval*1.35f);
        float probability = d * 1.0f; // todo other mods

        float roll = Random.value;

        if (roll < probability)
        {
            handleEvent(pickEvent());
        }
    }

    public void queryLocationEvents(string locationId, AllDoneDelegate callback)
    {
        allDoneCallback = callback;

        //@note test location events
        Debug.Log ("todo: load location events");

        handleEvent(pickEvent());
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
                handleEvent(e);
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
            if (e.available)
            {
                Character character = e.getEventCharacter();
                string location = e.getEventLocationID();

                if ((character == null || character.isActive) &&
                    ((e.locationRequired && location != null) || (!e.locationRequired)))
                {
                    availableEvents.Add(e);
                    e.initPre();
                    combinedProbability += e.calculateProbability();
                }
            }

            if (availableEvents.Count == 0)
            {
                Debug.Log("zero events picked!");
            }

            else
            {
                foreach (EventBase availableEvent in availableEvents)
                {
                    // todo picking by probability
                }
                Debug.Log ("TODO: load event");
            }
        }

        // todo
		//return new Event_4();
        return null;
    }

    public void handleEvent(EventBase e)
    {
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

    internal void startRandomStarMapEvent(AllDoneDelegate callback)
    {
        allDoneCallback = callback;
        handleEvent(pickEvent());
    }
}
