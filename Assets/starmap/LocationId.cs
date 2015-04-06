using UnityEngine;
using System.Collections;

public class LocationId : MonoBehaviour
{
    public string id { get; private set; }

    void Awake()
    {
        id = gameObject.name;
    }

    void OnMouseDown()
    {
        SimulationUI.selectedPlanet = gameObject.name;
    }

    public string getId()
    {
        return id;
    }
}
