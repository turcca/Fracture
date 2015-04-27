using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class Stockpile
{
    //public Stockpile()
    //{
    //    foreach (string name in Economy.getCommodityNames())
    //    {
    //        // use random until real values are inserted to exel
    //        commodities.Add(name, UnityEngine.Random.Range(0,99));
    //        lacking.Add(name, UnityEngine.Random.Range(0, 99));
    //        tradable.Add(name, UnityEngine.Random.Range(0, 99));
    //    }
    //}

    //public Dictionary<string, int> commodities = new Dictionary<string, int>();
    //public Dictionary<string, int> lacking = new Dictionary<string, int>();
    //public Dictionary<string, int> tradable = new Dictionary<string, int>();

    //public List<string> getImportList()
    //{
    //    var rv = from pair in lacking
    //             orderby pair.Value descending
    //             select pair.Key;
    //    return rv.ToList<string>();
    //}

    //public List<string> getExportList()
    //{
    //    var rv = from pair in tradable
    //             orderby pair.Value descending
    //             select pair.Key;
    //    return rv.ToList<string>();
    //}
}

public class Location
{
    private Data.Location data;

    public string name { get; private set; }
    public string description { get; private set; }
    public string id { get; private set; }
    public Vector3 position { get; private set; }

    public float population; 

    //public int numShips = 1;

    //public IdeologyData ideology;
    //public FactionData faction;

    //public Stockpile stockpile = new Stockpile();

    //LocationIndustry industry;
    //Inventory Stockpile;

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
        this.name = id;
        this.description = Tools.STRING_NOT_ASSIGNED;
        this.position = position;
        this.data = data;

        this.economy = new Simulation.LocationEconomy(this, new Simulation.LocationEconomyAI());
        this.ideology = new Simulation.LocationIdeology(this);
    }

    public void tick(float days)
    {
        economy.tick(days);
        // todo: foreach ship 
        //NewEconomy.LocationTrade.getTradePartnerForShip(ship);
    }

    //void initIdeologies()
    //{
    //    Dictionary<string, float> populationIdeology = new Dictionary<string, float>();
    //    Dictionary<string, float> factionIdeology = new Dictionary<string, float>();

    //    foreach (string i in IdeologyData.getIdeologyNames())
    //    {
    //        populationIdeology[i] = 0.0f;
    //        factionIdeology[i] = 0.0f;
    //    }

    //    // derived from location base stats
    //    populationIdeology["imperialist"] += (1 - info.frontier) * 18;
    //    populationIdeology["navigators"] += (1 - info.frontier) * 1;
    //    populationIdeology["brotherhood"] += (1 - info.frontier) * 1;
    //    populationIdeology["nationalist"] += info.frontier * 20;

    //    populationIdeology["imperialist"] += (1 - info.liberalValues) * 20;
    //    populationIdeology["liberal"] += info.liberalValues * 20;

    //    populationIdeology["imperialist"] += (1 - info.independent) * 18;
    //    populationIdeology["bureaucracy"] += (1 - info.independent) * 2;
    //    populationIdeology["nationalist"] += info.independent * 20;

    //    populationIdeology["liberal"] += (1 - info.religious) * 8;
    //    if (info.techLevel > 0.3f)
    //    {
    //        populationIdeology["technocrat"] += (1 - info.religious) * 12;
    //    }
    //    else
    //    {
    //        populationIdeology["transhumanist"] += (1 - info.religious) * 12;
    //    }
    //    populationIdeology["cult"] += info.religious * 20;

    //    populationIdeology["technocrat"] += (1 - info.psychic) * 20;
    //    populationIdeology["transhumanist"] += info.psychic * 20 * (1 - info.psyStability);
    //    populationIdeology["brotherhood"] += info.psychic * 16 * info.psyStability;
    //    populationIdeology["navigators"] += info.psychic * 4 * info.psyStability;


    //    // derived from nobles and guilds
    //    factionIdeology["cult"] += faction.control["noble3"] * 20;
    //    factionIdeology["mercantile"] += faction.control["noble2"] * 40;
    //    factionIdeology["liberal"] += faction.control["noble4"] * 30;
    //    factionIdeology["nationalist"] += faction.control["noble1"] * 40;

    //    factionIdeology["aristocrat"] += faction.control["noble1"] * 60;
    //    factionIdeology["aristocrat"] += faction.control["noble2"] * 60;
    //    factionIdeology["aristocrat"] += faction.control["noble3"] * 20;
    //    factionIdeology["aristocrat"] += faction.control["noble4"] * 50;

    //    factionIdeology["imperialist"] += faction.control["noble3"] * 60;
    //    factionIdeology["navigators"] += faction.control["noble4"] * 10;
    //    factionIdeology["brotherhood"] += faction.control["noble4"] * 10;

    //    factionIdeology["technocrat"] += faction.control["guild2"] * 40;
    //    factionIdeology["mercantile"] += faction.control["guild1"] * 50;
    //    factionIdeology["mercantile"] += faction.control["guild2"] * 20;
    //    factionIdeology["mercantile"] += faction.control["guild3"] * 60;
    //    factionIdeology["bureaucracy"] += faction.control["guild2"] * 30;
    //    factionIdeology["bureaucracy"] += faction.control["guild3"] * 30;
    //    factionIdeology["aristocrat"] += faction.control["guild2"] * 10;
    //    factionIdeology["navigators"] += faction.control["guild3"] * 10;
    //    factionIdeology["transhumanist"] += faction.control["guild1"] * 50;

    //    factionIdeology["cult"] += faction.control["church"] * 100;
    //    factionIdeology["transhumanist"] += faction.control["heretic"] * 100;


    //    // calculate support for ideologies in location
    //    // and precalculate effects from ideologies
    //    foreach (string i in IdeologyData.getIdeologyNames())
    //    {
    //        if (faction.getTotalControl() > 0)
    //        {
    //            ideology.support[i] += factionIdeology[i];
    //        }
    //        if (faction.getTotalControl() < 1)
    //        {
    //            ideology.support[i] += populationIdeology[i] * (1 - faction.getTotalControl());
    //        }
    //    }
    //    ideology.calculateEffects();
    //}

    //public void determineControl()
    //{

    //}

    public string toDebugString()
    {
        return "Name: " + name + "\n" +
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
}
