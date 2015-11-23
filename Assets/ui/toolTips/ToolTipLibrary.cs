using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class ToolTipLibrary
{
    static GameObject toolTipObj;
    static public Text toolTip { get; private set; }


    static ToolTipLibrary()
    {
        if (toolTipObj = null)
        {
            format();
            //toolTipObj.gameObject.SetActive(false);
        }
    }
    public static void format()
    {
        toolTipObj = GameObject.Find("toolTip");
        if (toolTipObj == null)
        {
            // instantiate new toolTip
            toolTipObj = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("ui/prefabs/toolTip"));
            if (toolTipObj == null) Debug.LogError("No 'toolTip' UI -object in resources");
            else
            {
                toolTipObj.name = "toolTip";
                toolTip = toolTipObj.GetComponentInChildren<Text>();
                if (GameState.isState(GameState.State.Location)) toolTipObj.transform.SetParent(GameObject.Find("LocationCanvas").transform);
                else if (GameState.isState(GameState.State.Starmap) || GameState.isState(GameState.State.Event)) toolTipObj.transform.SetParent(GameObject.Find("SideWindow").transform);
                if (toolTipObj.transform.parent == null) Debug.LogError("TODO: toolTip not under UI canvas.\n GameState.state = " + GameState.getState().ToString());
            }
        }
        else toolTip = toolTipObj.GetComponentInChildren<Text>();
        if (toolTip == null) Debug.LogWarning("No UI.Text component found under UI -object 'toolTip'");
        // hide for format
        hide(toolTipObj);
    }

    /// <summary>
    /// Hide tooltip
    /// </summary>
    /// <param name="sourceObject"></param>
    public static void hide(GameObject sourceObject)
    {
        if (toolTipObj == null) format();
        if (toolTip != null)
        {
            // disable toolTip
            toolTipObj.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Show toolTip
    /// </summary>
    /// <param name="sourceObject"></param>
    public static void show(GameObject sourceObject)
    {
        if (toolTipObj == null) format();
        if (toolTip != null)
        {
            // relocate toolTip object
            toolTipObj.transform.position = sourceObject.transform.position;
            // find components that should evoke tooltips
            toolTip.text = getToolTip(sourceObject);

            // enable toolTip - only if there is one
            if (toolTip.text != "") toolTipObj.gameObject.SetActive(true);
        }
    }



    // ----------------------------------------------------------------------------
    static string getToolTip(GameObject sourceObject)
    {
        string rv = "No assigned toolTip";
        //if (sourceObject == null) { Debug.LogError("ERROR, sourceObject == null"); return "ERROR (ToolTipLibrary)"; }

        // aquire GameObject component to understand what toolTip needs to be shown

        // location/trade/commodity -image
        //if (sourceObject.name == "Cathegory")

        // skill
        ToolTipScript tip = sourceObject.GetComponent<ToolTipScript>();
        if (tip != null) rv = tip.toolTip;

        //
        else if (true) rv = "";

        return rv;
    }
}
