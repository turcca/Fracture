using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private float elapsedDays = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getElapsedDays()
    {
        return (int)elapsedDays;
    }

    public double getWarpMagnitude()
    {
        return 0.0;
    }

    public Character getCharacter(string position)
    {
        return new Character();
    }
    public string getLocationID()
    {
        return "a";
    }

    public void tick(float days)
    {
        elapsedDays += days;
    }
}
