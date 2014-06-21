using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// **************************
// attached to PoirtraitGrid
// **************************

public class EventPortraitBtnManager : MonoBehaviour
{

/*
    public List<EventPortraitBtn> activePortraitBtnList;
    //public characterClass selected;
    //private Characters characters;
    private EventUI eventUI;
    private UIGrid uiGrid;
    //public static EventPortraitBtnManager Instance; // Gives out references to the only EventPortraitBtnManager script object

    public UIGrid portraitButtonGrid;

    public GameObject portraitButton;	// prefab for portraits
    public UILabel characterAdvice;		// text panel where advice is printed / shared between advisors (popup -style at locations)

    // Initialize own fields
    void Awake ()
    {
//        EventPortraitBtnManager.Instance = this;
        activePortraitBtnList = new List<EventPortraitBtn>();
//        characters = GameObject.Find("ScriptHolder").gameObject.GetComponent<Characters>();
        eventUI = GameObject.Find("eventUI").GetComponent<EventUI>();
        uiGrid = this.gameObject.GetComponent<UIGrid>();
        if (portraitButton == null) portraitButton = Resources.Load<GameObject>("Prefabs/UI_elements/portraitBtn");
        if (portraitButton == null) Debug.LogError("UI ERROR: no 'portraitBtn' loaded from resources!");
        if (characterAdvice == null) characterAdvice = GameObject.Find("eventCharacterAdvice").GetComponent<UILabel>();
        if (portraitButtonGrid == null) Debug.LogError("UI ERROR: no 'LocationPortraitGrid / portraitButtonGrid (UIGrid)' manually set!");
		
        //selected = null;
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
        activePortraitBtnList.Clear();
        // if activating: create prefab buttons for each character in list
        if (activate)
        {
            createPortraitButtonsForEvent();
        }
        // if disabling
        else
        {
        //    selected = null;
        //    characters.selectedCharacter = null;
        }
        // reposition grid
        uiGrid.repositionNow = true;
    }

    // *********************************************************************
    // private support methods

    private void createPortraitButtonsForEvent()	// add portrait sizes? small : 45 * 66
    {
        // check if selected character excists from previous event round
            if (selected != null && !characters.character.ContainsKey(selected.name) ) selected = null;
            // if clean start
            else 
            {
                // see if there is character defined in the event, try to select that
                if (eventUI.currentEvent.character != null && characters.character.ContainsKey(eventUI.currentEvent.character)) 
                {
                    selected = characters.character[eventUI.currentEvent.character];
                    characters.selectedCharacter = eventUI.currentEvent.character;
                }
                // otherwise will select first available character below
            }

        characterClass c = null;
        int i = 0;

        // create prefab button for each character
        foreach (string job in WorldState.assignmentNames)
        {
            // if not an empty name on the job
            if (characters.assignment[job] != null)
            {
                i++;
                if (characters.character.ContainsKey(characters.assignment[job])) 
                    c = characters.character[characters.assignment[job]]; 
                else Debug.LogError("ERROR: checking through jobs returned invalid character dictionary entry.");
                //Debug.Log("Creating: "+c.name);

                // create button as child
                GameObject btn = NGUITools.AddChild(this.gameObject, portraitButton);

                // set  values
                    // object name is character name
                    //btn.name = c.name;
				 	
                    // sprite
                    UISprite sprite = btn.GetComponent("UISprite") as UISprite;
                    sprite.spriteName = c.portraitName;
				 	
                    // set characterClass
                    EventPortraitBtn button = btn.GetComponent<EventPortraitBtn>() ;
                    button.c = c;
                    button.isLocationPortrait = false;

                    // set activity: if character is not 'isActive', or it's not a dedicated event character
                    if (!c.isActive || (eventUI.currentEvent.character != null && eventUI.currentEvent.character != c.name) ) button.isAvailableInEvent = false;
                    else 
                    {
                        button.isAvailableInEvent = true;
                        activePortraitBtnList.Add(button);
                    }
				 	
                    // set button colours (main colour reads from widget)
                        UIWidget b = btn.GetComponent<UIWidget>();
                        if (button.isAvailableInEvent)
                        {
                            // active character - leave defaults
                        }
                        else
                        {
                            // inactive character - make dark
                            b.color = new Color(0.3F, 0.3F, 0.3F, 1F);
                        }
				 	
                    // set keyBinding
                    UIKeyBinding key = btn.GetComponent("UIKeyBinding") as UIKeyBinding;
                    key.keyCode = getKeyFunction(i);

                    // find UILabel in children, and pass the advice to it
                    if (button.isAvailableInEvent)
                    { 
                        if (eventUI.advice.ContainsKey(button.c.name))
                        {
                            button.advice.opinion = "\""+ eventUI.advice[button.c.name].opinion +"\"";
                            button.advice.recommend = eventUI.advice[button.c.name].recommend;
                            //btn.GetComponentInChildren<UILabel>().text = button.advice.opinion; // for popups --> as is, moved to selected -section
							
                            //Debug.Log("Debugging, key: '"+button.c.name+"' has advice: '"+button.advice.recommend);

                        }
                        else 
                        {
                            button.advice.opinion = "";
                            button.advice.recommend = 0;
                            Debug.Log("Debugging, key: '"+button.c.name+"' doesn't excist in eventUI.advice.");
                        }
                    }

                 // select this, if it was selected on previous round OR just select default portrait if null selected
                 if ((selected == c) || (selected == null && button.isAvailableInEvent) )
                 { 
                    selected = c;
                    characters.selectedCharacter = c.name;
                    button.loadCharacterAdvice(true);
                    // load advisor data
                    button.loadCharacterData();
                    // load advice
                    characterAdvice.text = button.advice.opinion;
                    EventChoicesBtnManager.Instance.setRecommendedChoice(button.advice.recommend);
                 }
                 // else set advice off
                 else 
                 {
                    button.loadCharacterAdvice(false);
                 }
            }
        }
        // check selected
        if (selected == null)
        {
            Debug.Log("NOTE: no character is selected in event! This may mean all of them are inactive!");
            GameObject.Find("eventCharacterPortrait").GetComponent<UISprite>().spriteName = null;
        }

        // set gameObject position by portrait count (grid)
        setPositionByPortraitCount();
    }

    void setPositionByPortraitCount () // centers horizontally
    {
        // count portraits
        int count = activePortraitBtnList.Count;
        // get portrait width on the grid
        float cellWidth = portraitButtonGrid.cellWidth;

        // move this grid object position from the left half the portrait bar's length
        this.gameObject.transform.localPosition = new Vector3( (-cellWidth * count /2)+ cellWidth/2, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);

        //Debug.Log("re-positioning portrait grid. x: "+this.gameObject.transform.localPosition.x);

    }
*/
}


