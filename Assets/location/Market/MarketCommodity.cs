using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MarketCommodity : MonoBehaviour
{
    public Data.Resource.SubType trackedCommodity;
    public LocationSceneState state;

    public Image cathegoryImage;
    public Text commodityName;
    public Text price;
    public Text priceMultiplier;
    internal float priceMul;

    public Text cargoAmount;
    public Image cargoBtnImage;
    public Button cargoBtn;
    public Text storeAmount;
    public Image storeBtnImage;
    public Button storeBtn;

    private Text playerCredits;
    private Text playerCargo;
    private Image lineGraph;
    private Image lineGraphHor;
    public Image hilight;
    public Image baseColor;

    internal Data.TradeItem locationItem;
    int itemTier;
    internal float calculatedValue;

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
        ToolTipScript cathegoryTip = cathegoryImage.gameObject.GetComponent<ToolTipScript>(); if (cathegoryTip != null) cathegoryTip.toolTip = trackedCommodity.ToString() +": " + Root.game.player.getLocation().economy.resources[Data.Resource.getTypeOfSubType(trackedCommodity)].getState().ToString()/* +" ("++")"*/;
        updateCommodityInfo();
    }

    internal void updateCommodityInfo()
    {
        //// base colour
        //baseColor.color = (Color)Simulation.Trade.getTypeColor(Data.Resource.getTypeOfSubType(trackedCommodity));

        //// name
        //commodityName.text = Simulation.Trade.getCommodityName(trackedCommodity); //Enum.GetName(typeof(Data.Resource.SubType), trackedCommodity);
        //commodityName.gameObject.GetComponent<ToolTipScript>().toolTip = Simulation.Trade.getCommodityDescription(trackedCommodity);

        //// price
        //priceMul = Simulation.Trade.calculateItemPriceMultiplier(locationItem);
        //float value = Simulation.Trade.getCommodityValue(locationItem.subType);
        //// rounded multiplier calculators
        //calculatedValue = Mathf.Round (priceMul * value);
        //priceMul = Mathf.Round (calculatedValue / value * 100.0f)/100.0f;
        //// price percentage
        ////priceMultiplier.text = (priceMul != 1) ? "Item price at "+(priceMul * 100).ToString() + "%" : "";

        //price.text = calculatedValue.ToString();
        //if (priceMul != 1f)  // if price deviates from the norm
        //{
        //    price.text += (priceMul > 1f) ? "<color=#776666> (+" : "<color=#667766>  (";
        //    price.text += Mathf.Round(calculatedValue - Simulation.Trade.getCommodityValue(trackedCommodity)).ToString() + ")</color>";
        //}
        //// location price color: red importing, green exporting
        //if (locationItem.isExported) price.color = new Color(Mathf.Max(0, 0.8f - (priceMul-1f)/3f), 1.0f, Mathf.Max(0, 0.9f - (priceMul-1f)/3f) );
        //else price.color = new Color(0.9f, 0.9f/priceMul, 0.8f/priceMul);

        // store amount
        storeAmount.text = locationItem.amount.ToString(); //state.tradeList.commodities[trackedCommodity].ToString();
        if (locationItem.amount > 0)
        {
            if (!Root.game.player.cargo.hasFreeCargoSpace())
            {
                // cargo full
                storeBtnImage.enabled = true;
                storeAmount.color = new Color32(95, 93, 120, 255); // greyed out amount
                storeBtn.enabled = true;
                storeBtn.interactable = false;
                
            }
            else if (calculatedValue > Root.game.player.cargo.credits)
            {
                // can't be afforded
                storeBtnImage.enabled = false;
                storeAmount.color = new Color32(95, 93, 120, 255); // greyed out amount
                storeBtn.enabled = false;
            }
            else
            {
                // able to buy
                storeBtnImage.enabled = true;
                storeAmount.color = new Color32(215,223,229, 255);
                storeBtn.enabled = true;
                storeBtn.interactable = true;
            }
            storeAmount.enabled = true;
        }
        else
        {
            storeBtnImage.enabled = false;
            storeAmount.enabled = false;
        }

        // player amount
        cargoAmount.text = Root.game.player.cargo.commodities[trackedCommodity].ToString(); // economy.playerTradeList obsolite?
        if (Root.game.player.cargo.commodities[trackedCommodity] > 0)
        {
            cargoBtnImage.enabled = true;
            cargoAmount.enabled = true;
        }
        else
        {
            cargoBtnImage.enabled = false;
            cargoAmount.enabled = false;
        }

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
                if (Root.game.player.cargo.hasFreeCargoSpace())
                {
                    Root.game.player.cargo.credits -= calculatedValue;
                    --locationItem.amount;
                    Root.game.locations[state.trackedLocation].economy.export(locationItem.type, 0.25f);
                    Debug.Log (" todo: affect tier balancing / todo: cargo space");
                    ++Root.game.player.cargo.commodities[trackedCommodity];

                    marketPage.updateCommodityInfos();
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

            marketPage.updateCommodityInfos();
        }
        else if (debug) Debug.Log ("player is out of commodities");
    }
}
