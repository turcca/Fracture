﻿using UnityEngine;
using System.Collections.Generic;
using System;

namespace NewEconomy
{
    /**
     * Resource pool keeps track of resouce consumption/production.
     * Resource amount is labeled as follows:
     * 
     *                sustain              excess        excess/overflow
     * deficit |--------------------|-----------------|------------------| spoiled
     *        empty           target limit        grow limit        overflow limit
     * 
     * Grow/Sustain limits depend on population, tech levels etc.
     * Target limit does not affect calculation but is there to highlight amount of
     * resources that should be kept in storage.
     */
    public class ResourcePool
    {
        private Data.Resource data;
        public float consumptionRate { get; private set; }
        public float productionRate { get; private set; }
        public float targetLimit { get; private set; }
        public float growLimit { get;  private set; }
        public float overflowLimit { get; private set; }

        public ResourcePool(Data.Resource data, float consumptionRate,
                            float targetLimit = 0.0f, float growLimit = 0.0f,
                            float overflowLimit = 0.0f)
        {
            this.data = data;
            this.consumptionRate = consumptionRate;
            this.growLimit = growLimit;
            this.targetLimit = targetLimit;
            this.overflowLimit = overflowLimit;
        }
        public ResourcePool(Data.Resource data)
        {
            this.data = data;
            this.consumptionRate = 0.0f;
            this.growLimit = 0.0f;
            this.targetLimit = 0.0f;
            this.overflowLimit = 0.0f;
        }

        public float get()
        {
            return data.resources;
        }
        public float getDeficit()
        {
            return data.resources < 0.0f ? -data.resources : 0.0f;
        }
        public float getOverflow()
        {
            return data.resources > growLimit ? data.resources - growLimit : 0.0f;
        }
        public float getExcess()
        {
            return data.resources > targetLimit ? data.resources - targetLimit : 0.0f;
        }

        public void tick(float delta)
        {
            // prod + consumption
            data.resources += productionRate * delta;
            data.resources -= consumptionRate * delta;

            // spoilage
            Mathf.Clamp(data.resources, 0.0f, overflowLimit);

        }
        internal void spend(float amount)
        {
            data.resources -= amount;
        }
        internal float getAndResetDeficit()
        {
            float deficit = getDeficit();
            data.resources += deficit;
            return deficit;
        }

        public void add(float amount)
        {
            data.resources += amount;
        }

        internal bool atGrowLimit()
        {
            return data.resources >= growLimit;
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

        public void setProduction(float amount)
        {
            productionRate = amount;
        }

        internal void setConsumption(float p)
        {
            consumptionRate = p;
        }
    }


    /**
     * Resources.
     * They represent location's "stats", see enum Type for all resource categories.
     */
    public class Resource
    {
        private ResourcePool pool;
        private Data.Resource data;
        private Location location;

        public int level
        {
            get { return data.level; }
            private set { data.level = value; }
        }
        public Data.Resource.State state
        {
            get { return data.state; }
            private set { data.state = value; }
        }
        public Data.Resource.Policy policy
        {
            get { return data.policy; }
            set { data.policy = value; }
        }

        public Resource(Data.Resource data, ResourcePool pool, Location location)
        {
            this.data = data;
            this.pool = pool;
            this.location = location;
        }
        public float getResources()
        {
            return pool.get();
        }

		// ------------------------------------------------------------------------------
        public void tick(float delta)
        {
            updateFeatures();
            handlePolicyChanges();

            // produce and consume
            pool.tick(delta);

            setState();

            // normalize player influence
            foreach (var tier in data.playerInfluence.Keys)
            {
                Mathf.MoveTowards(data.playerInfluence[tier], 0.0f,
                    Parameters.playerResourceInfluenceNormalizationPerDay * delta);
            }
            return;
        }
		// ------------------------------------------------------------------------------

        private void setState()
        {
            if (pool.getAndResetDeficit() > 0.0f)
            {
                data.state = Data.Resource.State.Shortage;
            }
            else if (pool.atGrowLimit())
            {
                data.state = Data.Resource.State.AtGrowLimit;
            }
            else
            {
                data.state = Data.Resource.State.Sustain;
            }
        }

        private void handlePolicyChanges()
        {
            // set limits
            switch (data.policy)
            {
                case Data.Resource.Policy.Grow:
                    pool.setTargetLimit(pool.growLimit);
                    break;
                case Data.Resource.Policy.GrowTech:
                    pool.setTargetLimit(pool.growLimit); // todo tech cost /technology ?
                    break;
                case Data.Resource.Policy.Sustain:
                    pool.setTargetLimit(pool.consumptionRate * Parameters.resourcePolicyStockpileDays);
                    break;
                case Data.Resource.Policy.Stockpile:
                    pool.setTargetLimit(pool.overflowLimit);
                    break;
                case Data.Resource.Policy.BareMinimum:
                    pool.setTargetLimit(pool.consumptionRate);
                    break;
                case Data.Resource.Policy.Downsize:
                    pool.setTargetLimit(0.0f);
                    break;
                default:
                    //noop
                    break;
            }

        }
        
        internal string toDebugString()
        {
            string rv = Enum.GetName(typeof(Data.Resource.Type), data.type) + " " + pool.get().ToString("F") +
                " (lvl " + data.level.ToString() + ") " +
                "[" + Enum.GetName(typeof(Data.Resource.State), data.state) + "] " +
                "<" + Enum.GetName(typeof(Data.Resource.Policy), data.policy) + ">";
            return rv;            
        }

        internal void upgrade()
        {
            if (data.level < 4) 
            {
                data.level++;
                data.state = Data.Resource.State.Sustain;
                pool.setGrowLimit(data.level * 10.0f);
                pool.spend(pool.get() * 0.8f);
                setState();
            }
            else Debug.LogWarning ("Attempting to upgrade level 4 resource");
        }
        internal void downgrade()
        {
            if (data.level > 0) 
            {
                data.level--;
                setState();
            }
            else Debug.LogWarning ("Attempting to downgrade level 0 resource");
        }

        internal void updateFeatures()
        {
            pool.setConsumption(Parameters.resourceProducedDaily *              // static multiplier for resource production rate
                                Parameters.tierScaleMultiplier(data.level));    // tier-based multiplier for production/consumption scale 
            pool.setProduction(pool.consumptionRate *                           // base
                               location.economy.getEffectiveMul(data.type));    // locatin's feature mul * population ideology stat mul
        }
    }

}
