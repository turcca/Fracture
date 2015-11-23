using UnityEngine;
using System;
using System.Collections;

namespace Simulation
{
    public static class Parameters
    {
        public static float playerResourceInfluenceNormalizationPerDay = 5.0f; // how quickly player's purchases/sells are absorbed in resource pools
        public static float resourcePolicyStockpileDays = 5.0f;
        public static float resourceProducedDaily = 0.051f;
        public static float gameSpeed = 0.5f;
        private static float shipMovementMultiplier = 20.0f;
        public static float tradeShipMul = 0.5f; // amount of trade ships
        public static float cargoHoldMul = 10.0f; // basic cargoHold for trade ships
        public static float tradeScoreTreshold = 0.10f; //50.0f; // default trade scoring treshold for ship to be sent
        public static float resourceShortageMultiplier = 3.0f; // value of a shortaged resource

        public static float getPlayerShipSpeed()
        {
            return gameSpeed * shipMovementMultiplier * 0.8f;  // for some math reasons, playerShip speed is faster than NPC constant ship speed - so <1 multiplier
        }
        public static float getNPCShipSpeed()
        {
            return gameSpeed * shipMovementMultiplier;
        }

        public static float[] TierMultipliers = new float[] { 0.25f, 0.5f, 0.75f, 1.0f  };
        public static float tierScaleMultiplier(int tier)
        {
            if (tier == 0) return 0.0f;
            else if (tier == 1) return 1.0f;
            else if (tier == 2) return 2.0f;
            else if (tier == 3) return 3.0f;
            else if (tier == 4) return 4.0f;
            else { Debug.Log("Warning: tier exceeds 4"); return 0; }
        }
        public static float upgradeCostMultiplier(int currentTier)
        {
            if (currentTier == 0) return 1.0f;
            else if (currentTier == 1) return 10.0f;
            else if (currentTier == 2) return 20.0f;
            else if (currentTier == 3) return 40.0f;
            else return 0;
        }

        public static int getGovernmentStr(Location location)
        {
            //Debug.Log (location.name+" STR: "+(int)getImportance(location));
            //return (int)Mathf.Clamp (getImportance(location) / 4.0f, 
            //                    1, getImportance(location) / 4.0f);
            return (int)getImportance(location);
        }


        public static float getImportance(Location location)
        {
            float importance = Mathf.Sqrt(populationScaleMultiplier(location.features.population))  /20.0f + 1.0f;

            importance *= 1.0f + (float)(location.economy.technologies[Data.Tech.Type.Infrastructure].level +
                                  location.economy.technologies[Data.Tech.Type.Military].level +
                                  location.economy.technologies[Data.Tech.Type.Technology].level +
                                  location.features.assetStation)
                                /5.0f;
            return importance;
        }


        public static float populationScaleMultiplier(float population)
        {
            // pop in millions
            // esim jotain tällasta // x^1.7/x^1.1 /10 +0.2
            // 0 = 0.2, 1 = 0.3, 32 = 1, 100 = 1.8, 300 = 3.3, 1000 = 6.5
            return population > 0.0f ? Mathf.Pow(population, 1.7f) / Mathf.Pow(population, 1.1f) /10.0f +0.2f : 0.2f;
        }
        // default treshold for ship to be sent based on scoring 
        // succesful items that have shortages raise weighting scores to break through the treshold
        public static bool isTradeScoreEnough(float score)
        {
            return score > tradeScoreTreshold ? true : false; 
        }
        public static int getStartingTradeShips (Location location)
        {
            //Debug.Log (location.id+": "+tradeShipMul * (populationScaleMultiplier(location.features.population)+2) *location.ideology.resourceMultiplier[Data.Resource.Type.Economy]);
            int lvl = -1;
            try
            {
                lvl = location.economy.technologies[Data.Tech.Type.Technology].level;
            }
            catch (NullReferenceException)
            {
                Debug.LogError ("can't access tech level for some reason");
            }
            if (lvl > 0)
            {
                return (int)Mathf.Round (Mathf.Max (
                    tradeShipMul * 
                    (populationScaleMultiplier(location.features.population)+2) *
                    location.ideology.resourceMultiplier[Data.Resource.Type.Economy],

                    1)); // min 1 trade ship
            }
            return 0; // primitive world
        }
    }
}
