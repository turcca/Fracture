using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CommodityInventory
{
    public Dictionary<string, int> commodities = new Dictionary<string, int>();
    public int maxCargoSpace = 25;
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
    private List<Character> characters = new List<Character>();
    private Dictionary<Character.Job, Character> advisors = new Dictionary<Character.Job, Character>();

    public Vector3 position = new Vector3(0, 0, 0);
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
            characters.Add(c);
            advisors.Add(job, c);
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
        return advisors[job];
    }

    public Character getCharacter(int id)
    {
        return characters[id];
    }

    public Character[] getCharacters()
    {
        return characters.ToArray();
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
}
