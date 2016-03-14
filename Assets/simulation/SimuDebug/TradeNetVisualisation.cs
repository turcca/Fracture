using UnityEngine;
using System;
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
        }
        
        
        // draw guis
        string rv = "    Global Resource Tracker \n";
        foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)))
        {
            rv += type.ToString() + " [rate: " + Mathf.Round(Root.game.globalMarket.getGlobalResourceGrowth(type) * 100f) / 100f + "] [stock: " + Mathf.Round(Root.game.globalMarket.getGlobalResources(type)) + "]\n";
        }
        GUI.Label(new Rect(Screen.width / 2.6f, Screen.height - 150, 500, 150), rv);

        // location tags and shortages
        foreach (Location location in Root.game.locations.Values)
        {
            //if (location.economy.hasShortage())
            {
                float netMul = Mathf.Round(location.economy.getTotalEffectiveLocationResourceMultiplier() * 100f) / 100f;
                GUIStyle style = new GUIStyle();

                if (netMul < 0f) style.normal.textColor = new Color(-netMul +0.3f, 0.1f, 0.1f);
                else style.normal.textColor = new Color(0.1f, 0.1f+netMul, 0.1f);

                Vector3 pos = Camera.main.WorldToScreenPoint(location.position);
                GUI.Label(new Rect(pos.x + 20, Screen.height - pos.y, 300, 16),
                          location.id + " (net: " + netMul + ")", style);
                // shortages
                if (location.economy.hasShortage())
                {
                    GUI.Label(new Rect(pos.x + 20, Screen.height - pos.y +16, 300, 80), location.economy.shortagesToDebugStringFloating());
                }
            }
        }


    }


    public void trackShip(Simulation.NPCShip ship)
    {
        trackedShip = ship;
    }
}
