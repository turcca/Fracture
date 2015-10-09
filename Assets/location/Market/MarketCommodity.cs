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
    public Text priceMultiplier;
    private float priceMul;

    public Text cargoAmount;
    public Text storeAmount;

    private Text playerCredits;
    private Text playerCargo;
    private Image lineGraph;
    private Image lineGraphHor;
    public Image hilight;

    Data.TradeItem locationItem;
    int itemTier;
    public float calculatedValue {get; private set;}

    MarketPage marketPage;

    // Use this for initialization
    void Start()
    {
        hilight.enabled = false;
    }


    internal void trackLocation(LocationSceneState state, Data.Resource.SubType commodity, MarketPage mp)
    {
        this.state = state;
        this.marketPage = mp;
        trackedCommodity = commodity;
        locationItem = Root.game.locations[state.trackedLocation].economy.getTradeItem(trackedCommodity, marketPage.locationTradeList);
        itemTier = Data.Resource.getSubTypeTier(locationItem.subType);
        updateCommodityInfo();
    }

    private void updateCommodityInfo()
    {
        // name
        commodityName.text = Simulation.Trade.getCommodityName(trackedCommodity); //Enum.GetName(typeof(Data.Resource.SubType), trackedCommodity);

        // price
        priceMul = Simulation.Trade.calculateItemPriceMultiplier(locationItem);
        float value = Simulation.Trade.getCommodityValue(locationItem.subType);
        // rounded multiplier calculators
        calculatedValue = Mathf.Round (priceMul * value);
        priceMul = Mathf.Round (calculatedValue / value * 100.0f)/100.0f;

        priceMultiplier.text = priceMul.ToString();

        price.text = calculatedValue.ToString();
        // location price color: red importing, green exporting
        if (locationItem.isExported) price.color = new Color(0.8f, 1.0f, 0.9f);
        else price.color = new Color(1.0f, 0.9f/priceMul, 0.8f/priceMul);

        // store amount
        storeAmount.text = locationItem.amount.ToString(); //state.tradeList.commodities[trackedCommodity].ToString();

        // player amount
        cargoAmount.text = Root.game.player.cargo.commodities[trackedCommodity].ToString(); // economy.playerTradeList obsolite?

        // update credits
        marketPage.updateCredits();
        // update cargo space
        marketPage.updateCargo();
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
        bool debug = true;

        if (locationItem.amount >= 1.0f)
        {
            if (Root.game.player.cargo.credits >= calculatedValue)
            {
                if (Root.game.player.cargo.getUsedCargoSpace() < 10)
                {
                    Root.game.player.cargo.credits -= calculatedValue;
                    --locationItem.amount;
                    Root.game.locations[state.trackedLocation].economy.export(locationItem.type, 0.25f);
                    Debug.Log (" todo: affect tier balancing");
                    ++Root.game.player.cargo.commodities[trackedCommodity];

                    updateCommodityInfo();
                }
                else if (debug) Debug.Log ("cargo full (todo: crago space)");
            }
            else if (debug) Debug.Log ("not enough player credits");
        }
        else if (debug) Debug.Log ("not enough stock: "+locationItem.amount);
    }

    public void sell()
    {
        bool debug = true;
        
        if (Root.game.player.cargo.commodities[trackedCommodity] > 0)
        {
            Root.game.player.cargo.credits += calculatedValue;
            ++locationItem.amount;
            Root.game.locations[state.trackedLocation].economy.import(locationItem.type, 0.25f);
            Debug.Log (" todo: affect tier balancing");
            --Root.game.player.cargo.commodities[trackedCommodity];
            
            updateCommodityInfo();
        }
        else if (debug) Debug.Log ("player is out of commodities");
    }
}
