﻿using UnityEngine;
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
    public class ResourcePool
    {
        public float resources { get; private set; }
        public float consumptionRate { get; private set; }
        public float productionRate { get; private set; }
        public float targetLimit { get; private set; }
        public float growLimit { get;  private set; }
        public float overflowLimit { get; private set; }

        public ResourcePool(float startAmount, float consumptionRate, float targetLimit = 0.0f, 
                                float growLimit = 0.0f, float overflowLimit = 0.0f)
        {
            resources = startAmount;
            this.consumptionRate = consumptionRate;
            this.growLimit = growLimit;
            this.targetLimit = targetLimit;
            this.overflowLimit = overflowLimit;
        }
        public ResourcePool(float resourcesAtStart)
        {
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
            // prod + consumption
            resources += productionRate * delta;
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


        public ResourcePool pool { get; private set; }
        public Policy policy { get; set; }
        public State state { get; private set; }
        public Type type {get; private set; }

        public int level { get; private set; }

       
        public Resource(Type type, ResourcePool pool)
        {
            this.type = type;
            this.pool = pool;

            // set default policy + state
            policy = Policy.Sustain;
            state = State.Sustain;
       }
        public float getResources()
        {
            return pool.get();
        }

		// ------------------------------------------------------------------------------
        public void tick(float delta)
        {
            // check policies
            //handlePolicyChanges(location);

            // produce and consume
            pool.tick(delta);

            // set state
            if (pool.getAndResetDeficit() > 0.0f)
            {
                this.state = State.Shortage;
            }
            else if (pool.atGrowLimit())
            {
                state = State.ReadyToUpgrade;
                /*
                if (isResourceUpgradeableByTech(type, location))
                {
                    state = State.ReadyToUpgrade;
                }
                 */
            }
            else
            {
                state = State.Sustain;
            }

            // handle excess
            handleExcess();

            // check for level up
            return;
        }
		// ------------------------------------------------------------------------------

        private void handlePolicyChanges(LocationEconomy location)
        {
            /*
            foreach (ResourcePool pool in pools.Values)
            {

                if (policy == Policy.Grow)
                {
                    // set resorce limits
                    pool.setTargetLimit(pool.growLimit);
                }
                else if (policy == Policy.Sustain)
                {
                    // set resorce limits
                    pool.setTargetLimit(pool.consumptionRate * Parameters.stockpileDays);
                    // policy 2nd iteration
                    if (pool.resources < pool.consumptionRate) this.policy = Policy.Import;
                    else if (pool.resources > pool.targetLimit) this.policy = Policy.Export;
                }
                else if (policy == Policy.Import)
                {
                    // set resorce limits
                    pool.setTargetLimit(pool.consumptionRate * Parameters.stockpileDays);
                    // policy 2nd iteration
                    //if (pool.resources < pool.consumptionRate) this.state = State.Shortage;
                    if (pool.resources > pool.targetLimit) this.policy = Policy.Sustain;
                }
                else if (policy == Policy.Export) 
                {
                    // set resource limits
                    pool.setTargetLimit(pool.consumptionRate); // ? should export anything over consumptionRate
                }
            }
             */
        }



        private void handleExcess()
        {
            // not needed? excess can be handled in locationeconomy?
            // yeah, no spoilage - it can just be exported. Trying to make resources zero-sum game and avoid disappearing resources from the larger pool
        }

        internal string toDebugString()
        {
            string rv = Enum.GetName(typeof(Type), type) + " " + pool.get().ToString("F") + " (lvl " + level.ToString() + ") [" + Enum.GetName(typeof(State), state)
                + "] <" + Enum.GetName(typeof(Policy), policy) + ">";
            return rv;            
        }

        internal void upgrade()
        {
            level++;
            state = State.Sustain;
            pool.setGrowLimit(level * 10.0f);
            pool.spend(pool.get() * 0.8f);
        }
        internal void downgrade()
        {
            if (level > 0) level--;
            else Debug.LogWarning ("Attempting to downgrade level 0 resource");
            state = State.Sustain;
        }

		internal static bool isResourceUpgradeableByTech(Type type, LocationEconomy location)
		{
            /*
			if (location.resources[type].level < 4)
			{
				// not enough tech
				if (location.resources[type].level >= location.technologies[Tech.Type.Technology].level) return false;
				// not enough infra
				if (type == Resource.Type.Industry || type == Resource.Type.Mineral || type == Resource.Type.Economy)
				{
					if (location.resources[type].level >= location.technologies[Tech.Type.Infrastructure].level) return false;
				}
				// not enough military tech
				if (type == Resource.Type.Military)
				{
					if (location.resources[type].level >= location.technologies[Tech.Type.Military].level) return false;
				}
				return true;
			}
			else return false;
             */
            return true;
		}

        internal void updateFeatures(LocationFeatures features)
        {
            ///@todo production, consumption based on features
            ///      use random until reasonable values inserted
            UnityEngine.Random.seed = (int)type;
            pool.setProduction(pool.productionRate * features.resourceMultiplier[type]);
            pool.setConsumption(UnityEngine.Random.Range(0.0f, 2.0f));
        }
    }

}
