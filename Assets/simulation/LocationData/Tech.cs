using System;
using System.Collections;
using System.Collections.Generic;

namespace Data
{
    public class Tech
    {
        public enum Type { Technology, Infrastructure, Military };

        public Tech.Type type;
        public int level = 1;

        public Tech(Type type)
        {
            this.type = type;
        }

        public static Tech generateDebugData(Type type)
        {
            Tech rv = new Tech(type);
            rv.level = 1;
            return rv;
        }
    }
}