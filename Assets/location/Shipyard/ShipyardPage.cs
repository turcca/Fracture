using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShipyardPage : MonoBehaviour 
{

    public string playerShipId;
    public string compareShipId;
    ShipStats playerShipStats;
    ShipStats compareShipStats;

    public GameObject buyShipPrefab;
    public GridLayoutGroup grid;
    public Button tradeButton;
    Text tradeButtonLabel;

    public List<Text> shipInfos = new List<Text>();

    
    void OnEnable () 
    {
        if (tradeButtonLabel == null) tradeButtonLabel = tradeButton.transform.GetChild(0).GetComponent<Text>();
        compareShipId = "";
        updateShipStats();
        if (compareShipId != "") updateShipStats(ShipStatsLibrary.getShipStat(compareShipId));
        populateBuyShipGrid();
    }

    public void clickCompareShip(string shipID)
    {
        updateShipStats(ShipStatsLibrary.getShipStat(shipID));
    }

    void updateShipStats(ShipStats compareShip = null)
    {
        playerShipStats = ShipStatsLibrary.getShipStat(playerShipId);

        if (playerShipStats == null)
        {
            Debug.LogError("Shipyard / shipStats / player ship stats not found!");
            return;
        }

        if (compareShip == null)
        {
            // update only ship values (initializing)
            setStatsValue(0);
            for (int i = 5; i < shipInfos.Count; i += 2)
            {
                setStatsValue(i);
            }
            // reset compare values
            resetStatsValue(1); // compare ship name
            for (int i = 6; i < shipInfos.Count; i += 2)
            {
                resetStatsValue(i);
            }
            // hide buy-button
            tradeButton.interactable = false;
            tradeButtonLabel.text = "Trade";
        }
        else
        {
            // update only compare values
            compareShipStats = compareShip;
            setStatsValue(1);
            for (int i = 6; i < shipInfos.Count; i += 2)
            {
                setStatsValue(i);
            }

            // show buy -button
            string toolTipInfo = "";
            tradeButton.interactable = true;
            // check player local rep & cash
            bool enoughRep  = (Root.game.player.playerReputation.getReputation(Root.game.player.getLocation()) >= compareShipStats.reqRelations);                   // enough relation capital 
            bool enoughCash = (Root.game.player.cargo.credits + playerShipStats.value() - compareShipStats.value() >= 0);                                           // enough cash
            bool enoughTech = (Root.game.player.getLocation().economy.technologies[compareShipStats.reqTechType].level >= compareShipStats.getRequiredTechLevel()); // enough tech

            if (enoughTech == false)
            {
                toolTipInfo += "Not enough tech. NOTE: How is this button even here?";
                tradeButton.interactable = false;
            }
            if (enoughRep == false)
            {
                if (tradeButton.IsInteractable() == false)
                    toolTipInfo += "\n";
                else
                    tradeButton.interactable = false;
                toolTipInfo += "Not enough reputation ("+ Mathf.Round(Root.game.player.playerReputation.getReputation(Root.game.player.getLocation())*10f)/10f + "/"+ (int)compareShipStats.reqRelations + ")";
            }
            if (enoughCash == false)
            {
                if (tradeButton.IsInteractable() == false)
                    toolTipInfo += "\n";
                else
                    tradeButton.interactable = false;
                toolTipInfo += "Not enough cash.";
            }
            tradeButton.GetComponent<ToolTipScript>().toolTip = toolTipInfo;
     
            tradeButtonLabel.text = (playerShipStats.value() - compareShipStats.value() > 0f) ? "Trade: +" : "Trade: ";
            tradeButtonLabel.text += formatNumberToString( playerShipStats.value() - compareShipStats.value() );
            if (tradeButton.IsInteractable())
                tradeButtonLabel.color = new Color(1, 1, 1);
            else
                tradeButtonLabel.color = new Color(.6f, .6f, .6f);
        }
    }
    void resetStatsValue(int i)
    {
        shipInfos[i].text = "";
    }
    void setStatsValue(int i)
    {
        KeyValuePair<string, Color32> pair = getStringByObjectName(shipInfos[i].gameObject.name);
        shipInfos[i].text = pair.Key;
        shipInfos[i].color = pair.Value;
    }


    KeyValuePair<string, Color32> getStringByObjectName(string objName)
    {
        //ShipStats playerShipStats = ShipStatsLibrary.getShipStats(playerShipId);
        //KeyValuePair<string, Color> rv;// = new KeyValuePair<string, Color>();
        string same = "-";

        string rs = "";
        Color32 rc = new Color32(215, 225, 230, 150);
        bool? check = null;

        Color32 red = new Color32(160,60,40,255);
        Color32 green = new Color32(65,160,40,255);
        Color32 grey = new Color32(215, 225, 230, 150);

        switch (objName)
        {
            case "CLASS":
                rs =  playerShipStats.shipId;
                rc = new Color32(215, 225, 230, 255);
                break;
            case "compare class":
                rs =  "  / "+ compareShipStats.shipId;
                rc = grey;
                break;

            case "dam value":
                rs =  "0 %"; // TODO: access player's ship data / damage
                break;
            case "S cost":
                rs =  same; // calculate cost
                break;
            case "S time":
                rs =  same; // calculate repair time
                break;

            case "S value":
                rs =  formatNumberToString(playerShipStats.value());
                break;
            case "C value":
                check = compareStats(playerShipStats.value(), compareShipStats.value(), true);
                rs = check == null ? same : formatNumberToString(compareShipStats.value());
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S size":
                rs =  formatNumberToString(playerShipStats.size);
                break;
            case "C size":
                check = compareStats(playerShipStats.size, compareShipStats.size, true);
                rs = check == null ? same : formatNumberToString(compareShipStats.size);
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S volume":
                rs =  formatNumberToString(playerShipStats.exteriorVolume());
                break;
            case "C volume":
                check = compareStats(playerShipStats.exteriorVolume(), compareShipStats.exteriorVolume(), true);
                rs = check == null ? same : formatNumberToString(compareShipStats.exteriorVolume());
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S front":
                rs =  formatNumberToString(playerShipStats.signatureFront());
                break;
            case "C front":
                check = compareStats(playerShipStats.signatureFront(), compareShipStats.signatureFront(), false);
                rs = check == null ? same : formatNumberToString(compareShipStats.signatureFront());
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S side":
                rs =  formatNumberToString(playerShipStats.signatureSide());
                break;
            case "C side":
                check = compareStats(playerShipStats.signatureSide(), compareShipStats.signatureSide(), false);
                rs = check == null ? same : formatNumberToString(compareShipStats.signatureSide());
                rc = check == null ? grey : (bool)check ? green : red;
                break;

            case "S hp value":
                rs = formatNumberToString(playerShipStats.hpVal());
                break;
            case "C hp value":
                check = compareStats(playerShipStats.hpVal(), compareShipStats.hpVal(), true);
                rs = check == null ? same : formatNumberToString(compareShipStats.hpVal());
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S utility":
                rs =  formatNumberToString(playerShipStats.utility);
                break;
            case "C utility":
                check = compareStats(playerShipStats.utility, compareShipStats.utility, true);
                rs = check == null ? same : formatNumberToString(compareShipStats.utility);
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S cargo":
                rs =  formatNumberToString(playerShipStats.cargo);
                break;
            case "C cargo":
                check = compareStats(playerShipStats.cargo, compareShipStats.cargo, true);
                rs = check == null ? same : formatNumberToString(compareShipStats.cargo);
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S speed":
                rs =  formatNumberToString(playerShipStats.speed());
                break;
            case "C speed":
                check = compareStats(playerShipStats.speed(), compareShipStats.speed(), true);
                rs = check == null ? same : formatNumberToString(compareShipStats.speed());
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S hull":
                rs = formatNumberToString(playerShipStats.hull());
                break;
            case "C hull":
                check = compareStats(playerShipStats.hull(), compareShipStats.hull(), true);
                rs = check == null ? same : formatNumberToString(compareShipStats.hull());
                rc = check == null ? grey : (bool)check ? green : red;
                break;
            case "S crew":
                rs =  formatNumberToString(playerShipStats.crewCapacity());
                break;
            case "C crew":
                check = compareStats(playerShipStats.crewCapacity(), compareShipStats.crewCapacity(), true);
                rs = check == null ? same : formatNumberToString(compareShipStats.crewCapacity());
                rc = check == null ? grey : (bool)check ? green : red;
                break;

            default:
                Debug.LogError("Invalid shipYard shipStatsInfo input object: " + objName);
                break;
        }

        return new KeyValuePair<string, Color32>(rs, rc);
    }

    bool? compareStats(int playerShipValue, int compareShipValue, bool higherComparisonIsGreen)
    {
        if (compareShipValue > playerShipValue) return higherComparisonIsGreen;
        else if (compareShipValue < playerShipValue) return !higherComparisonIsGreen;
        else return null;
    }
    bool? compareStats(float playerShipValue, float compareShipValue, bool higherComparisonIsGreen)
    {
        if (compareShipValue > playerShipValue) return higherComparisonIsGreen;
        else if (compareShipValue < playerShipValue) return !higherComparisonIsGreen;
        else return null;
    }
    

    string formatNumberToString(int amount) { return formatNumberToString((float)amount); }
    string formatNumberToString(float amount)
    {
        string str = amount.ToString();
        if (amount >= 10000f)
            str = str.Insert(2, " ");
        else if (amount >= 1000f)
            str = str.Insert(1, " ");

        return str;
    }
    


    void populateBuyShipGrid()
    {
        Location location = Root.game.player.getLocation();
        if (buyShipPrefab == null) { Debug.LogError("add 'buy ship Btn' prefab to ShipyardPanel / ShipyardPage"); return; }
        if (location == null) { Debug.LogError("no location!"); return; }
        else
        {
            // remove old buttons
            Button[] removeButtons = grid.GetComponentsInChildren<Button>(true);
            foreach (Button btn in removeButtons) Destroy(btn.gameObject);
        }

        int btnCount = 0;

        // prefab buttons for availabler ships
        foreach (KeyValuePair<string, ShipStats> shipKvP in ShipStatsLibrary.getShipStats())
        {
            //Debug.Log("button "+shipKvP.Key+" \n tech (ship req/location): " + shipKvP.Value.reqTechType.ToString() + " (" + shipKvP.Value.getRequiredTechLevel() + " / " + location.economy.technologies[shipKvP.Value.reqTechType].level + ")");
            
            // pre-requisists for ship items to appear: needs to be a ship (not rto), not palyerShip, and available to the location
            if (shipKvP.Value.type == "ship" 
                && shipKvP.Key != playerShipId 
                && location.economy.technologies[shipKvP.Value.reqTechType].level >= shipKvP.Value.getRequiredTechLevel())
            {
                GameObject btnObj = Instantiate<GameObject>(buyShipPrefab);
                btnObj.name = "compare " + shipKvP.Value.shipId;
                btnObj.GetComponentInChildren<Text>().text = shipKvP.Value.shipId;
                Button btn = btnObj.GetComponent<Button>();

                // COLOR
                // not enough relation capital 
                if (Root.game.player.playerReputation.getReputation(Root.game.player.getLocation()) < shipKvP.Value.reqRelations)
                    btn.gameObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
                // not enough money
                if ((float)playerShipStats.value() + Root.game.player.cargo.credits < (float)shipKvP.Value.value())
                    btn.gameObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);

                //btn.interactable =
                //    (Root.game.player.playerReputation.getReputation(Root.game.player.getLocation()) < shipKvP.Value.reqRelations) ? false :    // not enough relation capital 
                //    ((float)playerShipStats.value() + Root.game.player.cargo.credits < (float)shipKvP.Value.value()) ? false :                  // not enough money
                //    true;

                // tooltip
                btnObj.GetComponent<ToolTipScript>().toolTip = shipKvP.Value.toolTip;

                // link listener (/w lamda, parameter shipID)
                btn.onClick.RemoveAllListeners();
                string key = shipKvP.Key; // need to be converted to string, and not refer to the dictionary entry
                btn.onClick.AddListener(() => { this.clickCompareShip(key); } );

                btnObj.gameObject.transform.SetParent(grid.transform);
                btnCount++;
            }
        }
        if (btnCount > 7)
        {
            // switch to horizontal grid when overfloading
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        }
    }

}
