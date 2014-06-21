using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocationTradeGrid : MonoBehaviour
{
    /*
    public static LocationTradeGrid Instance; // Gives out references to the only LocationTradeGrid script object
    private UIGrid uiGrid;
    public GameObject commodityObject;	// prefab for economic article

    public Dictionary<LocationCommodityObject, itemQuotas> itemBtn; // = new  Dictionary<LocationCommodityObject, itemQuotas>();

    public class itemQuotas
    {
        public string name;
        public int playerQuota;
        public int locationQuota;

        private cargoItem thisCargoItem;

        // constructor
        public itemQuotas(string Name)
        {
            this.name = Name;
            this.thisCargoItem = new cargoItem();
            this.thisCargoItem = LocationTradeGrid.Instance.generateLocalCargoItem(this.name);
            this.updateQuotas();
        }
        public void updateQuotas()
        {
            this.playerQuota = LocationTradeGrid.Instance.getCargoQuota(this.name);
            this.locationQuota = LocationTradeGrid.Instance.getLocationQuota(this.name);
        }
        public bool hasSoldPlayerQuota()
        {
            if (this.playerQuota == 0) return false;
            // transfer stockpile items
            Economy.Instance.cargoRemove(this.name);
            if (LocationUI.Instance.currentLocation.stockpile.ContainsKey(this.name)) LocationUI.Instance.currentLocation.stockpile[this.name].amount++;
            else { Debug.LogWarning("invalid item being sold - what to do with special items being sold?"); }
            //else LocationUI.Instance.currentLocation.stockpile.Add(new item?)

            this.updateQuotas();
            return true;
        }
        public bool hasSoldLocationQuota()
        {
            if (this.locationQuota == 0) return false;
            // transfer stockpile items
            Economy.Instance.cargoAdd(this.thisCargoItem, 0);
            if (LocationUI.Instance.currentLocation.stockpile.ContainsKey(this.name)) LocationUI.Instance.currentLocation.stockpile[this.name].amount--;
            else { Debug.LogError("non-excisting item being bought " + this.name); return false; }
            this.updateQuotas();
            return true;
        }
    }

    // Use this for initialization
    void Awake()
    {
        LocationTradeGrid.Instance = this;
        uiGrid = this.gameObject.GetComponent<UIGrid>();
        if (commodityObject == null) commodityObject = Resources.Load<GameObject>("Prefabs/UI_elements/commodityObject");

        itemBtn = new Dictionary<LocationCommodityObject, itemQuotas>();
    }

    public void buySellBtnEnabledAll()
    {
        foreach (LocationCommodityObject item in itemBtn.Keys)
        {
            item.buySellBtnEnabled();
        }
    }




    // eventCharacters(true) starts it, (false) ends it

    void enable(bool activate)
    {
        // delete all excisting objects (commodity buttons) under this object (locationTradeGrid)
        Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>(true);	// includeInactive = true
        int count = allChildren.Length;
        if (count > 1)
        {
            for (int i = 1; i < count; i++) 	// skip first, because it's 'this' object itself
            {
                Destroy(allChildren[i].gameObject);
            }
            itemBtn.Clear();
        }


        // if activating: create prefab buttons for each character in list
        if (activate)
        {
            createCommodityObjectsForLocation();
        }
        // if disabling
        else
        {

        }

        // reposition grid
        uiGrid.repositionNow = true;
    }

    void createCommodityObjectsForLocation()
    {
        int n = 0;

        // create prefab button for each commodity
        foreach (string item in Economy.Instance.item.Keys)
        {
            createSeparator(item);

            // create standard commodity items
            createCommodityInstance(item);
            n++;
        }

        // add special items from player cargo
        createSeparator(null);

        int count = Economy.Instance.cargo.Count;
        for (int i = 0; i < count; i++)
        {
            if (Economy.Instance.cargo[i].isSellable && !Economy.Instance.item.ContainsKey(Economy.Instance.cargo[i].name))
            {
                // item is special - check to add multiple cargoItems!
                if (itemDoesNotExcistYet(Economy.Instance.cargo[i].name))
                {
                    // add special cargo
                    createCommodityInstance(Economy.Instance.cargo[i].name);
                    n++;
                }
                //
            }
        }
    }

    // ********************************************


    void createCommodityInstance(string commodity)
    {
        // 
        // create button as child
        GameObject btn = NGUITools.AddChild(this.gameObject, commodityObject);
        LocationCommodityObject button = btn.GetComponent<LocationCommodityObject>();

        // set  values
        // add item to itemBtn list
        itemBtn.Add(button, new itemQuotas(commodity));
        button.init(commodity);
    }

    void createSeparator(string commodity) 	// haxy
    {
        // only create before "treshold" objects, starting a new group
        if (commodity == null || commodity == "common" || commodity == "bulk" || commodity == "supplies" || commodity == "consumer" || commodity == "common" || commodity == "ordnance" || commodity == "drinks")
        {
            GameObject btn = NGUITools.AddChild(this.gameObject, commodityObject);
            LocationCommodityObject button = btn.GetComponent<LocationCommodityObject>();
            //itemBtn.Add(button, new itemQuotas(null) );
            button.init(null);
        }
    }



    bool itemDoesNotExcistYet(string checkItem)
    {
        // go through itemBtn list values.name
        foreach (itemQuotas item in itemBtn.Values)
        {
            if (item.name == checkItem) return false;
        }
        return true;
    }

    int getCargoQuota(string Name)
    {
        int found = 0;
        // go through cargolist and add all matching cargo
        foreach (cargoItem item in Economy.Instance.cargo)
        {
            if (item.name == Name) found++;
        }
        return found;
    }

    int getLocationQuota(string Name)
    {
        if (Name == null || !Economy.Instance.item.ContainsKey(Name)) return 0;

        if (LocationUI.Instance.currentLocation != null)
        {
            return LocationCommodityPrice.amountWillingToSell(LocationUI.Instance.currentLocation, Name);
            // return LocationUI.Instance.currentLocation.stockpile[Name].amount;
        }
        else
        {
            Debug.LogError("ERROR: accessing locationQuota, but no locationUI/currentLocation was set.");
            return 0;
        }
    }

    cargoItem generateLocalCargoItem(string commodity)
    {
        if (commodity == null) return null;

        cargoItem c = new cargoItem();

        c.name = commodity;
        // list item
        if (Economy.Instance.item.ContainsKey(commodity))
        {
            c.size = 1;
            c.knownOrigins = LocationUI.Instance.currentLocation.name;
            c.purchaseValue = LocationCommodityPrice.getPrice(LocationUI.Instance.currentLocation, commodity, false);
            c.shelfLife = Economy.Instance.item[commodity].shelfLife;
        }
        else
        {
            Debug.Log("asking to generate cargo item from outside list - FEATURE INCOMPLETE!");
            return null;
        }

        return c;
    }
     */
}

