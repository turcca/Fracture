﻿using UnityEngine;
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

            // set effective value
            foreach (KeyValuePair<Data.Resource.Type, float> sorted in sortedResourceTypes)
            {
                location.resources[sorted.Key].setEffectiveMultiplier(sorted.Value);
            }

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
                        else if (sortedResourceTypes[i].Value < 1.0f)
                            location.setPolicy(sortedResourceTypes[i].Key, Data.Resource.Policy.Sustain);
                        // set positive ones to 'export'
                        else if (sortedResourceTypes[i].Value > 1.0f)
                            location.setPolicy(sortedResourceTypes[i].Key, Data.Resource.Policy.BareMinimum);
                        // else sustain
                        else location.setPolicy(sortedResourceTypes[i].Key,
                            Data.Resource.Policy.Sustain);
                    }
                }
                else Debug.LogWarning("WARNING: lowest sorted value >=1, even if 'totalEffectiveMul was <1");
            }
            // Positive growth! Find out best resources to upgrade (total effective multiplier >=1)
            else
            {
                // POLICY: find overall strategy goal & figure out needed resources for it
                location.setResourceGoal(null);

                ///@todo ASSET GOALS?

                // see in order which resource goal is achiavable
                foreach (KeyValuePair<Data.Resource.Type, float> sorted in sortedResourceTypes)
                {
                    if (location.resourceGoal == null &&
                        location.resources[sorted.Key].level < 4 &&
                        location.hasEnoughTechToUpgrade(sorted.Key))
                    {
                        // >resource goal
                        location.setResourceGoal(sorted.Key);
                        location.setTechGoal(null);
                        // set policy for resource grow
                        location.setPolicy(sorted.Key, Data.Resource.Policy.Grow);
                    }
                    else 
                    {
                        // if producing, export
                        if (sorted.Value > 1.0f)
                            location.setPolicy(sorted.Key, Data.Resource.Policy.BareMinimum);
                        // else needs to import
                        else
                            location.setPolicy(sorted.Key, Data.Resource.Policy.Sustain);
                    }
                }
                // needs a tech goal
                if (location.resourceGoal == null)
                {
                    // >needs tech upgrade
                    location.setTechGoal(Tech.getEligibleTechGoal(location.resourceGoal, location));

                    if (location.techGoal.HasValue)
                    {
                        List<Data.Resource.Type> resourceGoals = Tech.getResourcesForTech(location.techGoal.Value, location);

                        // go through resources and set policies for tech upgrade
                        foreach (KeyValuePair<Data.Resource.Type, float> sorted in sortedResourceTypes)
                        {
                            // if resource is listed in resourceGoals, grow
                            if (resourceGoals.Contains(sorted.Key))
                                location.setPolicy(sorted.Key, Data.Resource.Policy.GrowTech);
                            else
                            {
                                // if producing, export
                                if (sorted.Value > 1.0f)
                                    location.setPolicy(sorted.Key, Data.Resource.Policy.BareMinimum);
                                // else needs to import
                                else
                                    location.setPolicy(sorted.Key, Data.Resource.Policy.Sustain);
                            }
                        }
                    }
                }
                ///todo check after resource state allocation if total import mul > export mul. Can policies can be afforded?
            }
        }


        internal void manageLocationUpgrades(LocationEconomy location)
        {
            if (location.resourceGoal.HasValue)
            {
                location.upgradeResource((Data.Resource.Type)location.resourceGoal);
            }
            else if (location.techGoal.HasValue)
            {
                location.upgradeTech((Data.Tech.Type)location.techGoal);
            }
        }

        internal void setupTrades(LocationEconomy location)
        {
            location.tradeItems = getTradeList(location);


        }

        // gathers a list of articles, each with import/export status, quotas and weights 
        // to be compared against tradeList of other locations for best match
        public List<Data.TradeItem> getTradeList(LocationEconomy location)
        {
            List<Data.TradeItem> list = new List<Data.TradeItem>();
            Data.TradeItem current = new Data.TradeItem();
            
            foreach (KeyValuePair<Data.Resource.Type, Resource> resource in location.resources)
            {
                current.type = resource.Key;
                current.amount = resource.Value.getResourcesOverTargetLimit();
                
                // export
                if (current.amount > 0.0f)
                {
                    current.isExported = true;
                    current.weight = resource.Value.effectiveMultiplier;
                    
                    // set weights
                    switch (resource.Value.policy)
                    {
                        case Data.Resource.Policy.Grow:
                            current.weight *= 0.9f;
                            break;
                        case Data.Resource.Policy.GrowTech:
                            current.weight *= 0.9f;
                            break;
                        case Data.Resource.Policy.Sustain:
                            current.weight *= 0.5f;
                            break;
                        case Data.Resource.Policy.Stockpile:
                            current.weight *= 0.8f;
                            break;
                        case Data.Resource.Policy.BareMinimum:
                            current.weight *= 2.0f;
                            break;
                        case Data.Resource.Policy.Downsize:
                            current.weight *= 0.0f;
                            break;
                        default:
                            current.weight *= 1.0f;
                            break;
                    }
                }  
                // import
                else 
                {
                    current.isExported = false;
                    current.amount = -current.amount;
                    current.weight = resource.Value.effectiveMultiplier > 2.0f ? 0 : 2.0f-resource.Value.effectiveMultiplier;
                    if (resource.Value.state == Data.Resource.State.Shortage) current.weight *= 3.0f;
                    
                    // set weights
                    switch (resource.Value.policy)
                    {
                        case Data.Resource.Policy.Grow:
                            current.weight *= 1.1f;
                            break;
                        case Data.Resource.Policy.GrowTech:
                            current.weight *= 1.1f;
                            break;
                        case Data.Resource.Policy.Sustain:
                            current.weight *= 1.2f;
                            break;
                        case Data.Resource.Policy.Stockpile:
                            current.weight *= 1.0f;
                            break;
                        case Data.Resource.Policy.BareMinimum:
                            current.weight *= 1.0f;
                            break;
                        case Data.Resource.Policy.Downsize:
                            current.weight *= 1.0f;
                            break;
                        default:
                            current.weight *= 1.0f;
                            break;
                    }
                }
                
                list.Add(current);
            }
            return list;
        }
    }

    public class LocationEconomy
    {
        internal Dictionary<Data.Resource.Type, Resource> resources = new Dictionary<Data.Resource.Type, Resource>();
		internal Dictionary<Data.Tech.Type, Tech> technologies = new Dictionary<Data.Tech.Type, Tech>();

        public List<Data.TradeItem> tradeItems = new List<Data.TradeItem>();

        public Data.Tech.Type? techGoal;          // for debugging purposes, expose to inspector
        public Data.Resource.Type? resourceGoal;  

        Location location;
		LocationEconomyAI ai;

        public LocationEconomy(Location location, LocationEconomyAI ai)
        {
            this.location = location;
            this.ai = ai;

            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)))
            {
                Data.Resource resourceData = Data.Resource.generateDebugData(type);
                resources[type] = new Resource(resourceData, new ResourcePool(resourceData), location);
            }

            foreach (Data.Tech.Type type in Enum.GetValues(typeof(Data.Tech.Type)))
            {
                Data.Tech techData = Data.Tech.generateDebugData(type);
                technologies.Add(type, new Tech(techData));
            }
 
            this.techGoal = null;
            this.resourceGoal = null;
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

        internal void setTechGoal(Data.Tech.Type? type)
        {
            techGoal = type;
        }
        internal void setResourceGoal(Data.Resource.Type? type)
        {
            resourceGoal = type;
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
                    if (resources[type].level >= technologies[Data.Tech.Type.Infrastructure].level)
                    {
                        return false;
                    }
                }
                // not enough military tech
                else if (type == Data.Resource.Type.Military)
                {
                    if (resources[type].level >= technologies[Data.Tech.Type.Military].level)
                    {
                        return false;
                    }
                }
                // has enough technology
                else return true;
            }
            return false;
        }
        internal bool hasEnoughMaterialsToUpgradeTech(Data.Tech.Type type)
        {
            foreach (Resource resource in resources.Values)
            {
                // if resource is set to 'grow' (is part of the tech goal) & doesn't have enough resource to upgrade tech = fails
                if (resource.policy == Data.Resource.Policy.Grow && resource.state != Data.Resource.State.AtGrowLimit) return false;
            }
            return true;
        }

        internal void upgradeResource(Data.Resource.Type type)
        {
            if (hasEnoughMaterialsToUpgrade(type) && hasEnoughTechToUpgrade(type))
            {
                resources[type].upgrade();
            }
        }
        internal void upgradeTech(Data.Tech.Type type)
        {
            if (hasEnoughMaterialsToUpgradeTech(type)) // tech prerequisites already checked in policy
            {
                technologies[type].upgrade();
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