using UnityEngine;
using System;
using System.Collections;

namespace Simulation
{
    public static class StarmapVisualization 
    {
        static GameObject NPCShips;
        static GameObject tradeShipPrefab;

        public static GameMenuSystem ui;

        public static LaserScope lineUI; // line renderer effect for NPC ship info ui


        // Use this for initialization
        public static void initNPCShipVisuals()
        {
            if (!NPCShips) NPCShips = GameObject.Find("NPCShips");
            if (!lineUI) lineUI = GameObject.Find("lineUI").GetComponent<LaserScope>();

            if (!tradeShipPrefab)
            {
                tradeShipPrefab = (GameObject)Resources.Load("starmap/prefabs/TradeShip");
                if (!tradeShipPrefab)
                {
                    Debug.LogError ("Ship error: couldn't find resource");
                    return;
                }
            }
            // populate NPC ships
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

            ship.tradeShip.setVisibilityToStarmap(ship.isVisible);
        }


        public static void mouseOverNPCShipForInfo(NPCShip ship, bool isOver)
        {
            if (infoReady())
            {
                ui.showNPCshipInfo(ship, isOver);
            }
        }
        public static void mouseOverLocationForInfo(LocationStarmapVisibility location, bool isOver)
        {
            if (infoReady())
            {
                ui.showLocationInfo(location, isOver);
            }
        }


        private static bool infoReady() // UI /mouse-over info box for NPC ships
        {
            if (ui == null)
            {
                try
                {
                    ui = GameObject.Find("GameCanvas").GetComponent<GameMenuSystem>();
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
            return true;
        }
    }
}