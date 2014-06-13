using System;
using System.Collections.Generic;
using System.Globalization;

public class GroupData
{
    public Dictionary<string, float> control = new Dictionary<string, float>();

    static public string[] getGroupNames()
    {
        string[] rv = new string[]
        {
            "noble1",
            "noble2",
            "noble3",
            "noble4",
            "guild1",
            "guild2",
            "guild3",
            "church",
            "heretic"
        };
        return rv;
    }
    public GroupData(string data)
    {
        string[] dataChunk = data.Split(',');
        control["noble1"] = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        control["noble2"] = float.Parse(dataChunk[1], CultureInfo.InvariantCulture.NumberFormat);
        control["noble3"] = float.Parse(dataChunk[2], CultureInfo.InvariantCulture.NumberFormat);
        control["noble4"] = float.Parse(dataChunk[3], CultureInfo.InvariantCulture.NumberFormat);
        control["guild1"] = float.Parse(dataChunk[4], CultureInfo.InvariantCulture.NumberFormat);
        control["guild2"] = float.Parse(dataChunk[5], CultureInfo.InvariantCulture.NumberFormat);
        control["guild3"] = float.Parse(dataChunk[6], CultureInfo.InvariantCulture.NumberFormat);
        control["church"] = float.Parse(dataChunk[7], CultureInfo.InvariantCulture.NumberFormat);
        control["heretic"] = float.Parse(dataChunk[8], CultureInfo.InvariantCulture.NumberFormat);
    }

    public float getTotalControl()
    {
        float rv = 0.0f;
        foreach (float value in control.Values)
        {
            rv += value;
        }
        return rv;
    }
}

public class IdeologyData
{
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

    public IdeologyData()
    {
        foreach (string ideology in getIdeologyNames())
        {
            support[ideology] = 0.0f;
        }
        calculateEffects();
    }

    public void calculateEffects()
    {
        effects.pgrowth = (support["cult"] * 100 + support["technocrat"] * -60 + support["mercantile"] * -100 + support["bureaucracy"] * 60 + support["liberal"] * -100 + support["nationalist"] * 80 + support["aristocrat"] * 70 + support["imperialist"] * 20 + support["navigators"] * -300 + support["brotherhood"] * -70 + support["transhumanist"] * 60) / 100;
        effects.industry = (support["cult"] * 20 + support["technocrat"] * 100 + support["mercantile"] * 30 + support["bureaucracy"] * 80 + support["liberal"] * -40 + support["nationalist"] * 80 + support["aristocrat"] * 20 + support["imperialist"] * -10 + support["navigators"] * -50 + support["brotherhood"] * -100 + support["transhumanist"] * 0) / 100;
        effects.economy = (support["cult"] * -100 + support["technocrat"] * 30 + support["mercantile"] * 100 + support["bureaucracy"] * 60 + support["liberal"] * -30 + support["nationalist"] * -10 + support["aristocrat"] * 40 + support["imperialist"] * -10 + support["navigators"] * 50 + support["brotherhood"] * 0 + support["transhumanist"] * -20) / 100;
        effects.diplomacy = (support["cult"] * -100 + support["technocrat"] * 0 + support["mercantile"] * 200 + support["bureaucracy"] * 10 + support["liberal"] * 200 + support["nationalist"] * -100 + support["aristocrat"] * -20 + support["imperialist"] * 20 + support["navigators"] * 100 + support["brotherhood"] * 20 + support["transhumanist"] * 150) / 100;
        effects.happiness = (support["cult"] * -30 + support["technocrat"] * 20 + support["mercantile"] * 70 + support["bureaucracy"] * 0 + support["liberal"] * 100 + support["nationalist"] * 30 + support["aristocrat"] * -50 + support["imperialist"] * -20 + support["navigators"] * 20 + support["brotherhood"] * 10 + support["transhumanist"] * 100) / 100;
        effects.affluence = (support["cult"] * -70 + support["technocrat"] * 30 + support["mercantile"] * 100 + support["bureaucracy"] * 10 + support["liberal"] * 60 + support["nationalist"] * -30 + support["aristocrat"] * 30 + support["imperialist"] * 0 + support["navigators"] * 60 + support["brotherhood"] * 0 + support["transhumanist"] * -10) / 100;
        effects.innovation = (support["cult"] * -100 + support["technocrat"] * 70 + support["mercantile"] * 20 + support["bureaucracy"] * -10 + support["liberal"] * 100 + support["nationalist"] * -20 + support["aristocrat"] * -30 + support["imperialist"] * -20 + support["navigators"] * 70 + support["brotherhood"] * 50 + support["transhumanist"] * 100) / 100;
        effects.morale = (support["cult"] * 100 + support["technocrat"] * -20 + support["mercantile"] * -90 + support["bureaucracy"] * -20 + support["liberal"] * -100 + support["nationalist"] * 80 + support["aristocrat"] * 50 + support["imperialist"] * 30 + support["navigators"] * -20 + support["brotherhood"] * -10 + support["transhumanist"] * -100) / 100;
        effects.altruism = (support["cult"] * 80 + support["technocrat"] * 20 + support["mercantile"] * -100 + support["bureaucracy"] * -60 + support["liberal"] * 100 + support["nationalist"] * 20 + support["aristocrat"] * -50 + support["imperialist"] * -20 + support["navigators"] * -30 + support["brotherhood"] * -10 + support["transhumanist"] * 0) / 100;
        effects.military = (support["cult"] * 100 + support["technocrat"] * 20 + support["mercantile"] * -60 + support["bureaucracy"] * 10 + support["liberal"] * -100 + support["nationalist"] * 100 + support["aristocrat"] * 80 + support["imperialist"] * 40 + support["navigators"] * 20 + support["brotherhood"] * 0 + support["transhumanist"] * -50) / 100;

        effects.holy = (support["cult"] * 100 + support["technocrat"] * -20 + support["mercantile"] * -40 + support["bureaucracy"] * -10 + support["liberal"] * -60 + support["nationalist"] * 0 + support["aristocrat"] * -30 + support["imperialist"] * 60 + support["navigators"] * 0 + support["brotherhood"] * 10 + support["transhumanist"] * -100) / 100;
        effects.psych = (support["cult"] * -70 + support["technocrat"] * -100 + support["mercantile"] * 20 + support["bureaucracy"] * -20 + support["liberal"] * 40 + support["nationalist"] * -50 + support["aristocrat"] * 30 + support["imperialist"] * 20 + support["navigators"] * 100 + support["brotherhood"] * 200 + support["transhumanist"] * 60) / 100;
        effects.navigation = (support["cult"] * -40 + support["technocrat"] * 70 + support["mercantile"] * 100 + support["bureaucracy"] * -10 + support["liberal"] * 20 + support["nationalist"] * -100 + support["aristocrat"] * 10 + support["imperialist"] * 30 + support["navigators"] * 200 + support["brotherhood"] * 60 + support["transhumanist"] * 40) / 100;
        effects.purity = (support["cult"] * 100 + support["technocrat"] * -50 + support["mercantile"] * -60 + support["bureaucracy"] * 10 + support["liberal"] * -60 + support["nationalist"] * 40 + support["aristocrat"] * -30 + support["imperialist"] * 20 + support["navigators"] * -100 + support["brotherhood"] * -30 + support["transhumanist"] * -100) / 100;

        effects.police = (support["cult"] * 100 + support["technocrat"] * -20 + support["mercantile"] * -60 + support["bureaucracy"] * 40 + support["liberal"] * -100 + support["nationalist"] * 70 + support["aristocrat"] * 80 + support["imperialist"] * 30 + support["navigators"] * -20 + support["brotherhood"] * -40 + support["transhumanist"] * -200) / 100;
        effects.violent = (support["cult"] * 100 + support["technocrat"] * -20 + support["mercantile"] * -70 + support["bureaucracy"] * 0 + support["liberal"] * -100 + support["nationalist"] * 100 + support["aristocrat"] * 60 + support["imperialist"] * 30 + support["navigators"] * -50 + support["brotherhood"] * -60 + support["transhumanist"] * 20) / 100;
        effects.aristocracy = (support["cult"] * -30 + support["technocrat"] * 10 + support["mercantile"] * 100 + support["bureaucracy"] * 30 + support["liberal"] * -70 + support["nationalist"] * -30 + support["aristocrat"] * 100 + support["imperialist"] * 50 + support["navigators"] * 100 + support["brotherhood"] * 70 + support["transhumanist"] * -100) / 100;
        effects.imperialism = (support["cult"] * 80 + support["technocrat"] * -10 + support["mercantile"] * -20 + support["bureaucracy"] * 5 + support["liberal"] * -50 + support["nationalist"] * -100 + support["aristocrat"] * 50 + support["imperialist"] * 100 + support["navigators"] * 10 + support["brotherhood"] * 5 + support["transhumanist"] * -100) / 100;
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
}