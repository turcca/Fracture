using UnityEngine;
using System.Collections.Generic;
using System;

namespace NewEconomy
{
    /**
     * Resource tier pool keeps track of single resouce consumption/production.
     * Resource amount is labeled as follows:
     * 
     *                sustain              excess        excess/overflow
     * deficit |--------------------|-----------------|------------------| spoiled
     *        empty           target limit        grow limit        overflow limit
     * 
     * Grow/Sustain limits depend on population, tier, tech levels etc and
     * are given on creation. Target limit does not affect calculation but
     * is there to highlight amount of resources that should be kept in
     * storage.
     * 
     * Negative resources count as deficit and must be consumed from other
     * pools. Resources over grow limit count as excess and must be transferred
     * to trade.
     */
    public class ResourceTierPool
    {
        public enum Policy { Grow, Sustain }
        public Policy policy;

        float resources = 0.0f;

        float consumptionRate = 0.0f;
        float productionRate = 0.0f;
        float tradeRate = 0.0f;

        float targetLimit = 0.0f;
        float growLimit = 0.0f;
        float overflowLimit = 0.0f;

        public ResourceTierPool(float startAmount = 0.0f, float consumptionRate = 0.0f, float targetLimit = 0.0f, 
                                float growLimit = 0.0f, float overflowLimit = 0.0f)
        {
            resources = startAmount;
            this.consumptionRate = consumptionRate;
            this.growLimit = growLimit;
            this.targetLimit = targetLimit;
            this.overflowLimit = overflowLimit;
        }
        public float get()
        {
            return resources;
        }
        public float getDeficit()
        {
            return resources < 0.0f ? -resources : 0.0f;
        }
        public float getOverflow()
        {
            return resources > growLimit ? resources - growLimit : 0.0f;
        }
        public float getExcess()
        {
            return resources > targetLimit ? resources - targetLimit : 0.0f;
        }
        public void setProduction(float amount)
        {
            productionRate = amount;
        }
        public void setTrade(float amount)
        {
            tradeRate = amount;
        }

        public void tick(float delta)
        {
            resources += productionRate * delta;
            resources += tradeRate * delta;
            resources -= consumptionRate * delta;

            // spoilage
            Mathf.Clamp(resources, 0.0f, overflowLimit);

        }
        internal void spend(float amount)
        {
            resources -= amount;
        }
        internal float getAndResetDeficit()
        {
            float deficit = getDeficit();
            resources += deficit;
            return deficit;
        }

        public void add(float amount)
        {
            resources += amount;
        }

        internal bool atGrowLimit()
        {
            return resources >= growLimit;
        }

        internal static ResourceTierPool[] createTier1Pools(int amount)
        {
            ResourceTierPool pool = new ResourceTierPool(5.0f, 1.0f, 10.0f, 20.0f, 30.0f);
            return new ResourceTierPool[] { pool };
        }

        internal static ResourceTierPool[] createPools(int level)
        {
            return new ResourceTierPool[]
            {
                new ResourceTierPool(5.0f, 1.0f, 10.0f, 20.0f, 30.0f),
                new ResourceTierPool(5.0f, 1.0f, 10.0f, 20.0f, 30.0f),
                new ResourceTierPool(5.0f, 1.0f, 10.0f, 20.0f, 30.0f),
                new ResourceTierPool(5.0f, 1.0f, 10.0f, 20.0f, 30.0f)
            };
       }
    }


    /**
     * Resources are combination of all tiered commodities under the same category.
     * They represent location's "stats", see enum Type for all resource categories.
     */
    public class Resource
    {
        public enum Type { Food, Mineral, BlackMarket, Innovation, Culture, Industry, Economy, Military }
        public enum Policy { Grow, Sustain, Import, Export }
        public enum State { Shortage, Sustain, ReadyToUpgrade }

        private Dictionary<int, ResourceTierPool> pools = new Dictionary<int, ResourceTierPool>();
        public Policy policy { get; set; }
        public State state { get; private set; }
        public Type type {get; private set; }
        
        public Resource(Type type, ResourceTierPool[] pools)
        {
            this.type = type;

            // set default policy
            policy = Policy.Sustain;

            // add all tiers
            int tier = 1;
            foreach (var pool in pools)
            {
                this.pools.Add(tier, pool);
                ++tier;
            }
        }
        public float getResources(int tier)
        {
            return pools[tier].get();
        }

        public void tick(float delta)
        {
            // check policies
            handlePolicyChanges();

            // produce and consume
            bool allTiersAtGrowLimit = true;
            float deficitFromLowerTiers = 0.0f;
            foreach (var tier in pools)
            {
                tier.Value.spend(deficitFromLowerTiers);
                tier.Value.tick(delta);
                allTiersAtGrowLimit = tier.Value.atGrowLimit() ? allTiersAtGrowLimit : false;
                deficitFromLowerTiers = tier.Value.getAndResetDeficit();
            }
            // set state
            if (deficitFromLowerTiers > 0.0f)
            {
                state = State.Shortage;
            }
            else if (allTiersAtGrowLimit)
            {
                state = State.ReadyToUpgrade;
            }
            else
            {
                state = State.Sustain;
            }

            // handle excess
            foreach (var tier in pools)
            {
                handleExcess(tier.Value);
            }

            // check for level up
            return;
        }

        private void handlePolicyChanges()
        {
        }

        private void handleExcess(ResourceTierPool resourcePool)
        {
            // export
        }

        public ResourceTierPool getPool(int tier)
        {
            return pools[tier];
        }

        internal string toDebugString()
        {
            string rv = Enum.GetName(typeof(Type), type) + " [" + Enum.GetName(typeof(State), state)
                + "] <" + Enum.GetName(typeof(Policy), policy) + ">\n";
            int i = 1;
            foreach (ResourceTierPool pool in pools.Values)
            {
                rv += "T" + i.ToString() +": " + pools[1].get().ToString("F") + "    ";
                i++;
            }
            return rv;
            
        }
    }

}
