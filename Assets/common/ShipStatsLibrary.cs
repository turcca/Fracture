using UnityEngine;
using System.Collections.Generic;
using System.IO;


public static class ShipStatsLibrary
{
    // ship IDs / ShipStats
    static Dictionary<string, ShipStats> shipStats;


    // constructor
    static ShipStatsLibrary()
    {
        Debug.Log("reading ships.csv");
        shipStats = parseShipStats();
    }
        
    public static ShipStats getShipStat(string id)
    {
        if (shipStats.ContainsKey(id))
            return shipStats[id];
        else
        {
            Debug.LogError("no ship ID in library: '"+id+"'");
            return null;
        }
    }
    public static Dictionary<string, ShipStats> getShipStats()
    {
        return shipStats;
    }


    private static Dictionary<string, ShipStats> parseShipStats()
    {
        Dictionary<string, ShipStats> rv = new Dictionary<string, ShipStats>();
        FileInfo src = null;
        StreamReader reader = null;

        src = new FileInfo(Application.dataPath + "/data/ships.tsv");
        if (src != null && src.Exists)
        {
            reader = src.OpenText();
        }

        if (reader == null)
        {
            Debug.Log("ships.tsv data not found or not readable.");
        }
        else
        {
            // Each line starting from DATASTART to DATAEND contains one location 
            // split lines to strings and construct features from each line between markers
            List<string> lines = new List<string>();
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }

            bool dataBlock = false;
            foreach (string line in lines)
            {
                if (line.StartsWith("DATASTART"))
                {
                    dataBlock = true;
                }
                else if (line.StartsWith("DATAEND"))
                {
                    dataBlock = false;
                    break;
                }
                else if (dataBlock)
                {
                    // first block contains id, rest is data [TSV tab separated values]
                    rv.Add(line.Split('\t')[0], DataParser.parseShipStats(line));
                }
            }
        }
        return rv;
    }


}


public class ShipStats
{
    public string   name;               // 1
    public string   type;               // 2
    public int      size;               // 3
    public int      modules;            // 4
    public float    structureRatio;     // 5
    public int      engines;            // 6
    // Hard Points
    public int      hpFront;            // 7
    public int      hpDorsal;           // 8
    public int      hpSideL;            // 9
    public int      hpSideR;            // 10
    public int      hpE1;               // 11
    public int      hpE2;               // 12
    public int      hpPd;               // 13
    // signature sizes
    public float    width;              // 14
    public float    length;             // 15
    public float    height;             // 16

    public int      command;            // 17
    public int      cargo;              // 18
    public int      utility;            // 19

    public Data.Tech.Type reqTechType;  // 20
    public float    reqTechLevel;       // 21

    public float    reqRelations;       // 22
    public string   empty;              // 23

    public string   toolTip;            // 24


    // --------------------------------------

    public float exteriorVolume(bool returnRounded = true)
    {
        return returnRounded ? 
            Mathf.Round((float)modules + (float)modules * structureRatio) : 
            (float)modules + (float)modules * structureRatio;
    }
    public float speed()
    {
        return Mathf.Round((float)engines * 2f / exteriorVolume(false) * 100f);
    }
    public float hull()
    {
        return Mathf.Round(exteriorVolume(false) *2f);
    }
    public float signatureFront()
    {
        return Mathf.Round(width * height *10f)/10f;
    }
    public float signatureSide()
    {
        return Mathf.Round(length * height * 10f) / 10f;
    }
    public int hpVal()
    {
        return hpFront + hpDorsal + hpSideL + hpSideR + hpE1 + hpE2 + hpPd;
    }
    public int crew()
    {
        return
            command * 50 +
            utility * 50 +
            engines * 20 +
            cargo   * 10 +
            hpVal() * 50;
    }
    public int value()
    {
        return
            (int)
            Mathf.Round(
            (exteriorVolume(false) * 10f +
            hpVal() * 300f)
            * Mathf.Sqrt(reqTechLevel)
            /10f)*10; // round up to tens
    }
    /// <summary>
    /// tech level as used in Simulation.resources
    /// </summary>
    /// <returns></returns>
    public int getRequiredTechLevel()
    {
        return (int)reqTechLevel;
    }

}
