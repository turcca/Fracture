using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NewEconomy
{
    public class LocationTrade
    {
        // this is one of the methods that gets called a lot during trade, so some optimation is good
        public List<Data.TradeItem> getTradeList(Location fromLocation, Location toLocation/*, NPCShip ship*/)
        {
            List<Data.TradeItem> sortedItems = new List<Data.TradeItem>();

            // make trade list
            int itemCount = fromLocation.economy.tradeItems.Count-1;
            for (int i = 0; i > itemCount; i++)
            {
                sortedItems.Add(getResolvedItem(fromLocation.economy.tradeItems[i], toLocation.economy.tradeItems[i]));
            }
            // Sort list by weights (trade value to both locations)
            sortedItems.Sort(
                delegate(Data.TradeItem first,
                     Data.TradeItem next)
                {
                return first.weight.CompareTo(next.weight);
                }   
            );
            // go through list and allocate trade amounts to and from according to capacity
            // calculate score
            float score = 0;
            float cargoCapacity = 10.0f; // todo: use from ship data
            float exportCapacity = cargoCapacity;
            float importCapacity = cargoCapacity;

            foreach (Data.TradeItem item in sortedItems)
            {
                if (item.weight > 0.0f)
                {
                    if (item.isExported)
                    {
                        item.amount = item.amount > exportCapacity ? 
                    }
                    else
                    {

                    }
                }
            }
            
            
            return sortedItems;
        }
    
        internal Data.TradeItem getResolvedItem(Data.TradeItem fromItem, Data.TradeItem toItem)
        {
            Data.TradeItem resolvedItem = new Data.TradeItem();

            // exporting
            if (fromItem.isExported && !toItem.isExported)
            {
                resolvedItem.amount = Mathf.Min(fromItem.amount, toItem.amount);
                if (resolvedItem.amount > 0.0f)
                {
                    resolvedItem.isExported = true;
                    resolvedItem.type = fromItem.type;
                    resolvedItem.weight = fromItem.weight * toItem.weight;
                }
            }
            //importing
            else if (toItem.isExported && !fromItem.isExported)
            {
                resolvedItem.amount = Mathf.Min(fromItem.amount, toItem.amount);
                if (resolvedItem.amount > 0.0f)
                {
                    resolvedItem.isExported = false;
                    resolvedItem.type = fromItem.type;
                    resolvedItem.weight = fromItem.weight * toItem.weight;
                }
            }
            // else weight = 0, which means it doesn't qualify as trade item
            return resolvedItem;
        }
    }
}

