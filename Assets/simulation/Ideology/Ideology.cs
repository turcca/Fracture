using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Simulation
{
    public enum Effect
    {
        pgrowth,
        industry,
        economy,
        diplomacy,
        happiness,
        affluence,
        innovation,
        morale,
        altruism,
        military,
        holy,
        psych,
        navigation,
        purity,
        police,
        violent,
        aristocracy,
        imperialism
    }

    public class LocationIdeology
    {
        private Location location;


        

        public Dictionary<Data.Resource.Type, float> resourceMultiplier = new Dictionary<Data.Resource.Type, float>(); // ideology-based multiplier
        public Dictionary<Faction.IdeologyID, float> support = new Dictionary<Faction.IdeologyID, float>();
        public Dictionary<Effect, float> effects = new Dictionary<Effect, float>();

		public LocationIdeology(Location location)
		{
            this.location = location;

            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)))
            {
                resourceMultiplier.Add(type, 0.0f);
            }
            updateLocationIdeology(location);

            //Debug.Log (toDebugString());
            effects = getCalculatedEffects(support);
            resourceMultiplier = getCalculatedResourceMultipliers(effects);
        }

        public static Dictionary<Effect, float> getCalculatedEffects(Dictionary<Faction.IdeologyID, float> support)
        {
            Dictionary<Effect, float> effects = new Dictionary<Effect, float>();

            effects.Add (Effect.pgrowth,    (support[Faction.IdeologyID.cult] * 1.0f + support[Faction.IdeologyID.technocrat] * -0.6f + support[Faction.IdeologyID.mercantile] * -1.0f + support[Faction.IdeologyID.bureaucracy] * 0.6f + support[Faction.IdeologyID.liberal] * -1.0f + support[Faction.IdeologyID.nationalist] * 0.8f + support[Faction.IdeologyID.aristocrat] * 0.7f + support[Faction.IdeologyID.imperialist] * 0.2f + support[Faction.IdeologyID.navigators] * -3.0f + support[Faction.IdeologyID.brotherhood] * -0.7f + support[Faction.IdeologyID.transhumanist] * 0.6f));
            effects.Add (Effect.industry,   (support[Faction.IdeologyID.cult] * 0.4f + support[Faction.IdeologyID.technocrat] * 0.3f + support[Faction.IdeologyID.mercantile] * -0.1f + support[Faction.IdeologyID.bureaucracy] * 0.8f + support[Faction.IdeologyID.liberal] * -0.4f + support[Faction.IdeologyID.nationalist] * 0.7f + support[Faction.IdeologyID.aristocrat] * 0.3f + support[Faction.IdeologyID.imperialist] * 0.0f + support[Faction.IdeologyID.navigators] * -0.5f + support[Faction.IdeologyID.brotherhood] * -1.0f + support[Faction.IdeologyID.transhumanist] * -0.2f));
            effects.Add (Effect.economy,    (support[Faction.IdeologyID.cult] * -0.8f + support[Faction.IdeologyID.technocrat] * 0.0f + support[Faction.IdeologyID.mercantile] * 1.0f + support[Faction.IdeologyID.bureaucracy] * 0.3f + support[Faction.IdeologyID.liberal] * 0.2f + support[Faction.IdeologyID.nationalist] * -0.3f + support[Faction.IdeologyID.aristocrat] * -0.3f + support[Faction.IdeologyID.imperialist] * -0.1f + support[Faction.IdeologyID.navigators] * 0.5f + support[Faction.IdeologyID.brotherhood] * 0.2f + support[Faction.IdeologyID.transhumanist] * -0.3f));
            effects.Add (Effect.diplomacy,  (support[Faction.IdeologyID.cult] * -0.8f + support[Faction.IdeologyID.technocrat] * 0.0f + support[Faction.IdeologyID.mercantile] * 1.0f + support[Faction.IdeologyID.bureaucracy] * 0.1f + support[Faction.IdeologyID.liberal] * 1.0f + support[Faction.IdeologyID.nationalist] * -1.0f + support[Faction.IdeologyID.aristocrat] * 0.4f + support[Faction.IdeologyID.imperialist] * 0.3f + support[Faction.IdeologyID.navigators] * 0.6f + support[Faction.IdeologyID.brotherhood] * 0.5f + support[Faction.IdeologyID.transhumanist] * 0.8f));
            effects.Add (Effect.happiness,  (support[Faction.IdeologyID.cult] * -0.1f + support[Faction.IdeologyID.technocrat] * 0.0f + support[Faction.IdeologyID.mercantile] * 0.5f + support[Faction.IdeologyID.bureaucracy] * -0.2f + support[Faction.IdeologyID.liberal] * 0.9f + support[Faction.IdeologyID.nationalist] * 0.3f + support[Faction.IdeologyID.aristocrat] * -0.3f + support[Faction.IdeologyID.imperialist] * 0.0f + support[Faction.IdeologyID.navigators] * 0.2f + support[Faction.IdeologyID.brotherhood] * 0.3f + support[Faction.IdeologyID.transhumanist] * 1.0f));
            effects.Add (Effect.affluence,  (support[Faction.IdeologyID.cult] * -0.7f + support[Faction.IdeologyID.technocrat] * 0.3f + support[Faction.IdeologyID.mercantile] * 1.0f + support[Faction.IdeologyID.bureaucracy] * 0.1f + support[Faction.IdeologyID.liberal] * 0.6f + support[Faction.IdeologyID.nationalist] * -0.3f + support[Faction.IdeologyID.aristocrat] * 0.3f + support[Faction.IdeologyID.imperialist] * 0.0f + support[Faction.IdeologyID.navigators] * 0.6f + support[Faction.IdeologyID.brotherhood] * 0.0f + support[Faction.IdeologyID.transhumanist] * -0.1f));
            effects.Add (Effect.innovation, (support[Faction.IdeologyID.cult] * -0.4f + support[Faction.IdeologyID.technocrat] * 0.5f + support[Faction.IdeologyID.mercantile] * 0.4f + support[Faction.IdeologyID.bureaucracy] * -0.3f + support[Faction.IdeologyID.liberal] * 1.0f + support[Faction.IdeologyID.nationalist] * -0.3f + support[Faction.IdeologyID.aristocrat] * -0.1f + support[Faction.IdeologyID.imperialist] * -0.2f + support[Faction.IdeologyID.navigators] * 0.7f + support[Faction.IdeologyID.brotherhood] * 0.8f + support[Faction.IdeologyID.transhumanist] * 0.8f));
            effects.Add (Effect.morale,     (support[Faction.IdeologyID.cult] * 1.0f + support[Faction.IdeologyID.technocrat] * -0.2f + support[Faction.IdeologyID.mercantile] * -0.9f + support[Faction.IdeologyID.bureaucracy] * -0.2f + support[Faction.IdeologyID.liberal] * -1.0f + support[Faction.IdeologyID.nationalist] * 0.8f + support[Faction.IdeologyID.aristocrat] * 0.5f + support[Faction.IdeologyID.imperialist] * 0.4f + support[Faction.IdeologyID.navigators] * -0.2f + support[Faction.IdeologyID.brotherhood] * -0.1f + support[Faction.IdeologyID.transhumanist] * -1.0f));
            effects.Add (Effect.altruism,   (support[Faction.IdeologyID.cult] * 0.8f + support[Faction.IdeologyID.technocrat] * 0.0f + support[Faction.IdeologyID.mercantile] * -1.0f + support[Faction.IdeologyID.bureaucracy] * -0.6f + support[Faction.IdeologyID.liberal] * 1.0f + support[Faction.IdeologyID.nationalist] * 0.2f + support[Faction.IdeologyID.aristocrat] * -0.4f + support[Faction.IdeologyID.imperialist] * 0.1f + support[Faction.IdeologyID.navigators] * -0.3f + support[Faction.IdeologyID.brotherhood] * 0.0f + support[Faction.IdeologyID.transhumanist] * 0.0f));
            effects.Add (Effect.military,   (support[Faction.IdeologyID.cult] * 1.0f + support[Faction.IdeologyID.technocrat] * 0.2f + support[Faction.IdeologyID.mercantile] * -0.6f + support[Faction.IdeologyID.bureaucracy] * 0.0f + support[Faction.IdeologyID.liberal] * -1.0f + support[Faction.IdeologyID.nationalist] * 0.9f + support[Faction.IdeologyID.aristocrat] * 0.7f + support[Faction.IdeologyID.imperialist] * 0.9f + support[Faction.IdeologyID.navigators] * 0.2f + support[Faction.IdeologyID.brotherhood] * 0.0f + support[Faction.IdeologyID.transhumanist] * -0.6f));

            effects.Add (Effect.holy,       (support[Faction.IdeologyID.cult] * 1.0f + support[Faction.IdeologyID.technocrat] * -0.2f + support[Faction.IdeologyID.mercantile] * -0.4f + support[Faction.IdeologyID.bureaucracy] * -0.1f + support[Faction.IdeologyID.liberal] * -0.6f + support[Faction.IdeologyID.nationalist] * 0.0f + support[Faction.IdeologyID.aristocrat] * -0.3f + support[Faction.IdeologyID.imperialist] * 0.6f + support[Faction.IdeologyID.navigators] * 0.0f + support[Faction.IdeologyID.brotherhood] * 0.1f + support[Faction.IdeologyID.transhumanist] * -1.0f));
            effects.Add (Effect.psych,      (support[Faction.IdeologyID.cult] * -0.7f + support[Faction.IdeologyID.technocrat] * -1.0f + support[Faction.IdeologyID.mercantile] * 0.2f + support[Faction.IdeologyID.bureaucracy] * -0.2f + support[Faction.IdeologyID.liberal] * 0.4f + support[Faction.IdeologyID.nationalist] * -0.5f + support[Faction.IdeologyID.aristocrat] * 0.3f + support[Faction.IdeologyID.imperialist] * 0.2f + support[Faction.IdeologyID.navigators] * 1.0f + support[Faction.IdeologyID.brotherhood] * 2.0f + support[Faction.IdeologyID.transhumanist] * 0.6f));
            effects.Add (Effect.navigation, (support[Faction.IdeologyID.cult] * -0.7f + support[Faction.IdeologyID.technocrat] * 0.5f + support[Faction.IdeologyID.mercantile] * 0.7f + support[Faction.IdeologyID.bureaucracy] * -0.3f + support[Faction.IdeologyID.liberal] * 0.2f + support[Faction.IdeologyID.nationalist] * -1.0f + support[Faction.IdeologyID.aristocrat] * 0.1f + support[Faction.IdeologyID.imperialist] * 0.1f + support[Faction.IdeologyID.navigators] * 2.0f + support[Faction.IdeologyID.brotherhood] * 1.0f + support[Faction.IdeologyID.transhumanist] * 0.6f));
            effects.Add (Effect.purity,     (support[Faction.IdeologyID.cult] * 1.0f + support[Faction.IdeologyID.technocrat] * -0.5f + support[Faction.IdeologyID.mercantile] * -0.6f + support[Faction.IdeologyID.bureaucracy] * 0.1f + support[Faction.IdeologyID.liberal] * -0.6f + support[Faction.IdeologyID.nationalist] * 0.4f + support[Faction.IdeologyID.aristocrat] * -0.3f + support[Faction.IdeologyID.imperialist] * 0.2f + support[Faction.IdeologyID.navigators] * -1.0f + support[Faction.IdeologyID.brotherhood] * -0.3f + support[Faction.IdeologyID.transhumanist] * -1.0f));

            effects.Add (Effect.police,      (support[Faction.IdeologyID.cult] * 1.0f + support[Faction.IdeologyID.technocrat] * -0.2f + support[Faction.IdeologyID.mercantile] * -0.6f + support[Faction.IdeologyID.bureaucracy] * 0.4f + support[Faction.IdeologyID.liberal] * -1.0f + support[Faction.IdeologyID.nationalist] * 0.7f + support[Faction.IdeologyID.aristocrat] * 0.8f + support[Faction.IdeologyID.imperialist] * 0.3f + support[Faction.IdeologyID.navigators] * -0.2f + support[Faction.IdeologyID.brotherhood] * -0.4f + support[Faction.IdeologyID.transhumanist] * -2.0f));
            effects.Add (Effect.violent,     (support[Faction.IdeologyID.cult] * 1.0f + support[Faction.IdeologyID.technocrat] * -0.2f + support[Faction.IdeologyID.mercantile] * -0.7f + support[Faction.IdeologyID.bureaucracy] * 0.0f + support[Faction.IdeologyID.liberal] * -1.0f + support[Faction.IdeologyID.nationalist] * 1.0f + support[Faction.IdeologyID.aristocrat] * 0.6f + support[Faction.IdeologyID.imperialist] * 0.3f + support[Faction.IdeologyID.navigators] * -0.5f + support[Faction.IdeologyID.brotherhood] * -0.6f + support[Faction.IdeologyID.transhumanist] * 0.2f));
            effects.Add (Effect.aristocracy, (support[Faction.IdeologyID.cult] * -0.3f + support[Faction.IdeologyID.technocrat] * 0.1f + support[Faction.IdeologyID.mercantile] * 1.0f + support[Faction.IdeologyID.bureaucracy] * 0.3f + support[Faction.IdeologyID.liberal] * -0.7f + support[Faction.IdeologyID.nationalist] * -0.3f + support[Faction.IdeologyID.aristocrat] * 1.0f + support[Faction.IdeologyID.imperialist] * 0.5f + support[Faction.IdeologyID.navigators] * 1.0f + support[Faction.IdeologyID.brotherhood] * 0.7f + support[Faction.IdeologyID.transhumanist] * -1.0f));
            effects.Add (Effect.imperialism, (support[Faction.IdeologyID.cult] * 0.8f + support[Faction.IdeologyID.technocrat] * -0.1f + support[Faction.IdeologyID.mercantile] * -0.2f + support[Faction.IdeologyID.bureaucracy] * 0.05f + support[Faction.IdeologyID.liberal] * -0.5f + support[Faction.IdeologyID.nationalist] * -1.0f + support[Faction.IdeologyID.aristocrat] * 0.5f + support[Faction.IdeologyID.imperialist] * 1.0f + support[Faction.IdeologyID.navigators] * 0.1f + support[Faction.IdeologyID.brotherhood] * 0.05f + support[Faction.IdeologyID.transhumanist] * -1.0f));

            return effects;
        }
        public static Dictionary<Data.Resource.Type, float> getCalculatedResourceMultipliers(Dictionary<Effect, float> effects)
        {
            Dictionary<Data.Resource.Type, float> resourceMultiplier = new Dictionary<Data.Resource.Type, float>();

            resourceMultiplier.Add(Data.Resource.Type.Food, effects[Effect.pgrowth] + 1f);
            resourceMultiplier.Add(Data.Resource.Type.Mineral, effects[Effect.industry] + 1f);
            resourceMultiplier.Add(Data.Resource.Type.BlackMarket, 1f - effects[Effect.altruism]);
            resourceMultiplier.Add(Data.Resource.Type.Innovation, effects[Effect.innovation] + 1f);
            resourceMultiplier.Add(Data.Resource.Type.Culture, (effects[Effect.diplomacy] + effects[Effect.happiness] + effects[Effect.altruism]) / 3f + 1f);
            resourceMultiplier.Add(Data.Resource.Type.Industry, effects[Effect.industry] + 1f);
            resourceMultiplier.Add(Data.Resource.Type.Economy, effects[Effect.economy] + 1f);
            resourceMultiplier.Add(Data.Resource.Type.Military, effects[Effect.military] + 1f);

            return resourceMultiplier;
        }


        public void updateLocationIdeology(Location location)
        {
            // load base ideology
            foreach (Faction.IdeologyID ideology in Enum.GetValues(typeof(Faction.IdeologyID)))
            {
                support[ideology] = location.features.baseIdeology[ideology];
            }

            // load faction ideology
            float totalCtrl = 0f;
            Dictionary<Faction.IdeologyID, float> factionsIdeologies = getFactionsIdeologies();
            foreach (KeyValuePair<Faction.IdeologyID, float> factionsIdeology in factionsIdeologies)
            {
                if (factionsIdeology.Value > 0f)
                {
                    totalCtrl += factionsIdeology.Value;
                }
            }
            // split support into base support and faction support, then combine them
            float totalIdeology = 0f;
            if (totalCtrl > 0f)
            {
                foreach (Faction.IdeologyID ideology in Enum.GetValues(typeof(Faction.IdeologyID)))
                {
                    support[ideology] = support[ideology] * (1f - totalCtrl) + factionsIdeologies[ideology];
                    totalIdeology += support[ideology];
                }
                if (totalIdeology <0.999f || totalIdeology > 1.001f) Debug.LogWarning("WARNING: base ideology total: " + totalIdeology + "@ "+location.id);
            }
        }

        private Dictionary<Faction.IdeologyID, float> getFactionsIdeologies()
        {
            Dictionary<Faction.IdeologyID, float> factionsIdeology = new Dictionary<Faction.IdeologyID, float>();
            foreach (Faction.IdeologyID ideology in Enum.GetValues(typeof(Faction.IdeologyID)))
            {
                factionsIdeology.Add(ideology, 0.0f); // = location.features.baseIdeology[ideology];
            }
            foreach (var faction in location.features.factionCtrl)
            {
                switch (faction.Key)
                {
                    case Faction.FactionID.noble1:
                        {
                            factionsIdeology[Faction.IdeologyID.aristocrat] += faction.Value * 0.5f;
                            factionsIdeology[Faction.IdeologyID.nationalist] += faction.Value * 0.5f;
                            break;
                        }
                    case Faction.FactionID.noble2:
                        {
                            factionsIdeology[Faction.IdeologyID.aristocrat] += faction.Value * 0.6f;
                            factionsIdeology[Faction.IdeologyID.mercantile] += faction.Value * 0.4f;
                            break;
                        }
                    case Faction.FactionID.noble3:
                        {
                            factionsIdeology[Faction.IdeologyID.imperialist] += faction.Value * 0.6f;
                            factionsIdeology[Faction.IdeologyID.cult] += faction.Value * 0.2f;
                            factionsIdeology[Faction.IdeologyID.aristocrat] += faction.Value * 0.2f;
                            break;
                        }
                    case Faction.FactionID.noble4:
                        {
                            factionsIdeology[Faction.IdeologyID.aristocrat] += faction.Value * 0.5f;
                            factionsIdeology[Faction.IdeologyID.liberal] += faction.Value * 0.3f;
                            factionsIdeology[Faction.IdeologyID.navigators] += faction.Value * 0.1f;
                            factionsIdeology[Faction.IdeologyID.brotherhood] += faction.Value * 0.1f;
                            break;
                        }
                    case Faction.FactionID.guild1:
                        {
                            factionsIdeology[Faction.IdeologyID.transhumanist] += faction.Value * 0.5f;
                            factionsIdeology[Faction.IdeologyID.mercantile] += faction.Value * 0.3f;
                            factionsIdeology[Faction.IdeologyID.liberal] += faction.Value * 0.2f;
                            break;
                        }
                    case Faction.FactionID.guild2:
                        {
                            factionsIdeology[Faction.IdeologyID.technocrat] += faction.Value * 0.4f;
                            factionsIdeology[Faction.IdeologyID.bureaucracy] += faction.Value * 0.3f;
                            factionsIdeology[Faction.IdeologyID.mercantile] += faction.Value * 0.2f;
                            factionsIdeology[Faction.IdeologyID.aristocrat] += faction.Value * 0.1f;
                            break;
                        }
                    case Faction.FactionID.guild3:
                        {
                            factionsIdeology[Faction.IdeologyID.mercantile] += faction.Value * 0.6f;
                            factionsIdeology[Faction.IdeologyID.bureaucracy] += faction.Value * 0.3f;
                            factionsIdeology[Faction.IdeologyID.navigators] += faction.Value * 0.1f;
                            break;
                        }
                    case Faction.FactionID.church:
                        {
                            factionsIdeology[Faction.IdeologyID.cult] += faction.Value * 1.0f;
                            break;
                        }
                    case Faction.FactionID.heretic:
                        {
                            factionsIdeology[Faction.IdeologyID.transhumanist] += faction.Value * 1.0f;
                            break;
                        }
                }
            }

            return factionsIdeology;
        }

        public string getGovernmentController()
        {
            string rv = "";

            KeyValuePair<Faction.FactionID, float> faction = getHighestFactionAndValue();
            KeyValuePair<Faction.IdeologyID, float> ideology = getHighestIdeologyAndValue();

            bool controlled = (faction.Value >= ideology.Value) ? true : false;
            Debug.Log("TODO");
            return rv;
        }

        /// <summary>
        /// eg. "House Valeria Governor"
        /// </summary>
        /// <returns></returns>
        public string getRuler()
        {
            KeyValuePair<Faction.FactionID, float> faction = getHighestFactionAndValue();
            KeyValuePair<Faction.IdeologyID, float> ideology = getHighestIdeologyAndValue();
            
            bool controller = (faction.Value >= ideology.Value) ? true : false;

            string rv = "";
            // faction controlled ruler
            if (controller)
            {
                // House Valeria
                rv += Faction.getFactionName(faction.Key);
                rv += " ";
                // Governer
                rv += Faction.getTitle(faction.Key, Simulation.Parameters.getGovernmentStr(location));
                }
            // local government ruler
            else
            {
                // Governer
                rv += Faction.getTitle(ideology.Key, Simulation.Parameters.getGovernmentStr(location));
                rv += " of the ";
                // eg. 'Guild of Merchants'
                rv += Faction.getPartyName(ideology.Key);
            }

            //Debug.Log (location.id+" ruler: "+rv+ " ("+location.name+")");
            return rv;
        }
        
        public string getGovernmentType()
        {
            KeyValuePair<Faction.FactionID, float> faction = getHighestFactionAndValue();
            KeyValuePair<Faction.IdeologyID, float> ideology = getHighestIdeologyAndValue();

            bool controlled = (faction.Value >= ideology.Value) ? true : false;

            return (controlled) ? 
                Faction.getGovernmentType(faction.Key) :
                Faction.getGovernmentType(ideology.Key);
        }

        public KeyValuePair<Faction.IdeologyID, float> getHighestIdeologyAndValue()
        {
            KeyValuePair<Faction.IdeologyID, float> highest = new KeyValuePair<Faction.IdeologyID, float>();
            float highestValue = 0.0f;

            foreach (var pair in support)
            {
                if (pair.Value > highestValue)
                {
                    highest = pair;
                    highestValue = pair.Value;
                }
            }
            return highest;
        }
        public KeyValuePair<Faction.FactionID, float> getHighestFactionAndValue()
        {
            KeyValuePair<Faction.FactionID, float> highest = new KeyValuePair<Faction.FactionID, float>();
            float highestValue = 0.0f;
            
            foreach (var pair in location.features.factionCtrl)
            {
                if (pair.Value > highestValue)
                {
                    highest = pair;
                    highestValue = pair.Value;
                }
            }
            return highest;
        }
        public Faction.IdeologyID getRandomIdeology()
        {
            float roll = UnityEngine.Random.value;
            float accumulator = 0f;

            foreach (var pair in support)
            {
                accumulator += pair.Value;
                //Debug.Log(pair.Key.ToString() + ": " + pair.Value + " (total: " + accumulator + ")");
                if (roll < accumulator) return pair.Key;
            }
            Debug.Log("ERROR: no ideology returned! (accumulated: "+accumulator+" roll: "+roll+")");
            return Faction.IdeologyID.imperialist;
        }
		
		public string toDebugString()
		{
			string rv = "";
            foreach (Faction.IdeologyID ideology in Enum.GetValues(typeof(Faction.IdeologyID)))
			{
				rv = rv + ideology + ": " + support[ideology].ToString() + "\n";
			}
			return rv;
		}
    }
}