using UnityEngine;
using System.Collections;

public class PlanetId : MonoBehaviour
{
    public string Id = Tools.STRING_NOT_ASSIGNED;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        SimulationUI.selectedPlanet = Id;
    }
}
