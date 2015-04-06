using System.Collections;
using System.Collections.Generic;

public class Trade
{
    public static void transferAll(
        System.Collections.Generic.Dictionary<string,int> from,
        System.Collections.Generic.Dictionary<string,int> to)
    {
        foreach (KeyValuePair<string, int> commodity in from)
        {
            to[commodity.Key] += commodity.Value;
        }
        foreach (string commodity in Economy.getCommodityNames())
        {
            from[commodity] = 0;
        }
    }


    public static Location findBestImportLocation(List<string> importList, Location home)
    {
        List<Location> potentials = Game.universe.tradeNetwork.getNearestLocations(home);
        Location bestDestination = home;
        int maxScore = 0;
        for (int i = 0; i < 10; ++i)
        {
            int score = scoreImportPotential(importList, potentials[i]);
            if (score > maxScore)
            {
                bestDestination = potentials[i];
                maxScore = score;
            }
        }
        return bestDestination;
    }

    private static int scoreImportPotential(List<string> importList, Location location)
    {
        //List<string> exportList = location.stockpile.getExportList();
        //int score = 0;
        //for (int priority = 0; priority < importList.Count; ++priority)
        //{
        //    string want = importList[priority];
        //    for (int distance = 0; distance < exportList.Count; ++distance)
        //    {
        //        string offer = exportList[distance];
        //        if (offer == want)
        //        {
        //            score = score + (20 - priority) * (10 - distance);
        //        }
        //        if (distance > 10) break;
        //    }
        //    if (priority > 20) break;
        //}
        //return score;
        return 0;
    }

    internal static void tradeShipInventory(NPCShip ship, Location loc)
    {
        //List<string> shipWants = ship.wantedCommodityList;
        //foreach (string commodity in shipWants)
        //{
        //    while (loc.stockpile.tradable[commodity] > 0 && ship.inventory.getUsedCargoSpace() < ship.inventory.maxCargoSpace)
        //    {
        //        loc.stockpile.tradable[commodity]--;
        //        ship.inventory.commodities[commodity]++;
        //    }
        //    if (ship.inventory.getUsedCargoSpace() >= ship.inventory.maxCargoSpace) break;
        //}
    }
}
