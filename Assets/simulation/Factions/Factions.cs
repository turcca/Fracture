using UnityEngine;
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


        public Factions()
        {
            data = new FactionData();
        }

        public void tick(float tick)
        {

        }



        public string getRuler(Faction.FactionID faction)
        {
            return data.getFactionRuler(faction);
        }
    }
}