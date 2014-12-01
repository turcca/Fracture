using UnityEngine;
using System.Collections;

public class LocationId : MonoBehaviour
{
    void OnMouseDown()
    {
        SimulationUI.selectedPlanet = gameObject.name;
    }

    public string getId()
    {
        return gameObject.name;
    }
}
