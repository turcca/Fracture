using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarketPage : MonoBehaviour
{
    public LocationSceneState state;
    public GameObject grid;

    void Start()
    {
        populateCommodities();
    }

    private void populateCommodities()
    {
        int order = 0;
        //foreach (KeyValuePair<string, CommodityInfo> entry in Economy.commodityInfo)
        foreach(var pair in state.tradeList.commodities)
        {
            // add commodity
            GameObject commodityPrefab = Resources.Load<GameObject>("location/ui/CommodityMarketItem");
            GameObject commodity = (GameObject)GameObject.Instantiate(commodityPrefab);
            commodity.GetComponent<MarketCommodity>().trackLocation(state, pair.Key);
            commodity.name = order.ToString() + "_commodity";
            commodity.transform.SetParent(grid.transform);
            ++order;
        }
    }

    void Update()
    {

    }
}
