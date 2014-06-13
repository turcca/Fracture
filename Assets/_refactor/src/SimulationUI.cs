using UnityEngine;
using System.Collections;

public class SimulationUI : MonoBehaviour
{
    static public string selectedPlanet = "";

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (selectedPlanet == "") return;

        string planetInfo = "";
        Universe universe = GameObject.Find("GameLoop").GetComponent<GameLoop>().universe;

        planetInfo += universe.locations[selectedPlanet].toDebugString();

        GUI.TextArea(new Rect(0, 0, 300, 900), planetInfo);
    }
}
