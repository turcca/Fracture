using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCShip
{
    public Location home;
    public Location destination;
    public Vector3 position;
    public string captain;
    public List<string> wantedCommodityList = new List<string>();
    public CommodityInventory inventory = new CommodityInventory();

    List<NavNode> navPoints = new List<NavNode>();

    public NPCShip(Location homeLocation)
    {
        home = homeLocation;
        position = home.position;
        destination = home;
        //captain = NameGenerator.getName(home.faction.getStrongest());
        //wantedCommodityList = home.stockpile.getImportList();

        Location[] arr = new Location[Root.game.locations.Count];
        Root.game.locations.Values.CopyTo(arr, 0);
        embarkTo(arr[Random.Range(0, arr.Length - 1)]);
    }

    public void tick(float days)
    {
        if (navPoints.Count == 0) return;

        Vector3 dir = navPoints[0].position - position;
        if ((dir.normalized * days).magnitude > dir.magnitude)
        {
            navPoints.RemoveAt(0);
            if (navPoints.Count == 0)
            {
                arrived();
            }
        }
        else
        {
            position = position + dir.normalized * days;
        }
    }

    private void arrived()
    {
        if (destination == home)
        {
            // transfer loot
            //Trade.transferAll(inventory.commodities, home.stockpile.commodities);

            // and set off again
            //wantedCommodityList = home.stockpile.getImportList();
            //Location dest = Trade.findBestImportLocation(wantedCommodityList, home);
            //embarkTo(dest);
        }
        else
        {
            // trade
            Trade.tradeShipInventory(this, destination);

            // and return to home
            embarkTo(home);
        }
    }

    public void embarkTo(Location to)
    {
        navPoints = Root.game.tradeNetwork.getPath(Root.game.tradeNetwork.getNavNodeFor(destination),
                                                       Root.game.tradeNetwork.getNavNodeFor(to));
        destination = to;
    }
}
