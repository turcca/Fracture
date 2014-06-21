using UnityEngine;
using System.Collections;

public class ShipyardUI : MonoBehaviour
{
    /*

    public static ShipyardUI Instance; // Gives out references to the only LocationUI script object

    public GameObject ship; 		// the anchor, ship "root"
    public GameObject shipCam;		// UI cam that only records layer 8 [Player]

    // Use this for initialization
    void Awake()
    {

        ShipyardUI.Instance = this;
    }


    void enable(bool activate)	// called when entering location and loading locationUI
    {
        if (activate)
        {
            moveShipToUI();
        }
        else
        {
            moveShipToStarChart();
        }

    }

    // --------------------------------------------------------------------------------------------


    void moveShipToUI()
    {
        // transfer ship to UI anchor
        PlayerShip.Instance.playerShip.obj.transform.parent = ship.transform;
        PlayerShip.Instance.playerShip.obj.transform.localPosition = Vector3.zero;
        //PlayerShip.Instance.playerShip.obj.transform.localScale = Vector3(0.2,0.2,0.2);
        PlayerShip.Instance.playerShip.obj.transform.localEulerAngles = new Vector3(0, 0, 0);

    }
    void moveShipToStarChart()
    {
        // transfer ship back to its starChart player-location
        PlayerShip.Instance.playerShip.obj.transform.parent = LocationFinder.Instance.player.transform;
        PlayerShip.Instance.playerShip.obj.transform.localPosition = Vector3.zero;
        //PlayerShip.Instance.playerShip.obj.transform.localScale = Vector3(0.2,0.2,0.2);
        PlayerShip.Instance.playerShip.obj.transform.localEulerAngles = new Vector3(0, 0, 0);

    }
     */
}
