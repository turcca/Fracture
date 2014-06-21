using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShipyardModuleManager : MonoBehaviour
{
    /*
    public static ShipyardModuleManager Instance;

    public ModuleDataClass linkedModule = null;

    public bool needsToUpdate;

    // UI buttons that expand to grids
    public ShipyardModuleGrid engineBtn;
    public ShipyardModuleGrid powerBtn;
    public ShipyardModuleGrid commandBtn;
    public ShipyardModuleGrid navigationBtn;
    public ShipyardModuleGrid utilityBtn;
    public ShipyardModuleGrid armorBtn;
    //public Dictionary<hardPoints, ShipyardModuleGrid> hardPointBtn;
    public ShipyardModuleGrid frontBtn;
    public ShipyardModuleGrid dorsalBtn;
    public ShipyardModuleGrid sideLBtn;
    public ShipyardModuleGrid sideRBtn;
    public ShipyardModuleGrid extension1Btn;
    public ShipyardModuleGrid extension2Btn;
    public ShipyardModuleGrid pdBtn;

    private List<ShipyardModuleGrid> buttons;

    // Expanded items as lists - references to ship modules
    public List<ModuleDataClass> modulesEngine;
    public List<ModuleDataClass> modulesPower;
    public List<ModuleDataClass> modulesCommand;
    public List<ModuleDataClass> modulesNavigation;
    public List<ModuleDataClass> modulesUtility;
    public List<ModuleDataClass> modulesArmor;
    public Dictionary<hardPoints, ModuleDataClass> modulesHardPoint;
    public Dictionary<hardPoints, MountClass> mounts;


    public class MountClass
    {
        public string name = null;
        public int size;
        public int hpSize;
        public hardPoints hardPoint;
        public bool isSensor;
        public WeaponClass weapon;
        public SensorClass sensor;
        // constructor
        public MountClass(string name, int hpSize, hardPoints hardPoint)
        {
            this.name = name;
            this.hpSize = hpSize; this.hardPoint = hardPoint;
            // get weapon or sensor data
            if (name != null)
            {
                if (WorldState.weaponDictionary.ContainsKey(name)) { this.weapon = WorldState.weaponDictionary[name]; isSensor = false; this.size = weapon.size; this.sensor = null; }
                else if (WorldState.sensorDictionary.ContainsKey(name)) { this.sensor = WorldState.sensorDictionary[name]; isSensor = true; this.size = sensor.size; this.weapon = null; }
                else Debug.LogError("ERROR: couldn't find '" + name + "' in weaponData or sensorData. [hardPoint: " + hardPoint.ToString() + "]");
            }
            // null input means nothing is mounted on this hardPoint
            else { weapon = null; sensor = null; }
        }
    }

    // Use this for initialization
    void Awake()
    {
        ShipyardModuleManager.Instance = this;

        modulesEngine = new List<ModuleDataClass>();
        modulesPower = new List<ModuleDataClass>();
        modulesCommand = new List<ModuleDataClass>();
        modulesNavigation = new List<ModuleDataClass>();
        modulesUtility = new List<ModuleDataClass>();
        modulesHardPoint = new Dictionary<hardPoints, ModuleDataClass>();
        modulesArmor = new List<ModuleDataClass>();

        mounts = new Dictionary<hardPoints, MountClass>();

        buttons = new List<ShipyardModuleGrid>(); // list buttons
        buttons.Add(engineBtn); buttons.Add(powerBtn); buttons.Add(commandBtn); buttons.Add(navigationBtn); buttons.Add(utilityBtn); buttons.Add(armorBtn); buttons.Add(frontBtn); buttons.Add(dorsalBtn); buttons.Add(sideLBtn); buttons.Add(sideRBtn); buttons.Add(extension1Btn); buttons.Add(extension2Btn); buttons.Add(pdBtn); buttons.Add(powerBtn);
    }

    void Start() { updateModuleLists();}


    public void onSelect(ModuleClass moduleGroup)
    {
        // go through all non-hardPoint buttons
        foreach (ShipyardModuleGrid btn in buttons)
        {
            if (btn.moduleGroup != ModuleClass.hardpoint)
            {
                // effectively hides the grid of others, except the one selected
                if (moduleGroup == btn.moduleGroup) btn.expandGrid(true);
                else btn.expandGrid(false);
            }
        }
    }



    void updateModuleLists() 	// tarvii updaten vain kun alusta vaihdetaan?
    {
        if (needsToUpdate)
        {
            needsToUpdate = false;

            // Clear lists
            modulesEngine.Clear();
            modulesPower.Clear();
            modulesCommand.Clear();
            modulesNavigation.Clear();
            modulesUtility.Clear();
            modulesHardPoint.Clear();
            modulesArmor.Clear();
            mounts.Clear();

            // Repopulate modules & hard points
            Component[] modules;
            modules = PlayerShip.Instance.playerShip.obj.GetComponentsInChildren<ModuleDataClass>();
            foreach (ModuleDataClass m in modules)
            {
                if (m.moduleClass == ModuleClass.engine) modulesEngine.Add(m);
                else if (m.moduleClass == ModuleClass.power) modulesPower.Add(m);
                else if (m.moduleClass == ModuleClass.utility) modulesUtility.Add(m);
                else if (m.moduleClass == ModuleClass.command) modulesCommand.Add(m);
                else if (m.moduleClass == ModuleClass.navigation) modulesNavigation.Add(m);
                else if (m.moduleClass == ModuleClass.armor) modulesArmor.Add(m);
                else if (m.moduleClass == ModuleClass.hardpoint)
                {
                    if (m.hardPoint != hardPoints.none)
                    {
                        // add hardpoint
                        if (!modulesHardPoint.ContainsKey(m.hardPoint)) modulesHardPoint.Add(m.hardPoint, m);
                        else Debug.LogError("ERROR: alrady loaded 'hardpoint' module to '" + m.hardPoint.ToString() + "'");
                    }
                    else Debug.LogError("ERROR: module with moduleClass: 'hardpoint' was tagged as hardPoint: 'none'");
                }
                else if (m.moduleClass != ModuleClass.mount) Debug.LogError("ERROR: updateModuleLists() couldn't classify a ModuleDataClass. ModuleType: " + m.moduleType);
            }

            // Repopulate mounts
            bool hasWeapon;
            WeaponClass w;
            SensorClass s;
            // get mounts and their capacity from shipType
            Dictionary<hardPoints, int> shipHps = ShipTemplatesReader.shipTypeTemplates[PlayerShip.Instance.playerShip.type].getHardPointDictionary();

            foreach (KeyValuePair<hardPoints, int> hp in shipHps)
            {
                // check weapon
                if (PlayerShip.Instance.playerShip.weapon.ContainsKey(hp.Key))
                {
                    w = PlayerShip.Instance.playerShip.weapon[hp.Key];
                    hasWeapon = true;
                    // add mount
                    mounts.Add(hp.Key, new MountClass(w.weaponName, hp.Value, hp.Key));
                }
                else hasWeapon = false;

                // check sensor
                if (PlayerShip.Instance.playerShip.sensor.ContainsKey(hp.Key))
                {
                    // check there is no double-mount
                    if (hasWeapon) { Debug.LogError("ERROR: player's ship has both weapon AND sensor mounted on " + hp.Key.ToString()); break; }

                    s = PlayerShip.Instance.playerShip.sensor[hp.Key];
                    // add mount
                    mounts.Add(hp.Key, new MountClass(s.sensorName, hp.Value, hp.Key));
                }
                // has neither, empty weapon mount
                else if (!hasWeapon) mounts.Add(hp.Key, new MountClass(null, hp.Value, hp.Key));
            }


            // update ShipyardModuleGrid buttons (re-create content)
            foreach (ShipyardModuleGrid btn in buttons)
            {
                btn.updateModules(true);
                // active elements: modules and available hardPoints
                if (btn.hardPoint == hardPoints.none || mounts.ContainsKey(btn.hardPoint)) btn.gameObject.SetActive(true);
                else btn.gameObject.SetActive(false);
            }

        }
    }

    void debugDictionaries()
    {
        foreach (MountClass m in mounts.Values)
        {
            Debug.Log(m.name + " [" + m.size + "/" + m.hpSize + "]  @ " + m.hardPoint);
        }
    }

    */

}
