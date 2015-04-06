using UnityEngine;
using System.Collections;

namespace NewEconomy
{
    public static class Parameters
    {
        public enum ResourceType { Food, Mineral, BlackMarket, Innovation, Culture, Industry, Economy, Military };
        public enum TechType { Technology, Infrastructure, Military };

        public static float[] TierMultipliers = new float[] { 1.0f, 0.75f, 0.5f, 0.25f };
        public static float stockpileDays = 5.0f;
    }
}
