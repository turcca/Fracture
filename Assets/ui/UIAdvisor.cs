using UnityEngine;
using System.Collections;

public class UIAdvisor : MonoBehaviour
{
    public delegate void AdvisorSelectedDelegate(Character.Job job);

    private Character.Job advisorJob = Character.Job.none;
    private Character character;
    private GameObject adviseFrame;
    AdvisorSelectedDelegate callback;

    void Start()
    {
        adviseFrame = transform.GetChild(0).gameObject;
        //NGUITools.SetActive(adviseFrame, false);
    }


    public void setup(Character.Job job, AdvisorSelectedDelegate d)
    {
        advisorJob = job;
        character = Root.game.player.getAdvisor(advisorJob);
        //gameObject.GetComponent<UITexture>().mainTexture =
        //    Game.PortraitManager.getPortraitTexture(character.getPortrait().id);
        callback = d;
    }

    void OnClick()
    {
        callback(advisorJob);
    }
}
