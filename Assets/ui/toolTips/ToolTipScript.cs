using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ToolTipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public Character.Job job { get; private set; }
    public Character.Stat stat { get; private set; }
    public string toolTip = null;

    //public bool isOnDelay = true;
    //public float toolTipDelay = 0.5f; // todo: universal tooltip timer and on/off
    bool isHovering = false;
    float toolTipTimer = 0f; 
    Vector3 oldMouseScreenPos = new Vector3(0,0,0);

    public ToolTipScript(string tip/*, Character.Stat? characterStat = null, Character.Job? characterJob = null*/)
    {
        toolTip = tip;
        //if (characterJob != null) job = (Character.Job)characterJob;
        //if (characterStat != null) stat = (Character.Stat)characterStat;
    }
    void Update()
    {
        if (Root.game.gameSettings.toolTipsOn && isHovering)
        {
            if (Vector3.Distance (oldMouseScreenPos, Input.mousePosition) < 1f)
            {
                if (toolTipTimer <= 0f)
                {
                    ToolTipLibrary.show(this.gameObject);
                }
                else toolTipTimer -= Time.deltaTime;
            }
            else
            {
                toolTipTimer = Root.game.gameSettings.toolTipDelay;
                oldMouseScreenPos = Input.mousePosition;
                ToolTipLibrary.hide(this.gameObject);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Root.game.gameSettings.toolTipsOn)
        {
            isHovering = true;
            toolTipTimer = Root.game.gameSettings.toolTipDelay;
        }
        else ToolTipLibrary.show(this.gameObject);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (Root.game.gameSettings.toolTipsOn)
        {
            isHovering = false;
        }
        ToolTipLibrary.hide(this.gameObject);
    }
}
