using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiplomacyPage : MonoBehaviour
{
    public LocationSceneState scene;
    public GameObject grid;
    public GameObject factionListItem;

    private Location location;

    // Use this for initialization
    void Start()
    {
        location = Game.universe.locations[scene.trackedLocation];
        populateFactionPanels();
    }

    private void populateFactionPanels()
    {
        foreach (var entry in location.faction.control)
        {
            if (entry.Value > 0.01)
            {
                addMenuItem(entry.Key, entry.Value);
            }
        }
    }

    private void addMenuItem(string faction, float support)
    {
        GameObject factionPanel = (GameObject)GameObject.Instantiate(factionListItem);
        factionPanel.GetComponent<FactionMeetPanel>().setup(scene.trackedLocation, faction,
            new FactionMeetPanel.FactionSelectedDelegate(eventFactionPicked));
        factionPanel.transform.parent = grid.transform;
    }

    public void eventFactionPicked(string faction)
    {
        scene.startDiplomacyEvent(faction);
    }
}
