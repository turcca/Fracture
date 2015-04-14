using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeNetVisualisation : MonoBehaviour
{
    public GameObject tradeShipPrefab;

    NPCShip trackedShip;

    // Use this for initialization
    void Start()
    {
        foreach (NPCShip ship in Root.game.ships)
        {
            createShip(ship);
        }
    }

    private void createShip(NPCShip ship)
    {
        GameObject shipObj = (GameObject)GameObject.Instantiate(tradeShipPrefab, ship.position, Quaternion.identity);
        TradeShip shipData = shipObj.GetComponent<TradeShip>();
        shipData.trackShip(ship);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDrawGizmos()
    {
        //foreach (Location loc in Game.universe.locations.Values)
        //{
        //    foreach(Location toLoc in Game.universe.tradeNetwork.getNeighbours(loc))
        //    {
        //        Gizmos.color = new Color(1.0f, 1.0f, 1.0f);
        //        Gizmos.DrawLine(loc.position, toLoc.position);
        //    }
        //}

        foreach (NavNode node in Root.game.tradeNetwork.navNodes)
        {
            foreach (NavNode neighbour in node.links)
            {
                Gizmos.color = new Color(1.0f, 1.0f, 1.0f);
                Gizmos.DrawLine(node.position, neighbour.position);
            }
        }

        if (trackedShip != null)
        {
            Gizmos.color = new Color(1.0f, 0, 0);
            Gizmos.DrawSphere(trackedShip.position, 4.0f);
            Gizmos.color = new Color(1.0f, 1.0f, 0);
            Gizmos.DrawSphere(trackedShip.destination.position, 5.0f);
        }
        
    }

    void OnGUI()
    {
        if (trackedShip != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(trackedShip.position);
            GUI.Label(new Rect(pos.x + 20, Screen.height - pos.y, 300, 20),
                "Captain: " + trackedShip.captain);
            if (trackedShip.wantedCommodityList.Count >= 2)
            {
                GUI.Label(new Rect(pos.x + 20, Screen.height - pos.y + 15, 300, 20),
                    "Wants: " + trackedShip.wantedCommodityList[0] + ", " + trackedShip.wantedCommodityList[1]);
            }

            int cursor = 30;
            foreach (KeyValuePair<string, int> pair in trackedShip.inventory.commodities)
            {
                if (pair.Value > 0)
                {
                    GUI.Label(new Rect(pos.x + 20, Screen.height - pos.y + cursor, 300, 50),
                        pair.Key + ": " + pair.Value.ToString());
                    cursor += 15;
                }
            }
        }
    }

    public void trackShip(NPCShip ship)
    {
        trackedShip = ship;
    }
}
