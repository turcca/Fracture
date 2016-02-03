using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class StarmapInfo : MonoBehaviour 
{
    public Simulation.NPCShip ship { get; private set; }
    public LocationStarmapVisibility location { get; private set; }
    public Text info;

    RectTransform rectUi;

    bool needsUpdate;
    bool? trackingShipAndNotLocation = true; // null = no tracking

    void Start()
    {
        if (!rectUi) rectUi = GetComponent<RectTransform>();
    }

    void Update()
    {
        updateInfoPosition ();
    }
    void updateInfoPosition()
    {
        if (trackingShipAndNotLocation != null)
        {
            if ((bool)trackingShipAndNotLocation)
            {
                if (ship.isVisible) setInfoWindowPointForShip();
            }
            else
            {
                setInfoWindowPointForLocation();
                loadLocationInfo();
            }
        }
    }

    // -------------------------------------------


    public void setVisibility(Simulation.NPCShip shipInput, bool isVisible)
    {
        if (isVisible)
            trackingShipAndNotLocation = true;
        else
            trackingShipAndNotLocation = null;

        if (!rectUi) rectUi = GetComponent<RectTransform>();
        ship = shipInput;
        loadShipInfo ();
        updateInfoPosition();
    }
    public void setVisibility(LocationStarmapVisibility loc, bool isVisible)
    {
        if (isVisible)
            trackingShipAndNotLocation = false;
        else
            trackingShipAndNotLocation = null;

        if (!rectUi) rectUi = GetComponent<RectTransform>();
        location = loc;
        loadLocationInfo();
        updateInfoPosition();
    }



    void loadShipInfo () 
    {
        info.text = getShipInfo();
	}
    void loadLocationInfo()
    {
        info.text = getLocationInfo();
    }

    string getShipInfo()
    {
        string rv = "";
        string cm = "";
        rv += "<b>Captain " + ship.captain + "</b>";
        rv += "\n";
        if (ship.home.features.visibility == Data.Location.Visibility.Connected)
        {
            //rv += ship.home.name + "\n";
            
            cm += ship.getCargoManifest(); // TODO: if player is trusted
            if (cm != "")
            {
                cm = cm.Insert(0, "<i><size=11><color=#FFFFFF80>");
                cm += "</color></size></i>";
            }

            if (cm != "")
            {
                // Pre-
                if (ship.isGoingToDestination) cm = cm.Insert(0, "Cargo ship exporting \n"); //isGoingToDestination ? "Exporting" : "Importing";
                else cm = cm.Insert(0, "Cargo ship importing \n");
            }
            else
            {
                // (empty) No trade items going this way
                if (ship.isGoingToDestination) cm += "Cargo ship in transit to" + "\n" + ship.destination.name;
                else cm += "Cargo ship in transit to" + "\n" + ship.home.name;
            }
        }
        return rv + cm;
    }
    string getLocationInfo()
    {
        string rv = "";
        if (location.location.features.visibility != Data.Location.Visibility.Connected) rv += "\nNo Active Beacon!\n";
        else
        {
            //rv += location.location.name;
            rv += location.location.economy.shortagesToStringItems();
        }
        return rv;
    }


    // position for shipInfo popup screen
    void setInfoWindowPointForShip()
    {
        /*
        // follow mouse -implementation
        float offset = rectUi.sizeDelta.y +5;
        if (Input.mousePosition.y < offset) offset = -offset;

        rectUi.anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y - offset, 0);
        */

        //follow ship implementation
        // special math is due to having gameObjects appear through distorted lense, but not the UI
        Vector2 pivot = new Vector2();
        Vector3 shipScreenPos = Camera.main.WorldToScreenPoint(ship.position);


        //if (shipScreenPos.x > rectUi.sizeDelta.x * 1.2f) // * 1.5 is due to fisheye lense distortion
        //{
        //    // info to ship's left side
        //    pivot.x = 0.5f;// 1.2f;
        //}
        //else
        //{
        //    // info to ship's right side
        //    pivot.x = 0.5f;// -0.2f;
        //}
        pivot.x = 0.5f;

        if (shipScreenPos.y < rectUi.sizeDelta.y * 1.2f)
        {
            // info to ship's lower side
            pivot.y = -0.5f
                - (shipScreenPos.y / Camera.main.pixelHeight)
                ;
        }
        else
        {
            // info to ship's upper side
            pivot.y = 2.2f
                - (shipScreenPos.y / Camera.main.pixelHeight)
                ;
        }

        rectUi.pivot = pivot;
        rectUi.anchoredPosition = shipScreenPos;
    }

    void setInfoWindowPointForLocation()
    {
        Vector2 pivot = new Vector2(0, 0f);
        Vector3 locScreenPos = Camera.main.WorldToScreenPoint(location.location.position);

        // -------
        pivot.x = 0.5f;

        if (locScreenPos.y < Camera.main.pixelHeight - rectUi.sizeDelta.y *2.0f)
        {
            // info to loc's upper side
            pivot.y = 0.3f
                - (locScreenPos.y / Camera.main.pixelHeight)
                ;
        }
        else
        {
            // info to loc's lower side 
            pivot.y = 2.2f
                - (locScreenPos.y / Camera.main.pixelHeight)
                ;
        }
        // --------

        rectUi.pivot = pivot;
        rectUi.anchoredPosition = locScreenPos;
    }
}
