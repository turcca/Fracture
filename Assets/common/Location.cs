using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


public class Location
{
    private Data.Location data;

    public string name
    {
        get { return data.features.name; }
    }
    public string description { get; private set; }
    public string id { get; private set; }
    public Vector3 position { get; private set; }

    public Simulation.LocationEconomy economy;
    public Simulation.LocationIdeology ideology;
    public Data.LocationFeatures features
    {
        get { return data.features; }
    }

    public Location(string id, Vector3 position)
        : this(id, position, new Data.Location())
    {
    }

    public Location(string id, Vector3 position, Data.Location data)
    {
        this.id = id;
        this.description = Tools.STRING_NOT_ASSIGNED;
        this.position = position;
        this.data = data;

        this.economy = new Simulation.LocationEconomy(this, new Simulation.LocationEconomyAI());
        this.ideology = new Simulation.LocationIdeology(this);
    }

    public void tick(float days)
    {
        economy.tick(days);
    }


    public string toDebugString()
    {
        return "Name: " + name + " (pop: "+features.population+")\n" +
            "Features: " + data.features.toDebugString() + "\n" +
            "Economy:\n" + economy.toDebugString() + "\n";
            //"---\n" + "Politics:\n" + ideology.toDebugString();
            //"---\n" + "Economy:\n" + industry.toDebugString();
    }

    public float getImportance()
    {
        //float populationFactor = (float)Math.Pow(info.population, 0.17);
        //float orbitalFactor = info.orbitalInfra * 3;
        //float infraFactor = info.infrastructure * (ideology.effects.pgrowth / 2 + ideology.effects.industry / 2 + 1) + 0.5f;
        //float economyFactor = ideology.effects.economy + 1;
        //float militaryFactor = ideology.effects.military + 1;
        //float techFactor = (ideology.effects.innovation + 1 + info.techLevel + 0.5f) / 2;

        //return populationFactor * (economyFactor + techFactor + infraFactor + militaryFactor) / 4;
        return 0;
    }

    public int getCommodityPrice(string commodity)
    {
        //return industry.getCommodityPrice(commodity);
        return 0;
    }


    public List<Simulation.NPCShip> getFreeShips()
    {
        List<Simulation.NPCShip> rv = new List<Simulation.NPCShip>();
        foreach (Simulation.NPCShip ship in Root.game.ships)
        {
            if (ship.home == this && ship.free)
            {
                rv.Add(ship);
            }
        }
        return rv;
    }

    public PlayerTradeList getPlayerTradeList()
    {
        PlayerTradeList tradeList = new PlayerTradeList();
        foreach (Data.Resource.Type resource in economy.resources.Keys)
        {
            // build list
            //economy.resources[resource].getResourcesOverTargetLimit();
        }
        return tradeList;
    }
}
