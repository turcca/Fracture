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
    }


    public void setup(Character.Job job, AdvisorSelectedDelegate d)
    {
        advisorJob = job;
        character = Root.game.player.getAdvisor(advisorJob);

        callback = d;
    }
    /*
    void OnClick()
    {
        //Debug.Log("EventAdvisor [delegate]: " + advisorJob.ToString());
        callback(advisorJob);
    }
    */
}
