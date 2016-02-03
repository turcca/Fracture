using UnityEngine;
using System.Collections;

public class GlobalMarketAdjuster : MonoBehaviour
{
    public bool edit = false;

    // pre-adjustments
    public float food = -0.06f;
    public float mineral = -0.17f;
    public float industry = -0.13f;
    public float economy = 0.004f;
    public float innovation = -0.35f;
    public float culture = -0.25f;
    public float military = -0.39f;
    public float blackMarket = -0.0f;

    // enable this for real-time debugging in simulationScene
    void Update()
    {
        // debug - drive these elsewhere
        if (true)
        {
            // store manual inputs to this component
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




}
