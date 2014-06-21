using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventUI : MonoBehaviour
{
    public delegate void ChoiceDelegate(int i);

    EventBase currentEvent;
    EventManager.EventDoneDelegate callbackAllDone;

    UILabel eventDescriptionLabel;
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

    public void setEvent(EventBase e, EventManager.EventDoneDelegate callback)
    {
        callbackAllDone = callback;
        currentEvent = e;

        // set description
        eventDescriptionLabel.text = currentEvent.getText();

        // set choices
        int i = 0;
        // create prefab button for each character
        foreach (KeyValuePair<string, int> choice in currentEvent.choices)
        {
            i++;
            // create button as child
            GameObject btn = NGUITools.AddChild(choiceButtonNode, choiceButtonPrefab);

            // set text fields
            UILabel[] allChildren = btn.GetComponentsInChildren<UILabel>();
            allChildren[0].text = i + ".";
            allChildren[1].text = choice.Key;

            // set key binding (AlphaN)
            UIKeyBinding key = btn.GetComponent<UIKeyBinding>();
            key.keyCode = Tools.getKeyAlpha(i);
            
            // set relevant choice
            EventChoicesBtn buttonScript = btn.GetComponent<EventChoicesBtn>();
            buttonScript.callback = new ChoiceDelegate(eventChoicePicked);
            buttonScript.choice = choice.Value;
        }
        choiceButtonNode.GetComponent<UIGrid>().Reposition();
    }

    public void eventChoicePicked(int i)
    {
        Debug.Log("choice: " + i.ToString());
        currentEvent.choice = i;
        currentEvent.doOutcome();
        //if (currentEvent.ended)
        {
            clearEvent();
        }
        callbackAllDone();
    }

    public void setAdvisor(string pos)
    {
        if (currentEvent == null)
        {
            return;
        }

        advisorNode.transform.FindChild("eventCharacterAdvice").GetComponent<UILabel>().text =
            currentEvent.getAdvice(pos);
        advisorNode.transform.FindChild("eventCharacterPortrait").GetComponent<UISprite>().spriteName =
            Universe.singleton.player.getCharacter(pos).getPortraitId();
        advisorNode.transform.FindChild("eventCharacterData").GetComponent<UILabel>().text =
            Universe.singleton.player.getCharacter(pos).getStats();

    }

    public void clearEvent()
    {
        currentEvent = null;
    }
}
