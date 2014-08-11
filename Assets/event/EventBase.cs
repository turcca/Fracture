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
    public string name = "";
    public bool available = false;
    public Player player = Game.getUniverse().player;
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

    public bool locationEvent = false;

    public EventBase(string _name)
    {
        name = _name;
        available = true;
        //initPre();

        Game.getUniverse().eventManager.addEventToPool(this);
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
        return "INSERT GENERAL TEXT HERE";
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
    protected void addFilter(string str)
    {
        filters.Add(str, 1.0f);
    }
    protected int getElapsedDays()
    {
        return Game.getUniverse().player.getElapsedDays();
    }
    protected double getWarpMagnitude()
    {
        return Game.getUniverse().player.getWarpMagnitude();
    }
    protected EventBase getEvent(string name)
    {
        return EventDataBase.events[name];
    }
    protected string getPlayerLocationID()
    {
        return Game.getUniverse().player.getLocationId();
    }
    protected Location getLocation()
    {
        return Game.getUniverse().locations[location];
    }
    protected void factionChange(string f, int a)
    {
        //todo
    }
    protected void filterWeight(string w, int a)
    {
        //todo
    }

    protected Character getCharacter()
    {
        return character;
    }
    protected Character getCharacter(Character.Job job)
    {
        return Game.getUniverse().player.getCharacter(job);
    }
    protected Character getBestCharacter(Character.Stat s)
    {
        return Character.getBest(Game.getUniverse().player.getCharacters(), s);
    }
    protected double getCharacterStat(Character.Stat s)
    {
        return character.getStat(s);
    }
    protected double getCharacterStat(Character.Job job, Character.Stat s)
    {
        return Game.getUniverse().player.getCharacter(job).getStat(s);
    }
    protected Character getAdvisor()
    {
        return advisor;
    }
    protected void addCharacterStat(Character.Stat stat, double amount)
    {
        //todo
    }
    protected void setCharacterStat(Character.Stat stat, double amount)
    {
        //todo
    }

    protected int statRoll(string s)
    {
        return 0;
    }
    protected void end()
    {
        running = false;
    }
}

static public class EventDataBase
{
    static public Dictionary<string, EventBase> events = new Dictionary<string, EventBase>();
}
