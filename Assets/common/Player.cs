using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CommodityInventory
{
    internal Dictionary<Data.Resource.SubType, int> commodities = new Dictionary<Data.Resource.SubType, int>();
    public int maxCargoSpace { get; private set; }
    public float credits = 128.0f;

    public CommodityInventory()
    {
        foreach (Data.Resource.SubType type in Enum.GetValues(typeof(Data.Resource.SubType)))
        {
            commodities.Add(type, 0);
        }
        maxCargoSpace = 10; // todo: load from ship data
    }
    public void addCargo(Data.Resource.SubType type, int amount = 1)
    {
        commodities[type] += amount;
    }

    public int cargoAmount(Data.Resource.SubType type)
    {
        return commodities[type];
    }

    internal int getUsedCargoSpace()
    {
        int rv = 0;
        foreach (int value in commodities.Values)
        {
            rv += value;
        }
        return rv;
    }
    public bool hasFreeCargoSpace()
    {
        return (getUsedCargoSpace() < maxCargoSpace) ? true : false;
    }
}

public class Player
{
    public CommodityInventory cargo = new CommodityInventory();
    private Dictionary<int, Character> characters = new Dictionary<int, Character>();
    private Dictionary<Character.Job, int> advisors = new Dictionary<Character.Job, int>();

    public Vector3 position;
    private double warpMagnitude = 0.0d;
    private string locationId = "";

    private float elapsedDays = 0;

    public Player()
    {
    }

    public void init()
    {
        // starting characters initialized (todo: move to game init with game setup menus)
        createCharacter(Character.Job.captain, "starting captain");
        createCharacter(Character.Job.navigator, "starting navigator");
        createCharacter(Character.Job.engineer/*, "engineer"*/);
        createCharacter(Character.Job.security/*, "security"*/);
        createCharacter(Character.Job.quartermaster/*, "quartermaster"*/);
        createCharacter(Character.Job.psycher, "brotherhood");

    }
    public void createCharacter(Character.Job? assignment = null, string templateName = null) // job parameter will try to assign character to the job
    {
        Character c = CharacterTemplates.getCharacter(templateName);

        if (assignment == null)
        {
            characters.Add(c.id, c);
        }
        else
        {
            Character.Job job = (Character.Job)assignment;
            characters.Add(c.id, c);
            if (!advisors.ContainsKey(job)) advisors.Add(job, c.id);
            c.assignment = job;
        }
    }

    public int getElapsedDays()
    {
        return (int)elapsedDays;
    }

    public float getWarpMagnitude()
    {
        return (float)warpMagnitude*6.0f;
    }
    public void setWarpMagnitude(double mag)
    {
        warpMagnitude = mag;
    }
    public double getRawWarpMagnitude()
    {
        return warpMagnitude;
    }

    public Character getAdvisor(Character.Job job)
    {
        if (advisors.ContainsKey(job) && characters.ContainsKey(advisors[job]))
        {
            return characters[advisors[job]];
        }
        else
        {
            return Character.Empty;
        }
    }

    public Character getCharacter(int id)
    {
        if (characters.ContainsKey(id))
        {
            return characters[id];
        }
        else
        {
            return Character.Empty;
        }
    }

    public Character[] getCharacters()
    {
        Character[] copy = new Character[characters.Count];
        characters.Values.CopyTo(copy, 0);
        return copy;
    }

    public string getLocationId()
    {
        return locationId;
    }

    public void setLocationId(string loc)
    {
        locationId = loc;
    }
    public Location getLocation()
    {
        if (Root.game.locations.ContainsKey(locationId))
        {
            return Root.game.locations[locationId];
        }
        else
        {
            Debug.LogWarning ("WARNING: player.locationId not valid: "+locationId);
            return null;
        }
    }
    public Location getClosestHabitat()
    {
        return Root.game.getClosestHabitat(new Vector2(position.x, position.z));
    }

    public void tick(float days)
    {
        elapsedDays += days;
        //advisor exp - character tick?
    }

    public void setAdvisor(Character.Job job, int id)
    {
        foreach (var pair in advisors)
        {
            if (pair.Value == id)
            {
                advisors.Remove(pair.Key);
                break;
            }
        }

        advisors[job] = getCharacter(id).id;
    }
}
