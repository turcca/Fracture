using System;
using System.Collections.Generic;
using System.Globalization;

public class Faction
{
    public enum FactionID { noble1, noble2, noble3, noble4, guild1, guild2, guild3, church, heretic };

    static public string getFactionName(Faction.FactionID id)
    {
        switch(id)
        {
            case FactionID.noble1: return "House Furia";
            case FactionID.noble2: return "House Rathmund";
            case FactionID.noble3: return "House Tarquinia";
            case FactionID.noble4: return "House Valeria";
            case FactionID.guild1: return "Everlasting Union";
            case FactionID.guild2: return "Dacei Family";
            case FactionID.guild3: return "Coruna Cartel";
            case FactionID.church: return "Church";
            case FactionID.heretic: return "Radical Movement";
            default: return "";
        }
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
    static public string getTitle(FactionID id, int lvl)
    {
        // 4th title comes easier for factions!

        if (id == FactionID.noble1)      return getTitle (Simulation.LocationIdeology.IdeologyID.cult, lvl);
        else if (id == FactionID.noble2) return getTitle (Simulation.LocationIdeology.IdeologyID.cult, lvl);
        else if (id == FactionID.noble3) return getTitle (Simulation.LocationIdeology.IdeologyID.cult, lvl);
        else if (id == FactionID.noble4) return getTitle (Simulation.LocationIdeology.IdeologyID.cult, lvl);
        else if (id == FactionID.guild1)
        {
            if (lvl == 1)       return "Councillor";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 3)  return "President";
            else                return "Primus Transcended";
        }
        else if (id == FactionID.guild2) return getTitle (Simulation.LocationIdeology.IdeologyID.cult, lvl);
        else if (id == FactionID.guild3) return getTitle (Simulation.LocationIdeology.IdeologyID.cult, lvl);
        else if (id == FactionID.church)
        {
            if (lvl == 1)       return "Bishop";
            else if (lvl == 2)  return "Bishop";
            else if (lvl <= 3)  return "Arch Bishop";
            else                return "High Exarch";
        }
        else if (id == FactionID.heretic)
        {
            if (lvl == 1)       return "Councillor";
            else if (lvl == 2)  return "Protector";
            else if (lvl <= 3)  return "Messiah";
            else                return "Antipope";
        }
        return "Governor";
    }
    static public string getTitle(Simulation.LocationIdeology.IdeologyID id, int lvl)
    {
        if (id == Simulation.LocationIdeology.IdeologyID.cult) 
        {
            if (lvl == 1)       return "Senator";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.technocrat)   
        {
            if (lvl == 1)       return "Councillor";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.mercantile)   
        {
            if (lvl == 1)       return "Councillor";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.bureaucracy)  
        {
            if (lvl == 1)       return "Senator";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.liberal)      
        {
            if (lvl == 1)       return "Minister";
            else if (lvl == 2)  return "Prime Minister";
            else if (lvl <= 4)  return "President";
            else                return "Sector President";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.nationalist)  
        {
            if (lvl == 1)       return "Councillor";
            else if (lvl == 2)  return "Commissar";
            else if (lvl <= 4)  return "People's Commissar";
            else                return "High Commander";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.aristocrat)   
        {
            if (lvl == 1)       return "Senator";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.imperialist)  
        {
            if (lvl == 1)       return "Senator";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.navigators)   
        {
            if (lvl == 1)       return "Senior Advisor";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.brotherhood)  
        {
            if (lvl == 1)       return "Senior Advisor";
            else if (lvl == 2)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Simulation.LocationIdeology.IdeologyID.transhumanist)
        {
            if (lvl == 1)       return "Minister";
            else if (lvl == 2)  return "Prime Minister";
            else if (lvl <= 4)  return "Transcended";
            else                return "Primus Transcended";
        }
        return "Governor";
    }
    static public string getTitle() { return "Governor"; }
    /*
    static string getTitle(FactionID id, int lvl)
    {
        if (id == FactionID.noble1)      return getTitle (Simulation.LocationIdeology.IdeologyID.cult, lvl);
        else if (id == FactionID.noble2) return "Senator";
        else if (id == FactionID.noble3) return "Senator";
        else if (id == FactionID.noble4) return "Senator";
        else if (id == FactionID.guild1) return "Councillor";
        else if (id == FactionID.guild2) return "Councillor";
        else if (id == FactionID.guild3) return "Councillor";
        else if (id == FactionID.church) return "Bishop";
        else if (id == FactionID.heretic)return "Councillor";
        return "Governor";
    }
    */
}


public class FactionData
{
    // faction rulers
    public Dictionary<Faction.FactionID, string> ruler = new Dictionary<Faction.FactionID, string>();


    public FactionData(string data)
    {
        // randomize rulers -- get the HQ -location ruler?
        ruler[Faction.FactionID.noble1] = NameGenerator.getName(Faction.FactionID.noble1);
        ruler[Faction.FactionID.noble2] = NameGenerator.getName(Faction.FactionID.noble2);
        ruler[Faction.FactionID.noble3] = NameGenerator.getName(Faction.FactionID.noble3);
        ruler[Faction.FactionID.noble4] = "Calius Valeria"; //NameGenerator.getName(Faction.FactionID.noble4);
        ruler[Faction.FactionID.guild1] = NameGenerator.getName(Faction.FactionID.guild1);
        ruler[Faction.FactionID.guild2] = NameGenerator.getName(Faction.FactionID.guild2);
        ruler[Faction.FactionID.guild3] = NameGenerator.getName(Faction.FactionID.guild3);
        ruler[Faction.FactionID.church] = NameGenerator.getName(Faction.FactionID.church);
        ruler[Faction.FactionID.heretic]= NameGenerator.getName(Faction.FactionID.heretic);
    }

}
