using UnityEngine;
using System.Collections;

public class TabbedPanel : MonoBehaviour
{
    public Tab tab;

    void Start()
    {
        //tab.deactivate();
    }

    void OnDisable()
    {
        //Debug.Log("disable: " + gameObject.name);
        tab.deactivate();
    }

    void OnEnable()
    {
        //Debug.Log("enable: " + gameObject.name);
        tab.activate();
    }

}
