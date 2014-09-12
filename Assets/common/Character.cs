using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterCreator
{
    public CharacterCreator(Vector2 pos)
    {
        string homeId = Game.universe.getClosestHabitat(pos).id;
    }
}

public class Character
{
    static public Character Empty = new Character();

    static public Character[] sortBy(Stat s, Character[] c)
    {
        // returns with new objects!!! cannot be used
        return c.OrderBy(o => o.stats[s]).ToArray();
    }

    static public Character getBest(Character[] c, Stat s)
    {
        return c.OrderBy(o => o.stats[s]).First();
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
        age,
        health,
        polluted,
        insanity,
        clone,
        possessed,
        ai,
        loyalty,
        happiness,
        idealist,
        kind,
        leadership,
        emissar,
        hr,
        engineering,
        precognition,
        psy,
        navigation,
        spaceBattle,
        combat,
        trading,
        diplomat,
        scientist,
        integrity,
        holiness,
        purity,
        security,
        violent,
        aristocrat,
        imperialist,
        corruption
    }

    public  bool    isActive       = true;
    public  bool    player         = false;
    public  string  name           = "Unnamed";
    public  float   age            = 45;
    public  string  gender         = null;
    public  GameObject  origins    = null; //GameObject.Find("4EG01");
    public  string  affiliation    = null; // faction
    public  string  ideology       = null;
    public  string  agenda         = null;
    public  Job     assignment     = Job.none;
    public  float   dateAssigned   = -1;
    public int id { get; private set; }

    /*
    public  float   health         = 100;          // 0 - 100
    public  float   polluted       = 0;            // 0 - 100
    public  float   insanity       = 0;            // 0 - 100
    public  float   clone          = 0;            // 0 - 100
    public  bool    HQclone        = false;        // high quality clone?
    public  float   possessed      = 0;            // 0 - 100
    public  float   ai             = 0;            // 0 - 100
    public  float   loyalty        = 50;           // 0 - 100
    public  float   happiness      = 50;           // 0 - 100
    public  bool    navigator      = false;
    public  bool    psycher        = false;
    public  float   idealist       = 50;
    public  float   kind           = 50;

    public  float   leadership     = 0;
    public  float   emissar        = 0;
    public  float   hr             = 0;
    public  float   engineering    = 0;
    public  float   precognition   = 0;
    public  float   psy            = 0;
    public  float   navigation     = 0;
    public  float   spaceBattle    = 0;
    public  float   combat         = 0;
    public  float   trading        = 0;
    public  float   diplomat       = 0;
    public  float   scientist      = 0;
    public  float   integrity      = 0;
    public  float   holiness       = 0;
    public  float   purity         = 0;
    public  float   security       = 0;
    public  float   violent        = 0;
    public  float   aristocrat     = 0;
    public  float   imperialist    = 0;
    public  float   corruption     = 0;

    Dictionary<string, float> ideologies = new Dictionary<string, float>();
    */
    Dictionary<Stat, float> stats = new Dictionary<Stat,float>();

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
            "emissar",
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
    static public Stat[] getSkills()
    {
        return new Stat[]
        {
            Stat.leadership,
            Stat.emissar,
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
            Stat.emissar,
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
        id = ids;
        ++ids;

        // get appropriate portrait
        portrait = Game.PortraitManager.getPortrait("");
        // get name
        name = NameGenerator.getName("any");
        // setup stats
        foreach (Stat s in (Stat[])Enum.GetValues(typeof(Stat)))
        {
            stats[s] = 0;
        }
        stats[Stat.age] = 45;
        stats[Stat.health] = 100;
    }

    public float getStat (Stat skill) 
    { 
        return stats[skill];
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
        stats[skill] = amount;
        if (stats[Stat.health] > 100.0f)
        {
            stats[Stat.health] = 100.0f;
        }
    }

    // return all noteworthy skill names for the UI
    List<string> getSkillLevels ()
    {
        List<string> skillLevels = new List<string>();
    
        if (stats[Stat.leadership] >= 500.0f)      { skillLevels.Add("Leadership: Epic"); }
        else if (stats[Stat.leadership] >= 400.0f) { skillLevels.Add("Leadership: Legendary"); }
        else if (stats[Stat.leadership] >= 350.0f) { skillLevels.Add("Leadership: Renown"); }
        else if (stats[Stat.leadership] >= 300.0f) { skillLevels.Add("Leadership: Remarcable"); }
        else if (stats[Stat.leadership] >= 250.0f) { skillLevels.Add("Leadership: Excellent"); }
        else if (stats[Stat.leadership] >= 200.0f) { skillLevels.Add("Leadership: Very Good"); }
        else if (stats[Stat.leadership] >= 150.0f) { skillLevels.Add("Leadership: Good"); }
        else if (stats[Stat.leadership] >= 100.0f) { skillLevels.Add("Leadership: Trained"); }

        if (stats[Stat.hr] >= 500.0f)      { skillLevels.Add("Human Resources: Epic"); }
        else if (stats[Stat.hr] >= 400.0f) { skillLevels.Add("Human Resources: Legendary"); }
        else if (stats[Stat.hr] >= 350.0f) { skillLevels.Add("Human Resources: Exceptional"); }
        else if (stats[Stat.hr] >= 300.0f) { skillLevels.Add("Human Resources: Remarcable"); }
        else if (stats[Stat.hr] >= 250.0f) { skillLevels.Add("Human Resources: Excellent"); }
        else if (stats[Stat.hr] >= 200.0f) { skillLevels.Add("Human Resources: Very Good"); }
        else if (stats[Stat.hr] >= 150.0f) { skillLevels.Add("Human Resources: Good"); }
        else if (stats[Stat.hr] >= 100.0f) { skillLevels.Add("Human Resources: Trained"); }

        if (stats[Stat.engineering] >= 500.0f)      { skillLevels.Add("Engineering: Epic"); }
        else if (stats[Stat.engineering] >= 400.0f) { skillLevels.Add("Engineering: Legendary"); }
        else if (stats[Stat.engineering] >= 350.0f) { skillLevels.Add("Engineering: Exceptional"); }
        else if (stats[Stat.engineering] >= 300.0f) { skillLevels.Add("Engineering: Remarcable"); }
        else if (stats[Stat.engineering] >= 250.0f) { skillLevels.Add("Engineering: Excellent"); }
        else if (stats[Stat.engineering] >= 200.0f) { skillLevels.Add("Engineering: Very Good"); }
        else if (stats[Stat.engineering] >= 150.0f) { skillLevels.Add("Engineering: Good"); }
        else if (stats[Stat.engineering] >= 100.0f) { skillLevels.Add("Engineering: Trained"); }

        if (stats[Stat.precognition] >= 500.0f)      { skillLevels.Add("Precognition: Epic"); }
        else if (stats[Stat.precognition] >= 400.0f) { skillLevels.Add("Precognition: Legendary"); }
        else if (stats[Stat.precognition] >= 350.0f) { skillLevels.Add("Precognition: Exceptional"); }
        else if (stats[Stat.precognition] >= 300.0f) { skillLevels.Add("Precognition: Remarcable"); }
        else if (stats[Stat.precognition] >= 250.0f) { skillLevels.Add("Precognition: Good"); }
        else if (stats[Stat.precognition] >= 200.0f) { skillLevels.Add("Precognition: Fair"); }
        else if (stats[Stat.precognition] >= 150.0f) { skillLevels.Add("Precognition: Poor"); }

        if (stats[Stat.psy] >= 500.0f)      { skillLevels.Add("Psycher: Epic"); }
        else if (stats[Stat.psy] >= 400.0f) { skillLevels.Add("Psycher: Legendary"); }
        else if (stats[Stat.psy] >= 350.0f) { skillLevels.Add("Psycher: Powerful"); }
        else if (stats[Stat.psy] >= 300.0f) { skillLevels.Add("Psycher: Strong"); }
        else if (stats[Stat.psy] >= 250.0f) { skillLevels.Add("Psycher"); }
        else if (stats[Stat.psy] >= 200.0f) { skillLevels.Add("Psychic: Awoken"); }
        else if (stats[Stat.psy] >= 150.0f) { skillLevels.Add("Psychic: Latent"); }

        if (stats[Stat.navigation] >= 500.0f)      { skillLevels.Add("Navigator: Epic"); }
        else if (stats[Stat.navigation] >= 400.0f) { skillLevels.Add("Navigator: Legendary"); }
        else if (stats[Stat.navigation] >= 350.0f) { skillLevels.Add("Navigator: Renown"); }
        else if (stats[Stat.navigation] >= 300.0f) { skillLevels.Add("Navigator: Experienced"); }
        else if (stats[Stat.navigation] >= 250.0f) { skillLevels.Add("Navigator"); }
        else if (stats[Stat.navigation] >= 200.0f) { skillLevels.Add("Navigation: Trained"); }
        else if (stats[Stat.navigation] >= 150.0f) { skillLevels.Add("Navigation: Basic"); }

        if (stats[Stat.spaceBattle] >= 500.0f)      { skillLevels.Add("Space Battle: Epic"); }
        else if (stats[Stat.spaceBattle] >= 400.0f) { skillLevels.Add("Space Battle: Legendary"); }
        else if (stats[Stat.spaceBattle] >= 350.0f) { skillLevels.Add("Space Battle: Inspirational"); }
        else if (stats[Stat.spaceBattle] >= 300.0f) { skillLevels.Add("Space Battle: Outstanding"); }
        else if (stats[Stat.spaceBattle] >= 250.0f) { skillLevels.Add("Space Battle: Excellent"); }
        else if (stats[Stat.spaceBattle] >= 200.0f) { skillLevels.Add("Space Battle: Very Good"); }
        else if (stats[Stat.spaceBattle] >= 150.0f) { skillLevels.Add("Space Battle: Good"); }

        if (stats[Stat.combat] >= 500.0f)      { skillLevels.Add("Combat: Epic"); }
        else if (stats[Stat.combat] >= 400.0f) { skillLevels.Add("Combat: Legendary"); }
        else if (stats[Stat.combat] >= 350.0f) { skillLevels.Add("Combat: Inspirational"); }
        else if (stats[Stat.combat] >= 300.0f) { skillLevels.Add("Combat: Outstanding"); }
        else if (stats[Stat.combat] >= 250.0f) { skillLevels.Add("Combat: Excellent"); }
        else if (stats[Stat.combat] >= 200.0f) { skillLevels.Add("Combat: Very Good"); }
        else if (stats[Stat.combat] >= 150.0f) { skillLevels.Add("Combat: Good"); }

        if (stats[Stat.trading] >= 500.0f)      { skillLevels.Add("Trading: Epic"); }
        else if (stats[Stat.trading] >= 400.0f) { skillLevels.Add("Trading: Legendary"); }
        else if (stats[Stat.trading] >= 350.0f) { skillLevels.Add("Trading: Exceptional"); }
        else if (stats[Stat.trading] >= 300.0f) { skillLevels.Add("Trading: Remarcable"); }
        else if (stats[Stat.trading] >= 250.0f) { skillLevels.Add("Trading: Excellent"); }
        else if (stats[Stat.trading] >= 200.0f) { skillLevels.Add("Trading: Very Good"); }
        else if (stats[Stat.trading] >= 150.0f) { skillLevels.Add("Trading: Good"); }

        if (stats[Stat.diplomat] >= 400.0f)      { skillLevels.Add("Diplomat"); }
        else if (stats[Stat.diplomat] >= 250.0f) { skillLevels.Add("Negotiator"); }

        if (stats[Stat.scientist] >= 500.0f)      { skillLevels.Add("Scientist: Epic"); }
        else if (stats[Stat.scientist] >= 400.0f) { skillLevels.Add("Scientist: Legendary"); }
        else if (stats[Stat.scientist] >= 350.0f) { skillLevels.Add("Scientist: Renown"); }
        else if (stats[Stat.scientist] >= 300.0f) { skillLevels.Add("Scientist: Remarcable"); }
        else if (stats[Stat.scientist] >= 250.0f) { skillLevels.Add("Scientist: Recognized"); }
        else if (stats[Stat.scientist] >= 200.0f) { skillLevels.Add("Scientist"); }

        if (stats[Stat.integrity] >= 300.0f)       { skillLevels.Add("Benefactor"); }
        else if (stats[Stat.integrity] >= 175)     { skillLevels.Add("Honest"); }
        else if (stats[Stat.integrity] < -200.0f)  { skillLevels.Add("Unscrupulous"); }
        else if (stats[Stat.integrity] < -100.0f)  { skillLevels.Add("Opportunist"); }

        if (stats[Stat.holiness] >= 500.0f)      { skillLevels.Add("Saint"); }
        else if (stats[Stat.holiness] >= 400.0f) { skillLevels.Add("Holy"); }
        else if (stats[Stat.holiness] >= 300.0f) { skillLevels.Add("Revered"); }
        else if (stats[Stat.holiness] >= 200.0f) { skillLevels.Add("Blessed"); }
        else if (stats[Stat.holiness] >= 150.0f) { skillLevels.Add("Faithful"); }
        else if (stats[Stat.holiness] < -200.0f) { skillLevels.Add("Heretic"); }
        else if (stats[Stat.holiness] < -100.0f) { skillLevels.Add("Debauched"); }

        if (stats[Stat.purity] >= 500.0f)      { skillLevels.Add("Pure"); }
        else if (stats[Stat.purity] < -100.0f) { skillLevels.Add("Mutant"); }
        else if (stats[Stat.purity] <  -50.0f) { skillLevels.Add("Tainted"); }

        if (stats[Stat.security] >= 500.0f)      { skillLevels.Add("Agent"); }
        else if (stats[Stat.security] >= 300.0f) { skillLevels.Add("Secret Police"); }
        else if (stats[Stat.security] >= 175.0f) { skillLevels.Add("Security Training"); }
        else if (stats[Stat.security] < -150.0f) { skillLevels.Add("Anarchist"); }

        if (stats[Stat.violent] >= 500.0f)      { skillLevels.Add("Raving Mad"); }
        else if (stats[Stat.violent] >= 400.0f) { skillLevels.Add("Berzerk"); }
        else if (stats[Stat.violent] >= 300.0f) { skillLevels.Add("Hateful"); }
        else if (stats[Stat.violent] >= 200.0f) { skillLevels.Add("Aggressive"); }
        else if (stats[Stat.violent] < -300.0f) { skillLevels.Add("Pasifist"); }
        else if (stats[Stat.violent] < -100.0f) { skillLevels.Add("Peaceful"); }

        if (stats[Stat.aristocrat] >= 200.0f) { skillLevels.Add("Aristocrat"); }

        if (stats[Stat.imperialist] >= 500.0f)      { skillLevels.Add("Agent of the Empire"); }
        else if (stats[Stat.imperialist] >= 200.0f) { skillLevels.Add("Imperialist"); }
        else if (stats[Stat.imperialist] >= 100.0f) { skillLevels.Add("Imperialist"); }
        else if (stats[Stat.imperialist] < -200.0f) { skillLevels.Add("Rebel"); }
        else if (stats[Stat.imperialist] < -100.0f) { skillLevels.Add("Separatist"); }

        if (stats[Stat.corruption] >= 100.0f) { skillLevels.Add("Corrupted"); }
                                        
        return skillLevels;
    }
}
