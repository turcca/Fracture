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
        //advisorNode = gameObject.transform.FindChild("eventCharacterElement").gameObject;
        eventPage.SetActive(false);
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
        return "";
        //if (currentEvent == null)
        //{
        //    return "";
        //}

        ////advisorNode.transform.FindChild("eventCharacterAdvice").GetComponent<UILabel>().text =
        ////    currentEvent.getAdvice(job).text;
        //foreach (KeyValuePair<int, EventChoicesBtn> entry in choiceButtons)
        //{
        //    if (currentEvent.getAdvice(job).recommend == entry.Key)
        //    {
        //        entry.Value.recommend(true);
        //    }
        //    else
        //    {
        //        entry.Value.recommend(false);
        //    }
        //}
        ////advisorNode.transform.FindChild("eventCharacterPortrait").GetComponent<UITexture>().mainTexture =
        ////    Game.PortraitManager.getPortraitTexture(Game.getUniverse().player.getCharacter(job).getPortrait().id);
        ////advisorNode.transform.FindChild("eventCharacterData").GetComponent<UILabel>().text =
        ////    Game.getUniverse().player.getCharacter(job).getStats();

        //return currentEvent.getAdvice(job).text;
    }

    private void startEvent()
    {
        eventPage.SetActive(true);
        currentEvent.initPre();
        currentEvent.start();
        continueEvent();
    }
    private void endEvent()
    {
        currentEvent.stop();
        currentEvent = null;
        eventPage.SetActive(false);
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
        //choiceButtons[num] = btn.GetComponent<EventChoicesBtn>();

        btn.GetComponent<Text>().text = text;

        // set relevant choice
        EventChoicesBtn buttonScript = btn.GetComponent<EventChoicesBtn>();
        buttonScript.callback = new EventChoicesBtn.ChoiceDelegate(eventChoicePicked);
        buttonScript.choice = num;

        btn.transform.parent = choiceButtonGrid.transform;
    }
}
