using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace Data
{
    // SAVE ALL
    public class LocationFeatures 
    {
        public string name = "NO_NAME";
        public string subsector = "NO_SUBSECTOR";
        public string description1 = "NO_DESCRIPTION1";
        public string description2 = "NO_DESCRIPTION2";
        public Location.Visibility visibility = Location.Visibility.Connected;

        public Dictionary<Faction.FactionID, float> factionCtrl = new Dictionary<Faction.FactionID, float>();
        public Dictionary<Faction.IdeologyID, float> baseIdeology = new Dictionary<Faction.IdeologyID, float>();
        List<KeyValuePair<Faction.FactionID?, string>> rulers = new List<KeyValuePair<Faction.FactionID?, string>>();

        // faction headquarters
        public Faction.FactionID? hq = null;

        public Dictionary<Resource.Type, float> resourceMultiplier = new Dictionary<Resource.Type, float>(); // location feature multiplier

        public int legality = 0;

        public int startingTechLevel = 0;
        public int startingInfrastructure = 0;
        public int startingMilitaryTechLevel = 0;
        public float population = 10.0f;
        public int assetStation = 0;

        public LocationFeatures()
        {
            foreach (Resource.Type type in Enum.GetValues(typeof(Resource.Type)))
            {
                resourceMultiplier.Add(type, 1.0f);
            }
            foreach (Faction.FactionID faction in System.Enum.GetValues(typeof(Faction.FactionID)))
            {
                factionCtrl.Add(faction, 0.0f);
            }
            foreach (Faction.IdeologyID ideology in System.Enum.GetValues(typeof(Faction.IdeologyID)))
            {
                baseIdeology.Add(ideology, 0.0f);
            }
        }
        public string getRuler(Faction.FactionID? faction)
        {
            foreach (var ruler in rulers)
                if (ruler.Key == faction) return ruler.Value;
            // not found, create it and return
            rulers.Add(new KeyValuePair<Faction.FactionID?, string>(faction, NameGenerator.getName(faction)));
            return rulers[rulers.Count -1].Value;
        }
        public void addRuler(Faction.FactionID? faction, string name = null)
        {
            if (name == null) name = NameGenerator.getName(faction);

            for (int i = 0; i < rulers.Count; i++)
            {
                if (rulers[i].Key == faction)
                {
                    rulers[i] = new KeyValuePair<Faction.FactionID?, string>(faction, name);
                    return;
                }
            }
            rulers.Add(new KeyValuePair<Faction.FactionID?, string>(faction, name));
        }

        public string toDebugString()
        {
            string rv = "";
            foreach (var value in resourceMultiplier.Values)
            {
                rv += value.ToString() + "  ";
            }
            return rv;
        }

        public bool isStation()
        {
            if (resourceMultiplier[Resource.Type.Industry] == 2) return true;
            return false;
        }
    }

}
