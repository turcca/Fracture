using System.Collections;
using System.Collections.Generic;

namespace Simulation
{
    public class Trade
    {
        public static void tradeResources(NPCShip ship)
        {
            foreach (Data.TradeItem item in ship.tradeList)
            {
                if (item.amount > 0.0f)
                {
                    if (item.isExported)
                    {
                        ship.home.economy.export(item.type, item.amount);
                        ship.destination.economy.import (item.type, item.amount);
                    }
                    else
                    {
                        ship.home.economy.import(item.type, item.amount);
                        ship.destination.economy.export(item.type, item.amount);
                    }
                }
            }
            ship.home.economy.updateTradeItems();
            ship.destination.economy.updateTradeItems();
        }
        // if ship is lost or loses cargo in transit, reverse retroactively trades
        public static void reverseTradeResources(NPCShip ship)
        {
            foreach (Data.TradeItem item in ship.tradeList)
            {
                if (item.amount > 0.0f)
                {
                    if (item.isExported)
                    {
                        if (ship.isGoingToDestination) // ship is still en route to destination and hasn't delivered exports
                        {
                            ship.home.economy.import(item.type, item.amount);
                            ship.destination.economy.export (item.type, item.amount);
                        }
                    }
                    else
                    {
                        ship.home.economy.export(item.type, item.amount);
                        ship.destination.economy.import(item.type, item.amount);
                    }
                }
            }
            ship.home.economy.updateTradeItems();
            ship.destination.economy.updateTradeItems();
        }

    }
}
