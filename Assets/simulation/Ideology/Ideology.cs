using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Simulation
{
    public class LocationIdeology
    {
        public enum IdeologyID { cult, technocrat, mercantile, bureaucracy, liberal, nationalist, aristocrat, imperialist, navigators, brotherhood, transhumanist }

        private Location location;

		static public string[] getIdeologyNames()
		{
			return new string[]
			{
                "cult",
                "technocrat",
                "mercantile",
                "bureaucracy",
                "liberal",
                "nationalist",
                "aristocrat",
                "imperialist",
				"navigators",
				"brotherhood",
				"transhumanist"
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

        public Dictionary<Data.Resource.Type, float> resourceMultiplier = new Dictionary<Data.Resource.Type, float>(); // ideology-based multiplier
        public Dictionary<IdeologyID, float> support = new Dictionary<IdeologyID, float>();
        public Effects effects = new Effects();

		public LocationIdeology(Location location)
		{
            this.location = location;

            foreach (IdeologyID ideology in Enum.GetValues(typeof(IdeologyID)))
			{
				support[ideology] = location.features.baseIdeology[ideology];
			}
            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)))
            {
                resourceMultiplier.Add(type, 0.0f);
            }
            //Debug.Log (toDebugString());
            calculateEffects();
		}
		
		public void calculateEffects()
		{
			effects.pgrowth     = (support[IdeologyID.cult] * 1.0f   + support[IdeologyID.technocrat] * -0.6f + support[IdeologyID.mercantile] * -1.0f + support[IdeologyID.bureaucracy] * 0.6f + support[IdeologyID.liberal] * -1.0f + support[IdeologyID.nationalist] * 0.8f    + support[IdeologyID.aristocrat] * 0.7f + support[IdeologyID.imperialist] * 0.2f  + support[IdeologyID.navigators] * -3.0f + support[IdeologyID.brotherhood] * -0.7f + support[IdeologyID.transhumanist] * 0.6f);
			effects.industry    = (support[IdeologyID.cult] * 0.4f   + support[IdeologyID.technocrat] * 0.3f  + support[IdeologyID.mercantile] * -0.1f + support[IdeologyID.bureaucracy] * 0.8f + support[IdeologyID.liberal] * -0.4f + support[IdeologyID.nationalist] * 0.7f    + support[IdeologyID.aristocrat] * 0.3f + support[IdeologyID.imperialist] * 0.0f  + support[IdeologyID.navigators] * -0.5f + support[IdeologyID.brotherhood] * -1.0f + support[IdeologyID.transhumanist] * -0.2f);
			effects.economy     = (support[IdeologyID.cult] * -0.8f  + support[IdeologyID.technocrat] * 0.0f  + support[IdeologyID.mercantile] * 1.0f  + support[IdeologyID.bureaucracy] * 0.3f + support[IdeologyID.liberal] * 0.2f  + support[IdeologyID.nationalist] * -0.3f   + support[IdeologyID.aristocrat] * -0.3f + support[IdeologyID.imperialist] * -0.1f + support[IdeologyID.navigators] * 0.5f + support[IdeologyID.brotherhood] * 0.2f  + support[IdeologyID.transhumanist] * -0.3f);
			effects.diplomacy   = (support[IdeologyID.cult] * -0.8f  + support[IdeologyID.technocrat] * 0.0f  + support[IdeologyID.mercantile] * 1.0f  + support[IdeologyID.bureaucracy] * 0.1f + support[IdeologyID.liberal] * 1.0f + support[IdeologyID.nationalist] * -1.0f    + support[IdeologyID.aristocrat] * 0.4f + support[IdeologyID.imperialist] * 0.3f  + support[IdeologyID.navigators] * 0.6f  + support[IdeologyID.brotherhood] * 0.5f  + support[IdeologyID.transhumanist] * 0.8f);
			effects.happiness   = (support[IdeologyID.cult] * -0.1f  + support[IdeologyID.technocrat] * 0.0f  + support[IdeologyID.mercantile] * 0.5f  + support[IdeologyID.bureaucracy] * -0.2f + support[IdeologyID.liberal] * 0.9f + support[IdeologyID.nationalist] * 0.3f    + support[IdeologyID.aristocrat] * -0.3f + support[IdeologyID.imperialist] * 0.0f + support[IdeologyID.navigators] * 0.2f  + support[IdeologyID.brotherhood] * 0.3f  + support[IdeologyID.transhumanist] * 1.0f);
			effects.affluence   = (support[IdeologyID.cult] * -0.7f  + support[IdeologyID.technocrat] * 0.3f  + support[IdeologyID.mercantile] * 1.0f  + support[IdeologyID.bureaucracy] * 0.1f  + support[IdeologyID.liberal] * 0.6f + support[IdeologyID.nationalist] * -0.3f   + support[IdeologyID.aristocrat] * 0.3f + support[IdeologyID.imperialist] * 0.0f  + support[IdeologyID.navigators] * 0.6f  + support[IdeologyID.brotherhood] * 0.0f  + support[IdeologyID.transhumanist] * -0.1f);
			effects.innovation  = (support[IdeologyID.cult] * -0.4f  + support[IdeologyID.technocrat] * 0.5f  + support[IdeologyID.mercantile] * 0.4f  + support[IdeologyID.bureaucracy] * -0.3f + support[IdeologyID.liberal] * 1.0f + support[IdeologyID.nationalist] * -0.3f   + support[IdeologyID.aristocrat] * -0.1f + support[IdeologyID.imperialist] * -0.2f + support[IdeologyID.navigators] * 0.7f + support[IdeologyID.brotherhood] * 0.8f  + support[IdeologyID.transhumanist] * 0.8f);
			effects.morale      = (support[IdeologyID.cult] * 1.0f   + support[IdeologyID.technocrat] * -0.2f + support[IdeologyID.mercantile] * -0.9f + support[IdeologyID.bureaucracy] * -0.2f + support[IdeologyID.liberal] * -1.0f + support[IdeologyID.nationalist] * 0.8f   + support[IdeologyID.aristocrat] * 0.5f + support[IdeologyID.imperialist] * 0.4f  + support[IdeologyID.navigators] * -0.2f + support[IdeologyID.brotherhood] * -0.1f + support[IdeologyID.transhumanist] * -1.0f);
			effects.altruism    = (support[IdeologyID.cult] * 0.8f   + support[IdeologyID.technocrat] * 0.0f  + support[IdeologyID.mercantile] * -1.0f + support[IdeologyID.bureaucracy] * -0.6f + support[IdeologyID.liberal] * 1.0f + support[IdeologyID.nationalist] * 0.2f    + support[IdeologyID.aristocrat] * -0.4f + support[IdeologyID.imperialist] * 0.1f + support[IdeologyID.navigators] * -0.3f + support[IdeologyID.brotherhood] * 0.0f  + support[IdeologyID.transhumanist] * 0.0f);
			effects.military    = (support[IdeologyID.cult] * 1.0f   + support[IdeologyID.technocrat] * 0.2f  + support[IdeologyID.mercantile] * -0.6f + support[IdeologyID.bureaucracy] * 0.0f + support[IdeologyID.liberal] * -1.0f + support[IdeologyID.nationalist] * 0.9f    + support[IdeologyID.aristocrat] * 0.7f + support[IdeologyID.imperialist] * 0.9f  + support[IdeologyID.navigators] * 0.2f  + support[IdeologyID.brotherhood] * 0.0f  + support[IdeologyID.transhumanist] * -0.6f);
			
			effects.holy        = (support[IdeologyID.cult] * 1.0f   + support[IdeologyID.technocrat] * -0.2f + support[IdeologyID.mercantile] * -0.4f + support[IdeologyID.bureaucracy] * -0.1f + support[IdeologyID.liberal] * -0.6f + support[IdeologyID.nationalist] * 0.0f   + support[IdeologyID.aristocrat] * -0.3f + support[IdeologyID.imperialist] * 0.6f + support[IdeologyID.navigators] * 0.0f  + support[IdeologyID.brotherhood] * 0.1f  + support[IdeologyID.transhumanist] * -1.0f);
			effects.psych       = (support[IdeologyID.cult] * -0.7f  + support[IdeologyID.technocrat] * -1.0f + support[IdeologyID.mercantile] * 0.2f  + support[IdeologyID.bureaucracy] * -0.2f + support[IdeologyID.liberal] * 0.4f + support[IdeologyID.nationalist] * -0.5f   + support[IdeologyID.aristocrat] * 0.3f + support[IdeologyID.imperialist] * 0.2f  + support[IdeologyID.navigators] * 1.0f  + support[IdeologyID.brotherhood] * 2.0f  + support[IdeologyID.transhumanist] * 0.6f);
			effects.navigation  = (support[IdeologyID.cult] * -0.7f  + support[IdeologyID.technocrat] * 0.5f  + support[IdeologyID.mercantile] * 0.7f  + support[IdeologyID.bureaucracy] * -0.3f + support[IdeologyID.liberal] * 0.2f + support[IdeologyID.nationalist] * -1.0f   + support[IdeologyID.aristocrat] * 0.1f + support[IdeologyID.imperialist] * 0.1f  + support[IdeologyID.navigators] * 2.0f  + support[IdeologyID.brotherhood] * 1.0f  + support[IdeologyID.transhumanist] * 0.6f);
			effects.purity      = (support[IdeologyID.cult] * 1.0f   + support[IdeologyID.technocrat] * -0.5f + support[IdeologyID.mercantile] * -0.6f + support[IdeologyID.bureaucracy] * 0.1f + support[IdeologyID.liberal] * -0.6f + support[IdeologyID.nationalist] * 0.4f    + support[IdeologyID.aristocrat] * -0.3f + support[IdeologyID.imperialist] * 0.2f + support[IdeologyID.navigators] * -1.0f + support[IdeologyID.brotherhood] * -0.3f + support[IdeologyID.transhumanist] * -1.0f);
			
			effects.police      = (support[IdeologyID.cult] * 1.0f   + support[IdeologyID.technocrat] * -0.2f + support[IdeologyID.mercantile] * -0.6f + support[IdeologyID.bureaucracy] * 0.4f + support[IdeologyID.liberal] * -1.0f + support[IdeologyID.nationalist] * 0.7f    + support[IdeologyID.aristocrat] * 0.8f + support[IdeologyID.imperialist] * 0.3f  + support[IdeologyID.navigators] * -0.2f + support[IdeologyID.brotherhood] * -0.4f + support[IdeologyID.transhumanist] * -2.0f);
			effects.violent     = (support[IdeologyID.cult] * 1.0f   + support[IdeologyID.technocrat] * -0.2f + support[IdeologyID.mercantile] * -0.7f + support[IdeologyID.bureaucracy] * 0.0f + support[IdeologyID.liberal] * -1.0f + support[IdeologyID.nationalist] * 1.0f    + support[IdeologyID.aristocrat] * 0.6f + support[IdeologyID.imperialist] * 0.3f  + support[IdeologyID.navigators] * -0.5f + support[IdeologyID.brotherhood] * -0.6f + support[IdeologyID.transhumanist] * 0.2f);
			effects.aristocracy = (support[IdeologyID.cult] * -0.3f  + support[IdeologyID.technocrat] * 0.1f  + support[IdeologyID.mercantile] * 1.0f  + support[IdeologyID.bureaucracy] * 0.3f + support[IdeologyID.liberal] * -0.7f + support[IdeologyID.nationalist] * -0.3f   + support[IdeologyID.aristocrat] * 1.0f + support[IdeologyID.imperialist] * 0.5f  + support[IdeologyID.navigators] * 1.0f  + support[IdeologyID.brotherhood] * 0.7f  + support[IdeologyID.transhumanist] * -1.0f);
			effects.imperialism = (support[IdeologyID.cult] * 0.8f   + support[IdeologyID.technocrat] * -0.1f + support[IdeologyID.mercantile] * -0.2f + support[IdeologyID.bureaucracy] * 0.05f + support[IdeologyID.liberal] * -0.5f + support[IdeologyID.nationalist] * -1.0f + support[IdeologyID.aristocrat] * 0.5f + support[IdeologyID.imperialist] * 1.0f   + support[IdeologyID.navigators] * 0.1f  + support[IdeologyID.brotherhood] * 0.05f + support[IdeologyID.transhumanist] * -1.0f);
			
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
            foreach (IdeologyID ideology in Enum.GetValues(typeof(IdeologyID)))
			{
				rv = rv + ideology + ": " + support[ideology].ToString() + "\n";
			}
			return rv;
		}
    }
}