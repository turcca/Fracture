using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
    GameObject inventory;
    // Use this for initialization
    void Start()
    {
        inventory = GameObject.Find("UIInventory");
        NGUITools.SetActive(inventory, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void msgToggleInventory()
    {
        NGUITools.SetActive(inventory, !NGUITools.GetActive(inventory));
        if (NGUITools.GetActive(inventory))
        {
            inventory.GetComponent<UIInventory>().populateCommodities();
        }
    }
}
