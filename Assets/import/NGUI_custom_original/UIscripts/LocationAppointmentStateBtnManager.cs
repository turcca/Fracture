using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocationAppointmentStateBtnManager : MonoBehaviour
{
/*
    public float officeTreshold = 0.05f;

    public static LocationAppointmentStateBtnManager Instance; // Gives out references to the only LocationAppointmentStateBtnManager script object
    public UIGrid uiGrid;
    public GameObject locationStateBtn;	// prefab for location state changes


    // Use this for initialization
    void Awake()
    {
        LocationAppointmentStateBtnManager.Instance = this;
        if (uiGrid == null) uiGrid = this.gameObject.GetComponent<UIGrid>();
        if (locationStateBtn == null) locationStateBtn = Resources.Load<GameObject>("Prefabs/UI_elements/locationStateBtn");
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

        // if activating: create prefab buttons for each character in list
        if (activate)
        {
            createAppointmentButtonsForLocation();
        }
        // if disabling
        else
        {

        }

        // reposition grid
        uiGrid.repositionNow = true;
    }


    void createAppointmentButtonsForLocation()
    {
        int i = 0;
        int nth = 0;
        // create prefab button for each character
        foreach (factionclass f in LocationUI.Instance.currentLocation.faction)
        {
            // LOCAL GOVERNMENT (no controlling faction)
            // check if first faction/ideology isn't faction but a local government
            if (i == 0 && !f.ctrl && LocationUI.Instance.currentLocation.controlledBy == null)
            {
                // create button as child
                GameObject btn = NGUITools.AddChild(this.gameObject, locationStateBtn);
                nth++;
                // set  values

                // set state and faction
                int officeLevel = getOfficeLevel(f);
                LocationStateBtn button = btn.GetComponent<LocationStateBtn>();
                button.loadStateAndFaction(locationState.Appointment, nth, null, 0, officeLevel);

                // set button label text
                btn.GetComponentInChildren<UILabel>().text = getAppointmentLabel(f);
            }



            // FACTION BUTTONS
            // if ctrl == true, it's a faction & create a button
            else if (f.ctrl && f.val >= officeTreshold)
            {

                //Debug.Log("Creating: "+f.id);

                // create button as child
                GameObject btn = NGUITools.AddChild(this.gameObject, locationStateBtn);
                nth++;
                // set  values
                // sprite
                //UISprite sprite = btn.GetComponent("UISprite") as UISprite;
                //sprite.spriteName = c.portraitName;
                int officeLevel = getOfficeLevel(f);

                // set state and faction
                LocationStateBtn button = btn.GetComponent<LocationStateBtn>();
                button.loadStateAndFaction(locationState.Appointment, nth, f.id, f.val, officeLevel);

                // set button label text
                btn.GetComponentInChildren<UILabel>().text = getAppointmentLabel(f, officeLevel);

            }
            i++;
        }
    }
    int getOfficeLevel(factionclass f)
    {
        // office level from faction control value
        if (f.val <= 0.12) return 1;
        else if (f.val <= 0.3) return 2;
        else if (f.val <= 0.5) return 3;
        else if (f.val <= 0.8) return 4;
        else return 5;
    }

    string getAppointmentLabel(factionclass f, int officeLevel = 0)
    {
        string name = "";

        // if faction
        if (f.ctrl)
        {
            // faction name
            name += WorldState.faction[f.id].names[0] + " ";

            // office level from faction control value
            if (officeLevel == 5) name += WorldState.faction[f.id].names[5];	// official (Senator, Councillor, Bishop, Minister...)
            else if (officeLevel == 4) name += "Administrator";
            else if (officeLevel == 3) name += "Office";
            else if (officeLevel == 2) name += "Representative";
            else if (officeLevel == 1) name += "Contact";

            else Debug.LogError("input was " + officeLevel);

            return name;
        }
        // not a faction
        return "Local Government Office";
    }
 */
}
