﻿using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void msgButtonStart()
    {
        // do loading stuff
        UnityEngine.SceneManagement.SceneManager.LoadScene("starmapScene");
    }
    public void msgButtonExit()
    {
        Application.Quit();
    }
}
