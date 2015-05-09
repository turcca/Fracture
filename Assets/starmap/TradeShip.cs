using UnityEngine;
using System.Collections;

public class TradeShip : MonoBehaviour
{
    Simulation.NPCShip trackedShip;

    TradeNetVisualisation visualisation;

    // Use this for initialization
    void Start()
    {
        visualisation = GameObject.Find("Debug").GetComponent<TradeNetVisualisation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedShip != null)
        {
            gameObject.transform.position = trackedShip.position;
        }
    }

    public void trackShip(Simulation.NPCShip ship)
    {
        trackedShip = ship;
    }

    public void OnMouseDown()
    {
        visualisation.trackShip(trackedShip);
    }
}
