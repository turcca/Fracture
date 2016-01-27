using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MapMouseCatcher : MonoBehaviour
{
    public PlayerShipMover playerMover;
    public Simulation.NPCShip chaseTarget;

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000.0f, LayerMask.GetMask("MouseCatcher")))
            {
                setTarget(hit.point);
                chaseTarget = null;
            }
        }
        */
        if (chaseTarget != null)
        {
            setTarget(chaseTarget.position);
        }
    }

    /// <summary>
    /// PointerDown from EventTrigger
    /// </summary>
    public void eventPointerDown()
    {
        Debug.Log("MouseCatcher ");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("UI")))
        {
            setTarget(hit.point);
            chaseTarget = null;
        }
    }

    public void setTarget(Vector3 point)
    {
        playerMover.setTarget(point);
    }
    public void setChaseTarget(Simulation.NPCShip chaseTargetShip)
    {
        chaseTarget = chaseTargetShip;
    }
}


// Static access

public static class MapMoveTarget
{
    static MapMouseCatcher catcher;

    public static void setTarget(Vector3 point)
    {
        if (catcher == null) catcher = GameObject.Find("MouseCatcher").GetComponent<MapMouseCatcher>();

        catcher.setTarget(point);
    }
    public static void setChaseTarget(Simulation.NPCShip ship)
    {
        if (catcher == null) catcher = GameObject.Find("MouseCatcher").GetComponent<MapMouseCatcher>();

        catcher.setChaseTarget(ship);
    }
}
