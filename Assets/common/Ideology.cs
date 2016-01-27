//using System;
using System.Collections.Generic;
using UnityEngine;
//using System.IO;
//using UnityEngine.UI;
//using System.Globalization;

static public class Faction
{
    public enum FactionID { noble1, noble2, noble3, noble4, guild1, guild2, guild3, church, heretic };
    public enum Agenda { loyalist, idealist, faithful, reformist, transhumanist, separatist, nationalist, heretic, individualist } // todo agendas
    public enum IdeologyID { cult, technocrat, mercantile, bureaucracy, liberal, nationalist, aristocrat, imperialist, navigators, brotherhood, transhumanist }

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

    static public string getPartyName(IdeologyID id) // Governor of the [___]
    {
        if (id == IdeologyID.cult) return "Order";
        else if (id == IdeologyID.technocrat) return "Technocrats";
        else if (id == IdeologyID.mercantile) return "Guild of Merchants";
        else if (id == IdeologyID.bureaucracy) return "Bureaucrats";
        else if (id == IdeologyID.liberal) return "Liberals";
        else if (id == IdeologyID.nationalist) return "Nationalists";
        else if (id == IdeologyID.aristocrat) return "Aristocrats";
        else if (id == IdeologyID.imperialist) return "Imperialists";
        else if (id == IdeologyID.navigators) return "Navigator's Guild";
        else if (id == IdeologyID.brotherhood) return "Brotherhood";
        else if (id == IdeologyID.transhumanist) return "Radical Movement";
        Debug.LogError("ERROR");
        return "";
    }
    static public string getPartyDescription(IdeologyID id)
    {
        if (id == IdeologyID.cult) return "Believers are fanatic defenders of the Faith and the Empire. They live modest lives of worship when they're not on some crusade or another.";
        else if (id == IdeologyID.technocrat) return "Technologists are often seen running old and complex machinery, dedicating their lives to understand all that was built to serve mankind.";
        else if (id == IdeologyID.mercantile) return "Merchants are often members of powerful conglomerates who benefit from the fragmented production across the interstellar space. While motivated by self interest, they play an important role in the survival of the sector.";
        else if (id == IdeologyID.bureaucracy) return "Administrators are the pencil pushers and bureaucrats. They excel in running organizations from behind desks.";
        else if (id == IdeologyID.liberal) return "Liberals are diplomats and brilliant minds, often quite theoretical and proponents of radical ideals.";
        else if (id == IdeologyID.nationalist) return "Separatists are fiercely independent and hold little love for the Imperial rule. Industrious, militant and mostly practical, they can be found at the frontiers of the Empire.";
        else if (id == IdeologyID.aristocrat) return "Nobles and aristocrats, the favored few and some say the backbone of the Empire. They are raised to lead the masses, and provided with opportunities to dabble with anything interesting coming their way. ";
        else if (id == IdeologyID.imperialist) return "Loyalists live by the laws and ideals of the Empire. They may appear moderate, but from the masses arise formidable individuals that shape the galaxy.";
        else if (id == IdeologyID.navigators) return "Navigators are an ancient guild. There are many rumors and stories told about them in hushed voices. It is said that their devotion to fracture makes them quite detached.";
        else if (id == IdeologyID.brotherhood) return "The Brotherhood is a secretive order. It is said they're all frackers of great power, and work for some hidden agenda of the Empire. While psychers can create many problems for humanity, there is a measure of stability within the Brotherhood.";
        else if (id == IdeologyID.transhumanist) return "Radicals follow a different vision for the future of mankind, and it's not under the oppressive rule of the Empire. They are unhinged individualists that are said to harbor mutants and heretics among them.";
        Debug.LogError("ERROR");
        return "";
    }
    static public string getFactionDescription(FactionID id, bool hasFlipped = false)
    {
        if (id == FactionID.noble1 && !hasFlipped) return "Furia is an old noble family, older than the colonies on the sector, older even than the Imperium itself. Or that's what they tell you. They are warriors and weapon-makers and masters of clones. Sporting heavily militarized autonome planets, they are always under the scrutiny from the Capital, as well as the Imperial enforcers Church and House Tarquinia.";
        else if (id == FactionID.noble1 && hasFlipped) return "Furia is an old noble family, older than the Imperium. Or that's what they tell you. They are warriors and weapon-makers and masters of clones. As the heirs to Old Furia, they are working to secede from the Empire and for a new nation on the sector. Looking for allies, their support is entrenched in the sector backwaters.";
        else if (id == FactionID.noble2) return "Even the farthest reaches of the Imperium are not exempt from schism and intrigue. It has traditionally been the role of House Rathmund to remain above such conflicts, and offer the services of a neutral mediator to reach just and peaceful solutions. They are clever, and their ships run well and wealthy. They have franchised most deep space stations on the sector and conduct their business with everyone.";
        else if (id == FactionID.noble3) return "Tarquinia is one of the noble families who built the Imperium. They have been given Imperial power to protect the sector and humankind. According to old Tarquinia custom, the good of the Imperium and the good of the family are one and the same. They have close ties to the Church and are considered pious, stern but fair.";
        else if (id == FactionID.noble4) return "House Valeria claim to be originally refugees from the Old Earth, before the Imperium. Despite their nominal military, they are political heavyweight and are said to know things they shouldn't. Their home world houses the Academy, where most talented navigators come from. Church and Tarquinia often oppose Valerian initiative for being too radical. ";
        else if (id == FactionID.guild1) return "Union is a collective of worlds and ideologies. Drawing the eye of the Inquisitors, they are known for their radical policies as well as their brilliant, unorthodox researchers and navigators. Attracting little support from the establishment, they have somehow survived through their popularity among the workers of the sector. With no license for warships, they have to rely on other methods to survive.";
        else if (id == FactionID.guild2) return "The Dacei Family is not a true noble house, but a closed organization that is all about power from the shadows, the politics of the kingmaker and not the king. They are competent determined industrialists and quick to hit the mark. There is some exclusion from noble houses of the sector, as Dacei Family are not nobility.";
        else if (id == FactionID.guild3) return "Many of the players on the sector cloak their business under high ideals, but in the Cartel, trade comes first. There's money to be made, running an interstellar network of exchange. The Cartel works together, because a game that's rigged is a game where all the right people win.";
        else if (id == FactionID.church) return "The Church is the guardian of the Imperial Will. Their ongoing social project is to teach the children of men the correct way of life. It is a life of solemn worship and ascesis, discipline of mind and body. The teachings of the Church once saved mankind from annhilation, and they prepare for the day when we are all tested again.";
        else if (id == FactionID.heretic) return "Outcasts are seclusionists living in the inaccessable pockets of the sector. They are declared enemies of the Imperium, and heretics to the teachings of the Church. Some insist they are the descendants of the original colonists on the sector, before Imperium and even Old Furia. Others say they are Imperial defectors, fallen houses or even Imperial agents conducting sinister projects deep in the Fracture.";
        Debug.LogError("ERROR");
        return "";
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
    static public string getGovernmentType(FactionID id, bool hasFlipped = false)
    {
        switch (id)
        {
            case FactionID.noble1: return (!hasFlipped) ? "Imperial" : "Separatist"; // todo: return "Separatist" if House Furia flipped
            case FactionID.noble2: return "Imperial";
            case FactionID.noble3: return "Imperial";
            case FactionID.noble4: return "Imperial";
            case FactionID.guild1: return "Civilian";
            case FactionID.guild2: return "Civilian";
            case FactionID.guild3: return "Civilian";
            case FactionID.church: return "Theocratic";
            case FactionID.heretic: return (!hasFlipped) ? "Unknown" : "Theocratic"; // todo: has heretics come out of the closet
            default: return "";
        }
    }
    static public string getGovernmentType(Faction.IdeologyID id) // government:
    {
        switch (id)
        {
            case Faction.IdeologyID.cult:        return "Imperial"; 
            case Faction.IdeologyID.technocrat:  return "Civilian";
            case Faction.IdeologyID.mercantile:  return "Civilian";
            case Faction.IdeologyID.bureaucracy: return "Imperial";
            case Faction.IdeologyID.liberal:     return "Civilian";
            case Faction.IdeologyID.nationalist: return "Separatist";
            case Faction.IdeologyID.aristocrat:  return "Imperial";
            case Faction.IdeologyID.imperialist: return "Imperial";
            case Faction.IdeologyID.navigators:  return "Order";
            case Faction.IdeologyID.brotherhood: return "Order";
            case Faction.IdeologyID.transhumanist: return "Civilian";
            default: return "";
        }
    }

    static public string getTitle(FactionID id, int lvl)
    {
        // 4th title comes easier for factions!

        if (id == FactionID.noble1)      return getTitle (Faction.IdeologyID.cult, lvl);
        else if (id == FactionID.noble2) return getTitle (Faction.IdeologyID.cult, lvl);
        else if (id == FactionID.noble3) return getTitle (Faction.IdeologyID.cult, lvl);
        else if (id == FactionID.noble4) return getTitle (Faction.IdeologyID.cult, lvl);
        else if (id == FactionID.guild1)
        {
            if (lvl <= 2)       return "Councillor";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "President";
            else                return "Primus Transcended";
        }
        else if (id == FactionID.guild2) return getTitle (Faction.IdeologyID.cult, lvl);
        else if (id == FactionID.guild3) return getTitle (Faction.IdeologyID.cult, lvl);
        else if (id == FactionID.church)
        {
            if (lvl <= 2)       return "Bishop";
            else if (lvl <= 3)  return "Bishop";
            else if (lvl <= 4)  return "Archbishop";
            else                return "Apostle";
        }
        else if (id == FactionID.heretic)
        {
            if (lvl <= 2)       return "Councillor";
            else if (lvl <= 3)  return "Protector";
            else if (lvl <= 4)  return "Messiah";
            else                return "Antipope";
        }
        return "Governor";
    }
    static public string getTitle(Faction.IdeologyID id, int lvl)
    {
        if (id == Faction.IdeologyID.cult) 
        {
            if (lvl <= 2)       return "Senator";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Faction.IdeologyID.technocrat)   
        {
            if (lvl <= 2)       return "Councillor";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Faction.IdeologyID.mercantile)   
        {
            if (lvl <= 2)       return "Councillor";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Faction.IdeologyID.bureaucracy)  
        {
            if (lvl <= 2)       return "Senator";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Faction.IdeologyID.liberal)      
        {
            if (lvl <= 2)       return "Minister";
            else if (lvl <= 3)  return "Prime Minister";
            else if (lvl <= 4)  return "President";
            else                return "Sector President";
        }
        else if (id == Faction.IdeologyID.nationalist)  
        {
            if (lvl <= 2)       return "Councillor";
            else if (lvl <= 3)  return "Commissar";
            else if (lvl <= 4)  return "People's Commissar";
            else                return "High Commander";
        }
        else if (id == Faction.IdeologyID.aristocrat)   
        {
            if (lvl <= 2)       return "Senator";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Faction.IdeologyID.imperialist)  
        {
            if (lvl <= 2)       return "Senator";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Faction.IdeologyID.navigators)   
        {
            if (lvl <= 2)       return "Senior Advisor";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Faction.IdeologyID.brotherhood)  
        {
            if (lvl <= 2)       return "Senior Advisor";
            else if (lvl <= 3)  return "Governor";
            else if (lvl <= 4)  return "Exarch";
            else                return "High Exarch";
        }
        else if (id == Faction.IdeologyID.transhumanist)
        {
            if (lvl <= 2)       return "Minister";
            else if (lvl <= 3)  return "Prime Minister";
            else if (lvl <= 4)  return "Transcended";
            else                return "Primus Transcended";
        }
        return "Governor";
    }
    //static public string getTitle() { return "Governor"; }

    static public string getImportanceDescription(Location location)
    {
        string rv = "";
        float importance = Simulation.Parameters.getImportance(location);

        // primitive world
        if (location.economy.technologies[Data.Tech.Type.Technology].level == 0) rv += "Primitive";

        // station
        else if (location.features.isStation())
        {
            if (importance < 1f) rv += "Border";
            else if (importance < 2f) rv += "Minor";
            else if (importance < 3f) rv += "Midway";
            else if (importance < 4f) rv += "Major";
            else if (importance < 5f) rv += "Central";
            else rv += "Capital";
            rv += " Station";
        }

        // standard planetary world
        else if (importance < 1f) rv += "Border";
        else if (importance < 2f) rv += "Minor";
        else if (importance < 3f) rv += "Major";
        else if (importance < 4f) rv += "Central";
        else if (importance < 5f) rv += "Core";
        else rv += "Capital";

        if (importance < 5f)
        {
            float industry = location.economy.getEffectiveMul(Data.Resource.Type.Industry);
            float food = location.economy.getEffectiveMul(Data.Resource.Type.Food);
            if (industry > 1.2f && industry > food) rv += " Industrial";
            else if (food > 1.2f && food > industry) rv += " Agri";
            else
            {
                if (location.economy.getEffectiveMul(Data.Resource.Type.Innovation) > 1.2f) rv += " Tech";
                else if (location.economy.getEffectiveMul(Data.Resource.Type.Mineral) > 1.2f) rv += " Mining";
                else if (location.economy.getEffectiveMul(Data.Resource.Type.Economy) > 1.2f) rv += " Financial";
                else if (location.economy.getEffectiveMul(Data.Resource.Type.Culture) > 1.2f) rv += " Manufacturing";
                else if (location.economy.getEffectiveMul(Data.Resource.Type.Military) > 1.2f) rv += " Contractor";
                else if (location.economy.getEffectiveMul(Data.Resource.Type.BlackMarket) > 1.3f) rv += " Black Market";
            }
        }
        rv += " World";

        return rv;
    }
    static public string getTechDescription(Location location, Data.Tech.Type techType, bool getOnlyDescription = false)
    {
        string rv = "";

        if (techType == Data.Tech.Type.Infrastructure)
        {
            if (!getOnlyDescription) rv += "Infrastructure:  ";
            if (location.economy.technologies[Data.Tech.Type.Infrastructure].level == 0) rv += "None";
            else if (location.economy.technologies[Data.Tech.Type.Infrastructure].level == 1) rv += "Poor";
            else if (location.economy.technologies[Data.Tech.Type.Infrastructure].level == 2) rv += "Standard";
            else if (location.economy.technologies[Data.Tech.Type.Infrastructure].level == 3) rv += "Developed";
            else if (location.economy.technologies[Data.Tech.Type.Infrastructure].level == 4) rv += "Planetary";
        }
        else if (techType == Data.Tech.Type.Technology)
        {
            if (!getOnlyDescription) rv += "Tech Level:  ";
            if (location.economy.technologies[Data.Tech.Type.Technology].level == 0) rv += "Primitive";
            else if (location.economy.technologies[Data.Tech.Type.Technology].level == 1) rv += "Industrial";
            else if (location.economy.technologies[Data.Tech.Type.Technology].level == 2) rv += "Standard";
            else if (location.economy.technologies[Data.Tech.Type.Technology].level == 3) rv += "High";
            else if (location.economy.technologies[Data.Tech.Type.Technology].level == 4) rv += "Core Tech";
        }
        else if (techType == Data.Tech.Type.Military)
        {
            if (!getOnlyDescription) rv += "Military Tech:  ";
            if (location.economy.technologies[Data.Tech.Type.Military].level == 0) rv += "Primitive";
            else if (location.economy.technologies[Data.Tech.Type.Military].level == 1) rv += "Simple";
            else if (location.economy.technologies[Data.Tech.Type.Military].level == 2) rv += "Standard";
            else if (location.economy.technologies[Data.Tech.Type.Military].level == 3) rv += "Advanced";
            else if (location.economy.technologies[Data.Tech.Type.Military].level == 4) rv += "Core Tech";
        }
        return rv;
    }


    static public Sprite getFactionLogo(Faction.FactionID faction, bool isSmall = false)
    {
        string iconName =
            (faction == Faction.FactionID.noble1) ? "Logo_Faction_Furia" :
            (faction == Faction.FactionID.noble2) ? "Logo_Faction_Rathmund" :
            (faction == Faction.FactionID.noble3) ? "Logo_Faction_Tarquinia" :
            (faction == Faction.FactionID.noble4) ? "Logo_Faction_Valeria" :
            (faction == Faction.FactionID.guild1) ? "Logo_Faction_Union" :
            (faction == Faction.FactionID.guild2) ? "Logo_Faction_Dacei" :
            (faction == Faction.FactionID.guild3) ? "Logo_Faction_Coruna Cartel" :
            (faction == Faction.FactionID.church) ? "Logo_Faction_Church" :
            (faction == Faction.FactionID.heretic) ? "Logo_Faction_Heretics" :
            "";
        if (isSmall) iconName += "_s";
        //Debug.Log(String.Format("ui{0}factions{0}{1}", Path.DirectorySeparatorChar, iconName));
        //return Resources.Load<Sprite>(String.Format("ui{0}factions{0}{1}", Path.DirectorySeparatorChar, iconName));
        return Resources.Load<Sprite>("ui/factions/" + iconName);
    }
    static public string getIdeologyList(Location location)
    {
        string rv = "";
        foreach (var ideology in location.ideology.support)
        {
            if (ideology.Value > 0.0f)
            {
                rv += ideology.Key.ToString() +": "+ Mathf.Round(ideology.Value*100f) +"%\n";
            }
        }
        return rv;
    }
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
