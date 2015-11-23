using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventUI : MonoBehaviour
{
    public delegate void EventDoneDelegate();
    public Text eventText;
    public GameObject choiceButtonGrid;
    public GameObject eventPage;

    EventBase currentEvent;
    EventBase defaultAdvice;
    EventDoneDelegate eventDoneCallback;
    Dictionary<int, EventChoicesBtn> choiceButtons = new Dictionary<int, EventChoicesBtn>();
    GameObject choiceButtonPrefab;
    GameObject advisorNode;

    public EventUI()
    {
    }

    void Awake()
    {
        choiceButtonPrefab = Resources.Load<GameObject>("event/ui/EventChoice");
    }

    void Start()
    {
        defaultAdvice = Root.game.events.getEventByName("default_advice");
    }

    public void setEvent(EventBase e, EventDoneDelegate callback)
    {
        eventDoneCallback = callback;
        currentEvent = e;
        startEvent();
    }
    public void loadLocationAdviceEvent(EventBase e)
    {
        if (Root.game.player.getLocation() != null)
        {
            currentEvent = e;
            Debug.Log ("Loaded location event: '"+e.name+"'");
        }
        else currentEvent = null;

        setupAdvisorManager();
    }

    public void eventChoicePicked(int i)
    {
        Debug.Log("choice: " + i.ToString());

        currentEvent.choice = i;
        currentEvent.doOutcome();
        if (currentEvent.running)
        {
            continueEvent();
        }
        else
        {
            endEvent();
            eventDoneCallback();
            eventDoneCallback = null;
        }
    }

    public string setAdvisor(Character.Job job)
    {
        if (currentEvent == null)
        {
            return "";
        }
        foreach (KeyValuePair<int, EventChoicesBtn> entry in choiceButtons)
        {
            if (currentEvent.getAdvice(job).recommend == entry.Key)
            {
                entry.Value.recommend(true);
            }
            else
            {
                entry.Value.recommend(false);
            }
        }

        string advice = currentEvent.getAdvice(job).text;

        if (advice == "NO ADVICE") 
        {
            advice = defaultAdvice.getAdvice(job).text;
        }

        return advice;
    }

    private void startEvent()
    {
        currentEvent.initPre();
        currentEvent.start();
        continueEvent();
        setupAdvisorManager();
    }

    private void endEvent()
    {
        currentEvent.stop();
        currentEvent = null;
    }


    private void continueEvent()
    {
        // set description
        eventText.text = currentEvent.getText();

        // destroy old entries
        for (int i = choiceButtonGrid.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.DestroyImmediate(choiceButtonGrid.transform.GetChild(i).gameObject);
        }

        // set choices
        int choiceNum = 0;
        choiceButtons.Clear();
        foreach (KeyValuePair<string, int> choice in currentEvent.getChoices())
        {
            choiceNum++;
            drawChoice(choice.Value, choiceNum, choice.Key);
        }
    }

    private void drawChoice(int choiceInternal, int choiceNum, string text)
    {
        GameObject btn = (GameObject)GameObject.Instantiate(choiceButtonPrefab);

        // set relevant choice
        EventChoicesBtn buttonScript = btn.GetComponent<EventChoicesBtn>();
        choiceButtons.Add (choiceInternal, buttonScript);
        buttonScript.callback = new EventChoicesBtn.ChoiceDelegate(eventChoicePicked);
        buttonScript.choice = choiceInternal;
        buttonScript.choiceTxt.text = text;
        buttonScript.choiceNumber.text = choiceNum.ToString() +".";

        btn.transform.SetParent(choiceButtonGrid.transform);
    }





    internal void setupAdvisorManager()
    {
        // figure out if in event or location
        GameObject eventSideWindow = GameObject.Find("SideWindow");
        GameObject locationCanvas = GameObject.Find("LocationCanvas");

        AdvisorManager am = null;

        //if (eventSideWindow.activeSelf && locationCanvas.activeSelf)
        if (eventSideWindow == null && locationCanvas == null)
        {
            Debug.LogError("ERROR: both AdvisorManagers are active: 'SideWindow/' AND 'LocationCanvas/'");
            return;
        }
        // event
        else if (eventSideWindow && eventSideWindow.activeSelf)
        {
            //Debug.Log("setup AdvisorManager / event");
            am = eventSideWindow.GetComponentInChildren<AdvisorManager>();
            if (am) am.setup();
            else Debug.LogError("ERROR: event AdvisorManager not found.");
        }
        // location
        else if (locationCanvas && locationCanvas.activeSelf)
        {
            //Debug.Log("setup AdvisorManager / location");
            am = locationCanvas.GetComponentInChildren<AdvisorManager>();
            if (am) am.setup();
            else Debug.LogError("ERROR: location AdvisorManager not found.");
        }
        else
        {
            Debug.LogError("ERROR: no AdvisorManager are active: 'SideWindow/' AND 'LocationCanvas/'");
            return;
        }
    }
}
