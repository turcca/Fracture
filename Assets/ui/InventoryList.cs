using System;
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
        foreach (Data.Resource.SubType type in Enum.GetValues(typeof(Data.Resource.SubType)))
        {
            if (Root.game.player.cargo.commodities[type] > 0)
            {
                GameObject commodityPrefab = Resources.Load<GameObject>("ui/prefabs/InventoryCommodity");
                GameObject commodity = (GameObject)GameObject.Instantiate(commodityPrefab);
                commodity.GetComponent<InventoryCommodity>().setup(type);
                commodity.transform.parent = grid.transform;
                ++numItems;
            }
        }

        ((RectTransform)grid.transform).sizeDelta =
            new Vector2(300, 30 * numItems);

        needsUpdate = false;
    }
}
