//#pragma strict
//
//import System.Collections.Generic;
//
//enum GameState {
//	none,
//	starChart,
//	combat
//}
//
//
//static class WorldState
//{
//	var gameState : GameState;
//
//	// Location
//	 	//private var location : locationDatabase;
//
//	// Player - LocationFinder (Collider / Finder)
//		//private var player : GameObject;
//		private var finder : LocationFinder;
//
//	// Node
//		//private var nodes : GameObject;
//		//private var weather : NodeMesh;
//
//	// ScriptHolder 
//	private var scriptHolder : GameObject;
//		//private var nameGenerator : NameGenerator;
//		//private var gameTime : GameTime;
//		private var collector : locationsDataCollector;
//		//private var ship : PlayerShipData;
//		//private var shipPop : ShipPopulation;
//		//private var economy : Economy;
//		//private var event : Events;
//		//private var eventManager : EventManager;
//		private var characters : Characters;
//
//
//
//	// variables -------------------->
//	// **************************************
//
//	// Game settings
//		
//		public var settings = new settingsclass();
//		
//		
//	// Player
//
//		public var player = new playerClass();
//
//
//	// Agenda -summary of sector population
//		public var agendas 	: Dictionary.<String, generalagendaclass> = new Dictionary.<String, generalagendaclass>();	// 
//
//	// Factions & Ideologies
//
//		// *******************************
//		public var faction 	: Dictionary.<String, factionstateclass> = new Dictionary.<String, factionstateclass>();	// faction["nobleHouse1"].relationsTo["church"].total  /// faction["nobleHouse1"].factionStat["innovation"]
//		public var ideology 	: Dictionary.<String, ideologystateclass> = new Dictionary.<String, ideologystateclass>();	// ideology["cult"].ideologyStat["innovation"] ///	ideology["cult"].relationsMultiplierTo["technocrat"]
//		public var locationRelations : Dictionary.<locationDatabase, Dictionary.<locationDatabase, relationsclass> > = new Dictionary.<locationDatabase, Dictionary.<locationDatabase, relationsclass> >(); // locationRelations[GameObject.Find("1I01")][GameObject.Find("1I02")].update();		// nested 2d dictionary
//		// *******************************
//		
//		public var factionNames 	: List.<String> = new List.<String>();
//		public var ideologyNames : List.<String> = new List.<String>();
//		public var ideologyStatsNames: List.<String> = new List.<String>();
//		
//		public var agendaNames : List.<String> = new List.<String>();
//		
//		public var assignmentNames : List.<String> = new List.<String>();
//		public var skillNames : List.<String> = new List.<String>();
//		public var departmentNames : List.<String> = new List.<String>();
//		
//		public var diplomacyValues : List.<float[]> = new List.<float[]>();
//		
//		public var hardPointList : List.<hardPoints> = new List.<hardPoints>();
//		public var shipHardPointNames : List.<String> = new List.<String>();
//
//		
//	// **************************************
//
//	// CLASSES 	(classes may have defaults)	
//		
//		
//		// SETTINGS class (game settings)
//		
//		public class settingsclass {
//			var difficulty : int = 2;			// 2 easy (no permadeath), 3 normal
//			var permadeath : boolean = true;
//			
//		}
//		
//		
//		// PLAYER class
//		
//		public class playerClass {
//
//			var ideologyOriginal : String = null;	// player ideology at creation
//			var factionOriginal : String = null;	// player commitment to faction at creation
//			
//			var ideology : Dictionary.<String, float> = new Dictionary.<String, float>();	// player ideology based on choices
//			var faction : Dictionary.<String, float> = new Dictionary.<String, float>();	// player commitment to factions
//				
//				
//				// functions
//				var setIdeology = function (ideology : String, amount : float) {
//					if (this.ideology.ContainsKey(ideology) ) { 
//						this.ideology[ideology] += amount;
//						var total: float;
//						for (var a : float in this.ideology.Values) { total += a; }
//						if (total > 0) for (var a : float in this.ideology.Values) { a /= total; }
//					}
//					else Debug.LogError("ERROR: invalide key for setIdeology: "+ideology);
//				};
//				var setFaction = function (faction : String, amount : float) {
//					if (this.faction.ContainsKey(faction) ) { 
//						this.faction[faction] += amount;
//							if (this.faction[faction] < 0) this.faction[faction] = 0;
//							else if (this.faction[faction] > 1) this.faction[faction] = 1;
//					}
//					else Debug.LogError("ERROR: invalide key for setFaction: "+faction);
//				};
//
//			// funds
//			var funds : float = 100;
//
//			// Tracking
//			var herecy : float = 0;
//			
//			
//				
//			// World State Conditions for the player
//			var seenMutants : int = 0;
//			
//		}
//		
//		
//
//		// FACTION class	
//		public class factionstateclass {
//			
//			// agenda
//			var generalagendaclass 	: Dictionary.<String, float> = new Dictionary.<String, float>();	// agenda["idealist"] = 1;
//			var agenda : Dictionary.<String, agendaclass> = new Dictionary.<String, agendaclass>();
//			
//			// relations 	(dictionary of factions - self, + player --> relationsclass)
//			var relationsTo : Dictionary.<String, relationsclass> = new Dictionary.<String, relationsclass>();
//			
//			// other states
//			
//			
//			// faction stats
//			var ideologies : Dictionary.<String, float> = new Dictionary.<String, float>();		// cult, technocrats...
//			var factionStat : Dictionary.<String, float> = new Dictionary.<String, float>();	// pgrowth, innovation...
//			// names
//			var names : String[];
//			
//			
//			// agenda methods
//			var setAgenda = function (agenda : String, amount : float) {
//				if (this.agenda[agenda].value <= -amount) {	this.agenda[agenda].value = 0; return; }	// amount is negative (since values are at least 0) and would bring value to 0 or negative - no need to calculate more
//				else { this.agenda[agenda].value += amount; }			
//				// normalize if amount was positive
//				if (amount > 0) {
//					var a : String;
//					var total : float;
//					for (a in this.agenda.Keys) {
//						total += this.agenda[a].value;
//					}
//					if (total > 1) {
//						for (a in this.agenda.Keys) {
//							this.agenda[a].value /= total;
//						}
//					}
//				}
//			};
//			
//			var getAgenda = function (agenda : String) : KeyValuePair.<String, float> {
//				var dominantAgenda : String = null;
//				var agendaValue : float = 0;
//				var total : float;
//				// pick dominantAgenda
//				for (var a : String in this.agenda.Keys) {
//					if (this.agenda[a].value > agendaValue) { dominantAgenda = a; agendaValue = this.agenda[a].value; }
//					total += this.agenda[a].value;
//				}
//				// normalize
//				if (total > 1) {
//					for (var a : String in this.agenda.Keys) {
//						this.agenda[a].value /= total;
//					}
//					agendaValue /= total;
//				}
//				// return
//				return new KeyValuePair.<String, float>(dominantAgenda, agendaValue);
//			};
//		}
//		
//		// IDEOLOGY class	
//		class ideologystateclass {
//			
//			// agenda
//			//var agenda : String;
//			var agenda : Dictionary.<String, agendaclass> = new Dictionary.<String, agendaclass>();
//			
//			// relations 	(dictionary of ideological multipliers (when two ideologies meet))
//			//var relationsMultiplierTo : Dictionary.<String, float> = new Dictionary.<String, float>();
//			var relationsTo : Dictionary.<String, relationsclass> = new Dictionary.<String, relationsclass>(); // contains only "player" !!!
//			
//			// other states
//			
//			
//			// ideology stats
//			var ideologyStat : Dictionary.<String, float> = new Dictionary.<String, float>();	// ideology["cult"].ideologyStat["innovation"]	contains cult's innovation value (-1)
//			// names
//			var names : String[];
//		}
//		
//		// LOCATION relations (uses relationsClass straight in a nested 2d dictionary setup)
//			// just relationsclass
//		
//		
//		
//		
//		
//		// Relations class	
//		public class relationsclass {
//					
//			var state : float = 0; 		// -100 - 100 		official state --- war: -50 / friends +50 / neutral
//			
//			var total : float;			// -100 - 100		relations (good, bad)
//			
//			var reparations : float = 0; 	// 0 --
//			var historical : float = 0;		// -100 - 100
//			var recent : float = 0;			// -100 - 100
//			var ideological : float = 1;	// 0 - 2
//			var economical : float = 0;		// 0 - 100
//			
//			var ideologicalCalculated : boolean = false;
//			var lastUpdateDate : float;
//			
//			var visible : boolean = true;
//			
//			var calculateTotal = function() : float {
//				
//				this.update();
//				
//				var h : float = this.historical 	/100 +1;	// 0-2		/ 1	
//				var i : float = this.ideological;				// 0-2		/ 1
//				var r : float = this.recent 		/50 -1;		// -1 - 1	/ 0+	
//				var e : float = this.economical 	/100;		// 0-1		/ 0+
//				var s : float = Mathf.Pow((this.state/400+1),2);// ~0.6 - ~1.6 / 1
//				
//				// **************************************************************************************************************************
//				this.total = (Mathf.Max(0, Mathf.Min(2, 						// clamp at 0/2 (-100 / +100)	// CORE OF RELATIONS CALCULATIONS !!!
//									-this.reparations/100 +	// -reparations/100
//									r +				// recent history is remembered as it was, and it's fresh in the memory
//									h * i +			// ideology modifies historic relations, in what light people remember events
//									e * i +			// ideology modifies economic relations, in what light people remember and interpret trade (mutual benefical, or exploiting/rivarly?)
//									s				// shift everything by the formal 'state' relations (if any)	// -0.6 - 0.6	// backbone of 1
//									))-1 ) *100;				// convert back to -1 - 1,  -100 - 100
//				// **************************************************************************************************************************
//				return this.total;
//			};
//			var update = function() {
//				var d : float = (GameTime.gameDay - this.lastUpdateDate);
//				
//				if (d > 0) {
//					
//					// mellow out recent
//					if (this.recent != 0) {
//						var recentToZero : boolean = true;
//						// *0.1 = how many points is equalized from 'recent' per day?
//						var rd : float = d *0.1;		
//						
//						if (this.recent >= rd) { this.recent -= rd; recentToZero = false; }
//						else if (this.recent <= -rd) { this.recent += rd; recentToZero = false; }
//						// this.recent != 0, but less than d's equalizing force ---> zero it out
//						if (recentToZero) this.recent = 0;
//					}
//					
//					// reduce economical cooperation level
//					if (this.economical > 0) {
//						
//						// *0.5 = how many points is dropped from 'economical' per day? 0.01 = average 0.5 points (economy 50)
//						var ed : float = Mathf.Floor(d);
//						
//						// every day reduces economy by 1% of economy
//						for (var i : int = 0; i < ed; i++) {
//							this.economical -= this.economical*0.01;
//						}
//						this.economical -= this.economical*0.01 * (d-ed);	// handle fraction
//					}
//					else this.economical = 0;
//					
//					// Update ideological -- needs to be called by function updateLocationRelationsIdeological() or updateFactionRelationsIdeological()
//									
//					// set last update date
//					this.lastUpdateDate = GameTime.gameDay;
//				}
//			};
//			
//			var setHistorical = function (amount : float) { 
//				this.historical += amount;
//				if (this.historical > 100) { this.historical = 100; }
//				else if (this.historical < -100) { this.historical = -100; }
//				this.setRecent(amount);
//			};
//			/*
//			var setIdeological = function (amount : float) { 
//				this.ideological += amount;
//				if (this.ideological > 100) { this.ideological = 100; }
//				else if (this.ideological < -100) { this.ideological = -100; }
//				this.setRecent(amount);
//			};
//			*/
//			var setEconomical = function (amount : float) { 
//				var e : float = 1 - (this.economical/200 +0.5); 	// 1-0 	( -100 = 1, +100 = 0 )
//				this.economical += amount * e;
//
//				this.setRecent(amount*0.1);	// economic cooperation increases a bit of recent as well
//			};
//			
//			var setRecent = function (amount : float) { 
//				this.recent += amount;
//				if (this.recent > 100) { this.recent = 100; }
//				else if (this.recent < -100) { this.recent = -100; }
//			};
//			
//			//var relationsclass = function() {		}		// need constructor class?
//		}
//		
//		public class agendaclass {
//			var value : float = 0;
//			var weight : float = 0;
//			
//			function agendaclass(w : float) {	// class constructor
//				this.weight = w;
//			}
//		}
//		
//		public class generalagendaclass {
//			var sectorShare : float = 0;
//			
//		}
//
//	// -------------------- 
//
//		public var numberOfCoreWorlds : int = 1; 	// importanceFactor >= 19 	// add core world calculator?	// add check on ruler change, and core world status elevations/demotions
//		public var sectorRulers : Dictionary.<GameObject, String> = new Dictionary.<GameObject, String>();
//
//
//	// --------------------COMBAT------------------------------------------------------------------------------
//
//		var combatRadiationMultiplier : float = 0.5;
//		var combatReflectMultiplier 	: float = 8;
//
//		var combatBaseHPValue : float = 100;
//
//		////var reflectMultiplier 	: float = 8;
//		////var radiationMultiplier : float = 0.5;
//
//		//var accelerationDivisor		: float = 3;
//		//var inertiaCapMultiplier 	: float = 2.4;
//
//		////var baseHPValue : float = 100;
//
//		//var weaponExplosionSizeDivider	: float = 10;
//		//var otherExplosionSizeDivider	: float = 20;
//
//		//var collisionElasticity	: float = 1.0;
//
//		var combatTicksInSecond : float = 60;
//
//		var weaponDictionary : Dictionary.<String, WeaponClass> = new Dictionary.<String, WeaponClass>(); // Combat tmp (siirtyy-> WorldState)
//		var sensorDictionary : Dictionary.<String, SensorClass> = new Dictionary.<String, SensorClass>(); // Combat tmp (siirtyy-> WorldState)
//		var objectsToScripts : Dictionary.<GameObject,ModuleDataClass> = new Dictionary.<GameObject,ModuleDataClass>();  // getcomponent pool ?
//		
//		var moduleClasses : Dictionary.<ModuleClass, List.<ModuleTypes> > = new Dictionary.<ModuleClass, List.<ModuleTypes> >();
//
//	function getModuleTypesList (moduleClass : ModuleClass) : List.<ModuleTypes>
//	{
//		if (moduleClasses.ContainsKey(moduleClass))
//		{
//			var list : List.<ModuleTypes> = new List.<ModuleTypes>();
//			for (var t : ModuleTypes in moduleClasses[moduleClass])
//			{
//				list.Add(t);
//			}
//			return list;
//		}
//		Debug.LogError("ERROR: no moduleClass: '"+moduleClass.ToString()+"' in moduleClasses");
//	}
//
//
//	// ****************************************************************************************
//
//	function initialize () {
//
//		// LINKS
//			//player = GameObject.FindGameObjectWithTag("Player");
//			//finder = 	player.gameObject.GetComponent(LocationFinder);
//		
//		//nodes = GameObject.Find("nodes");
//			//weather 	= nodes.gameObject.GetComponent(NodeMesh);
//		
//		scriptHolder = GameObject.Find("ScriptHolder");
//		if (scriptHolder != null)
//		{
//			//nameGenerator 	= scriptHolder.gameObject.GetComponent(NameGenerator);
//			//gameTime 		= scriptHolder.gameObject.GetComponent(GameTime);
//			collector 		= scriptHolder.gameObject.GetComponent(locationsDataCollector);
//			//ship 			= scriptHolder.gameObject.GetComponent(PlayerShipData);
//			//shipPop 		= scriptHolder.gameObject.GetComponent(ShipPopulation);
//			//economy 		= scriptHolder.gameObject.GetComponent(Economy);
//			//event 			= scriptHolder.gameObject.GetComponent(Events);
//			//eventManager	= scriptHolder.gameObject.GetComponent(EventManager);
//			characters		= scriptHolder.gameObject.GetComponent(Characters);
//		}
//		
//		
//		
//		// POPULATE lists and dictionaries
//			var a : String; // string temp for "foreach"
//			var i : int;
//			var j : int;
//			// name lists
//			ideologyNames.Add("cult"); ideologyNames.Add("technocrat"); ideologyNames.Add("mercantile"); ideologyNames.Add("bureaucracy"); ideologyNames.Add("liberal"); ideologyNames.Add("nationalist"); ideologyNames.Add("aristocrat"); ideologyNames.Add("imperialist"); ideologyNames.Add("navigators"); ideologyNames.Add("brotherhood"); ideologyNames.Add("transhumanist");
//			factionNames.Add("nobleHouse1"); factionNames.Add("nobleHouse2"); factionNames.Add("nobleHouse3"); factionNames.Add("nobleHouse4"); factionNames.Add("guild1"); factionNames.Add("guild2"); factionNames.Add("guild3"); factionNames.Add("church"); factionNames.Add("heretic");
//			
//			ideologyStatsNames.Add("pgrowth"); ideologyStatsNames.Add("industry"); ideologyStatsNames.Add("economy"); ideologyStatsNames.Add("diplomacy"); ideologyStatsNames.Add("happiness"); ideologyStatsNames.Add("affluence"); ideologyStatsNames.Add("innovation"); ideologyStatsNames.Add("morale"); ideologyStatsNames.Add("altruism"); ideologyStatsNames.Add("military"); ideologyStatsNames.Add("holy"); ideologyStatsNames.Add("psych"); ideologyStatsNames.Add("navigation"); ideologyStatsNames.Add("purity"); ideologyStatsNames.Add("police"); ideologyStatsNames.Add("violent"); ideologyStatsNames.Add("aristocracy"); ideologyStatsNames.Add("imperialism");
//			
//			assignmentNames.Add("captain"); assignmentNames.Add("navigator"); assignmentNames.Add("engineer"); assignmentNames.Add("security"); assignmentNames.Add("quartermaster"); assignmentNames.Add("psycher"); assignmentNames.Add("priest");
//			departmentNames.Add("command"); departmentNames.Add("navigation"); departmentNames.Add("military"); departmentNames.Add("security"); departmentNames.Add("engineering"); departmentNames.Add("quarters");
//			skillNames.Add("leadership"); /*skillNames.Add("emissar");*/ skillNames.Add("hr"); skillNames.Add("engineering"); skillNames.Add("precognition"); skillNames.Add("psy"); skillNames.Add("navigation"); skillNames.Add("spaceBattle"); skillNames.Add("combat"); skillNames.Add("trading"); skillNames.Add("diplomat"); skillNames.Add("scientist"); skillNames.Add("integrity"); skillNames.Add("holiness"); skillNames.Add("purity"); skillNames.Add("security"); skillNames.Add("violent"); skillNames.Add("aristocrat"); skillNames.Add("imperialist"); skillNames.Add("corruption");
//			
//			// ship component names
//				hardPointList.Add(hardPoints.front); hardPointList.Add(hardPoints.dorsal); hardPointList.Add(hardPoints.sideL); hardPointList.Add(hardPoints.sideR); hardPointList.Add(hardPoints.extension1); hardPointList.Add(hardPoints.extension2); hardPointList.Add(hardPoints.pd); 
//				shipHardPointNames.Add("front"); shipHardPointNames.Add("dorsal"); shipHardPointNames.Add("sideL"); shipHardPointNames.Add("sideR"); shipHardPointNames.Add("extension1"); shipHardPointNames.Add("extension2"); shipHardPointNames.Add("pd");
//				// get weapons
//				//weaponDictionary = WeaponData.getWeapons(); // COMBAT! TMP
//				//sensorDictionary = SensorData.getSensors(); // COMBAT! TMP
//				
//			// agenda names
//				// imperial
//				agendaNames.Add("loyalist"); agendaNames.Add("idealist"); agendaNames.Add("faithful"); 
//				// reformist
//				agendaNames.Add("reformist"); agendaNames.Add("transhumanist"); 
//				// separatist
//				agendaNames.Add("separatist"); agendaNames.Add("nationalist"); agendaNames.Add("heretic"); 
//				// individualist (non-committed)
//				agendaNames.Add("individualist"); 
//			
//			// player ideology & faction
//			
//				// populate dictionaries
//					// ideology values
//					for (a in ideologyNames) player.ideology.Add(a, 0);
//					// faction values
//					for (a in factionNames) player.faction.Add(a, 0);
//			
//			
//			
//			// faction state + ideology state + location state SETUP
//					//	ideology and faction names	/name0 				"a known *****"1  		idealism2		Government3		Governm-Str4	Council5		Governor6		Governor(Strong)7	Governor(Core)8				ID	9
//					var nobleHouse1: String[]	= ["House Furia",		"aristocrat",			"Aristocracy",	"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				"nobleHouse1"];
//					var nobleHouse2: String[]	= ["House Rathmund",	"aristocrat",			"Aristocracy",	"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				"nobleHouse2"];
//					var nobleHouse3: String[]	= ["House Tarquinia",	"imperialist",			"Imperial",		"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				"nobleHouse3"];
//					var nobleHouse4: String[]	= ["House Valeria",		"aristocrat",			"Aristocracy",	"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				"nobleHouse4"];
//					
//					var guild1: String[] 		= ["Everlasting Union",	"guilder",				"Society",		"Civilian",		"Civilian",		"Councillor",	"Governor",		"President",		"Primus Transcended",		"guild1"];
//					var guild2: String[] 		= ["Dacei Family",		"guilder",				"Dynasty",		"Civilian",		"Civilian",		"Councillor",	"Governor",		"Exarch",			"High Exarch",				"guild2"];
//					var guild3: String[] 		= ["Coruna Cartel",		"guilder",				"Cartel",		"Civilian",		"Civilian",		"Councillor",	"Governor",		"Exarch",			"High Exarch",				"guild3"];
//					
//					var church: String[] 		= ["Church", 			"cleric", 				"Theocracy", 	"Theocratic",	"Church",		"Bishop",		"Bishop",		"Arch Bishop",		"Cardinal",					"church"];
//					var heretic: String[] 		= ["Radical Movement",	"radical", 				"Theocracy", 	"Order",		"Cult",			"Councillor",	"Protector",	"Messiah",			"Antipope",					"heretic"]; // apostacy apostate
//					
//					var cult : String[] 		= ["Faithful", 			"devout", 				"Theocracy", 	"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				"cult"];
//					var technocrat : String[] 	= ["Technocrats",		"technocrat",			"Technocracy",	"Civilian",		"Civilian",		"Councillor",	"Governor",		"Exarch",			"High Exarch",				"technocrat"];
//					var mercantile : String[] 	= ["Merchants",			"plutocrat",			"Plutocracy",	"Civilian",		"Civilian",		"Councillor",	"Governor",		"Exarch",			"High Exarch",				"mercantile"];
//					var bureaucracy : String[] 	= ["Bureaucrats",		"bureaucrat",			"Bureaucracy",	"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				"bureaucracy"];
//					var liberal : String[] 		= ["Democratic Party",	"democrat",				"Democracy",	"Civilian",		"Civilian",		"Minister",		"Prime Minister","President",		"Sector President",			"liberal"];
//					var nationalist : String[] 	= ["Nationalists",		"separatist",			"Sovereignty",	"Separatist",	"Separatist",	"Councillor",	"Commissar",	"People's Commissar","High Commander",			"nationalist"];
//					var aristocrat : String[] 	= ["Aristocrats",		"aristocrat",			"Aristocracy",	"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				"aristocrat"];
//					var imperialist : String[] 	= ["Imperialists",		"imperialist",			"Colony",		"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				"imperialist"];
//					var navigators : String[] 	= ["Navigator's Guild",	"Navigator",			"Navigators",	"Order",		"Order",		"Senior Advisor","Governor",	"Exarch",			"High Exarch",				"navigators"];
//					var brotherhood : String[] 	= ["Brotherhood",		"member of the Brotherhood","Brotherhood","Order",		"Order",		"Senior Advisor","Governor",	"Exarch",			"High Exarch",				"brotherhood"];
//					var transhumanist : String[]= ["Radical Movement",	"radical",				"Transcendency","Civilian",		"Anarchy",		"Minister",		"Prime Minister","Transcended",		"Primus Transcended",		"transhumanist"];
//					
//					// ideologyStats
//					var ideologyValues : Dictionary.<String, float[]> = new Dictionary.<String, float[]>();
//						ideologyValues.Add("cult", 				[ 1.0, 0.2,-1.0,-1.0,-0.3,-0.7,-1.0, 1.0, 0.8, 1.0, 	 1.0,-0.7,-0.4, 1.0, 	 1.0, 1.0,-0.3, 0.8 ]);
//						ideologyValues.Add("technocrat", 		[-0.6, 1.0, 0.3, 0.0, 0.2, 0.3, 0.7,-0.2, 0.2, 0.2, 	-0.2,-1.0, 0.7,-0.5, 	-0.2,-0.2, 0.1,-0.1 ]);
//						ideologyValues.Add("mercantile", 		[-1.0, 0.3, 1.0, 2.0, 0.7, 1.0, 0.2,-0.9,-1.0,-0.6, 	-0.4, 0.2, 1.0,-0.6, 	-0.6,-0.7, 1.0,-0.2 ]);
//						ideologyValues.Add("bureaucracy", 		[ 0.6, 0.8, 0.6, 0.1, 0.0, 0.1,-0.1,-0.2,-0.6, 0.1, 	-0.1,-0.2,-0.1, 0.1, 	 0.4, 0.0, 0.3, 0.05]);
//						ideologyValues.Add("liberal", 			[-1.0,-0.4,-0.3, 2.0, 1.0, 0.6, 1.0,-1.0, 1.0,-1.0, 	-0.6, 0.4, 0.2,-0.6, 	-1.0,-1.0,-0.7,-0.5 ]);
//						ideologyValues.Add("nationalist", 		[ 0.8, 0.8,-0.1,-1.0, 0.3,-0.3,-0.2, 0.8, 0.2, 1.0, 	 0.0,-0.5,-1.0, 0.4, 	 0.7, 1.0,-0.3,-1.0 ]);
//						ideologyValues.Add("aristocrat", 		[ 0.7, 0.2, 0.4,-0.2,-0.5, 0.3,-0.3, 0.5,-0.5, 0.8, 	-0.3, 0.3, 0.1,-0.3, 	 0.8, 0.6, 1.0, 0.5 ]);
//						ideologyValues.Add("imperialist", 		[ 0.2,-0.1,-0.1, 0.2,-0.2, 0.0,-0.2, 0.3,-0.2, 0.4, 	 0.6, 0.2, 0.3, 0.2, 	 0.3, 0.3, 0.5, 1.0 ]);
//						ideologyValues.Add("navigators", 		[-3.0,-0.5, 0.5, 1.0, 0.2, 0.6, 0.7,-0.2,-0.3, 0.2, 	 0.0, 1.0, 2.0,-1.0, 	-0.2,-0.5, 1.0, 0.1 ]);
//						ideologyValues.Add("brotherhood", 		[-0.7,-1.0, 0.0, 0.2, 0.1, 0.0, 0.5,-0.1,-0.1, 0.0, 	 0.1, 2.0, 0.6,-0.3, 	-0.4,-0.6, 0.7, 0.05]);
//						ideologyValues.Add("transhumanist", 	[ 0.6, 0.0,-0.2, 1.5, 1.0,-0.1, 1.0,-1.0, 0.0,-0.5, 	-1.0, 0.6, 0.4,-1.0, 	-2.0, 0.2,-1.0,-1.0 ]);
//					// factionIdeologies
//					var ideologyValuesFactions : Dictionary.<String, float[]> = new Dictionary.<String, float[]>();
//						// 												 cu   te   me   bu   li   nat  ar   im   nav  br   tr
//						ideologyValuesFactions.Add("nobleHouse1", 		[0.0, 0.0, 0.0, 0.0, 0.0, 40 , 60 , 0.0, 0.0, 0.0, 0.0]);
//						ideologyValuesFactions.Add("nobleHouse2", 		[0.0, 0.0, 40 , 0.0, 0.0, 0.0, 60 , 0.0, 0.0, 0.0, 0.0]);
//						ideologyValuesFactions.Add("nobleHouse3", 		[20 , 0.0, 0.0, 0.0, 0.0, 0.0, 20 , 60 , 0.0, 0.0, 0.0]);
//						ideologyValuesFactions.Add("nobleHouse4", 		[0.0, 0.0, 0.0, 0.0, 30 , 0.0, 50 , 0.0, 10 , 10 , 0.0]);
//						ideologyValuesFactions.Add("guild1", 			[0.0, 0.0, 50 , 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 50 ]);
//						ideologyValuesFactions.Add("guild2", 			[0.0, 40 , 20 , 30 , 0.0, 0.0, 10 , 0.0, 0.0, 0.0, 0.0]);
//						ideologyValuesFactions.Add("guild3", 			[0.0, 0.0, 60 , 30 , 0.0, 0.0, 0.0, 0.0, 10 , 0.0, 0.0]);
//						ideologyValuesFactions.Add("church", 			[100, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0]);
//						ideologyValuesFactions.Add("heretic", 			[0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 100]);
//					// factionStats
//					var factionValues : Dictionary.<String, float[]> = new Dictionary.<String, float[]>();
//						factionValues.Add("nobleHouse1", 		[ 0.74, 0.44, 0.20,-0.52,-0.18, 0.06,-0.26, 0.62,-0.22, 0.88, 	-0.18,-0.02,-0.34,-0.02, 	 0.76, 0.76, 0.48,-0.10 ]);	// 60% aris, 40% natio
//						factionValues.Add("nobleHouse2", 		[ 0.02, 0.24, 0.64, 0.68,-0.02, 0.58,-0.10,-0.06,-0.70, 0.24, 	-0.34, 0.26, 0.46,-0.42, 	 0.24, 0.08, 1.00, 0.22 ]);	// 60% aris, 40% merca
//						factionValues.Add("nobleHouse3", 		[ 0.46, 0.02,-0.18,-0.12,-0.28,-0.08,-0.38, 0.48,-0.06, 0.60, 	 0.50, 0.04, 0.12, 0.26, 	 0.54, 0.50, 0.44, 0.86 ]);	// 60% impe, 20% arist, 20% cult
//						factionValues.Add("nobleHouse4", 		[-0.32,-0.17, 0.16, 0.62, 0.08, 0.39, 0.27,-0.08, 0.01, 0.12, 	-0.32, 0.57, 0.37,-0.46, 	 0.04,-0.11, 0.46, 0.115]);	// 50% aris, 30% liber, 10% navi, 10% bro
//						factionValues.Add("guild1", 			[-0.20, 0.15, 0.40, 1.75, 0.85, 0.45, 0.60,-0.95,-0.50,-0.55, 	-0.70, 0.40, 0.70,-0.80, 	-1.30,-0.25, 0.00,-0.60 ]);	// 50% merc, 50% trans
//						factionValues.Add("guild2", 			[-0.19, 0.72, 0.54, 0.41, 0.17, 0.38, 0.26,-0.27,-0.35, 0.07, 	-0.22,-0.39, 0.46,-0.32, 	 0.00,-0.16, 0.43,-0.015]);	// 40% tech, 30% burea, 20% merc, 10% aris
//						factionValues.Add("guild3", 			[-0.72, 0.37, 0.83, 1.33, 0.44, 0.69, 0.16,-0.62,-0.81,-0.31, 	-0.27, 0.16, 0.77,-0.43, 	-0.26,-0.47, 0.79,-0.095]);	// 60% merc, 30% burea, 10% navi
//						factionValues.Add("church", 			[ 1.0 , 0.2 ,-1.0 ,-1.0 ,-0.3 ,-0.7 ,-1.0 , 1.0 , 0.8 , 1.0 , 	 1.0 ,-0.7 ,-0.4 , 1.0 , 	 1.0 , 1.0 ,-0.3 , 0.8  ]);	// 100% cult
//						factionValues.Add("heretic", 			[ 0.6 , 0.0 ,-0.2 , 1.5 , 1.0 ,-0.1 , 1.0 ,-1.0 , 0.0 ,-0.5 , 	-1.0 , 0.6 , 0.4 ,-1.0 , 	-2.0 , 0.2 ,-1.0 ,-1.0  ]);	// 100% trans
//					// ideologyDiplomacyValue
//						//						cult	techno	mercan	bureau	liber	natio	aris	imper	navig	bro 	trans
//						diplomacyValues.Add( 	[2.0,	0.8,	0.8,	1.2,	0.4,	1.0,	1.4,	1.6,	1.0,	0.6,	0.0]);
//						diplomacyValues.Add( 	[0.6,	1.2,	1.2,	0.8,	1.2,	1.0,	1.0,	1.0,	1.0,	1.0,	1.0]);
//						diplomacyValues.Add( 	[0.8,	1.0,	2.0,	1.0,	1.2,	1.0,	1.0,	1.0,	1.2,	1.0,	1.0]);
//						diplomacyValues.Add( 	[1.2,	1.0,	1.0,	1.2,	0.8,	1.0,	1.0,	1.0,	1.0,	1.0,	0.4]);
//						diplomacyValues.Add( 	[0.4,	1.0,	1.0,	1.0,	2.0,	0.6,	1.0,	0.8,	1.2,	1.2,	1.6]);
//						diplomacyValues.Add( 	[1.0,	1.0,	0.6,	1.0,	0.4,	0.0,	1.0,	0.0,	1.0,	0.8,	0.4]);
//						diplomacyValues.Add( 	[1.4,	1.0,	1.2,	1.0,	0.6,	0.6,	1.8,	1.6,	1.2,	1.0,	0.4]);
//						diplomacyValues.Add( 	[1.6,	1.0,	1.2,	1.2,	0.6,	0.0,	1.6,	2.0,	1.2,	1.0,	0.4]);
//						diplomacyValues.Add( 	[0.6,	1.0,	1.4,	1.0,	1.0,	1.0,	1.4,	1.2,	2.0,	1.6,	1.4]);
//						diplomacyValues.Add( 	[0.4,	1.0,	1.0,	1.0,	1.2,	1.0,	1.0,	1.4,	1.4,	2.0,	0.6]);
//						diplomacyValues.Add(	[0.0,	1.0,	1.2,	0.8,	2.0,	0.6,	0.6,	0.0,	1.4,	1.4,	2.5]);
//
//				// FACTIONS		
//				// go through factions
//				for (f in factionNames) {
//					faction.Add(f, new factionstateclass() );
//					// populate faction with relationTo other factions (not own)
//					for (a in factionNames) {
//						if (f != a) faction[f].relationsTo.Add(a, new relationsclass() );
//					}
//					
//					// add player relation
//					faction[f].relationsTo.Add("player", new relationsclass() );
//					
//					// populate factionStats
//						// create dictionary for factionStats
//						i = 0;
//						for (a in ideologyStatsNames) {
//							faction[f].factionStat.Add(a, factionValues[f][i]);
//							i++;
//						}
//					// populate ideologyValuesFactions
//						// create dictionary for ideologyValuesFactions
//						i = 0;
//						for (a in ideologyNames) {
//							faction[f].ideologies.Add(a, ideologyValuesFactions[f][i]);
//							i++;
//						}
//				}
//				
//					// names[]
//						faction["nobleHouse1"].names = nobleHouse1;
//						faction["nobleHouse2"].names = nobleHouse2;
//						faction["nobleHouse3"].names = nobleHouse3;
//						faction["nobleHouse4"].names = nobleHouse4;
//						faction["guild1"].names = guild1;
//						faction["guild2"].names = guild2;
//						faction["guild3"].names = guild3;
//						faction["church"].names = church;
//						faction["heretic"].names = heretic;
//				
//				// IDEOLOGIES		
//				// add ideologies
//				j = 0;
//				for (var f : String in ideologyNames) {
//					ideology.Add(f, new ideologystateclass() );
//									
//					// populate ideologyStat
//					
//						// add player relation
//							ideology[f].relationsTo.Add("player", new relationsclass() );
//					
//						// ideologyStat - populate dictionary
//							i = 0;
//							for (a in ideologyStatsNames) {
//								ideology[f].ideologyStat.Add(a, ideologyValues[f][i]);
//								i++;
//							}
//				}
//					// names[]
//						ideology["cult"].names = cult;					// []
//						ideology["technocrat"].names = technocrat;
//						ideology["mercantile"].names = mercantile;
//						ideology["bureaucracy"].names = bureaucracy;
//						ideology["liberal"].names = liberal;
//						ideology["nationalist"].names = nationalist;
//						ideology["aristocrat"].names = aristocrat;
//						ideology["imperialist"].names = imperialist;
//						ideology["navigators"].names = navigators;
//						ideology["brotherhood"].names = brotherhood;
//						ideology["transhumanist"].names = transhumanist;
//						//Debug.Log("debugging faction[\"nobleHouse1\"].names[5]: "+faction["nobleHouse1"].names[5]);
//				
//					
//				// AGENDAS
//				for (a in agendaNames) {
//					agendas.Add(a, new generalagendaclass() );
//				}
//				// faction agendas
//				var factionAgendaValues : Dictionary.<String, float[]> = new Dictionary.<String, float[]>();
//					factionAgendaValues.Add("nobleHouse1",		[-0.5,	-0.4,	-0.1,	0.0,	0.0,	1.0,	0.0,	0.0,	0.0 ]);
//					factionAgendaValues.Add("nobleHouse2", 		[0.5,	0.1,	-0.3,	0.0,	0.0,	0.0,	0.0,	0.0,	0.6 ]);
//					factionAgendaValues.Add("nobleHouse3", 		[0.0,	0.8,	0.2,	-0.2,	-0.8,	-0.5,	-0.2,	-0.3,	-0.3]);
//					factionAgendaValues.Add("nobleHouse4", 		[-0.2,	-0.2,	-0.5,	1.0,	0.0,	0.0,	0.0,	0.0,	0.0 ]);
//					factionAgendaValues.Add("guild1", 			[-0.1,	-0.1,	-0.8,	0.0,	1.0,	0.0,	0.0,	0.0,	0.0 ]);
//					factionAgendaValues.Add("guild2", 			[0.1,	0.0,	-0.2,	0.2,	0.0,	0.0,	0.0,	0.0,	0.7 ]);
//					factionAgendaValues.Add("guild3", 			[0.1,	0.0,	-0.2,	0.1,	0.0,	0.0,	0.0,	0.0,	0.9 ]);
//					factionAgendaValues.Add("church", 			[0.0,	0.0,	1.0,	-0.4,	-0.6,	-0.2,	-0.2,	-0.6,	-1.0]);
//					factionAgendaValues.Add("heretic", 			[-0.1,	-0.1,	-0.8,	0.0,	0.0,	0.0, 	0.0,	1.0,	0.0 ]);
//				
//				var ideologyAgendaValues : Dictionary.<String, float[]> = new Dictionary.<String, float[]>();
//					ideologyAgendaValues.Add("cult", 			[0.1,	0.1,	0.9,	-0.2,	-0.4,	-0.2,	-0.2,	-0.8,	-0.6]);
//					ideologyAgendaValues.Add("technocrat", 		[0.0,	0.0,	-0.1,	0.2,	-0.1,	0.0,	0.0,	0.0,	0.1 ]);
//					ideologyAgendaValues.Add("mercantile", 		[0.1,	0.1,	-0.3,	0.3,	0.0,	0.0,	0.0,	0.0,	1.0 ]);
//					ideologyAgendaValues.Add("bureaucracy", 	[0.2,	-0.1,	0.0,	0.0,	0.0,	-0.1,	-0.1,	-0.1,	-0.2]);
//					ideologyAgendaValues.Add("liberal", 		[-0.1,	-0.1,	-0.8,	1.0,	0.0,	0.0,	0.0,	0.0,	0.0 ]);
//					ideologyAgendaValues.Add("nationalist", 	[-0.9,	-0.9,	-0.1,	0.0,	0.0,	1.0,	1.0,	0.0,	0.0 ]);
//					ideologyAgendaValues.Add("aristocrat", 		[0.4,	0.1,	0.0,	-0.4,	-0.1,	-0.4,	-0.1,	0.0,	-0.2]);
//					ideologyAgendaValues.Add("imperialist", 	[0.9,	0.9,	0.1,	-0.3,	-0.4,	-0.4,	-0.4,	-0.2,	-0.4]);
//					ideologyAgendaValues.Add("navigators", 		[0.1,	0.1,	-0.1,	0.1,	0.3,	0.0,	0.0,	0.0,	0.2 ]);
//					ideologyAgendaValues.Add("brotherhood", 	[0.0,	0.1,	0.0,	0.0,	0.2,	-0.1,	-0.1,	-0.2,	0.0 ]);
//					ideologyAgendaValues.Add("transhumanist",	[-0.4,	-0.2,	-0.4,	0.0,	1.0,	0.0,	0.0,	1.0,	0.0 ]);
//				
//					// add faction agenda
//					for (var f : String in factionNames) {
//						i = 0;
//						for (a in agendaNames) {
//							faction[f].agenda.Add(a, new agendaclass(factionAgendaValues[f][i]) );
//							i++;
//						}
//					}		
//					// add ideology agenda
//					for (var f : String in ideologyNames) {
//						i = 0;
//						for (a in agendaNames) {
//							ideology[f].agenda.Add(a, new agendaclass(ideologyAgendaValues[f][i]) );
//							i++;
//						}
//					}	
//
//					// agenda values AT START	// > 0.15 agenda >0.5 is strong agenda, > 0.75 is committed! (even rebellion)
//						faction["nobleHouse1"].agenda["separatist"].value 		= 0.7;	// Furia
//						faction["nobleHouse2"].agenda["individualist"].value 	= 0.7;	// Rathmund
//						faction["nobleHouse2"].agenda["loyalist"].value 		= 0.2;
//						faction["nobleHouse3"].agenda["idealist"].value 		= 0.8;	// Tarquinia
//						faction["nobleHouse3"].agenda["faithful"].value 		= 0.2;
//						faction["nobleHouse4"].agenda["reformist"].value 		= 0.8;	// Valeria
//						faction["guild1"].agenda["transhumanist"].value			= 0.45; // Everlasting Union
//						faction["guild1"].agenda["individualist"].value			= 0.45;
//						faction["guild2"].agenda["individualist"].value 		= 0.55; // Dacei family
//						faction["guild2"].agenda["reformist"].value 			= 0.14;
//						faction["guild3"].agenda["individualist"].value 		= 0.8;	// Coruna Cartel
//						faction["guild3"].agenda["reformist"].value 			= 0.06;
//						faction["church"].agenda["faithful"].value 				= 1.0;	// church
//						faction["heretic"].agenda["heretic"].value 				= 1.0;	// heretic
//				
//				factionValues = null;
//				ideologyValues = null;
//				factionAgendaValues = null;
//				ideologyAgendaValues = null;
//				
//				// debug 2d nested dictionary
//				/*locationRelations[GameObject.Find("1I01")][GameObject.Find("1I02")].setHistorical(1);
//				locationRelations[GameObject.Find("1I01")][GameObject.Find("1I02")].setEconomical(2);
//				locationRelations[GameObject.Find("1I01")][GameObject.Find("1I02")].ideological = 2;
//				locationRelations[GameObject.Find("1I01")][GameObject.Find("1I02")].update();
//				var a = locationRelations[GameObject.Find("1I01")][GameObject.Find("1I02")].total;
//				Debug.Log("debug 2d nested array, total: "+a);*/
//				
//				//var b = ideology["cult"].ideologyStat["innovation"];
//				//Debug.Log("debug cult innovation: "+b);	
//				//Debug.Log("debug cult --> technocrat: "+c);	
//
//		// Enum lists
//			// moduleClasses - which moduleType belongs to under which moduleClass
//			var classes : ModuleClass[] = Enum.GetValues(typeof(ModuleClass));
//			var types : ModuleTypes[] = Enum.GetValues(typeof(ModuleTypes));
//			var cStr : String;
//			var tStr : String;
//			for (var c : ModuleClass in classes)
//			{
//				if (c != ModuleClass.none)
//				{
//					moduleClasses.Add(c, new List.<ModuleTypes>() );
//					cStr = c.ToString();
//					for (var t : ModuleTypes in types)
//					{
//						tStr = t.ToString();
//						// if type is sub-cathegory of class
//						if (t != ModuleTypes.none &&
//							// type shares class enum name
//							tStr.Contains(cStr) ||
//							(cStr.Contains("utility") && (tStr.Contains("cargo") || tStr.Contains("other utility modules")) )			
//						) 
//						{
//							moduleClasses[c].Add(t);
//						}
//					}
//				}
//			}
//
//		// DEFAULT values
//
//			weaponDictionary = WeaponData.getWeapons();
//			sensorDictionary = SensorData.getSensors();
//
//			ShipTemplatesReader.loadShipTypeTemplates();
//			ShipTemplatesReader.loadShipTemplates();
//			ShipTemplatesReader.loadModuleTemplates();
//		
//		//Debug.Log("player.seenMutants: "+player.seenMutants);
//	}
//
//	// ****************************************************************************************************************
//
//	public function lateInitialize() {	// this function needs game data initialized from other scripts
//				
//				// PLAYER - assign values to player ideology
//				player.ideologyOriginal = characters.character[characters.assignment["captain"]].ideology;
//				player.factionOriginal = characters.character[characters.assignment["captain"]].affiliation;
//					
//					// copy captain ideology values
//					for (var a : String in ideologyNames) {
//						player.ideology[a] = characters.character[characters.assignment["captain"]].ideologies[a];
//					}
//					// faction value
//					if (characters.character.ContainsKey(characters.assignment["captain"]) ) {
//						if (characters.character[characters.assignment["captain"]].affiliation != null) {
//							player.faction[characters.character[characters.assignment["captain"]].affiliation] = 1; 		// 1 = 100%, player gets to decide how strong affiliation he wants?
//						}
//					}
//				
//				// LOCATIONS - add locations (2D nested) location[locobject][locobject].update();
//				for (var locA : locationDatabase in collector.locationsPlanets) {
//					locationRelations.Add(locA, new Dictionary.<locationDatabase, relationsclass>() );
//					for (var locB : locationDatabase in collector.locationsPlanets) {
//						locationRelations[locA].Add(locB, new relationsclass() );
//					}
//				}
//					// calculate initial loc relations
//					for (locA in collector.locationsPlanets) {
//						for (locB in collector.locationsPlanets) {
//							if (locA != locB) updateLocationRelationsIdeological(locA, locB);
//							//if (locA.data.name == "4EG01" && locA.data != locB.data) Debug.Log(locA.data.name+" relations to "+locB.data.name+": "+getLocationRelations(locA.data, locB.data) );
//						}
//					}
//					// calculate initial faction relations
//					for (a in factionNames) {
//						for (var b : String in factionNames) {
//							if (a != b) updateFactionRelationsIdeological(a, b);
//						}
//					}
//					
//					
//				//Debug.Log("debugging location relations '4EG01' to '7IS08': "+getLocationRelations(GameObject.Find("4EG01"), GameObject.Find("7IS08") ) );
//				
//				// set up sectorRulers
//				sectorRulers.Add(GameObject.Find("4S01"), GameObject.Find("4S01").gameObject.GetComponent(locationDatabase).ruler);
//
//				// get player visibilities from captain's faction?
//
//				// PLAYER SHIP
//				
//	}
//
//	// ****************************************************************************************
//
//
//
//
//
//
//
//
//
//
//	// FUNCTIONS
//
//
//
//	public function getLocationState (whoseData : locationDatabase, toData : locationDatabase) : float {
//		
//		// if same
//		if (whoseData == toData) { Debug.Log("WARNING: hmm, this shouldn't happen"); return 100; }	// debug
//		
//		// set up links for location
//		var whosef : String = whoseData.controlledBy;
//		var tof : String = toData.controlledBy;
//		
//			// check if factions on both sides
//			if (whosef != null && tof != null) {
//				
//				// FACTION TO FACTION
//				
//				// if same
//				if (whosef == tof) { return 100; }	// 100 peace with itself
//				// else RETURN faction-to-faction state
//				else return faction[whosef].relationsTo[tof].state; 
//			}
//			
//			// LOCATION TO LOCATION
//			else {
//				if (locationRelations.ContainsKey(whoseData) && locationRelations[whoseData].ContainsKey(toData) ) {
//					return locationRelations[whoseData][toData].state;
//				} 
//				else Debug.LogWarning("ERROR: getLocationStatus GameObject-input for 'whose' or 'to' were not in locationRelations[]: "+whoseData+"/"+toData);
//			}
//		return 0;
//	}
//		
//		
//	function getLocationRelations (whose : locationDatabase, to : locationDatabase) : float {
//		
//		// if same
//		if (whose == to) { Debug.Log("WARNING: checking itself, this shouldn't happen [whose: "+whose+" to: "+to+"]"); return 100; }		// debug
//		
//		if (locationRelations.ContainsKey(whose) && locationRelations[whose].ContainsKey(to) ) {
//			
//			// check if factions on both sides
//			if (whose.controlledBy != null && to.controlledBy != null) {
//				
//				// FACTION TO FACTION
//				//Debug.Log("Faction-to-faction not ready");
//				var whosef : String = whose.controlledBy;
//				var tof : String = to.controlledBy;
//				
//				// if same faction
//				if (whosef == tof) return 100;
//				
//				updateFactionRelationsIdeological(whosef, tof);
//					//**/if (whose.name == "4EG01" && to.name == "7IS08") Debug.Log("F-F ["+whosef+"/"+tof+"]");
//				return faction[whosef].relationsTo[tof].calculateTotal();
//			}
//			// LOCATION TO LOCATION
//			else {
//				updateLocationRelationsIdeological(whose, to); //**/if (whose.name == "4EG01" && to.name == "7IS08") Debug.Log("L-L ["+whose.name+"/"+to.name+"]  calculateTotal(): "+locationRelations[whose][to].calculateTotal()+" ideological: "+locationRelations[whose][to].ideological+" reparations: "+locationRelations[whose][to].reparations+" recent: "+locationRelations[whose][to].recent+" historical: "+locationRelations[whose][to].historical+" economical: "+locationRelations[whose][to].economical+" ");
//				return locationRelations[whose][to].calculateTotal();
//			}
//			
//		}
//		else Debug.LogWarning("ERROR: getLocationStatus input for 'whose' or 'to' was illegal: "+whose+"/"+to);
//		return 0;
//	}
//
//	// Transform NobleHouse1 (House Furia) naming to separatist rebels
//	function nobleHouse1RebelNames(rebel : boolean) {
//		if (rebel) 	faction["nobleHouse1"].names = ["House Furia",		"separatist",			"Independent",	"Separatist",	"Separatist",	"Councillor",	"Commissar",	"People's Commissar","High Commander",			""];
//		else 		faction["nobleHouse1"].names = ["House Furia",		"aristocrat",			"Aristocracy",	"Imperial",		"Imperial",		"Senator",		"Governor",		"Exarch",			"High Exarch",				""];
//	}
//
//
//
//	function updateLocationRelationsIdeological (whoseData : locationDatabase, toData : locationDatabase) {
//			// update ideological - diplomacy modifier
//			
//			if (!locationRelations[whoseData][toData].ideologicalCalculated) {
//			
//				locationRelations[whoseData][toData].ideological = 
//				
//				(whoseData.ideologies["cult"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[0][0] + toData.ideologies["technocrat"]	* diplomacyValues[0][1] + toData.ideologies["mercantile"]	* diplomacyValues[0][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[0][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[0][4] + toData.ideologies["nationalist"]	* diplomacyValues[0][5] + toData.ideologies["aristocrat"]	* diplomacyValues[0][6] + toData.ideologies["imperialist"]	* diplomacyValues[0][7] + toData.ideologies["navigators"]	* diplomacyValues[0][8] + toData.ideologies["brotherhood"]	* diplomacyValues[0][9] + toData.ideologies["transhumanist"]* diplomacyValues[0][10]) +
//				whoseData.ideologies["technocrat"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[1][0] + toData.ideologies["technocrat"]	* diplomacyValues[1][1] + toData.ideologies["mercantile"]	* diplomacyValues[1][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[1][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[1][4] + toData.ideologies["nationalist"]	* diplomacyValues[1][5] + toData.ideologies["aristocrat"]	* diplomacyValues[1][6] + toData.ideologies["imperialist"]	* diplomacyValues[1][7] + toData.ideologies["navigators"]	* diplomacyValues[1][8] + toData.ideologies["brotherhood"]	* diplomacyValues[1][9] + toData.ideologies["transhumanist"]* diplomacyValues[1][10]) +
//				whoseData.ideologies["mercantile"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[2][0] + toData.ideologies["technocrat"]	* diplomacyValues[2][1] + toData.ideologies["mercantile"]	* diplomacyValues[2][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[2][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[2][4] + toData.ideologies["nationalist"]	* diplomacyValues[2][5] + toData.ideologies["aristocrat"]	* diplomacyValues[2][6] + toData.ideologies["imperialist"]	* diplomacyValues[2][7] + toData.ideologies["navigators"]	* diplomacyValues[2][8] + toData.ideologies["brotherhood"]	* diplomacyValues[2][9] + toData.ideologies["transhumanist"]* diplomacyValues[2][10]) +
//				whoseData.ideologies["bureaucracy"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[3][0] + toData.ideologies["technocrat"]	* diplomacyValues[3][1] + toData.ideologies["mercantile"]	* diplomacyValues[3][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[3][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[3][4] + toData.ideologies["nationalist"]	* diplomacyValues[3][5] + toData.ideologies["aristocrat"]	* diplomacyValues[3][6] + toData.ideologies["imperialist"]	* diplomacyValues[3][7] + toData.ideologies["navigators"]	* diplomacyValues[3][8] + toData.ideologies["brotherhood"]	* diplomacyValues[3][9] + toData.ideologies["transhumanist"]* diplomacyValues[3][10]) +
//				whoseData.ideologies["liberal"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[4][0] + toData.ideologies["technocrat"]	* diplomacyValues[4][1] + toData.ideologies["mercantile"]	* diplomacyValues[4][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[4][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[4][4] + toData.ideologies["nationalist"]	* diplomacyValues[4][5] + toData.ideologies["aristocrat"]	* diplomacyValues[4][6] + toData.ideologies["imperialist"]	* diplomacyValues[4][7] + toData.ideologies["navigators"]	* diplomacyValues[4][8] + toData.ideologies["brotherhood"]	* diplomacyValues[4][9] + toData.ideologies["transhumanist"]* diplomacyValues[4][10]) +
//				whoseData.ideologies["nationalist"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[5][0] + toData.ideologies["technocrat"]	* diplomacyValues[5][1] + toData.ideologies["mercantile"]	* diplomacyValues[5][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[5][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[5][4] + toData.ideologies["nationalist"]	* diplomacyValues[5][5] + toData.ideologies["aristocrat"]	* diplomacyValues[5][6] + toData.ideologies["imperialist"]	* diplomacyValues[5][7] + toData.ideologies["navigators"]	* diplomacyValues[5][8] + toData.ideologies["brotherhood"]	* diplomacyValues[5][9] + toData.ideologies["transhumanist"]* diplomacyValues[5][10]) +
//				whoseData.ideologies["aristocrat"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[6][0] + toData.ideologies["technocrat"]	* diplomacyValues[6][1] + toData.ideologies["mercantile"]	* diplomacyValues[6][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[6][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[6][4] + toData.ideologies["nationalist"]	* diplomacyValues[6][5] + toData.ideologies["aristocrat"]	* diplomacyValues[6][6] + toData.ideologies["imperialist"]	* diplomacyValues[6][7] + toData.ideologies["navigators"]	* diplomacyValues[6][8] + toData.ideologies["brotherhood"]	* diplomacyValues[6][9] + toData.ideologies["transhumanist"]* diplomacyValues[6][10]) +
//				whoseData.ideologies["imperialist"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[7][0] + toData.ideologies["technocrat"]	* diplomacyValues[7][1] + toData.ideologies["mercantile"]	* diplomacyValues[7][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[7][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[7][4] + toData.ideologies["nationalist"]	* diplomacyValues[7][5] + toData.ideologies["aristocrat"]	* diplomacyValues[7][6] + toData.ideologies["imperialist"]	* diplomacyValues[7][7] + toData.ideologies["navigators"]	* diplomacyValues[7][8] + toData.ideologies["brotherhood"]	* diplomacyValues[7][9] + toData.ideologies["transhumanist"]* diplomacyValues[7][10]) +
//				whoseData.ideologies["navigators"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[8][0] + toData.ideologies["technocrat"]	* diplomacyValues[8][1] + toData.ideologies["mercantile"]	* diplomacyValues[8][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[8][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[8][4] + toData.ideologies["nationalist"]	* diplomacyValues[8][5] + toData.ideologies["aristocrat"]	* diplomacyValues[8][6] + toData.ideologies["imperialist"]	* diplomacyValues[8][7] + toData.ideologies["navigators"]	* diplomacyValues[8][8] + toData.ideologies["brotherhood"]	* diplomacyValues[8][9] + toData.ideologies["transhumanist"]* diplomacyValues[8][10]) +
//				whoseData.ideologies["brotherhood"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[9][0] + toData.ideologies["technocrat"]	* diplomacyValues[9][1] + toData.ideologies["mercantile"]	* diplomacyValues[9][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[9][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[9][4] + toData.ideologies["nationalist"]	* diplomacyValues[9][5] + toData.ideologies["aristocrat"]	* diplomacyValues[9][6] + toData.ideologies["imperialist"]	* diplomacyValues[9][7] + toData.ideologies["navigators"]	* diplomacyValues[9][8] + toData.ideologies["brotherhood"]	* diplomacyValues[9][9] + toData.ideologies["transhumanist"]* diplomacyValues[9][10]) +
//				whoseData.ideologies["transhumanist"] * (
//					toData.ideologies["cult"]	 		* diplomacyValues[10][0] + toData.ideologies["technocrat"]	* diplomacyValues[10][1] + toData.ideologies["mercantile"]	* diplomacyValues[10][2] + toData.ideologies["bureaucracy"]	* diplomacyValues[10][3] + toData.ideologies["liberal"] 	 	* diplomacyValues[10][4] + toData.ideologies["nationalist"]	* diplomacyValues[10][5] + toData.ideologies["aristocrat"]	* diplomacyValues[10][6] + toData.ideologies["imperialist"]	* diplomacyValues[10][7] + toData.ideologies["navigators"]	* diplomacyValues[10][8] + toData.ideologies["brotherhood"]	* diplomacyValues[10][9] + toData.ideologies["transhumanist"]* diplomacyValues[10][10])
//					) /10000; // /100 from whose and /100 from to values
//				
//				locationRelations[whoseData][toData].ideologicalCalculated = true;
//				
//				//if (whose.name == "4EG09" && to.name == "7IS08") Debug.Log("calculated: locationRelations[4EG09][7IS08].ideological  "+locationRelations[whose][to].ideological);	// debug
//				//if (whose.name == "7IS08" && to.name == "2C09") Debug.Log("calculated: locationRelations[7IS08][2C09].ideological  "+locationRelations[whose][to].ideological);		// debug
//			}	
//			
//	}
//
//	function updateFactionRelationsIdeological (whose : String, to : String) {
//			
//			if (whose == to) { Debug.Log("maybe you should add faction's realationTo faction itself"); return; }	// debug
//			
//			// update ideological - diplomacy modifier
//			// faction[whose].relationsTo[to].total  /// faction[whose].factionStat["innovation"]
//			//if (!faction.ContainsKey(whose)) Debug.LogError("faction<dict> (whose) does not contain key "+whose);					// debugging
//			//if (!faction[whose].relationsTo.ContainsKey(to)) Debug.LogError("faction<dict>.relationsTo does not contain key "+to+"   (whose: "+whose+")");	// debugging
//			
//		
//			if (!faction[whose].relationsTo[to].ideologicalCalculated) {
//			
//				faction[whose].relationsTo[to].ideological = 
//				
//				(faction[whose].ideologies["cult"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[0][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[0][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[0][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[0][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[0][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[0][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[0][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[0][7] + faction[to].ideologies["navigators"]	* diplomacyValues[0][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[0][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[0][10]) +
//				faction[whose].ideologies["technocrat"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[1][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[1][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[1][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[1][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[1][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[1][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[1][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[1][7] + faction[to].ideologies["navigators"]	* diplomacyValues[1][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[1][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[1][10]) +
//				faction[whose].ideologies["mercantile"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[2][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[2][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[2][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[2][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[2][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[2][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[2][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[2][7] + faction[to].ideologies["navigators"]	* diplomacyValues[2][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[2][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[2][10]) +
//				faction[whose].ideologies["bureaucracy"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[3][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[3][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[3][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[3][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[3][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[3][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[3][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[3][7] + faction[to].ideologies["navigators"]	* diplomacyValues[3][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[3][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[3][10]) +
//				faction[whose].ideologies["liberal"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[4][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[4][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[4][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[4][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[4][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[4][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[4][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[4][7] + faction[to].ideologies["navigators"]	* diplomacyValues[4][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[4][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[4][10]) +
//				faction[whose].ideologies["nationalist"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[5][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[5][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[5][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[5][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[5][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[5][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[5][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[5][7] + faction[to].ideologies["navigators"]	* diplomacyValues[5][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[5][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[5][10]) +
//				faction[whose].ideologies["aristocrat"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[6][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[6][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[6][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[6][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[6][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[6][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[6][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[6][7] + faction[to].ideologies["navigators"]	* diplomacyValues[6][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[6][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[6][10]) +
//				faction[whose].ideologies["imperialist"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[7][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[7][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[7][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[7][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[7][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[7][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[7][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[7][7] + faction[to].ideologies["navigators"]	* diplomacyValues[7][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[7][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[7][10]) +
//				faction[whose].ideologies["navigators"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[8][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[8][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[8][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[8][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[8][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[8][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[8][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[8][7] + faction[to].ideologies["navigators"]	* diplomacyValues[8][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[8][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[8][10]) +
//				faction[whose].ideologies["brotherhood"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[9][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[9][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[9][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[9][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[9][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[9][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[9][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[9][7] + faction[to].ideologies["navigators"]	* diplomacyValues[9][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[9][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[9][10]) +
//				faction[whose].ideologies["transhumanist"] * (
//					faction[to].ideologies["cult"]	 		* diplomacyValues[10][0] + faction[to].ideologies["technocrat"]	* diplomacyValues[10][1] + faction[to].ideologies["mercantile"]	* diplomacyValues[10][2] + faction[to].ideologies["bureaucracy"]	* diplomacyValues[10][3] + faction[to].ideologies["liberal"] 	 	* diplomacyValues[10][4] + faction[to].ideologies["nationalist"]	* diplomacyValues[10][5] + faction[to].ideologies["aristocrat"]	* diplomacyValues[10][6] + faction[to].ideologies["imperialist"]	* diplomacyValues[10][7] + faction[to].ideologies["navigators"]	* diplomacyValues[10][8] + faction[to].ideologies["brotherhood"]	* diplomacyValues[10][9] + faction[to].ideologies["transhumanist"]* diplomacyValues[10][10])
//					) /10000; // /100 from whose and /100 from to values
//				
//				faction[whose].relationsTo[to].ideologicalCalculated = true;
//				
//				//if (whose == "nobleHouse1" && to == "nobleHouse2") Debug.Log("calculated: faction[nobleHouse1].relationsTo[nobleHouse2].ideological  "+faction[whose].relationsTo[to].ideological);	// debug
//				//if (whose == "nobleHouse1" && to == "church") Debug.Log("calculated: faction[nobleHouse1].relationsTo[church].ideological  "+faction[whose].relationsTo[to].ideological);	// debug
//			}	
//			
//	}
//
//
//	function doesLocationSee (whoseData : locationDatabase, toData : locationDatabase) : boolean {
//			
//			// if can't contact by itself, can't see
//			if (whoseData.visibleTo[10] <= 0) return false;
//			
//			// if no faction looking, rely on them being able to contact ( visibleTo[10] )
//			if (whoseData.controlledBy == null) {
//				if (toData.visibleTo[10] > 0 && whoseData.visibleTo[10] > 0) return true;
//				else return false;
//			}
//			
//			// Faction looking:
//			// find i matching factionLooking in visibleTo
//			var i : int = 0;
//			for (var str : String in factionNames) {
//				i++;	// starting with 1, since visibleTo[0] = player
//				if (str == whoseData.controlledBy) {
//					if (toData.visibleTo[i] > 0) return true;
//					else return false;
//				}
//			}
//			
//			Debug.LogError("ERROR: can't resolve visibility. Looking if '"+whoseData.name+"' see '"+toData.name+"'");
//			return false;
//	}
//
//	function locationFound (foundLocation : locationDatabase) {
//
//		// visible to player
//		if (foundLocation.visibleTo[0] == 0) foundLocation.visibleTo[0] = 1;
//
//		// location is visible IF PALYER RATS IT OUT TO IMPERIALS?
//		//if (false && !foundLocation.visible) foundLocation.visible = true;
//
//		// check visibilityTo from palyer faction?
//		if (canPlayerMakeVisibleTo(foundLocation, Characters.Instance.character[Characters.Instance.assignment["captain"]].affiliation)) Debug.Log("Player informed "+Characters.Instance.character[Characters.Instance.assignment["captain"]].affiliation+" about the location -- not implemented yet!");
//			// just make it visible to ALL
//			//for (var i : int = 0; i<10; i++) if (foundLocation.visibleTo[i] == 0) foundLocation.visibleTo[i] = 1;
//
//		// make location found (self visible)
//		if (foundLocation.visibleTo[10] == 0) {
//			foundLocation.visibleTo[10] = 1;
//			// rewards etc? Make an inLocation event for this?
//			Debug.Log("Found lost colony! Rewards are not implemented yet!");
//		}
//	}
//
//	function canPlayerMakeVisibleTo (foundLocation : locationDatabase, playerFaction : String) : boolean {
//
//		if (playerFaction == null) return false;
//
//		// get faction index for visibleTo -list
//		var i : int = 0;
//		for (var f : factionstateclass in faction.Values) {
//			i++;
//			if (playerFaction == f.names[0]) {
//				// faction found
//				if (foundLocation.visibleTo[i] == 0) {
//					// faction was invisible to faction
//					foundLocation.visibleTo[i] = 1;
//					return true;
//				}
//				else {
//					// faction already visible
//					return false;
//				}
//			}
//		}
//		// no faction found
//		Debug.LogError("ERROR: no faction '"+faction+"' found");
//		return false;
//	}
//
//
//}