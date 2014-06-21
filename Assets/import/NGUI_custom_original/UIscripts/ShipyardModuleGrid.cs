using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipyardModuleGrid : MonoBehaviour
{
/*
    public ModuleClass moduleGroup; 	// holds these moduleClasses
    public hardPoints hardPoint; 		// if modulegroup = mount, has to know hardPoint

    public UIGrid uiGrid;
    public UIButton uiButton;
    public UISprite uiSprite;

    static GameObject ShipyardModuleBtn;

    // Use this for initialization
    void Awake()
    {
        if (moduleGroup == ModuleClass.none) Debug.LogError("ERROR: moduleGroup not set in editor: " + this.gameObject.name);
        if (ShipyardModuleBtn == null) ShipyardModuleBtn = Resources.Load<GameObject>("Prefabs/UI_elements/ShipyardModuleBtn");
    }


    void OnClick()
    {
        ShipyardModuleManager.Instance.onSelect(moduleGroup);
    }

    public void expandGrid(bool activate)
    {
        Component[] allChildren = gameObject.GetComponentsInChildren<ShipyardModulePopup>(true);
        int count = allChildren.Length;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                allChildren[i].gameObject.SetActive(activate);
            }
        }
        uiGrid.repositionNow = true;
    }


    public void updateModules(bool activate)
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
            //itemBtn.Clear();
        }

        // if activating: create prefab buttons for each character in list
        if (activate)
        {
            createShipyardModulesForGrid();
        }

        // reposition grid
        uiGrid.repositionNow = true;
    }

    void createShipyardModulesForGrid()
    {

        // determin content by moduleGroup <ModuleClass>
        if (moduleGroup == ModuleClass.none)
        {
            Debug.LogError("ERROR: moduleGroup was \"none\"."); return;
        }

        else if (moduleGroup == ModuleClass.engine)
        {
            foreach (ModuleDataClass m in ShipyardModuleManager.Instance.modulesEngine)
            {
                createModuleBtnInstance(m);
            }
        }

        else if (moduleGroup == ModuleClass.power)
        {
            foreach (ModuleDataClass m in ShipyardModuleManager.Instance.modulesPower)
            {
                createModuleBtnInstance(m);
            }
        }

        else if (moduleGroup == ModuleClass.utility)
        {
            foreach (ModuleDataClass m in ShipyardModuleManager.Instance.modulesUtility)
            {
                createModuleBtnInstance(m);
            }
        }

        else if (moduleGroup == ModuleClass.command)
        {
            foreach (ModuleDataClass m in ShipyardModuleManager.Instance.modulesCommand)
            {
                createModuleBtnInstance(m);
            }
        }

        else if (moduleGroup == ModuleClass.navigation)
        {
            foreach (ModuleDataClass m in ShipyardModuleManager.Instance.modulesNavigation)
            {
                createModuleBtnInstance(m);
            }
        }

        else if (moduleGroup == ModuleClass.armor)
        {
            foreach (ModuleDataClass m in ShipyardModuleManager.Instance.modulesArmor)
            {
                createModuleBtnInstance(m);
            }
        }

        // hardPoints
        else if (moduleGroup == ModuleClass.hardpoint)
        {

        }
    }
    void createModuleBtnInstance(ModuleDataClass m)
    {
        if (m != null)
        {
            // create button as child
            GameObject btn = NGUITools.AddChild(this.gameObject, ShipyardModuleBtn);
            ShipyardModulePopup button = btn.GetComponent<ShipyardModulePopup>();

            // set  values
            button.loadModule(m);
        }
        else Debug.LogError("ERROR: module was null. Start debugging from createShipyardModulesForGrid() ");
    }
 */
}
