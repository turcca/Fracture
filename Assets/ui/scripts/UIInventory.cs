using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    GameObject grid;
    void Start()
    {
        grid = gameObject.transform.FindChild("commodityGrid").gameObject;
        populateCommodities();
    }

    public void populateCommodities()
    {
        // destroy old entries
        for (int i = grid.transform.childCount - 1; i >= 0; --i)
        {
            DestroyImmediate(grid.transform.GetChild(i).gameObject);
        }

        string lastCategory = "";
        int order = 0;
        foreach (KeyValuePair<string, CommodityInfo> entry in Economy.commodityInfo)
        {
            // check category
            if (entry.Value.category != lastCategory)
            {
                GameObject categoryLine = Resources.Load<GameObject>("ui/prefabs/ui_commodity_category_line");
                categoryLine.GetComponent<UILabel>().text = entry.Value.category;
                categoryLine.name = order.ToString() + "_category";
                ++order;
                NGUITools.AddChild(grid, categoryLine);
            }
            lastCategory = entry.Value.category;
            // add commodity
            GameObject commodityLine = Resources.Load<GameObject>("ui/prefabs/ui_inv_commodity");
            commodityLine.GetComponent<UiInventoryCommodity>().trackCommodity(entry.Key);
            commodityLine.name = order.ToString() + "_commodity";
            ++order;
            NGUITools.AddChild(grid, commodityLine);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }
}
