using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Text.RegularExpressions;


public static class ShipStatsLibrary
{
    // ship IDs / ShipStats
    static Dictionary<string, ShipStats> shipStats;


    // constructor
    static ShipStatsLibrary()
    {
        Debug.Log("reading ships data");
        shipStats = parseShipStats();
    }
        
    public static ShipStats getShipStat(string id)
    {
        if (shipStats.ContainsKey(id))
            return shipStats[id];
        else
        {
            Debug.LogError("no ship ID in library: '"+id+"'");
            return null;
        }
    }
    public static Dictionary<string, ShipStats> getShipStats()
    {
        return shipStats;
    }


    private static Dictionary<string, ShipStats> parseShipStats()
    {
        string src = ExternalFiles.IniReadFile(ExternalFiles.file.Ships);

        Dictionary<string, ShipStats> rv = new Dictionary<string, ShipStats>();

        if (src == null)
            Debug.LogError("'locations' data not found or not readable.");

        else
        {
            // Each line starting from DATASTART to DATAEND contains one location 
            // split lines to strings and construct features from each line between markers
            Regex regex = new Regex("DATASTART(.*)DATAEND", RegexOptions.Singleline);
            Match match = regex.Match (src);
            string[] lines = match.Groups[1].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line != null && line != String.Empty && line.Trim() != "")
                {
                    // first block contains id, rest is data [TSV tab separated values]
                    rv.Add(line.Split('\t')[0], DataParser.parseShipStats(line));
                }
            }
        }
        return rv;
    }


}


public class ShipStats
{
    public string   shipId;             // 1
    public string   type;               // 2
    public int      size;               // 3
    public int      modules;            // 4
    public float    structureRatio;     // 5
    public int      engines;            // 6
    // Hard Points
    public int      hpFront;            // 7
    public int      hpDorsal;           // 8
    public int      hpSideL;            // 9
    public int      hpSideR;            // 10
    public int      hpE1;               // 11
    public int      hpE2;               // 12
    public int      hpPd;               // 13
    // signature sizes
    public float    width;              // 14
    public float    length;             // 15
    public float    height;             // 16

    public int      command;            // 17
    public int      cargo;              // 18
    public int      utility;            // 19

    public Data.Tech.Type reqTechType;  // 20
    public float    reqTechLevel;       // 21

    public float    reqRelations;       // 22
    public string   empty;              // 23

    public string   toolTip;            // 24


    // --------------------------------------

    public float exteriorVolume(bool returnRounded = true)
    {
        return returnRounded ? 
            Mathf.Round((float)modules + (float)modules * structureRatio) : 
            (float)modules + (float)modules * structureRatio;
    }
    public float speed()
    {
        return Mathf.Round((float)engines * 2f / exteriorVolume(false) * 100f);
    }
    public float hull()
    {
        return Mathf.Round(exteriorVolume(false) *2f);
    }
    public float signatureFront()
    {
        return Mathf.Round(width * height *10f)/10f;
    }
    public float signatureSide()
    {
        return Mathf.Round(length * height * 10f) / 10f;
    }
    public int hpVal()
    {
        return hpFront + hpDorsal + hpSideL + hpSideR + hpE1 + hpE2 + hpPd;
    }
    public int crewCapacity()
    {
        return
            command * 50 +
            utility * 50 +
            engines * 20 +
            cargo   * 10 +
            hpVal() * 50;
    }
    public int value()
    {
        return
            (int)
            Mathf.Round(
            (exteriorVolume(false) * 10f +
            hpVal() * 300f)
            * Mathf.Sqrt(reqTechLevel)
            /10f)*10; // round up to tens
    }
    /// <summary>
    /// tech level as used in Simulation.resources
    /// </summary>
    /// <returns></returns>
    public int getRequiredTechLevel()
    {
        return (int)reqTechLevel;
    }

}

public static class ShipBonusesStats
{

    /// <summary>
    /// poll excisting utilities for ShipBonus component to be shown in ShipBonusStats
    /// returns null if none for the asked bonus
    /// </summary>
    /// <param name="utility"></param>
    /// <param name="bonus"></param>
    /// <returns></returns>
    public static ShipBonusItem getUtilityBonusItem(Utility utility, ShipBonus bonus)
    {
        ShipBonusItem item = new ShipBonusItem();
        item.bonusType = bonus;
        item.text = formatEnumString(utility.ToString());
        item.source = "Utility module: "+ item.text + "\n";
        item.color = ShipBonuses.getBonusColor(bonus);

        switch (utility)
        {
            case Utility.Empty:
                break;
            case Utility.CargoSpace:
                break;
            case Utility.CrewRecreation:
                item.source += "Mall of bars, virtual pods and other establishments for long journeys.";
                if (bonus == ShipBonus.Happiness)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Morale)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.FloodGenerators:
                item.source += "Machinery that augments the Flood shields to keep the fracture out.";
                if (bonus == ShipBonus.Happiness)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Link)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.Barracks:
                item.source += "For increased readiness and protection, military and key personnel are housed in barracks.";
                if (bonus == ShipBonus.Morale)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Happiness)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.SecurityCenter:
                item.source += "Security control center that locks the ship and its departments down, as well as restricts outside communications.";
                if (bonus == ShipBonus.Morale)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Trade)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.WeaponCoreTap:
                item.source += "Exposes core conduits to weapon systems for enhanced weapon reliability.";
                if (bonus == ShipBonus.Weapons)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Core)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.WeaponCapacitors:
                item.source += "Re-routs some of the excess engine energy capacity into weapon capacitors.";
                if (bonus == ShipBonus.Weapons)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Speed)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.CoreInsulation:
                item.source += "Capacitor system that insulates combat systems from the core, in case of secondary system instabilities.";
                if (bonus == ShipBonus.Core)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Weapons)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.EngineInsulation:
                item.source += "Energy redistribution system dampens fracture surges from the core.";
                if (bonus == ShipBonus.Core)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Speed)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.Collectors:
                item.source += "A restricted department that is said to feed on the minds of the crew.";
                if (bonus == ShipBonus.Link)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Happiness)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.TheRoundRoom:
                item.source += "Shaped like the core, the round room is accessible only for the selected few.";
                if (bonus == ShipBonus.Link)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Holiness)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.WeaponsRerouting:
                item.source += "Funnels the reserves from weapon systems to keep the engines running at peak capacity.";
                if (bonus == ShipBonus.Speed)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Weapons)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.CoreBypass:
                item.source += "Main power conduits are rerouted, bypassing secondary core systems to prioritize engine power.";
                if (bonus == ShipBonus.Speed)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Weapons)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.Chapel:
                item.source += "A place for the crew to gather and pray to reinforce their minds against the Fracture.";
                if (bonus == ShipBonus.Holiness)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Link)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.School:
                item.source += "Schools teach spiritualism and the way to live good life, according to the teachings of the Church.";
                if (bonus == ShipBonus.Holiness)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Trade)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.CivilianComms:
                item.source += "For increased individual freedoms, the ship has open communication systems for the crew.";
                if (bonus == ShipBonus.Trade)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Morale)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            case Utility.ShipBazaar:
                item.source += "A marketplace for the ship crew comes with relaxed regulations. Almost everything is traded on the ship.";
                if (bonus == ShipBonus.Trade)
                {
                    //item.text = "";
                    item.value = 1;
                    return item;
                }
                else if (bonus == ShipBonus.Holiness)
                {
                    //item.text = "";
                    item.value = -1;
                    return item;
                }
                break;
            default:
                break;
        }
        return null;
    }

    /// <summary>
    /// poll excisting character traits for ShipBonus component to be shown in ShipBonusStats
    /// returns null if none for the asked bonus OR the bonus is 0
    /// </summary>
    /// <param name="character"></param>
    /// <param name="bonus"></param>
    /// <returns></returns>
    public static ShipBonusItem getCharacterBonusItem(Character character, ShipBonus bonus)
    {
        if (character.characterTrait == CharacterTrait.None) return null;
        KeyValuePair<string, int> descriptAndBonus = getCharacterStatBonusAndDescription(character, bonus);
        if (descriptAndBonus.Value == 0) return null;

        ShipBonusItem item = new ShipBonusItem();
        item.bonusType = bonus;
        item.text = formatEnumString(character.characterTrait.ToString());
        item.source = Character.getJobName(character.assignment)+" trait: "+ formatEnumString(character.characterTrait.ToString())+"\n";
        item.color = ShipBonuses.getBonusColor(character.assignment);
        item.color.a = 160; // dim down character bonuses

        item.value = descriptAndBonus.Value;
        item.source += descriptAndBonus.Key;
        
        return item;
    }
    public static KeyValuePair<string, int> getCharacterStatBonusAndDescription(Character character, ShipBonus bonus)
    {
        string rs = "";
        float ri = 0f;

        switch (character.characterTrait)
        {
            case CharacterTrait.None:
                break;

            // CAPTAIN
            case CharacterTrait.PersonalLeadership:

                if (bonus == ShipBonus.Morale)
                {
                    ri = (character.getStat(Character.Stat.leadership) + character.getStat(Character.Stat.combat)) / 2f;
                    if (ri >= 200)
                    {
                        ri = 1;
                        rs = "The captain personally meets the troops and inspires loyalty.";
                    }
                    else if (ri < 100)
                    {
                        ri = -1;
                        rs = "The captain personally meets the troops, but his inability to lead troops makes them cringe-worthy events.";
                    }
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Happiness)
                {
                    ri = (character.getStat(Character.Stat.leadership) + character.getStat(Character.Stat.hr)) / 2f;
                    if (ri >= 200)
                    {
                        ri = 1;
                        rs = "The captain is close to the crew, and his leadership is well respected.";
                    }
                    else if (ri < 100)
                    {
                        ri = -1;
                        rs = "The captain tries to be close to the crew, but there is little respect for his leadership.";
                    }
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Diplomacy)
                {
                    ri = (character.getStat(Character.Stat.diplomat));
                    if (ri >= 200)
                    {
                        ri = 1;
                        rs = "The captain handles most of the diplomacy personally, as he is quite a skilled negotiator.";
                    }
                    else if (ri < 100)
                    {
                        ri = -1;
                        rs = "The captain handles most of the diplomacy personally, although he probably shouldn't.";
                    }
                    else ri = 0;
                }
                break;
            case CharacterTrait.Bureaucrat:

                if (bonus == ShipBonus.Happiness)
                {
                    ri = (character.getStat(Character.Stat.hr));
                    if (ri >= 200)
                    {
                        ri = 1;
                        rs = "The captain likes to sit behind his desk, and manage the ship on paper. Luckily, he is quite good at human resources.";
                    }
                    else if (ri < 100)
                    {
                        ri = -1;
                        rs = "The captain sits behind his desk, and manages the ship on paper. This does not make the crew very happy.";
                    }
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Morale)
                {
                    ri = (character.getStat(Character.Stat.combat));
                    if (ri >= 200)
                    {
                        ri = 1;
                        rs = "The will to fight relies upon the crew's trust on the commanding officer's abilities, even if he is directing from behind the table.";
                    }
                    else if (ri < 100)
                    {
                        ri = -1;
                        rs = "It is difficult for a bureaucrat to inspire troops from behind his table, especially when his inexperience for combat is well known.";
                    }
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Trade)
                {
                    rs = "Captain is present in all trade negotiations, and his skill is relevant.";
                    ri = (character.getStat(Character.Stat.trading));
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.SpiritualLeader:

                if (bonus == ShipBonus.Link)
                {
                    ri = (character.getStat(Character.Stat.psy));
                    if (ri >= 250)
                    {
                        ri = 1;
                        rs = "As a spiritual leader on a ship traversing the Fracture, the captain shows true control as he focuses the will of the crew.";
                    }
                    else if (ri < 150)
                    {
                        ri = -1;
                        rs = "As a spiritual leader on a ship traversing the Fracture, great strength of spirit is required to focus all the energies present.";
                    }
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Holiness)
                {
                    rs = "Captain's example in the matters of spiritual purity and prayer is clearly present onboard the ship.";
                    ri = (character.getStat(Character.Stat.holiness));
                    if (ri >= 150) ri =1;
                    else if (ri <= -100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Happiness)
                {
                    ri = (character.getStat(Character.Stat.leadership));
                    if (ri >= 200)
                    {
                        ri = 1;
                        rs = "The captain is a leader, and has certain rapport with the crew.";
                    }
                    else if (ri < 100)
                    {
                        ri = -1;
                        rs = "As a leader, the crew is not looking up to the captain.";
                    }
                    else ri = 0;
                }
                break;
            case CharacterTrait.TacticalLeader:

                if (bonus == ShipBonus.Weapons)
                {
                    rs = "Combat operations hinge on commanding officer's tactical knowledge of space battles.";
                    ri = (character.getStat(Character.Stat.spaceBattle));
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Core)
                {
                    rs = "As a tactical leader, crucial decisions need to be done in moment's notice. It is crucial that the commanding officer understands engineering.";
                    ri = (character.getStat(Character.Stat.engineering));
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Speed)
                {
                    rs = "Pseudo spatial cognition can only be learned through fracture-navigational studies, and are highly valued on tactical bridge.";
                    ri = (character.getStat(Character.Stat.navigation));
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.MerchantPrince:

                if (bonus == ShipBonus.Diplomacy)
                {
                    rs = "Personal gravitas is everything to merchant princes, who operate as much with diplomacy as other means.";
                    ri = (character.getStat(Character.Stat.diplomat));
                    if (ri > 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Trade)
                {
                    rs = "Keen business sense is everything to merchant princes, who operate as interstellar traders in the sector.";
                    ri = (character.getStat(Character.Stat.trading));
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-2;
                    else if (ri < 150) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Happiness)
                {
                    rs = "Leadership is to make the crew understand what they are doing and why.";
                    ri = (character.getStat(Character.Stat.leadership));
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;

            // NAVIGATOR
            case CharacterTrait.FractureLink:

                if (bonus == ShipBonus.Speed)
                {
                    rs = "The navigator guides the ship through his intuition and his experience with fracture navigation. Skilled navigators are extremely valued.";
                    ri = (character.getStat(Character.Stat.navigation));
                    if (ri >= 400) ri =3;
                    if (ri >= 300) ri =2;
                    else if (ri > 250) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Link)
                {
                    rs = "As the navigator surfs the currents of untamed chaotic energies, his psychic affinity and intuition with the fracture helps in dealing with these energies.";
                    ri = (character.getStat(Character.Stat.psy));
                    if (ri < 150) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Holiness)
                {
                    rs = "There is a level of unorthodoxy in the navigator's practices";
                    ri = (character.getStat(Character.Stat.holiness));
                    if (ri <= -100) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.CortexLink:

                if (bonus == ShipBonus.Speed)
                {
                    rs = "The navigator guides the ship through an array of gray tech implementations connecting straight through the implants on his skull. Skilled navigators are extremely valued.";
                    ri = (character.getStat(Character.Stat.navigation));
                    if (ri >= 400) ri =3;
                    if (ri >= 300) ri =2;
                    else if (ri > 250) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Weapons)
                {
                    rs = "Lack of tactical pseudo spatial training restricts effective aligning of fracture arrays and weapon systems in combat.";
                    ri = (character.getStat(Character.Stat.spaceBattle));
                    if (ri < 150) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Core)
                {
                    rs = "When directing the Fracture drive, lack of engineering training from the navigator puts a strain on the core system.";
                    ri = (character.getStat(Character.Stat.engineering));
                    if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;

            // QUARTERMASTER
            case CharacterTrait.CrewLiaison:

                if (bonus == ShipBonus.Happiness)
                {
                    rs = "Quartermaster represents the crew interests on the advisory board. His ability in human resources is central to crew happiness.";
                    ri = (character.getStat(Character.Stat.hr));
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Morale) 
                {
                    rs = "In crisis, quartermaster is responsible for directing general crew and non-combatants. It helps a lot, if he has experience in combat and security";
                    ri = (character.getStat(Character.Stat.combat) + character.getStat(Character.Stat.security)) / 2f;
                    if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 150) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.Administrator:

                if (bonus == ShipBonus.Happiness)
                {
                    rs = "Quartermaster represents the crew interests on the advisory board. His ability in human resources is central to crew happiness.";
                    ri = (character.getStat(Character.Stat.hr));
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                if (bonus == ShipBonus.Diplomacy)
                {
                    rs = "Ship administrator can be a great asset in negotiations and relations.";
                    ri = (character.getStat(Character.Stat.diplomat));
                    if (ri >= 250) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Trade)
                {
                    rs = "Quartermaster is responsible for many ship supplies and equipment. He keeps an inventory and trades for things when needed.";
                    ri = (character.getStat(Character.Stat.trading));
                    if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 150) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.Councellor:

                if (bonus == ShipBonus.Link)
                {
                    rs = "When ship councillor is attuned to the forces of the Fracture, he has many ways to help and guide the ship crew.";
                    ri = (character.getStat(Character.Stat.psy));
                    if (ri >= 250) ri =1;
                    else ri = 0;
                }
                if (bonus == ShipBonus.Holiness)
                {
                    rs = "Sometimes ship quartermasters can help the troubled members of the crew and organize events to unite them in faith.";
                    ri = (character.getStat(Character.Stat.holiness));
                    if (ri >= 200) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Happiness)
                {
                    ri = (character.getStat(Character.Stat.hr));
                    if (ri >= 200)
                    {
                        ri = 1;
                        rs = "Ship councellor who understands human resources can make things a lot easier for the crew.";
                    }
                    else if (ri < 75)
                    {
                        ri = -2;
                        rs = "Ship councellor who has no experience in human resources can really botch things up for everyone onboard.";
                    }
                    else if (ri < 150)
                    {
                        ri = -1;
                        rs = "Ship councellor who has little experience in human resources can quickly complicate matters for everyone.";
                    }
                    else ri = 0;
                }
                break;
            case CharacterTrait.ShipOfficer:

                if (bonus == ShipBonus.Weapons)
                {
                    rs = "When quartermasters are veterans of space battles, they can be a great help.";
                    ri = (character.getStat(Character.Stat.spaceBattle));
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Core)
                {
                    rs = "Ship officers are expected to direct damage control for the engineering department.";
                    ri = (character.getStat(Character.Stat.engineering));
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;

            // SECURITY OFFICER
            case CharacterTrait.TroopOfficer:

                if (bonus == ShipBonus.Morale)
                {
                    rs = "Directly in charge of the troops, troop officers ability to lead is essential, as well as his combat experience.";
                    ri = (character.getStat(Character.Stat.combat) + character.getStat(Character.Stat.leadership)) /2f;
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Core) 
                {
                    rs = "Troop officers contribute to the ship damage control operations when they have understanding of the ship engineering priorities.";
                    ri = (character.getStat(Character.Stat.engineering));
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.SecurityOfficer:

                if (bonus == ShipBonus.Morale)
                {
                    rs = "Security officer is in charge of general security, morale and ship's security forces.";
                    ri = (character.getStat(Character.Stat.combat) + character.getStat(Character.Stat.security)) / 2f;
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Core)
                {
                    rs = "First task is always to keep the core secure and safe. See that necessary ship resources are allocated.";
                    ri = (character.getStat(Character.Stat.security));
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;

            // ENGINEER
            case CharacterTrait.CivilianEngineer:

                if (bonus == ShipBonus.Happiness)
                {
                    rs = "Engineers with knowledge in human resources can do a lot to make the accommodations onboard more tolerable. Personal power taps, department safety systems, or even the lighting can be nice.";
                    ri = (character.getStat(Character.Stat.hr));
                    if (ri >= 200) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Core)
                {
                    rs = "Attending the core takes special kind of dedication. Only the best engineers and core scientists should be allowed.";
                    ri = Mathf.Max(character.getStat(Character.Stat.engineering), character.getStat(Character.Stat.scientist)); // nav OR sci
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Speed)
                {
                    rs = "Most important function of the core is to tap it for fracture engines. To understand any of it is to have experience in fracture navigation or the theoretical sciences behind it.";
                    ri = Mathf.Max(character.getStat(Character.Stat.navigation), character.getStat(Character.Stat.scientist)); // nav OR sci
                    if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.MilitaryEngineer:

                if (bonus == ShipBonus.Core)
                {
                    rs = "Attending the core takes special kind of dedication. Only the best engineers should be allowed.";
                    ri = character.getStat(Character.Stat.engineering);
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Weapons)
                {
                    rs = "It is not just about yelling \"all power to weapons\". Military engineers need to have experience in space battles.";
                    ri = character.getStat(Character.Stat.spaceBattle);
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Speed)
                {
                    rs = "Most important function of the core is to tap it for fracture engines. To understand any of it is to have experience in fracture navigation.";
                    ri = character.getStat(Character.Stat.navigation);
                    if (ri >= 200) ri =1;
                    else if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                break;

            // PSYCHER
            case CharacterTrait.ShipFracker:

                if (bonus == ShipBonus.Link)
                {
                    rs = "Ship fracker focuses the energies on the ship and outside it, manipulating them with the powers of his mind. Ship frackers are scary individuals.";
                    ri = character.getStat(Character.Stat.psy);
                    if (ri >= 400) ri =3;
                    else if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Holiness)
                {
                    rs = "Given power over ship operations, the methods used reflect upon the whole ship. Many times Brotherhood frackers are more reliable than unlicensed ones.";
                    ri = character.getStat(Character.Stat.holiness);
                    if (ri <= -200) ri =-3;
                    else if (ri <= -100) ri =-2;
                    else if (ri < 0) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Speed)
                {
                    rs = "Ship Fracker takes on certain responsibilities regarding ship operations, but without any affinity things aren't operating with efficiency";
                    ri = character.getStat(Character.Stat.psy);
                    if (ri < 150) ri =-2;     
                    else if (ri < 200) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.CrewFracker:
                
                if (bonus == ShipBonus.Happiness)
                {
                    rs = "Crew Fracker has power over crew mood, only the strong frackers can overcome the crew's natural suspicion of psychic auditions and indoctrinations.";
                    ri = character.getStat(Character.Stat.psy);
                    if (ri >= 300) ri =1;
                    else if (character.getStat(Character.Stat.hr) < 75) ri =-2;
                    else if (character.getStat(Character.Stat.hr) < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Holiness)
                {
                    rs = "Given power over ship operations, the methods used reflect upon the whole ship. Many times Brotherhood frackers are more reliable than unlicensed ones.";
                    ri = character.getStat(Character.Stat.holiness);
                    if (ri <= -200) ri =-3;
                    else if (ri <= -100) ri =-2;
                    else if (ri < 0) ri =-1;
                    else ri = 0;
                }
                if (bonus == ShipBonus.Morale)
                {
                    ri = (character.getStat(Character.Stat.psy));
                    if (ri >= 300)
                    {
                        ri = 1;
                        rs = "Powerful crew frackers can have an effect on the crew's will to fight.";
                    }
                    else if (character.getStat(Character.Stat.leadership) < 100)
                    {
                        ri = -1;
                        rs = "Crew Fracker is allowed to fumble around with troops and security systems.";
                    }
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Link)
                {
                    rs = "Crew fracker focuses the energies on the ship and outside it, manipulating them with the powers of his mind. Crew frackers are scary individuals.";
                    ri = character.getStat(Character.Stat.psy);
                    if (ri >= 400) ri =3;
                    else if (ri >= 350) ri =2;
                    else if (ri >= 250) ri =1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.Negotiator:

                if (bonus == ShipBonus.Link)
                {
                    rs = "Negotiators spend less time in controlling the raw energies of the fracture, and more advising in ship's negotiations.";
                    ri = (character.getStat(Character.Stat.psy));
                    if (ri >= 300) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Holiness)
                {
                    rs = "Given power over ship operations, the methods used reflect upon the whole ship. Many times Brotherhood frackers are more reliable than unlicensed ones.";
                    ri = character.getStat(Character.Stat.holiness);
                    if (ri <= -200) ri =-3;
                    else if (ri <= -100) ri =-2;
                    else if (ri < 0) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Diplomacy)
                {
                    rs = "Fracker with power over mind and a keen diplomatic mind can be quite a formidable asset in negotiations.";
                    ri = (character.getStat(Character.Stat.psy) + character.getStat(Character.Stat.diplomat)) /2f;
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Trade)
                {
                    rs = "Fracker with power over mind and keen business sense can be quite a formidable asset in trade negotiations.";
                    ri = (character.getStat(Character.Stat.psy) + character.getStat(Character.Stat.trading)) / 2f;
                    if (ri >= 300) ri =2;
                    else if (ri >= 200) ri =1;
                    else ri = 0;
                }
                break;

            // PRIEST
            case CharacterTrait.Father:

                if (bonus == ShipBonus.Holiness)
                {
                    rs = "The priest is in charge of the congregation on the ship. His personal conviction is the heart of faith onboard.";
                    ri = character.getStat(Character.Stat.holiness);
                    if (ri >= 300) ri =3;
                    else if (ri >= 200) ri =2;
                    else if (ri >= 150) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Happiness)
                {
                    rs = "Father of the crew has lot of social responsibilities. It takes some tact and experience in human resources.";
                    ri = character.getStat(Character.Stat.hr);
                    if (ri >= 200) ri =1;
                    else if (ri < 100) ri =-2;
                    else if (ri < 150) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Speed)
                {
                    rs = "Sometimes a priest is so aggressive in his faith that he manages to curb beneficial behaviour in ship operations, especially on those involving church sanctified functions.";
                    ri = character.getStat(Character.Stat.violent);
                    if (ri > 200) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Link)
                {
                    rs = "There has alway been a level of which hunting within the church. Aggressive priests can hunt down latent frackers and severe any connections to the fracture onboard.";
                    ri = character.getStat(Character.Stat.violent); // hunts down anyone latent/psychic in the crew
                    if (ri > 300) ri =-3;
                    else if (ri > 200) ri =-2;
                    else if (ri > 100) ri =-1;
                    else ri = 0;
                }
                break;
            case CharacterTrait.Priest:

                if (bonus == ShipBonus.Holiness)
                {
                    rs = "The priest is in charge of the congregation on the ship. His personal conviction is the heart of faith onboard.";
                    ri = character.getStat(Character.Stat.holiness);
                    if (ri >= 300) ri =3;
                    else if (ri >= 200) ri =2;
                    else if (ri >= 150) ri =1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Happiness)
                {
                    rs = "A priest of the crew has lot of social responsibilities. It takes some tact and experience in human resources, or else a priest can quickly start to appear imperious.";
                    ri = character.getStat(Character.Stat.hr);
                    if (ri < 75) ri =-2;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Morale)
                {
                    rs = "Being a priest is not just a spiritual position. In combat, they are supposed to join the fight and bolster the morale of the troops by their prowess.";
                    ri = character.getStat(Character.Stat.combat);
                    if (ri > 200) ri =1;
                    else if (ri < 100) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Speed)
                {
                    rs = "Sometimes a priest is so aggressive in his faith that he manages to curb beneficial behaviour in ship operations, especially on those involving church sanctified functions.";
                    ri = character.getStat(Character.Stat.violent); // hunts down anyone latent/psychic in the crew
                    if (ri > 200) ri =-1;
                    else ri = 0;
                }
                else if (bonus == ShipBonus.Link)
                {
                    rs = "There has alway been a level of which hunting within the church. Aggressive priests can hunt down latent frackers and severe any connections to the fracture onboard.";
                    ri = character.getStat(Character.Stat.violent); // hunts down anyone latent/psychic in the crew
                    if (ri > 300) ri =-3;
                    else if (ri > 200) ri =-2;
                    else if (ri > 100) ri =-1;
                    else ri = 0;
                }
                break;
            default:
                break;
        }
        //Debug.Log(character.assignment + "/" + bonus + ": " + ri + "  (leadership: " + character.getStat(Character.Stat.leadership) + ")");
        return new KeyValuePair<string, int>(DataParser.changeGenderContent(rs, true, (bool)character.isMale), (int)ri);
    }
    

    public static ShipBonusItem getCrewBonusItem(PlayerCrew crew, ShipBonus bonus)
    {
        ShipBonusItem item = new ShipBonusItem();
        if (bonus == ShipBonus.None) return null;

        item.bonusType = bonus;
        item.text = "Crew "+formatEnumString(bonus.ToString());
        item.source = "Crew modifier from " + formatEnumString(bonus.ToString());
        item.color = new Color(.3f, .3f, .3f, .7f); //ShipBonuses.getBonusColor(character.assignment);

        float stat = 0;

        switch (bonus)
        {
            case ShipBonus.Happiness:
                stat = crew.getStat(Simulation.Effect.happiness);
                break;
            case ShipBonus.Morale:
                stat = (crew.getStat(Simulation.Effect.military) + crew.getStat(Simulation.Effect.morale)) /2f;
                break;
            case ShipBonus.Weapons:
                stat = (crew.getStat(Simulation.Effect.military) + crew.getStat(Simulation.Effect.navigation)) /2f;
                break;
            case ShipBonus.Core:
                stat = (crew.getStat(Simulation.Effect.military) /4f + crew.getStat(Simulation.Effect.industry)) /4f + crew.getStat(Simulation.Effect.innovation) /2f;
                break;
            case ShipBonus.Link:
                stat = crew.getStat(Simulation.Effect.psych);
                break;
            case ShipBonus.Speed:
                stat = crew.getStat(Simulation.Effect.navigation);
                break;
            case ShipBonus.Holiness:
                stat = crew.getStat(Simulation.Effect.holy);
                break;
            case ShipBonus.Diplomacy:
                stat = crew.getStat(Simulation.Effect.diplomacy);
                break;
            case ShipBonus.Trade:
                stat = crew.getStat(Simulation.Effect.economy);
                break;
            default:
                break;
        }
        if (stat > .5f) item.value = 1;
        else if (stat < -.5f) item.value = -1;

        if (item.value == 0) return null;

        // get source description
        item.source = getCrewBonusSource(bonus, item.value >= 1);

        return item;
    }
    static string getCrewBonusSource(ShipBonus bonus, bool positive)
    {
        switch (bonus)
        {
            case ShipBonus.Happiness:
                if (positive)
                    return "Most of the crew is fundamentally optimistic, forward looking and positive. They can endure more hardship, and recover quickly from misfortunes.";
                return "The crew is mostly depressed about their future prospects. People are on the edge, and constantly complaining.";
            case ShipBonus.Morale:
                if (positive)
                    return "This crew is territorial and jumpy by nature. There is some underlying frustration and some infighting, but they have good instincts for violent crisis.";
                return "The crew is mostly at ease, and many of them come from backgrounds that allow them a level of complacency.";
            case ShipBonus.Weapons:
                if (positive)
                    return "Most of the crew come from background where most are familiar with ship operations. It is good when you have fifty people who need to cooperate on operating large machinery.";
                return "Most of the crew come from planets of jobs in which they are unfamiliar with huge machinery and comprehending spatial trajectories.";
            case ShipBonus.Core:
                if (positive)
                    return "Most of the crew comes from a background where most are familiar with ship operations. It is good when you have fifty people who need to cooperate on operating large machinery.";
                return "Most of the crew come from planets and jobs in which they are unfamiliar with huge industrial machinery.";
            case ShipBonus.Link:
                if (positive)
                    return "This crew has been living on the edge of high Fracture, or else there are many adepts of Brotherhood or the other kind among them. And so, the link to Fracture is strong.";
                return "The crew has not been living near Fracture, or else possess the mental capacity to control its surges. There is a level of wasted energy involved.";
            case ShipBonus.Speed:
                if (positive)
                    return "Navigating the Fracture is a rare talent, and the machinery involved is not the most intuitive. The crew is showing some affinity, creativity and adaptation operating the core.";
                return "Navigating the Fracture is a rare talent, and the machinery involved is not the most intuitive. Perhaps it's just that this crew comes from being too grounded.";
            case ShipBonus.Holiness:
                if (positive)
                    return "It all comes from the small deeds. A prayer whispered every time a warp conduit is passed. The discipline that comes from harsh social taboos and peer pressure.";
                return "The Church claims that an inquisitive mind is not unlike a door left open. When a certain form is followed, the society is secure. On this ship, the form has never take to the root.";
            case ShipBonus.Diplomacy:
                if (positive)
                    return "The ship crew is active and has outside contacts of their own. It makes dealings and negotiations easier, as most of the time some link of friendship can be traced.";
                return "The crew is withdrawn and distrusting by nature. Whatever relations they have outside the ship are typically inhospitable";
            case ShipBonus.Trade:
                if (positive)
                    return "There are a fair amount of trade from the ship crew, and it opens up a lot of contacts and deals when buying or selling commodities around.";
                return "Whatever trades the crew are making are more than likely to bleed out ship resources. Picture a hole in the bottom of the ship, and wealth spilling out.";
            default:
                return "";
        }
    }

    public static string getBonusDescription(ShipBonusCathegory bonusCathegory, int value)
    {
        string rs = "";

        switch (bonusCathegory)
        {
            case ShipBonusCathegory.None:
                return rs;
            case ShipBonusCathegory.Crew:
                rs = "[Crew recruiting modifier]\nOverall crew loyalty comes from happiness and morale.\n\n";
                if (value > 9) rs += "Onboard this ship, serves the most dedicated crew on the sector! People beg to get a position on your ship.";
                else if (value > 5) rs += "Right now the crew is very dedicated! Reputation on crew conditions is great which makes recruitment easy.";
                else if (value > 2) rs += "Right now the crew is focused and dedicated. The ship is considered a good one to sign on, so there should be no problems in recruiting.";
                else if (value >= 0) rs += "There are no problems from the crew at the moment. There shouldn't be anyone jumping the ship at the next port of call.";
                else if (value < -9) rs += "The crew is in open mutiny, and many of the departments are run by local leaders. It is very hard to get a good idea what is going on.";
                else if (value < -5) rs += "The crew is out of control. Orders are not being carried out, and is possible to hear the chanting and carousing from down below the decks.";
                else if (value < -2) rs += "The crew is not focused, and things are not running smoothly. You suspect some of them will jump the ship at the next port of call.";
                else if (value < 0) rs += "There are some complaints. Either people are not performing, or there are some problems in the management. You may lose some crew on the next port of call.";
                break;
            case ShipBonusCathegory.Ship:
                rs = "[Ship structure modifier]\nInternal ship stability comes mostly from critical systems like the core stability and weapon status.\n\n";
                if (value > 9) rs += "The engineering team onboard has made wonders to the ship. It is in perfect working order!";
                else if (value > 5) rs += "When kept well, these ships can handle quite a lot. And clearly, there are very qualified people running the ship.";
                else if (value > 2) rs += "The ship is well maintained. Sometimes you can hear these things from ship vibrations when you come aboard.";
                else if (value >= 0) rs += "This ship is on par with maintainance.";
                else if (value < -9) rs += "This ship is at the breaking point, and any system failure could be catastrophic.";
                else if (value < -5) rs += "There are several ongoing critical system errors. These ships are very robust, but you shouldn't push your luck.";
                else if (value < -2) rs += "There are discrepencies in ship maintainance. When pushed to the limits, these discrepencies can become prohibitive.";
                else if (value < 0) rs += "These ships are old, and it is hard to keep taps with its many arcane systems and components.";
                break;
            case ShipBonusCathegory.Fracture:
                rs = "[Fracture safety modifier]\nFracture is no place for human beings. No place. However, there are ways to make the fracture fall manageable, so a thread of sanity is left when the ship comes out.\n\n";
                if (value > 9) rs += "But oboard this ship, it is like it was not inside the fracture at all!";
                else if (value > 5) rs += "On this ship, it seems to be working well.";
                else if (value > 2) rs += "This ship is a typical example. There are some unexplained headaches and insomnia.";
                else if (value >= 0) rs += "This ship is a typical example. Some people live estranged lives on the ship, others need some serious rest and recreation afterwards.";
                else if (value < -9) rs += "On this ship, it is total chaos. Even within the protected sanctum of advisors, people show signs of madness. But from outside the security doors, a terrifying inhuman choir can be heard.";
                else if (value < -5) rs += "This ship is a typical example. There are reports of creepy corridors that lead nowhere on the ship. Many fly into murderous fits, while others withdraw into their chambers and are not seen again.";
                else if (value < -2) rs += "This ship is a typical example. People are constantly stressed out and some get fits and convulsions. Or stabbing pain behind the eyes. The crew reports apperitions passing through or strange voices whispering to them when they sleep.";
                else if (value < 0) rs += "This ship is a typical example. People are jumpy and distrusting. Someone loses his mind or yells for no apparent reason. And at night you can hear the crew cry in their sleep.";
                break;
            case ShipBonusCathegory.Relations:
                rs = "[Fame modifier]\nRelations, reputation and recognition onboard a starship resonates from the political- and business networks it extends.\n\n";
                if (value > 9) rs += "This ship is reknown, and its actions are an interest on many worlds.";
                else if (value > 5) rs += "The actions of this ship are followed with great interest, and are well remembered.";
                else if (value > 2) rs += "The actions of this ship are always local news.";
                else if (value >= 0) rs += "This ship is news in most ports it visits.";
                else if (value < -9) rs += "The ship has minimal public profile, and represent only its employer for most.";
                else if (value < -5) rs += "The ship is mostly recognized for its employers.";
                else if (value < -2) rs += "The ship has little recognition value, mostly people remember who it works for.";
                else if (value < 0) rs += "This ship is news in the small ports it visist.";
                break;
            default:
                return rs;
        }
        return rs;
    }
    public static string getBonusDescription(ShipBonus bonus, int value)
    {
        string rs = "";

        switch (bonus)
        {
            case ShipBonus.None:
                return rs;
            case ShipBonus.Happiness:
                rs = "[Ship structure modifier]\nThe crew is ";
                if (value > 9) rs += "enthusiastic!";
                else if (value > 5) rs += "happy.";
                else if (value > 2) rs += "comfortable.";
                else if (value >= 0) rs += "content.";
                else if (value < -9) rs += "miserable. The conditions aboard the ship are intolerable.";
                else if (value < -5) rs += "depressed. The conditions aboard the ship are grim.";
                else if (value < -2) rs += "disgruntled. There are serious complaints over the conditions onboard.";
                else if (value < 0) rs += "reserved. There are some complaints, but they are mostly minor issues.";
                break;
            case ShipBonus.Morale:
                rs = "[Security and combat modifier]\nMorale is crew's conviction, the will to fight as well as representative of the internal security.\n\n";
                if (value > 9) rs += "Reported morale is 'Excellent': The crew's faith in the ship is unwavering.";
                else if (value > 5) rs += "Reported morale is 'Good': Crew is dedicated and eager to serve.";
                else if (value > 2) rs += "Reported morale is 'Solid': No security threats, the crew is ready and willing.";
                else if (value >= 0) rs += "Reported morale is 'Nominal': Standard security threats apply, routine operations are uncompromized.";
                else if (value < -9) rs += "There is no morale report. People are abandoning their posts or not coming to work. There is looting on the ship.";
                else if (value < -5) rs += "Reported morale is 'Critical': There are wide reports of corruption and misconduct. People are stealing and fighting among themselves.";
                else if (value < -2) rs += "Reported morale is 'Shaken': The crew is undependable. Orders are not executed exactly, or are even lost on the way. There are reports of corruption.";
                else if (value < 0) rs += "Reported morale is 'Substandard': Working morale is not good. Critical operations are not compromized, but there are reports of mismanagement from most departments.";
                break;
            case ShipBonus.Weapons:
                rs = "[Weapon range modifier]\nWeapon systems and remote tactical objects are key to efficient space combat operations.";
                if (value >= 0) rs += "\n\nWeapon range bonus:  +" + Mathf.Round((1f- Mathf.Sqrt(1f - value /15f))*100f ) + "%"; // multiplier: 1 - Sqrt(1-weapons/15)
                else rs += "\n\nWeapon range bonus:  " + Mathf.Round((1f - Mathf.Sqrt(1f - value / 15f)) * 100f) + "%";
                //else if (value > 5) rs += "";
                //else if (value > 2) rs += "";
                //else if (value >= 0) rs += "";
                //else if (value < -9) rs += "";
                //else if (value < -5) rs += "";
                //else if (value < -2) rs += "";
                //else if (value < 0) rs += "";
                break;
            case ShipBonus.Core:
                rs = "[Energy stability modifier]\nThe core is a sealed fracture power source in a round compartment.\n\n"; // TODO: core mechanics
                if (value > 9) rs += "Even the most delicate systems on the ship are operating without a hiccup!";
                else if (value > 5) rs += "The whole ship is more reliable for its steady power source.";
                else if (value > 2) rs += "It is stable.";
                else if (value >= 0) rs += "It is operating well enough.";
                else if (value < -9) rs += "Now the core is critical, and in need of emergency repairs.";
                else if (value < -5) rs += "Now secondary systems are experiencing blackouts and there are often problems in core systems.";
                else if (value < -2) rs += "There is a ratting shudders coming from down below, like fits of cough.";
                else if (value < 0) rs += "Sometimes there are brownouts as the current fluxes. Primary systems are secured.";
                break;
            case ShipBonus.Link:
                rs = "[Fracker power]\nFracture link represents control over the fracture vortex that forms around everything in fracture.\n\n";
                if (value > 9) rs += "The ship is like a perfect storm: calm in the centre, and a great whirlwind of power crackling around it!";
                else if (value > 5) rs += "The energies around the ship are powerful, yet projected outwards exatly like they should be.";
                else if (value > 2) rs += "There is a measurable barrier of fracture circling the ship, deflecting fracture surges.";
                else if (value >= 0) rs += "It seems to be going okay for now.";
                else if (value < -9) rs += "In this case, that vortex is inverted, like a funnel pouring down on the ship. Immediate escape from the fracture is recommended!";
                else if (value < -5) rs += "There is little control here. It takes all the energy to withstand as the flood shield leak in uncontrolled energies.";
                else if (value < -2) rs += "Flood shields screaming, there is little control over the tides fracture beating against the ship.";
                else if (value < 0) rs += "There are volatile surges of fracture around the ship, beating against the flood shields.";
                break;
            case ShipBonus.Speed:
                rs = "[Ship speed modifier]\nSpeed comes from the core churning power to the engines, and from navigating through the currents of fracture.";
                if (value >= 0) rs += "\n\nSpeed bonus:  +" + Mathf.Round((1f - Mathf.Sqrt(1f - value / 15f)) * 100f) + "%"; // multiplier: 1 - Sqrt(1-weapons/15)
                else rs += "\n\nSpeed bonus:  " + Mathf.Round((1f - Mathf.Sqrt(1f - value / 15f)) * 100f) + "%";
                //if (value > 9) rs += "";
                //else if (value > 5) rs += "";
                //else if (value > 2) rs += "";
                //else if (value >= 0) rs += "";
                //else if (value < -9) rs += "";
                //else if (value < -5) rs += "";
                //else if (value < -2) rs += "";
                //else if (value < 0) rs += "";
                break;
            case ShipBonus.Holiness:
                rs = "[Fracture resistance]\nWith purity of mind, it is possible to resist the corrupting powers constantly attacking the human mind.\n\n";
                if (value > 9) rs += "The ship is a sanctuary, and will suffer no harm from wicked forces!";
                else if (value > 5) rs += "The ship is obviously blessed. The communion is strong and united.";
                else if (value > 2) rs += "There is something blessed about this ship. A peace of mind that can be felt.";
                else if (value >= 0) rs += "The proper protocol is observed, and practises on the ship are mostly sound.";
                else if (value < -9) rs += "It is quite possible the ship and its crew is possessed. But how is it necessarily a bad thing?";
                else if (value < -5) rs += "Someone could find many cases of malpractises from core dabbling to outright daemon summonings among the crew.";
                else if (value < -2) rs += "To an outsider, crew's widespread and unsanctified beliefs and practises can seem a bit disturbing.";
                else if (value < 0) rs += "Each ship has their unique ways of handling the daily routines.";
                break;
            case ShipBonus.Diplomacy:
                rs = "[Relations modifier]\nEach transaction, exchange of ideas or negotiations changes the relationship.\n\n";
                if (value > 9) rs += "To walk through life without enemies is a great achievement!";
                else if (value > 5) rs += "Master diplomats can win friends easily and dodge complications with alarming grace.";
                else if (value > 2) rs += "Sometimes a bad thing can be fixed, and good things can have great power to connect people.";
                else if (value >= 0) rs += "It is good to have many friends and few enemies.";
                else if (value < -9) rs += "But something is seriously wrong, when no opportunity can make friends, and even the smallest of slight turns into a catastrophe.";
                else if (value < -5) rs += "But these dealings rarely offer little beyond their current value of transaction. At least there won't be shortage of enemies.";
                else if (value < -2) rs += "But it is hard to form relations and trust that grow beyond the moment. Unless it is something bad. People remember the bad.";
                else if (value < 0) rs += "It would be helpful if more came out of it. Can't have too many friends.";
                break;
            case ShipBonus.Trade:
                rs = "[Trade modifier]\nWhere others buy at list prices, master traders can find extra profit.\n\n";
                if (value > 9) rs += "And this ship is famous for it!";
                else if (value > 5) rs += "Most of the time the right buyer or a great opportunity can be found.";
                else if (value > 2) rs += "This ship usually trades above list prices.";
                else if (value >= 0) rs += "This ship trades at list prices.";
                else if (value < -9) rs += "Ships like this are their favourite marks.";
                else if (value < -5) rs += "But why is no-one showing those lists? Runnign a ship is very expensive.";
                else if (value < -2) rs += "But there are so many extra expenses and fees it is hard to keep track.";
                else if (value < 0) rs += "This ship trades at list prices.";
                break;
            default:
                return rs;
        }
        return rs;
    }

    public static string formatEnumString(string enumString, bool preserveAcronyms = false)
    {
        if(string.IsNullOrEmpty(enumString))
           return string.Empty;
        // välilyönti isojen kirjainten eteen (camelCase -> camel Case)
        StringBuilder newText = new StringBuilder(enumString.Length * 2);
        newText.Append(enumString[0]);
        for (int i = 1; i < enumString.Length; i++)
        {
            if (char.IsUpper(enumString[i]))
                if ((enumString[i - 1] != ' ' && !char.IsUpper(enumString[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(enumString[i - 1]) &&
                     i < enumString.Length - 1 && !char.IsUpper(enumString[i + 1])))
                    newText.Append(' ');
            newText.Append(enumString[i]);
        }
        // capitalize first letter
        return char.ToUpper(newText.ToString()[0]) + newText.ToString().Substring(1);
    }
}

