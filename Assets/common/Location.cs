using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class LocationData
{
    public float minerals = 0.2f;
    public float biomass = 0.2f;
    public float frontier = 0.2f;
    public float liberalValues = 0.2f;
    public float independent = 0.0f;
    public float religious = 0.2f;
    public float techLevel = 0.5f;
    public float psychic = 0.1f;
    public float psyStability = 0.9f;
    public float population = 10.0f;
    public float infrastructure = 1.0f;
    public float orbitalInfra = 0.2f;
    public float legalLevel = 1.0f;

    public LocationData(string data)
    {
        string[] dataChunk = data.Split(',');
        minerals = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        biomass = float.Parse(dataChunk[1], CultureInfo.InvariantCulture.NumberFormat);
        frontier = float.Parse(dataChunk[2], CultureInfo.InvariantCulture.NumberFormat);
        liberalValues = float.Parse(dataChunk[3], CultureInfo.InvariantCulture.NumberFormat);
        independent = float.Parse(dataChunk[4], CultureInfo.InvariantCulture.NumberFormat);
        religious = float.Parse(dataChunk[5], CultureInfo.InvariantCulture.NumberFormat);
        techLevel = float.Parse(dataChunk[6], CultureInfo.InvariantCulture.NumberFormat);
        psychic = float.Parse(dataChunk[7], CultureInfo.InvariantCulture.NumberFormat);
        psyStability = float.Parse(dataChunk[8], CultureInfo.InvariantCulture.NumberFormat);
        population = float.Parse(dataChunk[9], CultureInfo.InvariantCulture.NumberFormat);
        infrastructure = float.Parse(dataChunk[10], CultureInfo.InvariantCulture.NumberFormat);
        orbitalInfra = float.Parse(dataChunk[11], CultureInfo.InvariantCulture.NumberFormat);
        legalLevel = float.Parse(dataChunk[12], CultureInfo.InvariantCulture.NumberFormat);
    }
}


public class Stockpile
{
    public Stockpile()
    {
        foreach (string name in Economy.getCommodityNames())
        {
            // use random until real values are inserted to exel
            commodities.Add(name, UnityEngine.Random.Range(0,99));
            lacking.Add(name, UnityEngine.Random.Range(0, 99));
            tradable.Add(name, UnityEngine.Random.Range(0, 99));
        }
    }

    public Dictionary<string, int> commodities = new Dictionary<string, int>();
    public Dictionary<string, int> lacking = new Dictionary<string, int>();
    public Dictionary<string, int> tradable = new Dictionary<string, int>();

    public List<string> getImportList()
    {
        var rv = from pair in lacking
                 orderby pair.Value descending
                 select pair.Key;
        return rv.ToList<string>();
    }

    public List<string> getExportList()
    {
        var rv = from pair in tradable
                 orderby pair.Value descending
                 select pair.Key;
        return rv.ToList<string>();
    }
}

public class Location
{
    public string Name = Tools.STRING_NOT_ASSIGNED;
    public string Description = Tools.STRING_NOT_ASSIGNED;
    public string id = "";
    public Vector3 position = new Vector3(0, 0, 0);
    //public int numShips = 1;

    //public IdeologyData ideology;
    //public FactionData faction;
    //public LocationData info;

    //public Stockpile stockpile = new Stockpile();

    //LocationIndustry industry;
    //Inventory Stockpile;

    NewEconomy.LocationEconomy economy;

    public Location(string id, Vector3 position, NewEconomy.LocationEconomy economy)
    {
        this.id = id;
        this.position = position;
        this.economy = economy;

        //int i = 0;
        //string factionData = "";
        //string infoData = "";
        //foreach (string value in data.Split(','))
        //{
        //    if (i == 1)
        //    {
        //        Name = value;
        //    }
        //    else if (i == 2)
        //    {
        //        Description = value;
        //    }
        //    else if (i >= 3 && i <= 11)
        //    {
        //        factionData += value + ",";
        //    }
        //    else if (i >= 12)
        //    {
        //        infoData += value + ",";
        //    }
        //    ++i;
        //}

        //info = new LocationData(infoData);
        //faction = new FactionData(factionData);
        //ideology = new IdeologyData();
        //industry = new LocationIndustry(this);
        //initIdeologies();
    }

    public void tick(float days)
    {
        economy.tick(days);
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
        return "Name: " + Name + "\n" +
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


    public void initShips()
    {
        //for (int i = 0; i < numShips; ++i )
        //{
        //    Game.universe.ships.Add(new NPCShip(this));
        //}
    }
}
