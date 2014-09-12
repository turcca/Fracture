using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventAdvisor : MonoBehaviour
{
    public delegate void AdvisorSelectedDelegate(Character.Job job);
    public GameObject advicePanel;
    public Character.Job advisorJob;
    public Text adviceText;
    
    private Character character;
    AdvisorSelectedDelegate callback;
    
    void Start()
    {
        advicePanel.SetActive(false);
    }
    
    
    public void setup(Character.Job job, AdvisorSelectedDelegate d)
    {
        character = Game.universe.player.getCharacter(advisorJob);
        advicePanel.SetActive(false);
        callback = d;
    }

    public void showAdvice(string advice)
    {
        adviceText.text = advice;
        advicePanel.SetActive(true);
    }

    public void hideAdvice()
    {
        advicePanel.SetActive(false);
    }
    
    void OnClick()
    {
        callback(advisorJob);
    }
}
