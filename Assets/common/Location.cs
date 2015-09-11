﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;


public class Location
{
    Data.Location data { get; set; } // features are loaded to Data.Location

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

        this.ideology = new Simulation.LocationIdeology(this);
        this.economy = new Simulation.LocationEconomy(this, new Simulation.LocationEconomyAI());
    }
    
    public void tick(float days)
    {
        economy.tick(days);
    }


    public string toDebugString()
    {
        return "Name: " + name + " (pop: "+features.population+")\n" +
            "Features: " + features.toDebugString() + "\n" +
            "Economy:\n" + economy.toDebugString() + "\n";
            //"---\n" + "Politics:\n" + ideology.toDebugString();
            //"---\n" + "Assets:\n" + .toDebugString();
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

    public List<Data.TradeItem> getLocationTradeList()
    {
        List<Data.TradeItem> tradeList = new List<Data.TradeItem>();
        int tier = 1;
        int i = 0;
        int nthTier;
        float poolAmount;
        //foreach (Data.Resource.Type resource in Enum.GetValues(typeof(Data.Resource.Type)))

        foreach (Data.TradeItem item in economy.tradeItems)
        {
            tier = Mathf.Max (economy.resources[item.type].level, 1);
            poolAmount = (item.isExported && item.amount >= tier) ? Mathf.Floor (item.amount / tier) : 0.0f;
            nthTier = 0;

            foreach (Data.Resource.SubType subType in Data.Resource.getSubTypes(item.type))
            {
                tradeList.Add (new Data.TradeItem());
                // only assign commodities up to location tier level
                tradeList[i].amount = (nthTier < tier) ?  poolAmount : 0.0f;
                tradeList[i].type = item.type;
                tradeList[i].subType = subType;
                tradeList[i].weight = item.weight;
                tradeList[i].isExported = item.isExported;
                nthTier++;
                i++;
            }
        }
        return tradeList;
    }

    public List<Data.TradeItem> getPlayerTradeList()
    {
        List<Data.TradeItem> tradeList = new List<Data.TradeItem>();
        //tradeList.commodities = Root.game.player.cargo.commodities;
        int i = 0;
        foreach (var item in Root.game.player.cargo.commodities)
        {
            tradeList.Add (new Data.TradeItem());
            tradeList[i].amount = item.Value;
            tradeList[i].subType = item.Key;
            i++;
        }
        return tradeList;
    }

}
