using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventBase
{
    public string name = "";
    public bool available = false;
    public Player player = Universe.singleton.player;
    public Dictionary<string, float> filters = new Dictionary<string, float>();
    public Dictionary<string, int> choices = new Dictionary<string, int>();

    public Character character;
    public Character advisor;
    public string location;
    public int outcome = -1;
    public int choice = -1;

    public EventBase(string _name)
    {
        name = _name;
        available = true;

        initChoices();
    }

    public void addFilter(string str)
    {
        filters.Add(str, 1.0f);
    }
    public virtual void initChoices()
    {
    }

    public virtual float calculateProbability()
    {
        return 1;
    }
    public virtual string getAdvice(string who)
    {
        return "INSERT GENERAL ADVICE HERE";
    }
    public virtual string getText()
    {
        return "INSERT GENERAL TEXT HERE";
    }
    public virtual void doOutcome()
    {
    }

    protected int getElapsedDays()
    {
        return Universe.singleton.player.getElapsedDays();
    }
    protected Character getCharacter()
    {
        return character;
    }
    protected Character getCharacter(string position)
    {
        return Universe.singleton.player.getCharacter(position);
    }
    protected Character getAdvisor()
    {
        return advisor;
    }
    protected double getWarpMagnitude()
    {
        return Universe.singleton.player.getWarpMagnitude();
    }
    protected EventBase getEvent(string name)
    {
        return EventDataBase.events[name];
    }
    protected string getLocationID()
    {
        return Universe.singleton.player.getLocationID();
    }
    protected Location getLocation()
    {
        return Universe.singleton.locations[getLocationID()];
    }
    protected void factionChange(string f, int a)
    {
        //todo
    }
    protected void filterWeight(string w, int a)
    {
        //todo
    }
    protected int statRoll(string s)
    {
        return 0;
    }
    protected void end()
    {
        //todo
    }
}

static public class EventDataBase
{
    static public Dictionary<string, EventBase> events = new Dictionary<string, EventBase>();
}

public class Event_xx : EventBase
{
    public Event_xx(string name)
        : base(name)
    { }

    public override float calculateProbability()
    {
        return 0;
    }
}
