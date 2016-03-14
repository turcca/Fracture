using UnityEngine;
using System.Collections;
using System;

public class GlobalMarketAdjuster : MonoBehaviour
{

    // pre-adjustments
    // Bypass GlobalMarket and drive GlobalMarketAdjuster values from aditor
    public bool edit = false;
    /// <summary>
    /// update local GlobalMarketAdjuster values in editor for debugging
    /// </summary>
    public bool debugUpdate = false;
    
    // for tier multipliers 1,2,3,4
    //public float food = -0.06f;
    //public float mineral = -0.17f;
    //public float industry = -0.13f;
    //public float economy = 0.004f;
    //public float innovation = -0.35f;
    //public float culture = -0.25f;
    //public float military = -0.39f;
    //public float blackMarket = -0.0f;

    // for tier multipliers 1, 1.02, 1.04, 1.05
    public float food = -0.15f;
    public float mineral = -0.14f;
    public float industry = -0.11f;
    public float economy = 0.11f;
    public float innovation = -0.18f;
    public float culture = -0.16f;
    public float military = -0.27f;
    public float blackMarket = 0.05f;


    // enable this for real-time debugging in simulationScene
    void Update()
    {
        // debug - drive these elsewhere
        if (debugUpdate)
        {
            // store manual inputs from this component
            if (edit)
            {
                Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Food, food);
                Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Mineral, mineral);
                Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Industry, industry);
                Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.BlackMarket, blackMarket);
                Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Innovation, innovation);
                Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Culture, culture);
                Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Economy, economy);
                Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Military, military);
            }
            // update these values from 
            else
            {
                food = Simulation.Parameters.getGlobatMarketAdjuster(Data.Resource.Type.Food);
                mineral = Simulation.Parameters.getGlobatMarketAdjuster(Data.Resource.Type.Mineral);
                industry = Simulation.Parameters.getGlobatMarketAdjuster(Data.Resource.Type.Industry);
                blackMarket = Simulation.Parameters.getGlobatMarketAdjuster(Data.Resource.Type.BlackMarket);
                innovation = Simulation.Parameters.getGlobatMarketAdjuster(Data.Resource.Type.Innovation);
                culture = Simulation.Parameters.getGlobatMarketAdjuster(Data.Resource.Type.Culture);
                economy = Simulation.Parameters.getGlobatMarketAdjuster(Data.Resource.Type.Economy);
                military = Simulation.Parameters.getGlobatMarketAdjuster(Data.Resource.Type.Military);
            }
        }

    }


    void Awake()
    {
        Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Food, food);
        Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Mineral, mineral);
        Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Industry, industry);
        Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.BlackMarket, blackMarket);
        Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Innovation, innovation);
        Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Culture, culture);
        Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Economy, economy);
        Simulation.Parameters.setGlobatMarketAdjuster(Data.Resource.Type.Military, military);
    }

    /// <summary>
    /// adds an increment to a market adjuster to adjust global resource growth
    /// max deviation clamps growth/shrink values within -maxDeviation - +maxDeviation
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="maxDeviation"></param>
    public void addMarketAdjuster(Data.Resource.Type type, float value, float maxDeviation = 0.3f)
    {
        if (edit)
        {
            Debug.LogWarning("MarketAdjuster overridden by manual edit!\ntype: "+type+"   value: "+value+"");
            return;
        }

        Simulation.Parameters.setGlobatMarketAdjuster(type, Mathf.Clamp(value + Simulation.Parameters.getGlobatMarketAdjuster(type), -maxDeviation, maxDeviation));

        if (debugUpdate)
        {
            Debug.Log("   >GlobalMarketAdjuster ["+type+"] +=  " + value);

            switch (type)
            {
                case Data.Resource.Type.Food:
                    food += value;
                    break;
                case Data.Resource.Type.Mineral:
                    mineral += value;
                    break;
                case Data.Resource.Type.Industry:
                    industry += value;
                    break;
                case Data.Resource.Type.Economy:
                    economy += value;
                    break;
                case Data.Resource.Type.Innovation:
                    innovation += value;
                    break;
                case Data.Resource.Type.Culture:
                    culture += value;
                    break;
                case Data.Resource.Type.Military:
                    military += value;
                    break;
                case Data.Resource.Type.BlackMarket:
                    blackMarket += value;
                    break;
                default:
                    break;
            }
        }
    }

}
