using UnityEngine;
using System.Collections.Generic;
using System;


namespace Simulation
{

    /// <summary>
    /// fluctuate / balance global market growths to fluctuate each resource ~ between 100 and 200
    /// introduce market crashes if resource climbs over 200
    /// </summary>
    public class GlobalMarket
    {
        GlobalMarketAdjuster adjuster;

        /// <summary>
        /// combined resource pool from all locations
        /// </summary>
        Dictionary<Data.Resource.Type, float> globalResources = new Dictionary<Data.Resource.Type, float>();
        /// <summary>
        /// combined resource growth rate from all locations
        /// </summary>
        Dictionary<Data.Resource.Type, float> globalResourceGrowth = new Dictionary<Data.Resource.Type, float>();

        float adjustBuffer = 0;
        public static float globalGrowthTarget = 0.04f;

        public GlobalMarket()
        {
            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type))) { globalResources.Add(type, 0f); globalResourceGrowth.Add(type, 0f); }
            // find for starmap scene
            GameObject adjusterObj = GameObject.Find("Debug");
            // find for simulation scene
            if (adjusterObj == null) adjusterObj = GameObject.Find("GameLoop");
            // get GlobalMarketAdjuster script
            if (adjusterObj != null) adjuster = adjusterObj.GetComponent<GlobalMarketAdjuster>();
            else Debug.LogError("ERROR: GlobalMarkets can't locate 'Debug/GameLoop' gameObject [GameState: " + GameState.ToDebugString() + "]");

            if (adjuster == null) Debug.LogError("ERROR: GlobalMarkets can't locate 'Debug/GlobalMarketAdjuster' script  [GameState: " + GameState.ToDebugString() + "]");
        }

        /// <summary>
        /// update values each full day
        /// adjust growth speed if needed. 
        /// INCLUDES hard-coded GLOBAL BOUNDARY PARAMETERS
        /// </summary>
        /// <param name="tick"></param>
        public void updateGlobalEconomy(float tick)
        {
            if (adjustBuffer <= 0f)
            {
                // reset pools
                foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type))) { globalResources[type] = 0f; globalResourceGrowth[type] = 0f; }

                // update global resource pool & growth pool from all locations
                foreach (Location loc in Root.game.locations.Values)
                {
                    foreach (var resource in loc.economy.resources)
                    {
                        globalResources[resource.Key] += resource.Value.getResources();
                        globalResourceGrowth[resource.Key] += resource.Value.getNetResourceProduction();
                    }
                }
                // check if global stores are out of bounds, adjust when needed
                foreach (Data.Resource.Type resource in Enum.GetValues(typeof(Data.Resource.Type)))
                {
                    if (globalResources[resource] < 0f)
                    {
                        if (globalResourceGrowth[resource] < 0.5f)
                            adjuster.addMarketAdjuster(resource, 0.001f);
                    }
                    else if (globalResources[resource] < 100f)
                    {
                        if (globalResourceGrowth[resource] < 0.3f)
                            adjuster.addMarketAdjuster(resource, 0.001f);
                        else if (globalResourceGrowth[resource] > 0.3f)
                            adjuster.addMarketAdjuster(resource, -0.001f);
                    }
                    else if (globalResources[resource] > 200f)
                    {
                        if (globalResourceGrowth[resource] > 0)
                        {
                            adjuster.addMarketAdjuster(resource, -0.1f); // crash
                            Debug.Log("GlobalMarkets: introducing MARKET CRASH for ['" + resource.ToString().ToUpper() + "']");
                        }
                    }
                    //else if (globalResources[resource] > 200f)
                    //{
                    //    if (globalResourceGrowth[resource] > globalGrowthTarget)
                    //        adjuster.addMarketAdjuster(resource, -0.001f);
                    //}
                    else
                    {
                        // check for small market crash
                        if (UnityEngine.Random.value < 0.002f)
                        {
                            adjuster.addMarketAdjuster(resource, -0.07f); // random crash
                            Debug.Log("GlobalMarkets: introducing RANDOM MARKET CRASH for ['" + resource.ToString().ToUpper() + "']");
                        }
                        // balence resource in between boundaries
                        if (globalResourceGrowth[resource] < globalGrowthTarget - 0.02f)
                            adjuster.addMarketAdjuster(resource, +0.0001f);
                        else if (globalResourceGrowth[resource] > globalGrowthTarget + 0.02f)
                            adjuster.addMarketAdjuster(resource, -0.0005f);
                    }

                }
                adjustBuffer += 1f;
            }
            else adjustBuffer -= tick;
        }

        public float getGlobalResources(Data.Resource.Type type)
        {
            return globalResources[type];
        }
        public float getGlobalResourceGrowth(Data.Resource.Type type)
        {
            return globalResourceGrowth[type];
        }


    }
}