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
    public Image controllerBorders;
    public Text playerReputation;

    private FactionSelectedDelegate callback;


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
        float ctrl = Mathf.Floor(Root.game.locations[location].features.factionCtrl[faction] * 100f);
        string txt = "";
        // faction name
        txt += Faction.getFactionName(faction) +"\n";
        txt += "<color=#aaaaaa>";
        // contact level (admin, contact...)
        txt += getContactTitle() + " </color>\n";
        // % control
        if (ctrl < 10f) txt += "<color=#666666>";
        else if (ctrl < 50f) txt += "<color=888888>";
        else txt += "<color=lightblue>";
        txt += ctrl.ToString() + "% control </color>";

        meetDesc.text = txt;
        controllerBorders.enabled = ctrl > 50f;

        float rep = (Mathf.Round(Root.game.player.playerReputation.getReputationValue(faction) *10f)/10f);
        playerReputation.text = rep.ToString();
        playerReputation.color = new Color(Mathf.Min((1f - rep/100f), 1f), Mathf.Min((1f + rep/ 100f), 1f), 1f - Mathf.Abs(rep/100f));

        GetComponent<ToolTipScript>().toolTip = Faction.getFactionDescription(faction);
    }

    private void updateImage()
    {
        factionImage.sprite = Faction.getFactionLogo(faction, true);
        //Tools.debug("faction set!");
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

        if (c <= 0.12f) return "Contact "+ Root.game.locations[location].getLocalfactionHeadName(faction);
        else if (c < 0.2f) return "Representative " + Root.game.locations[location].getLocalfactionHeadName(faction);
        else return Root.game.locations[location].getLocalfactionHeadTitleAndName(faction);
        //if (c <= 0.5f) return "Office";
        //else return Faction.getTitle(faction, Simulation.Parameters.getGovernmentStr(Root.game.locations[location]));
    }
}
