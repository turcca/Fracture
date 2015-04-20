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
            //// if negative total growth
            //if (location.totalEffectiveMultiplier < 1.0f)
            //{
            //    // POLICY: find out the worse resource to downgrade
            //    int lastIndex = location.sortedResourceTypes.Count-1;

            //    if (location.sortedResourceTypes[lastIndex].Value <1.0f) 
            //    { 
            //        for (int i = lastIndex; i >=0; i--)
            //        {
            //            // set last & bad resources to 'Downsize'
            //            if (i == lastIndex || location.sortedResourceTypes[i].Value < 0.3f) location.setPolicy(location.sortedResourceTypes[i].Key, Resource.Policy.Downsize);
            //            // set negative ones to 'Import'
            //            else if (location.sortedResourceTypes[i].Value < 1.0f) location.setPolicy(location.sortedResourceTypes[i].Key, Resource.Policy.Import);
            //            // set positive ones to 'export'
            //            else if (location.sortedResourceTypes[i].Value > 1.0f) location.setPolicy(location.sortedResourceTypes[i].Key, Resource.Policy.Export);
            //            // else sustain
            //            else location.setPolicy(location.sortedResourceTypes[i].Key, Resource.Policy.Sustain);
            //        }
            //    }
            //    else Debug.LogWarning ("WARNING: lowest sorted value >=1, even if 'totalEffectiveMul was <1");
            //}
            //// Positive growth! Find out best resources to upgrade (if total effective multiplier >=1)
            //else
            //{
            //    // POLICY: find overall strategy goal & figure out needed resources for that
            //    Resource.Type resourceGoal = location.sortedResourceTypes[0].Key;
            //    List<Resource.Type> resourceGoals = new List<Resource.Type>();
            //    Tech.Type techGoal = Tech.Type.Technology;

            //    ///@todo ASSET GOALS?


            //    // see if resource goal is achiavable
            //    foreach (KeyValuePair<Resource.Type, float> sorted in location.sortedResourceTypes)
            //    {
            //        if (location.resources[sorted.Key].level < 4)
            //        {
            //            //if (Resource.isResourceUpgradeableByTech(sorted.Key, location)) 
            //            //{
            //            //    // >resource goal
            //            //    resourceGoals.Add (sorted.Key);
            //            //    techGoal = Tech.Type.None;
            //            //    break;
            //            //}
            //        }
            //    }
            //    // needs a tech goal
            //    if (techGoal != Tech.Type.None) 
            //    {
            //        // >needs tech upgrade
            //        techGoal = Tech.getEligibleTechGoal(resourceGoal, location);
            //        resourceGoals = Tech.getResourcesForTech(techGoal, location);
            //    }

            //    // go through resources and set policies
            //    foreach(KeyValuePair<Resource.Type, float> sorted in location.sortedResourceTypes)
            //    {
            //        // if resource is listed in resourceGoals, grow
            //        if (resourceGoals.Contains (sorted.Key)) location.setPolicy (sorted.Key, Resource.Policy.Grow);
            //        // if producing, export
            //        else if (sorted.Value > 1.0f) location.setPolicy (sorted.Key, Resource.Policy.Export);
            //        // else needs to import
            //        else if (sorted.Value < 1.0f) location.setPolicy (sorted.Key, Resource.Policy.Import);
            //        else location.setPolicy (sorted.Key, Resource.Policy.Sustain);
            //    }
            //    ///todo check after resource state allocation if total import mul > export mul. Can policies can be afforded?
            //}
            ////location.setPolicy(Resource.Type.Military, Resource.Policy.Grow);
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
        internal Dictionary<Data.LocationResource.Type, Resource> resources = new Dictionary<Data.LocationResource.Type, Resource>();
		//internal Dictionary<Tech.Type, Tech> technologies = new Dictionary<Tech.Type, Tech>();

        //internal List<KeyValuePair<Resource.Type, float>> sortedResourceTypes = new List<KeyValuePair<Resource.Type, float>>(); // sorted by (float) effective resource multiplier - might be nice to see in the inspector
		//internal float totalEffectiveMultiplier;
		LocationEconomyAI ai;

        public LocationEconomy(Data.Location data, LocationEconomyAI ai)
        {
            this.ai = ai;
            foreach (Data.LocationResource.Type type in Enum.GetValues(typeof(Data.LocationResource.Type)))
            {
                Data.LocationResource resourceData = Data.LocationResource.generateDebugData(type);
                resources[type] = new Resource(resourceData, new ResourcePool(resourceData));
            }
            //foreach (Tech.Type type in Enum.GetValues(typeof(Tech.Type)))
            //{
            //    ///@todo create tech based on input data
            //    if (type != Tech.Type.None) technologies.Add(type, new Tech(type, 1));
            //}
            
        }

        // ----------------------------------------------------------TICK
        public void tick(float delta)
        {
			// calculate totalEffectiveMultiplier for the tick // doesn't change unless location stats change
			//totalEffectiveMultiplier = getTotalEffectiveLocationResourceMultiplier(location);
			// sort resources by resource multiplier - ie. how fast it's generating production // doesn't change unless location stats change
			//sortedResourceTypes = getSortedResourceTypes(location);

			ai.setPolicies(this);

            foreach (Resource resource in resources.Values)
            {
                resource.tick(delta);
            }

            ai.manageLocationUpgrades(this);

            ai.setupTrades(this);
        }
        // ----------------------------------------------------------

        //internal void setPolicy(Resource.Type type, Resource.Policy policy)
        //{
        //    resources[type].policy = policy;
        //}

        //internal bool hasEnoughMaterialsToUpgrade(Resource.Type type)
        //{
        //    return resources[type].state == Resource.State.AtGrowLimit;
        //}

        //internal bool hasEnoughTechToUpgrade(Resource.Type type)
        //{
        //    if (resources[type].level < 4 &&
        //        resources[type].level < technologies[Tech.Type.Technology].level)
        //    {
        //        // not enough infra
        //        if (type == Resource.Type.Industry || type == Resource.Type.Mineral || type == Resource.Type.Economy)
        //        {
        //            if (resources[type].level < technologies[Tech.Type.Infrastructure].level)
        //            {
        //                return true;
        //            }
        //        }
        //        // not enough military tech
        //        else if (type == Resource.Type.Military)
        //        {
        //            if (resources[type].level < technologies[Tech.Type.Military].level)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        //internal void upgradeResource(Resource.Type type)
        //{
        //    if (hasEnoughMaterialsToUpgrade(type) && hasEnoughTechToUpgrade(type))
        //    {
        //        resources[type].upgrade();
        //    }
        //}

        internal string toDebugString()
        {
            string rv = "";
            foreach (Resource resource in resources.Values)
            {
                rv = rv + resource.toDebugString() + "\n";
            }
            return rv;
        }

        //internal void updateFeatures(LocationFeatures features)
        //{
        //    foreach (var pair in resources)
        //    {
        //        pair.Value.updateFeatures(features);
        //    }
        //}

        //internal IEnumerable<Resource> getResources()
        //{
        //    return resources.Values;
        //}

        //// location economy variables

        //public float getTotalLocationResourceMultiplier(LocationFeatures features)
        //{
        //    float mul = 1;
        //    foreach (var pair in resources)
        //    {
        //        mul *= pair.Value.level > 0 ? features.resourceMultiplier[pair.Key] : 1.0f;
        //    }
        //    return mul;
        //}
        //public float getTotalEffectiveLocationResourceMultiplier(Location location)
        //{
        //    float mul = 1;
        //    foreach (var pair in resources)
        //    {
        //        mul *= pair.Value.level > 0 ? getEffectiveMul(location, pair.Key) : 1.0f; 
        //    }
        //    return mul;
        //}
        //internal float getEffectiveMul(Location location, Resource.Type type)
        //{
        //    // should probably get all the multipliers at once for getSortedResourceTypes
        //    //return location.features.resourceMultiplier[type] * location.ideology.resourceMultiplier[type];
        //    if (type == Resource.Type.Food) return location.features.resourceMultiplier[Resource.Type.Food] * location.ideology.effects.foodMul;
        //    else if (type == Resource.Type.Mineral) return location.features.resourceMultiplier[Resource.Type.Mineral] * location.ideology.effects.mineralsMul;
        //    else if (type == Resource.Type.BlackMarket) return location.features.resourceMultiplier[Resource.Type.BlackMarket] * location.ideology.effects.blackMarketMul;
        //    else if (type == Resource.Type.Innovation) return location.features.resourceMultiplier[Resource.Type.Innovation] * location.ideology.effects.innovationMul;
        //    else if (type == Resource.Type.Culture) return location.features.resourceMultiplier[Resource.Type.Culture] * location.ideology.effects.cultureMul;
        //    else if (type == Resource.Type.Industry) return location.features.resourceMultiplier[Resource.Type.Industry] * location.ideology.effects.industryMul;
        //    else if (type == Resource.Type.Economy) return location.features.resourceMultiplier[Resource.Type.Economy] * location.ideology.effects.economyMul;
        //    else if (type == Resource.Type.Military) return location.features.resourceMultiplier[Resource.Type.Military] * location.ideology.effects.militaryMul;
        //    else { Debug.LogWarning("WARNING: bad input type."); return 0; }
        //}

        //internal List<KeyValuePair<Resource.Type, float>> getSortedResourceTypes(Location location)
        //{
        //    List<KeyValuePair<Resource.Type, float>> sortedList = new List<KeyValuePair<Resource.Type, float>>();
            
        //    foreach(Resource.Type type in Enum.GetValues(typeof(Resource.Type)))
        //    {
        //            sortedList.Add (new KeyValuePair<Resource.Type, float>(type, getEffectiveMul(location, type)));
        //    }
            
        //    // Sort list by values
        //    sortedList.Sort(
        //            delegate(KeyValuePair<Resource.Type, float> firstPair,
        //             KeyValuePair<Resource.Type, float> nextPair)
        //            {
        //            return firstPair.Value.CompareTo(nextPair.Value);
        //    }
        //    );
        //    return sortedList;
        //}

    }
}