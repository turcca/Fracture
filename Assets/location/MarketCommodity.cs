using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MarketCommodity : MonoBehaviour
{
    public string trackedCommodity = "not defined";
    public string trackedLocation = "not defined";

    public Text name;
    public Text price;
    public Button cargo;
    public Button store;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void trackLocation(string locationId, string commodity)
    {
        trackedLocation = locationId;
        trackedCommodity = commodity;

        updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    private void updateCommodityInfo(string location, string commodity)
    {
        name.text = Economy.commodityInfo[commodity].name;
        price.text = Game.getUniverse().locations[location].getCommodityPrice(commodity).ToString();

        //gameObject.transform.FindChild("sellButton").FindChild("playerQuota").GetComponent<UILabel>().text =
        //    Game.getUniverse().player.cargo.commodities[commodity].ToString();
        //gameObject.transform.FindChild("buyButton").FindChild("locationQuota").GetComponent<UILabel>().text =
        //    Game.getUniverse().locations[location].stockpile.tradable[commodity].ToString();
        //if (Game.getUniverse().locations[location].getCommodityPrice(commodity) >
        //    Economy.commodityInfo[commodity].value)
        //{
        //    gameObject.transform.FindChild("price").GetComponent<UILabel>().color = new Color(0, 0.7f, 0);
        //}
        //else
        //{
        //    gameObject.transform.FindChild("price").GetComponent<UILabel>().color = new Color(0, 0, 0);
        //}

        //GameObject.Find("statusDesc").GetComponent<UILabel>().text =
        //    "Cargo: " + Game.getUniverse().player.cargo.getUsedCargoSpace().ToString() + " / " +
        //        Game.getUniverse().player.cargo.maxCargoSpace.ToString() + "\n" +
        //        "Credits: " + Game.getUniverse().player.cargo.credits.ToString();
    }
}
