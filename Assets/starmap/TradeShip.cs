using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TradeShip : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    public Simulation.NPCShip trackedShip;

    Renderer rend;
    Collider coll;
    ParticleSystem ps;

    // debug
    //public bool free;
    //public bool isGoingToDestination;
    //public float downTime;
    //public int nodes;
    //public string home;
    //public string destination;

   
    // Update is called once per frame
    void Update()
    {
        if (trackedShip != null)
        {
            gameObject.transform.position = new Vector3(trackedShip.position.x, 0.1f, trackedShip.position.z);//+ trackedShip.deviation;

            //if (true) // debugging
            //{
            //    free = trackedShip.free;
            //    isGoingToDestination = trackedShip.isGoingToDestination;
            //    downTime = trackedShip.downtime;
            //    nodes = trackedShip.getNavPointCount();
            //    home = (trackedShip.home != null) ? trackedShip.home.id : "";
            //    destination = (trackedShip.destination != null) ? trackedShip.destination.id : "";
            //}
        }
    }

    public void trackShip(Simulation.NPCShip ship)
    {
        trackedShip = ship;
    }
    /*
    public void OnMouseDown()
    {
        if (visualisation)
        {
            visualisation.trackShip(trackedShip);
        }
    }
    */

    public void eventPointerOver(bool entering) // false = exiting
    {
        if (GameState.isState(GameState.State.Simulation) == false)
        {
            Simulation.StarmapVisualization.mouseOverNPCShipForInfo(trackedShip, entering);
            if (entering) Simulation.StarmapVisualization.lineUI.setTargetObject(trackedShip.tradeShip.gameObject);
            else Simulation.StarmapVisualization.lineUI.clearTargetObject();
        }
    }
    public void eventPointerClick()
    {
        MapMoveTarget.setChaseTarget(trackedShip);
    }


    public void setVisibilityToStarmap(bool isVisible)
    {
        // manage all visual triggers
        if (!rend)
        {
            rend = GetComponent<Renderer>();
            coll = GetComponent<Collider>();
            ps = GetComponent<ParticleSystem>();
        }
        rend.enabled = isVisible;
        coll.enabled = isVisible;

        if (isVisible) ps.Play();
        else ps.Stop();
    }

}
