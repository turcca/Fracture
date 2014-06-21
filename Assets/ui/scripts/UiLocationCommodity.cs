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
            Universe.singleton.player.cargo.commodities[commodity].ToString();
        gameObject.transform.FindChild("buyButton").FindChild("locationQuota").GetComponent<UILabel>().text =
            Universe.singleton.locations[location].stockpile.tradable[commodity].ToString();
        gameObject.transform.FindChild("price").GetComponent<UILabel>().text =
            Universe.singleton.locations[location].getCommodityPrice(commodity).ToString();
        if (Universe.singleton.locations[location].getCommodityPrice(commodity) >
            Economy.commodityInfo[commodity].value)
        {
            gameObject.transform.FindChild("price").GetComponent<UILabel>().color = new Color(0, 0.7f, 0);
        }
        else
        {
            gameObject.transform.FindChild("price").GetComponent<UILabel>().color = new Color(0, 0, 0);
        }

        GameObject.Find("statusDesc").GetComponent<UILabel>().text =
            "Cargo: " + Universe.singleton.player.cargo.getUsedCargoSpace().ToString() + " / " +
                Universe.singleton.player.cargo.maxCargoSpace.ToString() + "\n" +
                "Credits: " + Universe.singleton.player.cargo.credits.ToString();
    }

    // msg
    public void sell()
    {
        Universe.singleton.player.cargo.commodities[trackedCommodity]--;
        Universe.singleton.player.cargo.credits += Universe.singleton.locations[trackedLocation].getCommodityPrice(trackedCommodity);
        Universe.singleton.locations[trackedLocation].stockpile.tradable[trackedCommodity]++;
        updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    // msg
    public void buy()
    {
        if (Universe.singleton.player.cargo.getUsedCargoSpace() < Universe.singleton.player.cargo.maxCargoSpace)
        {
            Universe.singleton.player.cargo.commodities[trackedCommodity]++;
            Universe.singleton.player.cargo.credits -= Universe.singleton.locations[trackedLocation].getCommodityPrice(trackedCommodity);
            Universe.singleton.locations[trackedLocation].stockpile.tradable[trackedCommodity]--;
            updateCommodityInfo(trackedLocation, trackedCommodity);
        }
    }

    //public void OnHover(bool hover)
    //{
    //    GameObject.Find("commodityDesc").GetComponent<UILabel>().text =
    //        Economy.commodityInfo[trackedCommodity].name;
    //}
}
