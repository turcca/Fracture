using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Universe
{
    public Dictionary<string, Location> locations = new Dictionary<string, Location>();
    public EventManager eventManager = new EventManager();
    public Player player { get; private set; }
    public NavNetwork tradeNetwork;
    public List<NPCShip> ships = new List<NPCShip>();

    Dictionary<string, string> locationData = new Dictionary<string, string>();

    public Universe()
    {
    }

    public void initLocations()
    {
        parseLocationData();
        GameObject root = GameObject.Find("SystemRoot");
        if (root)
        {
            foreach (LocationId loc in root.GetComponentsInChildren<LocationId>())
            {
                if (locationData.ContainsKey(loc.Id))
                {
                    locations.Add(loc.Id, new Location(loc.Id, locationData[loc.Id], loc.gameObject.transform.position));
                }
                else
                {
                    Tools.error("Id '" + loc.Id + "' not found in location data!");
                }
            }
        }
        else
        {
            // for scene testing purposes
            locations.Add("a", new Location("a", locationData["a"], new Vector2(0, 0)));
        }

        Location[] arr = new Location[locations.Count];
        locations.Values.CopyTo(arr, 0);
        tradeNetwork = new NavNetwork(arr);
    }

    public void initNPCShips()
    {
        foreach (Location location in locations.Values)
        {
            for (int i = 0; i < 5; i++)
                ships.Add(new NPCShip(location));
        }
    }

    public void initPlayer()
    {
        player = new Player();
        player.init();
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

    public Location getClosestHabitat(Vector2 pos)
    {
        Location closestPlanet = null;
        float closestDistance = 9999999.0f;
        foreach (Location loc in locations.Values)
        {
            if (Vector2.Distance(pos, loc.position) < closestDistance)
            {
                closestPlanet = loc;
                closestDistance = Vector2.Distance(pos, loc.position);
            }
        }
        return closestPlanet;
    }

    public void tick(float days)
    {
        if (GameState.getState() != GameState.State.Event)
        {
            foreach (Location location in locations.Values)
            {
                location.tick(days);
            }
            player.tick(days);
            eventManager.tick(days);

            foreach (NPCShip ship in ships)
            {
                ship.tick(days);
            }
        }
    }
}
