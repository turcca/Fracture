using System;
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
    public enum Policy { Grow, GrowTech, Sustain, Stockpile, BareMinimum, Downsize }
    public enum State { Shortage, Sustain, AtGrowLimit }


    public Dictionary<SubType, float> playerInfluence = new Dictionary<SubType, float>();
    public Policy policy = Policy.Sustain;
    public State state = State.Sustain;
    public Type type;
    public int level = 0;

    public float resources = 0.0f;

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
}

}

