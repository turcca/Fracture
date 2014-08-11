using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager
{
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

    public void queryLocationEvents(AllDoneDelegate callback)
    {
        allDoneCallback = callback;
        handleEvent(pickEvent());
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

            foreach (EventBase availableEvent in availableEvents)
            {
                
            }
            // do stuff
        }

        // todo
        return new Event_4();
    }

    public void handleEvent(EventBase e)
    {
        if (!GameState.requestState(GameState.State.Event))
        {
            return;
        }

        EventUI eventUI = GameObject.Find("EventCanvas").GetComponent<EventUI>();
        eventUI.setEvent(e, new EventUI.EventDoneDelegate(eventDone));
    }

    public void eventDone()
    {
        GameState.returnFromState(GameState.State.Event);
        
        // loop through all events in queue?
        if (allDoneCallback != null)
        {
            allDoneCallback();
            allDoneCallback = null;
        }
    }
}
