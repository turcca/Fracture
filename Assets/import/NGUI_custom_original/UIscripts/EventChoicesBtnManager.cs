using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventChoicesBtnManager : MonoBehaviour
{
    /*
    Dictionary<string, int> choices; // ("choice text player can click", int: internal number)
    List<EventChoicesBtn> choicesBtnList;
    //private Events events;
    private EventUI eventUI;
    public static EventChoicesBtnManager Instance; // Gives out references to the only EventChoicesBtnManager script object

    public GameObject choiceButton;	// prefab for portraits

    // Initialize own fields
    void Awake()
    {
        EventChoicesBtnManager.Instance = this;
        choices = new Dictionary<string, int>();
        choicesBtnList = new List<EventChoicesBtn>();
        //events = GameObject.Find("ScriptHolder").gameObject.GetComponent("Events") as Events;
        eventUI = GameObject.Find("eventUI").gameObject.GetComponent("EventUI") as EventUI;
        choiceButton = Resources.Load<GameObject>("Prefabs/UI_elements/eventChoiceBtn");
        if (choiceButton == null) Debug.LogError("UI ERROR: no 'eventChoiceBtn' loaded from resources!");
    }


    // eventCharacters(true) starts it, (false) ends it

    void enable(bool activate)
    {
        // delete all excisting objects (portrait buttons) under this object (PortraitGrid)
        Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
        int count = allChildren.Length;
        if (count > 1)
        {
            for (int i = 1; i < count; i++) 	// skip first, because it's 'this' object itself
            {
                Destroy(allChildren[i].gameObject);
            }
        }
        choicesBtnList.Clear();

        // create prefab buttons for each character in list
        if (activate)
        {
            createChoiceButtons();
        }
        // reposition grid
        //SendMessage("Reposition");
        this.gameObject.GetComponent<UIGrid>().repositionNow = true;
    }

    // *********************************************************************
    // private support methods


    private void createChoiceButtons()	// 
    {
        eventClass e = eventUI.currentEvent;
        if (e == null) { Debug.LogError("ERROR: creating choice-buttons for event failed, because EventUI.currentEvent = null."); return; }
        // load choices
        choices = e.getChoices();
        int i = 0;
        // create prefab button for each character
        foreach (string choice in choices.Keys)
        {
            i++;
            //Debug.Log("Creating: "+c.name);
            // create button as child
            GameObject btn = NGUITools.AddChild(this.gameObject, choiceButton);

            // set  values
            // 

            // set text fields
            UILabel[] allChildren = btn.GetComponentsInChildren<UILabel>();
            for (int n = 0; n < allChildren.Length; n++)
            {
                // number
                if (n == 0) allChildren[n].text = i + ".";
                // txt
                if (n == 1) allChildren[n].text = choice;
            }

            // set key binding (AlphaN)
            UIKeyBinding key = btn.GetComponent("UIKeyBinding") as UIKeyBinding;
            key.keyCode = getKeyAlpha(i);

            // set relevant choice
            EventChoicesBtn button = btn.GetComponent("EventChoicesBtn") as EventChoicesBtn;
            button.choice = choices[choice];

            // add button to choicesBtnList
            choicesBtnList.Add(button);
        }

    }

    public void setRecommendedChoice(int recommend)
    {
        foreach (EventChoicesBtn btn in choicesBtnList)
        {
            if (btn.choice == recommend && recommend != 0) btn.recommend(true);
            else btn.recommend(false);
        }
    }

    private KeyCode getKeyAlpha(int i)
    {
        if (i == 1) return KeyCode.Alpha1;
        if (i == 2) return KeyCode.Alpha2;
        if (i == 3) return KeyCode.Alpha3;
        if (i == 4) return KeyCode.Alpha4;
        if (i == 5) return KeyCode.Alpha5;
        else
        {
            Debug.LogError("ERROR: over 5 choices - need to expand key bindings...");
            return KeyCode.Alpha6;
        }
    }
    */
}
