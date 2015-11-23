using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Data
{
    public class TradeItem
    {
        public bool isExported;
        public Resource.Type type;
        public Resource.SubType subType;
        public float amount = 0;
        public float weight = 0.0f;
        
        public TradeItem(TradeItem item = null)
        {
            if (item != null)
            {
                this.isExported = item.isExported;
                this.type = item.type;
                this.amount = item.amount;
                this.weight = item.weight;
            }
        }
    }

    public class Resource
    {
        public enum Type { Food, Mineral, Industry, Economy, Innovation, Culture, Military, BlackMarket }
        public enum SubType
        {
            FoodT1, FoodT2, FoodT3, FoodT4, MineralT1, MineralT2, MineralT3, MineralT4,
            IndustryT1, IndustryT2, IndustryT3, IndustryT4, EconomyT1, EconomyT2, EconomyT3, EconomyT4, 
            InnovationT1, InnovationT2, InnovationT3, InnovationT4, CultureT1, CultureT2, CultureT3, CultureT4,
            MilitaryT1, MilitaryT2, MilitaryT3, MilitaryT4, BlackMarketT1, BlackMarketT2, BlackMarketT3, BlackMarketT4, 
            Unknown
        }
        public enum Policy { Grow, GrowTech, Sustain, Stockpile, BareMinimum, Downsize }
        public enum State { Shortage, Sustain, AtGrowLimit }


        public Dictionary<SubType, float> playerInfluence = new Dictionary<SubType, float>();
        public Policy policy = Policy.Sustain;
        public State state = State.Sustain;
        public Type type;
        public int level = 1;

        public float resources = 2.0f;

        public Resource(Type type)
        {
            this.type = type;
            foreach (SubType tier in Enum.GetValues(typeof(SubType)))
            {
                playerInfluence.Add(tier, 0.0f);
            }
        }

        public static Resource generateDebugData(Type type)
        {
            Resource rv = new Resource(type);
            return rv;
        }




        public static SubType[] getSubTypes (Type? type)
        {
            switch (type)
            {
            case Type.Food:
                return new SubType[] {SubType.FoodT1, SubType.FoodT2, SubType.FoodT3, SubType.FoodT4};
            case Type.Mineral:
                return new SubType[] {SubType.MineralT1, SubType.MineralT2, SubType.MineralT3, SubType.MineralT4};
            case Type.Industry:
                return new SubType[] {SubType.IndustryT1, SubType.IndustryT2, SubType.IndustryT3, SubType.IndustryT4};
            case Type.Economy:
                return new SubType[] {SubType.EconomyT1, SubType.EconomyT2, SubType.EconomyT3, SubType.EconomyT4};
            case Type.Innovation:
                return new SubType[] {SubType.InnovationT1, SubType.InnovationT2, SubType.InnovationT3, SubType.InnovationT4};
            case Type.Culture:
                return new SubType[] {SubType.CultureT1, SubType.CultureT2, SubType.CultureT3, SubType.CultureT4};
            case Type.Military:
                return new SubType[] {SubType.MilitaryT1, SubType.MilitaryT2, SubType.MilitaryT3, SubType.MilitaryT4};
            case Type.BlackMarket:
                return new SubType[] {SubType.BlackMarketT1, SubType.BlackMarketT2, SubType.BlackMarketT3, SubType.BlackMarketT4};
            
            default: 
                return new SubType[] {SubType.Unknown};
            }
        }
        public static Type getTypeOfSubType (SubType subType)
        {
            if (subType == SubType.FoodT1 || subType == SubType.FoodT2 || subType ==  SubType.FoodT3 || subType ==  SubType.FoodT4)
                return Type.Food;
            else if (subType == SubType.MineralT1 || subType == SubType.MineralT2 || subType ==  SubType.MineralT3 || subType ==  SubType.MineralT4)
                return Type.Mineral;
            else if (subType == SubType.IndustryT1 || subType == SubType.IndustryT2 || subType ==  SubType.IndustryT3 || subType ==  SubType.IndustryT4)
                return Type.Industry;
            else if (subType == SubType.EconomyT1 || subType == SubType.EconomyT2 || subType ==  SubType.EconomyT3 || subType ==  SubType.EconomyT4)
                return Type.Economy;
            else if (subType == SubType.InnovationT1 || subType == SubType.InnovationT2 || subType ==  SubType.InnovationT3 || subType ==  SubType.InnovationT4)
                return Type.Innovation;
            else if (subType == SubType.CultureT1 || subType == SubType.CultureT2 || subType ==  SubType.CultureT3 || subType ==  SubType.CultureT4)
                return Type.Culture;
            else if (subType == SubType.MilitaryT1 || subType == SubType.MilitaryT2 || subType ==  SubType.MilitaryT3 || subType ==  SubType.MilitaryT4)
                return Type.Military;
            else if (subType == SubType.BlackMarketT1 || subType == SubType.BlackMarketT2 || subType ==  SubType.BlackMarketT3 || subType ==  SubType.BlackMarketT4)
                return Type.BlackMarket;
            else
                return Type.Innovation;
        }
        public static int getSubTypeTier (SubType subType)
        {
            if      (subType == SubType.FoodT1 || subType == SubType.MineralT1 || subType ==  SubType.IndustryT1 || subType ==  SubType.EconomyT1 || subType ==  SubType.InnovationT1 || subType == SubType.CultureT1 || subType == SubType.MilitaryT1 || subType == SubType.BlackMarketT1)
                return 1;
            else if (subType == SubType.FoodT2 || subType == SubType.MineralT2 || subType ==  SubType.IndustryT2 || subType ==  SubType.EconomyT2 || subType ==  SubType.InnovationT2 || subType == SubType.CultureT2 || subType == SubType.MilitaryT2 || subType == SubType.BlackMarketT2)
                return 2;
            else if (subType == SubType.FoodT3 || subType == SubType.MineralT3 || subType ==  SubType.IndustryT3 || subType ==  SubType.EconomyT3 || subType ==  SubType.InnovationT3 || subType == SubType.CultureT3 || subType == SubType.MilitaryT3 || subType == SubType.BlackMarketT3)
                return 3;
            else if (subType == SubType.FoodT4 || subType == SubType.MineralT4 || subType ==  SubType.IndustryT4 || subType ==  SubType.EconomyT4 || subType ==  SubType.InnovationT4 || subType == SubType.CultureT4 || subType == SubType.MilitaryT4 || subType == SubType.BlackMarketT4)
                return 4;
            else
                return 5;
        }

    }
    
}

