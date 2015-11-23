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
            locationId = Root.game.player.getLocationId();
            locationName.text = Root.game.locations[locationId].name;
            needsUpdate = false;
        }
    }

    void OnDisable()
    {
        needsUpdate = true;
    }

    public void enterLocation()
    {
        if (GameState.isState(GameState.State.Starmap))
        {
            GameState.requestState(GameState.State.Location);
            Application.LoadLevel("locationScene");
        }
    }
}
