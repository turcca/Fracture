using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Simulation
{

    public class LocationIdeology
    {
        private Location location;

		static public string[] getIdeologyNames()
		{
			return new string[]
			{
				"imperialist",
				"nationalist",
				"navigators",
				"brotherhood",
				"liberal",
				"bureaucracy",
				"technocrat",
				"transhumanist",
				"cult",
				"mercantile",
				"aristocrat"
			};
		}
		
		public struct Effects
		{
			public float pgrowth;
			public float industry;
			public float economy;
			public float diplomacy;
			public float happiness;
			public float affluence;
			public float innovation;
			public float morale;
			public float altruism;
			public float military;
			public float holy;
			public float psych;
			public float navigation;
			public float purity;
			public float police;
			public float violent;
			public float aristocracy;
			public float imperialism;
		}
		
		//static public string[] getEffectNames()
		//{
		//    return new string[]
		//    {
		//        "pgrowth",
		//        "industry",
		//        "economy",
		//        "diplomacy",
		//        "happiness",
		//        "affluence",
		//        "innovation",
		//        "morale",
		//        "altruism",
		//        "military",
		//        "holy",
		//        "psych",
		//        "navigation",
		//        "purity",
		//        "police",
		//        "violent",
		//        "aristocracy",
		//        "imperialism"
		//    };
		//}

        public Dictionary<Data.Resource.Type, float> resourceMultiplier =
            new Dictionary<Data.Resource.Type, float>();
        public Dictionary<string, float> support = new Dictionary<string, float>();
        public Effects effects = new Effects();

		public LocationIdeology(Location location)
		{
            this.location = location;

			foreach (string ideology in getIdeologyNames())
			{
				support[ideology] = 0.0f;
			}
            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)))
            {
                resourceMultiplier.Add(type, 0.0f);
            }
			calculateEffects();
		}
		
		public void calculateEffects()
		{
			effects.pgrowth     = (support["cult"] * 1.0f   + support["technocrat"] * -0.6f + support["mercantile"] * -1.0f + support["bureaucracy"] * 0.6f + support["liberal"] * -1.0f + support["nationalist"] * 0.8f    + support["aristocrat"] * 0.7f + support["imperialist"] * 0.2f  + support["navigators"] * -3.0f + support["brotherhood"] * -0.7f + support["transhumanist"] * 0.6f);
			effects.industry    = (support["cult"] * 0.4f   + support["technocrat"] * 0.3f  + support["mercantile"] * -0.1f + support["bureaucracy"] * 0.8f + support["liberal"] * -0.4f + support["nationalist"] * 0.7f    + support["aristocrat"] * 0.3f + support["imperialist"] * 0.0f  + support["navigators"] * -0.5f + support["brotherhood"] * -1.0f + support["transhumanist"] * -0.2f);
			effects.economy     = (support["cult"] * -0.8f  + support["technocrat"] * 0.0f  + support["mercantile"] * 1.0f  + support["bureaucracy"] * 0.3f + support["liberal"] * 0.2f  + support["nationalist"] * -0.3f   + support["aristocrat"] * -0.3f + support["imperialist"] * -0.1f + support["navigators"] * 0.5f + support["brotherhood"] * 0.2f  + support["transhumanist"] * -0.3f);
			effects.diplomacy   = (support["cult"] * -0.8f  + support["technocrat"] * 0.0f  + support["mercantile"] * 1.0f  + support["bureaucracy"] * 0.1f + support["liberal"] * 1.0f + support["nationalist"] * -1.0f    + support["aristocrat"] * 0.4f + support["imperialist"] * 0.3f  + support["navigators"] * 0.6f  + support["brotherhood"] * 0.5f  + support["transhumanist"] * 0.8f);
			effects.happiness   = (support["cult"] * -0.1f  + support["technocrat"] * 0.0f  + support["mercantile"] * 0.5f  + support["bureaucracy"] * -0.2f + support["liberal"] * 0.9f + support["nationalist"] * 0.3f    + support["aristocrat"] * -0.3f + support["imperialist"] * 0.0f + support["navigators"] * 0.2f  + support["brotherhood"] * 0.3f  + support["transhumanist"] * 1.0f);
			effects.affluence   = (support["cult"] * -0.7f  + support["technocrat"] * 0.3f  + support["mercantile"] * 1.0f  + support["bureaucracy"] * 0.1f  + support["liberal"] * 0.6f + support["nationalist"] * -0.3f   + support["aristocrat"] * 0.3f + support["imperialist"] * 0.0f  + support["navigators"] * 0.6f  + support["brotherhood"] * 0.0f  + support["transhumanist"] * -0.1f);
			effects.innovation  = (support["cult"] * -0.4f  + support["technocrat"] * 0.5f  + support["mercantile"] * 0.4f  + support["bureaucracy"] * -0.3f + support["liberal"] * 1.0f + support["nationalist"] * -0.3f   + support["aristocrat"] * -0.1f + support["imperialist"] * -0.2f + support["navigators"] * 0.7f + support["brotherhood"] * 0.8f  + support["transhumanist"] * 0.8f);
			effects.morale      = (support["cult"] * 1.0f   + support["technocrat"] * -0.2f + support["mercantile"] * -0.9f + support["bureaucracy"] * -0.2f + support["liberal"] * -1.0f + support["nationalist"] * 0.8f   + support["aristocrat"] * 0.5f + support["imperialist"] * 0.4f  + support["navigators"] * -0.2f + support["brotherhood"] * -0.1f + support["transhumanist"] * -1.0f);
			effects.altruism    = (support["cult"] * 0.8f   + support["technocrat"] * 0.0f  + support["mercantile"] * -1.0f + support["bureaucracy"] * -0.6f + support["liberal"] * 1.0f + support["nationalist"] * 0.2f    + support["aristocrat"] * -0.4f + support["imperialist"] * 0.1f + support["navigators"] * -0.3f + support["brotherhood"] * 0.0f  + support["transhumanist"] * 0.0f);
			effects.military    = (support["cult"] * 1.0f   + support["technocrat"] * 0.2f  + support["mercantile"] * -0.6f + support["bureaucracy"] * 0.0f + support["liberal"] * -1.0f + support["nationalist"] * 0.9f    + support["aristocrat"] * 0.7f + support["imperialist"] * 0.9f  + support["navigators"] * 0.2f  + support["brotherhood"] * 0.0f  + support["transhumanist"] * -0.6f);
			
			effects.holy        = (support["cult"] * 1.0f   + support["technocrat"] * -0.2f + support["mercantile"] * -0.4f + support["bureaucracy"] * -0.1f + support["liberal"] * -0.6f + support["nationalist"] * 0.0f   + support["aristocrat"] * -0.3f + support["imperialist"] * 0.6f + support["navigators"] * 0.0f  + support["brotherhood"] * 0.1f  + support["transhumanist"] * -1.0f);
			effects.psych       = (support["cult"] * -0.7f  + support["technocrat"] * -1.0f + support["mercantile"] * 0.2f  + support["bureaucracy"] * -0.2f + support["liberal"] * 0.4f + support["nationalist"] * -0.5f   + support["aristocrat"] * 0.3f + support["imperialist"] * 0.2f  + support["navigators"] * 1.0f  + support["brotherhood"] * 2.0f  + support["transhumanist"] * 0.6f);
			effects.navigation  = (support["cult"] * -0.7f  + support["technocrat"] * 0.5f  + support["mercantile"] * 0.7f  + support["bureaucracy"] * -0.3f + support["liberal"] * 0.2f + support["nationalist"] * -1.0f   + support["aristocrat"] * 0.1f + support["imperialist"] * 0.1f  + support["navigators"] * 2.0f  + support["brotherhood"] * 1.0f  + support["transhumanist"] * 0.6f);
			effects.purity      = (support["cult"] * 1.0f   + support["technocrat"] * -0.5f + support["mercantile"] * -0.6f + support["bureaucracy"] * 0.1f + support["liberal"] * -0.6f + support["nationalist"] * 0.4f    + support["aristocrat"] * -0.3f + support["imperialist"] * 0.2f + support["navigators"] * -1.0f + support["brotherhood"] * -0.3f + support["transhumanist"] * -1.0f);
			
			effects.police      = (support["cult"] * 1.0f   + support["technocrat"] * -0.2f + support["mercantile"] * -0.6f + support["bureaucracy"] * 0.4f + support["liberal"] * -1.0f + support["nationalist"] * 0.7f    + support["aristocrat"] * 0.8f + support["imperialist"] * 0.3f  + support["navigators"] * -0.2f + support["brotherhood"] * -0.4f + support["transhumanist"] * -2.0f);
			effects.violent     = (support["cult"] * 1.0f   + support["technocrat"] * -0.2f + support["mercantile"] * -0.7f + support["bureaucracy"] * 0.0f + support["liberal"] * -1.0f + support["nationalist"] * 1.0f    + support["aristocrat"] * 0.6f + support["imperialist"] * 0.3f  + support["navigators"] * -0.5f + support["brotherhood"] * -0.6f + support["transhumanist"] * 0.2f);
			effects.aristocracy = (support["cult"] * -0.3f  + support["technocrat"] * 0.1f  + support["mercantile"] * 1.0f  + support["bureaucracy"] * 0.3f + support["liberal"] * -0.7f + support["nationalist"] * -0.3f   + support["aristocrat"] * 1.0f + support["imperialist"] * 0.5f  + support["navigators"] * 1.0f  + support["brotherhood"] * 0.7f  + support["transhumanist"] * -1.0f);
			effects.imperialism = (support["cult"] * 0.8f   + support["technocrat"] * -0.1f + support["mercantile"] * -0.2f + support["bureaucracy"] * 0.05f + support["liberal"] * -0.5f + support["nationalist"] * -1.0f + support["aristocrat"] * 0.5f + support["imperialist"] * 1.0f   + support["navigators"] * 0.1f  + support["brotherhood"] * 0.05f + support["transhumanist"] * -1.0f);
			
            resourceMultiplier[Data.Resource.Type.Food] = effects.pgrowth + 1;
            resourceMultiplier[Data.Resource.Type.Mineral] = effects.industry + 1;
            resourceMultiplier[Data.Resource.Type.BlackMarket] = 1 - effects.altruism;
            resourceMultiplier[Data.Resource.Type.Innovation] = effects.innovation + 1;
            resourceMultiplier[Data.Resource.Type.Culture] = (effects.diplomacy + effects.happiness + effects.altruism) / 3 + 1;
            resourceMultiplier[Data.Resource.Type.Industry] = effects.industry + 1;
            resourceMultiplier[Data.Resource.Type.Economy] = effects.economy + 1;
            resourceMultiplier[Data.Resource.Type.Military] = effects.military + 1;
		}
		
		public string toDebugString()
		{
			string rv = "";
			foreach (string ideology in getIdeologyNames())
			{
				rv = rv + ideology + ": " + support[ideology].ToString() + "\n";
			}
			return rv;
		}
    }
}