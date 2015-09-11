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
        //public string ruler = "NO_RULER"; // not read from data
        public string subsector = "NO_SUBSECTOR";
        public string description1 = "NO_DESCRIPTION1";
        public string description2 = "NO_DESCRIPTION2";
        public Location.Visibility visibility = Location.Visibility.Connected;

        public Dictionary<Faction.FactionID, float> factionCtrl = new Dictionary<Faction.FactionID, float>();
        public Dictionary<Simulation.LocationIdeology.IdeologyID, float> baseIdeology = new Dictionary<Simulation.LocationIdeology.IdeologyID, float>();
        public Dictionary<Faction.FactionID, string> ruler = new Dictionary<Faction.FactionID, string>();

        public Faction.FactionID? hq = null;

        public Dictionary<Resource.Type, float> resourceMultiplier = new Dictionary<Resource.Type, float>(); // location feature multiplier


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

        //public LocationFeatures(string data)
        //    : this()
        //{
        //    string[] dataChunk = data.Split(',');
        //    resourceMultiplier[Resource.Type.Food] = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    resourceMultiplier[Resource.Type.Mineral] = float.Parse(dataChunk[1], CultureInfo.InvariantCulture.NumberFormat);
        //    resourceMultiplier[Resource.Type.Industry] = float.Parse(dataChunk[2], CultureInfo.InvariantCulture.NumberFormat);
        //    resourceMultiplier[Resource.Type.Economy] = float.Parse(dataChunk[3], CultureInfo.InvariantCulture.NumberFormat);
        //    resourceMultiplier[Resource.Type.Innovation] = float.Parse(dataChunk[4], CultureInfo.InvariantCulture.NumberFormat);
        //    resourceMultiplier[Resource.Type.Culture] = float.Parse(dataChunk[5], CultureInfo.InvariantCulture.NumberFormat);
        //    resourceMultiplier[Resource.Type.Military] = float.Parse(dataChunk[6], CultureInfo.InvariantCulture.NumberFormat);
        //    resourceMultiplier[Resource.Type.BlackMarket] = float.Parse(dataChunk[7], CultureInfo.InvariantCulture.NumberFormat);

        //    // commented out until new data is made
        //    //// todo: change to baseIdeology stats
        //    //frontier = float.Parse(dataChunk[8], CultureInfo.InvariantCulture.NumberFormat);
        //    //liberalValues = float.Parse(dataChunk[9], CultureInfo.InvariantCulture.NumberFormat);
        //    //independent = float.Parse(dataChunk[10], CultureInfo.InvariantCulture.NumberFormat);
        //    //religious = float.Parse(dataChunk[11], CultureInfo.InvariantCulture.NumberFormat);
        //    //psychic = float.Parse(dataChunk[12], CultureInfo.InvariantCulture.NumberFormat);
        //    //psyStability = float.Parse(dataChunk[13], CultureInfo.InvariantCulture.NumberFormat);

        //    //// /\
        //    ///*
        //    //imperialist 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //nationalist 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //navigators 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //brotherhood 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //liberal 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //bureaucracy 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //technocrat 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //transhumanist 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //cult 			= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //mercantile 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //aristocrat 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //    //*/


        //    // commented out until new data is made
        //    //// todo: change technology levels to: Technology, Infrastructure, Military
        //    /// todo: add assets: SpaceStation, TransportShips, groundTroops, MilitaryShips (needs amount and type, ships need tracking and cargo)
        //    //population = float.Parse(dataChunk[14], CultureInfo.InvariantCulture.NumberFormat);
        //    //techLevel = float.Parse(dataChunk[15], CultureInfo.InvariantCulture.NumberFormat);
        //    //infrastructure = float.Parse(dataChunk[16], CultureInfo.InvariantCulture.NumberFormat);
        //    //orbitalInfra = float.Parse(dataChunk[17], CultureInfo.InvariantCulture.NumberFormat);
        //    //legalLevel = int.Parse(dataChunk[18], CultureInfo.InvariantCulture.NumberFormat);
        //}

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
