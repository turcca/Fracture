using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NewIdeology
{

    public class LocationIdeology
    {
		// Turkka laitto nää tähän toisesta Ideology.sc:stä, oli muuten tyhjä ---->
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
			
			public float foodMul;
			public float mineralsMul;
			public float blackMarketMul;
			public float innovationMul;
			public float cultureMul;
			public float industryMul;
			public float economyMul;
			public float militaryMul;
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
		
		public LocationIdeology()
		{
			foreach (string ideology in getIdeologyNames())
			{
				support[ideology] = 0.0f;
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
			
			effects.foodMul        = effects.pgrowth +1;
			effects.mineralsMul    = effects.industry +1;
			effects.blackMarketMul = 1-effects.altruism;
			effects.innovationMul  = effects.innovation +1;
			effects.cultureMul     = (effects.diplomacy+effects.happiness+effects.altruism)/3 +1;
			effects.industryMul    = effects.industry +1;
			effects.economyMul     = effects.economy +1;
			effects.militaryMul    = effects.military +1;
			
			
			//        effects.pgrowth = (support["cult"] * 100 + support["technocrat"] * -60 + support["mercantile"] * -100 + support["bureaucracy"] * 60 + support["liberal"] * -100 + support["nationalist"] * 80 + support["aristocrat"] * 70 + support["imperialist"] * 20 + support["navigators"] * -300 + support["brotherhood"] * -70 + support["transhumanist"] * 60) / 100;
			//        effects.industry = (support["cult"] * 20 + support["technocrat"] * 100 + support["mercantile"] * 30 + support["bureaucracy"] * 80 + support["liberal"] * -40 + support["nationalist"] * 80 + support["aristocrat"] * 20 + support["imperialist"] * -10 + support["navigators"] * -50 + support["brotherhood"] * -100 + support["transhumanist"] * 0) / 100;
			//        effects.economy = (support["cult"] * -100 + support["technocrat"] * 30 + support["mercantile"] * 100 + support["bureaucracy"] * 60 + support["liberal"] * -30 + support["nationalist"] * -10 + support["aristocrat"] * 40 + support["imperialist"] * -10 + support["navigators"] * 50 + support["brotherhood"] * 0 + support["transhumanist"] * -20) / 100;
			//        effects.diplomacy = (support["cult"] * -100 + support["technocrat"] * 0 + support["mercantile"] * 200 + support["bureaucracy"] * 10 + support["liberal"] * 200 + support["nationalist"] * -100 + support["aristocrat"] * -20 + support["imperialist"] * 20 + support["navigators"] * 100 + support["brotherhood"] * 20 + support["transhumanist"] * 150) / 100;
			//        effects.happiness = (support["cult"] * -30 + support["technocrat"] * 20 + support["mercantile"] * 70 + support["bureaucracy"] * 0 + support["liberal"] * 100 + support["nationalist"] * 30 + support["aristocrat"] * -50 + support["imperialist"] * -20 + support["navigators"] * 20 + support["brotherhood"] * 10 + support["transhumanist"] * 100) / 100;
			//        effects.affluence = (support["cult"] * -70 + support["technocrat"] * 30 + support["mercantile"] * 100 + support["bureaucracy"] * 10 + support["liberal"] * 60 + support["nationalist"] * -30 + support["aristocrat"] * 30 + support["imperialist"] * 0 + support["navigators"] * 60 + support["brotherhood"] * 0 + support["transhumanist"] * -10) / 100;
			//        effects.innovation = (support["cult"] * -100 + support["technocrat"] * 70 + support["mercantile"] * 20 + support["bureaucracy"] * -10 + support["liberal"] * 100 + support["nationalist"] * -20 + support["aristocrat"] * -30 + support["imperialist"] * -20 + support["navigators"] * 70 + support["brotherhood"] * 50 + support["transhumanist"] * 100) / 100;
			//        effects.morale = (support["cult"] * 100 + support["technocrat"] * -20 + support["mercantile"] * -90 + support["bureaucracy"] * -20 + support["liberal"] * -100 + support["nationalist"] * 80 + support["aristocrat"] * 50 + support["imperialist"] * 30 + support["navigators"] * -20 + support["brotherhood"] * -10 + support["transhumanist"] * -100) / 100;
			//        effects.altruism = (support["cult"] * 80 + support["technocrat"] * 20 + support["mercantile"] * -100 + support["bureaucracy"] * -60 + support["liberal"] * 100 + support["nationalist"] * 20 + support["aristocrat"] * -50 + support["imperialist"] * -20 + support["navigators"] * -30 + support["brotherhood"] * -10 + support["transhumanist"] * 0) / 100;
			//        effects.military = (support["cult"] * 100 + support["technocrat"] * 20 + support["mercantile"] * -60 + support["bureaucracy"] * 10 + support["liberal"] * -100 + support["nationalist"] * 100 + support["aristocrat"] * 80 + support["imperialist"] * 40 + support["navigators"] * 20 + support["brotherhood"] * 0 + support["transhumanist"] * -50) / 100;
			//
			//        effects.holy = (support["cult"] * 100 + support["technocrat"] * -20 + support["mercantile"] * -40 + support["bureaucracy"] * -10 + support["liberal"] * -60 + support["nationalist"] * 0 + support["aristocrat"] * -30 + support["imperialist"] * 60 + support["navigators"] * 0 + support["brotherhood"] * 10 + support["transhumanist"] * -100) / 100;
			//        effects.psych = (support["cult"] * -70 + support["technocrat"] * -100 + support["mercantile"] * 20 + support["bureaucracy"] * -20 + support["liberal"] * 40 + support["nationalist"] * -50 + support["aristocrat"] * 30 + support["imperialist"] * 20 + support["navigators"] * 100 + support["brotherhood"] * 200 + support["transhumanist"] * 60) / 100;
			//        effects.navigation = (support["cult"] * -40 + support["technocrat"] * 70 + support["mercantile"] * 100 + support["bureaucracy"] * -10 + support["liberal"] * 20 + support["nationalist"] * -100 + support["aristocrat"] * 10 + support["imperialist"] * 30 + support["navigators"] * 200 + support["brotherhood"] * 60 + support["transhumanist"] * 40) / 100;
			//        effects.purity = (support["cult"] * 100 + support["technocrat"] * -50 + support["mercantile"] * -60 + support["bureaucracy"] * 10 + support["liberal"] * -60 + support["nationalist"] * 40 + support["aristocrat"] * -30 + support["imperialist"] * 20 + support["navigators"] * -100 + support["brotherhood"] * -30 + support["transhumanist"] * -100) / 100;
			//
			//        effects.police = (support["cult"] * 100 + support["technocrat"] * -20 + support["mercantile"] * -60 + support["bureaucracy"] * 40 + support["liberal"] * -100 + support["nationalist"] * 70 + support["aristocrat"] * 80 + support["imperialist"] * 30 + support["navigators"] * -20 + support["brotherhood"] * -40 + support["transhumanist"] * -200) / 100;
			//        effects.violent = (support["cult"] * 100 + support["technocrat"] * -20 + support["mercantile"] * -70 + support["bureaucracy"] * 0 + support["liberal"] * -100 + support["nationalist"] * 100 + support["aristocrat"] * 60 + support["imperialist"] * 30 + support["navigators"] * -50 + support["brotherhood"] * -60 + support["transhumanist"] * 20) / 100;
			//        effects.aristocracy = (support["cult"] * -30 + support["technocrat"] * 10 + support["mercantile"] * 100 + support["bureaucracy"] * 30 + support["liberal"] * -70 + support["nationalist"] * -30 + support["aristocrat"] * 100 + support["imperialist"] * 50 + support["navigators"] * 100 + support["brotherhood"] * 70 + support["transhumanist"] * -100) / 100;
			//        effects.imperialism = (support["cult"] * 80 + support["technocrat"] * -10 + support["mercantile"] * -20 + support["bureaucracy"] * 5 + support["liberal"] * -50 + support["nationalist"] * -100 + support["aristocrat"] * 50 + support["imperialist"] * 100 + support["navigators"] * 10 + support["brotherhood"] * 5 + support["transhumanist"] * -100) / 100;
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
		
		public Dictionary<string, float> support = new Dictionary<string, float>();
		public Effects effects = new Effects();

		// <--- end of copy-paste
    }
}