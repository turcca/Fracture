using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NewEconomy
{
    public class LocationTrade
    {
        public static KeyValuePair<Location, List<Data.TradeItem>> getTradePartnerForShip (Location home/*, todo: NPCShip ship - if ship knows its Location it's enough*/)
        {
            KeyValuePair<float, List<Data.TradeItem>> candidate = new KeyValuePair<float, List<Data.TradeItem>>();
            List<Data.TradeItem> bestPartnerTradeList = new List<Data.TradeItem>();
            Location bestPartner = home;
            float bestScore = 0.0f;

            foreach(Location partner in Root.game.locations.Values)
            {
                if (partner != home)
                {
                    candidate = scoreTradeListPair(home, partner);
                    if (candidate.Key > bestScore)
                    {
                        bestScore = candidate.Key;
                        bestPartner = partner;
                        bestPartnerTradeList = candidate.Value;
                    }
                }
            }
            return new KeyValuePair<Location, List<Data.TradeItem>>(bestPartner, bestPartnerTradeList);
        }

        // score (needs to be divided by distance*2 and compared against other location pairs) / trade list for ship
        // this is one of the methods that gets called a lot during trade, so some optimation is good
        static KeyValuePair<float, List<Data.TradeItem>> scoreTradeListPair(Location fromLocation, Location toLocation/*, float NPCShip.cargoHold*/)
        {
            List<Data.TradeItem> sortedItems = new List<Data.TradeItem>();
            float cargoCapacity = 10.0f; // todo: use from ship data

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
            float exportCapacity = cargoCapacity;
            float importCapacity = cargoCapacity;

            foreach (Data.TradeItem item in sortedItems)
            {
                if (item.weight > 0.0f)
                {
                    if (item.isExported)
                    {
                        if (exportCapacity > 0.0f)
                        {
                            if (item.amount <= exportCapacity)
                            {
                                exportCapacity -= item.amount;
                                score += (item.amount * item.weight);
                            }
                            else
                            {
                                item.amount = exportCapacity;
                                score += (item.amount * item.weight);
                                exportCapacity = 0.0f;
                            }
                        }
                    }
                    else
                    {
                        if (importCapacity > 0.0f)
                            {
                            if (item.amount <= importCapacity)
                            {
                                importCapacity -= item.amount;
                                score += (item.amount * item.weight);
                            }
                            else
                            {
                                item.amount = importCapacity;
                                score += (item.amount * item.weight);
                                importCapacity = 0.0f;
                            }
                        }
                    }
                }
            }
            // todo: factore in node-based distance-calculation x2 
            // score /= (distance *2)
            return new KeyValuePair<float, List<Data.TradeItem>>(score, sortedItems);
        }
    
        static Data.TradeItem getResolvedItem(Data.TradeItem fromItem, Data.TradeItem toItem)
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

