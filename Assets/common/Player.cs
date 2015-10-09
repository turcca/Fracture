using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CommodityInventory
{
    public Dictionary<Data.Resource.SubType, int> commodities = new Dictionary<Data.Resource.SubType, int>();
    public int maxCargoSpace = 10;
    public float credits = 128.0f;

    public CommodityInventory()
    {
        foreach (Data.Resource.SubType type in Enum.GetValues(typeof(Data.Resource.SubType)))
        {
            commodities.Add(type, 0);
        }
    }
    public int cargoAmount(Data.Resource.SubType type)
    {
        return commodities[type];
    }

    public int getUsedCargoSpace()
    {
        int rv = 0;
        foreach (int value in commodities.Values)
        {
            rv += value;
        }
        return rv;
    }
}

public class Player
{
    public CommodityInventory cargo = new CommodityInventory();
    private Dictionary<int, Character> characters = new Dictionary<int, Character>();
    private Dictionary<Character.Job, int> advisors = new Dictionary<Character.Job, int>();

    public Vector3 position;// = new Vector3(0, 0, 0);
    private double warpMagnitude = 0.0d;
    private string locationId = "";

    private float elapsedDays = 0;

    public Player()
    {
    }

    public void init()
    {
        Character.Job[] tempChars =
        {
            Character.Job.captain,
            Character.Job.navigator,
            Character.Job.engineer,
            Character.Job.security,
            Character.Job.quartermaster,
            Character.Job.psycher
        };

        foreach (Character.Job job in tempChars)
        {
            Character c = new Character();
            c.assignment = job;

            characters.Add(c.id, c);
            advisors.Add(job, c.id);
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

    public void tick(float days)
    {
        elapsedDays += days;
        //advisor exp
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
