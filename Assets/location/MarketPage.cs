using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarketPage : MonoBehaviour
{
    public LocationSceneState state;

    // Use this for initialization
    void Start()
    {
        populateCommodities();
    }

    private void populateCommodities()
    {
        string lastCategory = "";
        int order = 0;
        foreach (KeyValuePair<string, CommodityInfo> entry in Economy.commodityInfo)
        {
            // check category
            if (entry.Value.category != lastCategory)
            {
                //GameObject categoryLine = Resources.Load<GameObject>("location/ui/CommodityMarketItem");
                //categoryLine.GetComponent<UILabel>().text = entry.Value.category;
                //categoryLine.name = order.ToString() + "_category";
                //++order;
                //NGUITools.AddChild(grid, categoryLine);
            }
            lastCategory = entry.Value.category;
            // add commodity
            GameObject commodityPrefab = Resources.Load<GameObject>("location/ui/CommodityMarketItem");
            GameObject commodity = (GameObject)GameObject.Instantiate(commodityPrefab);
            commodity.GetComponent<MarketCommodity>().trackLocation(state.trackedLocation, entry.Key);
            commodity.name = order.ToString() + "_commodity";
            commodity.transform.parent = gameObject.transform;
            ++order;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
