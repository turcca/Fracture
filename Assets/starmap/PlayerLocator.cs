using UnityEngine;
using System.Collections;

public class PlayerLocator : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.position = Game.getUniverse().player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Game.getUniverse().player.position = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        LocationId locId = other.gameObject.GetComponent<LocationId>();
        if (locId)
        {
            Game.getUniverse().player.setLocationId(locId.Id);
            Tools.debug("Entering " + locId.Id.ToString());
        }
    }

    void OnTriggerExit(Collider other)
    {
        LocationId locId = other.gameObject.GetComponent<LocationId>();
        if (locId)
        {
            Game.getUniverse().player.setLocationId("");
            Tools.debug("Leaving " + locId.Id.ToString());
        }
    }
}
