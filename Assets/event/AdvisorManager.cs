using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdvisorManager : MonoBehaviour
{
    public EventUI eventUI;
    public List<EventAdvisor> advisors;

    private Character.Job lastSelectedJob;
    private bool showStatsNext = false;

    public void setup()
    {
        // link characters to event edvisors
        foreach (Character character in Root.game.player.getCharacters())
        {
            foreach (EventAdvisor advisor in advisors)
            {
                if (advisor.advisorJob == character.assignment)
                {
                    advisor.setCharacter(character);
                    advisor.setup();
                    break;
                }
            }
        }
        // todo: remove non-assigned advisors? Or populate EventAdvisors through other means than prefab?
    }

    public void advisorSelected(GameObject advisorNode)
    {
        Character.Job job = advisorNode.GetComponent<EventAdvisor>().advisorJob;

        string advice = "";

        // on Events
        if (GameState.isState(GameState.State.Event))
        {
            if (eventUI == null) eventUI = GameObject.Find("SideWindow").GetComponent<EventUI>();
            if (eventUI != null)
            {
                advice = eventUI.setAdvisor(job);
            }
            else Debug.LogWarning ("event state couldn't locate EventUI -script in 'SideWindow'");
        }
        // on Locations
        else if (GameState.isState(GameState.State.Location))
        {
            if (eventUI == null) eventUI = GameObject.Find("MainContent").GetComponent<EventUI>();
                        if (eventUI != null)
            {
                advice = eventUI.setAdvisor(job);
            }
            else Debug.LogWarning ("Location state couldn't locate EventUI -script in 'MainContent'");
        }
        else
        {
            Debug.Log ("GameState.getState() = "+GameState.getState().ToString() +"[TODO advice]");
            ///@todo get some advice for different situations. Combat?
            advice = "Some general advice.";
        }

        // show advice
        foreach (EventAdvisor advisor in advisors)
        {
            // handle the clicked character
            if (advisor.advisorJob == job)
            {
                // new click, load advice
                if (job != lastSelectedJob)
                {
                    advisor.showAdvice(advice);
                    showStatsNext = true;
                }
                // click on same advisor, load stats/advice
                else
                {
                    if (showStatsNext)
                    {
                        advisor.showStats();
                        showStatsNext = false;
                    }
                    else
                    {
                        advisor.showAdvice(advice);
                        showStatsNext = true;
                    }
                }
            }
            // handle all not-clicked characters
            else
            {
                advisor.hideAdvice();
            }
        }
        lastSelectedJob = job;
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
