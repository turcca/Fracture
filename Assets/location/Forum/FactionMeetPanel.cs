﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FactionMeetPanel : MonoBehaviour
{
    public delegate void FactionSelectedDelegate(string faction);
    public Faction.FactionID faction;
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

    public void setup(string trackLocation, Faction.FactionID trackFaction, FactionSelectedDelegate cb)
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
            getContactTitle() + " ";// + 
            //Root.game.locations[location].features.ruler[Faction.factionToEnum(faction)];
    }

    private void updateImage()
    {
        Dictionary<Faction.FactionID, string> factionLogoMap = new Dictionary<Faction.FactionID,string>
        {
            {Faction.FactionID.noble1, "Logo_Faction_Church"},
            {Faction.FactionID.noble2, "Logo_Faction_Heretics"},
            {Faction.FactionID.noble3, "Logo_Faction_Furia"},
            {Faction.FactionID.noble4, "Logo_Faction_Rathmund"},
            {Faction.FactionID.guild1, "Logo_Faction_Valeria"},
            {Faction.FactionID.guild2, "Logo_Faction_Tarquinia"},
            {Faction.FactionID.guild3, "Logo_Faction_Union"},
            {Faction.FactionID.church, "Logo_Faction_Dacei"},
            {Faction.FactionID.heretic, "Logo_Faction_Caruna Cartel"}
        };

        factionImage.sprite = Resources.Load<Sprite>("ui/factions/" + factionLogoMap[faction]);
        Tools.debug("faction set!");
    }

    public void click()
    {
        Debug.Log ("debug callback: "+faction);
        callback(Faction.factionToString(faction));
    }
    public string getContactTitle()
    {
        // office level from faction control value
        float c = Root.game.locations[location].features.factionCtrl[faction];

        if (c <= 0.12) return "Contact";
        else if (c <= 0.3) return "Representative";
        else if (c <= 0.5) return "Office";
        else if (c <= 0.8) return "Administrator";
        else return "Administrator";
    }

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
