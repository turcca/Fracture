using UnityEngine;
using System.Collections;

namespace NewEconomy
{
    public static class Parameters
    {
        public static float playerResourceInfluenceNormalizationPerDay = 5.0f;
        public static float resourcePolicyStockpileDays = 5.0f;

        public static float[] TierMultipliers = new float[] { 1.0f, 0.75f, 0.5f, 0.25f };
    }
}
