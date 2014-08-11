using UnityEngine;
using System.Collections;

public class EventAdvisor : MonoBehaviour
{
    public delegate void AdvisorSelectedDelegate(Character.Job job);
    
    private Character.Job advisorJob = Character.Job.none;
    private Character character;
    private GameObject adviceFrame;
    AdvisorSelectedDelegate callback;
    
    void Start()
    {
        adviceFrame = transform.GetChild(0).gameObject;
        //NGUITools.SetActive(adviceFrame, false);
    }
    
    
    public void setup(Character.Job job, AdvisorSelectedDelegate d)
    {
        advisorJob = job;
        character = Game.getUniverse().player.getCharacter(advisorJob);
//        gameObject.GetComponent<UITexture>().mainTexture =
//            Game.PortraitManager.getPortraitTexture(character.getPortrait().id);
        callback = d;
    }

    public void showAdvice(string advice)
    {
//        NGUITools.SetActive(adviceFrame, true);
//        adviceFrame.GetComponent<UILabel>().text = advice;
    }

    public void hideAdvice()
    {
//        NGUITools.SetActive(adviceFrame, false);
    }
    
    void OnClick()
    {
        callback(advisorJob);
    }
}
