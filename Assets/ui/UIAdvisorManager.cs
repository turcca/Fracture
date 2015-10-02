using UnityEngine;
using System.Collections;

public class UIAdvisorManager : MonoBehaviour
{
    private GameObject advisorPrefab;
    private EventUI eventUI;

    void Awake()
    {
        advisorPrefab = Resources.Load<GameObject>("ui/prefabs/ui_advisor_button");
        eventUI = GameObject.Find("eventUI").GetComponent<EventUI>();
    }
    
    void Start()
    {
    }

    public void eventAdvisorSelected(Character.Job job)
    {
        eventUI.setAdvisor(job);
    }
}
