using System;
using System.Collections.Generic;
using System.Globalization;

public class Faction
{
    public enum FactionID { noble1, noble2, noble3, noble4, guild1, guild2, guild3, church, heretic };
    public enum IdeologyID { cult, technocrat, mercantile, bureaucracy, liberal, nationalist, aristocrat, imperialist, navigators, brotherhood, transhumanist }

    static public string getFactionName(Faction.FactionID id)
    {
        switch(id)
        {
            case FactionID.noble1: return "House Furia";
            case FactionID.noble2: return "House Rathmund";
            case FactionID.noble3: return "House Valeria";
            case FactionID.noble4: return "House Tarquinia";
            case FactionID.guild1: return "Everlasting Union";
            case FactionID.guild2: return "Dacei Family";
            case FactionID.guild3: return "Coruna Cartel";
            case FactionID.church: return "Church";
            case FactionID.heretic: return "Heretics";
            default: return "";
        }
    }
    static public string getFactionName(string id)
    {
        Dictionary<string, string> names = new Dictionary<string, string>()
        {
            {"noble1", "House Furia"},
            {"noble2", "House Rathmund"},
            {"noble3", "House Valeria"},
            {"noble4", "House Tarquinia"},
            {"guild1", "Everlasting Union"},
            {"guild2", "Dacei Family"},
            {"guild3", "Coruna Cartel"},
            {"church", "Church"},
            {"cult", "Heretics"}
        };
        return names[id];
    }
    static public string factionToString (FactionID faction)
    {
        switch(faction)
        {
            case FactionID.noble1: return "noble1";
            case FactionID.noble2: return "noble2";
            case FactionID.noble3: return "noble3";
            case FactionID.noble4: return "noble4";
            case FactionID.guild1: return "guild1";
            case FactionID.guild2: return "guild2";
            case FactionID.guild3: return "guild3";
            case FactionID.church: return "church";
            case FactionID.heretic: return "cult";
            default: return "";
        }
    }
    static public FactionID factionToEnum (string faction)
    {
        switch(faction)
        {
            case "noble1": return FactionID.noble1;
            case "noble2": return FactionID.noble2;
            case "noble3": return FactionID.noble3;
            case "noble4": return FactionID.noble4;
            case "guild1": return FactionID.guild1;
            case "guild2": return FactionID.guild2;
            case "guild3": return FactionID.guild3;
            case "church": return FactionID.church;
            case "cult": return FactionID.heretic;
        default: return FactionID.noble1;
        }
    }
    static public string getTitle(string id)
    {
        Dictionary<string, string> names = new Dictionary<string, string>()
        {
            {"noble1", "Governor"},
            {"noble2", "Governor"},
            {"noble3", "Governor"},
            {"noble4", "Governor"},
            {"guild1", "Governor"},
            {"guild2", "Governor"},
            {"guild3", "Governor"},
            {"church", "Bishop"},
            {"cult", "Protector"}
        };
        return names[id];
    }
}

public class FactionData
{
    //public Dictionary<string, float> control = new Dictionary<string, float>();
    public Dictionary<string, string> ruler = new Dictionary<string, string>();

    static public string[] getFactionIds()
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
    public FactionData(string data)
    {
        /*
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
*/
        // randomize rulers
        ruler["noble1"] = NameGenerator.getName("noble1");
        ruler["noble2"] = NameGenerator.getName("noble2");
        ruler["noble3"] = NameGenerator.getName("noble3");
        ruler["noble4"] = NameGenerator.getName("noble4");
        ruler["guild1"] = NameGenerator.getName("guild1");
        ruler["guild2"] = NameGenerator.getName("guild2");
        ruler["guild3"] = NameGenerator.getName("guild3");
        ruler["church"] = NameGenerator.getName("church");
        ruler["cult"] = NameGenerator.getName("cult");
    }
    /*
    public float getTotalControl()
    {
        float rv = 0.0f;
        foreach (float value in control.Values)
        {
            rv += value;
        }
        return rv;
    }
    public string getStrongest()
    {
        string rv = "";
        float currentPick = 0.0f;
        foreach(KeyValuePair<string, float> pair in control)
        {
            if (pair.Value > currentPick)
            {
                rv = pair.Key;
                currentPick = pair.Value;
            }
        }
        return rv;
    }
    */
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


    public Dictionary<Data.Resource.Type, float> resourceMultiplier =
        new Dictionary<Data.Resource.Type, float>(); 
    public Dictionary<string, float> support = new Dictionary<string, float>();
    public Effects effects = new Effects();

    public IdeologyData()
    {
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

        resourceMultiplier[Data.Resource.Type.Food]        = effects.pgrowth +1;
        resourceMultiplier[Data.Resource.Type.Mineral]     = effects.industry +1;
        resourceMultiplier[Data.Resource.Type.BlackMarket] = 1-effects.altruism;
        resourceMultiplier[Data.Resource.Type.Innovation]  = effects.innovation +1;
        resourceMultiplier[Data.Resource.Type.Culture]     = (effects.diplomacy+effects.happiness+effects.altruism)/3 +1;
        resourceMultiplier[Data.Resource.Type.Industry]    = effects.industry +1;
        resourceMultiplier[Data.Resource.Type.Economy]     = effects.economy +1;
        resourceMultiplier[Data.Resource.Type.Military]    = effects.military +1;
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