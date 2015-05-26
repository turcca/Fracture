using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerTradeList
{
    public Dictionary<Data.Resource.SubType, int> commodities =
        new Dictionary<Data.Resource.SubType, int>();
    public PlayerTradeList()
    {
        foreach (Data.Resource.SubType type in Enum.GetValues(typeof(Data.Resource.SubType)))
        {
            ///@fixme initialise with zero after debugging is done
            commodities.Add(type, 5);
        }
    }
}
