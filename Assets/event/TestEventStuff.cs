using UnityEngine;
using System.Collections;

public class TestEventStuff : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        EventManager mgr = new EventManager();
        mgr.handleEvent(mgr.pickEvent());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
