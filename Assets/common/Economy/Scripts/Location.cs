using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace NewEconomy
{
    public class LocationEconomyAI
    {
        public void setPolicies(LocationEconomy location)
        {
            location.setPolicy(Resource.Type.Military, Resource.Policy.Grow);
        }

        internal void manageLocationUpgrades(LocationEconomy location)
        {
            // todo
        }

        internal void setupTrades(LocationEconomy location)
        {
            // todo
        }
    }

    public class ResourceData
    {
        public int level;
        public Dictionary<int, float> pools = new Dictionary<int, float>();
    }

    public class LocationEconomyData
    {
        public Dictionary<Resource.Type, ResourceData> resourceData = new Dictionary<Resource.Type, ResourceData>();

        internal void generateDebugData()
        {
            foreach (Resource.Type type in Enum.GetValues(typeof(Resource.Type)))
            {
                ResourceData data = new ResourceData();
                data.level = UnityEngine.Random.Range(0, 5);
                resourceData[type] = data;
            }
        }
    }


    public class LocationEconomy
    {
        Dictionary<Resource.Type, Resource> resources = new Dictionary<Resource.Type, Resource>();
        LocationEconomyAI ai;

        public LocationEconomy(LocationEconomyData data, LocationEconomyAI ai)
        {
            this.ai = ai;
            foreach(Resource.Type type in Enum.GetValues(typeof(Resource.Type)))
            {
                // create pools based on input data
                resources[type] = new Resource(type, ResourceTierPool.createPools(data.resourceData[type].level));
            }
        }

        public void tick(float delta)
        {
            //AISetStrategy(this);
            ai.setPolicies(this);

            foreach (Resource resource in resources.Values)
            {
                resource.tick(delta);
            }

            ai.manageLocationUpgrades(this);

            ai.setupTrades(this);
        }

        internal void setPolicy(Resource.Type type, Resource.Policy policy)
        {
            resources[type].policy = policy;
        }

        internal string toDebugString()
        {
            string rv = "";
            foreach (Resource resource in resources.Values)
            {
                rv = rv + "   " + resource.toDebugString() + "\n";
            }
            return rv;
            throw new NotImplementedException();
        }
    }
}