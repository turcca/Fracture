using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Simulation
{
    public class LocationEconomyAI
    {
        public void setPolicies(LocationEconomy location)
        {
            List<KeyValuePair<Data.Resource.Type, float>> sortedResourceTypes = location.getSortedResourceTypes();

            // set effective value /redundant? not used. Then again, this on is called first, if need to cache -->
            foreach (KeyValuePair<Data.Resource.Type, float> sorted in sortedResourceTypes)
            {
                location.resources[sorted.Key].setEffectiveMultiplier(sorted.Value);
            } // <---

            // if negative total growth
            if (location.getTotalEffectiveLocationResourceMultiplier() < 0.0f)
            {
                // POLICY: find out the worse resource to downgrade
                int lastIndex = sortedResourceTypes.Count - 1;

                if (sortedResourceTypes[lastIndex].Value < 1.0f)
                {
                    bool foundDowngradable = false;

                    for (int i = lastIndex; i >= 0; i--)
                    {
                        // set (sorted) last & bad resources to 'Downsize'
                        if (/*i == lastIndex ||*/ !foundDowngradable/*sortedResourceTypes[i].Value < 0.3f*/)
                        {
                            if (location.resources[sortedResourceTypes[i].Key].level > 0)
                            {
                                location.setPolicy(sortedResourceTypes[i].Key, Data.Resource.Policy.Downsize);
                                foundDowngradable = true;
                            }
                        }
                        // set negative ones to 'Import'
                        else if (sortedResourceTypes[i].Value < .999f)
                            location.setPolicy(sortedResourceTypes[i].Key, Data.Resource.Policy.Sustain);
                        // set positive ones to 'export'
                        else if (sortedResourceTypes[i].Value > 1.001f)
                            location.setPolicy(sortedResourceTypes[i].Key, Data.Resource.Policy.BareMinimum);
                        // else sustain
                        else
                            location.setPolicy(sortedResourceTypes[i].Key,
                           Data.Resource.Policy.Sustain);
                    }
                    if (!foundDowngradable) Debug.LogError(location.location.id + " needs to 'downgrade', but no resource was downgradable");
                }
                else Debug.LogWarning("WARNING: lowest sorted value >=1, even if 'totalEffectiveMul was <0");
            }
            // Positive growth! Find out best resources to upgrade (total effective multiplier >=1)
            else
            {
                // POLICY: find overall strategy goal & figure out needed resources for it
                location.setResourceGoal(null);
                bool foundGrowth = false;
                ///@todo ASSET GOALS?

                // see in order which resource goal is achiavable
                foreach (KeyValuePair<Data.Resource.Type, float> sorted in sortedResourceTypes)
                {
                    // see if resource can be 'grown' (resourceGoal)
                    if (!foundGrowth &&
                        location.resources[sorted.Key].level < 4 &&
                        sorted.Value > 1f &&
                        location.hasEnoughTechToUpgrade(sorted.Key))
                    {
                        // >resource goal
                        foundGrowth = true;
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
                if (!foundGrowth)
                {
                    // >needs tech upgrade
                    location.setTechGoal(Tech.getEligibleTechGoal(location.resourceGoal, location));
                    location.setResourceGoal(null);

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
            }
        }


        internal void manageLocationUpgrades(LocationEconomy location)
        {
            if (location.resourceGoal.HasValue)
            {
                location.upgradeResource( (Data.Resource.Type) location.resourceGoal);
            }
            else if (location.techGoal.HasValue)
            {
                location.upgradeTech( (Data.Tech.Type) location.techGoal);
            }
        }

        internal void setupTrades(LocationEconomy location)
        {
            location.tradeItems = getTradeList(location);
        }

        // gathers a list of articles, each with import/export status, quotas and weights 
        // to be compared against tradeList of other locations for best match
        // and also to show location inventory of commodities to player [Location.getLocationTradeList()]
        internal List<Data.TradeItem> getTradeList(LocationEconomy location)
        {
            List<Data.TradeItem> list = new List<Data.TradeItem>();
            Data.TradeItem current = new Data.TradeItem();
            
            foreach (KeyValuePair<Data.Resource.Type, Resource> resource in location.resources)
            {
                current.type = resource.Key;
                current.amount = resource.Value.getResourcesOverTargetLimit();
                
                // export
                if (current.amount > 0.001f)
                {
                    current.isExported = true;
                    current.weight = resource.Value.effectiveMultiplier;
                    
                    // set weights
                    switch (resource.Value.policy)
                    {
                        case Data.Resource.Policy.Grow:
                            Debug.LogWarning("exporting 'Grow'");
                            current.weight *= 0.9f;
                            break;
                        case Data.Resource.Policy.GrowTech:
                            current.weight *= 0.9f;
                            break;
                        case Data.Resource.Policy.Sustain:
                            current.weight *= 0.5f;
                            break;
                        case Data.Resource.Policy.Stockpile:
                            Debug.LogWarning("exporting 'Stockpile'");
                            current.weight *= 0.8f;
                            break;
                        case Data.Resource.Policy.BareMinimum:
                            current.weight *= 2.0f;
                            break;
                        case Data.Resource.Policy.Downsize:
                            //LogWarning("exporting 'Downsize' @"+location.location.id + " / resource: " + current.type.ToString() + ": " + -current.amount);
                            current.weight *= 0.0f;
                            break;
                        default:
                            current.weight *= 1.0f;
                            break;
                    }
                }  
                // import
                else if (current.amount < -0.001f)
                {
                    current.isExported = false;
                    current.amount = -current.amount;
                    current.weight = resource.Value.effectiveMultiplier > 2.0f ? 0 : 2.0f-resource.Value.effectiveMultiplier;
                    if (resource.Value.state == Data.Resource.State.Shortage) current.weight *= Parameters.resourceShortageMultiplier;
                    
                    // set weights
                    switch (resource.Value.policy)
                    {
                        case Data.Resource.Policy.Grow:
                            current.weight *= 0.8f;
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
                            //Debug.LogWarning("importing 'BareMinimum' @"+location.location.id+" / resource: "+current.type.ToString()+": "+ -current.amount); // may return values beyond float-tolerance values if delta between simulation ticks grow too large
                            current.weight *= 1.0f;
                            break;
                        case Data.Resource.Policy.Downsize:
                            current.weight *= 1.5f;
                            break;
                        default:
                            current.weight *= 1.0f;
                            break;
                    }
                }
                // at target limit
                else
                {
                    current.isExported = false;
                    current.amount = 0;
                    current.weight = resource.Value.effectiveMultiplier > 2.0f ? 0 : 2.0f - resource.Value.effectiveMultiplier;
                    if (resource.Value.state == Data.Resource.State.Shortage) current.weight *= Parameters.resourceShortageMultiplier;

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
                            current.weight *= 1f;
                            break;
                        case Data.Resource.Policy.Stockpile:
                            current.weight *= 1f;
                            break;
                        case Data.Resource.Policy.BareMinimum:
                            current.isExported = true;
                            current.weight *= 1.0f;
                            break;
                        case Data.Resource.Policy.Downsize:
                            current.weight *= 1.5f;
                            break;
                        default:
                            current.weight *= 1.0f;
                            break;
                    }
                }
                list.Add(new Data.TradeItem(current));
            }
            return list;
        }
    }

    public class LocationEconomy
    {
        internal Dictionary<Data.Resource.Type, Resource> resources = new Dictionary<Data.Resource.Type, Resource>();
		internal Dictionary<Data.Tech.Type, Tech> technologies = new Dictionary<Data.Tech.Type, Tech>();

        public List<Data.TradeItem> tradeItems = new List<Data.TradeItem>();

        public Data.Tech.Type? techGoal { get; private set; }
        public Data.Resource.Type? resourceGoal { get; private set; }  

        internal Location location { get; private set; }
		LocationEconomyAI ai;

        public LocationEconomy(Location location, LocationEconomyAI ai)
        {
            this.location = location;
            this.ai = ai;

            // starting resource init
            float popScale = Parameters.populationScaleMultiplier(location.features.population);

            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)))
            {
                Data.Resource resourceData = Data.Resource.generateDebugData(type);
                resources[type] = new Resource(resourceData, new ResourcePool(resourceData), location);
                resources[type].import( (location.features.resourceMultiplier[type] -1f) * popScale); // give initial resources
            }

            foreach (Data.Tech.Type type in Enum.GetValues(typeof(Data.Tech.Type)))
            {
                Data.Tech techData = Data.Tech.generateDebugData(type);
                technologies.Add(type, new Tech(techData));
            }
 
            this.techGoal = null;
            this.resourceGoal = null;

            loadFeaturesToEconomy();
        }

        // ----------------------------------------------------------TICK
        public void tick(float delta)
        {
			ai.setPolicies(this);

            foreach (Resource resource in resources.Values)
            {
                //if (float.IsNaN(resource.getResources())) Debug.Log ("nan"); else Debug.Log (resource.toDebugString()); 
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

        public Data.TradeItem getTradeItem (Data.Resource.SubType subType, List<Data.TradeItem> tradeList = null)
        {
            if (tradeList == null)
            {
                foreach (Data.TradeItem item in tradeItems)
                {
                    if (item.subType == subType) return item;
                }
            }
            else
            {
                foreach (Data.TradeItem item in tradeList)
                {
                    if (item.subType == subType) return item;
                }
            }
            Debug.LogError ("input error");
            return null;
        }

        internal string toDebugString()
        {
            string rv = "";
            rv += "Total resource net value: "+Mathf.Round (getTotalEffectiveLocationResourceMultiplier()*100)/100 + " ["+Mathf.Round(getTotalLocationResourceMultiplier()*100f)/100f+"] \n";
            if (resourceGoal.HasValue) rv += "Resource Goal: " + Enum.GetName(typeof(Data.Resource.Type), resourceGoal) + "\n";
            if (techGoal.HasValue) rv += "Tech Goal: "+ Enum.GetName(typeof(Data.Tech.Type), techGoal) + "\n";
            rv += "Tech: "+technologies[Data.Tech.Type.Technology].level+", Infra: "+technologies[Data.Tech.Type.Infrastructure].level+", Milit: "+technologies[Data.Tech.Type.Military].level +"\n";
            foreach (Resource resource in resources.Values)
            {
                rv += resource.toDebugString() + "\n";
            }
            rv += tradeListToDebugString();
            rv += shortagesToDebugString();

            return rv;
        }
        internal string tradeListToDebugString()
        {
            string rv = "\n tradeList: \n";
            foreach (Data.TradeItem item in tradeItems)
            {
                if (item.amount > 0)
                {
                    rv += item.isExported ? "exportable: " : "importable :";
                    rv += Enum.GetName(typeof(Data.Resource.Type), item.type) + ": "+ item.amount +"\n";
                }
            }
            return rv;
        }
        internal string shortagesToDebugString()
        {
            string rv = "";
            foreach (Data.Resource.Type type in getShortagedResources())
            {
                rv += "Shortage: "+ Enum.GetName(typeof(Data.Resource.Type), type) + "@ ["+ location.id +"]";
            }
            if (rv != "")
            {
                float m = Mathf.Round(getTotalEffectiveLocationResourceMultiplier()* 100.0f)/100.0f;
                rv += " [total eff mul: "+ m +"] \n";
            }
            return rv;
        }
        internal string shortagesToDebugStringFloating()
        {
            string rv = "";
            foreach (KeyValuePair<Data.Resource.Type, Resource> resource in resources)
            {
                if (resource.Value.state == Data.Resource.State.Shortage)
                {
                    rv += "Shortage: "+ Enum.GetName(typeof(Data.Resource.Type), resource.Key) + (" ("+Mathf.Round(resource.Value.getResources()*10)/10 +")\n");
                }
            }
            return rv;
        }
        internal string shortagesToStringItems()
        {
            string rv = "";
            foreach (KeyValuePair<Data.Resource.Type, Resource> resource in resources)
            {
                if (resource.Value.state == Data.Resource.State.Shortage)
                {
                    //rv += "Shortage: " + Enum.GetName(typeof(Data.Resource.Type), resource.Key) +"\n";
                    rv += "Shortage: " + Simulation.Trade.getCommodityDescription(resource.Key, false) +"\n";
                }
            }
            return rv;
        }

        //// location economy variables
        /// <summary>
        /// ResourceMultiplier (stagnant is 1.0) This is pure location feature value
        /// </summary>
        /// <returns></returns>
        public float getTotalLocationResourceMultiplier()
        {
            float mul = 1;
            foreach (var pair in resources)
            {
                mul *= /*pair.Value.level > 0 ?*/ location.features.resourceMultiplier[pair.Key] /*: 1.0f*/;
            }
            return mul;
        }
        /// <summary>
        /// what is the net gain between all the resources (0f is stagnant)
        /// </summary>
        /// <returns></returns>
        public float getTotalEffectiveLocationResourceMultiplier() 
        {
            float acc = 0;
            //float count = 0;
            foreach (var pair in resources)
            {
               if (pair.Value.level > 0)
                {
                    acc += location.economy.resources[pair.Key].getNetResourceProduction(); //getEffectiveMul(pair.Key);
                    //count++;
                }
            }
            //acc = count > 0 ? acc/count : 0f; 
            
            return acc;
        }
        /// <summary>
        /// Feature-multiplier * ideology-multiplier + adjuster value
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal float getEffectiveMul(Data.Resource.Type type)
        {
            return location.features.resourceMultiplier[type] * location.ideology.resourceMultiplier[type] +Parameters.getGlobatMarketAdjuster(type);
        }
        internal KeyValuePair<Data.Resource.Type, float> getBestResourceEffectiveMultiplier()
        {
            return getSortedResourceTypes()[0];
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
                    return nextPair.Value.CompareTo(firstPair.Value);
                }
            );
            return sortedList;
        }

        public void updateTradeItems()
        {
            ai.setupTrades(this);
        }

        internal void export(Data.Resource.Type type, float amount)
        {
            resources[type].export(amount);
        }

        internal void import(Data.Resource.Type type, float amount)
        {
            resources[type].import(amount);
        }
        internal bool hasShortage()
        {
            foreach (var pair in resources)
            {
                if (pair.Value.state == Data.Resource.State.Shortage)
                    return true;
            }
            return false;
        }
        internal List<Data.Resource.Type> getShortagedResources()
        {
            List<Data.Resource.Type> shortages = new List<Data.Resource.Type>();

            foreach (KeyValuePair<Data.Resource.Type, Resource> resource in resources)
            {
                if (resource.Value.state == Data.Resource.State.Shortage)
                    shortages.Add(resource.Key);
            }
            return shortages;
        }

        private void loadFeaturesToEconomy()
        {
            int startingLevel = 0;
            // load tech-level & resource-level features to economy              
            technologies[Data.Tech.Type.Technology].level = location.features.startingTechLevel;
            technologies[Data.Tech.Type.Infrastructure].level = location.features.startingInfrastructure;
            technologies[Data.Tech.Type.Military].level = location.features.startingMilitaryTechLevel;

            // resolve resource starting levels from starting tech levels (parsed from location features)
            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)) )
            {
                // infra-dependent industry
                if (type == Data.Resource.Type.Food || type == Data.Resource.Type.Mineral || type == Data.Resource.Type.Industry) 
                {
                    if (getEffectiveMul(type) <1.0f) { startingLevel = (location.features.startingInfrastructure > 0) ? 1 : 0; } // non-developped industry: resource = 1, except if infra = 0, then 0
                    else { startingLevel = (location.features.startingInfrastructure != 1) ? (Mathf.Max (location.features.startingInfrastructure-1, 0)) : 1; } // developped industry: infra0 = 0, infra1 = 1, infra2 = 1, infra3 = 2, infra4 = 3
                }
                // tech-dependent industry
                else if (type == Data.Resource.Type.Economy || type == Data.Resource.Type.Innovation || type == Data.Resource.Type.Culture)  
                {
                    if (getEffectiveMul(type) <1.0f) { startingLevel = (location.features.startingTechLevel > 0) ? 1 : 0; } // non-developped industry: resource = 1, except if tech = 0, then 0
                    else { startingLevel = location.features.startingTechLevel; } // resource = tech
                }
                // military-dependent industry
                else if (type == Data.Resource.Type.Military)  
                {
                    if (getEffectiveMul(type) <1.0f) { startingLevel = (location.features.startingMilitaryTechLevel > 0) ? 1 : 0; } // non-developped industry: resource = 1, except if tech = 0, then 0
                    else { startingLevel = location.features.startingMilitaryTechLevel; } // resource = tech
                }
                else if (type == Data.Resource.Type.BlackMarket) 
                {
                    if (getEffectiveMul(type) <1.0f) { startingLevel = (location.features.startingTechLevel > 0) ? 1 : 0; } // non-developped industry: resource = 1, except if tech = 0, then 0
                    else { startingLevel = location.features.startingTechLevel; } // resource = tech
                    if (location.features.visibility == Data.Location.Visibility.Hiding) startingLevel += 1; // +1 from hiding
                    startingLevel += (int)(0.0f - ((location.ideology.effects[Effect.police] *3.0f)+1.0f));         // -3 to +2 from ideology
                    startingLevel = Mathf.Min (startingLevel, location.features.startingTechLevel);         // cap to max tech lvl

                    // SETTING LEGALITY legality
                    location.features.legality = Mathf.Clamp(startingLevel, 0, 4);
                    //Debug.Log (location.name+" ("+location.id+") blackMarket starting lvl: "+startingLevel);
                }
                resources[type].level = Mathf.Clamp(startingLevel, 0, 4);
            }

            // resolve starting resource pools
            foreach (Data.Resource.Type type in Enum.GetValues(typeof(Data.Resource.Type)) )
            {
                resources[type].import (resources[type].level * 2);
            }
        }
    }
}