using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TradeShip : MonoBehaviour
{
    public Simulation.NPCShip trackedShip;

    TradeNetVisualisation visualisation;

    Renderer renderer;
    Collider collider;
    ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        ps = GetComponent<ParticleSystem>();
        ps.Stop ();
        collider = GetComponent<Collider>();
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

            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log ("ship: "+trackedShip.captain);
            }
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
    }*/
    public void setVisibilistyToStarmap (bool isVisible)
    {
        // manage all visual triggers
        renderer.enabled = isVisible;
        collider.enabled = isVisible;

        if (isVisible) ps.Play ();
        else ps.Stop ();
    }
}
