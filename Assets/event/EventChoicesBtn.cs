﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventChoicesBtn : MonoBehaviour
{
    public delegate void ChoiceDelegate(int choice);

    public int choice = 0;
    public ChoiceDelegate callback;
    public Text choiceTxt;

    public void click()
    {
        callback(choice);
    }

    public void recommend(bool on)
    {
        if (on)
        {
            choiceTxt.color = new Color(0.0F, 0.4F, 0.7F, 1F);
        }
        else
        {
            choiceTxt.color = new Color(0.0F, 0.0F, 0.0F, 1F);
        }
    }
}


