using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventChoicesBtn : MonoBehaviour
{
    public delegate void ChoiceDelegate(int choice);

    public int choice = 0;
    public ChoiceDelegate callback;
    public Text choiceNumber;
    public Text choiceTxt;

    public void click()
    {
        callback(choice);
    }

    public void recommend(bool on)
    {
        if (on)
        {
            choiceTxt.color = new Color32(120, 150, 255, 230);
        }
        else
        {
            choiceTxt.color = new Color32(255, 255, 255, 128);
        }
    }
}


