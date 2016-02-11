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

    public List<Text> shipInfos = new List<Text>();

    
    void OnEnable () 
    {
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
            for (int i = 6; i < shipInfos.Count; i += 2)
            {
                resetStatsValue(i);
            }
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
                rs =  playerShipStats.name;
                rc = new Color32(215, 225, 230, 255);
                break;
            case "compare class":
                rs =  "  / "+ compareShipStats.name;
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
                rs =  formatNumberToString(playerShipStats.crew());
                break;
            case "C crew":
                check = compareStats(playerShipStats.crew(), compareShipStats.crew(), true);
                rs = check == null ? same : formatNumberToString(compareShipStats.crew());
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
            if (shipKvP.Value.type == "ship" 
                && shipKvP.Key != playerShipId 
                && location.economy.technologies[shipKvP.Value.reqTechType].level >= shipKvP.Value.getRequiredTechLevel())
            {
                GameObject btn = Instantiate<GameObject>(buyShipPrefab);
                btn.name = "compare " + shipKvP.Value.name;
                btn.GetComponentInChildren<Text>().text = shipKvP.Value.name;

                // COLOR
                // not enough relation capital 
                if (0f < shipKvP.Value.reqRelations)                   // TODO: link to location relations
                    btn.gameObject.GetComponent<Image>().color = new Color(0.3f, 0.15f, 0.15f);
                // not enough money
                /*else */if ((float)playerShipStats.value() + Root.game.player.cargo.credits < (float)shipKvP.Value.value())
                    btn.gameObject.GetComponent<Image>().color = new Color(0.15f, 0.15f, 0.3f);

                // tooltip
                btn.GetComponent<ToolTipScript>().toolTip = shipKvP.Value.toolTip;

                // link listener (/w lamda, parameter shipID)
                btn.GetComponent<Button>().onClick.RemoveAllListeners();
                string key = shipKvP.Key; // need to be converted to string, and not refer to the dictionary entry
                btn.GetComponent<Button>().onClick.AddListener(() => { this.clickCompareShip(key); } );

                btn.gameObject.transform.SetParent(grid.transform);
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
