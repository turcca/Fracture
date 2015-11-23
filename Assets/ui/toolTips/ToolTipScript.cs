using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ToolTipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public Character.Job job { get; private set; }
    public Character.Stat stat { get; private set; }
    public string toolTip = null;

    public ToolTipScript(string tip/*, Character.Stat? characterStat = null, Character.Job? characterJob = null*/)
    {
        toolTip = tip;
        //if (characterJob != null) job = (Character.Job)characterJob;
        //if (characterStat != null) stat = (Character.Stat)characterStat;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipLibrary.show(this.gameObject);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipLibrary.hide(this.gameObject);
    }
}
