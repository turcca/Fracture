using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MarketCommodity : MonoBehaviour
{
    public Data.Resource.SubType trackedCommodity;
    public LocationSceneState state;

    public Text commodityName;
    public Text price;
    public Text cargoAmount;
    public Text storeAmount;

    public Image hilight;
    
    private Text playerCredits;
    private Text playerCargo;
    private Image lineGraph;
    private Image lineGraphHor;
    //private string trackedLocation = "not defined";


    // Use this for initialization
    void Start()
    {
        hilight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void trackLocation(LocationSceneState state, Data.Resource.SubType commodity)
    {
        this.state = state;
        //trackedLocation = state.trackedLocation;
        trackedCommodity = commodity;
        updateCommodityInfo();
    }

    private void updateCommodityInfo()
    {
        ///@todo set readable names
        commodityName.text = Enum.GetName(typeof(Data.Resource.SubType), trackedCommodity);
        price.text = "0";
        storeAmount.text = state.tradeList.commodities[trackedCommodity].ToString();
        cargoAmount.text = "0"; //Root.game.player.cargo.commodities[].ToString();
    }

    public void msgMouseEnter()
    {
        hilight.enabled = true;
    }

    public void msgMouseExit()
    {
        hilight.enabled = false;
    }

    public void buy()
    {
        //Dictionary<string, int> store = Game.universe.locations[trackedLocation].stockpile.tradable;
        //Dictionary<string, int> player = Game.universe.player.cargo.commodities;

        //if (store[trackedCommodity] > 0 &&
        //    Game.universe.player.cargo.credits >= Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity) &&
        //    Game.universe.player.cargo.getUsedCargoSpace() < Game.universe.player.cargo.maxCargoSpace)
        //{
        //    --store[trackedCommodity];
        //    ++player[trackedCommodity];
        //    Game.universe.player.cargo.credits -= Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity);
        //}

        //updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    public void sell()
    {
        //Dictionary<string, int> store = Game.universe.locations[trackedLocation].stockpile.tradable;
        //Dictionary<string, int> player = Game.universe.player.cargo.commodities;

        //if (Game.universe.player.cargo.commodities[trackedCommodity] > 0)
        //{
        //    ++store[trackedCommodity];
        //    --player[trackedCommodity];
        //    Game.universe.player.cargo.credits += Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity);
        //}

        //updateCommodityInfo(trackedLocation, trackedCommodity);
    }
}
