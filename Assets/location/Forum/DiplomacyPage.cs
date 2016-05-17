using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DiplomacyPage : MonoBehaviour
{
    public LocationSceneState scene;
    public GridLayoutGroup grid;
    public GameObject factionListItem;

    private Location location;

    // Use this for initialization
    void Start()
    {
        location = Root.game.locations[scene.trackedLocation];
        populateFactionPanels();
    }

    private void populateFactionPanels()
    {
        List<KeyValuePair<Faction.FactionID, float>> sortedControl = new List<KeyValuePair<Faction.FactionID, float>>();

        foreach (var entry in location.features.factionCtrl)
        {
            if (entry.Value > 0.008f)
            {
                sortedControl.Add(new KeyValuePair<Faction.FactionID, float>(entry.Key, entry.Value));
            }
        }
        if (sortedControl.Count > 0)
        {
            // Sort list by values
            sortedControl.Sort(
                delegate(KeyValuePair<Faction.FactionID, float> firstPair,
                     KeyValuePair<Faction.FactionID, float> nextPair)
                {
                return nextPair.Value.CompareTo(firstPair.Value);
                }
            );

            foreach (var item in sortedControl)
            {
                addMenuItem(item.Key, item.Value);
            }
            // align one-colum grid vs two-column grid when 6+ factionListItems
            grid.childAlignment = (sortedControl.Count <= 5) ? TextAnchor.UpperCenter : TextAnchor.UpperLeft;
        }
    }

    private void addMenuItem(Faction.FactionID faction, float support)
    {
        GameObject factionPanel = (GameObject)GameObject.Instantiate(factionListItem);
        factionPanel.GetComponent<FactionMeetPanel>().setup(scene.trackedLocation, faction,
            new FactionMeetPanel.FactionSelectedDelegate(eventFactionPicked));
        factionPanel.transform.SetParent(grid.gameObject.transform);
    }

    public void eventFactionPicked(string faction)
    {
        scene.startDiplomacyEvent(faction);
    }
}
