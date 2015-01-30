using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdvisorManager : MonoBehaviour
{
    public EventUI eventUI;
    public List<EventAdvisor> advisors;

    private Character.Job lastSelectedJob;

    public void advisorSelected(GameObject advisorNode)
    {
        Character.Job job = advisorNode.GetComponent<EventAdvisor>().advisorJob;

        string advice = "";
        if (Game.state == Game.State.Event)
        {
            advice = eventUI.setAdvisor(job);
        }
        else
        {
            ///@todo get some advice for different situations
            advice = "Some general advice.";
        }

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

    internal void hideAdvices()
    {
        foreach (EventAdvisor advisor in advisors)
        {
            advisor.hideAdvice();
        }
    }

    internal static AdvisorManager get()
    {
        return GameObject.Find("Advisors").GetComponent<AdvisorManager>();
    }
}
