using UnityEngine;
using System.Collections;

public class LocationDiplomacyMenu : MonoBehaviour {

    Location location;
    GameObject buttonPrefab;
    GameObject grid;

	// Use this for initialization
	void Start ()
    {
        location = Game.getUniverse().locations[Game.getUniverse().player.getLocationId()];
        buttonPrefab = Resources.Load<GameObject>("ui/prefabs/locationMenuButton");
        grid = transform.FindChild("grid").gameObject;
        buildMenuItems();
	}

    private void buildMenuItems()
    {
        foreach (var entry in location.faction.control)
        {
            //Tools.debug(entry.Key + " read!");
            if (entry.Value > 0.01)
            {
                addMenuItem(entry.Key, entry.Value);
            }
        }
    }
    private void addMenuItem(string faction, float control)
    {
        //GameObject go = NGUITools.AddChild(grid, buttonPrefab);
        //go.GetComponent<UILabel>().text = getAppointmentLabel(faction, getOfficeLevel(control));
        //go.GetComponent<LocationButtonCallback>().param = faction;
        //go.GetComponent<LocationButtonCallback>().callback = new LocationButtonCallback.CallbackDelegate(pickFaction);
    }
    private int getOfficeLevel(float c)
    {
        // office level from faction control value
        if (c <= 0.12) return 1;
        else if (c <= 0.3) return 2;
        else if (c <= 0.5) return 3;
        else if (c <= 0.8) return 4;
        else return 5;
    }   
    private string getAppointmentLabel(string faction, int officeLevel)
    {
        string name = faction + " ";
            
        // office level from faction control value
        //if (officeLevel == 5) name += WorldState.faction[f.id].names[5];    // official (Senator, Councillor, Bishop, Minister...)
        if (officeLevel == 5) name += "TODOSenatorTODO";
        else if (officeLevel == 4) name += "Administrator";
        else if (officeLevel == 3) name += "Office";
        else if (officeLevel == 2) name += "Representative";
        else if (officeLevel == 1) name += "Contact";
        else Debug.LogError("input was " + officeLevel);
        
        return name;
    
    }

    void pickFaction(string faction)
    {
        GameObject.Find("LocationSceneState").GetComponent<LocationSceneState>().startDiplomacyEvent(faction);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
