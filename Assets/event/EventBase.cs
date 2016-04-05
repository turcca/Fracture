using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventAdvice
{
    public string text = "NO ADVICE";
    public int recommend = -1;
}

public class EventBase
{
    public enum freq { Rare, Default, Elevated, Probable }
    public enum status { Quiet, Default, Alert, Panic }
    public enum noise { QuietHum, QuietEerie, DefaultBridge, EchoRoom, EngineRoom, ActionBase, Fracture }
    public enum trigger { None, inLocation, atLocation, Object }

    public string name = "";
    public bool available = false;
    public Player player = Root.game.player;
    public Dictionary<string, float> filters = new Dictionary<string, float>();
    public Dictionary<string, int> choices = new Dictionary<string, int>();

    public Character character;
    public Character advisor;
    public string location;
    public int outcome = 0;
    public int choice = -1;
    public bool locationRequired = false;

    public float lastProbability = 0.0f;
    public bool running = false;

    public trigger triggerEvent = trigger.None;


    public EventBase(string _name)
    {
        name = _name;
        available = true;
        initPre();
        initFilters();
        //Debug.Log ("formatting EventBase: "+name+" / "+_name);
        if (locationRequired && location == null) Debug.LogError("Event Parse ERROR: '" + name + "'\n locationRequired, but no location string assigned");
        Root.game.events.addEventToPool(this);
    }
    public void start()
    {
        running = true;
    }
    public void stop()
    {
        running = false;
    }
    public virtual void initPre()
    {
    }
    public virtual float calculateProbability()
    {
        return lastProbability;
    }
    public Dictionary<string, int> getChoices()
    {
        choices.Clear();
        initChoices();
        return choices;
    }
    public virtual EventAdvice getAdvice(Character.Job who)
    {
        return new EventAdvice();
    }
    public virtual string getText()
    {
        return "INSERT EVENT DESCRIPTION TEXT HERE";
    }
    public virtual freq getFrequency()
    {
        return freq.Default;
    }
    public virtual status getCrewStatus()
    {
        return status.Default;
    }
    public virtual noise getAmbientNoise()
    {
        return noise.DefaultBridge;
    }
    public virtual void doOutcome()
    {
        if (choice == 1) end();
    }
    public Character getEventCharacter()
    {
        return character;
    }
    public string getEventLocationID()
    {
        return location;
    }

    public virtual void initChoices()
    {
        choices.Add("CONTINUE", 1);
    }
    public virtual void initFilters()
    {
        addFilter("Filter");
    }
    protected void addFilter(string str)
    {
        filters.Add(str, 1.0f);
    }
    public bool hasFilter(string filter)
    {
        foreach (var f in filters) {
            if (filter == f.Key) return (f.Value > 0.0f) ? true : false;
        }
        return false;
    }

    // -------------------------------------------------------------------

    protected int getElapsedDays()
    {
        return Root.game.getElapsedDays();
    }
    protected double getWarpMagnitude()
    {
        return Root.game.player.getWarpMagnitude();
    }
    protected EventBase getEvent(string name)
    {
        return Root.game.events.getEventByName(name); //EventDataBase.events[name];
    }
    protected string getPlayerLocationID()
    {
        return Root.game.player.getLocationId();
    }
    protected Location getLocation()
    {
        if (location != null && Root.game.locations.ContainsKey(location))
            return Root.game.locations[location];
        else return getCurrentLocation();
    }
    /// <summary>
    /// Access the current location data directly. From event strings, use +getCurrentLocation().name+ for example
    /// </summary>
    /// <returns></returns>
    protected Location getCurrentLocation()
    {
        return Root.game.player.getLocation();
    }
    protected void newLocFactionLeader(string faction, string name)
    {
        Root.game.player.getLocation().newLocationLeader((Faction.FactionID?) Enum.Parse(typeof(Faction.FactionID), faction), name);
    }
    protected PlayerShip getShip()
    {
        return Root.game.player.playerShip;
    }
    protected void factionChange(string f, int a)
    {
        Debug.Log("TODO factionChange()"); //todo
    }
    protected void filterWeight(string w, int a)
    {
        Debug.Log("TODO filterWeight()"); //todo
    }
    protected void leaveLocation()
    {
        Debug.Log("TODO leaveLocation()"); //todo
    }
    protected void startCombat()
    {
        Debug.Log("TODO startCombat()"); //todo
    }

    protected Character getCharacter()
    {
        return character;
    }
    protected Character getCharacter(Character.Job job)
    {
        return (Root.game.player.getAdvisor(job) != null) ? Root.game.player.getAdvisor(job) : (getCharacter() != null) ? getCharacter() : getBestCharacter(Character.Stat.age);
    }
    protected Character getBestCharacter(Character.Stat s)
    {
        return Character.getBest(Root.game.player.getCharacters(), s);
    }
    protected double getCharacterStat(Character.Stat s)
    {
        return character.getStat(s);
    }
    protected double getAdvisorStat(string job, string s)
    {
        return getCharacterStat((Character.Job) Enum.Parse(typeof(Character.Job), job), (Character.Stat)Enum.Parse(typeof(Character.Stat), s));
    }
    protected double getCharacterStat(Character.Job job, Character.Stat s)
    {
        return Root.game.player.getAdvisor(job).getStat(s);
    }
    protected int getShipStat(string s)
    {
        return Root.game.player.shipBonuses.getTotalBonus(s);
    }
    protected float getCredits()
    {
        return Root.game.player.cargo.credits;
    }
    protected void addCredits(float amount)
    {
        Root.game.player.cargo.credits += amount;
        if (Root.game.player.cargo.credits < 0) Debug.LogWarning("events/addCredits: reduced credits below 0 : ("+ Root.game.player.cargo.credits + ")");
    }
    protected int getFactionReputation(string faction)
    {
        return (int)Root.game.player.playerReputation.getReputationValue(Faction.factionToEnum(faction));
    }
    protected string explainRelations(Faction.FactionID faction)
    {
        return Root.game.factions.getFactionRelationsDescription(faction);
    }
    protected string explainRelations(string faction)
    {
        return explainRelations((Faction.FactionID)Enum.Parse(typeof(Faction.FactionID), faction));
    }
    protected void addFactionReputation(string faction, float amount)
    {
        Root.game.player.playerReputation.addReputation(Faction.factionToEnum(faction), amount);
    }
    protected void addCharacterStat(Character.Stat stat, double amount)
    {
        Debug.Log("TODO addCharacterStat()"); //todo
    }
    protected void setCharacterStat(Character.Stat stat, double amount)
    {
        Debug.Log("TODO setCharacterStat()"); //todo
    }

    protected int statRoll(string s)
    {
        Debug.Log("TODO setCharacterStat()"); //todo
        return 0;
    }
    protected void restartEvent()
    {
        outcome = 0;
        choice = 0;
    }
    protected void end()
    {
        running = false;
    }
}

static public class EventDataBase
{
    static public Dictionary<string, EventBase> events = new Dictionary<string, EventBase>();

    static public string toDebugString(EventBase e = null)
    {
        string rv = "";

        if (e == null)
        {
            rv = "Debug all events: \n";
            foreach (EventBase listedEvent in events.Values)
            {
                rv += e.name+"\n";
            }
        }
        else
        {
            rv = "Debug event: "+e.name+"\n";
            rv += "probability: "+e.calculateProbability() +"\n";
        }
        return rv;
    }
}
