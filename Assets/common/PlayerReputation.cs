using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerReputation
{
    // SAVE data
    private Dictionary<Faction.FactionID, float> factionReputation = new Dictionary<Faction.FactionID, float>();
    private Dictionary<string, float> locationReputation = new Dictionary<string, float>();

    /// <summary>
    /// constructor for new game's starting reputations by factions
    /// </summary>
    /// <param name="startingFaction"></param>
    public PlayerReputation(Faction.FactionID? startingFaction = null) // TODO: refactor these elsewhere, library or world startValues parse file
    {
        if (startingFaction != null)
        {
            foreach (Faction.FactionID faction in Enum.GetValues(typeof(Faction.FactionID)))
                if (faction != startingFaction)
                    addReputation(faction, Root.game.factions.getRelations((Faction.FactionID)startingFaction, faction));

            // starting reputation inside your own faction
            addReputation((Faction.FactionID)startingFaction, 20f);
        }
        //Debug.Log(ToDebugString());
    }
    /// <summary>
    /// player's reputation with a faction. 
    /// Value range is -100 - +100
    /// </summary>
    /// <param name="faction"></param>
    /// <returns></returns>
    public float getReputationValue(Faction.FactionID faction)
    {
        return (factionReputation.ContainsKey(faction)) ? factionReputation[faction] : 0f;
    }
    /// <summary>
    /// ALL factions represent rep + location's own rep
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public float getReputation(Location location)
    {
        float rep = 0;
        //factions rep by faction presence
        foreach (var pair in location.features.factionCtrl)
        {
            rep += getReputationValue(pair.Key) * pair.Value;
        }
        // location's individual reputation
        return (locationReputation.ContainsKey(location.id)) ? locationReputation[location.id] +rep : rep;
    }
    /// <summary>
    /// typically reputation range is -100 - +100
    /// +80: Admiralty, Battleships
    /// +50: Ally, active cooperation, BC
    /// +40: Great Captain, military trust (cruisers)
    /// +20: Captain, cooperation, corvette/M transport
    /// -20: distrust, easily turns hostile
    /// -40: open rivarly/hostility
    /// -50: war, active
    /// -80: Vendetta
    /// -100: Nemesis
    /// </summary>
    /// <param name="faction"></param>
    /// <param name="value"></param>
    public void addReputation(Faction.FactionID faction, float value)
    {
        if (factionReputation.ContainsKey(faction)) factionReputation[faction] += value;
        else factionReputation.Add(faction, value);
    }
    /// <summary>
    /// add reputation to location
    /// </summary>
    /// <param name="location"></param>
    /// <param name="value"></param>
    public void addReputation(Location location, float value) { addReputation(location.id, value); }
    public void addReputation(string locationId, float value)
    {
        float factionCtrShare = 0;
        // distribute reputation to present factions by their share
        foreach (var pair in Root.game.locations[locationId].features.factionCtrl)
        {
            if (pair.Value != 0f)
            {
                factionReputation[pair.Key] += value * pair.Value;
                factionCtrShare += pair.Value;
            }
        }
        // add location reputation
        // location reputation is independent of faction reputation
        if (locationReputation.ContainsKey(locationId)) locationReputation[locationId] += value * (1f - factionCtrShare);
        else locationReputation.Add(locationId, value * (1f - factionCtrShare));
    }
    //public void setReputation(Faction.FactionID faction, float value)
    //{
    //    if (reputation.ContainsKey(faction)) reputation[faction] = value;
    //    else reputation.Add(faction, value);
    //}
    public string ToDebugString()
    {
        string rs = "Player Reputation:\n";
        foreach (Faction.FactionID faction in Enum.GetValues(typeof(Faction.FactionID)))
            rs += Faction.getFactionName(faction)+": "+ getReputationValue (faction)+ "\n";
        return rs;
    }
}
