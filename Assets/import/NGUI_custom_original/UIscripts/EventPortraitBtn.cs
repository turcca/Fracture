using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventPortraitBtn : MonoBehaviour
{
    /*
    public characterClass c;

    public bool isAvailableInEvent;

    public UILabel data;
    public UISprite portrait;
    public adviceClass advice;
    public GameObject adviceFrame;

    public bool isLocationPortrait;

    private Characters characters;

    private EventPortraitBtnManager manager;
    private EventManager eventManager;

    public List<EventPortraitBtn> activePortraitBtnList;

    private string nl() { return "\n"; }

    void Awake ()
    {
        isAvailableInEvent = true;
        characters = GameObject.Find("ScriptHolder").gameObject.GetComponent<Characters>();
        eventManager = GameObject.Find("ScriptHolder").gameObject.GetComponent<EventManager>();
        activePortraitBtnList = new List<EventPortraitBtn>();
        advice = new adviceClass();

        if (adviceFrame == null) //Debug.LogError("GAMEOBJECT not manually set: 'adviceFrame'.");
        {
            Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
            if (allChildren[1].gameObject.name == "adviceFrame") adviceFrame = allChildren[1].gameObject;
            else Debug.LogError("ERROR: couldn't obtain child 'adviceFrame' for '"+c.name+". (child count: "+allChildren.Length);
        }
    }
    void Start ()
    {
        setManager();
    } 
	
    public void setManager()
    {	
        initPortraitData();
    }

    // called when clicked
    void OnClick ()
    {
		
        // select character and handle advice
        if (isAvailableInEvent)
        {
            characters.selectedCharacter = c.name;
            EventPortraitBtnManager.Instance.selected = c;
            LocationPortraitBtnManager.Instance.selected = c;

            if (activePortraitBtnList.Count == 0) Debug.LogWarning("WARNING: no characters to activePortraitBtnList");

            // set recommended choice if "first run" of the event (doesn't have recommendations for more than the first choice)
            if (eventManager.firstRun) EventChoicesBtnManager.Instance.setRecommendedChoice(this.advice.recommend);

            // load advice
            foreach (EventPortraitBtn btn in activePortraitBtnList)
            {
                if (btn == this)
                {
                    // turn this advice ON
                    btn.loadCharacterAdvice(true);
                }
                else
                {
                    // turn other advices OFF
                    btn.loadCharacterAdvice(false);
                }
            }

        }

        // load character data
        loadCharacterData();
	
    }

    public void loadCharacterAdvice(bool onOff)
    {
        // location
        if (this.isLocationPortrait) adviceFrame.SetActive(onOff);
        // event
        else if (onOff && c.isActive) EventPortraitBtnManager.Instance.characterAdvice.text = this.advice.opinion; // load advice text

        //if (!onOff || (advice.recommend != 0 && advice.opinion != ""))
        //{
        //	adviceFrame.SetActive(onOff);
        //}
        //else Debug.Log("NOTE: empty opinion or recommendation ("+c.name+")");
		
    }

	
    public void loadCharacterData()
    {
        // check that portrait and data are properly initialized
        if (portrait == null || data == null) initPortraitData();

        // gather character data
        string t = "[000000]";
        if (!isAvailableInEvent) { t+= "	* UNAVAILABLE * [777777] " +nl() +nl(); }

        t+= c.name+", "+c.assignment +nl();		// name, assignment
        t+= "("+c.ideology+")" +nl();			// (ideology)
        t+= nl();

        // add skills
        foreach (string skill in WorldState.skillNames)
        {
            string s = c.skillLevel(skill);
            if (s != null) t+= s + nl();
        }

        // input data
        data.text = t;

        // change portrait
        portrait.spriteName = c.portraitName;
    }

    void initPortraitData ()
    {
        // check opinion for advice-tags
        if (advice != null) this.advice.opinion = EventAdviceTags.resolveTags( this.advice.opinion, c );
        else Debug.LogError("ERROR: advice = null");

        //GameObject parent = this.transform.parent.gameObject;
        // event
        if (!isLocationPortrait parent.name == "PortraitGrid") 
        { 
            //isLocationPortrait = false;
            activePortraitBtnList = EventPortraitBtnManager.Instance.activePortraitBtnList; 
            data = GameObject.Find("eventCharacterData").GetComponent<UILabel>();
            portrait = GameObject.Find("eventCharacterPortrait").GetComponent<UISprite>();
            // no popup-advice  
            adviceFrame.SetActive(false);
            //adviceFrame.GetComponent<UISprite>().enabled = false;
        }
        // location
        else if (parent.name == "LocationPortraitGrid")
        { 
            //isLocationPortrait = true;
            activePortraitBtnList = LocationPortraitBtnManager.Instance.activePortraitBtnList;
            data = GameObject.Find("locationCharacterData").GetComponent<UILabel>();
            portrait = GameObject.Find("locationCharacterPortrait").GetComponent<UISprite>();
        }

        //else Debug.LogError("ERROR: portrait button wasn't initialized for a proper parent.activePortraitBtnList");
    }
     */
}