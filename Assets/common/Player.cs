using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CommodityInventory
{
    public Dictionary<string, int> commodities = new Dictionary<string, int>();
    public int maxCargoSpace = 10;
    public int credits = 1000;

    public CommodityInventory()
    {
        foreach (string key in Economy.getCommodityNames())
        {
            commodities.Add(key, 0);
        }
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

    public Vector3 position = new Vector3(0, 0, 0);
    public float warpMagnitude = 0.0f;
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

            characters.Add(c.id, c);
            advisors.Add(job, c.id);
        }
    }

    public int getElapsedDays()
    {
        return (int)elapsedDays;
    }

    public double getWarpMagnitude()
    {
        return 0.0;
    }

    public Character getCharacter(Character.Job job)
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

    public void tick(float days)
    {
        elapsedDays += days;
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
