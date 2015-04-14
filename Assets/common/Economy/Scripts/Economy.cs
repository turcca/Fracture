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
			// sort resources by resource multiplier - ie. how fast it's generating production

			// if negative growth, find out best resource to downgrade

			// else Find out best resources to upgrade



            //location.setPolicy(Resource.Type.Military, Resource.Policy.Grow);
        }

        internal void manageLocationUpgrades(LocationEconomy location)
        {
            ///@todo upgrade resources based on policies, just upgrade all eligible for now
            foreach (Resource resource in location.getResources())
            {
                if (resource.state == Resource.State.ReadyToUpgrade)
                {
                    resource.upgrade();
                }
            }
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
		List<KeyValuePair<Resource.Type, float>> sortedResourceTypes = new List<KeyValuePair<Resource.Type, float>>(); // sorted by effective resource multiplier
        LocationEconomyAI ai;

        public LocationEconomy(LocationEconomyData data, LocationEconomyAI ai)
        {
            this.ai = ai;
            foreach(Resource.Type type in Enum.GetValues(typeof(Resource.Type)))
            {
                // create pools based on input data
                //resources[type] = new Resource(type, ResourceTierPool.createPools(data.resourceData[type].level));
                resources[type] = new Resource(type, ResourceTierPool.createPools(type));

				// populate sort list for types
				sortedResourceTypes.Add(new KeyValuePair<Resource.Type, float>(type, 1.0f/*getEffectiveMul(location, ideology, type)*/));
            }
            updateFeatures(null);
        }

		// ----------------------------------------------------------TICK
        public void tick(float delta)
        {
            ai.setPolicies(this);

            foreach (Resource resource in resources.Values)
            {
                resource.tick(delta);
            }

            ai.manageLocationUpgrades(this);

            ai.setupTrades(this);
        }
		// ----------------------------------------------------------

        internal void setPolicy(Resource.Type type, Resource.Policy policy)
        {
            // resources[type].policy = policy;



			foreach (Resource resource in resources.Values)
			{
				//resource.;
			}
        }

        internal string toDebugString()
        {
            string rv = "";
            foreach (Resource resource in resources.Values)
            {
                rv = rv + resource.toDebugString() + "\n";
            }
            return rv;
            throw new NotImplementedException();
        }

        internal void updateFeatures(LocationFeatures features)
        {
            foreach (var pair in resources)
            {
                pair.Value.updateFeatures(features);
            }
        }

        internal IEnumerable<Resource> getResources()
        {
            return resources.Values;
        }

		// location economy variables

		public float getTotalLocationResourceMultiplier(LocationFeatures location)
		{
			float mul = 1;
			if (resources[Resource.Type.Food].level > 0) mul *= location.food;
			if (resources[Resource.Type.Mineral].level > 0) mul *= location.minerals;
			if (resources[Resource.Type.BlackMarket].level > 0) mul *= location.blackMarket;
			if (resources[Resource.Type.Innovation].level > 0) mul *= location.innovation;
			if (resources[Resource.Type.Culture].level > 0) mul *= location.culture;
			if (resources[Resource.Type.Industry].level > 0) mul *= location.industry;
			if (resources[Resource.Type.Economy].level > 0) mul *= location.economy;
			if (resources[Resource.Type.Military].level > 0) mul *= location.military;
			return mul;
		}
		public float getTotalEffectiveLocationResourceMultiplier(LocationFeatures location, IdeologyData ideology)
		{
			// IdeologyData ideology = location.info.ideology;
			float mul = 1;
			if (resources[Resource.Type.Food].level > 0) mul *= getEffectiveMul(location, ideology, Resource.Type.Food);
			if (resources[Resource.Type.Mineral].level > 0) mul *= getEffectiveMul(location, ideology, Resource.Type.Mineral);
			if (resources[Resource.Type.BlackMarket].level > 0) mul *= getEffectiveMul(location, ideology, Resource.Type.BlackMarket);
			if (resources[Resource.Type.Innovation].level > 0) mul *= getEffectiveMul(location, ideology, Resource.Type.Innovation);
			if (resources[Resource.Type.Culture].level > 0) mul *= getEffectiveMul(location, ideology, Resource.Type.Culture);
			if (resources[Resource.Type.Industry].level > 0) mul *= getEffectiveMul(location, ideology, Resource.Type.Industry);
			if (resources[Resource.Type.Economy].level > 0) mul *= getEffectiveMul(location, ideology, Resource.Type.Economy);
			if (resources[Resource.Type.Military].level > 0) mul *= getEffectiveMul(location, ideology, Resource.Type.Military);
			return mul;
		}
		// todo remove: -"IdeologyData ideology" // replace: ideology ---> location.info.ideology;
		internal float getEffectiveMul(LocationFeatures location, IdeologyData ideology, Resource.Type type)
		{
			if (type == Resource.Type.Food) return location.food * ideology.effects.foodMul;
			else if (type == Resource.Type.Mineral) return location.minerals * ideology.effects.mineralsMul;
			else if (type == Resource.Type.BlackMarket) return location.blackMarket * ideology.effects.blackMarketMul;
			else if (type == Resource.Type.Innovation) return location.innovation * ideology.effects.innovationMul;
			else if (type == Resource.Type.Culture) return location.culture * ideology.effects.cultureMul;
			else if (type == Resource.Type.Industry) return location.industry * ideology.effects.industryMul;
			else if (type == Resource.Type.Economy) return location.economy * ideology.effects.economyMul;
			else if (type == Resource.Type.Military) return location.military * ideology.effects.militaryMul;
			else { Debug.LogWarning ("WARNING: bad input type."); return 0; }
		}	

		public void sortResourceTypes(LocationFeatures location)
		{
			//sortedResourceTypes = 


			/*
			List<KeyValuePair<Resource.Type, Resource>> resourceList = resources.ToList();
			resourceList.Sort(
				delegate(KeyValuePair<Resource.Type, Resource> firstPair,
			         KeyValuePair<Resource.Type, Resource> nextPair)
				{
				return firstPair.Value.CompareTo(nextPair.Value);
			}
			);
			*/
		}
    }
}