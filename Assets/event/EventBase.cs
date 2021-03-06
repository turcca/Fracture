﻿using UnityEngine;
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

    public bool locationEvent = false;


    public EventBase(string _name)
    {
        name = _name;
        available = true;
        //initPre();
        initFilters();
        //Debug.Log ("formatting EventBase: "+name+" / "+_name);
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
    protected int getElapsedDays()
    {
        return Root.game.player.getElapsedDays();
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
        return Root.game.locations[location];
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
    /*
    protected Character getCharacter(Character.Job job)
    {
        return Root.game.player.getAdvisor(job);
    }*/
    protected Character getBestCharacter(Character.Stat s)
    {
        return Character.getBest(Root.game.player.getCharacters(), s);
    }
    protected double getCharacterStat(Character.Stat s)
    {
        return character.getStat(s);
    }
    protected double getCharacterStat(Character.Job job, Character.Stat s)
    {
        return Root.game.player.getAdvisor(job).getStat(s);
    }
    protected double getShipStat(string s)
    {
        //todo
        return 0.0;
    }
    protected Character getAdvisor(Character.Job job)
    {
        return Root.game.player.getAdvisor(job);
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
