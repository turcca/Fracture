using System.Collections;
using System.Globalization;

public static class DataParser
{
    public static Data.LocationFeatures parseLocationFeatures(string locationData)
    {
        Data.LocationFeatures data = new Data.LocationFeatures();
        int i = 0;
        foreach (string value in locationData.Split('\t'))
        {
            switch(i)
            {
                case 1: data.name = value;
                    break;
                case 2: data.subsector = value;
                    break;
                case 3: data.description1 = value;
                    break;
                case 4: data.description2 = value;
                    break;
                case 5: data.visibility = (Data.Location.Visibility) System.Enum.Parse(typeof(Data.Location.Visibility), value);
                    break;

                    // faction control
                case 6: data.factionCtrl[Faction.FactionID.noble1] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 7: data.factionCtrl[Faction.FactionID.noble2] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 8: data.factionCtrl[Faction.FactionID.noble3] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 9: data.factionCtrl[Faction.FactionID.noble4] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 10: data.factionCtrl[Faction.FactionID.guild1] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 11: data.factionCtrl[Faction.FactionID.guild2] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 12: data.factionCtrl[Faction.FactionID.guild3] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 13: data.factionCtrl[Faction.FactionID.church] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 14: data.factionCtrl[Faction.FactionID.heretic] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                case 15: data.hq = value == "" ? (Faction.FactionID?) null : (Faction.FactionID) System.Enum.Parse(typeof(Faction.FactionID), value);
                    break;

                    // base ideologies
                case 16: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.cult] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 17: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.technocrat] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 18: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.mercantile] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 19: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.bureaucracy] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 20: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.liberal] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 21: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.nationalist] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 22: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.aristocrat] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 23: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.imperialist] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 24: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.navigators] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 25: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.brotherhood] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 26: data.baseIdeology[Simulation.LocationIdeology.IdeologyID.transhumanist] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                    // resource multipliers
                case 27: data.resourceMultiplier[Data.Resource.Type.Food] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 28: data.resourceMultiplier[Data.Resource.Type.Mineral] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 29: data.resourceMultiplier[Data.Resource.Type.BlackMarket] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 30: data.resourceMultiplier[Data.Resource.Type.Innovation] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 31: data.resourceMultiplier[Data.Resource.Type.Culture] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 32: data.resourceMultiplier[Data.Resource.Type.Industry] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 33: data.resourceMultiplier[Data.Resource.Type.Economy] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 34: data.resourceMultiplier[Data.Resource.Type.Military] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                    // tech levels
                case 35: data.techLevel = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 36: data.infrastructure = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 37: data.militaryTechLevel = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                case 38: data.population =  float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                    // assets
                case 39: data.assetStation = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                default:
                    //Tools.debug("Read error, cell not in data range: " + i.ToString());
                    break;
            }
            ++i;
        }

        return data;
    }
}
