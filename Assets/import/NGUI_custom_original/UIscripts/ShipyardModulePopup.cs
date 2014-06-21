using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipyardModulePopup : MonoBehaviour
{
    /*
    public string moduleTypeName;

    public UIPopupList uiPopupList;
    public UILabel label;
    public UISprite sprite;

    public ModuleDataClass module;

    // Use this for initialization
    void Awake()
    {

    }
    void Start()
    {
        //changeLabel(uiPopupList.value);
    }

    public void loadModule(ModuleDataClass module)
    {
        if (module != null)
        {
            this.module = module;
            // trigger selectionChange()
            moduleTypeName = module.moduleType.ToString();
            uiPopupList.value = moduleTypeName;
            this.gameObject.SetActive(false);
            changeLabel(moduleTypeName);
            loadPopupList();
        }
        else Debug.LogError("ERROR: loadModule was null.");
    }

    // **********************************************

    public void selectionChange()
    {
        // only on active buttons // would .activeInHierarchy be more appropriate here?
        if (this.gameObject.activeSelf)
        {
            setValue(uiPopupList.value);
        }
    }

    public void setValue(string value)
    {
        if (value != moduleTypeName)
        {
            //uiPopupList.value = value; // will loop! causes selectionChange?
            moduleTypeName = value;
            changeLabel(value);
            // change module on ship
            changeModule(moduleTypeName);

            loadPopupList();
            Debug.Log("set value to " + uiPopupList.value);
        }
    }


    void loadPopupList()
    {
        if (module != null)
        {
            uiPopupList.items.Clear();
            // collect possible modueTypes to items
            List<ModuleTypes> availableItems = WorldState.getModuleTypesList(module.moduleClass);
            foreach (ModuleTypes t in availableItems)
            {
                if (t != module.moduleType) // don't add moduleType that's already fitted on the module
                {
                    uiPopupList.items.Add(t.ToString());
                }
            }
        }
        else Debug.LogError("ERROR: module is null.");
    }
    void changeLabel(string txt)
    {
        label.text = txt;
    }


    void changeModule(string newModuleTypeName)
    {
        // attempt to convert moduleTypeName to ModuleTypes enum
        ModuleTypes newModuleType = (ModuleTypes)System.Enum.Parse(typeof(ModuleTypes), newModuleTypeName);
        if (newModuleType == ModuleTypes.none) Debug.LogError("ERROR: newModuleType translated to 'none'.");

        PlayerShip.Instance.playerShip.changeModule(module, newModuleType);
    }

    // *******************************************************************************************

    void OnHover(bool isOver)
    {
        // highlight PlayerShip / ShipDataClass / ModuleDataClass module in view
        if (isOver) module.obj.transform.renderer.material.color = Color.white;
        else module.obj.transform.renderer.material.color = Color.grey;
    }
*/
}
