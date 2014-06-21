using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager
{
    public delegate void EventDoneDelegate();

    private GameObject eventUINode;
    private List<EventBase> eventPool = new List<EventBase>();
    private float daysSinceLastEvent = 0;

    //List<KeyValuePair<int, EventBase>> eventQueue;

    public EventManager()
    {
    }

    public void tick(float days)
    {
        daysSinceLastEvent += days;

        // todo properly
        if (daysSinceLastEvent > 10.0f)
        {
            pickEvent();
            daysSinceLastEvent = 0;
        }
    }

    public void pickEvent()
    {
        foreach (EventBase e in eventPool)
        {
            e.calculateProbability();
            // do stuff
        }

        handleEvent(new Event_4());
    }

    private void handleEvent(EventBase e)
    {
        if (!GameState.requestState(GameState.State.Event))
        {
            return;
        }

        if (!eventUINode)
        {
            eventUINode = GameObject.Find("eventUI");
        }
        
        EventUI eventUI = eventUINode.GetComponent<EventUI>();

        NGUITools.SetActive(eventUINode, true);

        eventUI.setEvent(e, new EventDoneDelegate(eventDone));
    }

    public void eventDone()
    {
        GameState.returnFromState(GameState.State.Event);
        NGUITools.SetActive(eventUINode, false);
    }
}
