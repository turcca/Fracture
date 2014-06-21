using UnityEngine;
using System.Collections;

public class LocationStateBtn : MonoBehaviour
{
    /*
    public locationState state;
    public string faction;
    public float factionCtrl;
    public int officeLevel;
    public int btnNumber;

    public UISprite sprite;
    public UIWidget widget;

    void OnClick()
    {
        LocationUI.Instance.loadState(state, faction, null);
    }


    public void loadStateAndFaction(locationState State, int N, string Faction = null, float Val = 0, int OfficeLevel = 0)
    {
        // set state and faction
        state = State;

        if (State == locationState.Appointment)
        {
            if (Faction != null && !WorldState.faction.ContainsKey(Faction)) { Debug.LogError("ERROR: invalid faction input: " + Faction); return; }

            btnNumber = N;
            faction = Faction;
            factionCtrl = Val;
            officeLevel = OfficeLevel;

            loadSprite();
        }
    }

    void loadSprite()
    {
        if (officeLevel > 0)
        {
            // no re-sizing - this is only loaded for appointments which is fixed in the prefab
            //NGUIMath.ResizeWidget(widget, UIWidget.Pivot.Center, 1f, 1f, 120, 30);

            sprite.spriteName = "appointment_" + officeLevel;
        }
    }
     */
}
