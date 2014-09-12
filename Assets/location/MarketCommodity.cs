using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MarketCommodity : MonoBehaviour
{
    public string trackedCommodity = "not defined";
    public LocationSceneState state;

    public Text name;
    public Text price;
    public Text cargoAmount;
    public Text storeAmount;

    public Image hilight;
    
    private Text playerCredits;
    private Text playerCargo;
    private Image lineGraph;
    private Image lineGraphHor;
    private string trackedLocation = "not defined";


    // Use this for initialization
    void Start()
    {
        hilight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void trackLocation(string locationId, string commodity)
    {
        trackedLocation = locationId;
        trackedCommodity = commodity;

        playerCredits = GameObject.Find("playerCredits").GetComponent<Text>();
        playerCargo = GameObject.Find("playerCargo").GetComponent<Text>();
        //lineGraph = GameObject.Find("CommodityLineGraph").GetComponent<Image>();
        //lineGraphHor = GameObject.Find("CommodityLineGraphHor").GetComponent<Image>();
        //inlineGraphHor.enabled = false;

        updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    private void updateCommodityInfo(string location, string commodity)
    {
        name.text = Economy.commodityInfo[commodity].name;
        price.text = Game.universe.locations[location].getCommodityPrice(commodity).ToString();
        cargoAmount.text = Game.universe.player.cargo.commodities[commodity].ToString();
        storeAmount.text = Game.universe.locations[location].stockpile.tradable[commodity].ToString();

        playerCredits.text = Game.universe.player.cargo.credits.ToString();
        playerCargo.text = Game.universe.player.cargo.getUsedCargoSpace().ToString() + "/" +
                           Game.universe.player.cargo.maxCargoSpace.ToString();
    }

    public void msgMouseEnter()
    {
        hilight.enabled = true;
        //lineGraph.rectTransform.sizeDelta = new Vector2(48 + 214 - ((RectTransform)gameObject.transform).localPosition.y, 3);
        //lineGraphHor.enabled = true;
        //if (((RectTransform)gameObject.transform).localPosition.x < 0)
        //    lineGraphHor.rectTransform.localPosition = new Vector3(-24, -42 + ((RectTransform)gameObject.transform).localPosition.y, 0);
        //else
        //    lineGraphHor.rectTransform.localPosition = new Vector3(24, -42 + ((RectTransform)gameObject.transform).localPosition.y, 0);
    }

    public void msgMouseExit()
    {
        hilight.enabled = false;
        //lineGraph.rectTransform.sizeDelta = new Vector2(48, 3);
        //lineGraphHor.enabled = false;
    }

    public void buy()
    {
        Dictionary<string, int> store = Game.universe.locations[trackedLocation].stockpile.tradable;
        Dictionary<string, int> player = Game.universe.player.cargo.commodities;

        if (store[trackedCommodity] > 0 &&
            Game.universe.player.cargo.credits >= Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity) &&
            Game.universe.player.cargo.getUsedCargoSpace() < Game.universe.player.cargo.maxCargoSpace)
        {
            --store[trackedCommodity];
            ++player[trackedCommodity];
            Game.universe.player.cargo.credits -= Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity);
        }

        updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    public void sell()
    {
        Dictionary<string, int> store = Game.universe.locations[trackedLocation].stockpile.tradable;
        Dictionary<string, int> player = Game.universe.player.cargo.commodities;

        if (Game.universe.player.cargo.commodities[trackedCommodity] > 0)
        {
            ++store[trackedCommodity];
            --player[trackedCommodity];
            Game.universe.player.cargo.credits += Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity);
        }

        updateCommodityInfo(trackedLocation, trackedCommodity);
    }
}
