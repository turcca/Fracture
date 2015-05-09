using UnityEngine;
using System.Collections;

namespace Simulation
{
    public static class Parameters
    {
        public static float playerResourceInfluenceNormalizationPerDay = 5.0f;
        public static float resourcePolicyStockpileDays = 5.0f;
        public static float resourceProducedDaily = 10.0f;
        public static float shipMovementMultiplier = 1000.0f;

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
        public static float populationScaleMultiplier(float population)
        {
            // pop in millions?
            // esim jotain tällasta // x^1.7/x^1.1 /10 +0.2
            // 0 = 0.2, 1 = 0.3, 32 = 1, 100 = 1.8, 300 = 3.3, 1000 = 6.5
            return population > 0.0f ? Mathf.Pow(population, 1.7f) / Mathf.Pow(population, 1.1f) /10.0f +0.2f : 0.2f;
        }
        // default treshold for ship to be sent based on scoring 
        // succesful items that have shortages raise weighting scores to break through the treshold
        public static bool isTradeScoreEnough(float score)
        {
            return score > 1.0f ? true : false; 
        }
    }
}
