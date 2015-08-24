using UnityEngine;
using System.Collections;

namespace Simulation
{
    public static class NPCShipVisualisation 
    {
        static GameObject NPCShips = GameObject.Find("NPCShips");
        static GameObject tradeShipPrefab;

        // Use this for initialization
        public static void initNPCShipVisuals()
        {
            tradeShipPrefab = (GameObject)Resources.Load("starmap/prefabs/TradeShip");
            if (!tradeShipPrefab)
            {
                Debug.LogError ("Ship error: couldn't find resource");
                return;
            }
            foreach (NPCShip ship in Root.game.ships)
            {
                createShip(ship);
            }
        }

        private static void createShip(NPCShip ship)
        {
            GameObject shipObj = (GameObject)GameObject.Instantiate(tradeShipPrefab);
            shipObj.name = ship.isTradeShip ? "TradeShip" : "WarShip";
            TradeShip shipData = shipObj.GetComponent<TradeShip>();
            shipData.trackShip(ship);
            shipObj.transform.parent = NPCShips.transform;
            ship.tradeShip = shipData;
        }
    }
}