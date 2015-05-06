using UnityEngine;
using System.Collections;

public class TabbedPanel : MonoBehaviour
{
    public Tab tab;

    void Start()
    {
        tab.deactivate();
    }

    void OnDisable()
    {
        tab.deactivate();
    }

    void OnEnable()
    {
        tab.activate();
    }

}
