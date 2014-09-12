using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventAdvisorManager : MonoBehaviour
{
    public EventUI eventUI;
    public List<EventAdvisor> advisors;

    private Character.Job lastSelectedJob;

    void Start()
    {
    }
    
    public void eventAdvisorSelected(GameObject advisorNode)
    {
        Character.Job job = advisorNode.GetComponent<EventAdvisor>().advisorJob;
        string advice = eventUI.setAdvisor(job);

        foreach (EventAdvisor advisor in advisors)
        {
            if (advisor.advisorJob == job && job != lastSelectedJob)
            {
                advisor.showAdvice(advice);
            }
            else
            {
                advisor.hideAdvice();
            }
        }

        if (lastSelectedJob == job)
        {
            lastSelectedJob = Character.Job.none;
        }
        else
        {
            lastSelectedJob = job;
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
