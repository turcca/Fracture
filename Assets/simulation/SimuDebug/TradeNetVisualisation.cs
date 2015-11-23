﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeNetVisualisation : MonoBehaviour
{
    public GameObject tradeShipPrefab;

    Simulation.NPCShip trackedShip;

    // Use this for initialization
    void Start()
    {
        foreach (Simulation.NPCShip ship in Root.game.ships)
        {
            createShip(ship);
        }
    }

    private void createShip(Simulation.NPCShip ship)
    {
        GameObject shipObj = (GameObject)GameObject.Instantiate(tradeShipPrefab/*, ship.position, Quaternion.identity*/);
        TradeShip shipData = shipObj.GetComponent<TradeShip>();
        shipData.trackShip(ship);
        ship.tradeShip = shipData;
        //shipObj.GetComponent<MeshFilter>().mesh = MeshFilter.
        ship.tradeShip.setVisibilityToStarmap(ship.isVisible);

        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.GetComponent<Renderer>().material.shader = Shader.Find ("Custom/brdf");
        box.transform.parent = shipObj.transform;
        box.transform.localPosition = new Vector3(0,-1,0);
        box.transform.localScale = new Vector3(1,1,1);
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

        foreach (Navigation.NavNode node in Root.game.navNetwork.navNodes)
        {
            foreach (Navigation.NavNode neighbour in node.links)
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

                GUI.Label(new Rect(pos.x + 20, Screen.height - pos.y + 15, 300, 20* trackedShip.tradeList.Count),
                      trackedShip.cargoToDebugString());
                      //"Wants: " + trackedShip.wantedCommodityList[0] + ", " + trackedShip.wantedCommodityList[1]);

            int cursor = 30;
            /*
            foreach (KeyValuePair<string, int> pair in trackedShip.inventory.commodities)
            {
                if (pair.Value > 0)
                {
                    GUI.Label(new Rect(pos.x + 20, Screen.height - pos.y + cursor, 300, 50),
                        pair.Key + ": " + pair.Value.ToString());
                    cursor += 15;
                }
            }
            */
        }
        foreach (Location location in Root.game.locations.Values)
        {
            if (location.economy.hasShortage())
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(location.position);
                GUI.Label(new Rect(pos.x + 20, Screen.height - pos.y, 300, 80),
                          location.id +"\n" + location.economy.shortagesToDebugStringFloating());
            }
        }
    }


    public void trackShip(Simulation.NPCShip ship)
    {
        trackedShip = ship;
    }
}
