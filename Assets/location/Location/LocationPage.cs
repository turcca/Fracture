using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LocationPage : MonoBehaviour 
{
    Location location;

    public Text locationName;
    public Text systemName;

    public Text controlled;
    public Image factionLogo;
    public Text governmentType;
    public Text importance;

    public Text description;
    public Text locationStats;


	// Use this for initialization
	void Start () 
    {
        loadLocationInfo();
	}

    private void loadLocationInfo()
    {
        bool isControlled = false;
        location = Root.game.player.getLocation();
        if (location == null) Debug.LogError ("Location not found");

        else
        {
            if (locationName != null) locationName.text = location.features.name;
            if (systemName != null) systemName.text = location.features.subsector;

            if (importance != null) importance.text = Faction.getImportanceDescription(location);
            if (governmentType != null) governmentType.text = location.ideology.getGovernmentType();
            
            if (description != null)
            { 
                description.text = location.features.description1;
                Debug.Log("todo: description2 prerequisits");
                description.text += "\n\n" + location.features.description2;
            }

            // controlled
            KeyValuePair<Faction.FactionID, float> faction = location.ideology.getHighestFactionAndValue();
            if (faction.Value > 0.5f)
            {
                isControlled = true;
                controlled.text = "CONTROLLED:  " + Faction.getFactionName(faction.Key);
                factionLogo.enabled = true;
                factionLogo.sprite = Faction.getFactionLogo(faction.Key, true);
                if (factionLogo.sprite == null) Debug.LogError("ERROR loading faction logo.  Faction: " + Faction.getFactionName(faction.Key) + "  value: " + faction.Value);
            }
            else factionLogo.enabled = false;

            // stats
            if (locationStats != null)
            {
                string stats = "";

                if (isControlled)
                {
                    //stats += "\n";
                }

                // Population
                if (location.features.population < 1f) stats += "Population: " + location.features.population * 1000000 + "\n";
                else stats += "Population:  " + Mathf.Round(location.features.population *1000)/1000 + "M\n";
                stats += "\n";
                // Ruler
                stats += "Ruler: \n"+location.ideology.getRuler() +"\n";
                stats += "\n";
                // Techs
                stats += Faction.getTechDescription(location, Data.Tech.Type.Technology) + "\n";
                stats += Faction.getTechDescription(location, Data.Tech.Type.Infrastructure) + "\n";
                stats += Faction.getTechDescription(location, Data.Tech.Type.Military) + "\n";
                stats += "\n";
                // ideology
                stats += "Political Groups:\n";
                stats += Faction.getIdeologyList(location);

                // control in contacts?


                locationStats.text = stats;
            }
        }
    }
	

}
