using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace NewEconomy
{
    public class Tech
    {
		public enum Type { None, Technology, Infrastructure, Military };

		public Type type {get; private set; }
		public int level { get; private set; }

        public Tech(Type type, int startingLevel)
		{
			this.type = type;
			this.level = startingLevel;
		}


		internal static Type getEligibleTechGoal(Resource.Type resourceGoal, LocationEconomy location)
		{
			Type techGoal;
			// accrue tech goal from most successful resource / Technology Infrastructure Military
			if (resourceGoal == Resource.Type.Food) techGoal = Type.Infrastructure;
			else if (resourceGoal == Resource.Type.Mineral) techGoal = Type.Infrastructure;
			else if (resourceGoal == Resource.Type.BlackMarket) techGoal = Type.Infrastructure;
			else if (resourceGoal == Resource.Type.Innovation) techGoal = Type.Technology;
			else if (resourceGoal == Resource.Type.Culture) techGoal = Type.Technology;
			else if (resourceGoal == Resource.Type.Industry) techGoal = Type.Military;
			else if (resourceGoal == Resource.Type.Economy) techGoal = Type.Technology;
			else if (resourceGoal == Resource.Type.Military) techGoal = Type.Military;

			else { techGoal = Type.None; Debug.Log ("WARNING: no proper key found"); }
			if (techGoal == Type.None) return Type.None;

			// check tech level pre-requisits for selected tech goal
			else if (techGoal == Type.Technology) return Type.Technology;
			else if (techGoal == Type.Infrastructure)
			{
				if (location.technologies[Type.Technology].level <= location.technologies[Type.Infrastructure].level) return Type.Technology;
				return Type.Infrastructure;
			}
			else if (techGoal == Type.Military)
			{
				if (location.technologies[Type.Technology].level < location.technologies[Type.Infrastructure].level) return Type.Technology;
				if (location.technologies[Type.Infrastructure].level <= location.technologies[Type.Military].level) return Type.Infrastructure;
				return Type.Military;
			}
			return Type.None;
		}

		internal static List<Resource.Type> getResourcesForTech(Tech.Type type, LocationEconomy location)
		{
			List<Resource.Type> list = new List<Resource.Type>();

			if (type == Type.None) return list;
			else if (type == Type.Technology)
			{
				if (location.resources[Resource.Type.Innovation].level <= location.technologies[Type.Technology].level) list.Add (Resource.Type.Innovation);
				if (location.resources[Resource.Type.Culture].level <= location.technologies[Type.Technology].level) list.Add (Resource.Type.Culture);
			}
			else if (type == Type.Infrastructure)
			{
				// tech check
				if (location.technologies[Type.Technology].level < location.technologies[Type.Infrastructure].level) Debug.LogWarning ("infrastructure upgrade attempted without sufficient technology level.");
				if (location.resources[Resource.Type.Industry].level <= location.technologies[Type.Technology].level) list.Add (Resource.Type.Economy);
				if (location.resources[Resource.Type.Economy].level <= location.technologies[Type.Technology].level) list.Add (Resource.Type.Economy);
				list.Add (Resource.Type.Innovation); // should alrady be adequate level by technology level
			}
			else if (type == Type.Military)
			{
				if (location.technologies[Type.Technology].level < location.technologies[Type.Infrastructure].level) Debug.LogWarning ("military tech upgrade attempted without sufficient infrastructure level.");
				if (location.resources[Resource.Type.Military].level <= location.technologies[Type.Technology].level) list.Add (Resource.Type.Military);
				list.Add (Resource.Type.Innovation); // should alrady be adequate level by technology level
			}

			return list;
		}
    }
}