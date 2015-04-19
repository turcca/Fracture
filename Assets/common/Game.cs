using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Game
{
    public Dictionary<string, Location> locations { get; private set; } 
    public EventManager events {get; private set; } 
    public Player player { get; private set; }
    public NavNetwork tradeNetwork { get; private set; }
    public List<NPCShip> ships { get; private set; }

    bool testingMode = false;

    internal void initLocations()
    {
        locations = new Dictionary<string, Location>();
        Dictionary<string, Data.LocationFeatures> locationFeatures = new Dictionary<string, Data.LocationFeatures>();
        locationFeatures = parseLocationFeatures();
        GameObject root = GameObject.Find("SystemRoot");
        if (root)
        {
            foreach (LocationId loc in root.GetComponentsInChildren<LocationId>())
            {
                string id = loc.getId();
                if (locationFeatures.ContainsKey(id))
                {
                    NewEconomy.LocationEconomyAI ai = new NewEconomy.LocationEconomyAI();
                    Data.Location data = new Data.Location();
                    locations.Add(id, new Location(id, loc.gameObject.transform.position,
                                  new NewEconomy.LocationEconomy(data, ai)));
                }
                else
                {
                    NewEconomy.LocationEconomyAI ai = new NewEconomy.LocationEconomyAI();
                    Data.Location data = new Data.Location();
                    locations.Add(id, new Location(id, loc.gameObject.transform.position,
                                  new NewEconomy.LocationEconomy(data, ai)));
                    Tools.debug("Id '" + id + "' not found in location data!");
                }
            }
        }
        else
        {
            // for scene testing purposes
            //locations.Add("a", new Location("1c01", locationData["1c01"], new Vector2(0, 0)));
            testingMode = true;
            return;
        }

        //// all locations
        //Location[] arr = new Location[locations.Count];
        //locations.Values.CopyTo(arr, 0);
        //// all navpoints
        //GameObject navRoot = GameObject.Find("NavpointRoot");
        //List<NavpointId> navs = new List<NavpointId>();
        //foreach (NavpointId nav in navRoot.GetComponentsInChildren<NavpointId>())
        //{
        //    navs.Add(nav);
        //}
        //tradeNetwork = new NavNetwork(arr, navs);
    }

    internal void initEvents()
    {
        events = new EventManager();
    }

    internal void initNPCShips()
    {
        ships = new List<NPCShip>();

        if (testingMode) return;

        foreach (Location location in locations.Values)
        {
            location.initShips();
        }
    }

    internal void initPlayer()
    {
        player = new Player();
        player.init();
        player.position = new Vector3(-280, 0, -180);
    }


    private Dictionary<string, Data.LocationFeatures> parseLocationFeatures()
    {
        Dictionary<string, Data.LocationFeatures> rv = new Dictionary<string, Data.LocationFeatures>();
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
                    // first block contains id, rest is data
                    rv.Add(line.Split(',')[0], DataParser.parseLocationFeatures(line));
                }
            }
        }
        return rv;
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
            //player.tick(days);
            //events.tick(days);

            //foreach (NPCShip ship in ships)
            //{
            //    ship.tick(days);
            //}
        }
    }
}
