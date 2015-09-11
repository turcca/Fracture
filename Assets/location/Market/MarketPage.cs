using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MarketPage : MonoBehaviour
{
    public LocationSceneState state;
    public GameObject grid;

    public Text playerCargo;
    public Text playerCredit;

    public List<Data.TradeItem> playerTradeList = new List<Data.TradeItem>();
    public List<Data.TradeItem> locationTradeList = new List<Data.TradeItem>();


    void Start()
    {
        playerTradeList = Root.game.locations[state.trackedLocation].getPlayerTradeList();
        locationTradeList = Root.game.locations[state.trackedLocation].getLocationTradeList();
        
        populateCommodities();
        updateCargo();
        updateCredits();
    }

    private void populateCommodities()
    {
        int order = 0;
        //foreach (KeyValuePair<string, CommodityInfo> entry in Economy.commodityInfo)
        grid.transform.DetachChildren();
        foreach(Data.TradeItem item in locationTradeList)
        {
            // add commodity ui gameobjects
            GameObject commodityPrefab = Resources.Load<GameObject>("location/ui/CommodityMarketItem");
            GameObject commodity = (GameObject)GameObject.Instantiate(commodityPrefab);
            commodity.GetComponent<MarketCommodity>().trackLocation(state, item.subType, this);
            commodity.name = order.ToString() + "_commodity";
            commodity.transform.SetParent(grid.transform);
            ++order;
        }
    }

    public void updateCargo()
    {
        if (playerCargo != null)
        {
            playerCargo.text = "Cargo: "+Root.game.player.cargo.getUsedCargoSpace()+" / "+Root.game.player.cargo.maxCargoSpace;
        }
        else Debug.LogError ("ERROR: playerCargo not set in Editor");
    }
    public void updateCredits()
    {
        if (playerCargo != null)
        {
            playerCredit.text = "Credits: "+Root.game.player.cargo.credits;
        }
        else Debug.LogError ("ERROR: playerCargo not set in Editor");
    }
}
