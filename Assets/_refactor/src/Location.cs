using System;
using System.Collections.Generic;
using System.Globalization;

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

public class Location
{
    public string Name = Tools.STRING_NOT_ASSIGNED;
    public string Description = Tools.STRING_NOT_ASSIGNED;
    // etc

    public IdeologyData ideology;
    public GroupData group;
    public LocationData info;

    LocationIndustry industry;
    //Inventory Stockpile;

    public Location(string data)
    {
        int i = 0;
        string groupData = "";
        string infoData = "";
        foreach (string value in data.Split(','))
        {
            if (i==1)
            {
                Name = value;
            }
            else if (i==2)
            {
                Description = value;
            }
            else if (i>=3 && i<=11)
            {
                groupData += value + ",";
            }
            else if (i >= 12)
            {
                infoData += value + ",";
            }
            ++i;
        }

        info = new LocationData(infoData);
        group = new GroupData(groupData);
        ideology = new IdeologyData();
        industry = new LocationIndustry(this);

        initIdeologies();
    }

    public void tick(float days)
    {
        industry.tick(days);
    }

    void initIdeologies()
    {
        Dictionary<string, float> populationIdeology = new Dictionary<string,float>();
        Dictionary<string, float> groupIdeology = new Dictionary<string, float>();

        foreach (string i in IdeologyData.getIdeologyNames())
        {
            populationIdeology[i] = 0.0f;
            groupIdeology[i] = 0.0f;
        }

        // derived from location base stats
        populationIdeology["imperialist"] += (1 - info.frontier) * 18;
        populationIdeology["navigators"] += (1 - info.frontier) * 1;
        populationIdeology["brotherhood"] += (1 - info.frontier) * 1;
        populationIdeology["nationalist"] += info.frontier * 20;

        populationIdeology["imperialist"] += (1 - info.liberalValues) * 20;
        populationIdeology["liberal"] += info.liberalValues * 20;

        populationIdeology["imperialist"] += (1 - info.independent) * 18;
        populationIdeology["bureaucracy"] += (1 - info.independent) * 2;
        populationIdeology["nationalist"] += info.independent * 20;

        populationIdeology["liberal"] += (1 - info.religious) * 8;
        if (info.techLevel > 0.3f)
        {
            populationIdeology["technocrat"] += (1 - info.religious) * 12;
        }
        else
        {
            populationIdeology["transhumanist"] += (1 - info.religious) * 12;
        }
        populationIdeology["cult"] += info.religious * 20;

        populationIdeology["technocrat"] += (1 - info.psychic) * 20;
        populationIdeology["transhumanist"] += info.psychic * 20 * (1-info.psyStability);
        populationIdeology["brotherhood"] += info.psychic * 16 * info.psyStability;
        populationIdeology["navigators"] += info.psychic * 4 * info.psyStability;


        // derived from nobles and guilds
        groupIdeology["cult"] += group.control["noble3"] * 20;
        groupIdeology["mercantile"] += group.control["noble2"] * 40;
        groupIdeology["liberal"] += group.control["noble4"] * 30;
        groupIdeology["nationalist"] += group.control["noble1"] * 40;

        groupIdeology["aristocrat"] += group.control["noble1"] * 60;
        groupIdeology["aristocrat"] += group.control["noble2"] * 60;
        groupIdeology["aristocrat"] += group.control["noble3"] * 20;
        groupIdeology["aristocrat"] += group.control["noble4"] * 50;

        groupIdeology["imperialist"] += group.control["noble3"] * 60;
        groupIdeology["navigators"] += group.control["noble4"] * 10;
        groupIdeology["brotherhood"] += group.control["noble4"] * 10;

        groupIdeology["technocrat"] += group.control["guild2"] * 40;
        groupIdeology["mercantile"] += group.control["guild1"] * 50;
        groupIdeology["mercantile"] += group.control["guild2"] * 20;
        groupIdeology["mercantile"] += group.control["guild3"] * 60;
        groupIdeology["bureaucracy"] += group.control["guild2"] * 30;
        groupIdeology["bureaucracy"] += group.control["guild3"] * 30;
        groupIdeology["aristocrat"] += group.control["guild2"] * 10;
        groupIdeology["navigators"] += group.control["guild3"] * 10;
        groupIdeology["transhumanist"] += group.control["guild1"] * 50;

        groupIdeology["cult"] += group.control["church"] * 100;
        groupIdeology["transhumanist"] += group.control["heretic"] * 100;


        // calculate support for ideologies in location
        // and precalculate effects from ideologies
        foreach (string i in IdeologyData.getIdeologyNames())
        {
            if (group.getTotalControl() > 0)
            {
                ideology.support[i] += groupIdeology[i];
            }
            if (group.getTotalControl() < 1)
            {
                ideology.support[i] += populationIdeology[i] * (1 - group.getTotalControl());
            }
        }
        ideology.calculateEffects();
    }

    public void determineControl()
    {

    }

    public string toDebugString()
    {
        return "Name: " + Name + "\n" +
            "---\n" + "Politics:\n" + ideology.toDebugString() +
            "---\n" + "Economy:\n" + industry.toDebugString();
    }

    public float getImportance()
    {
        float populationFactor = (float)Math.Pow(info.population, 0.17);
        float orbitalFactor = info.orbitalInfra * 3;
        float infraFactor = info.infrastructure * (ideology.effects.pgrowth/2 + ideology.effects.industry/2 +1) +0.5f;
        float economyFactor = ideology.effects.economy + 1;
        float militaryFactor = ideology.effects.military + 1;
        float techFactor = (ideology.effects.innovation + 1 + info.techLevel + 0.5f) / 2;

        return populationFactor * (economyFactor + techFactor + infraFactor + militaryFactor) / 4;
    }

}
