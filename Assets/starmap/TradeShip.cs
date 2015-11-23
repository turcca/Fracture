using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TradeShip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Simulation.NPCShip trackedShip;

    TradeNetVisualisation visualisation;

    Renderer renderer;
    Collider collider;
    ParticleSystem ps;

    // debug
    //public bool free;
    //public bool isGoingToDestination;
    //public float downTime;
    //public int nodes;
    //public string home;
    //public string destination;


    // Use this for initialization / simulation
    void Start()
    {
        // check for simulation scene setup
        GameObject obj = GameObject.Find("Debug");
        if (obj)
        {
            visualisation = obj.GetComponent<TradeNetVisualisation>();
        }
    }

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

    public void OnMouseDown()
    {
        if (visualisation)
        {
            visualisation.trackShip(trackedShip);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!visualisation)
        {
            Simulation.NPCShipVisualisation.mouseOverNPCShipForInfo(trackedShip, true);
            Simulation.NPCShipVisualisation.lineUI.setTargetObject(trackedShip.tradeShip.gameObject);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!visualisation)
        {
            Simulation.NPCShipVisualisation.mouseOverNPCShipForInfo(trackedShip, false);
            Simulation.NPCShipVisualisation.lineUI.clearTargetObject();
        }
    }


    public void setVisibilityToStarmap (bool isVisible)
    {
        // manage all visual triggers
        if (!renderer)
        {
            renderer = GetComponent<Renderer>();
            collider = GetComponent<Collider>();
            ps = GetComponent<ParticleSystem>();
        }
        renderer.enabled = isVisible;
        collider.enabled = isVisible;

        if (isVisible) ps.Play ();
        else ps.Stop ();
    }



    public Vector3 getLineUIHalfwayPoint()
    {
        Vector3 distance = this.gameObject.transform.position - Root.game.player.position;
        //Debug.Log ("x: "+distance.x+" z: "+distance.z);
        if (distance.x > 0 && distance.z < -2 /*&& distance.z > -30*/) return Root.game.player.position + distance /1.4f + new Vector3(0,0,5.0f);
        return Root.game.player.position + distance /1.3f;
    }
}
