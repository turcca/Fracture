using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Game
{
    public Dictionary<string, Location> locations { get; private set; } 
    public EventManager events {get; private set; } 
    public Player player { get; private set; }
    public Simulation.Factions factions { get; private set; }
    public Simulation.GlobalMarket globalMarket { get; private set; }
    public Navigation.NavNetwork navNetwork { get; private set; }
    public List<Simulation.NPCShip> ships { get; private set; }

    public GameSettings gameSettings { get; private set; }

    private float elapsedDays = 0;  // SAVE


    internal void initGameSettings()
    {
        gameSettings = new GameSettings();
    }
    internal void initGlobalMarket()
    {
        globalMarket = new Simulation.GlobalMarket();
    }
    internal void initFactions()
    {
        factions = new Simulation.Factions();
    }

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
                    //Simulation.LocationEconomyAI ai = new Simulation.LocationEconomyAI();
                    Data.Location data = new Data.Location();
                    data.features = locationFeatures[id];
                    locations.Add(id, new Location(id, loc.gameObject.transform.position, data));
                }
                else
                {
                    //Simulation.LocationEconomyAI ai = new Simulation.LocationEconomyAI();
                    //Data.Location data = new Data.Location();
                    if (locations.ContainsKey (id)) Debug.LogWarning ("WARNING: location '"+id+"' already existed");
                    else locations.Add(id, new Location(id, loc.gameObject.transform.position));
                    Tools.debug("Id '" + id + "' not found in location data!");
                }
            }
        }
        else
        {
            //Simulation.LocationEconomyAI ai = new Simulation.LocationEconomyAI();
            //Data.Location data = new Data.Location();
            locations.Add("test", new Location("test", new Vector3(0,0,0)));
            Tools.debug("Using test location!");
            return;
        }

        //// all locations
        Location[] arr = new Location[locations.Count];
        locations.Values.CopyTo(arr, 0);

        //// all navpoints
        //GameObject navRoot = GameObject.Find("NavpointRoot");
        List<NavpointId> navs = new List<NavpointId>();
        //foreach (NavpointId nav in navRoot.GetComponentsInChildren<NavpointId>())
        //{
        //    navs.Add(nav);
        //}
        navNetwork = new Navigation.NavNetwork(arr, navs);

        ////debug locations descriptions
        //if (true){
        //    foreach (var pair in locations)
        //        Debug.Log(pair.Key + ":\n" + Faction.getImportanceDescription(pair.Value) + "");
        //}
    }

    internal void initEvents()
    {
        events = new EventManager();
        events.createAllEvents();
    }

    internal void initNPCShips()
    {
        ships = new List<Simulation.NPCShip>();
        foreach (Location location in locations.Values)
        {
            int tradeShips = Simulation.Parameters.getStartingTradeShips(location);
            ///@todo read amount of ships from data/location?
            for (int i = 0; i < tradeShips; i++)
            {
                ships.Add(new Simulation.NPCShip(location));
            }
        }
    }

    internal void initPlayer()
    {
        Debug.Log("gameState: " + GameState.getState());
        if (GameState.isState(GameState.State.Simulation) == false)
        {
            player = new Player();
            //@todo init player position based on faction choice
            player.position = new Vector3(237, 0, 143); // player starting position
                                                        //@todo init advisors from starting settings: faction choice & ideology
            player.init();
        }
    }


    private Dictionary<string, Data.LocationFeatures> parseLocationFeatures()
    {
        Dictionary<string, Data.LocationFeatures> rv = new Dictionary<string, Data.LocationFeatures>();
        FileInfo src = null;
        StreamReader reader = null;
 
        src = new FileInfo (Application.dataPath + "/data/locations.tsv");
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
                    // first block contains id, rest is data [TSV tab separated values]
                    rv.Add(line.Split('\t')[0], DataParser.parseLocationFeatures(line));
                }
            }

            //Debug.Log("2v08: " + rv["2v08"].description2 + " --- " + "populiation: " + rv["2v08"].population);
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

    public int getElapsedDays()
    {
        return (int)elapsedDays;
    }

    // ------------------------------------------------------------------
    public void tick(float days)
    {
        elapsedDays += days;

        if (GameState.isState(GameState.State.Event) == false)
        {
            // [Simulation.]
            // economy, ideology, 
            // factions
            foreach (Location location in locations.Values)
            {
                location.tick(days);
            }
            factions.tick(days);

            // Events
            if (GameState.isState(GameState.State.Simulation) == false)
            {
                player.tick(days);
                events.tick(days);

                // atLocation Events
                if (Root.game.player.isAtLocation())
                {
                    EventBase e = events.queryAtLocationEvents();
                    if (e != null)
                        eventStart(e);
                }
                // inLocation triggered from LocationSceneState.Update()

                // Starmap Events
                else
                {
                    EventBase e = events.queryStarmapEvents();
                    if (e != null)
                        eventStart(e);
                }
            }
            
            // ships, trade
            foreach (Simulation.NPCShip ship in ships)
            {
                ship.sendFreeShip();
                ship.tick(days);
            }
            globalMarket.updateGlobalEconomy(days);
        }
    }
    // ------------------------------------------------------------------

    internal string locationShortagesToDebugString()
    {
        string rv = "";
        int n = 0;
        foreach (Location location in locations.Values)
        {
            string s = location.economy.shortagesToDebugString();
            string a = "";
            if (s != "") 
            {
                n++;
                a +=  s;
            }
            if (a != "")
                rv += a;
        }
        if (n == 0)
            return "";
        else
            return "Shortages: ["+n+"] \n"+rv;
    }

    public void eventStart(EventBase e)
    {
        Root.ui.showEventWindow();
        GameState.requestState(GameState.State.Event);
        events.handleEvent(e, eventDone);
    }

    public void eventDone()
    {
        Root.ui.hideEventWindow();
        GameState.returnFromState(GameState.State.Event);
    }

    internal void startRandomStarmapEvent()
    {
        EventBase e = events.pickEvent();
        if (e != null)
            eventStart(e);
    }



    // ----------------- debug tools
    public string shipUsageToString()
    {
        string rv = "";

        int shipCount = 0;
        int free = 0;
        foreach (Simulation.NPCShip ship in ships)
        {
            ++shipCount;
            if (ship.free) free++;
        }
        rv += "Ships: "+ shipCount+ " (free: "+free+")";
        return rv;
    }
}
