using UnityEngine;
using System;
using System.Collections;


namespace Simulation
{
    /// <summary>
    /// Faction & F2F action!
    /// Factions script holds faction data in .data, and drives factions behaviour
    /// Player to Faction data is in Player
    /// </summary>
    public class Factions
    {
        private FactionData data;

        // TODO
        // faction 2 faction relations and status [peace, rivarly, war]

        private float tickBuffer = 0f;


        public Factions()
        {
            data = new FactionData();
            Debug.Log(ToDebugString());
        }


        public void tick(float tick)
        {
            tickBuffer += tick;
            if (tickBuffer >= 0.5f)
            {
                data.tick(tickBuffer);
                tickBuffer = 0f;
            }
        }



        public string getLeader(Faction.FactionID faction)
        {
            return data.getFactionLeader(faction);
        }
        public float getRelations(Faction.FactionID factionsOpinion, Faction.FactionID toFaction)
        {
            return data.getFactionToFactionRelations(factionsOpinion, toFaction);
        }
        public void addFactionRelationItem(Faction.FactionID factionsOpinion, Faction.FactionID toFaction, float itemLifetime, string description, float staticModifier = 0f, float dailyRelationsModifier = 0f, bool startInDecline = false)
        {
            data.addFactionRelationItem(factionsOpinion, toFaction, itemLifetime, description, staticModifier, dailyRelationsModifier, startInDecline);
        }

        public string getFactionRelationsDescription(Faction.FactionID? Parsedfaction)
        {
            if (Parsedfaction == null) { Debug.LogError("Unable to parse faction"); return ""; }
            Faction.FactionID faction = (Faction.FactionID)Parsedfaction;

            string rs = "";
            float relationValue;

            foreach (Faction.FactionID f in Enum.GetValues(typeof(Faction.FactionID)))
            {
                if (faction != f)
                {
                    relationValue = data.getFactionToFactionRelations(faction, f);

                    if (relationValue >= 50) rs += Faction.getFactionName(f) + " is our ally. You should help them whenever you can, and we'll see to it that you are compensated.\n";
                    else if (relationValue >= 20) rs += Faction.getFactionName(f) + " are our friends, and you should consider them friends as well, if our relations concern you. Help them if you can.\n";
                    else if (relationValue >= 10) rs += Faction.getFactionName(f) + ". You shouldn't act against them, or then do so at the peril of complicating our relations.\n";
                    //else if (relationValue >= 0)    rs += Faction.getFactionName(f) + ". You shouldn't act against them, or then do so at the peril of complicating our relations.\n";
                    else if (relationValue <= -70) rs += Faction.getFactionName(f) + " are our sworn enemies. Kill them all and you shall be greatly rewarded!\n";
                    else if (relationValue <= -50) rs += Faction.getFactionName(f) + " are our enemies. You are sanctioned to attack all of their assets, and be rewarded for it.\n";
                    else if (relationValue <= -20) rs += Faction.getFactionName(f) + " are our rivals. If you can find a way to hinder their efforts , we will see that you are compensated for your efforts. Now I should ask you not to break any rules of engagement, but it is little bit more complicated for private contractors. If you know what I am saying.\n";
                    else if (relationValue <= -10) rs += Faction.getFactionName(f) + " can be a nuissance, but you really shouldn't feel free to make their lives more complicated. Of course, if there were circumstances that were within the boundaries of law, we'd be able to openly compensate you for aggressive market strategies.\n";
                    //else if (relationValue < 0)     rs += Faction.getFactionName(f) + " can be a nuissance, but what can you do? It is not the time to make enemies.\n";
                }
            }
            // TODO per-faction reactions
            return rs;
        }

        public string ToDebugString()
        {
            return data.ToDebugString();
        }
    }
}