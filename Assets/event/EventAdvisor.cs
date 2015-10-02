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
        //setup();
    }

    public void setup()
    {
        // auto-select captain
        if (advisorJob == Character.Job.captain)
        {
            this.gameObject.transform.parent.gameObject.GetComponent<AdvisorManager>().advisorSelected(this.gameObject);
            advicePanel.SetActive(true);
        }
        else advicePanel.SetActive(false);
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

}
