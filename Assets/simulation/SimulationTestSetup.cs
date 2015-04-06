using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NewEconomy
{

public class SimulationTestSetup : MonoBehaviour
{
    static public Dictionary<string, Location> locations = new Dictionary<string, Location>();
    LocationEconomyAI ai = new LocationEconomyAI();
    NavNetwork tradeNetwork;

    void Start()
    {
        initLocations();
    }

    void Update()
    {
        foreach (Location loc in locations.Values)
        {
            loc.tick(Time.deltaTime / 10.0f);
        }
    }

    private void initLocations()
    {
        
        GameObject root = GameObject.Find("SystemRoot");
        if (root)
        {
            foreach (LocationId loc in root.GetComponentsInChildren<LocationId>())
            {
                string id = loc.getId();
                NewEconomy.LocationEconomyData data = new LocationEconomyData();
                data.generateDebugData();
                locations.Add(id, new Location(id, loc.gameObject.transform.position, new LocationEconomy(data, ai)));
            }
        }

        // all locations
        Location[] arr = new Location[locations.Count];
        locations.Values.CopyTo(arr, 0);
        List<NavpointId> navs = new List<NavpointId>();
        tradeNetwork = new NavNetwork(arr, navs);
    }
}

}