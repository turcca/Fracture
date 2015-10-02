using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace Data
{
    public class LocationFeatures 
    {
        public string name = "NO_NAME";
        public string subsector = "NO_SUBSECTOR";
        public string description1 = "NO_DESCRIPTION1";
        public string description2 = "NO_DESCRIPTION2";
        public Location.Visibility visibility = Location.Visibility.Connected;

        public Dictionary<Faction.FactionID, float> factionCtrl = new Dictionary<Faction.FactionID, float>();
        public Dictionary<Simulation.LocationIdeology.IdeologyID, float> baseIdeology = new Dictionary<Simulation.LocationIdeology.IdeologyID, float>();
        public Dictionary<Faction.FactionID, string> ruler = new Dictionary<Faction.FactionID, string>();

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
                ruler.Add (faction, NameGenerator.getName(faction));
            }
            foreach (Simulation.LocationIdeology.IdeologyID ideology in System.Enum.GetValues(typeof(Simulation.LocationIdeology.IdeologyID)))
            {
                baseIdeology.Add(ideology, 0.0f);
            }
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

    }

}
