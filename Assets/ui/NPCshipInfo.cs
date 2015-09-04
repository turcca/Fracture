using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCshipInfo : MonoBehaviour 
{

    public Simulation.NPCShip ship { get; private set; }
    public Text shipInfo;

    Camera camera;
    RectTransform rectUi;

    bool needsUpdate;
    bool isShown;

    void Update()
    {
        if (ship.isVisible) updatePosition ();
    }

    public void setVisibility(Simulation.NPCShip shipInput, bool isVisible)
    {
        if (!camera) camera = Camera.main;
        if (!rectUi) rectUi = GetComponent<RectTransform>();
        ship = shipInput;
        loadShipInfo ();
        updatePosition();
    }

    void updatePosition()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(ship.tradeShip.getLineUIHalfwayPoint() /*shipInput.position*/); // input position 
        Vector2 halfPoint = new Vector2(screenPos.x, screenPos.y); 
        rectUi.anchoredPosition = halfPoint;
    }

    void loadShipInfo () 
    {
        shipInfo.text = getShipInfo();
	}

    string getShipInfo()
    {
        string rv = "";
        rv += "Captain: "+ship.captain;
        rv += "\n";
        rv += ship.isTradeShip ? "Trade Ship" : "Warship";
        rv += "\n";
        rv += "Cargo: \n";
        rv += ship.cargoToDebugString();
        return rv;
    }


}
