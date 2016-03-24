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
    
    public string shipId { get; private set; } // SAVE
    public string shipName { get; private set; }// SAVE
    ShipStats getShipStats() { return ShipStatsLibrary.getShipStat(shipId); }

    List<Utility> utilities = new List<Utility>(); // SAVE
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
        addUtility(Utility.FloodGenerators);
        addUtility(Utility.CoreBypass);
        addUtility(Utility.WeaponsRerouting);
        addUtility(Utility.CargoSpace);
        Debug.Log("TODO: added manual ship utilities. ("+utilities.Count+" / "+ getShipStats().utility + ")");
    }

    public string getShipTypeAndName()
    {
        return shipId + " " + shipName;
    }
	public List<Utility> getUtilities()
    {
        return utilities;
    }
    /// <summary>
    /// returns availability of adding the util, or empty util slots when prompted with Empty parameter
    /// </summary>
    /// <param name="utility"></param>
    /// <returns></returns>
    public bool addUtility(Utility utility = Utility.Empty)
    {
        if (utility == Utility.Empty || utility == Utility.CargoSpace || utilities.Contains(utility) == false)
        {
            if (utilities.Count < getShipStats().utility)
            {
                if (utility != Utility.Empty) utilities.Add(utility);
                return true;
            }
            else if (utility != Utility.Empty) Debug.LogWarning("Adding utility '" + utility.ToString() + "' but already maxed out on utility slots: " + getShipStats().utility);
        }
        else Debug.LogWarning("utility already exists: " + utility.ToString());
        return false;
    }
    public int getMaxCargoSpace()
    {
        int ri = getShipStats().cargo;
        foreach (var util in utilities)
            if (util == Utility.CargoSpace) ri++;
        return ri;
    }
}
