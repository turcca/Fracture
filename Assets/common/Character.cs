using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterCreator
{
    public CharacterCreator(Vector2 pos)
    {
        string homeId = Root.game.getClosestHabitat(pos).id;
    }
}

public class Character
{
    static public Character Empty = new Character();

    static private Character[] sortBy(Stat s, Character[] c)
    {
        Debug.LogWarning("returns with new objects!!! cannot be used - todo: ref ?");
        return c.OrderBy(o => o.stats[s]).ToArray();
    }

    static public Character getBest(Character[] c, Stat s)
    {
        return c.OrderBy(o => o.stats[s]).First();
    }

    static public string getJobName(Job job)
    {
        switch (job)
        {
            case Job.captain: return "Captain";
            case Job.navigator: return "Navigator";
            case Job.engineer: return "Chief Engineer";
            case Job.security: return "Security Officer";
            case Job.quartermaster: return "Quartermaster";
            case Job.priest: return "Priest";
            case Job.psycher: return "Fracker";
            case Job.none: return "";
            default: return "";
        }
    }

    public enum Job
    {
        none,
        captain,
        navigator,
        engineer,
        security,
        quartermaster,
        priest,
        psycher
    }

   public enum Stat {
        /// <summary>
        /// age in years
        /// </summary>
        age,
        /// <summary>
        /// hp, out of 100?
        /// </summary>
        health,
        /// <summary>
        /// Radioation / toxicity levels: 0: clean, 100: lethal
        /// </summary>
        polluted,
        /// <summary>
        /// Insanity level: 0 = sane, 100 = insane
        /// </summary>
        insanity,
        /// <summary>
        /// Cloned dna: 0-100
        /// </summary>
        clone,
        /// <summary>
        /// "Outside influence"
        /// 0 = no corruption, 100 = corrupted
        /// </summary>
        possessed,
        /// <summary>
        /// AI influence/brain implant, 0-100
        /// </summary>
        ai,
        /// <summary>
        /// loyalty to the captain, 0-100
        /// </summary>
        loyalty,
        /// <summary>
        /// 100 = euphoric, 0 = rebellious
        /// </summary>
        happiness,
        /// <summary>
        /// 100 = idealistic, naive, 0 = cynic, jaded
        /// </summary>
        idealist,
        /// <summary>
        /// 100 = kind, 0 = mean
        /// </summary>
        kind,
        /// <summary>
        /// Skill
        /// </summary>
        leadership,
        /// <summary>
        /// Skill: human resources
        /// </summary>
        hr,
        /// <summary>
        /// Skill
        /// </summary>
        engineering,
        /// <summary>
        /// Skill, psy warning- seeing into the future
        /// </summary>
        precognition,
        /// <summary>
        /// Skill: fracture strength/skill
        /// </summary>
        psy,
        /// <summary>
        /// Skill
        /// </summary>
        navigation,
        /// <summary>
        /// Skill: directing ship-to-ship combat
        /// </summary>
        spaceBattle,
        /// <summary>
        /// Skill: personal combat, and leading troops
        /// </summary>
        combat,
        /// <summary>
        /// Skill
        /// </summary>
        trading,
        /// <summary>
        /// Skill
        /// </summary>
        diplomat,
        /// <summary>
        /// Skill
        /// </summary>
        scientist,
        /// <summary>
        /// -corruption / +true
        /// </summary>
        integrity,
        /// <summary>
        /// -unholy / +holy
        /// </summary>
        holiness,
        /// <summary>
        /// -mutated / +pure human gene
        /// </summary>
        purity,
        /// <summary>
        /// -anarchist / +police control
        /// </summary>
        security,
        /// <summary>
        /// -pacifist / +violent
        /// </summary>
        violent,
        /// <summary>
        /// How aristocratic? 0 = none
        /// </summary>
        aristocrat,
        /// <summary>
        /// -non-imperialist / +imperialist
        /// </summary>
        imperialist,
        /// <summary>
        /// "Curiosity into the forbidden"
        /// 0 = innocent/law abiding, 100 = corrupted
        /// </summary>
        corruption,
        /// <summary>
        /// xp
        /// </summary>
        experience
    }

    public  bool    isActive       = true;
    public  bool?    player         = false;
    public  string  name           = "";
    public  bool?  isMale           = null;
    public bool? HQclone            = null;
    public  Location  origins      = null;
    public  Faction.FactionID?  faction    = null; // faction
    public  Faction.IdeologyID? ideology       = null;
    public  Faction.Agenda?  agenda         = null;
    public  Job     assignment     = Job.none;
    public CharacterTrait characterTrait = CharacterTrait.None;
    public  float   dateAssigned   = -1;
    public int id { get; private set; }

    internal Dictionary<Stat, float> stats = new Dictionary<Stat,float>();

    public class Portrait
    {
        public int id = 1;
        public int age = 0;

        public Portrait(int _id, int _age)
        {
            id = _id;
            age = _age;
        }
        public Portrait()
        {
        }
    }
    public Portrait portrait {get; private set;}

    static public string[] getSkillNames()
    {
        return new string[]
        {
            "leadership",
            "hr",
            "engineering",
            "precognition",
            "psy",
            "navigation",
            "spaceBattle",
            "combat",
            "trading",
            "diplomat",
            "scientist",
            "integrity",
            "holiness",
            "purity",
            "security",
            "violent",
            "aristocrat",
            "imperialist",
            "corruption"
        };
    }
    public static List<Stat> skillList = new List<Stat>
    {
        Stat.leadership,
        Stat.hr,
        Stat.engineering,
        Stat.precognition,
        Stat.psy,
        Stat.navigation,
        Stat.spaceBattle,
        Stat.combat,
        Stat.trading,
        Stat.diplomat,
        Stat.scientist,
        Stat.integrity,
        Stat.holiness,
        Stat.security,
        Stat.corruption
    };
    
    static public Stat[] getStatNames()
    {
        return new Stat[]
        {
            Stat.age,
            Stat.health,
            Stat.polluted,
            Stat.insanity,
            Stat.clone,
            Stat.possessed,
            Stat.ai,
            Stat.loyalty,
            Stat.happiness,
            Stat.idealist,
            Stat.kind,
            Stat.leadership,
            Stat.hr,
            Stat.engineering,
            Stat.precognition,
            Stat.psy,
            Stat.navigation,
            Stat.spaceBattle,
            Stat.combat,
            Stat.trading,
            Stat.diplomat,
            Stat.scientist,
            Stat.integrity,
            Stat.holiness,
            Stat.purity,
            Stat.security,
            Stat.violent,
            Stat.aristocrat,
            Stat.imperialist,
            Stat.corruption
        };
    }
    static public Job[] getAdvisorAssignmentNames()
    {
        return new Job[]
        {
            Job.captain,
            Job.navigator,
            Job.engineer,
            Job.security,
            Job.quartermaster,
            Job.psycher
        };
    }

    static private int ids = 0;

    public Character()
    {
        
    }
    public void format(Character template = null)
    {
        id = ids;
        ++ids;
        // set location
        //Debug.Log("set character origins by player.position: " + Root.game.player.position.ToString());
        if (template != null && template.origins != null) origins = template.origins;
        else
        {
            origins = Root.game.player.getClosestHabitat(); // get closest location
        }
        // set faction
        if (template != null && template.faction != null) faction = template.faction; // else assign factions to crew outside templates

        // set ideology
        if (template != null && template.ideology != null) ideology = template.ideology; else ideology = origins.ideology.getRandomIdeology();

        // get appropriate portrait
        if (template != null && template.portrait != null) portrait = template.portrait; portrait = Root.PortraitManager.getPortrait("");
        // gender
        if (template != null && template.isMale != null) { isMale = template.isMale; } //else { isMale = UnityEngine.Random.value < 0.5f ? true : false; Debug.Log("character not gendered from template"); }
        // get name
        if (template != null && template.name != "") { name = template.name; } else if (ideology != null) name = NameGenerator.getName((Faction.IdeologyID)ideology, isMale); else Debug.LogError("Character name error: ideology null");
        // get trait
        if (template != null && template.characterTrait != CharacterTrait.None) characterTrait = template.characterTrait;

        // setup stats
        foreach (Stat s in (Stat[])Enum.GetValues(typeof(Stat)))
        {
            setStat(s, 0f);
        }
        setStat(Stat.health, 100f);

        // roll stats
        rollStats(template);
    }

    public float getStat (Stat skill) 
    {
        if (stats.ContainsKey(skill))
        {
            return stats[skill];
        }
        Debug.LogWarning("get stat - NOT FOUND: '" + skill.ToString() + "' (name: " + name + ")");
        return 0f;
    }
    public float getStat (string statName)
    {
        return getStat((Stat)Enum.Parse(typeof(Stat), statName));
    }
    internal void clearStats()
    {
        stats.Clear();
    }

    public bool isIdeology(string ideologyName)
    {
        return isIdeology((Faction.IdeologyID) Enum.Parse(typeof(Faction.IdeologyID), ideologyName));
    }
    public bool isIdeology(Faction.IdeologyID ideologyTest)
    {
        return (ideology == ideologyTest) ? true : false;
    }

    public void addStat (Stat skill, float amount) 
    { 
        stats[skill] += amount;
        if (stats[Stat.health] > 100.0f)
        {
            stats[Stat.health] = 100.0f;
        }
    }
    public void setStat (Stat skill, float amount) 
    {
        if (stats.ContainsKey(skill))
        {
            stats[skill] = amount;
            if (skill == Stat.health && stats[Stat.health] > 100.0f)
            {
                stats[Stat.health] = 100.0f;
            }
        }
        else
        {
            //Debug.Log("setStat / adding dictionary entry: " + skill.ToString());
            stats.Add(skill, amount);
        }
    }
    /// <summary>
    /// no param: return current assignment (if any) best trait
    /// job param: return best trait for potential job choice
    /// </summary>
    /// <param name="job"></param>
    /// <returns></returns>
    public CharacterTrait getBestCharacterTrait(Job job = Job.none)
    {
        CharacterTrait oldTrait = characterTrait;

        if (job == Job.none)
        {
            job = assignment;
            if (job == Job.none) return CharacterTrait.None;
        }

        int bestValue = -99;
        CharacterTrait bestTrait = CharacterTrait.None;

        foreach (CharacterTrait trait in getJobTraits(job))
        {
            characterTrait = trait; // passes trait to character, so evaluator checks that trait for skill values
            int jobBonuses = 0;
            foreach (ShipBonus bonus in Enum.GetValues(typeof(ShipBonus)))
            {
                jobBonuses += ShipBonusesStats.getCharacterStatBonusAndDescription(this, bonus).Value;
            }
            if (jobBonuses > bestValue)
            {
                bestTrait = trait;
                bestValue = jobBonuses;
            }
            //Debug.Log("    evaluatorfor " + assignment + ": " + trait + " (" + jobBonuses + ")");
        }
        //Debug.Log("best trait for " + assignment + ": " + bestTrait+" ("+bestValue+")");
        characterTrait = oldTrait;
        // default is first job trait - actually works like this already since starting comparison for none is -99
        return bestTrait != CharacterTrait.None ? bestTrait : getJobTraits(job)[0];
    }

    /// <summary>
    /// noteworthy 'skill descriptions', 'toolTips'
    /// </summary>
    private Dictionary<string, string> getSkillLevelsList ()
    {
        Dictionary<string, string> skillLevels = new Dictionary<string, string>();

        if (getStat(Stat.leadership) >= 500.0f) { skillLevels.Add("Leadership: Epic",           "A leader others will follow into heaven or hell. History makes careful accounts of these once-in-a-generation leaders."); }
        else if (getStat(Stat.leadership) >= 400.0f) { skillLevels.Add("Leadership: Legendary", "Greatest leaders invoke absolute trust and loyalty. There are but a handful known leaders with such reputation."); }
        else if (getStat(Stat.leadership) >= 350.0f) { skillLevels.Add("Leadership: Renown",    "Great leaders command the respect and admiration of the crew and are famous for it."); }
        else if (getStat(Stat.leadership) >= 300.0f) { skillLevels.Add("Leadership: Remarcable","Remarcable leaders have the full confidence of the crew. They are creative and reliable."); }
        else if (getStat(Stat.leadership) >= 250.0f) { skillLevels.Add("Leadership: Excellent", "Excellent leaders inspire confidence in others and are great personalities at handling crisis."); }
        else if (getStat(Stat.leadership) >= 200.0f) { skillLevels.Add("Leadership: Very Good", "Very good leaders personally inspire confidence and cooperation in others, even in hard times."); }
        else if (getStat(Stat.leadership) >= 150.0f) { skillLevels.Add("Leadership: Good",      "People will follow good leaders willingly."); }
        else if (getStat(Stat.leadership) >= 100.0f) { skillLevels.Add("Leadership: Trained",   "A leader personally leads the crew. Without experience, only routine tasks are managed."); }

        if (getStat(Stat.hr) >= 500.0f) { skillLevels.Add("Human Resources: Epic",              "These people are nexuses. They can forge virtually any group into dream teams."); }
        else if (getStat(Stat.hr) >= 400.0f) { skillLevels.Add("Human Resources: Legendary",    "Some people have the talent of coaching the dream teams and achieving what others can."); }
        else if (getStat(Stat.hr) >= 350.0f) { skillLevels.Add("Human Resources: Exceptional",  "Exceptionally well run departments are those everyone wants to work in. Poeple are happy and focused."); }
        else if (getStat(Stat.hr) >= 300.0f) { skillLevels.Add("Human Resources: Remarcable",   "Remarcably well performing department with motivated and focused crew."); }
        else if (getStat(Stat.hr) >= 250.0f) { skillLevels.Add("Human Resources: Excellent",    "Managing a motivated and focused department makes a big difference in performance."); }
        else if (getStat(Stat.hr) >= 200.0f) { skillLevels.Add("Human Resources: Very Good",    "Managing a department well enough to make it run smoothly."); }
        else if (getStat(Stat.hr) >= 150.0f) { skillLevels.Add("Human Resources: Good",         "Managing a departments on a ship is a full-time job."); }
        else if (getStat(Stat.hr) >= 100.0f) { skillLevels.Add("Human Resources: Trained",      "Managing a department on a ship is a full-time job, even when things are running smoothly."); }

        if (getStat(Stat.engineering) >= 500.0f) { skillLevels.Add("Engineering: Epic",             "Wielding knowledge of the ancient men, no system is too complex or no crisis out of control."); }
        else if (getStat(Stat.engineering) >= 400.0f) { skillLevels.Add("Engineering: Legendary",   "Extremely capable with ship systems. No systems failure seems too difficult."); }
        else if (getStat(Stat.engineering) >= 350.0f) { skillLevels.Add("Engineering: Exceptional", "Rare talent with ship systems and exceptional damage control."); }
        else if (getStat(Stat.engineering) >= 300.0f) { skillLevels.Add("Engineering: Remarcable",  "Deep understanding with ship systems and the capacity to handle most crisis."); }
        else if (getStat(Stat.engineering) >= 250.0f) { skillLevels.Add("Engineering: Excellent",   "Experienced with ship systems and the ability to handle small crisis."); }
        else if (getStat(Stat.engineering) >= 200.0f) { skillLevels.Add("Engineering: Very Good",   "Understanding of the ship systems and the ability to maintain them."); }
        else if (getStat(Stat.engineering) >= 150.0f) { skillLevels.Add("Engineering: Good",        "Basic understanding of the ship systems and the ability to do routine maintainance."); }
        else if (getStat(Stat.engineering) >= 100.0f) { skillLevels.Add("Engineering: Trained",     "Rudimentary understanding of the most basic ship systems."); }

        if (getStat(Stat.precognition) >= 500.0f) { skillLevels.Add("Precognition: Epic",           ""); }
        else if (getStat(Stat.precognition) >= 400.0f) { skillLevels.Add("Precognition: Legendary", ""); }
        else if (getStat(Stat.precognition) >= 350.0f) { skillLevels.Add("Precognition: Exceptional",""); }
        else if (getStat(Stat.precognition) >= 300.0f) { skillLevels.Add("Precognition: Remarcable", ""); }
        else if (getStat(Stat.precognition) >= 250.0f) { skillLevels.Add("Precognition: Good",      ""); }
        else if (getStat(Stat.precognition) >= 200.0f) { skillLevels.Add("Precognition: Fair",      ""); }
        else if (getStat(Stat.precognition) >= 150.0f) { skillLevels.Add("Precognition: Poor",      ""); }

        if (getStat(Stat.psy) >= 500.0f) { skillLevels.Add("Fracker: Epic",                         "Frackers in this scale are singular. There is a theory that they are the re-incarnating spirit of the first awoken, Senno Shoru."); }
        else if (getStat(Stat.psy) >= 400.0f) { skillLevels.Add("Fracker: Legendary",               "There are but a handful of frackers existing on this level. They are said to be living portals betweem this world and the fracture. They are terrifying individuals."); }
        else if (getStat(Stat.psy) >= 350.0f) { skillLevels.Add("Fracker: Powerful",                "Powerful frackers can alter the reality at will, using the terrible powers of the fracture. Powerful frackers are a source of great value, and great fear."); }
        else if (getStat(Stat.psy) >= 300.0f) { skillLevels.Add("Fracker: Strong",                  "Strong frackers can sense and manipulate the energies of the fracture to visible effects. Strong frackers are feared and revered."); }
        else if (getStat(Stat.psy) >= 250.0f) { skillLevels.Add("Fracker",                          "'Frackers' are people with innate ability to sense and even manipulate the energies of another reality - the fracture."); }
        else if (getStat(Stat.psy) >= 200.0f) { skillLevels.Add("Fracker: Awoken",                  "Some people have experienced an awakening, usually during exposure to the powers of the fracture. Th awoken are typically untrained to the forces and risks involved."); }
        else if (getStat(Stat.psy) >= 150.0f) { skillLevels.Add("Fracker: Latent",                  "Few develop sensitivity to the powers of fracture in adolescense. Latent frackers have little abilities but they share the risks involved."); }

        if (getStat(Stat.navigation) >= 500.0f) { skillLevels.Add("Navigator: Epic",                "Some navigators trancend the boundaries of this world. It is prophesied that a navigator will find the way out of the sector one day, and reconnect humanity again."); }
        else if (getStat(Stat.navigation) >= 400.0f) { skillLevels.Add("Navigator: Legendary",      "Best navigators feel fracture on another level, and traverse the deep fracture where no others dare to go. They can find paths for others to follow."); }
        else if (getStat(Stat.navigation) >= 350.0f) { skillLevels.Add("Navigator: Renown",         "Great navigators are sought through the sector to challenge deep fracture. So much rides on the fracture ships that skilled navigators can make a name for themselves."); }
        else if (getStat(Stat.navigation) >= 300.0f) { skillLevels.Add("Navigator: Experienced",    "Experienced navigators are highly valued. They are a crucial part of any successful ship. More experienced navigators can find their way even through patches of high fracture."); }
        else if (getStat(Stat.navigation) >= 250.0f) { skillLevels.Add("Navigator",                 "Navigators are a vast guild of exceptional individuals. It takes years of training and arch tech implants to earn the rank of a navigator. They are crucial to interstellar travel."); }
        else if (getStat(Stat.navigation) >= 200.0f) { skillLevels.Add("Navigation: Trained",       "To be a true navigator, more than training is required. But it is possible for a trained pilot to navigate low fracture without navigator implants."); }
        else if (getStat(Stat.navigation) >= 150.0f) { skillLevels.Add("Navigation: Basic",         "Navigating the fracture is a complicated skill. It takes a real effort to learn even the basics."); }

        if (getStat(Stat.spaceBattle) >= 500.0f) { skillLevels.Add("Space Battle: Epic",            "Mastermind for ship battle tactics. A pseudo-spatial savant whose combat maneuvers defy all logic."); }
        else if (getStat(Stat.spaceBattle) >= 400.0f) { skillLevels.Add("Space Battle: Legendary",  "Master of space battles and an amazing mind challenging the conventional understanding of pseudo-spatial maneuvers."); }
        else if (getStat(Stat.spaceBattle) >= 350.0f) { skillLevels.Add("Space Battle: Inspirational", "Inspired ship battle tactician. Few people show this much potential for pseudo-spatial capacity."); }
        else if (getStat(Stat.spaceBattle) >= 300.0f) { skillLevels.Add("Space Battle: Outstanding", "Solid ship battle tactics veteran and notable talent for pseudo-spatial maneuvering."); }
        else if (getStat(Stat.spaceBattle) >= 250.0f) { skillLevels.Add("Space Battle: Excellent",  "Experienced in ship battle tactics and decent pseudo-spatial talent."); }
        else if (getStat(Stat.spaceBattle) >= 200.0f) { skillLevels.Add("Space Battle: Very Good",  "Understanding in ship battle tactics and some pseudo-spatial talent."); }
        else if (getStat(Stat.spaceBattle) >= 150.0f) { skillLevels.Add("Space Battle: Good",       "Understanding in basic ship battle maneuvers, but little pseudo-spatial talent."); }

        if (getStat(Stat.combat) >= 500.0f) { skillLevels.Add("Combat: Epic",               "Epic prowess in personal combat and awesome at leading the troops in battle."); }
        else if (getStat(Stat.combat) >= 400.0f) { skillLevels.Add("Combat: Legendary",     "Legendary prowess in personal combat and amazing at leading troops in battle."); }
        else if (getStat(Stat.combat) >= 350.0f) { skillLevels.Add("Combat: Inspirational", "Outstanding prowess in personal combat and truly inspiring when leading troops in battle."); }
        else if (getStat(Stat.combat) >= 300.0f) { skillLevels.Add("Combat: Outstanding",   "Great prowess in personal combat and an outstanding ability to lead troops in battle."); }
        else if (getStat(Stat.combat) >= 250.0f) { skillLevels.Add("Combat: Excellent",     "Notable prowess in personal combat and excellent at leading troops in battle."); }
        else if (getStat(Stat.combat) >= 200.0f) { skillLevels.Add("Combat: Very Good",     "Prowess in personal combat and solid leader of the troops in battle."); }
        else if (getStat(Stat.combat) >= 150.0f) { skillLevels.Add("Combat: Good",          "Some prowess in personal combat and the ability to lead the troops in battle."); }

        if (getStat(Stat.trading) >= 500.0f) { skillLevels.Add("Trading: Epic",             "Greatest merchants on the sector are well known and connected. Most of the time they can surprise with truly amazing trades."); }
        else if (getStat(Stat.trading) >= 400.0f) { skillLevels.Add("Trading: Legendary",   "A few merchants are well known throughout the sector. They can have extensive contacts on most worlds or else pull out amazing trades."); }
        else if (getStat(Stat.trading) >= 350.0f) { skillLevels.Add("Trading: Exceptional", "There’s a lot of interstellar trading going on and some merchants are well connected on local markets to find exceptional trades."); }
        else if (getStat(Stat.trading) >= 300.0f) { skillLevels.Add("Trading: Remarcable",  "There’s a lot of interstellar trading going on and some merchants are well connected on local markets to find great trades."); }
        else if (getStat(Stat.trading) >= 250.0f) { skillLevels.Add("Trading: Excellent",   "There’s a lot of interstellar trading going on and an excellent trader can often pull on their contacts on local markets to find the right trade."); }
        else if (getStat(Stat.trading) >= 200.0f) { skillLevels.Add("Trading: Very Good",   "There’s a lot of interstellar trading going on and a good trader can often pull on their contacts on local markets to find the right trade."); }
        else if (getStat(Stat.trading) >= 150.0f) { skillLevels.Add("Trading: Good",        "There’s a lot of interstellar trading going on and a good trader can sometimes pull on their contacts on local markets to find the right trade."); }

        if (getStat(Stat.diplomat) >= 400.0f) { skillLevels.Add("Diplomat",                 "A diplomat is an expert on relations as well as well recognized negotiator in ship's internal matters."); }
        else if (getStat(Stat.diplomat) >= 250.0f) { skillLevels.Add("Negotiator",          "Negotiators are very useful in resolving both internal and external diplomatic relations."); }

        if (getStat(Stat.scientist) >= 500.0f) { skillLevels.Add("Scientist: Epic",             "Epic scientists are unique expert advisors on ships. They can contribute to some of the ship's most complex systems."); }
        else if (getStat(Stat.scientist) >= 400.0f) { skillLevels.Add("Scientist: Legendary",   "Legendary scientists are rare expert advisors on ships. They can contribute to some of the ship's most complex systems."); }
        else if (getStat(Stat.scientist) >= 350.0f) { skillLevels.Add("Scientist: Renown",      "Renown scientists are valued expert advisors on ships. They can contribute to some of the ship's most complex systems."); }
        else if (getStat(Stat.scientist) >= 300.0f) { skillLevels.Add("Scientist: Remarcable",  "Remarcable scientists can advice as experts on technical issues, as well as work to improve some of the ship's systems."); }
        else if (getStat(Stat.scientist) >= 250.0f) { skillLevels.Add("Scientist: Recognized",  "Recognized scientists can advice as experts on technical issues, as well as work to improve some of the ship's systems."); }
        else if (getStat(Stat.scientist) >= 200.0f) { skillLevels.Add("Scientist",              "Scientists can sometimes advice as experts on technical matters."); }

        if (getStat(Stat.integrity) >= 300.0f) { skillLevels.Add("Benefactor",          "Serving others and serving the truth is a sign of a true, virtuous benefactor."); }
        else if (getStat(Stat.integrity) >= 175) { skillLevels.Add("Honest",            "Honesty is a reputation earned in service to the truth and to others. It is a valued virtue."); }
        else if (getStat(Stat.integrity) < -200.0f) { skillLevels.Add("Unscrupulous",   "Some opportunists are well known for their preference in enlightened self-interests. It may be difficult for others to trust such an individual."); }
        else if (getStat(Stat.integrity) < -100.0f) { skillLevels.Add("Opportunist",    "Opportunists care little for the rules and instead let their instincts guide them."); }

        if (getStat(Stat.holiness) >= 500.0f) { skillLevels.Add("Saint",            "Saints are unwavering in their faith and recognized by the Church and the congregation."); }
        else if (getStat(Stat.holiness) >= 400.0f) { skillLevels.Add("Holy",        "The Holy are recognized leaders of their congregation and known for their great spiritual strength"); }
        else if (getStat(Stat.holiness) >= 300.0f) { skillLevels.Add("Revered",     "The revered are often leaders of their congregation and known for their spiritual strength."); }
        else if (getStat(Stat.holiness) >= 200.0f) { skillLevels.Add("Blessed",     "The blessed are often recognized in their congregation and can draw upon inner strength when facing spiritual crisis."); }
        else if (getStat(Stat.holiness) >= 150.0f) { skillLevels.Add("Faithful",    "The faithful are living virtuous lives and can draw upon their faith when facing spiritual crisis."); }
        else if (getStat(Stat.holiness) < -200.0f) { skillLevels.Add("Heretic",     "Heretics are declared lost or even enemies of the Church. They often have to conceal their beliefs to avoid church inquiry."); }
        else if (getStat(Stat.holiness) < -100.0f) { skillLevels.Add("Debauched",   "There are people who not only care little about the teachings of the Church, but actively ignore them in their lives."); }

        if (getStat(Stat.purity) >= 500.0f) { skillLevels.Add("Pure",           "Human genome has been polluted through centuries by the exposure to cosmic radiation, alien environments and the forces of the Fracture. It is rare to find pure human DNA intact."); }
        else if (getStat(Stat.purity) < -100.0f) { skillLevels.Add("Mutant",    "Human genome has been polluted through centuries by the exposure to cosmic radiation, alien environments and the forces of the Fracture. Some individuals are riding the borders of human parameters."); }
        else if (getStat(Stat.purity) < -50.0f) { skillLevels.Add("Tainted",    "Human genome has been polluted through centuries by the exposure to cosmic radiation, alien environments and the forces of the Fracture."); }

        if (getStat(Stat.security) >= 500.0f) { skillLevels.Add("Agent",                ""); }
        else if (getStat(Stat.security) >= 300.0f) { skillLevels.Add("Secret Police",   ""); }
        else if (getStat(Stat.security) >= 175.0f) { skillLevels.Add("Security Expert", ""); }
        else if (getStat(Stat.security) < -150.0f) { skillLevels.Add("Anarchist",       ""); }

        if (getStat(Stat.violent) >= 500.0f) { skillLevels.Add("Raving Mad",        ""); }
        else if (getStat(Stat.violent) >= 400.0f) { skillLevels.Add("Berzerk",      ""); }
        else if (getStat(Stat.violent) >= 300.0f) { skillLevels.Add("Hateful",      ""); }
        else if (getStat(Stat.violent) >= 200.0f) { skillLevels.Add("Aggressive",   ""); }
        else if (getStat(Stat.violent) < -300.0f) { skillLevels.Add("Pasifist",     ""); }
        else if (getStat(Stat.violent) < -100.0f) { skillLevels.Add("Peaceful",     ""); }

        if (getStat(Stat.aristocrat) >= 200.0f) { skillLevels.Add("Aristocrat",     "Members of aristocracy often consider themselves wardens of the people. With greater power comes greater responsibility."); }

        if (getStat(Stat.imperialist) >= 500.0f) { skillLevels.Add("Agent of the Empire","Absolutely loyal to the Imperial Order, agents sometimes hold positions within the various imperial organizations."); }
        else if (getStat(Stat.imperialist) >= 200.0f) { skillLevels.Add("Imperialist",  "Imperialists are loyal citizens of the Imperium."); }
        else if (getStat(Stat.imperialist) >= 100.0f) { skillLevels.Add("Imperialist",  "Imperialists are citizens of the Imperium."); }
        else if (getStat(Stat.imperialist) < -200.0f) { skillLevels.Add("Rebel",        "Known for their anti-imperial ethos, rebels are considered dangerous political dissidents in the Imperium and often need to hide their true political motives."); }
        else if (getStat(Stat.imperialist) < -100.0f) { skillLevels.Add("Separatist",   "Separatists are in favour of seceding from the Imperium. They are considered with prejudice in the Imperium, but can be parts of popular movements in the frontiers."); }

        if (getStat(Stat.corruption) >= 100.0f) { skillLevels.Add("Corrupted",  "Exposure to the forces of the Fracture raise concerns. It is believed to change people."); }

        return skillLevels;
    }

    /// <summary>
    ///   skill, toolTip
    /// </summary>
    /// <returns></returns>
    public List<KeyValuePair<string, string>> getCharacterInfoList()
    {
        List<KeyValuePair<string, string>> lines = new List<KeyValuePair<string, string>>();
        foreach (KeyValuePair<string, string> item in getSkillLevelsList()) lines.Add(item);
        lines.Insert(0, new KeyValuePair < string, string >("Age: " + Mathf.Floor(getStat(Stat.age)).ToString(), "")); //The age of "+name+" in Solar Years.
        lines.Insert(0, new KeyValuePair<string, string>("Affiliation: " + Faction.getPartyName((Faction.IdeologyID)ideology) + " ", Faction.getPartyDescription((Faction.IdeologyID)ideology)));
        lines.Insert(0, new KeyValuePair<string, string>("Trait: " + ShipBonusesStats.formatEnumString(characterTrait.ToString()), getJobName(assignment) + " trait\n" + listOfTraitBonuses() ));
        lines.Insert(0, new KeyValuePair<string, string>("Name: " + name, ""));
        return lines;
    }
    private string listOfTraitBonuses()
    {
        Dictionary<string, int> bonuses = new Dictionary<string, int>();
        KeyValuePair<string, int> pair;
        foreach (ShipBonus bonus in Enum.GetValues(typeof(ShipBonus)))
        {
            pair = ShipBonusesStats.getCharacterStatBonusAndDescription(this, bonus);
            if (pair.Key != "" && pair.Value != 0) bonuses.Add(ShipBonusesStats.formatEnumString(bonus.ToString()), pair.Value);
        }
        string rs = "";
        foreach (var kvp in bonuses)
        {
            rs += "\n" + kvp.Key + ": " + ShipStatBonusUI.valueToString(kvp.Value);
        }
        return rs;
    }

    public string toDebugString()
    {
        string rv = "";
        rv += "Name: "+name+ "  Job: " +getJobName(assignment)+ "  Affiliation: "+ Faction.getPartyName((Faction.IdeologyID)ideology) +"\n";
        foreach (string skill in getSkillLevelsList().Keys)
        {
            rv += skill + "\n";
        }
        return rv;
    }

    // ------------------------------------------ hard-coded stats for skill bases (spreadsheet calculations /Characters)

    private void rollStats(Character template = null)
    {
        if (template != null)
        {
            // copy template over (only assigned values are in the template.stats)
            foreach (var pair in template.stats)
            {
                //Debug.Log("copying template stat: " + pair.Key.ToString());
                setStat(pair.Key, pair.Value);
            }
        }
        // initial experience: age-independent brilliance/competence
        if (template != null && template.stats.ContainsKey(Stat.experience)) setStat(Stat.experience, template.getStat(Stat.experience)); else setStat(Stat.experience, Mathf.Round(UnityEngine.Random.Range(-10, 25)));

        // age
        if (template != null && template.stats.ContainsKey(Stat.age)) setStat(Stat.age, template.getStat(Stat.age)); else setStat(Stat.age, UnityEngine.Random.Range(24f + UnityEngine.Random.value * getStat(Stat.experience), 64f + (UnityEngine.Random.value * getStat(Stat.experience) / 2f)));
        // add experience from age
        addStat(Stat.experience, /*Mathf.Round(Mathf.Pow(UnityEngine.Random.value, 2f) **/ getStat(Stat.age) - 24f);

        // gender
        if (template != null && template.isMale != null) isMale = template.isMale; else isMale = (UnityEngine.Random.value < 0.65f) ? true : false;
        // corruption
        if (template != null && template.stats.ContainsKey(Stat.corruption)) setStat(Stat.corruption, template.getStat(Stat.corruption)); else setStat(Stat.corruption, Mathf.Pow(UnityEngine.Random.value, 20f) * stats[Stat.psy]);
        // insanity
        if (template != null && template.stats.ContainsKey(Stat.insanity)) setStat(Stat.insanity, template.getStat(Stat.insanity)); else { setStat(Stat.insanity, Mathf.Pow(UnityEngine.Random.Range(0f, 2.515f), 5f)); if (getStat(Stat.insanity) < 30f) setStat(Stat.insanity, 0f); }
        // clone
        if (template != null && template.stats.ContainsKey(Stat.clone)) setStat(Stat.clone, template.getStat(Stat.clone)); else if (UnityEngine.Random.value < 0.2f) { setStat(Stat.clone, Mathf.Pow(UnityEngine.Random.value * 10f, 2f)); } // 80% of the time is 0%
        if (template != null && template.HQclone == null) HQclone = template.HQclone; else HQclone = (UnityEngine.Random.value < 0.05f) ? true : false;
        // possessed
        if (template != null && template.stats.ContainsKey(Stat.possessed)) setStat(Stat.possessed, template.getStat(Stat.possessed)); else { setStat(Stat.possessed, Mathf.Pow(UnityEngine.Random.Range(0f, 2.515f), 5f)); if (getStat(Stat.possessed) < 30f) setStat(Stat.possessed, 0f); }
        // idealist
        if (template != null && template.stats.ContainsKey(Stat.idealist)) setStat(Stat.idealist, template.getStat(Stat.idealist)); else { setStat(Stat.idealist, UnityEngine.Random.value*100f); }
        // kind
        if (template != null && template.stats.ContainsKey(Stat.kind)) setStat(Stat.kind, template.getStat(Stat.kind)); else { setStat(Stat.kind, UnityEngine.Random.value * 100f); }

        // IDEOLOGY
        foreach (var pair in getBaseStatsByIdeology((Faction.IdeologyID) ideology))
        {
            stats[pair.Key] = pair.Value;
        }

        // add stat rolls
        foreach (Stat stat in getStatNames())
        {
            if (template != null && template.stats.ContainsKey(stat)) addStat(stat, UnityEngine.Random.value); 
            else addStat(stat, roll(stat, getStat(Stat.experience)));
        }

        // loyalty is stat-based
        if (template != null && template.stats.ContainsKey(Stat.loyalty)) setStat(Stat.loyalty, template.getStat(Stat.loyalty));
        else if (UnityEngine.Random.value * 100f < getStat(Stat.integrity) / 2f + 50f) { setStat(Stat.loyalty, UnityEngine.Random.Range(40f, 70f)); } else setStat(Stat.loyalty, UnityEngine.Random.Range(0f, 70f));

    }

    float roll (Stat stat, float experience = 0f)
    {
        float roll = 0;
        //if (roll > 0.9f) { } // "exploading die"

        // skills
        if (stat == Stat.leadership || stat == Stat.hr || stat == Stat.engineering || stat == Stat.precognition || stat == Stat.psy || stat == Stat.navigation || stat == Stat.trading || stat == Stat.diplomat || stat == Stat.scientist || stat == Stat.security)
        {
            if (UnityEngine.Random.value < 0.5f) roll = Mathf.Pow(UnityEngine.Random.value, 3f) * (200f + experience);
            else roll = Mathf.Pow(UnityEngine.Random.value, 3f) * (-200f + experience);
        }
        else if (stat == Stat.combat)
        {
            if (UnityEngine.Random.value < 0.5f) roll = Mathf.Pow(UnityEngine.Random.value, 3f) * (200f + experience);
            else roll = Mathf.Pow(UnityEngine.Random.value, 3f) * (-200f + experience);
            if (getStat(Stat.age) > 50) { roll += (UnityEngine.Random.value * experience - (getStat(Stat.age) - 50f) * 7.5f); }
        }
        else if (stat == Stat.spaceBattle)
        {
            if (UnityEngine.Random.value < 0.5f) roll = Mathf.Pow(UnityEngine.Random.value, 3f) * (200f + experience);
            else roll = Mathf.Pow(UnityEngine.Random.value, 3f) * (-200f + experience);
            if (getStat(Stat.age) > 58) roll += experience - ((getStat(Stat.age) - 58f) * 2.5f);
        }
        // other stats
        else if (stat == Stat.violent) { roll = (UnityEngine.Random.value * 200f - 100f); }
        else if (stat == Stat.purity) { roll = UnityEngine.Random.value * 100f - UnityEngine.Random.value * experience; } //roll = UnityEngine.Random.value * 2f - roll * 100f + stats[Stat.purity] * experience; } // if (HQclone && ideology != "navigators") { stats[Stat.purity] /= 3f; }


        return roll;
    }

    public static Dictionary<Stat, float> getBaseStatsByIdeology(Faction.IdeologyID ideology)
    {
        Dictionary<Stat, float> stats = new Dictionary<Stat, float>();

        if (ideology == Faction.IdeologyID.cult)
        {
            stats.Add(Stat.leadership, 100f);
            stats.Add(Stat.hr, -15f);
            stats.Add(Stat.engineering, -40f);
            stats.Add(Stat.precognition, -70f);
            stats.Add(Stat.psy, -70f);
            stats.Add(Stat.navigation, -40f);
            stats.Add(Stat.spaceBattle, 100f);
            stats.Add(Stat.combat, 200f);
            stats.Add(Stat.trading, -100f);
            stats.Add(Stat.diplomat, -100f);
            stats.Add(Stat.scientist, -100f);
            stats.Add(Stat.integrity, 180f);
            stats.Add(Stat.holiness, 100f);
            stats.Add(Stat.purity, 100f);
            stats.Add(Stat.security, 100f);
            stats.Add(Stat.violent, 100f);
            stats.Add(Stat.aristocrat, -30f);
            stats.Add(Stat.imperialist, 80f);
        }
        else if (ideology == Faction.IdeologyID.technocrat)
        {
            stats.Add(Stat.leadership, 0f);
            stats.Add(Stat.hr, 0f);
            stats.Add(Stat.engineering, 130f);
            stats.Add(Stat.precognition, -90f);
            stats.Add(Stat.psy, -100f);
            stats.Add(Stat.navigation, 70f);
            stats.Add(Stat.spaceBattle, 90f);
            stats.Add(Stat.combat, -20f);
            stats.Add(Stat.trading, 15f);
            stats.Add(Stat.diplomat, 0f);
            stats.Add(Stat.scientist, 70f);
            stats.Add(Stat.integrity, 0f);
            stats.Add(Stat.holiness, -20f);
            stats.Add(Stat.purity, -20f);
            stats.Add(Stat.security, -20f);
            stats.Add(Stat.violent, -20f);
            stats.Add(Stat.aristocrat, 10f);
            stats.Add(Stat.imperialist, -10f);
        }
        else if (ideology == Faction.IdeologyID.mercantile)
        {
            stats.Add(Stat.leadership, 50f);
            stats.Add(Stat.hr, -5f);
            stats.Add(Stat.engineering, 5f);
            stats.Add(Stat.precognition, 90f);
            stats.Add(Stat.psy, 20f);
            stats.Add(Stat.navigation, 100f);
            stats.Add(Stat.spaceBattle, 10f);
            stats.Add(Stat.combat, -40f);
            stats.Add(Stat.trading, 150f);
            stats.Add(Stat.diplomat, 200f);
            stats.Add(Stat.scientist, 20f);
            stats.Add(Stat.integrity, -140f);
            stats.Add(Stat.holiness, -40f);
            stats.Add(Stat.purity, -40f);
            stats.Add(Stat.security, -60f);
            stats.Add(Stat.violent, -70f);
            stats.Add(Stat.aristocrat, 100f);
            stats.Add(Stat.imperialist, -20f);
        }
        else if (ideology == Faction.IdeologyID.bureaucracy)
        {
            stats.Add(Stat.leadership, 0f);
            stats.Add(Stat.hr, 85f);
            stats.Add(Stat.engineering, 35f);
            stats.Add(Stat.precognition, -10f);
            stats.Add(Stat.psy, -20f);
            stats.Add(Stat.navigation, -10f);
            stats.Add(Stat.spaceBattle, 0f);
            stats.Add(Stat.combat, -20f);
            stats.Add(Stat.trading, 35f);
            stats.Add(Stat.diplomat, 10f);
            stats.Add(Stat.scientist, -10f);
            stats.Add(Stat.integrity, -70f);
            stats.Add(Stat.holiness, -10f);
            stats.Add(Stat.purity, -10f);
            stats.Add(Stat.security, 40f);
            stats.Add(Stat.violent, 0f);
            stats.Add(Stat.aristocrat, 30f);
            stats.Add(Stat.imperialist, 5f);
        }
        else if (ideology == Faction.IdeologyID.liberal)
        {
            stats.Add(Stat.leadership, 0f);
            stats.Add(Stat.hr, -15f);
            stats.Add(Stat.engineering, 30f);
            stats.Add(Stat.precognition, 90f);
            stats.Add(Stat.psy, 40f);
            stats.Add(Stat.navigation, 20f);
            stats.Add(Stat.spaceBattle, 0f);
            stats.Add(Stat.combat, -100f);
            stats.Add(Stat.trading, 85f);
            stats.Add(Stat.diplomat, 200f);
            stats.Add(Stat.scientist, 100f);
            stats.Add(Stat.integrity, 40f);
            stats.Add(Stat.holiness, -60f);
            stats.Add(Stat.purity, -60f);
            stats.Add(Stat.security, -100f);
            stats.Add(Stat.violent, -100f);
            stats.Add(Stat.aristocrat, -70f);
            stats.Add(Stat.imperialist, -50f);
        }
        else if (ideology == Faction.IdeologyID.nationalist)
        {
            stats.Add(Stat.leadership, 80f);
            stats.Add(Stat.hr, 80f);
            stats.Add(Stat.engineering, 70f);
            stats.Add(Stat.precognition, -50f);
            stats.Add(Stat.psy, -50f);
            stats.Add(Stat.navigation, -100f);
            stats.Add(Stat.spaceBattle, 160f);
            stats.Add(Stat.combat, 160f);
            stats.Add(Stat.trading, -55f);
            stats.Add(Stat.diplomat, -100f);
            stats.Add(Stat.scientist, -20f);
            stats.Add(Stat.integrity, 20f);
            stats.Add(Stat.holiness, 0f);
            stats.Add(Stat.purity, 0f);
            stats.Add(Stat.security, 70f);
            stats.Add(Stat.violent, 100f);
            stats.Add(Stat.aristocrat, -30f);
            stats.Add(Stat.imperialist, -100f);
        }
        else if (ideology == Faction.IdeologyID.aristocrat)
        {
            stats.Add(Stat.leadership, 110f);
            stats.Add(Stat.hr, 50f);
            stats.Add(Stat.engineering, 20f);
            stats.Add(Stat.precognition, 60f);
            stats.Add(Stat.psy, 30f);
            stats.Add(Stat.navigation, 10f);
            stats.Add(Stat.spaceBattle, 160f);
            stats.Add(Stat.combat, 160f);
            stats.Add(Stat.trading, 10f);
            stats.Add(Stat.diplomat, -20f);
            stats.Add(Stat.scientist, -30f);
            stats.Add(Stat.integrity, -80f);
            stats.Add(Stat.holiness, -30f);
            stats.Add(Stat.purity, -30f);
            stats.Add(Stat.security, 80f);
            stats.Add(Stat.violent, 60f);
            stats.Add(Stat.aristocrat, 100f);
            stats.Add(Stat.imperialist, 50f);
        }
        else if (ideology == Faction.IdeologyID.imperialist)
        {
            stats.Add(Stat.leadership, 90f);
            stats.Add(Stat.hr, -5f);
            stats.Add(Stat.engineering, -5f);
            stats.Add(Stat.precognition, 50f);
            stats.Add(Stat.psy, 20f);
            stats.Add(Stat.navigation, 30f);
            stats.Add(Stat.spaceBattle, 110f);
            stats.Add(Stat.combat, 120f);
            stats.Add(Stat.trading, 5f);
            stats.Add(Stat.diplomat, 20f);
            stats.Add(Stat.scientist, -20f);
            stats.Add(Stat.integrity, 40f);
            stats.Add(Stat.holiness, 60f);
            stats.Add(Stat.purity, 60f);
            stats.Add(Stat.security, 30f);
            stats.Add(Stat.violent, 30f);
            stats.Add(Stat.aristocrat, 50f);
            stats.Add(Stat.imperialist, 100f);
        }
        else if (ideology == Faction.IdeologyID.navigators)
        {
            stats.Add(Stat.leadership, 100f);
            stats.Add(Stat.hr, -230f);
            stats.Add(Stat.engineering, 55f);
            stats.Add(Stat.precognition, 160f);
            stats.Add(Stat.psy, 100f);
            stats.Add(Stat.navigation, 200f);
            stats.Add(Stat.spaceBattle, 190f);
            stats.Add(Stat.combat, 80f);
            stats.Add(Stat.trading, 75f);
            stats.Add(Stat.diplomat, 100f);
            stats.Add(Stat.scientist, 70f);
            stats.Add(Stat.integrity, -30f);
            stats.Add(Stat.holiness, 0f);
            stats.Add(Stat.purity, 0f);
            stats.Add(Stat.security, -20f);
            stats.Add(Stat.violent, -50f);
            stats.Add(Stat.aristocrat, 100f);
            stats.Add(Stat.imperialist, 10f);
        }
        else if (ideology == Faction.IdeologyID.brotherhood)
        {
            stats.Add(Stat.leadership, 10f);
            stats.Add(Stat.hr, -40f);
            stats.Add(Stat.engineering, 0f);
            stats.Add(Stat.precognition, 210f);
            stats.Add(Stat.psy, 200f);
            stats.Add(Stat.navigation, 60f);
            stats.Add(Stat.spaceBattle, 60f);
            stats.Add(Stat.combat, 0f);
            stats.Add(Stat.trading, 10f);
            stats.Add(Stat.diplomat, 20f);
            stats.Add(Stat.scientist, 50f);
            stats.Add(Stat.integrity, 0f);
            stats.Add(Stat.holiness, 10f);
            stats.Add(Stat.purity, 10f);
            stats.Add(Stat.security, -40f);
            stats.Add(Stat.violent, -60f);
            stats.Add(Stat.aristocrat, 70f);
            stats.Add(Stat.imperialist, 5f);
        }
        else if (ideology == Faction.IdeologyID.transhumanist)
        {
            stats.Add(Stat.leadership, 0f);
            stats.Add(Stat.hr, 150f);
            stats.Add(Stat.engineering, 75f);
            stats.Add(Stat.precognition, 110f);
            stats.Add(Stat.psy, 60f);
            stats.Add(Stat.navigation, 40f);
            stats.Add(Stat.spaceBattle, 50f);
            stats.Add(Stat.combat, -100f);
            stats.Add(Stat.trading, 65f);
            stats.Add(Stat.diplomat, 150f);
            stats.Add(Stat.scientist, 100f);
            stats.Add(Stat.integrity, -100f);
            stats.Add(Stat.holiness, -100f);
            stats.Add(Stat.purity, -100f);
            stats.Add(Stat.security, -200f);
            stats.Add(Stat.violent, 20f);
            stats.Add(Stat.aristocrat, -100f);
            stats.Add(Stat.imperialist, -100f);
        }
        return stats;
    }


    public static CharacterTrait[] getJobTraits(Character.Job job)
    {
        switch (job)
        {
            case Job.none:
                return null;
            case Job.captain:
                return new CharacterTrait[] { CharacterTrait.PersonalLeadership, CharacterTrait.Bureaucrat, CharacterTrait.SpiritualLeader, CharacterTrait.TacticalLeader, CharacterTrait.MerchantPrince };
            case Job.navigator:
                return new CharacterTrait[] { CharacterTrait.FractureLink, CharacterTrait.CortexLink };
            case Job.engineer:
                return new CharacterTrait[] { CharacterTrait.CivilianEngineer, CharacterTrait.MilitaryEngineer };
            case Job.security:
                return new CharacterTrait[] { CharacterTrait.ShipOfficer, CharacterTrait.TroopOfficer, CharacterTrait.SecurityOfficer };
            case Job.quartermaster:
                return new CharacterTrait[] { CharacterTrait.CrewLiaison, CharacterTrait.Administrator, CharacterTrait.Councellor };
            case Job.priest:
                return new CharacterTrait[] { CharacterTrait.Father, CharacterTrait.Priest };
            case Job.psycher:
                return new CharacterTrait[] { CharacterTrait.ShipFracker, CharacterTrait.CrewFracker, CharacterTrait.Negotiator };
            default:
                return null;
        }
    }
}

public enum CharacterTrait
{
    None,
    // captain

    /// <summary>
    /// [captain trait]
    /// leadership, diplomacy, combat½, hr½
    /// </summary>
    PersonalLeadership,
    // captain
    /// <summary>
    /// [captain trait]
    /// hr, trade, combat
    /// </summary>
    Bureaucrat,
    // captain
    /// <summary>
    /// [captain trait]
    /// leadership, fracture, holy
    /// </summary>
    SpiritualLeader,
    // captain
    /// <summary>
    /// [captain trait]
    /// space battle, navigation, engineering
    /// </summary>
    TacticalLeader,
    // captain
    /// <summary>
    /// [captain trait]
    /// leadership, diplomacy, trade
    /// </summary>
    MerchantPrince,

    // navigator

    /// <summary>
    /// [navigator trait]
    /// navigation, fracture½
    /// </summary>
    FractureLink,
    /// <summary>
    /// [navigator trait]
    /// navigation, space battle, engineering
    /// </summary>
    CortexLink,

    // quartermaster

    /// <summary>
    /// [quartermaster trait]
    /// hr, combat*.25, security*.25
    /// </summary>
    CrewLiaison,
    /// <summary>
    /// [quartermaster trait]
    /// hr, diplomacy½, trade½
    /// </summary>
    Administrator,
    /// <summary>
    /// [quartermaster trait]
    ///  hr, fracture, holy
    /// </summary>
    Councellor,

    // security

    /// <summary>
    /// [security officer trait]
    /// space battle, engineering
    /// </summary>
    ShipOfficer,
    /// <summary>
    /// [security officer trait]
    /// leadership, combat, engineering
    /// </summary>
    TroopOfficer,
    /// <summary>
    /// [security officer trait]
    /// security, combat½
    /// </summary>
    SecurityOfficer,

    // engineer

    /// <summary>
    /// [engineer trait]
    /// (engineering, navigation½) OR science, hr½
    /// </summary>
    CivilianEngineer,
    /// <summary>
    /// [engineer trait]
    /// engineering, space battle, navigation½
    /// </summary>
    MilitaryEngineer,

    // psycher

    /// <summary>
    /// [fracker trait]
    /// fracture
    /// </summary>
    ShipFracker,
    /// <summary>
    /// [fracker trait]
    /// fracture, hr.25
    /// </summary>
    CrewFracker,
    /// <summary>
    /// [fracker trait] (inquisitor?)
    /// fracture, diplomacy½, trade½
    /// </summary>
    Negotiator,

    // priest

    /// <summary>
    /// [priest trait]
    /// holy, hr½
    /// </summary>
    Father,
    /// <summary>
    /// [priest trait]
    /// holy, combat½
    /// </summary>
    Priest
}
