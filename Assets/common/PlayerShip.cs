using UnityEngine;
using System.Collections.Generic;

public enum Utility
{
    Empty,
    CargoSpace,
    CrewRecreation,
    FloodGenerators,
    Barracks,
    SecurityCenter,
    WeaponCoreTap,
    WeaponCapacitors,
    CoreInsulation,
    EngineInsulation,
    Collectors,
    TheRoundRoom,
    WeaponsRerouting,
    CoreBypass,
    Chapel,
    School,
    CivilianComms,
    ShipBazaar
}

public class PlayerShip
{
    
    public string shipId { get; private set; }
    public string shipName { get; private set; }
    public ShipStats shipStats() { return ShipStatsLibrary.getShipStat(shipId); }

    List<Utility> utilities = new List<Utility>();
    //Dictionary<hardpoints, mount> hardpoints = new Dictionary<hardpoints, mount>();
    //Dictionary<string, mount> RTO = new Dictionary<string, mount>();

    public PlayerShip(string shipId)
    {
        this.shipId = (shipId != null) ? shipId : "";

        // format utilities
        /*
        for (int i = 0; i < shipStats().utility; i++)
        {
            addUtility(Utility.Empty);
        }
        */
        addUtility(Utility.CivilianComms);
        addUtility(Utility.CrewRecreation);
        addUtility(Utility.CoreBypass);
        addUtility(Utility.CargoSpace);
        Debug.Log("TODO: added manual ship utilities. ("+utilities.Count+" / "+ shipStats().utility + ")");
    }

    public string getShipTypeAndName()
    {
        return "";
    }
	public List<Utility> getUtilities()
    {
        return utilities;
    }
    public void addUtility(Utility utility)
    {
        if (!utilities.Contains(utility))
        {
            if (utilities.Count < shipStats().utility)
            {
                utilities.Add(utility);
            }
            else Debug.LogWarning("Adding utility '" + utility.ToString() + "' but already maxed out on utility slots: " + shipStats().utility);
        }
        else Debug.LogWarning("Trying to add an utility that already exists: " + utility.ToString());
    }

}
