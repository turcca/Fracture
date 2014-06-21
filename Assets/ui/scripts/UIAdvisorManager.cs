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
        GameObject root = gameObject;

        int i = 0;
        foreach (string position in Character.getAdvisorPositionNames())
        {
            ++i;
            GameObject go = NGUITools.AddChild(gameObject, advisorPrefab);
            go.GetComponent<UIKeyBinding>().keyCode = Tools.getKeyFunction(i);
            go.GetComponent<UIAdvisor>().setup(position, new UIAdvisor.AdvisorSelectedDelegate(advisorSelected));
        }
        gameObject.GetComponent<UIGrid>().Reposition();
    }

    public void advisorSelected(string pos)
    {
        eventUI.setAdvisor(pos);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
