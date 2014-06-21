using UnityEngine;
using System.Collections;

public class EventChoicesBtn : MonoBehaviour
{
    public EventUI.ChoiceDelegate callback;
    public int choice = 0;

    void OnClick()
    {
        callback(choice);
    }

    /*
        public int choice;
        private EventManager eventManager;
        public UIWidget choiceTxt;

        // Use this for initialization
        void Awake()
        {
            eventManager = GameObject.Find("ScriptHolder").gameObject.GetComponent<EventManager>();
            if (choiceTxt == null) Debug.LogError("GAMEOBJECT not manually set: 'choiceTxt'.");

            //Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
            //if (allChildren[1].gameObject.name == "eventChoiceTxt") choiceTxt = allChildren[1].gameObject.GetComponent<UIWidget>();
            //else Debug.LogError("ERROR: couldn't obtain child 'eventChoiceTxt' for choice '"+choice+". (child count: "+allChildren.Length);
        }

        public void recommend(bool on)
        {
            if (on)
            {
                //Debug.Log(" 	> turning choice "+choice+" off.");
                choiceTxt.color = new Color(0.0F, 0.4F, 0.7F, 1F);
            }
            else
            {
                //Debug.Log(" 	> turning choice "+choice+" ON.");
                choiceTxt.color = new Color(0.0F, 0.0F, 0.0F, 1F);
            }
        }
        */
}


