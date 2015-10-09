using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MarketPage : MonoBehaviour
{
    public LocationSceneState state;
    public GameObject grid;

    public Text marketDescription;

    public Text playerCargo;
    public Text playerCredit;

    public List<Data.TradeItem> playerTradeList = new List<Data.TradeItem>();
    public List<Data.TradeItem> locationTradeList = new List<Data.TradeItem>();


    void Start()
    {
        playerTradeList = Root.game.locations[state.trackedLocation].getPlayerTradeList();
        locationTradeList = Root.game.locations[state.trackedLocation].getLocationTradeList();
        
        populateCommodities();
        updateCargo();
        updateCredits();

        updateDescription();
    }

    private void populateCommodities()
    {
        int order = 0;
        //foreach (KeyValuePair<string, CommodityInfo> entry in Economy.commodityInfo)
        grid.transform.DetachChildren();
        foreach(Data.TradeItem item in locationTradeList)
        {
            if (item.amount > 0f || Root.game.player.cargo.cargoAmount(item.subType) > 0)
            {
                // add commodity ui gameobjects
                GameObject commodityPrefab = Resources.Load<GameObject>("location/ui/CommodityMarketItem");
                GameObject commodity = (GameObject)GameObject.Instantiate(commodityPrefab);
                commodity.GetComponent<MarketCommodity>().trackLocation(state, item.subType, this);
                commodity.name = order.ToString() + "_commodity";
                commodity.transform.SetParent(grid.transform);
                ++order;
            }
        }
        if (order == 0)
        {
            Debug.Log("todo: 0 trade items");
        }
    }

    public void updateCargo()
    {
        if (playerCargo != null)
        {
            playerCargo.text = "Cargo: "+Root.game.player.cargo.getUsedCargoSpace()+" / "+Root.game.player.cargo.maxCargoSpace;
        }
        else Debug.LogError ("ERROR: playerCargo not set in Editor");
    }
    public void updateCredits()
    {
        if (playerCargo != null)
        {
            playerCredit.text = "Credits: "+Root.game.player.cargo.credits;
        }
        else Debug.LogError ("ERROR: playerCargo not set in Editor");
    }
    public void updateDescription()
    {
        if (marketDescription)
        {
            marketDescription.text = getMarketDescription();
        }
    }




    string getMarketDescription()
    {
        Location location = Root.game.player.getLocation();

        string rv = "";

        List<KeyValuePair<Data.Resource.Type, float>> shortagedItmes = new List<KeyValuePair<Data.Resource.Type, float>>();
        List<KeyValuePair<Data.Resource.Type, float>> covetedItems = new List<KeyValuePair<Data.Resource.Type, float>>();
        List<KeyValuePair<Data.Resource.Type, float>> abundantItems = new List<KeyValuePair<Data.Resource.Type, float>>();

        foreach (Data.TradeItem item in locationTradeList)
        {
            float mul = Simulation.Trade.calculateItemPriceMultiplier(item);

            // pick shortage
            if (location.economy.resources[item.type].getState() == Data.Resource.State.Shortage)
            {
                //Debug.Log(item.type.ToString() + ": mul: " + mul + "[" + item.subType.ToString() + "]" + " isExported:" + item.isExported +"SHORTAGE");

                bool duplicateNotFound = true;
                foreach (KeyValuePair<Data.Resource.Type, float> itemType in shortagedItmes)
                {
                    if (itemType.Key == item.type) { duplicateNotFound = false; break; }
                }
                if (duplicateNotFound) shortagedItmes.Add(new KeyValuePair<Data.Resource.Type, float>(item.type, mul));
            }
            // pick coveted
            else if (!item.isExported && mul > 1f)
            {
                //Debug.Log(item.type.ToString() + ": mul: " + mul + "[" + item.subType.ToString() + "]" + " isExported:" + item.isExported +"COVETED");

                bool duplicateNotFound = true;
                foreach (KeyValuePair<Data.Resource.Type, float> itemType in covetedItems)
                {
                    if (itemType.Key == item.type) { duplicateNotFound = false; break; }
                }
                if (duplicateNotFound) covetedItems.Add(new KeyValuePair<Data.Resource.Type, float>(item.type, mul));
            }
            // pick selling
            else if (item.isExported && mul < 0.95f)
            {
                //Debug.Log(item.type.ToString() + ": mul: " + mul + "[" + item.subType.ToString() + "]" + " isExported:" + item.isExported +"SELLING");

                bool duplicateNotFound = true;
                foreach (KeyValuePair<Data.Resource.Type, float> itemType in abundantItems)
                {
                    if (itemType.Key == item.type) { duplicateNotFound = false; break; }
                }
                if (duplicateNotFound) abundantItems.Add(new KeyValuePair<Data.Resource.Type, float>(item.type, mul));

            }
        }
        // sort
        if (shortagedItmes.Count > 1)
        {
            shortagedItmes.Sort(
                delegate (KeyValuePair<Data.Resource.Type, float> firstPair,
                     KeyValuePair<Data.Resource.Type, float> nextPair)
                {
                    return nextPair.Value.CompareTo(firstPair.Value);
                }
            );
        }
        if (covetedItems.Count > 1)
        {
            covetedItems.Sort(
                delegate (KeyValuePair<Data.Resource.Type, float> firstPair,
                     KeyValuePair<Data.Resource.Type, float> nextPair)
                {
                    return nextPair.Value.CompareTo(firstPair.Value);
                }
            );
        }
        if (abundantItems.Count > 1)
        {
            // reverse order - cheapest first
            abundantItems.Sort(
                delegate (KeyValuePair<Data.Resource.Type, float> firstPair,
                     KeyValuePair<Data.Resource.Type, float> nextPair)
                {
                    return firstPair.Value.CompareTo(nextPair.Value);
                }
            );
        }
        // descriptions
        // Shortage text
        if (shortagedItmes.Count > 0)
        {
            int count = shortagedItmes.Count;

            // only 1
            if (count == 1)
            {
                // starving
                if (shortagedItmes[0].Key == Data.Resource.Type.Food)
                {
                    if (shortagedItmes[0].Value > 8f) rv += "SEVERE PLANETARY FAMINE: Rampaging malnutrition and starvation have skyrocketed food prices. The government struggles to control the revolting planet. ";
                    else if (shortagedItmes[0].Value > 5f) rv += "PLANETARY FAMINE: With widespread food riots and heavy food regulation, the government is buying food at a premium. Armed troops are protecting food shipments coming through the space port. ";
                    else rv += "PLANETARY FOOD SHORTAGES: Food stockpiling and irregular food imports are driving the food prices up. ";
                }
                else if (shortagedItmes[0].Key == Data.Resource.Type.Mineral)
                {
                    if (shortagedItmes[0].Value > 8f) rv += "SEVERE MATERIAL SHORTAGE: Industries are shutting down due to a prolonged material shortage. ";
                    else if (shortagedItmes[0].Value > 5f) rv += "MATERIAL SHORTAGE: Industries are grinding to a halt due to material shortages. ";
                    else rv += "MATERIAL SHORTFALL: Induestries are struggling as material imports have failed to fill the supply. ";
                }
                else if (shortagedItmes[0].Key == Data.Resource.Type.Industry)
                {
                    if (shortagedItmes[0].Value > 8f) rv += "IMMINENT INDUSTRIAL COLLAPSE: The core industries are collapsing and energy shortages are threatening even the old underground terraformers running environment. Rampant unemployment is destabilizing the region. ";
                    else if (shortagedItmes[0].Value > 5f) rv += "INDUSTRIAL SHUTDOWN: Widespread industrial shutdown is leaving workforce unemployment and infrastructre crumbling. ";
                    else rv += "INDUSTRIAL SHORTAGE: Stagnating local industry is unable to replenish production. Factories are in dire need of imports, or core industries are threatened next. Unemployment is on the rise. ";
                }
                else if (shortagedItmes[0].Key == Data.Resource.Type.Economy)
                {
                    if (shortagedItmes[0].Value > 8f) rv += "IMMINENT ECONOMIC COLLAPSE: Local economy is in crisis mode. Interest rates are through the roof and the government is desperately looking for ways to stimulate the economy. ";
                    else if (shortagedItmes[0].Value > 5f) rv += "ECONOMIC CRISIS: Contagious liquidity crisis have crippled the planetary markets. Interest rates are high as the government looks for releaf. ";
                    else rv += "ECONOMIC RECESSION: Lack of external investments have stagnated the local economy. Interest rates are rising to compensate. ";
                }
                else if (shortagedItmes[0].Key == Data.Resource.Type.Innovation)
                {
                    if (shortagedItmes[0].Value > 8f) rv += "EDUCATION AND RESEARCH CRISIS: There is a serious education deficit and lack of expertice in key positions in the private and public sector. ";
                    else if (shortagedItmes[0].Value > 5f) rv += "EDUCATION AND RESEARCH CRISIS: There is an education deficit and serious concerns over the lack of expertice in private and public sectors. ";
                    else rv += "EDUCATION AND RESEARCH DEFICIT: There is a growing education deficit and concern over the lack of expertice in private and public sectors. ";
                }
                else if (shortagedItmes[0].Key == Data.Resource.Type.Culture)
                {
                    if (shortagedItmes[0].Value > 8f) rv += "CONSUMER GOODS SHORTAGE: There is an unprecidented lack of consumer goods on the markets and prices are astronomical. ";
                    else if (shortagedItmes[0].Value > 5f) rv += "CONSUMER GOODS SHORTAGE: Consumer goods are in high demand, and imported goods are being fought over. ";
                    else rv += "CONSUMER GOODS SHORTFALL: Consumer product demand is exceeding demand, imported goods are feching high price on the market. ";
                }
                else if (shortagedItmes[0].Key == Data.Resource.Type.Military)
                {
                    if (shortagedItmes[0].Value > 8f) rv += "MILITARY EXPANSION: There is a planetary arming going on, and scarcity of weapons are driving the prices through the roof. ";
                    else if (shortagedItmes[0].Value > 5f) rv += "MILITARY EXPANSION: The government is expanding military defence and is willing to pay for it. ";
                    else rv += "MILITARY STOCK UP: There is a planetary stockpiling of military equipment. Prices of guns and weapon systems are on the rise. ";
                }
                else if (shortagedItmes[0].Key == Data.Resource.Type.BlackMarket)
                {
                    if (shortagedItmes[0].Value > 8f) rv += "BLACK MARKET DEMAND: Black markets are starving for imports. ";
                    else if (shortagedItmes[0].Value > 5f) rv += "BLACK MARKET DEMAND: Black markets are starving for imports. ";
                    else rv += "BLACK MARKET DEMAND: Black markets are starving for imports. ";
                }
            }
            // multiple
            else
            {
                int n = 1;

                rv += "PLANETARY SHORTAGES: ";
                foreach (KeyValuePair<Data.Resource.Type, float> item in shortagedItmes)
                {
                    rv += getCommodityShortageDescription(item, true);

                    if (n < count - 1) rv += ", ";
                    else if (n == count - 1) rv += " and ";

                    ++n;
                }
                rv += ". ";
            }
        }
        // Coveted text
        if (covetedItems.Count > 0)
        {
            rv += "There is a demand for ";
            int count = covetedItems.Count;
            int n = 1;
            foreach (KeyValuePair<Data.Resource.Type, float> item in covetedItems)
            {
                rv += getCommodityDescription(item, false);
                if (count > 1)
                {
                    if (n < count - 1) rv += ", ";
                    else if (n == count -1) rv += " and ";
                }
                ++n;
            }
            rv += ". ";
        }
        // Selling text
        if (abundantItems.Count > 0)
        {
            rv += "Local exports: ";
            int count = abundantItems.Count;
            int n = 1;
            foreach (KeyValuePair<Data.Resource.Type, float> item in abundantItems)
            {
                rv += getCommodityDescription(item, false);
                if (count > 1)
                {
                    if (n < count - 1) rv += ", ";
                    else if (n == count - 1) rv += " and ";
                }
                ++n;
            }
            rv += ".";
        }

        return rv;
    }


    string getCommodityShortageDescription(KeyValuePair<Data.Resource.Type, float> item, bool returnCaps = false)
    {
        string rv = "";

        if (item.Key == Data.Resource.Type.Food)
        {
            if (item.Value > 8f) rv += "SEVERE PLANETARY FAMINE";
            else if (item.Value > 5f) rv += "PLANETARY FAMINE";
            else rv += "PLANETARY FOOD SHORTAGES";
        }
        else if (item.Key == Data.Resource.Type.Mineral)
        {
            if (item.Value > 8f) rv += "SEVERE MATERIAL SHORTAGE";
            else if (item.Value > 5f) rv += "MATERIAL SHORTAGE";
            else rv += "MATERIAL SHORTFALL";
        }
        else if (item.Key == Data.Resource.Type.Industry)
        {
            if (item.Value > 8f) rv += "IMMINENT INDUSTRIAL COLLAPSE";
            else if (item.Value > 5f) rv += "INDUSTRIAL SHUTDOWN";
            else rv += "INDUSTRIAL SHORTAGE";
        }
        else if (item.Key == Data.Resource.Type.Economy)
        {
            if (item.Value > 8f) rv += "IMMINENT ECONOMIC COLLAPSE";
            else if (item.Value > 5f) rv += "ECONOMIC CRISIS";
            else rv += "ECONOMIC RECESSION";
        }
        else if (item.Key == Data.Resource.Type.Innovation)
        {
            if (item.Value > 8f) rv += "EDUCATION AND RESEARCH CRISIS";
            else if (item.Value > 5f) rv += "EDUCATION AND RESEARCH CRISIS";
            else rv += "EDUCATION AND RESEARCH DEFICIT";
        }
        else if (item.Key == Data.Resource.Type.Culture)
        {
            if (item.Value > 8f) rv += "CONSUMER GOODS SHORTAGE";
            else if (item.Value > 5f) rv += "CONSUMER GOODS SHORTAGE";
            else rv += "CONSUMER GOODS SHORTFALL";
        }
        else if (item.Key == Data.Resource.Type.Military)
        {
            if (item.Value > 8f) rv += "MILITARY EXPANSION";
            else if (item.Value > 5f) rv += "MILITARY EXPANSION";
            else rv += "MILITARY STOCK UP";
        }
        else if (item.Key == Data.Resource.Type.BlackMarket)
        {
            if (item.Value > 8f) rv += "BLACK MARKET DEMAND";
            else if (item.Value > 5f) rv += "BLACK MARKET DEMAND";
            else rv += "BLACK MARKET DEMAND";
        }
        if (returnCaps) return rv;
        else return rv.ToLower();
    }
    string getCommodityDescription(KeyValuePair<Data.Resource.Type, float> item, bool lowerCase = true)
    {
        string rv = "";

        if (item.Key == Data.Resource.Type.Food)
        {
            rv += "Food items";
        }
        else if (item.Key == Data.Resource.Type.Mineral)
        {
            rv += "Materials";
        }
        else if (item.Key == Data.Resource.Type.Industry)
        {
            rv += "Industrial machinery";
        }
        else if (item.Key == Data.Resource.Type.Economy)
        {
            rv += "Foreign ivestments";
        }
        else if (item.Key == Data.Resource.Type.Innovation)
        {
            rv += "Innovation assets";
        }
        else if (item.Key == Data.Resource.Type.Culture)
        {
            rv += "Consumer goods";
        }
        else if (item.Key == Data.Resource.Type.Military)
        {
            rv += "Weapons";
        }
        else if (item.Key == Data.Resource.Type.BlackMarket)
        {
            rv += "Black market goods";
        }
        if (!lowerCase) return rv;
        else return rv.ToLower();
    }
}
