using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventUI : MonoBehaviour
{
    public delegate void EventDoneDelegate();
    public Text eventText;
    public GameObject eventTextField;
    //public GameObject eventPage;

    EventBase currentEvent;
    EventBase defaultAdvice;
    EventDoneDelegate eventDoneCallback;
    Dictionary<int, EventChoicesBtn> choiceButtons = new Dictionary<int, EventChoicesBtn>();
    GameObject choiceButtonPrefab;
    GameObject advisorNode;

    public EventBase DefaultAdvice
    {
        get
        {
            if (defaultAdvice == null)
            {
                defaultAdvice = Root.game.events.getEventByName("default_advice");
                if (defaultAdvice == null) Debug.LogWarning("default_advice -event not found!");
            }
            return defaultAdvice;
        }
    }

    public EventUI()
    {
    }

    void Awake()
    {
        choiceButtonPrefab = Resources.Load<GameObject>("event/ui/EventChoice");
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
            if (e == null) Debug.LogError("current event == null");
            else Debug.Log ("Loaded location event: '"+e.name+"'");
        }
        else currentEvent = null;

        setupAdvisorManager();
    }

    public void eventChoicePicked(int i)
    {
        if (GameState.isState(GameState.State.Pause) == false)
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
        else Debug.Log("PAUSED: no event inputs");
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
            if (DefaultAdvice == null) Debug.LogError("defaultAdvice is null");
            advice = DefaultAdvice.getAdvice(job).text;
        }
        // resolve advice tags
        if (Root.game.player.getAdvisor(job) == null) return advice;
        else return EventAdviceTags.resolveTags(advice, Root.game.player.getAdvisor(job));
    }

    private void startEvent()
    {
        currentEvent.initPre();
        currentEvent.start();
        setupAdvisorManager();
        continueEvent();
    }

    private void endEvent()
    {
        currentEvent.stop();
        currentEvent = null;
    }


    private void continueEvent()
    {
        // set description
        if (eventText == null) { Debug.LogError("eventText == null"); /*eventText = GameObject.Find("EventText").GetComponentInChildren<Text>();*/ }
        else eventText.text = currentEvent.getText();

        // destroy old entries
        foreach (EventChoicesBtn choiseBtn in eventTextField.GetComponentsInChildren<EventChoicesBtn>())
        {
            GameObject.Destroy(choiseBtn.gameObject);
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

        btn.transform.SetParent(eventTextField.transform);
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
        // event (or diplomacy event)
        else if (currentEvent == null) Debug.LogError("locationAdvice == null");
        else if (GameState.isState(GameState.State.Event) || (currentEvent != null && currentEvent.name.StartsWith("contact_")))
        {
            //Debug.Log("setup AdvisorManager / event");
            if (eventSideWindow)
            {
                am = eventSideWindow.GetComponentInChildren<AdvisorManager>();
                if (am) am.setup();
                else Debug.LogError("ERROR: event AdvisorManager not found.");
            }
            else Debug.LogError("No 'SideWindow' found");
        }
        // location
        //else if (locationCanvas && locationCanvas.activeSelf)
        else if (GameState.isState(GameState.State.Location))
        {
            // diplomacy
            if (currentEvent.name.StartsWith("contact_"))
            {

            }

            else
            {
                //Debug.Log("setup AdvisorManager / location");
                am = locationCanvas.GetComponentInChildren<AdvisorManager>();
                if (am) am.setup();
                else Debug.LogError("ERROR: location AdvisorManager not found.");
            }
        }
        else
        {
            Debug.LogError("ERROR: Event Fallthrough. no AdvisorManager are active: 'SideWindow/' AND 'LocationCanvas/'  [GameState: "+GameState.getState().ToString()+"]");
            return;
        }
    }
}
