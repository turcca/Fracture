using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryList : MonoBehaviour
{
    public GameObject grid;
    private bool needsUpdate = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (needsUpdate)
        {
            populateCommodities();
        }
    }

    void OnDisable()
    {
        needsUpdate = true;
    }

    public void populateCommodities()
    {
        // destroy old entries
        for (int i = grid.transform.childCount - 1; i >= 0; --i)
        {
            DestroyImmediate(grid.transform.GetChild(i).gameObject);
        }

        int numItems = 0;
        foreach (KeyValuePair<string, CommodityInfo> entry in Economy.commodityInfo)
        {
            if (Root.game.player.cargo.commodities[entry.Key] > 0)
            {
                GameObject commodityPrefab = Resources.Load<GameObject>("ui/prefabs/InventoryCommodity");
                GameObject commodity = (GameObject)GameObject.Instantiate(commodityPrefab);
                commodity.GetComponent<InventoryCommodity>().setup(entry.Key);
                commodity.transform.parent = grid.transform;
                ++numItems;
            }
        }

        ((RectTransform)grid.transform).sizeDelta =
            new Vector2(300, 30 * numItems);

        needsUpdate = false;
    }
}
