using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Data
{

    public class Resource
    {
        public enum Type { Food, Mineral, BlackMarket, Innovation, Culture, Industry, Economy, Military }
        public enum SubType
        {
            FoodT1, FoodT2, FoodT3, FoodT4, MineralT1, MineralT2, MineralT3, MineralT4,
            BlackMarketT1, BlackMarketT2, BlackMarketT3, BlackMarketT4, InnovationT1, InnovationT2,
            InnovationT3, InnovationT4, CultureT1, CultureT2, CultureT3, CultureT4,
            IndustryT1, IndustryT2, IndustryT3, IndustryT4, EconomyT1, EconomyT2,
            EconomyT3, EconomyT4, MilitaryT1, MilitaryT2, MilitaryT3, MilitaryT4, Unknown
        }
        public enum Policy { Grow, Sustain, Stockpile, BareMinimum, Downsize }
        public enum State { Shortage, Sustain, AtGrowLimit }


        public Dictionary<SubType, float> playerInfluence { get; set; }
        public Policy policy { get; set; }
        public State state { get; set; }
        public Type type { get; set; }
        public int level { get; set; }


    }

    public class LocationData
    {

    }

}

