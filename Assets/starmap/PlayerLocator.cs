using UnityEngine;
using System;
using System.Collections;

public class PlayerLocator : MonoBehaviour
{
    private GameMenuSystem ui;

    // Use this for initialization
    void Start()
    {
        transform.position = Game.universe.player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Game.universe.player.position = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        LocationId locId = other.gameObject.GetComponent<LocationId>();
        if (locId)
        {
            Game.universe.player.setLocationId(locId.getId());
            if (dialogReady())
            {
                ui.showLocationEntryDialog();
            }
        }
    }

    private bool dialogReady()
    {
        if (ui == null)
        {
            try
            {
                ui = GameObject.Find("GameCanvas").GetComponent<GameMenuSystem>();
            }
            catch (NullReferenceException e)
            {
                return false;
            }
        }
        return true;
    }

    void OnTriggerExit(Collider other)
    {
        LocationId locId = other.gameObject.GetComponent<LocationId>();
        if (locId)
        {
            if (dialogReady())
            {
                ui.hideLocationEntryDialog();
            }
        }
    }
}
