﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocationPage : MonoBehaviour 
{
    Location location;

    public Text locationName;
    public Text systemName;

    public Text description1;
    public Text description2;



	// Use this for initialization
	void Start () 
    {
        loadLocationInfo();
	}

    public void loadLocationInfo()
    {
        location = Root.game.player.getLocation();
        if (location == null) Debug.LogError ("Location not found");

        else
        {
            if (locationName != null) locationName.text = location.features.name;
            if (systemName != null) systemName.text = location.features.subsector;

            if (description1 != null) description1.text = location.features.description1;
            if (description2 != null) description2.text = location.features.description2;
        }
    }
	

}
