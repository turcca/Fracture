using UnityEngine;
using System;
using System.Collections.Generic;


public class ShipBonusItem
{
    /// <summary>
    /// bonus type, eg. "Happiness"
    /// </summary>
    public ShipBonus bonusType;
    /// <summary>
    /// color of the item
    /// </summary>
    public Color32 color;
    /// <summary>
    /// item name
    /// </summary>
    public string text;
    /// <summary>
    /// item bonus value
    /// -1 / +1 / +2
    /// </summary>
    public int value;
    /// <summary>
    /// source of the bonus
    /// character / utility
    /// </summary>
    public string source;

    // constructors
    public ShipBonusItem()
    {
        bonusType = ShipBonus.None;
        color = new Color32(64, 64, 64, 128);
        text = "";
        value = 0;
        source = "";
    }
    public ShipBonusItem(ShipBonus bonusType)
    {
        // arch-type
        this.bonusType = bonusType;
        color = ShipBonuses.getBonusColor(bonusType);
        color.a = 230; // brighten arch-types
        text = bonusType.ToString();
        value = Root.game.player.shipBonuses.getTotalBonus(bonusType);
        source = ShipBonusesStats.getBonusDescription(bonusType, value);
    }
}


public class ShipBonuses
{
    // eg. "Happiness", relevant items
    Dictionary<ShipBonus, List<ShipBonusItem>> items = new Dictionary<ShipBonus, List<ShipBonusItem>>();


    public ShipBonuses()
    {
        foreach (ShipBonus bonus in Enum.GetValues(typeof(ShipBonus))) items.Add(bonus, new List<ShipBonusItem>());
    }

    public int getTotalBonus(string parseEnum)
    {
        ShipBonus? bonusType = null;
        ShipBonusCathegory? bonusCathegory = null;

        // check if ShipBonus
        try
        {
            bonusType = (ShipBonus?)Enum.Parse(typeof(ShipBonus), parseEnum);
        }
        catch (Exception)
        {
            //throw;
        }
        if (bonusType != null) return getTotalBonus((ShipBonus)bonusType);
        // check if ShipBonusCathegory
        bonusCathegory = (ShipBonusCathegory?) Enum.Parse(typeof(ShipBonusCathegory), parseEnum);
        if (bonusCathegory != null) return getTotalBonus((ShipBonusCathegory)bonusCathegory);
        
        Debug.LogError("Unable to parse ('" + parseEnum + "') into 'ShipBonus' or 'ShipBonusCathegory'");
        return 0;
    }
    /// <summary>
    /// ship (int)value for particular bonus
    /// eg. getTotalBonus(ShipBonus.Happiness) = +2
    /// </summary>
    /// <param name="bonusType"></param>
    /// <returns></returns>
    public int getTotalBonus(ShipBonus bonusType) 
    {
        int rn = 0;
        foreach (ShipBonusItem item in getItems(bonusType)) rn += item.value; 
        return rn;
    }
    /// <summary>
    /// ship (int)value for particular bonus
    /// eg. getTotalBonus(ShipBonusCathegory.Crew) = +3
    /// </summary>
    /// <param name="bonusCathegory"></param>
    /// <returns></returns>
    public int getTotalBonus(ShipBonusCathegory bonusCathegory)
    {
        int rn = 0;
        foreach (ShipBonus bonusType in getBonusesByCathegory(bonusCathegory))
        {
            rn += getTotalBonus(bonusType);
        }
        return rn;
    }
    internal List<ShipBonusItem> getItems(ShipBonus bonusType)
    {
        if (items.ContainsKey(bonusType) == false)
            Debug.LogError("Fetching un-initiated dictionary 'items/ "+bonusType+"'");

        List < ShipBonusItem > bonusItems = new List<ShipBonusItem>(items[bonusType]);
        return bonusItems;  
    }

    // need to populate with data / PlayerShip values
    public void updateItems()
    {
        // clear old lists
        foreach (List<ShipBonusItem> list in items.Values)
            list.Clear();

        // item template
        ShipBonusItem item = null;

        // update from utilities
        foreach (Utility util in Root.game.player.playerShip.getUtilities())
        {
            foreach (ShipBonus bonus in Enum.GetValues(typeof(ShipBonus)))
            {
                if (bonus != ShipBonus.None)
                {
                    //item = new ShipBonusItem(bonus, util);
                    item = ShipBonusesStats.getUtilityBonusItem(util, bonus);
                    if (item != null && item.bonusType != ShipBonus.None) { items[bonus].Add(item); }//Debug.Log("added Util item --> "+items[bonus][items[bonus].Count-1].text); }
                }
            }
        }

        // update from characters
        foreach (Character character in Root.game.player.getAdvisors())
        {
            //Debug.Log("Advisor: " + character.name + " (job: "+character.assignment+") (trait: " + character.characterTrait + ")");
            if (character.assignment != Character.Job.none && character.characterTrait != CharacterTrait.None)
            {
                //Debug.Log("Advisor: " + character.name + " (trait: "+character.characterTrait+")");
                foreach (ShipBonus bonus in Enum.GetValues(typeof(ShipBonus)))
                {
                    if (bonus != ShipBonus.None)
                    {
                        item = ShipBonusesStats.getCharacterBonusItem(character, bonus);
                        if (item != null && item.bonusType != ShipBonus.None) { items[bonus].Add(item); }//Debug.Log("added Char item --> " + items[bonus][items[bonus].Count - 1].text); }
                    }
                }
            }
        }
        // update from ship population
        foreach (ShipBonus bonus in Enum.GetValues(typeof(ShipBonus)))
        {
            if (bonus != ShipBonus.None)
            {
                item = ShipBonusesStats.getCrewBonusItem(Root.game.player.playerCrew, bonus);
                if (item != null && item.bonusType != ShipBonus.None) { items[bonus].Add(item); }
            }
        }
        // debug
        //if (true)
        //{
        //    string s = "!! items dictionary ShipBonsItems !!\n";
        //    foreach (List<ShipBonusItem> list in items.Values)
        //    {
        //        foreach (ShipBonusItem item_ in list)
        //        {
        //            s += item_.text + "\n";
        //        }
        //    }
        //    Debug.Log(s);
        //}
    }

    internal static Color32 getBonusColor(ShipBonus bonus)
    {
        Color32 color = getBonusColor(getBonusCathegory(bonus));
        color.a = 115;
        return color;
    }
    internal static Color32 getBonusColor(ShipBonusCathegory cathegory)
    {
        switch (cathegory)
        {
            case ShipBonusCathegory.Crew:
                return new Color32(147, 196, 125, 255); // green
            case ShipBonusCathegory.Ship:
                return new Color32(230, 145, 56, 255); // orange
            case ShipBonusCathegory.Fracture:
                return new Color32(142, 104, 195, 255); // purple
            case ShipBonusCathegory.Relations:
                return new Color32(225, 183, 36, 255); // yellow
            default:
                return new Color32(32, 32, 32, 255);
        }
    }
    internal static Color32 getBonusColor(Character.Job job)
    {
        switch (job)
        {
            case Character.Job.none:
                return new Color32(32, 32, 32, 255); // (dark)
            case Character.Job.captain:
                return new Color32(204, 65, 37, 255); // red
            case Character.Job.navigator:
                return new Color32(166, 77, 121, 255); // purple
            case Character.Job.engineer:
                return new Color32(230, 145, 56, 255); // orange
            case Character.Job.security:
                return new Color32(61, 133, 200, 255); // blue
            case Character.Job.quartermaster:
                return new Color32(150, 200, 125, 255); // green
            case Character.Job.priest:
                return new Color32(217, 217, 217, 255); // light grey
            case Character.Job.psycher:
                return new Color32(142, 124, 200, 255); // violet
            default:
                return new Color32(32, 32, 32, 255); // (dark)
        }
    }
    internal static ShipBonusCathegory getBonusCathegory(ShipBonus bonus)
    {
        if (bonus == ShipBonus.Happiness || bonus == ShipBonus.Morale)
            return ShipBonusCathegory.Crew;
        if (bonus == ShipBonus.Weapons || bonus == ShipBonus.Core)
            return ShipBonusCathegory.Ship;
        if (bonus == ShipBonus.Link || bonus == ShipBonus.Speed || bonus == ShipBonus.Holiness)
            return ShipBonusCathegory.Fracture;
        if (bonus == ShipBonus.Diplomacy || bonus == ShipBonus.Trade)
            return ShipBonusCathegory.Relations;
        else {
            Debug.LogError("ERROR: bonus fall through: "+bonus);
            return ShipBonusCathegory.Crew; }
    }
    internal static ShipBonus[] getBonusesByCathegory(ShipBonusCathegory cathegory)
    {
        switch (cathegory)
        {
            case ShipBonusCathegory.Crew:
                return new ShipBonus[] { ShipBonus.Happiness, ShipBonus.Morale };
            case ShipBonusCathegory.Ship:
                return new ShipBonus[] { ShipBonus.Weapons, ShipBonus.Core };
            case ShipBonusCathegory.Fracture:
                return new ShipBonus[] { ShipBonus.Link, ShipBonus.Speed, ShipBonus.Holiness };
            case ShipBonusCathegory.Relations:
                return new ShipBonus[] { ShipBonus.Diplomacy, ShipBonus.Trade };
            default:
                return new ShipBonus[0];
        }
    }
    internal static bool traitBelongsToJob(CharacterTrait trait, Character.Job job)
    {
        switch (job)
        {
            case Character.Job.none:
                return false;
            case Character.Job.captain:
                if (trait == CharacterTrait.PersonalLeadership || trait == CharacterTrait.Bureaucrat || trait == CharacterTrait.SpiritualLeader || trait == CharacterTrait.TacticalLeader || trait == CharacterTrait.MerchantPrince) return true;
                break;
            case Character.Job.navigator:
                if (trait == CharacterTrait.FractureLink || trait == CharacterTrait.CortexLink) return true;
                break;
            case Character.Job.engineer:
                if (trait == CharacterTrait.CrewLiaison || trait == CharacterTrait.Administrator || trait == CharacterTrait.Councellor) return true;
                break;
            case Character.Job.security:
                if (trait == CharacterTrait.ShipOfficer || trait == CharacterTrait.TroopOfficer || trait == CharacterTrait.SecurityOfficer) return true;
                break;
            case Character.Job.quartermaster:
                if (trait == CharacterTrait.CivilianEngineer || trait == CharacterTrait.MilitaryEngineer) return true;
                break;
            case Character.Job.priest:
                if (trait == CharacterTrait.Father || trait == CharacterTrait.Priest) return true;
                break;
            case Character.Job.psycher:
                if (trait == CharacterTrait.ShipFracker || trait == CharacterTrait.CrewFracker || trait == CharacterTrait.Negotiator) return true;
                break;
            default:
                return false;
        }
        return false;
    }
}



public enum ShipBonusCathegory
{
    None,
    /// <summary>
    /// Loyalty
    /// </summary>
    Crew,
    /// <summary>
    /// Structure modifier
    /// </summary>
    Ship,
    /// <summary>
    /// Fracture safety
    /// </summary>
    Fracture,
    /// <summary>
    /// Fame modifier
    /// </summary>
    Relations
}
public enum ShipBonus
{
    None,
    /// <summary>
    /// Crew mood
    /// </summary>
    Happiness,
    /// <summary>
    /// Will to fight
    /// </summary>
    Morale,
    /// <summary>
    /// Weapon performance
    /// </summary>
    Weapons,
    /// <summary>
    /// Core stability
    /// </summary>
    Core,
    /// <summary>
    /// Psychic Power
    /// </summary>
    Link,
    /// <summary>
    /// Fracture traverse speed
    /// </summary>
    Speed,
    /// <summary>
    /// Fracture resistance
    /// </summary>
    Holiness,
    /// <summary>
    /// Relation modifier
    /// </summary>
    Diplomacy,
    /// <summary>
    /// Trade profit modifier
    /// </summary>
    Trade }



