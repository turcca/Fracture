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
     * Grow/Sustain limits depend on population, tier, tech levels.
     * Target limit does not affect calculation but
     * is there to highlight amount of resources that should be kept in
     * storage.
     * 
     * Negative resources count as deficit and must be consumed from other
     * pools. Resources over grow limit count as excess.
     */
    public class ResourceTierPool
    {
        private float resources = 0.0f;
        Resource.SubType type;

        public float consumptionRate { get; private set; }
        public float productionRate { get; private set; }
        public float targetLimit { get; private set; }
        public float growLimit { get;  private set; }
        public float overflowLimit { get; private set; }

        public ResourceTierPool(float startAmount = 0.0f, float consumptionRate = 0.0f, float targetLimit = 0.0f, 
                                float growLimit = 0.0f, float overflowLimit = 0.0f)
        {
            resources = startAmount;
            this.consumptionRate = consumptionRate;
            this.growLimit = growLimit;
            this.targetLimit = targetLimit;
            this.overflowLimit = overflowLimit;
        }
        public ResourceTierPool(Resource.SubType type, float resourcesAtStart)
        {
            this.type = type;
            this.resources = resourcesAtStart;
            this.consumptionRate = 0.0f;
            this.growLimit = 0.0f;
            this.targetLimit = 0.0f;
            this.overflowLimit = 0.0f;
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

        public void tick(float delta)
        {
			//Produce
            resources += productionRate * delta;
            resources -= consumptionRate * delta;

			// Shortages


            // spoilage
            //Mathf.Clamp(resources, 0.0f, overflowLimit);

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

        internal void setGrowLimit(float limit)
        {
            growLimit = limit;
            overflowLimit = limit * 1.2f;
        }

        internal void setTargetLimit(float limit)
        {
            targetLimit = limit;
        }

        internal void setConsumption(float p)
        {
            consumptionRate = p;
        }

        internal static ResourceTierPool[] createPools(Resource.Type type)
        {
            ///maybe move to external class
            switch (type)
            {
                case Resource.Type.Food:
                    return new ResourceTierPool[] {
                        createTierPool(Resource.SubType.FoodT1, 0),
                        createTierPool(Resource.SubType.FoodT2, 0),
                        createTierPool(Resource.SubType.FoodT3, 0),
                        createTierPool(Resource.SubType.FoodT4, 0)
                    };
				case Resource.Type.Mineral:
					return new ResourceTierPool[] {
						createTierPool(Resource.SubType.MineralT1, 0),
						createTierPool(Resource.SubType.MineralT2, 0),
						createTierPool(Resource.SubType.MineralT3, 0),
						createTierPool(Resource.SubType.MineralT4, 0)
					};
				case Resource.Type.BlackMarket:
					return new ResourceTierPool[] {
						createTierPool(Resource.SubType.BlackMarketT1, 0),
						createTierPool(Resource.SubType.BlackMarketT2, 0),
						createTierPool(Resource.SubType.BlackMarketT3, 0),
						createTierPool(Resource.SubType.BlackMarketT4, 0)
					};
				case Resource.Type.Innovation:
					return new ResourceTierPool[] {
						createTierPool(Resource.SubType.InnovationT1, 0),
						createTierPool(Resource.SubType.InnovationT2, 0),
						createTierPool(Resource.SubType.InnovationT3, 0),
						createTierPool(Resource.SubType.InnovationT4, 0)
					};
				case Resource.Type.Culture:
					return new ResourceTierPool[] {
						createTierPool(Resource.SubType.CultureT1, 0),
						createTierPool(Resource.SubType.CultureT2, 0),
						createTierPool(Resource.SubType.CultureT3, 0),
						createTierPool(Resource.SubType.CultureT4, 0)
					};
				case Resource.Type.Industry:
					return new ResourceTierPool[] {
						createTierPool(Resource.SubType.IndustryT1, 0),
						createTierPool(Resource.SubType.IndustryT2, 0),
						createTierPool(Resource.SubType.IndustryT3, 0),
						createTierPool(Resource.SubType.IndustryT4, 0)
					};
				case Resource.Type.Economy:
					return new ResourceTierPool[] {
						createTierPool(Resource.SubType.EconomyT1, 0),
						createTierPool(Resource.SubType.EconomyT2, 0),
						createTierPool(Resource.SubType.EconomyT3, 0),
						createTierPool(Resource.SubType.EconomyT4, 0)
					};
				case Resource.Type.Military:
					return new ResourceTierPool[] {
						createTierPool(Resource.SubType.MilitaryT1, 0),
						createTierPool(Resource.SubType.MilitaryT2, 0),
						createTierPool(Resource.SubType.MilitaryT3, 0),
						createTierPool(Resource.SubType.MilitaryT4, 0)
				}; 
                default:
                    return new ResourceTierPool[] {
                        createTierPool(Resource.SubType.FoodT1, 0),
                        createTierPool(Resource.SubType.FoodT2, 0),
                        createTierPool(Resource.SubType.FoodT3, 0),
                        createTierPool(Resource.SubType.FoodT4, 0)
                    };
            }
        }

        private static ResourceTierPool createTierPool(Resource.SubType subType, int level)
        {
			return new ResourceTierPool(subType, 5.0f);	// todo starting resources (5)
        }

        internal static ResourceTierPool[] createPools(int level)
        {
            return new ResourceTierPool[]            
            {
                createTierPool(1, level),
                createTierPool(2, level),
                createTierPool(3, level),
                createTierPool(4, level)
            };
        }

        internal static ResourceTierPool createTierPool(int tier, int level)
        {
            return new ResourceTierPool(UnityEngine.Random.Range(1.0f, 10.0f), 1.0f, 5.0f, 10.0f, 20.0f); // Mitä tää on?
        }
    }


    /**
     * Resources are combination of all tiered commodities under the same category.
     * They represent location's "stats", see enum Type for all resource categories.
     */
    public class Resource
    {
        public enum Type { Food, Mineral, BlackMarket, Innovation, Culture, Industry, Economy, Military }
        public enum SubType { FoodT1, FoodT2, FoodT3, FoodT4, MineralT1, MineralT2, MineralT3, MineralT4,
                              BlackMarketT1, BlackMarketT2, BlackMarketT3, BlackMarketT4, InnovationT1, InnovationT2,
                              InnovationT3, InnovationT4, CultureT1, CultureT2, CultureT3, CultureT4,
                              IndustryT1, IndustryT2, IndustryT3, IndustryT4, EconomyT1, EconomyT2,
                              EconomyT3, EconomyT4, MilitaryT1, MilitaryT2, MilitaryT3, MilitaryT4, Unknown }
        public enum Policy { Grow, Sustain, Import, Export, Downsize }
        public enum State { Shortage, Sustain, ReadyToUpgrade }

        private Dictionary<int, ResourceTierPool> pools = new Dictionary<int, ResourceTierPool>();
        public Policy policy { get; set; }
        public State state { get; private set; }
        public Type type {get; private set; }

        public int level { get; private set; }

       
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

		// ------------------------------------------------------------------------------
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
                if (level < 4)
                {
                    state = State.ReadyToUpgrade;
                }
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
		// ------------------------------------------------------------------------------

        private void handlePolicyChanges()
        {
            if (policy == Policy.Grow)
            {
                foreach (ResourceTierPool pool in pools.Values)
                {
                    pool.setTargetLimit(pool.growLimit);
                }
            }
            else if (policy == Policy.Sustain)
            {
                foreach (ResourceTierPool pool in pools.Values)
                {
					pool.setTargetLimit(pool.consumptionRate * Parameters.stockpileDays);
                }
            }
        }

        private void handleExcess(ResourceTierPool resourcePool)
        {
            // not needed? excess can be handled in locationeconomy?
        }

        public ResourceTierPool getPool(int tier)
        {
            return pools[tier];
        }

        internal string toDebugString()
        {
            string rv = Enum.GetName(typeof(Type), type) + " lvl " + level.ToString() + " [" + Enum.GetName(typeof(State), state)
                + "] <" + Enum.GetName(typeof(Policy), policy) + ">\n";
            int i = 1;
            foreach (ResourceTierPool pool in pools.Values)
            {
                rv += "T" + i.ToString() +": " + pools[i].get().ToString("F") + "  (+" +
                      pools[i].productionRate.ToString("F") + ")" + " (-" + pools[i].consumptionRate.ToString("F") + ")\n";
                i++;
            }
            return rv;
            
        }

        internal void upgrade()
        {
            level++;
            foreach (var pool in pools)
            {
                pool.Value.setGrowLimit(level * 10.0f);
                pool.Value.spend(pool.Value.get() * 0.8f);
            }
        }

        internal void updateFeatures(LocationFeatures features)
        {
            ///@todo production, consumption based on features
            ///      use random until reasonable values inserted
            UnityEngine.Random.seed = (int)type;
            foreach (var pair in pools)
            {
                pair.Value.setProduction(UnityEngine.Random.Range(0.0f, 5.0f));
                pair.Value.setProduction(pair.Value.productionRate * features.resourceMultiplier[type]);

                pair.Value.setConsumption(UnityEngine.Random.Range(0.0f, 2.0f));
            }
        }
    }

}
