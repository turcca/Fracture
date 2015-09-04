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

        return currentEvent.getAdvice(job).text;
    }

    private void startEvent()
    {
        currentEvent.initPre();
        currentEvent.start();
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
            drawChoice(choice.Value, choiceNum.ToString() + ". " + choice.Key);
        }
    }

    private void drawChoice(int num, string text)
    {
        GameObject btn = (GameObject)GameObject.Instantiate(choiceButtonPrefab);
        btn.GetComponent<Text>().text = text;

        // set relevant choice
        EventChoicesBtn buttonScript = btn.GetComponent<EventChoicesBtn>();
        buttonScript.callback = new EventChoicesBtn.ChoiceDelegate(eventChoicePicked);
        buttonScript.choice = num;

        btn.transform.parent = choiceButtonGrid.transform;
    }
}
