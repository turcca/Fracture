using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdvisorManager : MonoBehaviour
{
    public EventUI eventUI;
    public List<EventAdvisor> advisors;

    private Character.Job lastSelectedJob;

    void Start()
    {
    }

    public void advisorSelected(GameObject advisorNode)
    {
        Character.Job job = advisorNode.GetComponent<EventAdvisor>().advisorJob;

        string advice = "";

        if (GameState.isState(GameState.State.Event))
        {
            if (eventUI == null) eventUI = GameObject.Find("SideWindow").GetComponent<EventUI>();
            if (eventUI != null)
            {
                advice = eventUI.setAdvisor(job);
            }
            else Debug.LogWarning ("event state couldn't locate EventUI -script in SideWindow");
        }
        else if (GameState.isState(GameState.State.Location))
        {
            //advice = currentEvent.getAdvice(job).text
            advice = GameObject.Find("MainContent").GetComponent<EventUI>().setAdvisor(job);
            //Debug.Log ("advice: "+advice);
        }
        else
        {
            Debug.Log ("GameState.getState() = "+GameState.getState().ToString() );
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

    internal void refreshAdvisors()
    {
        foreach (EventAdvisor advisor in advisors)
        {
            advisor.setup();
        }
    }
}
