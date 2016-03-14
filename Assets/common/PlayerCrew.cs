using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// crew effects (skills)
/// </summary>
public class PlayerCrew
{
    
    Dictionary<Faction.IdeologyID, float> support = new Dictionary<Faction.IdeologyID, float>();
    //Dictionary<Character.Stat, float> stats = new Dictionary<Character.Stat, float>();
    Dictionary<Simulation.Effect, float>  effects = new Dictionary<Simulation.Effect, float>();
    //public Dictionary<Data.Resource.Type, float> resourceMultiplier = new Dictionary<Data.Resource.Type, float>(); // ideology-based multiplier

    public PlayerCrew(Location location)
    {
        // load location ideology
        // <-- the current location spread to represent ship population 
        // (taking current stats and faction controls into account)
        support = location.ideology.support;
        effects = Simulation.LocationIdeology.getCalculatedEffects(support);

        // debug ideologies
        //foreach (var pair in support)
        //    Debug.Log("Ship support [" + pair.Key + "] " + Mathf.Round(pair.Value * 100f) / 100f);

        // debug stats
        //foreach (var pair in stats)
        //    Debug.Log("Ship stats [" + pair.Key + "] " + Mathf.Round(pair.Value * 100f) / 100f);

        // debug effects
        //foreach (var pair in effects)
        //    Debug.Log("Ship effect [" + pair.Key + "] " + Mathf.Round(pair.Value * 100f) / 100f);
    }

    public float getStat(Simulation.Effect effect)
    {
        if (effects.ContainsKey(effect))
            return effects[effect];
        else
        {
            Debug.LogError("Simulation.Effect entry '" + effect + "' was not found in 'effects' dictionary");
            return 0f;
        }
    }
}
