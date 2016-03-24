using System;
using System.Collections.Generic;
using UnityEngine;
//using System.IO;
//using UnityEngine.UI;
//using System.Globalization;


namespace Simulation
{
    public class FactionData
    {
        // SAVE data

        /// <summary>
        /// faction rulers, 1 per faction, live at hq
        /// </summary>
        Dictionary<Faction.FactionID, string> factionRuler = new Dictionary<Faction.FactionID, string>();

        Dictionary<Faction.FactionID, Dictionary<Faction.FactionID, float>> f2fRelations = new Dictionary<Faction.FactionID, Dictionary<Faction.FactionID, float>>();
        Dictionary<Faction.FactionID, List<FactionRelationItem>> factionRelationItems = new Dictionary<Faction.FactionID, List<FactionRelationItem>>();


        public FactionData()
        {
            // randomize rulers -- get the HQ -location ruler?
            factionRuler[Faction.FactionID.noble1] = NameGenerator.getName(Faction.FactionID.noble1);
            factionRuler[Faction.FactionID.noble2] = NameGenerator.getName(Faction.FactionID.noble2);
            factionRuler[Faction.FactionID.noble3] = NameGenerator.getName(Faction.FactionID.noble3);
            factionRuler[Faction.FactionID.noble4] = "Calius"; //NameGenerator.getName(Faction.FactionID.noble4);
            factionRuler[Faction.FactionID.guild1] = NameGenerator.getName(Faction.FactionID.guild1);
            factionRuler[Faction.FactionID.guild2] = NameGenerator.getName(Faction.FactionID.guild2);
            factionRuler[Faction.FactionID.guild3] = NameGenerator.getName(Faction.FactionID.guild3);
            factionRuler[Faction.FactionID.church] = NameGenerator.getName(Faction.FactionID.church);
            factionRuler[Faction.FactionID.heretic] = NameGenerator.getName(Faction.FactionID.heretic);

            // f2fRelations
            foreach (Faction.FactionID faction1 in Enum.GetValues(typeof(Faction.FactionID)))
            {
                factionRelationItems.Add(faction1, new List<FactionRelationItem>());
                f2fRelations.Add(faction1, new Dictionary<Faction.FactionID, float>());
                foreach (Faction.FactionID faction2 in Enum.GetValues(typeof(Faction.FactionID)))
                {
                    if (faction1 != faction2) f2fRelations[faction1].Add(faction2, 0f);
                }
            }
            // set pre-relations
            setFactionToFactionRelations(Faction.FactionID.noble1)
        }

        public string getFactionLeader(Faction.FactionID faction)
        {
            if (factionRuler.ContainsKey(faction) == false)
                factionRuler.Add(faction, NameGenerator.getName(faction));
            return factionRuler[faction];
        }
        public void newRuler(Faction.FactionID faction, string newRulerName = null)
        {
            if (newRulerName == null)
                newRulerName = NameGenerator.getName(faction);

            if (factionRuler.ContainsKey(faction) == false)
                factionRuler.Add(faction, newRulerName);
            else factionRuler[faction] = newRulerName;

            Debug.Log("TODO: update faction hq location ruler");
        }

        public float getFactionToFactionRelations(Faction.FactionID factionsOpinion, Faction.FactionID toFaction)
        {
            if (f2fRelations.ContainsKey(factionsOpinion) && f2fRelations[factionsOpinion].ContainsKey(toFaction))
                return f2fRelations[factionsOpinion][toFaction];
            else Debug.LogError("ERROR: couldn't find faction2faction relations for\n factionsOpinion: " + factionsOpinion + "\ntoFaction: " + toFaction + "");
            return 0;
        }
        public void addFactionToFactionRelations(Faction.FactionID factionsOpinion, Faction.FactionID toFaction, float value)
        {
            if (f2fRelations.ContainsKey(factionsOpinion) && f2fRelations[factionsOpinion].ContainsKey(toFaction))
                f2fRelations[factionsOpinion][toFaction] += value;
            else Debug.LogError("ERROR: couldn't find faction2faction relations for\n factionsOpinion: " + factionsOpinion + "\ntoFaction: " + toFaction + "");
        }
        public void setFactionToFactionRelations(Faction.FactionID factionsOpinion, Faction.FactionID toFaction, float value)
        {
            if (f2fRelations.ContainsKey(factionsOpinion) && f2fRelations[factionsOpinion].ContainsKey(toFaction))
                f2fRelations[factionsOpinion][toFaction] = value;
            else Debug.LogError("ERROR: couldn't find faction2faction relations for\n factionsOpinion: " + factionsOpinion + "\ntoFaction: " + toFaction + "");
        }

        public void addFactionRelationItem(Faction.FactionID faction, float itemLifetime, string description, float? staticModifier = null, float? dailyRelationsModifier = null, float? dailyRelationsModifierLowerLimit = null, float? dailyRelationsModifierUpperLimit = null)
        {
            foreach (FactionRelationItem item in factionRelationItems[faction])
            {
                if (description == item.description) item.addToExistingItem(itemLifetime, description, staticModifier, dailyRelationsModifier, dailyRelationsModifierLowerLimit, dailyRelationsModifierUpperLimit);
            }
            factionRelationItems[faction].Add(new FactionRelationItem(itemLifetime, description, staticModifier, dailyRelationsModifier, dailyRelationsModifierLowerLimit, dailyRelationsModifierUpperLimit));
        }
    }

    // SAVE data
    public class FactionRelationItem
    {
        public float? dailyRelationsModifier { get; private set; }
        public float? dailyRelationsModifierLowerLimit { get; private set; }
        public float? dailyRelationsModifierUpperLimit { get; private set; }

        public float? staticModifier { get; private set; }

        public float endDate { get; private set; }

        public string description { get; private set; }

        public FactionRelationItem(float itemLifetime, string description, float? staticModifier = null, float? dailyRelationsModifier = null, float? dailyRelationsModifierLowerLimit = null, float? dailyRelationsModifierUpperLimit = null)
        {
            this.endDate = Root.game.player.getElapsedDays() + itemLifetime;
            this.description = description;
            if (staticModifier != null) this.staticModifier = staticModifier;
            if (staticModifier != null) this.dailyRelationsModifier = dailyRelationsModifier;
            if (staticModifier != null) this.dailyRelationsModifierLowerLimit = dailyRelationsModifierLowerLimit;
            if (staticModifier != null) this.dailyRelationsModifierUpperLimit = dailyRelationsModifierUpperLimit;
        }

        public void addToExistingItem(float itemLifetime, string description, float? staticModifier = null, float? dailyRelationsModifier = null, float? dailyRelationsModifierLowerLimit = null, float? dailyRelationsModifierUpperLimit = null)
        {
            endDate += itemLifetime;
            if (staticModifier != null && staticModifier > this.staticModifier) this.staticModifier = staticModifier;
            if (dailyRelationsModifier != null && dailyRelationsModifier > this.dailyRelationsModifier) this.dailyRelationsModifier = dailyRelationsModifier;
            if (dailyRelationsModifierLowerLimit != null && dailyRelationsModifierLowerLimit > this.dailyRelationsModifierLowerLimit) this.dailyRelationsModifierLowerLimit = dailyRelationsModifierLowerLimit;
            if (dailyRelationsModifierUpperLimit != null && dailyRelationsModifierUpperLimit > this.dailyRelationsModifierUpperLimit) this.dailyRelationsModifierUpperLimit = dailyRelationsModifierUpperLimit;
        }

        public bool isOutdated()
        {
            return this.endDate < Root.game.player.getElapsedDays() ? true : false;
        }
    }
}