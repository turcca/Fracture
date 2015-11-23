using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryCommodity : MonoBehaviour
{
    public Data.Resource.SubType trackedCommodity;
    public Text name;
    public Text pcs;

    public void setup(Data.Resource.SubType type)
    {
        trackedCommodity = type;
        name.text = Simulation.Trade.getCommodityName(type); //Economy.commodityInfo[trackedCommodity].name;
        pcs.text = "Hook this 'pcs' up [InventoryCommodity.cs]"; // Root.game.player.cargo.commodities[trackedCommodity].ToString();
    }

}
