using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FactionMeetPanel : MonoBehaviour
{
    public delegate void FactionSelectedDelegate(string faction);
    public string faction;
    public string location;
    public Image factionImage;
    public Text meetDesc;

    private FactionSelectedDelegate callback;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setup(string trackLocation, string trackFaction, FactionSelectedDelegate cb)
    {
        location = trackLocation;
        faction = trackFaction;
        callback = cb;

        updateImage();
        updateInfo();
    }

    private void updateInfo()
    {
        meetDesc.text = Faction.getFactionName(faction) + "\n" +
                        Faction.getTitle(faction) + " " + Game.universe.locations[location].faction.ruler[faction];
    }

    private void updateImage()
    {
        Dictionary<string, string> factionLogoMap = new Dictionary<string,string>
        {
            {"church", "Logo_Faction_Church"},
            {"cult", "Logo_Faction_Heretics"},
            {"noble1", "Logo_Faction_Furia"},
            {"noble2", "Logo_Faction_Rathmund"},
            {"noble3", "Logo_Faction_Valeria"},
            {"noble4", "Logo_Faction_Tarquinia"},
            {"guild1", "Logo_Faction_Union"},
            {"guild2", "Logo_Faction_Dacei"},
            {"guild3", "Logo_Faction_Caruna Cartel"}
        };

        factionImage.sprite = Resources.Load<Sprite>("ui/factions/" + factionLogoMap[faction]);
        Tools.debug("faction set!");
    }

    public void click()
    {
        callback(faction);
    }

    //private int getOfficeLevel(float c)
    //{
    //    // office level from faction control value
    //    if (c <= 0.12) return 1;
    //    else if (c <= 0.3) return 2;
    //    else if (c <= 0.5) return 3;
    //    else if (c <= 0.8) return 4;
    //    else return 5;
    //}
    //private string getAppointmentLabel(string faction, int officeLevel)
    //{
    //    string name = faction + " ";

    //    // office level from faction control value
    //    //if (officeLevel == 5) name += WorldState.faction[f.id].names[5];    // official (Senator, Councillor, Bishop, Minister...)
    //    if (officeLevel == 5) name += "TODOSenatorTODO";
    //    else if (officeLevel == 4) name += "Administrator";
    //    else if (officeLevel == 3) name += "Office";
    //    else if (officeLevel == 2) name += "Representative";
    //    else if (officeLevel == 1) name += "Contact";
    //    else Debug.LogError("input was " + officeLevel);

    //    return name;
    //}
}
