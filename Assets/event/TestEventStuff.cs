using UnityEngine;
using System.Collections;

public class TestEventStuff : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        EventManager mgr = new EventManager();
        mgr.handleEvent(mgr.pickEvent());
        Tools.debug("ALERT - Test events enabled in eventScene!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
