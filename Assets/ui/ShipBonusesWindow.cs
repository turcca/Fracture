using UnityEngine;
using System;
using System.Collections;

public class ShipBonusesWindow : MonoBehaviour
{
    public bool needsUpdate = true;
    bool needsResize = false;

    public RectTransform mainWindow;

    public ShipStatBonusUI crewGrid;
    public ShipStatBonusUI shipGrid;
    public ShipStatBonusUI fractureGrid;
    public ShipStatBonusUI relationsGrid;

    Animator animator;
    

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    public void showShipStats()
    {
        if (needsUpdate)
        {
            populateBonuses();
            needsUpdate = false;
            needsResize = true;
        }
        // pause
        if (GameState.isState(GameState.State.Pause) == false) GameState.requestState(GameState.State.Pause); // PROBLEM: sometimes mouse-trigger hits twice, and applies 2 pauses on the stack
        // animate in
        animator.SetBool("showPanel", true);
    }
    public void hide()
    {
        // unpause
        //while (GameState.getState() == GameState.State.Pause) // ? HAX : [remove all pauses] I wonder if this will screw up 'pause' some times
        //{
            GameState.returnFromState(GameState.State.Pause);
        //}
        // animate out
        animator.SetBool("showPanel", false);
    }
    public void toggleShipStats()
    {
        //Debug.Log("animator.showPanel = " + animator.GetBool("showPanel"));
        if (animator.GetBool("showPanel"))
            hide();
        else
            showShipStats();
    }

    public static ShipBonusesWindow get()
    {
        GameObject topObj = GameObject.Find("TopWindow");
        if (topObj == null)
        {
            Debug.LogError("ERROR: no 'TopWindow' in the scene");
            return null;
        }

        ShipBonusesWindow top = null;
        if (topObj)
        {
            top = topObj.GetComponent<ShipBonusesWindow>();
            if (top == null) Debug.LogError("ERROR: TopWindow was not found");
        }
        return top;
    }


    void populateBonuses()
    {
        ShipStatBonusUI[] grids = { crewGrid, shipGrid, fractureGrid, relationsGrid };
        ShipBonusCathegory[] cathegories = (ShipBonusCathegory[])Enum.GetValues(typeof(ShipBonusCathegory)); // 0 = none

        int [] oldChildren = new int[grids.Length];
        int [] newChildren = new int[grids.Length];

        // update bonus items
        Root.game.player.shipBonuses.updateItems();
        Debug.Log("populateBonuses()");

        
        for (int i = 0; i < grids.Length; i++) //(ShipStatBonusUI group in grids)
        {
            // update cathegory stats & toolTip
            grids[i].initialize(cathegories[i+1], Root.game.player.shipBonuses.getTotalBonus(cathegories[i+1]));

            // calculate previous game objects under grids
            oldChildren[i] = grids[i].gameObject.transform.childCount;

            foreach (RectTransform item in grids[i].gameObject.GetComponentsInChildren<RectTransform>())
            {
                if (item.gameObject != grids[i].gameObject) Destroy(item.gameObject);
            }
        }

        // populate new game objects by bonus ShipBonus
        GameObject bonusPrefab = Resources.Load<GameObject>("ui/prefabs/shipStatBonus");
        GameObject bonusPrefabArchtype = Resources.Load<GameObject>("ui/prefabs/shipStatBonusArchtype");

        foreach (ShipBonus bonus in Enum.GetValues(typeof(ShipBonus)))
        {
            if (bonus != ShipBonus.None)
            {
                // pre-arch empty space
                spawnBonusItem(new ShipBonusItem(bonus));

                // arch type
                spawnBonusItem(new ShipBonusItem(bonus), bonusPrefabArchtype);

                foreach (ShipBonusItem bonusItem in Root.game.player.shipBonuses.getItems(bonus))
                {
                    spawnBonusItem(bonusItem, bonusPrefab);
                }
            }
        }

        // resize height for MainWindow (need to discount old children, because Destroy hasn't taken place yet)
        for (int i = 0; i < grids.Length; i++)
        {
            newChildren[i] = grids[i].gameObject.transform.childCount - oldChildren[i];
        }
        
        mainWindow.sizeDelta = new Vector2(mainWindow.sizeDelta.x,
            (int)Mathf.Max(newChildren)                                         // max items in columns
            * (crewGrid.getGrid().cellSize.y + crewGrid.getGrid().spacing.y)    // colum item height
            + 60f);                                                             // top+bottom -borders + cathegory-item height
        

        //Debug.Log("resized TopWindow to " + mainWindow.sizeDelta.y+"\n"+
        //    "(childCount: " + newChildren[0] +") [old: "+oldChildren[0]+"]"+"\n" +
        //    "(childCount: " + newChildren[1] + ") [old: " + oldChildren[1] + "]" + "\n" +
        //    "(childCount: " + newChildren[2] + ") [old: " + oldChildren[2] + "]" + "\n" +
        //    "(childCount: " + newChildren[3] + ") [old: " + oldChildren[3] + "]"
        //    );
    }
    void spawnBonusItem(ShipBonusItem bonusItem, GameObject bonusPrefab = null)
    {
        if (bonusPrefab == null)
        {
            GameObject skip = new GameObject("pre-arch separator", typeof(RectTransform));
            skip.transform.SetParent(getParentObject(bonusItem.bonusType).transform);
            return;
        }
        // instantiate
        GameObject obj = Instantiate<GameObject>(bonusPrefab);
        
        // set parent
        obj.transform.SetParent(getParentObject(bonusItem.bonusType).transform);
        obj.name = "shipStatBonus: " + bonusItem.text;
        // initialize prefab values
        obj.GetComponent<ShipStatBonusUI>().initialize(bonusItem);
        // tool tip
        ToolTipScript tip = obj.AddComponent<ToolTipScript>();
        tip.toolTip = bonusItem.source;
    }
    

    GameObject getParentObject(ShipBonus shipBonus)
    {

        switch (ShipBonuses.getBonusCathegory(shipBonus))
        {
            case ShipBonusCathegory.None:
                Debug.LogError("trying to find parent object for bonus: " + shipBonus + " but its cathegory is 'None'");
                return null;
            case ShipBonusCathegory.Crew:
                return crewGrid.gameObject;
            case ShipBonusCathegory.Ship:
                return shipGrid.gameObject;
            case ShipBonusCathegory.Fracture:
                return fractureGrid.gameObject;
            case ShipBonusCathegory.Relations:
                return relationsGrid.gameObject;
            default:
                Debug.LogError("trying to find parent object for bonus: " + shipBonus + " but it fell through");
                return null;
        }
    }
}
