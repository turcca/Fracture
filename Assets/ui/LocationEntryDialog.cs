using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationEntryDialog : MonoBehaviour
{
    public Text locationName;

    private string locationId;
    private bool needsUpdate = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (needsUpdate)
        {
            locationId = Game.universe.player.getLocationId();
            locationName.text = Game.universe.locations[locationId].name;
            needsUpdate = false;
        }
    }

    void OnDisable()
    {
        needsUpdate = true;
    }

    public void enterLocation()
    {
        Application.LoadLevel("locationScene");
    }
}
