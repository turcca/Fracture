﻿using UnityEngine;
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
        planetInfo += Root.game.locations[selectedPlanet].toDebugString();
        GUI.Label(new Rect(0, 0, 400, 900), planetInfo);
        //if (GUI.Button(new Rect(300, 0, 100, 100), "go"))
        //{
        //    Application.LoadLevel("locationScene");
        //}

    }
}
