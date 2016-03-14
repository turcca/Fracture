using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

public static class DataParser
{
    public static Data.LocationFeatures parseLocationFeatures(string locationData)
    {
        Data.LocationFeatures data = new Data.LocationFeatures();
        int i = 0;
        foreach (string value in locationData.Split('\t'))
        {
            switch (i)
            {
                case 1:
                    data.name = value;
                    break;
                case 2:
                    data.subsector = value;
                    break;
                case 3:
                    data.description1 = value;
                    break;
                case 4:
                    data.description2 = value;
                    break;
                case 5:
                    data.visibility = (Data.Location.Visibility)System.Enum.Parse(typeof(Data.Location.Visibility), value);
                    break;

                // faction control
                case 6:
                    data.factionCtrl[Faction.FactionID.noble1] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 7:
                    data.factionCtrl[Faction.FactionID.noble2] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 8:
                    data.factionCtrl[Faction.FactionID.noble3] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 9:
                    data.factionCtrl[Faction.FactionID.noble4] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 10:
                    data.factionCtrl[Faction.FactionID.guild1] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 11:
                    data.factionCtrl[Faction.FactionID.guild2] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 12:
                    data.factionCtrl[Faction.FactionID.guild3] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 13:
                    data.factionCtrl[Faction.FactionID.church] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 14:
                    data.factionCtrl[Faction.FactionID.heretic] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                case 15:
                    data.hq = value == "" ? (Faction.FactionID?)null : (Faction.FactionID)System.Enum.Parse(typeof(Faction.FactionID), value);
                    break;

                // base ideologies
                case 16:
                    data.baseIdeology[Faction.IdeologyID.cult] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 17:
                    data.baseIdeology[Faction.IdeologyID.technocrat] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 18:
                    data.baseIdeology[Faction.IdeologyID.mercantile] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 19:
                    data.baseIdeology[Faction.IdeologyID.bureaucracy] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 20:
                    data.baseIdeology[Faction.IdeologyID.liberal] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 21:
                    data.baseIdeology[Faction.IdeologyID.nationalist] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 22:
                    data.baseIdeology[Faction.IdeologyID.aristocrat] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 23:
                    data.baseIdeology[Faction.IdeologyID.imperialist] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 24:
                    data.baseIdeology[Faction.IdeologyID.navigators] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 25:
                    data.baseIdeology[Faction.IdeologyID.brotherhood] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 26:
                    data.baseIdeology[Faction.IdeologyID.transhumanist] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                // resource multipliers
                case 27:
                    data.resourceMultiplier[Data.Resource.Type.Food] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 28:
                    data.resourceMultiplier[Data.Resource.Type.Mineral] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 29:
                    data.resourceMultiplier[Data.Resource.Type.BlackMarket] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 30:
                    data.resourceMultiplier[Data.Resource.Type.Innovation] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 31:
                    data.resourceMultiplier[Data.Resource.Type.Culture] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 32:
                    data.resourceMultiplier[Data.Resource.Type.Industry] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 33:
                    data.resourceMultiplier[Data.Resource.Type.Economy] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 34:
                    data.resourceMultiplier[Data.Resource.Type.Military] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                // tech levels
                case 35:
                    data.startingTechLevel = parseInt(value, i);
                    break;
                case 36:
                    data.startingInfrastructure = parseInt(value, i);
                    break;
                case 37:
                    data.startingMilitaryTechLevel = parseInt(value, i);
                    break;

                case 38:
                    data.population = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                // assets
                case 39:
                    data.assetStation = parseInt(value, i);
                    break;

                default:
                    //Tools.debug("Read error, cell not in data range: " + i.ToString());
                    break;
            }
            ++i;
        }

        return data;
    }




    public static ShipStats parseShipStats(string shipStatData)
    {
        ShipStats stats = new ShipStats();
        int i = 0;
        foreach (string value in shipStatData.Split('\t'))
        {
            switch (i)
            {
                case 1:
                    stats.shipId = value;
                    break;
                case 2:
                    stats.type = value;
                    break;
                case 3:
                    stats.size = parseInt(value, i);
                    break;
                case 4:
                    stats.modules = parseInt(value, i);
                    break;
                case 5:
                    stats.structureRatio = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 6:
                    stats.engines = parseInt(value, i);
                    break;
                case 7:
                    stats.hpFront = parseInt(value, i);
                    break;
                case 8:
                    stats.hpDorsal = parseInt(value, i);
                    break;
                case 9:
                    stats.hpSideL = parseInt(value, i);
                    break;
                case 10:
                    stats.hpSideR = parseInt(value, i);
                    break;
                case 11:
                    stats.hpE1 = parseInt(value, i);
                    break;
                case 12:
                    stats.hpE2 = parseInt(value, i);
                    break;
                case 13:
                    stats.hpPd = parseInt(value, i);
                    break;
                case 14:
                    stats.width = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 15:
                    stats.length = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 16:
                    stats.height = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 17:
                    stats.command = parseInt(value, i);
                    break;
                case 18:
                    stats.cargo = parseInt(value, i);
                    break;
                case 19:
                    stats.utility = parseInt(value, i);
                    break;

                case 20:
                    stats.reqTechType = (Data.Tech.Type)System.Enum.Parse(typeof(Data.Tech.Type), value);
                    break;
                case 21:
                    stats.reqTechLevel = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                case 22:
                    stats.reqRelations = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 23:
                    stats.empty = value;
                    break;

                case 24:
                    stats.toolTip = value;
                    break;

                default:
                    //Tools.debug("Read error, cell not in data range: " + i.ToString());
                    break;

            }
            i++;
        }
        return stats;
    }



    static int parseInt(string value, int i)
    {
        int a;
        try
        {
            a = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        catch (System.Exception)
        {
            UnityEngine.Debug.Log("parseInt() failed at ships.tsv column '"+i+"'  \n input value was: '" + value + "'");
            throw;
        }
        return a;
    }

    /// <summary>
    /// Changes all 'he' to 'she' or vice versa
    /// use with care!
    /// DOES NOT RECOGNIZE "..is his" -> "..is hers"!
    /// </summary>
    /// <param name="parseString"></param>
    /// <param name="toFemale"></param>
    /// <returns></returns>
    public static string changeGenderContent(string parseString, bool isMale, bool makeMale)
    {
        if (isMale == makeMale) return parseString;

        //string[] he = { "He", "he", "His", "his", "Him", "him", "Himself", "himself" };
        //string[] she = { "She", "she", "Hers", "hers", "Her", "her", "Herself", "herself" };
        string[] he = { "He", "he", "His", "his", "Him", "him", "Himself", "himself" };
        string[] she = { "She", "she", "Her", "her", "Her", "her", "Herself", "herself" };
        //string[] he = { "he", "his", "him", "himself" };
        //string[] she = { "she ", "hers", "her", "herself" };

        if (makeMale == false)
        {
            for (int i = 0; i < he.Length; i++)
            {
                parseString = ReplaceFullWords(parseString, he[i], she[i]);
            }
        }
        else
        {
            for (int i = 0; i < she.Length; i++)
            {
                parseString = ReplaceFullWords(parseString, she[i], he[i]);
            }
        }

        return parseString;
    }
    static string ReplaceFullWords(string input, string from, string to)
    {
        if (input == null) { return null; }
        return Regex.Replace(input, "\\b" + Regex.Escape(from) + "\\b", to);
    }
    static string Replace(string input, string from, string to)
    {
        if (input == null) { return null; }
        return Regex.Replace(input, Regex.Escape(from), to);
    }
}

/*
1   public string   name;
2   public string   type;
3   public int      size;
4   public int      modules;
5   public float    structureRatio;
6   public int      engines;
    // Hard Points
7   public int      hpFront;
8   public int      hpDorsal;
9   public int      hpSideL;
10  public int      hpSideR;
11  public int      hpE1;
12  public int      hpE2;
13  public int      hpPd;
    // signature sizes
14  public float    width;
15  public float    length;
16  public float    height;

17 public int       command;
18  public int      cargo;
19  public int      utility;

20  public Data.Tech.Type reqTechType;
21  public float    reqTechLevel;

22  public float    reqRelations;
23  public string   empty;

24  public string   toolTip;

*/