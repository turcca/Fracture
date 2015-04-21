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
            List<KeyValuePair<Data.Resource.Type, float>> sortedResourceTypes = location.getSortedResourceTypes();
            // if negative total growth
            if (location.getTotalEffectiveLocationResourceMultiplier() < 1.0f)
            {
                // POLICY: find out the worse resource to downgrade
                int lastIndex = sortedResourceTypes.Count - 1;

                if (sortedResourceTypes[lastIndex].Value < 1.0f)
                {
                    for (int i = lastIndex; i >= 0; i--)
                    {
                        // set last & bad resources to 'Downsize'
                        if (i == lastIndex || sortedResourceTypes[i].Value < 0.3f)
                            location.setPolicy(sortedResourceTypes[i].Key, Data.Resource.Policy.Downsize);
                        // set negative ones to 'Import'
                        //else if (sortedResourceTypes[i].Value < 1.0f)
                        //    location.setPolicy(sortedResourceTypes[i].Key, Data.Resource.Policy.Import);
                        //// set positive ones to 'export'
                        //else if (sortedResourceTypes[i].Value > 1.0f)
                        //    location.setPolicy(sortedResourceTypes[i].Key, Data.Resource.Policy.Export);
                        // else sustain
                        else location.setPolicy(sortedResourceTypes[i].Key,
                            Data.Resource.Policy.Sustain);
                    }
                }
                else Debug.LogWarning("WARNING: lowest sorted value >=1, even if 'totalEffectiveMul was <1");
            }
            // Positive growth! Find out best resources to upgrade (if total effective multiplier >=1)
            else
            {
                // POLICY: find overall strategy goal & figure out needed resources for that
                Data.Resource.Type resourceGoal = sortedResourceTypes[0].Key;
                List<Data.Resource.Type> resourceGoals = new List<Data.Resource.Type>();
                Data.Tech.Type? techGoal = Data.Tech.Type.Technology;

                ///@todo ASSET GOALS?


                // see if resource goal is achiavable
                foreach (KeyValuePair<Data.Resource.Type, float> sorted in sortedResourceTypes)
                {
                    if (location.resources[sorted.Key].level < 4)
                    {
                        if (location.hasEnoughTechToUpgrade(sorted.Key)) 
                        {
                            // >resource goal
                            resourceGoals.Add (sorted.Key);
                            techGoal = null;
                            break;
                        }
                    }
                }
                // needs a tech goal
                if (techGoal.HasValue)
                {
                    // >needs tech upgrade
                    techGoal = Tech.getEligibleTechGoal(resourceGoal, location);
                    resourceGoals = Tech.getResourcesForTech(techGoal.Value, location);
                }

                // go through resources and set policies
                foreach (KeyValuePair<Data.Resource.Type, float> sorted in sortedResourceTypes)
                {
                    // if resource is listed in resourceGoals, grow
                    if (resourceGoals.Contains(sorted.Key))
                        location.setPolicy(sorted.Key, Data.Resource.Policy.Grow);
                    // if producing, export
                    //else if (sorted.Value > 1.0f)
                    //    location.setPolicy(sorted.Key, Data.Resource.Policy.Export);
                    //// else needs to import
                    //else if (sorted.Value < 1.0f)
                    //    location.setPolicy(sorted.Key, Data.Resource.Policy.Import);
                    else
                        location.setPolicy(sorted.Key, Data.Resource.Policy.Sustain);
                }
                ///todo check after resource state allocation if total import mul > export mul. Can policies can be afforded?
            }
        }


        internal void manageLocationUpgrades(LocationEconomy location)
        {
            //foreach (Resource resource in location.getResources())
            //{
            //    if (resource.state == Resource.State.AtGrowLimit)
            //    {
            //        resource.upgrade();
            //    }
            //    if (resource.policy == Resource.Policy.Downsize)
            //    {
            //        resource.downgrade();
            //    }
            //}
        }

        internal void setupTrades(LocationEconomy location)
        {
            // todo
        }
    }

    public class LocationEconomy
    {
        internal Dictionary<Data.Resource.Type, Resource> resources = new Dictionary<Data.Resource.Type, Resource>();
		internal Dictionary<Data.Tech.Type, Tech> technologies = new Dictionary<Data.Tech.Type, Tech>();

        Location location;
		LocationEconomyAI ai;

        public LocationEconomy(Location location, LocationEconomyAI ai)
        {
            this.location = location;
            this.ai = ai;

            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)))
            {
                Data.Resource resourceData = Data.Resource.generateDebugData(type);
                resources[type] = new Resource(resourceData, new ResourcePool(resourceData));
            }
            foreach (Data.Tech.Type type in Enum.GetValues(typeof(Data.Tech.Type)))
            {
                Data.Tech techData = Data.Tech.generateDebugData(type);
                technologies.Add(type, new Tech(techData));
            }   
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

        internal void setPolicy(Data.Resource.Type type, Data.Resource.Policy policy)
        {
            resources[type].policy = policy;
        }

        internal bool hasEnoughMaterialsToUpgrade(Data.Resource.Type type)
        {
            return resources[type].state == Data.Resource.State.AtGrowLimit;
        }

        internal bool hasEnoughTechToUpgrade(Data.Resource.Type type)
        {
            if (resources[type].level < 4 &&
                resources[type].level < technologies[Data.Tech.Type.Technology].level)
            {
                // not enough infra
                if (type == Data.Resource.Type.Industry || type == Data.Resource.Type.Mineral ||
                    type == Data.Resource.Type.Economy)
                {
                    if (resources[type].level < technologies[Data.Tech.Type.Infrastructure].level)
                    {
                        return true;
                    }
                }
                // not enough military tech
                else if (type == Data.Resource.Type.Military)
                {
                    if (resources[type].level < technologies[Data.Tech.Type.Military].level)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal void upgradeResource(Data.Resource.Type type)
        {
            if (hasEnoughMaterialsToUpgrade(type) && hasEnoughTechToUpgrade(type))
            {
                resources[type].upgrade();
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
        }

        //// location economy variables

        public float getTotalLocationResourceMultiplier()
        {
            float mul = 1;
            foreach (var pair in resources)
            {
                mul *= pair.Value.level > 0 ? location.features.resourceMultiplier[pair.Key] : 1.0f;
            }
            return mul;
        }
        public float getTotalEffectiveLocationResourceMultiplier()
        {
            float mul = 1;
            foreach (var pair in resources)
            {
                mul *= pair.Value.level > 0 ? getEffectiveMul(pair.Key) : 1.0f;
            }
            return mul;
        }
        internal float getEffectiveMul(Data.Resource.Type type)
        {
            // should probably get all the multipliers at once for getSortedResourceTypes
            //return location.features.resourceMultiplier[type] * location.ideology.resourceMultiplier[type];
            if (type == Data.Resource.Type.Food) return location.features.resourceMultiplier[Data.Resource.Type.Food] * location.ideology.effects.foodMul;
            else if (type == Data.Resource.Type.Mineral) return location.features.resourceMultiplier[Data.Resource.Type.Mineral] * location.ideology.effects.mineralsMul;
            else if (type == Data.Resource.Type.BlackMarket) return location.features.resourceMultiplier[Data.Resource.Type.BlackMarket] * location.ideology.effects.blackMarketMul;
            else if (type == Data.Resource.Type.Innovation) return location.features.resourceMultiplier[Data.Resource.Type.Innovation] * location.ideology.effects.innovationMul;
            else if (type == Data.Resource.Type.Culture) return location.features.resourceMultiplier[Data.Resource.Type.Culture] * location.ideology.effects.cultureMul;
            else if (type == Data.Resource.Type.Industry) return location.features.resourceMultiplier[Data.Resource.Type.Industry] * location.ideology.effects.industryMul;
            else if (type == Data.Resource.Type.Economy) return location.features.resourceMultiplier[Data.Resource.Type.Economy] * location.ideology.effects.economyMul;
            else if (type == Data.Resource.Type.Military) return location.features.resourceMultiplier[Data.Resource.Type.Military] * location.ideology.effects.militaryMul;
            else { Debug.LogWarning("WARNING: bad input type."); return 0; }
        }

        internal List<KeyValuePair<Data.Resource.Type, float>> getSortedResourceTypes()
        {
            List<KeyValuePair<Data.Resource.Type, float>> sortedList = new List<KeyValuePair<Data.Resource.Type, float>>();

            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)))
            {
                sortedList.Add(new KeyValuePair<Data.Resource.Type, float>(type, getEffectiveMul(type)));
            }

            // Sort list by values
            sortedList.Sort(
                delegate(KeyValuePair<Data.Resource.Type, float> firstPair,
                            KeyValuePair<Data.Resource.Type, float> nextPair)
                {
                    return firstPair.Value.CompareTo(nextPair.Value);
                }
            );
            return sortedList;
        }
    }
}