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
        int i = 0;
        foreach (Character.Job job in Character.getAdvisorAssignmentNames())
        {
            ++i;
            //GameObject go = NGUITools.AddChild(gameObject, advisorPrefab);
            //go.GetComponent<UIKeyBinding>().keyCode = Tools.getKeyFunction(i);
            //go.GetComponent<UIAdvisor>().setup(job, new UIAdvisor.AdvisorSelectedDelegate(eventAdvisorSelected));
        }
        //gameObject.GetComponent<UIGrid>().Reposition();
    }

    public void eventAdvisorSelected(Character.Job job)
    {
        eventUI.setAdvisor(job);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
