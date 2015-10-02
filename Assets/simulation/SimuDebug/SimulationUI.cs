using UnityEngine;
using System.Collections;

public class SimulationUI : MonoBehaviour
{
    static public string selectedPlanet = "";

    static SimulationTestSetup simulationTicker;

    // Use this for initialization
    void Start()
    {
        simulationTicker = gameObject.GetComponent<SimulationTestSetup>();
        if (simulationTicker == null) Debug.LogWarning("SimulationTestSetup missing on '" + gameObject.name + "'");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (simulationTicker) GUI.Label(new Rect(Screen.width - 80, 0, 80, 50), "Day: "+ simulationTicker.getElapsedDays().ToString());
        GUI.Label(new Rect(Screen.width - 80, 50, 80, 50), Root.game.shipUsageToString());

        if (selectedPlanet != "")
        {
            string planetInfo = "";
            planetInfo += Root.game.locations[selectedPlanet].toDebugString();
            GUI.Label(new Rect(0, 0, 400, 900), planetInfo);
            //if (GUI.Button(new Rect(300, 0, 100, 100), "go"))
            //{
            //    Application.LoadLevel("locationScene");
            //}
        }
        
    }
}
