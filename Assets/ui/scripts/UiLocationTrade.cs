﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiLocationTrade : MonoBehaviour
{
    GameObject grid;
    // Use this for initialization
    void Start()
    {
        grid = gameObject.transform.FindChild("commodityGrid").gameObject;
        populateCommodities();
    }

    private void populateCommodities()
    {
        // destroy old entries
        for (int i = grid.transform.childCount-1; i >= 0; --i)
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
            GameObject commodityLine = Resources.Load<GameObject>("ui/prefabs/ui_commodity_line");
            commodityLine.GetComponent<UiLocationCommodity>().trackCommodity("a", entry.Key);
            commodityLine.name = order.ToString() + "_commodity";
            ++order;
            NGUITools.AddChild(grid, commodityLine);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void show()
    {
        NGUITools.SetActive(gameObject, true);
    }
    public void hide()
    {
        NGUITools.SetActive(gameObject, false);
    }
}
