//using System;
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
        }

        public string getFactionRuler(Faction.FactionID faction)
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
    }
}