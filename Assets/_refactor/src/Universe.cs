using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Universe
{
    public Dictionary<string, Location> locations = new Dictionary<string, Location>();
    Dictionary<string, string> locationData = new Dictionary<string, string>();

    public Universe()
    {
        parseLocationData();

        locations.Add("a", new Location(locationData["a"]));
        locations.Add("b", new Location(locationData["b"]));
    }

    void parseLocationData()
    {
        FileInfo src = null;
        StreamReader reader = null;
 
        src = new FileInfo (Application.dataPath + "/data/locations.csv");
        if (src != null && src.Exists)
        {
            reader = src.OpenText();
        }
 
        if (reader == null)
        {
            Debug.Log("Location data not found or not readable.");
        }
        else
        {
            // Read each line from the file
            List<string> lines = new List<string>();
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }
            Debug.Log(lines[0]);

            bool dataBlock = false;
            foreach (string line in lines)
            {
                if (line.StartsWith("DATASTART"))
                {
                    dataBlock = true;
                }
                else if (dataBlock)
                {
                    locationData.Add(line.Split(',')[0], line);
                }
                else if (line.StartsWith("DATAEND"))
                {
                    dataBlock = false;
                    break;
                }
            }
        }
    }

    public void tick(float days)
    {
        foreach (Location location in locations.Values)
        {
            location.tick(days);
        }
    }
}
