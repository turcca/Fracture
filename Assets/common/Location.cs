using UnityEngine;
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

    public string ruler { get; private set; }

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

        this.ruler = getRuler();
    }
    
    public void tick(float days)
    {
        economy.tick(days);
    }


    public string toDebugString()
    {
        return "Name: ["+id+"] "+ name + " (pop: "+features.population+")\n" +
            "Features: " + features.toDebugString() + "\n" +
            "Economy:\n" + economy.toDebugString() + "\n";
            //"---\n" + "Politics:\n" + ideology.toDebugString();
            //"---\n" + "Assets:\n" + .toDebugString();
    }

    public string getRuler()
    {
        return ideology.getRuler();
    }


    public float getImportance()
    {
        return Simulation.Parameters.getImportance(this);
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
    public Simulation.NPCShip getFreeShip()
    {
        foreach (Simulation.NPCShip ship in Root.game.ships)
        {
            if (ship.home == this && ship.free)
            {
                return ship;
            }
        }
        return null;
    }

    public List<Data.TradeItem> getLocationTradeList() // CHECK CHECK CHECK CHECK CHECK CHECK 
    {
        List<Data.TradeItem> tradeList = new List<Data.TradeItem>();
        int tier = 1;
        int i = 0;
        int nthTier; // list item index
        float poolAmount;

        // go through resource types
        foreach (Data.TradeItem item in economy.tradeItems)
        {
            tier = Mathf.Max (economy.resources[item.type].level, 1); // does it try to sell lvl0 resources?
            poolAmount = (item.isExported && item.amount >= tier) ? Mathf.Floor (item.amount / tier) : 0.0f; // ? pool system scales between resources & player invetory correctly?
            nthTier = 0;

            // go through type's subtypes
            foreach (Data.Resource.SubType subType in Data.Resource.getSubTypes(item.type))
            {
                tradeList.Add (new Data.TradeItem());
                // only assign commodities for sale up to location tier level
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



    // ---- static functions

    static public float getLocationDistance (Location location1, Location location2)
    {
        return Vector3.Distance(location1.position, location2.position);
    }
}
