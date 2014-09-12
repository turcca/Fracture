using UnityEngine;
using System.Collections;

public class TradeNetVisualisation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        foreach (Location loc in Game.universe.locations.Values)
        {
            foreach(Location toLoc in Game.universe.tradeNetwork.getLocationsFrom(loc, 1))
            {
                Gizmos.color = new Color(0.5f, 0.5f, 0.5f);
                Gizmos.DrawLine(loc.position, toLoc.position);
            }
        }

        foreach(NPCShip ship in Game.universe.ships)
        {
            Gizmos.color = new Color(1.0f, 0, 0);
            Gizmos.DrawSphere(ship.position, 0.3f);
        }
    }
}
