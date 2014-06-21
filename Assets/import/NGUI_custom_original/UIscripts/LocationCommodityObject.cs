using UnityEngine;
using System.Collections;





public class LocationCommodityObject : MonoBehaviour
{
    /*
    public enum TradeStatus
    {
        available,
        tooExpensive,
        illegal
    }

    // object links
    public UILabel uiLabelName;
    public UILabel uiLabelPrice;
    public UILabel playerQuota;
    public UILabel locationQuota;

    public UIButton sellBtn;
    public UIButton buyBtn;

    public BoxCollider sellCollider;
    public BoxCollider buyCollider;
    public UISprite sellSprite;
    public UISprite buySprite;
    public UIWidget itemWidget;
    public UIWidget sellWidget;
    public UIWidget buyWidget;
    public TweenColor sellTween;
    public TweenColor buyTween;

    // object info
    public string itemName;
    public float price;

    public TradeStatus sellStatus;
    public TradeStatus buyStatus;

    public Color tooExpensive = new Color(0.8F, 0.8F, 0.8F, 1F);

    // Use this for initialization
    void Awake()
    {
        if (uiLabelName == null) Debug.LogError("ERROR: uiLabelName not manually set.");
    }

    public void init(string Name)
    {
        itemName = Name;

        // if separator, turn off UISprite
        if (Name == null)
        { //Debug.Log("null separator, turning off UISprite");
            this.gameObject.SetActive(false);
            //this.gameObject.GetComponent<UISprite>().enabled = false;
        }
        // Initialize button
        else
        {
            // set button label text
            uiLabelName.text = Economy.Instance.item[Name].name;
            // 
            // bg colour?
            // get quotas and price
            LocationTradeGrid.Instance.itemBtn[this].updateQuotas();
            updateObjectData(false);
        }

    }

    public void sell()
    {
        if (sellStatus == TradeStatus.available)
        {
            if (LocationTradeGrid.Instance.itemBtn[this].hasSoldPlayerQuota())
            {
                // transfer money
                WorldState.player.funds += price;

                updateObjectData(true);
            }
        }
        else if (sellStatus == TradeStatus.illegal)
        {
            // consequences
        }
    }

    public void buy()
    {
        if (buyStatus == TradeStatus.available)
        {
            if (LocationTradeGrid.Instance.itemBtn[this].hasSoldLocationQuota())
            {
                // transfer money
                WorldState.player.funds -= price;

                updateObjectData(true);
            }
        }
        else if (buyStatus == TradeStatus.illegal)
        {
            // consequences
        }
    }



    void updateObjectData(bool updateAll)
    {
        // set label quotas
        playerQuota.text = LocationTradeGrid.Instance.itemBtn[this].playerQuota.ToString();
        locationQuota.text = LocationTradeGrid.Instance.itemBtn[this].locationQuota.ToString();
        // set price
        updateLocationPricing();
        // handle isEnable on buttons
        if (updateAll) LocationTradeGrid.Instance.buySellBtnEnabledAll();
        else buySellBtnEnabled();
    }



    public void buySellBtnEnabled()
    {
        if (itemName == null) { sellBtn.isEnabled = false; buyBtn.isEnabled = false; Debug.LogError("null"); return; }

        // set SELL button's activity according to item quota
        if (LocationTradeGrid.Instance.itemBtn[this].playerQuota == 0) sellBtn.isEnabled = false;
        else
        {
            sellBtn.isEnabled = true;

            // illegale to sell
            if (false)
            {
                sellStatus = TradeStatus.illegal;
                sellBtn.defaultColor = new Color(0.8F, 0.1F, 0.05F, 1F);
                sellTween.to = sellBtn.defaultColor;
                Debug.Log("illegal to sell: " + itemName);
            }
            // loc out of funds
            // COMMODITY is available
            else
            {
                sellStatus = TradeStatus.available;
                sellBtn.defaultColor = Color.white;
                sellTween.to = sellBtn.defaultColor;
            }
        }

        // set BUY button's activity according to item quota
        if (LocationCommodityPrice.amountWillingToSell(LocationUI.Instance.currentLocation, itemName) == 0) buyBtn.isEnabled = false;
        else
        {
            // button enabled
            buyBtn.isEnabled = true;

            // player funds limit purchases
            if (WorldState.player.funds < price)
            {
                buyStatus = TradeStatus.tooExpensive;
                buyBtn.defaultColor = tooExpensive;
                buyTween.to = tooExpensive;
            }
            // illegal to buy
            else if (false)
            {
                buyStatus = TradeStatus.illegal;
                buyBtn.defaultColor = new Color(0.8F, 0.1F, 0.05F, 1F);
                buyTween.to = buyBtn.defaultColor;
                Debug.Log("illegal to buy: " + itemName);
            }
            // available
            else
            {
                buyStatus = TradeStatus.available;
                if (buyBtn.defaultColor != Color.white)
                {
                    buyBtn.defaultColor = Color.white;
                    buyTween.to = Color.white;
                }
                //Debug.Log(itemName+" available (color: "+buyWidget.color+")");
            }

        }
    }
    void updateLocationPricing()
    {
        // Check price
        price = LocationCommodityPrice.getPrice(LocationUI.Instance.currentLocation, itemName, false);
        // adjust colour
        float mul = LocationCommodityPrice.priceMultiplier(itemName, price);
        if (mul < 0.95) uiLabelPrice.text = "[990000]";									// red
        else if (mul < 1.05) uiLabelPrice.text = "[000000]";							// black
        else uiLabelPrice.text = "[00" + Mathf.Round(Mathf.Min(mul * 60 - 20, 99)) + "11]";	// greener

        // check if decimals
        price = Mathf.Round(price * 10) / 10;
        if (price % 1 == 0) uiLabelPrice.text += price.ToString();
        else uiLabelPrice.text += price.ToString("#.0");
        //else uiLabelPrice.text += price.ToString("#.00");
    }
*/
}
