using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventUI : MonoBehaviour
{
    public delegate void EventDoneDelegate();

    EventBase currentEvent;
    EventDoneDelegate eventDoneCallback;

    UILabel eventDescriptionLabel;
    Dictionary<int, EventChoicesBtn> choiceButtons = new Dictionary<int, EventChoicesBtn>();
    GameObject choiceButtonNode;
    GameObject choiceButtonPrefab;
    GameObject advisorNode;

    public EventUI()
    {
    }

    void Awake()
    {
        eventDescriptionLabel = gameObject.transform.FindChild("eventTxtElement").FindChild("eventDescriptionTxt").GetComponent<UILabel>();
        choiceButtonNode = gameObject.transform.FindChild("eventTxtElement").FindChild("eventChoices").gameObject;
        choiceButtonPrefab = Resources.Load<GameObject>("ui/prefabs/event_choice_button");
        advisorNode = gameObject.transform.FindChild("eventCharacterElement").gameObject;
    }

    void Start()
    {
    }

    public void setEvent(EventBase e, EventDoneDelegate callback)
    {
        eventDoneCallback = callback;
        currentEvent = e;
        currentEvent.initPre();
        currentEvent.start();
        continueEvent();
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
            clearEvent();
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

        //advisorNode.transform.FindChild("eventCharacterAdvice").GetComponent<UILabel>().text =
        //    currentEvent.getAdvice(job).text;
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
        //advisorNode.transform.FindChild("eventCharacterPortrait").GetComponent<UITexture>().mainTexture =
        //    Game.PortraitManager.getPortraitTexture(Game.getUniverse().player.getCharacter(job).getPortrait().id);
        //advisorNode.transform.FindChild("eventCharacterData").GetComponent<UILabel>().text =
        //    Game.getUniverse().player.getCharacter(job).getStats();

        return currentEvent.getAdvice(job).text;
    }

    private void clearEvent()
    {
        currentEvent.stop();
        currentEvent = null;
    }

    private void continueEvent()
    {
        // set description
        eventDescriptionLabel.text = currentEvent.getText();

        // destroy old entries
        for (int i = choiceButtonNode.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.DestroyImmediate(choiceButtonNode.transform.GetChild(i).gameObject);
        }

        // set choices
        int choiceNum = 0;
        choiceButtons.Clear();
        foreach (KeyValuePair<string, int> choice in currentEvent.getChoices())
        {
            choiceNum++;
            // create button as child
            GameObject btn = NGUITools.AddChild(choiceButtonNode, choiceButtonPrefab);
            choiceButtons[choiceNum] = btn.GetComponent<EventChoicesBtn>();

            // set text fields
            UILabel[] allChildren = btn.GetComponentsInChildren<UILabel>();
            allChildren[0].text = choiceNum + ".";
            allChildren[1].text = choice.Key;

            // set key binding (AlphaN)
            UIKeyBinding key = btn.GetComponent<UIKeyBinding>();
            key.keyCode = Tools.getKeyAlpha(choiceNum);

            // set relevant choice
            EventChoicesBtn buttonScript = btn.GetComponent<EventChoicesBtn>();
            buttonScript.callback = new EventChoicesBtn.ChoiceDelegate(eventChoicePicked);
            buttonScript.choice = choice.Value;
        }
        choiceButtonNode.GetComponent<UIGrid>().Reposition();
    }


}
