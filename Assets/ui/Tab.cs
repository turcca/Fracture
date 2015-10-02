using UnityEngine;
using System.Collections;

public class Tab : MonoBehaviour
{
    private GameObject selected;
    private GameObject notSelected;


    void Start()
    {
        init();

        if (gameObject.name == "LocationTab") { activate (); Debug.Log ("activating locationTab"); }
        else deactivate ();
    }

    void init()
    {
        if (!selected) selected = gameObject.transform.FindChild("Selected").gameObject;
        if (!notSelected) notSelected = gameObject.transform.FindChild("NotSelected").gameObject;
    }

    public void activate()
    {
        if (!selected  || !notSelected) init ();
        selected.SetActive(true); //Debug.Log (gameObject.name+": "+selected.activeSelf.ToString());
        notSelected.SetActive(false);
    }

    public void deactivate()
    {
        if (!selected  || !notSelected) init ();
        selected.SetActive(false);
        notSelected.SetActive(true);
    }
}
