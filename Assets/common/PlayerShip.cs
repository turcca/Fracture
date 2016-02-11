using UnityEngine;
using System.Collections.Generic;

enum Utility
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
    public ShipStats shipStats() { return ShipStatsLibrary.getShipStat(shipId); }

    List<Utility> utilities = new List<Utility>();
    //Dictionary<hardpoints, mount> hardpoints = new Dictionary<hardpoints, mount>();
    //Dictionary<string, mount> RTO = new Dictionary<string, mount>();

    public PlayerShip(string shipId)
    {
        this.shipId = (shipId != null) ? shipId : "";

        // format utilities
        for (int i = 0; i < shipStats().utility; i++)
        {
            utilities.Add(Utility.Empty);
        }
	}
	

}
