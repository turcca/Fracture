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

        Dictionary<Faction.FactionID, Dictionary<Faction.FactionID, FactionRelation>> f2fRelations = new Dictionary<Faction.FactionID, Dictionary<Faction.FactionID, FactionRelation>>();
    
        // SAVE data ^

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

            // initialize f2fRelations
            foreach (Faction.FactionID faction1 in Enum.GetValues(typeof(Faction.FactionID)))
            {
                f2fRelations.Add(faction1, new Dictionary<Faction.FactionID, FactionRelation>());
                foreach (Faction.FactionID faction2 in Enum.GetValues(typeof(Faction.FactionID)))
                {
                    if (faction1 != faction2)
                    {
                        f2fRelations[faction1].Add(faction2, new FactionRelation());
                    }
                }
            }
            // set pre-relations
            // Furia
            addFactionRelationItem(Faction.FactionID.noble1, Faction.FactionID.noble3, Mathf.Infinity, "Tarquinia was the noble house that defeated Dirae in the Great Crusade over two hundred years ago. Never forget!", -15);
            addFactionRelationItem(Faction.FactionID.noble1, Faction.FactionID.noble4, 500, "Ia. 386 There was a resolution to reduce political power of the High Exarch, and House Valeria was a co-sponsor to the bill.", +5);
            addFactionRelationItem(Faction.FactionID.noble1, Faction.FactionID.guild1, 500, "Ia. 386 There was a resolution to reduce political power of the High Exarch, and the Everlasting Union was a co-sponsor to the bill.", +4);
            addFactionRelationItem(Faction.FactionID.noble1, Faction.FactionID.church, Mathf.Infinity, "Church is in integral part of the Imperium that machinated the Great Crusade defeating Dirae over two hundred years ago. Never forget!", -15);
            // Rathmund
            addFactionRelationItem(Faction.FactionID.noble2, Faction.FactionID.guild2, 900, "Ia. 319 houses Rathmund and Valeria waged a trade war against the emerging Dacei family.", -3);
            addFactionRelationItem(Faction.FactionID.noble2, Faction.FactionID.noble4, 400, "Ia. 393 House Valeria pushed for reforms to the Sector Charter that limit Inquisitorial access to the noble houses.", +3);
            // Tarquinia
            addFactionRelationItem(Faction.FactionID.noble3, Faction.FactionID.noble1, Mathf.Infinity, "House Furia are descendants of rebels and separatists that lost against the Imperium in the Great Crusade. They can never truly be trusted.", -5);
            addFactionRelationItem(Faction.FactionID.noble3, Faction.FactionID.noble1, 500, "Ia. 386 There was a resolution to reduce political power of the High Exarch, and House Furia was a co-sponsor to the bill.", -3);
            addFactionRelationItem(Faction.FactionID.noble3, Faction.FactionID.noble4, 500, "Ia. 386 There was a resolution to reduce political power of the High Exarch, and House Valeria was a co-sponsor to the bill.", -3);
            addFactionRelationItem(Faction.FactionID.noble3, Faction.FactionID.noble4, 400, "Ia. 393  House Valeria pushed for reforms to the Sector Charter constricting the central governmnets rule.", -3);
            addFactionRelationItem(Faction.FactionID.noble3, Faction.FactionID.guild1, 800, "Ia. 320 Autonomous Union cells perform a campaign of bomb attacks on Tarquinia worlds. There are still radical elements within the trade guild.", -8);
            addFactionRelationItem(Faction.FactionID.noble3, Faction.FactionID.guild1, 500, "Ia. 386 there was a resolution to reduce political power of the High Exarch, and the Everlasting Union was a co-sponsor to the bill.", -3);
            addFactionRelationItem(Faction.FactionID.noble3, Faction.FactionID.church, Mathf.Infinity, "Church is in integral part of the Imperium and an old ally to the Bleak Order since long before the birth of the Imperium.", +20);
            addFactionRelationItem(Faction.FactionID.noble3, Faction.FactionID.heretic, Mathf.Infinity, "There are heretics and deviants on the sector, and it is the sworn duty of House Tarquinia to get rid of them!", -50);
            // Valeria
            addFactionRelationItem(Faction.FactionID.noble4, Faction.FactionID.noble1, 500, "Ia. 386 There was a resolution to reduce political power of the High Exarch, and House Furia was a co-sponsor to the bill.", +5);
            addFactionRelationItem(Faction.FactionID.noble4, Faction.FactionID.guild1, 500, "Ia. 386 There was a resolution to reduce political power of the High Exarch, and the Everlasting Union was a co-sponsor to the bill.", +4);
            addFactionRelationItem(Faction.FactionID.noble4, Faction.FactionID.noble2, 600, "Ia. 319 Houses Rathmund and Valeria waged a trade war against the emerging Dacei family.", +3);
            addFactionRelationItem(Faction.FactionID.noble4, Faction.FactionID.guild2, 900, "Ia. 319 Houses Rathmund and Valeria waged a trade war against the emerging Dacei family.", -3);
            addFactionRelationItem(Faction.FactionID.noble4, Faction.FactionID.guild2, Mathf.Infinity, "Ia. 306 Corrupted Dacei family was cast out from house Valeria. Dacei are old enemies of house Valeria, and there is lot of bad blood between the houses.", -20);
            // Everlastung Union
            addFactionRelationItem(Faction.FactionID.guild1, Faction.FactionID.noble1, 500, "Ia. 386 There was a resolution to reduce political power of the High Exarch, and House Furia was a co-sponsor to the bill.", +5);
            addFactionRelationItem(Faction.FactionID.guild1, Faction.FactionID.noble1, Mathf.Infinity, "House Furia are slave masters and old oppressors befor the Great Crusade, never forget!", -10);
            addFactionRelationItem(Faction.FactionID.guild1, Faction.FactionID.noble3, Mathf.Infinity, "The Union is born out of a long, hard struggle against oppressors. House Tarquinia are the Imperial enforcers and must be forever resisted!", -10);
            addFactionRelationItem(Faction.FactionID.guild1, Faction.FactionID.noble4, 500, "Ia. 386 There was a resolution to reduce political power of the High Exarch, and House Valeria was a co-sponsor to the bill.", +5);
            addFactionRelationItem(Faction.FactionID.guild1, Faction.FactionID.noble4, 400, "Ia. 393 House Valeria pushed for reforms to the Sector Charter beneficial to the trade guilds.", +3);
            // Dacei Family
            addFactionRelationItem(Faction.FactionID.guild2, Faction.FactionID.noble4, Mathf.Infinity, "Ia. 306 Dacei family was cast out from house Valeria. After that, Dacei are sworn enemies of house Valeria, and there is lot of bad blood between the houses.", -20);
            addFactionRelationItem(Faction.FactionID.guild2, Faction.FactionID.noble4, 400, "Ia. 393 House Valeria pushed for reforms to the Sector Charter beneficial to the trade guilds.", +3);
            addFactionRelationItem(Faction.FactionID.guild2, Faction.FactionID.noble4, 900, "Ia. 319 Houses Rathmund and Valeria waged an unjust trade war against the emerging Dacei family.", +6);
            addFactionRelationItem(Faction.FactionID.guild2, Faction.FactionID.noble2, 900, "Ia. 319 Houses Rathmund and Valeria waged an unjust trade war against the emerging Dacei family.", -6);
            // Coruna Cartel
            addFactionRelationItem(Faction.FactionID.guild3, Faction.FactionID.noble4, 300, "Ia. 393 House Valeria pushed for reforms to the Sector Charter that was lobbied to be beneficial to the the Cartel.", +2);
            // Chruch
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.noble1, 500, "Ia. 350 House Furia blocks a Church investigation on several colonies in the Martyr's Expanse.", -4);
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.noble1, 900, "Since general Furia died Ia. 306, there has been growing dissatisfaction towards the Imperium, and the name \"Dirae\" is again heard in Martyr's Expanse.", -5);
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.noble2, 600, "House Rathmund are not well known. They seem to be doing the right things but the intel shows them pulling a lot of dubious strings behind the scenes.", -5);
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.noble3, Mathf.Infinity, "House Tarquinia are the declared protectors in the sector. Since long befor the birth of Imperium, the Bleak Order has ever been an ally to the Church.", +20);
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.heretic, Mathf.Infinity, "An old and dangerous legacy is hidden from the Eyes of Truth, but thir wickedness shall be revealed and amended!", -50);
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.noble4, 800, "Ia. 393 House Valeria pushed for reforms to the Sector Charter that limit Inquisitorial access to the noble houses.", -8);
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.guild1, 900, "Since its birth, the Everlasting Union has ever been opposed to the ideals of the Imperium. But these projects take time and eventually they too will be brought in the fold.", -8);
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.guild2, 500, "Trade guilds are young and opportunist organizations. House Dacei are no different, but in time they too will learn.", -3);
            addFactionRelationItem(Faction.FactionID.church, Faction.FactionID.guild3, 700, "Trade guilds are young and opportunist organizations. Coruna Cartel has their secrets, but in time they too will learn.", -4);
            // Heretics
            addFactionRelationItem(Faction.FactionID.heretic, Faction.FactionID.noble1, 300, "A noble house of the Imperium. They too hide a seed of freedom within their souls, but a proud legacy is holding them back from transcendance.", -15);
            addFactionRelationItem(Faction.FactionID.heretic, Faction.FactionID.noble2, 600, "A noble house of the Imperium. But a reasonable one, as we have seen in our dealings with them.", -5);
            addFactionRelationItem(Faction.FactionID.heretic, Faction.FactionID.noble3, Mathf.Infinity, "A noble house of the Imperium. Slaves to their arcane beliefs, they are beyond redemption.", -40);
            addFactionRelationItem(Faction.FactionID.heretic, Faction.FactionID.noble4, 600, "A noble house of the Imperium. But an interesting one, to be sure.", -5);
            addFactionRelationItem(Faction.FactionID.heretic, Faction.FactionID.guild1, Mathf.Infinity, "Our lost brothers and sisters. If they can get past their profane chains, they can aspire to the spiritual goals.", +10);
            addFactionRelationItem(Faction.FactionID.heretic, Faction.FactionID.guild2, 700, "Climbing all the ladders of the Imperium, these little rats are up to something naughty.", -10);
            addFactionRelationItem(Faction.FactionID.heretic, Faction.FactionID.guild3, 300, "Navigators by origin, they are not entirely inept in the matters of the Fracture and Ingenium. With their eyes open, they could be something to behold.", -5);
            addFactionRelationItem(Faction.FactionID.heretic, Faction.FactionID.church, Mathf.Infinity, "Here comes the Inquisition!", -50);

        }

        public void tick(float tick)
        {
            foreach (Dictionary<Faction.FactionID, FactionRelation> faction in f2fRelations.Values)
                foreach (FactionRelation relation in faction.Values)
                    relation.update(tick);
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
            if (factionsOpinion == toFaction) { Debug.LogError("Improper use of method: comparing the same faction [" + factionsOpinion + "]"); return 0; }

            if (f2fRelations.ContainsKey(factionsOpinion) && f2fRelations[factionsOpinion].ContainsKey(toFaction))
                return f2fRelations[factionsOpinion][toFaction].Value;
            else Debug.LogError("ERROR: couldn't find faction2faction relations for\n factionsOpinion: " + factionsOpinion + "\ntoFaction: " + toFaction + "");
            return 0;
        }

        public void addFactionRelationItem(Faction.FactionID factionsOpinion, Faction.FactionID toFaction, float itemLifetime, string description, float staticModifier = 0f, float dailyRelationsModifier = 0f, bool startInDecline = false)
        {
            if (f2fRelations.ContainsKey(factionsOpinion) && f2fRelations[factionsOpinion].ContainsKey(toFaction))
                f2fRelations[factionsOpinion][toFaction].addFactionRelationItem(itemLifetime, description, staticModifier, dailyRelationsModifier, startInDecline);
            else Debug.LogError("ERROR: couldn't find faction2faction relations for\n factionsOpinion: " + factionsOpinion + "\ntoFaction: " + toFaction + "");
        }

        public string getListOfRelationModifiersToAllFactions(Faction.FactionID faction)
        {
            string rs = "";
            foreach (FactionRelation relation in f2fRelations[faction].Values)
                rs += relation.ToDebugString();
            return rs;
        }
        //public string getListOfRelationModifiers(Faction.FactionID faction, Faction.FactionID toFaction)
        //{
        //    if (faction == toFaction) { Debug.LogError("Can't get list from faction to itself:" + faction); return ""; }
        //    if (f2fRelations.ContainsKey(faction) && f2fRelations[faction].ContainsKey(toFaction)) return f2fRelations[faction][toFaction].ToDebugString();
        //    else
        //    {
        //        Debug.LogError("no FactionRelations in f2fRelations for [" + faction + "][" + toFaction + "]");
        //        return "";
        //    }
        //}

        public string ToDebugString()
        {
            string rs = "FactionsData:\n";

            rs += "    Relations:\n";
            foreach (Faction.FactionID faction1 in Enum.GetValues(typeof(Faction.FactionID)))
                foreach (Faction.FactionID faction2 in Enum.GetValues(typeof(Faction.FactionID)))
                    if (faction1 != faction2)
                        rs += "[" + Faction.getFactionName(faction1) + "] relations to ["+ Faction.getFactionName(faction2) + "]: " + Mathf.Round(getFactionToFactionRelations(faction1, faction2)) +"\n" + f2fRelations[faction1][faction2].ToDebugString(true);

            return rs;
        }
    }




    /// <summary>
    /// each [faction]'s relation to the [faction]
    /// stored in FactionData.f2fRelations[][]
    /// </summary>
    public class FactionRelation
    {
        /// <summary>
        /// total faction relations to another faction in 'f2fRelations'
        /// </summary>
        public float Value
        {
            get
            {
                if (isUpdated == false) update();
                return value;
            }
            private set { this.value = Value; }
        }

        float value;
        bool isUpdated = false;
        List<FactionRelationItem> relationItems = new List<FactionRelationItem>();

        public void update(float tick = 0f)
        {
            // go through all relationItems, update/obsolete them //relationItems.RemoveAll(item => item.isOutdated())
            float totalValue = 0;
            if (tick != 0f)
            {
                for (int i = relationItems.Count - 1; i >= 0; i--)
                {
                    FactionRelationItem item = relationItems[i];
                    // remove obsoletes & update staticModifier
                    if (item.isOutdatedAfterUpdate(tick))
                        relationItems.RemoveAt(i);
                    else
                        totalValue += item.staticModifier;
                }
            }
            else
                foreach (FactionRelationItem item in relationItems) totalValue += item.staticModifier;
            this.value = totalValue;
            isUpdated = true;
        }


        public void addFactionRelationItem(float itemLifetime, string description, float staticModifier = 0f, float dailyRelationsModifier = 0f, bool startInDecline = false)
        {
            foreach (FactionRelationItem item in relationItems)
            {
                if (description == item.description) item.addToExistingItem(itemLifetime, description, staticModifier, dailyRelationsModifier);
            }
            relationItems.Add(new FactionRelationItem(itemLifetime, description, staticModifier, dailyRelationsModifier, startInDecline));
            isUpdated = false;
        }

        public string ToDebugString(bool showLifetime = false)
        {
            string rs = "";
            foreach (var item in relationItems)
            {
                rs += "    "+item.ToDebugString(showLifetime) + "\n";
            }
            return rs;
        }
    }


    public class FactionRelationItem
    {
        /// <summary>
        /// total modifier of this item
        /// </summary>
        public float staticModifier { get; private set; }

        /// <summary>
        /// shifts staticModifier until lifetime is over, then starts the opposite shift until ends
        /// </summary>
        public float dailyRelationsModifier { get; private set; }
        public bool dailyIncreasing { get; private set; }

        public float endDate { get; private set; }

        public string description { get; private set; }

        /// <summary>
        /// if using dailyRelationsModifier, peak effect is dailyRelationsModifier * itemLifetime + staticModifier, and declines out from there
        /// </summary>
        /// <param name="itemLifetime"></param>
        /// <param name="description"></param>
        /// <param name="staticModifier"></param>
        /// <param name="dailyRelationsModifier"></param>
        public FactionRelationItem(float itemLifetime, string description, float staticModifier = 0f, float dailyRelationsModifier = 0f, bool startInDecline = false)
        {
            this.endDate = Root.game.getElapsedDays() + itemLifetime;
            this.description = description;
            this.staticModifier = staticModifier;
            this.dailyRelationsModifier = dailyRelationsModifier;
            dailyIncreasing = (dailyRelationsModifier != 0f && startInDecline == false) ? true : false; //||
        }

        
        public bool isOutdatedAfterUpdate(float tick = 0f)
        {
            if (this.endDate < Root.game.getElapsedDays())
                if (dailyIncreasing && dailyRelationsModifier != 0f)
                {
                    // reached peak daily-building effect, setting to decline
                    dailyIncreasing = false;
                    dailyRelationsModifier = -dailyRelationsModifier; // opposite shift
                    endDate = Root.game.getElapsedDays() + (staticModifier / dailyRelationsModifier);
                    return false;
                }
                else return true;
            else
            {   // update
                if (tick > 0f && dailyRelationsModifier != 0f)
                    staticModifier += dailyRelationsModifier * tick;
                return false;
            }
        }


        public void addToExistingItem(float itemLifetime, string description, float staticModifier = 0f, float dailyRelationsModifier = 0f)
        {
            if (description != this.description)
            {
                Debug.LogError("addToExistingItem: description is supposed to be the same!\n   [original item] "+this.description+"\n   [new item    ] " +description);
                return;
            }
            if (dailyRelationsModifier != 0f)
            {
                // check for ongoing positive daily-building effect for added negative dailyRelationsModifier
                if (dailyRelationsModifier < 0f)
                {
                    if (dailyIncreasing && this.dailyRelationsModifier > 0f) // original effect still increasing
                    { Debug.LogError("original positive effect still increasing, but a negative dailyRelationsModifier is tried to add\n[description] " + description); return; }
                    else if (dailyIncreasing == false && this.dailyRelationsModifier < 0f) // original effect already in decline
                    { Debug.LogError("original positive effect is in decline, but a negative dailyRelationsModifier is tried to add\n[description] " + description); return; }
                }
                if (dailyRelationsModifier > 0f)
                {
                    if (dailyIncreasing && this.dailyRelationsModifier < 0f) // original effect still increasing
                    { Debug.LogError("original negative effect still getting stronger, but a positive dailyRelationsModifier is tried to add\n[description] " + description); return; }
                    else if (dailyIncreasing == false && this.dailyRelationsModifier < 0f) // original effect already in decline
                    { Debug.LogError("original negative effect is in diminishing, but a positive dailyRelationsModifier is tried to add\n[description] " + description); return; }
                }
                // amplify effect by shifting the static modifier
                this.staticModifier += (this.staticModifier + itemLifetime * dailyRelationsModifier);
            }
            else
            {
                if (dailyRelationsModifier == 0f) endDate += itemLifetime;
                this.staticModifier += staticModifier;
            }
        }

        public string ToDebugString(bool showLifetime = false)
        {
            string rs = "[";
            if (staticModifier > 0) rs += "+";
            rs += staticModifier+"] "+ description;
            if (showLifetime) rs += " (lifetime: " + (endDate - Root.game.getElapsedDays()) + ")";
            return rs;
        }
    }
}