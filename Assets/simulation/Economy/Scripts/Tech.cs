using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;

namespace Simulation
{
    public class Tech
    {
        private Data.Tech data;
        public int level
        {
            get { return data.level; }
            set { data.level = value; }
        }

        public Tech(Data.Tech data)
        {
            this.data = data;
        }

        internal void upgrade()
        {
            data.level++;
        }
        internal void downgrade()
        {
            data.level--;
        }

        internal static Data.Tech.Type? getEligibleTechGoal(Data.Resource.Type? resourceGoal,
                                                            LocationEconomy location)
        {            
            Data.Tech.Type? techGoal;
            
            // accrue tech goal from most successful resource / Technology Infrastructure Military
            if (resourceGoal == Data.Resource.Type.Food) techGoal = Data.Tech.Type.Infrastructure;
            else if (resourceGoal == Data.Resource.Type.Mineral) techGoal = Data.Tech.Type.Infrastructure;
            else if (resourceGoal == Data.Resource.Type.BlackMarket) techGoal = Data.Tech.Type.Infrastructure;
            else if (resourceGoal == Data.Resource.Type.Innovation) techGoal = Data.Tech.Type.Technology;
            else if (resourceGoal == Data.Resource.Type.Culture) techGoal = Data.Tech.Type.Technology;
            else if (resourceGoal == Data.Resource.Type.Industry) techGoal = Data.Tech.Type.Military;
            else if (resourceGoal == Data.Resource.Type.Economy) techGoal = Data.Tech.Type.Technology;
            else if (resourceGoal == Data.Resource.Type.Military) techGoal = Data.Tech.Type.Military;
            else
            {
                Debug.Log("WARNING: no proper key found");
                return null;
            }

            // check tech level pre-requisits for selected tech goal
            if (techGoal == Data.Tech.Type.Technology)
            {
                return Data.Tech.Type.Technology;
            }
            else if (techGoal == Data.Tech.Type.Infrastructure)
            {
                if (location.technologies[Data.Tech.Type.Technology].level <=
                    location.technologies[Data.Tech.Type.Infrastructure].level)
                {
                    return Data.Tech.Type.Technology;
                }
                else
                {
                    return Data.Tech.Type.Infrastructure;
                }
            }
            else if (techGoal == Data.Tech.Type.Military)
            {
                if (location.technologies[Data.Tech.Type.Technology].level <
                    location.technologies[Data.Tech.Type.Infrastructure].level)
                {
                    return Data.Tech.Type.Technology;
                }
                if (location.technologies[Data.Tech.Type.Infrastructure].level <=
                    location.technologies[Data.Tech.Type.Military].level)
                {
                    return Data.Tech.Type.Infrastructure;
                }
                return Data.Tech.Type.Military;
            }

            return null;
        }

        internal static List<Data.Resource.Type> getResourcesForTech(Data.Tech.Type type, LocationEconomy location)
        {
            List<Data.Resource.Type> list = new List<Data.Resource.Type>();

            if (type == Data.Tech.Type.Technology)
            {
                if (location.resources[Data.Resource.Type.Innovation].level <=
                    location.technologies[Data.Tech.Type.Technology].level)
                {
                    list.Add(Data.Resource.Type.Innovation);
                }
                if (location.resources[Data.Resource.Type.Culture].level <=
                    location.technologies[Data.Tech.Type.Technology].level)
                {
                    list.Add(Data.Resource.Type.Culture);
                }
            }
            else if (type == Data.Tech.Type.Infrastructure)
            {
                // tech check
                if (location.technologies[Data.Tech.Type.Technology].level <
                    location.technologies[Data.Tech.Type.Infrastructure].level)
                {
                    Debug.LogWarning("infrastructure upgrade attempted without sufficient technology level.");
                }
                if (location.resources[Data.Resource.Type.Industry].level <=
                    location.technologies[Data.Tech.Type.Technology].level)
                {
                    list.Add(Data.Resource.Type.Economy);
                }
                if (location.resources[Data.Resource.Type.Economy].level <=
                    location.technologies[Data.Tech.Type.Technology].level)
                {
                    list.Add(Data.Resource.Type.Economy);
                }
                list.Add(Data.Resource.Type.Innovation); // should alrady be adequate level by technology level
            }
            else if (type == Data.Tech.Type.Military)
            {
                if (location.technologies[Data.Tech.Type.Technology].level <
                    location.technologies[Data.Tech.Type.Infrastructure].level)
                {
                    Debug.LogWarning("military tech upgrade attempted without sufficient infrastructure level.");
                }
                if (location.resources[Data.Resource.Type.Military].level <=
                    location.technologies[Data.Tech.Type.Technology].level)
                {
                    list.Add(Data.Resource.Type.Military);
                }
                list.Add(Data.Resource.Type.Innovation); // should alrady be adequate level by technology level
            }

            return list;
        }
    }
}