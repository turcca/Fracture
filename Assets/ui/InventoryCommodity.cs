using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryCommodity : MonoBehaviour
{
    public string trackedCommodity;
    public Text name;
    public Text pcs;

    public void setup(string commodity)
    {
        trackedCommodity = commodity;
        name.text = Economy.commodityInfo[trackedCommodity].name;
        pcs.text = Game.universe.player.cargo.commodities[trackedCommodity].ToString();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
