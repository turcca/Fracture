using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipStatBonusUI : MonoBehaviour
{
    public Text label;
    public Text value;
    public Image valueBg;
    public Image bonusBg;

    GridLayoutGroup grid;
    RectTransform rect;

    public void initialize (ShipBonusItem shipBonusItem)
    {
        label.text = shipBonusItem.text;
        value.text = valueToString(shipBonusItem.value);
        if (shipBonusItem.value < 0)
        {
            // invert bg/text for value if negative
            value.color = new Color(1, 1, 1, 0.6f);
            valueBg.color = new Color(0, 0, 0, 0.4f);
        }
        bonusBg.color = shipBonusItem.color;
    }
    public void initialize(ShipBonusCathegory cathegory, int value)
    {
        // for main cathegory - eg. "CREW"
        this.value.text = valueToString(value);
        if (value < 0)
        {
            // invert bg/text for value if negative
            this.value.color = new Color(1, 1, 1, 0.6f);
            this.valueBg.color = new Color(0, 0, 0, 0.7f);
        }
        // add tooltip to parent
        ToolTipScript tip = transform.parent.gameObject.AddComponent<ToolTipScript>();
        tip.toolTip = ShipBonusesStats.getBonusDescription(cathegory, value);
    }
    public GridLayoutGroup getGrid()
    {
        if (grid == null) grid = this.gameObject.GetComponent<GridLayoutGroup>();
        if (grid != null) return grid;
        else { Debug.LogError("No GridLayoutGroup component on gameObject"); return null; }
    }
    public RectTransform getRectTransform()
    {
        if (rect == null) rect = this.gameObject.GetComponent<RectTransform>();
        if (rect != null) return rect;
        else { Debug.LogError("No RectTransform component on gameObject: "+this.gameObject.name); return null; }
    }

    /// <summary>
    /// format an in to a string, where you have a "+" in front of a positive number
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static string valueToString(int i)
    {
        // 3 --> "+3"
        string rs = "";
        //if (i < 0) rs += "-";
        if (i > 0) rs += "+";
        rs += i.ToString();
        return rs;
    }

}
