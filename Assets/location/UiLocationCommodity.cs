using UnityEngine;
using System.Collections;

public class UiLocationCommodity : MonoBehaviour
{
    public string trackedCommodity = "not defined";
    public string trackedLocation = "not defined";
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void trackCommodity(string location, string commodity)
    {
        trackedLocation = location;
        trackedCommodity = commodity;

        updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    private void updateCommodityInfo(string location, string commodity)
    {
        gameObject.transform.FindChild("name").GetComponent<UILabel>().text =
            Economy.commodityInfo[commodity].name;
        gameObject.transform.FindChild("sellButton").FindChild("playerQuota").GetComponent<UILabel>().text =
            Game.getUniverse().player.cargo.commodities[commodity].ToString();
        gameObject.transform.FindChild("buyButton").FindChild("locationQuota").GetComponent<UILabel>().text =
            Game.getUniverse().locations[location].stockpile.tradable[commodity].ToString();
        gameObject.transform.FindChild("price").GetComponent<UILabel>().text =
            Game.getUniverse().locations[location].getCommodityPrice(commodity).ToString();
        if (Game.getUniverse().locations[location].getCommodityPrice(commodity) >
            Economy.commodityInfo[commodity].value)
        {
            gameObject.transform.FindChild("price").GetComponent<UILabel>().color = new Color(0, 0.7f, 0);
        }
        else
        {
            gameObject.transform.FindChild("price").GetComponent<UILabel>().color = new Color(0, 0, 0);
        }

        GameObject.Find("statusDesc").GetComponent<UILabel>().text =
            "Cargo: " + Game.getUniverse().player.cargo.getUsedCargoSpace().ToString() + " / " +
                Game.getUniverse().player.cargo.maxCargoSpace.ToString() + "\n" +
                "Credits: " + Game.getUniverse().player.cargo.credits.ToString();
    }

    // msg
    public void sell()
    {
        Game.getUniverse().player.cargo.commodities[trackedCommodity]--;
        Game.getUniverse().player.cargo.credits += Game.getUniverse().locations[trackedLocation].getCommodityPrice(trackedCommodity);
        Game.getUniverse().locations[trackedLocation].stockpile.tradable[trackedCommodity]++;
        updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    // msg
    public void buy()
    {
        if (Game.getUniverse().player.cargo.getUsedCargoSpace() < Game.getUniverse().player.cargo.maxCargoSpace)
        {
            Game.getUniverse().player.cargo.commodities[trackedCommodity]++;
            Game.getUniverse().player.cargo.credits -= Game.getUniverse().locations[trackedLocation].getCommodityPrice(trackedCommodity);
            Game.getUniverse().locations[trackedLocation].stockpile.tradable[trackedCommodity]--;
            updateCommodityInfo(trackedLocation, trackedCommodity);
        }
    }

    //public void OnHover(bool hover)
    //{
    //    GameObject.Find("commodityDesc").GetComponent<UILabel>().text =
    //        Economy.commodityInfo[trackedCommodity].name;
    //}
}
