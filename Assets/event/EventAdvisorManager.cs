using UnityEngine;
using System.Collections;

public class EventAdvisorManager : MonoBehaviour
{
    private EventUI eventUI;
    
    void Awake()
    {
        eventUI = GameObject.Find("eventUI").GetComponent<EventUI>();
    }
    
    void Start()
    {
        int i = 0;
        foreach (Character.Job job in Character.getAdvisorAssignmentNames())
        {
            ++i;
            GameObject go = transform.FindChild("advisor_" + i.ToString()).gameObject;
            if (go != null)
            {
                go.GetComponent<EventAdvisor>().setup(job, new EventAdvisor.AdvisorSelectedDelegate(eventAdvisorSelected));
            }
        }
    }
    
    public void eventAdvisorSelected(Character.Job job)
    {
        string advice = eventUI.setAdvisor(job);

        int i = 0;
        foreach (Character.Job j in Character.getAdvisorAssignmentNames())
        {
            ++i;
            GameObject go = transform.FindChild("advisor_" + i.ToString()).gameObject;
            if (go != null)
            {
                if (j == job)
                {
                    go.GetComponent<EventAdvisor>().showAdvice(advice);
                }
                else
                {
                    go.GetComponent<EventAdvisor>().hideAdvice();
                }

            }
        }

    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
