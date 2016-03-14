using UnityEngine;
using System;
using System.Collections;

public class PlayerLocator : MonoBehaviour
{
    private GameMenuSystem ui;

    // Use this for initialization
    void Start()
    {
        transform.position = Root.game.player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Root.game.player.position = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger");
        LocationId locId = other.gameObject.GetComponent<LocationId>();
        if (locId)
        {
            Root.game.player.setLocationId(locId.getId());

            if (dialogReady())
            {
                //Debug.Log("dialog");
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
            catch (NullReferenceException)
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
            Root.game.player.setLocationId("");
        }
    }
}
