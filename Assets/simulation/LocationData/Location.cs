using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Data
{
    public class Location
    {
        public enum Visibility { Connected, Hiding, Lost };

        //public Dictionary<Resource.Type, Resource> resources =
        //    new Dictionary<Resource.Type, Resource>();
        //public Dictionary<Tech.Type, Tech> techs =
        //    new Dictionary<Tech.Type, Tech>();
        public LocationFeatures features = new LocationFeatures();



        public Location()
        {
            //foreach (Resource.Type type in Enum.GetValues(typeof(Resource.Type)))
            //{
            //    resources.Add(type, new Resource(type));
            //}
            //foreach (Tech.Type type in Enum.GetValues(typeof(Tech.Type)))
            //{
            //    techs.Add(type, new Tech(type));
            //}
        }
    }
}

