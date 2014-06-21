//#pragma strict
//import System.Collections.Generic;
//import System.IO;
//
//static var Instance : Characters;
//
//// Player - LocationFinder (Collider / Finder)
//private var player : GameObject;
//private var finder : LocationFinder;
//
//// ScriptHolder 
//static var Characters : Characters;
//private var scriptHolder : GameObject;
//	private var location : locationDatabase;
//	private var nameGenerator : NameGenerator;
//	private var collector : locationsDataCollector;
//	//private var ship : PlayerShipData;
//	//private var shipPop : ShipPopulation;
//	//private var economy : Economy;
//	private var event : Events;
//	//private var eventManager : EventManager;
//
//var debugging : boolean = false;
//private var isProxy : boolean = false;
//
//var selectedCharacter : String;
//
//var portrait : List.<portraitClass> = new List.<portraitClass>();
//
//var characterName : String = "";
//var affiliation	: 	String;
//var ideology 	: 	String;
//var agenda 		: 	String;
//var experience	: 	float;
//
//var roll : float;	// roll from the script where you want to roll stats
//
//
//private var portraitTag : String = null;private var age			: float = 0; private var gender			: String = null; private var health 	: float = 0; private var insanity 		: float = 0; 		private var clone 		: float = 0; private var HQclone		: boolean = false;	private var possessed	: float = 0; private var ai 			: float = 0; private var loyalty 	: float = 0; private var happiness 	: float = 0; private var idealist 	: float = 50; private var kind 	: float = 50; 
//private var leadership 	: float = 0; private var emissar		: float = 0; 	private var hr			: float = 0; private var engineering	: float = 0; 		private var precognition: float = 0; private var psy			: float = 0; private var navigation	: float = 0; private var spaceBattle: float = 0; private var combat		: float = 0; private var trading		: float = 0; private var diplomat	: float = 0; private var scientist	: float = 0; private var integrity	: float = 0; private var holiness	: float = 0; private var purity		: float = 0; private var security	: float = 0; private var violent		: float = 0; private var aristocrat	: float = 0; private var imperialist	: float = 0; private var corruption	: float = 0;
//private var navigator : boolean = false; private var psycher : boolean = false; 
//
//enum portraitStatus {
//	taken,		// a character is currently using this portrait
//	available,	// available
//	used		// portrait was used before in this game - if out of portraits, reshuffle these as available
//}
//
//class portraitClass {
//	var name : String;		// name of the portrait file
//	var status : portraitStatus;	// in-use status
//	var tag : String;		// specially tagged portraits, such as "navigator", can be reserved for those characters
//	var id : float;			// game-unique 
//
//	// constructor
//	function portraitClass(Name : String, Tag : String) { 
//		this.name = Name; 
//		this.tag = Tag;
//		this.status = portraitStatus.available;
//		this.id = Random.value;
//	}
//}
//
//class characterStats {
//		var pgrowth : float;
//		var industry : float;
//		var economy : float;
//		var diplomacy : float;
//		var happiness : float;
//		var affluence : float;
//		var innovation : float;
//		var morale : float;
//		var altruism : float;
//		var military : float;
//		
//		var holy : float;
//		var psych : float;
//		var navigation : float;
//		var purity : float;
//		
//		var police : float;
//		var violent : float;
//		var aristocracy : float;
//		var imperialism : float;
//}
///*class ideologyclass {
//		var cult : float 		= 0;
//		var technocrat : float 	= 0;
//		var mercantile : float 	= 0;
//		var bureaucracy : float = 0;
//		var liberal : float 	= 0;
//		var nationalist : float = 0;
//		var aristocrat : float 	= 0;
//		var imperialist : float = 0;
//		var navigators : float 	= 0;
//		var brotherhood : float = 0;
//		var transhumanist : float = 0;
//}*/
//var characterStats = new characterStats();
////private var ideologyClass = new ideologyclass();	replace ideologyClass. with ideologies[""]
//var ideologies : Dictionary.<String, float> = new Dictionary.<String, float>();
//
//public var assignment : Dictionary.<String, String> = new Dictionary.<String, String>();	// job, name
//
//// *********************************************************************************************
//public var character : Dictionary.<String, characterClass>;// = new Dictionary.<String, characterClass>();
//class characterClass {
//	
//	var isActive 	: boolean 		= true;
//	var portraitName: String 		= null;
//	var portraitTag	: String 		= null;
//	var player 		: boolean 		= false;
//
//	var name		: String		= "Unnamed";
//	var age			: float			= 45;
//	var gender		: String 		= null;		
//	var origins		: GameObject 	= null; //GameObject.Find("4EG01");
//	var affiliation : String		= null;	// faction
//	var ideology	: String		= null;
//	var agenda 		: String 		= null;
//	var assignment	: String 		= null;
//	var dateAssigned: float			= -1;
//	var health		: float			= 100;			// 0 - 100
//	var polluted	: float			= 0;			// 0 - 100
//	var insanity	: float			= 0;			// 0 - 100
//	var clone		: float			= 0;			// 0 - 100
//	var HQclone		: boolean		= false;		// high quality clone?
//	var possessed	: float			= 0;			// 0 - 100
//	var ai			: float			= 0;			// 0 - 100
//	var loyalty		: float			= 50;			// 0 - 100
//	var happiness	: float 		= 50;			// 0 - 100
//	var navigator	: boolean		= false;
//	var psycher		: boolean		= false;
//	
//	var idealist	: float			= 50;
//	var kind		: float			= 50;
//	
//	var leadership 	: float			= 0;
//	var emissar		: float			= 0;
//	var hr			: float			= 0;
//	var engineering	: float			= 0;
//	
//	var precognition: float			= 0;
//	var psy			: float			= 0;
//	var navigation	: float			= 0;
//	
//	var spaceBattle : float			= 0;
//	var combat		: float			= 0;
//	
//	var trading		: float			= 0;
//	var diplomat	: float			= 0;
//	var scientist	: float			= 0;
//	var integrity	: float			= 0;
//	var holiness	: float			= 0;
//	var purity		: float			= 0;
//	var security	: float			= 0;
//	var violent		: float			= 0;
//	var aristocrat	: float			= 0;
//	var imperialist	: float			= 0;
//
//	var corruption	: float			= 0;
//	
//	var ideologies : Dictionary.<String, float> = new Dictionary.<String, float>();
//	//var ideologies = new ideologyclass();
//	
//	var portraitGeneration = function() {
//		if (this.portraitName != null) {
//			var generation : String;
//			while (true) {
//				if (this.age > 64) { generation = "d"; break; }
//				if (this.age > 44) { generation = "c"; break; }
//				if (this.age > 27) { generation = "b"; break; }
//				else generation = "a"; 
//				break;
//			}
//			this.portraitName = this.portraitName.Remove(12, 1);
//			this.portraitName = this.portraitName.Insert(12, generation);
//
//			//Debug.Log("portraitGeneration: "+this.portraitName);
//		}
//		else Debug.LogError("ERROR: characterPortrait generation was 'null'.");
//	};
//
//	var addStat		= function (skill : String, amount : float) { 
//		if (skill == "age") 		{ this.age		+= amount; return;}
//		if (skill == "health") 		{ if (this.health <= 100-amount) { this.health	+= amount; return;} else this.health = 100; return; }
//		if (skill == "polluted") 	{ this.polluted	+= amount; return;}
//		if (skill == "insanity") 	{ this.insanity	+= amount; return;}
//		if (skill == "clone") 		{ this.clone	+= amount; return;}
//		if (skill == "possessed") 	{ this.possessed+= amount; return;}
//		if (skill == "ai") 			{ this.ai 		+= amount; return;}
//		if (skill == "loyalty") 	{ this.loyalty	+= amount; return;}
//		if (skill == "happiness") 	{ this.happiness+= amount; return;}
//		
//		if (skill == "idealist") 	{ this.idealist	+= amount; return;}
//		if (skill == "kind") 		{ this.kind		+= amount; return;}
//		
//		if (skill == "leadership") 		{ this.leadership 	+= amount; return;}
//		if (skill == "emissar") 		{ this.emissar		+= amount; return;}
//		if (skill == "hr") 				{ this.hr			+= amount; return;}
//		if (skill == "engineering") 	{ this.engineering	+= amount; return;}
//		if (skill == "precognition") 	{ this.precognition	+= amount; return;}
//		if (skill == "psy") 			{ this.psy			+= amount; return;}
//		if (skill == "navigation") 		{ this.navigation	+= amount; return;}
//		if (skill == "spaceBattle") 	{ this.spaceBattle	+= amount; return;}
//		if (skill == "combat") 			{ this.combat		+= amount; return;}
//		if (skill == "trading") 		{ this.trading		+= amount; return;}
//		if (skill == "diplomat") 		{ this.diplomat		+= amount; return;}
//		if (skill == "scientist") 		{ this.scientist	+= amount; return;}
//		if (skill == "integrity") 		{ this.integrity	+= amount; return;}
//		if (skill == "holiness") 		{ this.holiness		+= amount; return;}
//		if (skill == "purity") 			{ this.purity		+= amount; return;}
//		if (skill == "security") 		{ this.security		+= amount; return;}
//		if (skill == "violent") 		{ this.violent		+= amount; return;}
//		if (skill == "aristocrat") 		{ this.aristocrat	+= amount; return;}
//		if (skill == "imperialist") 	{ this.imperialist	+= amount; return;}
//		if (skill == "corruption") 		{ this.corruption	+= amount; return;}
//		else Debug.LogError("Incorrect skill input for \"addStat()\"");
//		};		
//	
//	var setStat		= function (skill : String, amount : float) { 
//		if (skill == "age") 		{ this.age		= amount; return;}
//		if (skill == "health") 		{ if (this.health <= 100-amount) { this.health	= amount; return;} else this.health = 100; return; }
//		if (skill == "polluted") 	{ this.polluted	= amount; return;}
//		if (skill == "insanity") 	{ this.insanity	= amount; return;}
//		if (skill == "clone") 		{ this.clone	= amount; return;}
//		if (skill == "possessed") 	{ this.possessed= amount; return;}
//		if (skill == "ai") 			{ this.ai 		= amount; return;}
//		if (skill == "loyalty") 	{ this.loyalty	= amount; return;}
//		if (skill == "happiness") 	{ this.happiness= amount; return;}
//		
//		if (skill == "idealist") 	{ this.idealist	= amount; return;}
//		if (skill == "kind") 		{ this.kind		= amount; return;}
//		
//		if (skill == "leadership") 		{ this.leadership 	= amount; return;}
//		if (skill == "emissar") 		{ this.emissar		= amount; return;}
//		if (skill == "hr") 				{ this.hr			= amount; return;}
//		if (skill == "engineering") 	{ this.engineering	= amount; return;}
//		if (skill == "precognition") 	{ this.precognition	= amount; return;}
//		if (skill == "psy") 			{ this.psy			= amount; return;}
//		if (skill == "navigation") 		{ this.navigation	= amount; return;}
//		if (skill == "spaceBattle") 	{ this.spaceBattle	= amount; return;}
//		if (skill == "combat") 			{ this.combat		= amount; return;}
//		if (skill == "trading") 		{ this.trading		= amount; return;}
//		if (skill == "diplomat") 		{ this.diplomat		= amount; return;}
//		if (skill == "scientist") 		{ this.scientist	= amount; return;}
//		if (skill == "integrity") 		{ this.integrity	= amount; return;}
//		if (skill == "holiness") 		{ this.holiness		= amount; return;}
//		if (skill == "purity") 			{ this.purity		= amount; return;}
//		if (skill == "security") 		{ this.security		= amount; return;}
//		if (skill == "violent") 		{ this.violent		= amount; return;}
//		if (skill == "aristocrat") 		{ this.aristocrat	= amount; return;}
//		if (skill == "imperialist") 	{ this.imperialist	= amount; return;}
//		if (skill == "corruption") 		{ this.corruption	= amount; return;}
//		else Debug.LogError("Incorrect skill input for \"setStat()\"");
//		};		
//		
//	var getStat		= function (skill : String) : float { 
//		if (skill == "age") 		{ return this.age		;}
//		if (skill == "health") 		{ return this.health	;}
//		if (skill == "polluted") 	{ return this.polluted	;}
//		if (skill == "insanity") 	{ return this.insanity	;}
//		if (skill == "clone") 		{ return this.clone	;}
//		if (skill == "possessed") 	{ return this.possessed;}
//		if (skill == "ai") 			{ return this.ai 		;}
//		if (skill == "loyalty") 	{ return this.loyalty	;}
//		if (skill == "happiness") 	{ return this.happiness;}
//		
//		if (skill == "idealist") 	{ return this.idealist	;}
//		if (skill == "kind") 		{ return this.kind		;}
//		
//		if (skill == "leadership") 		{ return this.leadership 	;}
//		if (skill == "emissar") 		{ return this.emissar		;}
//		if (skill == "hr") 				{ return this.hr			;}
//		if (skill == "engineering") 	{ return this.engineering	;}
//		if (skill == "precognition") 	{ return this.precognition	;}
//		if (skill == "psy") 			{ return this.psy			;}
//		if (skill == "navigation") 		{ return this.navigation	;}
//		if (skill == "spaceBattle") 	{ return this.spaceBattle	;}
//		if (skill == "combat") 			{ return this.combat		;}
//		if (skill == "trading") 		{ return this.trading		;}
//		if (skill == "diplomat") 		{ return this.diplomat		;}
//		if (skill == "scientist") 		{ return this.scientist	;}
//		if (skill == "integrity") 		{ return this.integrity	;}
//		if (skill == "holiness") 		{ return this.holiness		;}
//		if (skill == "purity") 			{ return this.purity		;}
//		if (skill == "security") 		{ return this.security		;}
//		if (skill == "violent") 		{ return this.violent		;}
//		if (skill == "aristocrat") 		{ return this.aristocrat	;}
//		if (skill == "imperialist") 	{ return this.imperialist	;}
//		if (skill == "corruption") 		{ return this.corruption	;}
//		else Debug.LogError("Incorrect skill input for \"getStat()\"");
//		return;
//		};	
//
//	var skillLevel	= function (skill : String) : String { 
//		if (skill == "leadership") 	{ 
//										if (this.leadership >= 500) { return "Leadership: Epic" ;}
//										if (this.leadership >= 400) { return "Leadership: Legendary" ;}
//										if (this.leadership >= 350) { return "Leadership: Renown" ;}
//										if (this.leadership >= 300) { return "Leadership: Remarcable" ;}
//										if (this.leadership >= 250) { return "Leadership: Excellent" ;}
//										if (this.leadership >= 200) { return "Leadership: Very Good" ;}
//										if (this.leadership >= 150) { return "Leadership: Good" ;}
//										if (this.leadership >= 100) { return "Leadership: Trained" ;}
//										if (this.leadership <  100) { return null;}
//										return null;
//										}
//		/*if (skill == "emissar") 	{ 
//										if (this.emissar >= 500) { return "Emissar: Epic" ;}
//										if (this.emissar >= 400) { return "Emissar: Legendary" ;}
//										if (this.emissar >= 350) { return "Emissar: Formidable" ;}
//										if (this.emissar >= 300) { return "Emissar: Influential" ;}
//										if (this.emissar >= 250) { return "Emissar: Recognized" ;}
//										if (this.emissar >= 200) { return "Emissar: Skilled" ;}
//										if (this.emissar >= 150) { return "Emissar" ;}
//										if (this.emissar <  150) { return null;}
//										return null;
//									}*/
//		if (skill == "hr") 			{ 
//										if (this.hr >= 500) { return "Human Resources: Epic" ;}
//										if (this.hr >= 400) { return "Human Resources: Legendary" ;}
//										if (this.hr >= 350) { return "Human Resources: Exceptional" ;}
//										if (this.hr >= 300) { return "Human Resources: Remarcable" ;}
//										if (this.hr >= 250) { return "Human Resources: Excellent" ;}
//										if (this.hr >= 200) { return "Human Resources: Very Good" ;}
//										if (this.hr >= 150) { return "Human Resources: Good" ;}
//										if (this.hr >= 100) { return "Human Resources: Trained" ;}
//										if (this.hr <  100) { return null;}
//										return null;
//									}
//		if (skill == "engineering") { 
//										if (this.engineering >= 500) { return "Engineering: Epic" ;}
//										if (this.engineering >= 400) { return "Engineering: Legendary" ;}
//										if (this.engineering >= 350) { return "Engineering: Exceptional" ;}
//										if (this.engineering >= 300) { return "Engineering: Remarcable" ;}
//										if (this.engineering >= 250) { return "Engineering: Excellent" ;}
//										if (this.engineering >= 200) { return "Engineering: Very Good" ;}
//										if (this.engineering >= 150) { return "Engineering: Good" ;}
//										if (this.engineering >= 100) { return "Engineering: Trained" ;}
//										if (this.engineering <  100) { return null;}
//										return null;
//									}
//		if (skill == "precognition"){ 
//										if (this.precognition >= 500) { return "Precognition: Epic" ;}
//										if (this.precognition >= 400) { return "Precognition: Legendary" ;}
//										if (this.precognition >= 350) { return "Precognition: Exceptional" ;}
//										if (this.precognition >= 300) { return "Precognition: Remarcable" ;}
//										if (this.precognition >= 250) { return "Precognition: Good" ;}
//										if (this.precognition >= 200) { return "Precognition: Fair" ;}
//										if (this.precognition >= 150) { return "Precognition: Poor" ;}
//										if (this.precognition <  150) { return null;}
//										return null;
//									}
//		if (skill == "psy") 		{
//										if (this.psy >= 500) { return "Psycher: Epic" ;}
//										if (this.psy >= 400) { return "Psycher: Legendary" ;}
//										if (this.psy >= 350) { return "Psycher: Powerful" ;}
//										if (this.psy >= 300) { return "Psycher: Strong" ;}
//										if (this.psy >= 250) { return "Psycher" ;}
//										if (this.psy >= 200) { return "Psychic: Awoken" ;}
//										if (this.psy >= 150) { return "Psychic: Latent" ;}
//										if (this.psy <  150) { return null;}
//										return null;
//									}
//		if (skill == "navigation") 	{ 
//										if (this.navigation >= 500) { return "Navigator: Epic" ;}
//										if (this.navigation >= 400) { return "Navigator: Legendary" ;}
//										if (this.navigation >= 350) { return "Navigator: Renown" ;}
//										if (this.navigation >= 300) { return "Navigator: Experienced" ;}
//										if (this.navigation >= 250) { return "Navigator" ;}
//										if (this.navigation >= 200) { return "Navigation: Trained" ;}
//										if (this.navigation >= 150) { return "Navigation: Basic" ;}
//										if (this.navigation <  150) { return null;}
//										return null;
//									}
//		if (skill == "spaceBattle"){ 
//										if (this.spaceBattle >= 500) { return "Space Battle: Epic" ;}
//										if (this.spaceBattle >= 400) { return "Space Battle: Legendary" ;}
//										if (this.spaceBattle >= 350) { return "Space Battle: Inspirational" ;}
//										if (this.spaceBattle >= 300) { return "Space Battle: Outstanding" ;}
//										if (this.spaceBattle >= 250) { return "Space Battle: Excellent" ;}
//										if (this.spaceBattle >= 200) { return "Space Battle: Very Good" ;}
//										if (this.spaceBattle >= 150) { return "Space Battle: Good" ;}
//										if (this.spaceBattle <  150) { return null;}
//										return null;
//									}
//		if (skill == "combat") 		{ 
//										if (this.combat >= 500) { return "Combat: Epic" ;}
//										if (this.combat >= 400) { return "Combat: Legendary" ;}
//										if (this.combat >= 350) { return "Combat: Inspirational" ;}
//										if (this.combat >= 300) { return "Combat: Outstanding" ;}
//										if (this.combat >= 250) { return "Combat: Excellent" ;}
//										if (this.combat >= 200) { return "Combat: Very Good" ;}
//										if (this.combat >= 150) { return "Combat: Good" ;}
//										if (this.combat <  150) { return null;}
//										return null;
//									}
//		if (skill == "trading") 	{ 
//										if (this.trading >= 500) { return "Trading: Epic" ;}
//										if (this.trading >= 400) { return "Trading: Legendary" ;}
//										if (this.trading >= 350) { return "Trading: Exceptional" ;}
//										if (this.trading >= 300) { return "Trading: Remarcable" ;}
//										if (this.trading >= 250) { return "Trading: Excellent" ;}
//										if (this.trading >= 200) { return "Trading: Very Good" ;}
//										if (this.trading >= 150) { return "Trading: Good" ;}
//										if (this.trading <  150) { return null;}
//										return null;
//									}
//		if (skill == "diplomat") 	{ 
//										if (this.diplomat >= 400) { return "Diplomat" ;}
//										if (this.diplomat >= 250) { return "Negotiator" ;}
//										if (this.diplomat <  250) { return null;}
//										return null;
//									}
//		if (skill == "scientist") 	{ 
//										if (this.scientist >= 500) { return "Scientist: Epic" ;}
//										if (this.scientist >= 400) { return "Scientist: Legendary" ;}
//										if (this.scientist >= 350) { return "Scientist: Renown" ;}
//										if (this.scientist >= 300) { return "Scientist: Remarcable" ;}
//										if (this.scientist >= 250) { return "Scientist: Recognized" ;}
//										if (this.scientist >= 200) { return "Scientist" ;}
//										if (this.scientist <  200) { return null;}
//										return null;
//									}
//		if (skill == "integrity") 	{ 
//										if (this.integrity >= 300) { return "Benefactor" ;}
//										if (this.integrity >= 175) { return "Honest" ;}
//										if (this.integrity < -200) { return "Unscrupulous" ;}
//										if (this.integrity < -100) { return "Opportunist" ;}
//										return null;
//									}
//		if (skill == "holiness") 	{ 
//										if (this.holiness >= 500) { return "Saint" ;}
//										if (this.holiness >= 400) { return "Holy" ;}
//										if (this.holiness >= 300) { return "Revered" ;}
//										if (this.holiness >= 200) { return "Blessed" ;}
//										if (this.holiness >= 150) { return "Faithful" ;}
//										if (this.holiness < -200) { return "Heretic" ;}
//										if (this.holiness < -100) { return "Debauched" ;}
//										return null;
//									}
//		if (skill == "purity") 		{
//										if (this.purity >= 500) { return "Pure" ;}
//										if (this.purity < -100) { return "Mutant" ;}
//										if (this.purity <  -50) { return "Tainted" ;}
//										return null;
//									}
//		if (skill == "security") 	{ 
//										if (this.security >= 500) { return "Agent" ;}
//										if (this.security >= 300) { return "Secret Police" ;}
//										if (this.security >= 175) { return "Security Training" ;}
//										if (this.security < -150) { return "Anarchist" ;}
//										return null;
//									}
//		if (skill == "violent") 	{ 
//										if (this.violent >= 500) { return "Raving Mad" ;}
//										if (this.violent >= 400) { return "Berzerk" ;}
//										if (this.violent >= 300) { return "Hateful" ;}
//										if (this.violent >= 200) { return "Aggressive" ;}
//										if (this.violent < -300) { return "Pasifist" ;}
//										if (this.violent < -100) { return "Peaceful" ;}
//										return null;
//									}
//		if (skill == "aristocrat") 	{ 
//										if (this.aristocrat >= 200) { return "Aristocrat" ;}
//										return null;
//									}
//		if (skill == "imperialist") { 
//										if (this.imperialist >= 500) { return "Agent of the Empire" ;}
//										if (this.imperialist >= 200) { return "Imperialist" ;}
//										if (this.imperialist >= 100) { return "Imperialist" ;}
//										if (this.imperialist < -200) { return "Rebel" ;}
//										if (this.imperialist < -100) { return "Separatist" ;}
//										return null;
//									}
//		if (skill == "corruption") 	{ 
//										if (this.corruption >= 100) { return "Corrupted" ;}
//										return null;
//									}
//		Debug.Log("ERROR");
//		return null;
//	};
//
//}
//
//
//
//
//
//
//// ********************************** FUNCTIONS ****************************************
//
//function Awake () {
//	Instance = this;
//}
//
//function initializeCharacters () {
//
//	player = GameObject.FindGameObjectWithTag("Player");
//		finder = 	player.gameObject.GetComponent(LocationFinder);
//	
//	scriptHolder = GameObject.Find("ScriptHolder");
//		nameGenerator 	= scriptHolder.gameObject.GetComponent(NameGenerator);
//		collector 		= scriptHolder.gameObject.GetComponent(locationsDataCollector);
//		//ship 			= scriptHolder.gameObject.GetComponent(PlayerShipData);
//		//shipPop 		= scriptHolder.gameObject.GetComponent(ShipPopulation);
//		//economy 		= scriptHolder.gameObject.GetComponent(Economy);
//		event 			= scriptHolder.gameObject.GetComponent(Events);
//		//eventManager	= scriptHolder.gameObject.GetComponent(EventManager);	
//		
//		character = new Dictionary.<String, characterClass>();
//
//		// assign ideologies keys in dictionary
//		for (var a : String in WorldState.ideologyNames) {
//			ideologies.Add(a, 0);
//		}
//		// assign assignment keys in dictionary
//		for (a in WorldState.assignmentNames) {
//			assignment.Add(a, null);
//		}
//		// add all portraits to a list
//		//portraitList = new List.<String>();
//		portrait = gatherPortraitFileNames();
//		
//		select(null);
//
//		//character.Add("Testimies", new characterClass() );
//		//Debug.Log("leadership skill: "+character["Testimies"].leadership+" level: "+character["Testimies"].skillLevel("leadership"));
//		//character["Testimies"].addStat("leadership", 200);
//		//Debug.Log("leadership skill: "+character["Testimies"].leadership+" level: "+character["Testimies"].skillLevel("leadership"));
//		
//		assign(newCharacter("starting captain"), "captain");
//		assign(newCharacter("starting navigator"), "navigator");
//		assign(newCharacter("random"), "security");
//		assign(newCharacter("random"), "engineer");
//		assign(newCharacter("random"), "quartermaster");
//		assign(newCharacter("priest"), "psycher");
//		//newCharacter("brotherhood");
//		//newCharacter("navigator");
//
//}	
//
//function selected() : String {
//	return selectedCharacter;
//}
//function select(cha : String) {
//	selectedCharacter = cha;
//}
//
//
//
//function newCharacter (archtype : String) : String {
//	
//	characterName = "";
//	var home : GameObject;
//	affiliation = null;
//	ideology 	= null;
//	agenda 		= null;
//	
//	// setup archtype
//	archTypes(archtype);
//
//	// Origins
//		collector.locationsSortDistance();
//		var closelocation : locationDatabase;
//		closelocation	= finder.currentLocation;	// location refers to finder's location "gameobject"
//	
//		var roll : float;
//		var i : int = 0;
//		
//		while (true) {
//		
//			roll = Random.value;
//			if (roll < closelocation.ideologyStats["navigation"]/200+0.5 -0.3) {	// High navigation makes these people move around
//				roll = Mathf.Round(Random.value * 6);
//				home = collector.getClosestLocation(roll);
//				location = home.gameObject.GetComponent(locationDatabase);
//				if (location.visibleTo[0] > 0) { break; }
//			}
//			else { 
//				home = finder.currentLocation.gameobject; 			// Else get a local
//				location = home.gameObject.GetComponent(locationDatabase);
//				if (location.visibleTo[0] > 0) { break; }
//			}
//			
//			if (i > 100) { Debug.LogError("Can't find home planet"); break; }
//			i++;
//		}
//	
//	// character Name
//	// find a new unique name
//	var n : int = 0;
//	while (true) {
//		n++;
//		characterName = nameGenerator.getLocalName(home);		// sets affiliation if factions. else affiliation = null;
//		if (!character.ContainsKey(characterName)) { break; }
//		if (n > 100) { Debug.LogError("Unable to assign a new unique name to character -dictionary"); break; }
//	}
//	character.Add(characterName, new characterClass() );
//	character[characterName].name = characterName;
//	character[characterName].origins = home;
//	// Ideology
//	ideology = getIdeology(location, affiliation);		// gets faction ideology from faction/location
//	character[characterName].ideologies = ideologies;	// copy tmp ideology(dict) (made in getIdeology() ) to character
//	character[characterName].ideology = ideology;
//	character[characterName].affiliation = affiliation;
//		if (ideology == "navigators") { character[characterName].navigator = true; }
//		if (ideology == "brotherhood") { character[characterName].psycher = true; }
//		// base stats per ideology
//		setCharacterStats();						// calculates character stats from base skills
//	
//	// Rolls age & stats
//	rollNewStats();
//	
//	// portrait tag
//	character[characterName].portraitTag = portraitTag;
//	
//	// Debug stats
//	if (debugging) { 
//		//if (debugging) Debug.Log("NEW "+archtype+" character: "+character[characterName].name+", home: "+character[characterName].origins.name); 
//		// debug
//		//displayStats(); 
//	}
//	assignPortraits();	
//	return characterName;
//}
//
//function archTypes(archtypeIn : String) {
//	
//	var archtype = archtypeIn;
//	
//	// reset vars
//	experience	= 0;// default: 0 	exp range for normal = -10 - +30
//	age			= 0;  gender 		= null; health    	= 100;insanity 		= -1; clone 		= -1; possessed		= 0;  ai 			= -1; loyalty 		= -1; happiness 		= -1;   idealist 		= -1; kind 		= -1;
//	leadership 	= 0;  emissar		= 0;  hr			= 0;  engineering	= 0;  precognition	= 0;  psy			= 0;  navigation	= 0;  spaceBattle= 0;  combat		= 0;  trading		= 0;  diplomat	= 0;  scientist	= 0;  integrity	= 0;  holiness	= 0;  purity		= 0;  security	= 0;  violent		= 0;  aristocrat	= 0;  imperialist	= 0;  corruption	= 0;
//	navigator = false; psycher = false; HQclone = false; portraitTag = null;
//
//	// Archtypes	if (archtype == "captain") { leadership = 90; return; }
//	
//	// Captain
//	if (archtype == "starting captain") { age = 40; portraitTag = "captain"; /*HQclone = true;*/ insanity = 0; leadership = 90; loyalty = 100; return; }	
//	
//	// Navigator
//	if (archtype == "starting navigator") { ideology = "navigators"; portraitTag = "navigator"; age = 62; navigation = 50; navigator = true; gender = "probablyMale"; insanity = 10; HQclone = true; psycher = true; loyalty = 92; return; }	
//	if (archtype == "navigation expert"){ age = 40; navigation = 140; insanity = 0; experience = 10; loyalty = 92; return; }
//	if (archtype == "navigator") 		{ ideology = "navigators"; portraitTag = "navigator"; HQclone = true; age = Random.Range(50, 100);  experience = Mathf.Round(Random.Range(-10, 50)); navigator = true; HQclone = true; psycher = true; return; }	
//	
//	// Psycher
//	if (archtype == "brotherhood") 	{ ideology = "brotherhood"; psycher = true; insanity = Mathf.Pow(Random.Range(0, 3.16), 4); return; }	
//	if (archtype == "transhumanist psycher"){ ideology = "transhumanist"; psycher = true; insanity = Random.Range(0, 99); psy = 100; return; }
//	
//	// Priest
//	if (archtype == "priest") 	{ ideology = "cult"; portraitTag = "priest"; return; }		
//
//	// Random
//	if (archtype == "random" || archtype == "") { experience = Mathf.Round(Random.Range(-10, 25)); gender = "probablyMale"; }
//	
//	else Debug.LogError("Wrong archtype entered for New Character "+characterName);
//}
//
//function rollNewStats () {
//	
//	var roll : float = Random.value;
//		
//		if (gender != null) { if (gender == "male" || gender == "female") character[characterName].gender = gender; if (gender == "random") { if (Random.value < 0.5) character[characterName].gender = "male"; else character[characterName].gender = "female"; } if (gender == "probablyMale") { if (Random.value < 0.9) character[characterName].gender = "male"; else character[characterName].gender = "female"; } if (gender == "probablyFemale") { if (Random.value < 0.1) character[characterName].gender = "male"; else character[characterName].gender = "female"; } if (character[characterName].gender == null) /*else direct input*/  character[characterName].gender = gender;}
//		if (gender == null) { character[characterName].gender = "male"; }
//		if (age == 0) 		{ character[characterName].age = Random.Range(24+(Random.value*experience), 64+(Random.value*experience/2));  } else { character[characterName].age = age+(Random.value*experience); } 
//			// experience adjust from age
//		if (experience == 0) { experience = Mathf.Round(Mathf.Pow(Random.value,2)*(character[characterName].age-24)); /*if (experience < 0) experience = 0;*/ }
//		character[characterName].health = health;
//		if (insanity == -1) 	{ character[characterName].insanity = Mathf.Pow(Random.Range(0, 2.515), 5); if (insanity < 30) character[characterName].insanity = 0; } else { character[characterName].insanity = insanity; } 
//		if (clone == -1) { if (Random.value < 0.2) { clone = Mathf.Pow(Random.value*10, 2); } if (Random.value < 0.05) { HQclone = true; } } else { character[characterName].clone = clone; } 
//		character[characterName].HQclone = HQclone;
//		if (possessed == -1) { character[characterName].possessed = Mathf.Pow(Random.Range(0, 2.515), 5);  } else { character[characterName].clone = clone; } 
//		character[characterName].ai = ai;
//		if (loyalty == -1) 	{ if (roll < integrity/2+50) { character[characterName].loyalty = Random.Range(40, 70);  } else { character[characterName].loyalty = Random.Range(0, 70);  } } else {character[characterName].loyalty = loyalty; } 
//		if (happiness == -1) 	{ character[characterName].happiness = 50; } else {character[characterName].happiness = happiness; } 
//		if (idealist == -1) character[characterName].idealist 	= Random.value*100; /* { if (Random.value < 0.5) { character[characterName].idealist 	= Mathf.Sqrt(Random.value)*50; }  else { character[characterName].idealist 	= Mathf.Pow(Random.value,2) *50 +50; } } */ else character[characterName].idealist = idealist;
//		if (kind == -1) 	character[characterName].kind 		= Random.value*100; /* { if (Random.value < 0.5) { character[characterName].kind 		= Mathf.Sqrt(Random.value)*50; }  else { character[characterName].kind 		= Mathf.Pow(Random.value,2) *50 +50; } } */ else character[characterName].kind = kind;
//		
//		// set up psycher and navigator statuses
//		if (character[characterName].ideology == "brotherhood" || (character[characterName].ideology == "navigators" && character[characterName].psy > 150)) { character[characterName].psycher = true; } else character[characterName].psycher = false;
//		if (character[characterName].ideology == "navigators" || character[characterName].navigation >= 200) { character[characterName].navigator = true; } else character[characterName].navigator = false;
//		
//		
//		
//		
//		roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//		
//			character[characterName].leadership += characterStats.diplomacy + characterStats.morale + characterStats.military;
//			if (leadership == 0) { character[characterName].leadership += Roll();  } else { character[characterName].leadership += leadership +Random.value;  } 
//
//		/*//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].emissar += characterStats.diplomacy + characterStats.morale;
//			if (emissar == 0) { character[characterName].emissar += Roll();  } else { character[characterName].emissar += emissar +Random.value;  } 
//		*/
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].hr += characterStats.pgrowth + characterStats.innovation/2 + characterStats.economy/2 + characterStats.happiness/2;
//			if (hr == 0) { character[characterName].hr += Roll();  } else { character[characterName].hr += hr +Random.value;  } 
//
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].engineering += characterStats.industry/2 + characterStats.innovation + characterStats.military/2;
//			if (engineering == 0) { character[characterName].engineering += Roll();  } else { character[characterName].engineering += engineering +Random.value;  } 
//
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].precognition += characterStats.psych + characterStats.military/2 + characterStats.diplomacy/2;
//			if (precognition == 0) { character[characterName].precognition += Roll();  } else { character[characterName].precognition += precognition +Random.value;  } 
//
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].psy += characterStats.psych;
//			if (psy == 0) { character[characterName].psy += Roll();  } else { character[characterName].psy += psy +Random.value;  } 
//
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].navigation += characterStats.navigation; 
//			if (navigation == 0) { character[characterName].navigation += Roll();  } else { character[characterName].navigation += navigation +Random.value; } 
//
//		roll = Random.value*100; if (character[characterName].age > 58) { roll += (Random.value*experience-((character[characterName].age-58)*2.5)); } else roll += Random.value*experience;
//
//			character[characterName].spaceBattle += characterStats.diplomacy + characterStats.morale + characterStats.military + /*characterStats.industry +*/ characterStats.innovation + characterStats.military;
//			if (spaceBattle == 0) { character[characterName].spaceBattle += roll;  } else { character[characterName].spaceBattle += spaceBattle +Random.value;  } 
//
//		roll = Random.value*100; if (character[characterName].age > 50) { roll += (Random.value*experience-((character[characterName].age-50)*7.5)); } else roll += Random.value*experience;
//
//			character[characterName].combat += characterStats.diplomacy + characterStats.morale + characterStats.military + characterStats.morale;
//			if (combat == 0) { character[characterName].combat += roll;  } else { character[characterName].combat += combat +Random.value;  } 
//
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].trading += characterStats.economy/2 + characterStats.diplomacy/2;
//			if (trading == 0) { character[characterName].trading += Roll(); } else { character[characterName].trading += trading +Random.value;  } 
//
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].diplomat += characterStats.diplomacy;
//			if (diplomat == 0) { character[characterName].diplomat += Roll();  } else { character[characterName].diplomat += diplomat +Random.value;  } 
//
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].scientist += characterStats.innovation;
//			if (scientist == 0) { character[characterName].scientist += Roll();  } else { character[characterName].scientist += scientist +Random.value;  } 
//
//		roll = Random.value*200 -100;
//
//			character[characterName].integrity += characterStats.holy + characterStats.altruism;
//			if (integrity == 0) { character[characterName].integrity += roll;  } else { character[characterName].integrity += integrity +Random.value;  } 
//
//		roll = Random.value*200 -100;
//
//			character[characterName].holiness += characterStats.holy;
//			if (holiness == 0) { character[characterName].holiness += roll;  } else { character[characterName].holiness += holiness +Random.value;  } 
//
//		roll = Random.value*100; if (experience != 0) { roll -= Random.value*experience; }
//
//			character[characterName].purity += characterStats.purity;
//			if (purity == 0) { character[characterName].purity += roll;  } else { character[characterName].purity += purity +Random.value;  } 
//			if (HQclone && ideology != "navigators") { character[characterName].purity /= 3; }
//
//		//roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			character[characterName].security += characterStats.police;
//			if (security == 0) { character[characterName].security += Roll();  } else { character[characterName].security += security +Random.value;  } 
//
//		roll = Random.value*200 -100;
//
//			character[characterName].violent += characterStats.violent;
//			if (violent == 0) { character[characterName].violent += roll;  } else { character[characterName].violent += violent +Random.value;  } 
//
//			character[characterName].aristocrat += characterStats.aristocracy; 
//
//			character[characterName].imperialist += characterStats.imperialism;
//
//		roll = Random.value*100; if (experience != 0) { roll += Random.value*experience; }
//
//			if (corruption == 0) { character[characterName].corruption += Mathf.Pow(roll/100, 20)*character[characterName].psy;  } else { character[characterName].corruption += corruption +Random.value;  } 
//
//		if (character[characterName].corruption > 100) { character[characterName].corruption = 100; }
//		
//
//		// set negatives to 0?
//}
//
//function Roll() : float {
//	if (Random.value < 0.5) return Mathf.Pow(Random.value,3) *(200 +experience);
//	else return Mathf.Pow(Random.value,3) *(-200 +experience);
//}
//
//
//
//function getIdeology(homeLoc : locationDatabase, affil : String) : String {
//	
//	var roll : float = Random.value*100;
//	var adder : float = 0;
//	
//		ideologies["cult"] 			= 0;
//		ideologies["technocrat"] 	= 0;
//		ideologies["mercantile"] 	= 0;
//		ideologies["bureaucracy"] 	= 0;
//		ideologies["liberal"] 		= 0;
//		ideologies["nationalist"] 	= 0;
//		ideologies["aristocrat"] 	= 0;
//		ideologies["imperialist"] 	= 0;
//		ideologies["navigators"] 	= 0;
//		ideologies["brotherhood"] 	= 0;
//		ideologies["transhumanist"] = 0;
//		
//	// if already ideology, return here	
//	if (ideology != null) {
//		ideologies[ideology] = 100; 
//		return ideology;
//	}
//	
//	if (affil == "Church") {
//		ideologies["cult"] 			= 100;
//		return "cult";
//	}
//	if (affil == "Heretics") {
//		ideologies["transhumanist"] = 100;
//		return "transhumanist";
//	}	
//	
//	// people are one ideology 100% (is this good approach?) Odds are per *faction* representation on the location ideology / if not affiliated it's by base population 
//	if (affil == "House Furia" || affil == "House Rathmund" || affil == "House Tarquinia" || affil == "House Valeria") {	// by faction ideology
//			adder += location.nobleHousesIdeology["cult"];
//				if (roll <= adder) { ideologies["cult"] 		= 100; return "cult"; }
//			adder += location.nobleHousesIdeology["technocrat"];
//				if (roll <= adder) { ideologies["technocrat"] 	= 100; return "technocrat"; }
//			adder += location.nobleHousesIdeology["mercantile"];
//				if (roll <= adder) { ideologies["mercantile"] 	= 100; return "mercantile"; }
//			adder += location.nobleHousesIdeology["bureaucracy"];
//				if (roll <= adder) { ideologies["bureaucracy"] 	= 100; return "bureaucracy"; }
//			adder += location.nobleHousesIdeology["liberal"];
//				if (roll <= adder) { ideologies["liberal"] 		= 100; return "liberal"; }
//			adder += location.nobleHousesIdeology["nationalist"];
//				if (roll <= adder) { ideologies["nationalist"] 	= 100; return "nationalist"; }
//			adder += location.nobleHousesIdeology["aristocrat"];
//				if (roll <= adder) { ideologies["aristocrat"] 	= 100; return "aristocrat"; }
//			adder += location.nobleHousesIdeology["imperialist"];
//				if (roll <= adder) { ideologies["imperialist"] 	= 100; return "imperialist"; }
//			adder += location.nobleHousesIdeology["navigators"];
//				if (roll <= adder) { ideologies["navigators"] 	= 100; return "navigators"; }
//			adder += location.nobleHousesIdeology["brotherhood"];
//				if (roll <= adder) { ideologies["brotherhood"] 	= 100; return "brotherhood"; }
//			ideologies["transhumanist"] = 100; return "transhumanist";
//	}
//	
//	if (affil == "Everlasting Union" || affil == "Dacei Family" || affil == "Coruna Cartel") {		// by faction ideology
//			adder += location.guildsIdeology["cult"];
//				if (roll <= adder) { ideologies["cult"] 		= 100; return "cult"; }
//			adder += location.guildsIdeology["technocrat"];
//				if (roll <= adder) { ideologies["technocrat"] 	= 100; return "technocrat"; }
//			adder += location.guildsIdeology["mercantile"];
//				if (roll <= adder) { ideologies["mercantile"] 	= 100; return "mercantile"; }
//			adder += location.guildsIdeology["bureaucracy"];
//				if (roll <= adder) { ideologies["bureaucracy"] 	= 100; return "bureaucracy"; }
//			adder += location.guildsIdeology["liberal"];
//				if (roll <= adder) { ideologies["liberal"] 		= 100; return "liberal"; }
//			adder += location.guildsIdeology["nationalist"];
//				if (roll <= adder) { ideologies["nationalist"] 	= 100; return "nationalist"; }
//			adder += location.guildsIdeology["aristocrat"];
//				if (roll <= adder) { ideologies["aristocrat"] 	= 100; return "aristocrat"; }
//			adder += location.guildsIdeology["imperialist"];
//				if (roll <= adder) { ideologies["imperialist"] 	= 100; return "imperialist"; }
//			adder += location.guildsIdeology["navigators"];
//				if (roll <= adder) { ideologies["navigators"] 	= 100; return "navigators"; }
//			adder += location.guildsIdeology["brotherhood"];
//				if (roll <= adder) { ideologies["brotherhood"] 	= 100; return "brotherhood"; }
//			ideologies["transhumanist"] = 100; return "transhumanist";
//	}
//	else {													// by location population factions
//			adder += location.ideologies["cult"];
//				if (roll <= adder) { ideologies["cult"] 		= 100; return "cult"; }
//			adder += location.ideologies["technocrat"];
//				if (roll <= adder) { ideologies["technocrat"] 	= 100; return "technocrat"; }
//			adder += location.ideologies["mercantile"];
//				if (roll <= adder) { ideologies["mercantile"] 	= 100; return "mercantile"; }
//			adder += location.ideologies["bureaucracy"];
//				if (roll <= adder) { ideologies["bureaucracy"] 	= 100; return "bureaucracy"; }
//			adder += location.ideologies["liberal"];
//				if (roll <= adder) { ideologies["liberal"] 		= 100; return "liberal"; }
//			adder += location.ideologies["nationalist"];
//				if (roll <= adder) { ideologies["nationalist"] 	= 100; return "nationalist"; }
//			adder += location.ideologies["aristocrat"];
//				if (roll <= adder) { ideologies["aristocrat"] 	= 100; return "aristocrat"; }
//			adder += location.ideologies["imperialist"];
//				if (roll <= adder) { ideologies["imperialist"] 	= 100; return "imperialist"; }
//			adder += location.ideologies["navigators"];
//				if (roll <= adder) { ideologies["navigators"] 	= 100; return "navigators"; }
//			adder += location.ideologies["brotherhood"];
//				if (roll <= adder) { ideologies["brotherhood"] 	= 100; return "brotherhood"; }
//			ideologies["transhumanist"] = 100; return "transhumanist";
//	}
//	
//}
//
//function setCharacterStats() {
//	
//
//		characterStats.pgrowth 		= (ideologies["cult"] *  100 + ideologies["technocrat"] *  -60 + ideologies["mercantile"] * -100 + ideologies["bureaucracy"] *   60 + ideologies["liberal"] * -100 + ideologies["nationalist"] *   80 + ideologies["aristocrat"] *   70 + ideologies["imperialist"] *   20 + ideologies["navigators"] * -300 + ideologies["brotherhood"] *  -70 + ideologies["transhumanist"] *   60 ) /100;
//		characterStats.industry 	= (ideologies["cult"] *   20 + ideologies["technocrat"] *  100 + ideologies["mercantile"] *   30 + ideologies["bureaucracy"] *   80 + ideologies["liberal"] *  -40 + ideologies["nationalist"] *   80 + ideologies["aristocrat"] *   20 + ideologies["imperialist"] *  -10 + ideologies["navigators"] *  -50 + ideologies["brotherhood"] * -100 + ideologies["transhumanist"] *    0 ) /100;
//		characterStats.economy 		= (ideologies["cult"] * -100 + ideologies["technocrat"] *   30 + ideologies["mercantile"] *  100 + ideologies["bureaucracy"] *   60 + ideologies["liberal"] *  -30 + ideologies["nationalist"] *  -10 + ideologies["aristocrat"] *   40 + ideologies["imperialist"] *  -10 + ideologies["navigators"] *   50 + ideologies["brotherhood"] *    0 + ideologies["transhumanist"] *  -20 ) /100;
//		characterStats.diplomacy 	= (ideologies["cult"] * -100 + ideologies["technocrat"] *    0 + ideologies["mercantile"] *  200 + ideologies["bureaucracy"] *   10 + ideologies["liberal"] *  200 + ideologies["nationalist"] * -100 + ideologies["aristocrat"] *  -20 + ideologies["imperialist"] *   20 + ideologies["navigators"] *  100 + ideologies["brotherhood"] *   20 + ideologies["transhumanist"] *  150 ) /100;
//		characterStats.happiness 	= (ideologies["cult"] *  -30 + ideologies["technocrat"] *   20 + ideologies["mercantile"] *   70 + ideologies["bureaucracy"] *    0 + ideologies["liberal"] *  100 + ideologies["nationalist"] *   30 + ideologies["aristocrat"] *  -50 + ideologies["imperialist"] *  -20 + ideologies["navigators"] *   20 + ideologies["brotherhood"] *   10 + ideologies["transhumanist"] *  100 ) /100;
//		characterStats.affluence 	= (ideologies["cult"] *  -70 + ideologies["technocrat"] *   30 + ideologies["mercantile"] *  100 + ideologies["bureaucracy"] *   10 + ideologies["liberal"] *   60 + ideologies["nationalist"] *  -30 + ideologies["aristocrat"] *   30 + ideologies["imperialist"] *    0 + ideologies["navigators"] *   60 + ideologies["brotherhood"] *    0 + ideologies["transhumanist"] *  -10 ) /100;
//		characterStats.innovation 	= (ideologies["cult"] * -100 + ideologies["technocrat"] *   70 + ideologies["mercantile"] *   20 + ideologies["bureaucracy"] *  -10 + ideologies["liberal"] *  100 + ideologies["nationalist"] *  -20 + ideologies["aristocrat"] *  -30 + ideologies["imperialist"] *  -20 + ideologies["navigators"] *   70 + ideologies["brotherhood"] *   50 + ideologies["transhumanist"] *  100 ) /100;
//		characterStats.morale 		= (ideologies["cult"] *  100 + ideologies["technocrat"] *  -20 + ideologies["mercantile"] *  -90 + ideologies["bureaucracy"] *  -20 + ideologies["liberal"] * -100 + ideologies["nationalist"] *   80 + ideologies["aristocrat"] *   50 + ideologies["imperialist"] *   30 + ideologies["navigators"] *  -20 + ideologies["brotherhood"] *  -10 + ideologies["transhumanist"] * -100 ) /100;
//		characterStats.altruism 	= (ideologies["cult"] *   80 + ideologies["technocrat"] *   20 + ideologies["mercantile"] * -100 + ideologies["bureaucracy"] *  -60 + ideologies["liberal"] *  100 + ideologies["nationalist"] *   20 + ideologies["aristocrat"] *  -50 + ideologies["imperialist"] *  -20 + ideologies["navigators"] *  -30 + ideologies["brotherhood"] *  -10 + ideologies["transhumanist"] *    0 ) /100;
//		characterStats.military 	= (ideologies["cult"] *  100 + ideologies["technocrat"] *   20 + ideologies["mercantile"] *  -60 + ideologies["bureaucracy"] *   10 + ideologies["liberal"] * -100 + ideologies["nationalist"] *  100 + ideologies["aristocrat"] *   80 + ideologies["imperialist"] *   40 + ideologies["navigators"] *   20 + ideologies["brotherhood"] *    0 + ideologies["transhumanist"] *  -50 ) /100;
//		
//		characterStats.holy			= (ideologies["cult"] *  100 + ideologies["technocrat"] *  -20 + ideologies["mercantile"] *  -40 + ideologies["bureaucracy"] *  -10 + ideologies["liberal"] *  -60 + ideologies["nationalist"] *    0 + ideologies["aristocrat"] *  -30 + ideologies["imperialist"] *   60 + ideologies["navigators"] *    0 + ideologies["brotherhood"] *   10 + ideologies["transhumanist"] * -100 ) /100;
//		characterStats.psych 		= (ideologies["cult"] *  -70 + ideologies["technocrat"] * -100 + ideologies["mercantile"] *   20 + ideologies["bureaucracy"] *  -20 + ideologies["liberal"] *   40 + ideologies["nationalist"] *  -50 + ideologies["aristocrat"] *   30 + ideologies["imperialist"] *   20 + ideologies["navigators"] *  100 + ideologies["brotherhood"] *  200 + ideologies["transhumanist"] *   60 ) /100;
//		characterStats.navigation 	= (ideologies["cult"] *  -40 + ideologies["technocrat"] *   70 + ideologies["mercantile"] *  100 + ideologies["bureaucracy"] *  -10 + ideologies["liberal"] *   20 + ideologies["nationalist"] * -100 + ideologies["aristocrat"] *   10 + ideologies["imperialist"] *   30 + ideologies["navigators"] *  200 + ideologies["brotherhood"] *   60 + ideologies["transhumanist"] *   40 ) /100;
//		characterStats.purity 		= (ideologies["cult"] *  100 + ideologies["technocrat"] *  -50 + ideologies["mercantile"] *  -60 + ideologies["bureaucracy"] *   10 + ideologies["liberal"] *  -60 + ideologies["nationalist"] *   40 + ideologies["aristocrat"] *  -30 + ideologies["imperialist"] *   20 + ideologies["navigators"] * -100 + ideologies["brotherhood"] *  -30 + ideologies["transhumanist"] * -100 ) /100;
//		
//		characterStats.police 		= (ideologies["cult"] *  100 + ideologies["technocrat"] *  -20 + ideologies["mercantile"] *  -60 + ideologies["bureaucracy"] *   40 + ideologies["liberal"] * -100 + ideologies["nationalist"] *   70 + ideologies["aristocrat"] *   80 + ideologies["imperialist"] *   30 + ideologies["navigators"] *  -20 + ideologies["brotherhood"] *  -40 + ideologies["transhumanist"] * -200 ) /100;
//		characterStats.violent 		= (ideologies["cult"] *  100 + ideologies["technocrat"] *  -20 + ideologies["mercantile"] *  -70 + ideologies["bureaucracy"] *    0 + ideologies["liberal"] * -100 + ideologies["nationalist"] *  100 + ideologies["aristocrat"] *   60 + ideologies["imperialist"] *   30 + ideologies["navigators"] *  -50 + ideologies["brotherhood"] *  -60 + ideologies["transhumanist"] *   20 ) /100;
//		characterStats.aristocracy 	= (ideologies["cult"] *  -30 + ideologies["technocrat"] *   10 + ideologies["mercantile"] *  100 + ideologies["bureaucracy"] *   30 + ideologies["liberal"] *  -70 + ideologies["nationalist"] *  -30 + ideologies["aristocrat"] *  100 + ideologies["imperialist"] *   50 + ideologies["navigators"] *  100 + ideologies["brotherhood"] *   70 + ideologies["transhumanist"] * -100 ) /100;
//		characterStats.imperialism 	= (ideologies["cult"] *   80 + ideologies["technocrat"] *  -10 + ideologies["mercantile"] *  -20 + ideologies["bureaucracy"] *    5 + ideologies["liberal"] *  -50 + ideologies["nationalist"] * -100 + ideologies["aristocrat"] *   50 + ideologies["imperialist"] *  100 + ideologies["navigators"] *   10 + ideologies["brotherhood"] *    5 + ideologies["transhumanist"] * -100 ) /100;
//
//}
//
//function displayStats() {
//	if (character[characterName].affiliation != null) { Debug.Log("Affiliation: "+character[characterName].affiliation); }
//	Debug.Log("Ideology: "+character[characterName].ideology);
//	Debug.Log("Age: "+Mathf.Round(character[characterName].age*10)/10);
//
//	if (character[characterName].skillLevel("leadership"  ) != null) { Debug.Log(character[characterName].skillLevel("leadership"  )); }
//	//if (character[characterName].skillLevel("emissar"     ) != null) { Debug.Log(character[characterName].skillLevel("emissar"     )); }
//	if (character[characterName].skillLevel("hr"          ) != null) { Debug.Log(character[characterName].skillLevel("hr"          )); }
//	if (character[characterName].skillLevel("engineering" ) != null) { Debug.Log(character[characterName].skillLevel("engineering" )); }
//	if (character[characterName].skillLevel("precognition") != null) { Debug.Log(character[characterName].skillLevel("precognition")); }
//	if (character[characterName].skillLevel("psy"         ) != null) { Debug.Log(character[characterName].skillLevel("psy"         )); }
//	if (character[characterName].skillLevel("navigation"  ) != null) { Debug.Log(character[characterName].skillLevel("navigation"  )); }
//	if (character[characterName].skillLevel("spaceBattle" ) != null) { Debug.Log(character[characterName].skillLevel("spaceBattle" )); }
//	if (character[characterName].skillLevel("combat"      ) != null) { Debug.Log(character[characterName].skillLevel("combat"      )); }
//	if (character[characterName].skillLevel("trading"     ) != null) { Debug.Log(character[characterName].skillLevel("trading"     )); }
//	if (character[characterName].skillLevel("diplomat"    ) != null) { Debug.Log(character[characterName].skillLevel("diplomat"    )); }
//	if (character[characterName].skillLevel("scientist"   ) != null) { Debug.Log(character[characterName].skillLevel("scientist"   )); }
//	if (character[characterName].skillLevel("integrity"   ) != null) { Debug.Log(character[characterName].skillLevel("integrity"   )); }
//	if (character[characterName].skillLevel("holiness"    ) != null) { Debug.Log(character[characterName].skillLevel("holiness"    )); }
//	if (character[characterName].skillLevel("purity"      ) != null) { Debug.Log(character[characterName].skillLevel("purity"      )); }
//	if (character[characterName].skillLevel("security"    ) != null) { Debug.Log(character[characterName].skillLevel("security"    )); }
//	if (character[characterName].skillLevel("violent"     ) != null) { Debug.Log(character[characterName].skillLevel("violent"     )); }
//	if (character[characterName].skillLevel("aristocrat"  ) != null) { Debug.Log(character[characterName].skillLevel("aristocrat"  )); }
//	if (character[characterName].skillLevel("imperialist" ) != null) { Debug.Log(character[characterName].skillLevel("imperialist" )); }
//	if (character[characterName].skillLevel("corruption"  ) != null) { Debug.Log(character[characterName].skillLevel("corruption"  )); }
//	
//	//Debug.Log("Idealist / Cynical: "+character[characterName].idealist);
//	//Debug.Log("Kind / Cold : "+character[characterName].kind);
//}
//
//
//
//// ******************************** CHARACTER MANAGEMENT TOOLS **************************************************************************
//
//// get character by skill from those assigned 
//
//function getBest(skill : String) : String {
//	
//	//var s : Object = skill as Object;
//	
//	var name 	: String 	= "";
//	var val 	: float 	= -1000;
//	
//	while (true){
//		if (skill == "leadership")  { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.leadership   > val && person.Value.assignment != null) { val = person.Value.leadership  ; name = person.Key; }} break; }
//		if (skill == "emissar")     { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.emissar      > val && person.Value.assignment != null) { val = person.Value.emissar     ; name = person.Key; }} break; }
//		if (skill == "hr")          { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.hr           > val && person.Value.assignment != null) { val = person.Value.hr          ; name = person.Key; }} break; }
//		if (skill == "engineering") { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.engineering  > val && person.Value.assignment != null) { val = person.Value.engineering ; name = person.Key; }} break; }
//		if (skill == "precognition"){ for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.precognition > val && person.Value.assignment != null) { val = person.Value.precognition; name = person.Key; }} break; }
//		if (skill == "psy")         { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.psy          > val && person.Value.assignment != null) { val = person.Value.psy         ; name = person.Key; }} break; }
//		if (skill == "navigation")  { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.navigation   > val && person.Value.assignment != null) { val = person.Value.navigation  ; name = person.Key; }} break; }
//		if (skill == "spaceBattle") { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.spaceBattle  > val && person.Value.assignment != null) { val = person.Value.spaceBattle ; name = person.Key; }} break; }
//		if (skill == "combat")      { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.combat       > val && person.Value.assignment != null) { val = person.Value.combat      ; name = person.Key; }} break; }
//		if (skill == "trading")     { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.trading      > val && person.Value.assignment != null) { val = person.Value.trading     ; name = person.Key; }} break; }
//		if (skill == "diplomat")    { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.diplomat     > val && person.Value.assignment != null) { val = person.Value.diplomat    ; name = person.Key; }} break; }
//		if (skill == "scientist")   { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.scientist    > val && person.Value.assignment != null) { val = person.Value.scientist   ; name = person.Key; }} break; }
//		if (skill == "integrity")   { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.integrity    > val && person.Value.assignment != null) { val = person.Value.integrity   ; name = person.Key; }} break; }
//		if (skill == "holiness")    { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.holiness     > val && person.Value.assignment != null) { val = person.Value.holiness    ; name = person.Key; }} break; }
//		if (skill == "purity")      { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.purity       > val && person.Value.assignment != null) { val = person.Value.purity      ; name = person.Key; }} break; }
//		if (skill == "security")    { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.security     > val && person.Value.assignment != null) { val = person.Value.security    ; name = person.Key; }} break; }
//		if (skill == "violent")     { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.violent      > val && person.Value.assignment != null) { val = person.Value.violent     ; name = person.Key; }} break; }
//		if (skill == "aristocrat")  { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.aristocrat   > val && person.Value.assignment != null) { val = person.Value.aristocrat  ; name = person.Key; }} break; }
//		if (skill == "imperialist") { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.imperialist  > val && person.Value.assignment != null) { val = person.Value.imperialist ; name = person.Key; }} break; }
//		if (skill == "corruption")  { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.corruption   > val && person.Value.assignment != null) { val = person.Value.corruption  ; name = person.Key; }} break; }	
//		break;
//	}
//
//	if (val != -1000) { return name; } else Debug.LogError("ERROR: While picking character with best "+skill+", none was found.");
//	return null;
//}
//
//// get character by skill
//
//function getBestFromAll(skill : String) : String {
//	
//	//var s : Object = skill as Object;
//	
//	var name 	: String 	= "";
//	var val 	: float 	= -1000;
//	
//	if (skill == "leadership")  { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.leadership   > val) { val = person.Value.leadership  ; name = person.Key; }} }
//	if (skill == "emissar")     { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.emissar      > val) { val = person.Value.emissar     ; name = person.Key; }} }
//	if (skill == "hr")          { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.hr           > val) { val = person.Value.hr          ; name = person.Key; }} }
//	if (skill == "engineering") { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.engineering  > val) { val = person.Value.engineering ; name = person.Key; }} }
//	if (skill == "precognition"){ for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.precognition > val) { val = person.Value.precognition; name = person.Key; }} }
//	if (skill == "psy")         { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.psy          > val) { val = person.Value.psy         ; name = person.Key; }} }
//	if (skill == "navigation")  { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.navigation   > val) { val = person.Value.navigation  ; name = person.Key; }} }
//	if (skill == "spaceBattle") { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.spaceBattle  > val) { val = person.Value.spaceBattle ; name = person.Key; }} }
//	if (skill == "combat")      { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.combat       > val) { val = person.Value.combat      ; name = person.Key; }} }
//	if (skill == "trading")     { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.trading      > val) { val = person.Value.trading     ; name = person.Key; }} }
//	if (skill == "diplomat")    { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.diplomat     > val) { val = person.Value.diplomat    ; name = person.Key; }} }
//	if (skill == "scientist")   { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.scientist    > val) { val = person.Value.scientist   ; name = person.Key; }} }
//	if (skill == "integrity")   { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.integrity    > val) { val = person.Value.integrity   ; name = person.Key; }} }
//	if (skill == "holiness")    { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.holiness     > val) { val = person.Value.holiness    ; name = person.Key; }} }
//	if (skill == "purity")      { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.purity       > val) { val = person.Value.purity      ; name = person.Key; }} }
//	if (skill == "security")    { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.security     > val) { val = person.Value.security    ; name = person.Key; }} }
//	if (skill == "violent")     { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.violent      > val) { val = person.Value.violent     ; name = person.Key; }} }
//	if (skill == "aristocrat")  { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.aristocrat   > val) { val = person.Value.aristocrat  ; name = person.Key; }} }
//	if (skill == "imperialist") { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.imperialist  > val) { val = person.Value.imperialist ; name = person.Key; }} }
//	if (skill == "corruption")  { for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.corruption   > val) { val = person.Value.corruption  ; name = person.Key; }} }	
//	
//	if (val != -1000) { return name; } else Debug.LogError("ERROR: While picking character with best "+skill+", none was found.");
//	return null;
//}
//
//function statRoll (skill : String) {
//	isProxy = false;
//
//	if (event.Event[event.n].character != "empty") {
//
//		if (skill == "leadership")  { return character[event.Event[event.n].character].leadership   + roll; }
//		if (skill == "emissar")     { return character[event.Event[event.n].character].emissar      + roll; }
//		if (skill == "hr")          { return character[event.Event[event.n].character].hr           + roll; }
//		if (skill == "engineering") { return character[event.Event[event.n].character].engineering  + roll; }
//		if (skill == "precognition"){ return character[event.Event[event.n].character].precognition + roll; }
//		if (skill == "psy")         { return character[event.Event[event.n].character].psy          + roll; }
//		if (skill == "navigation")  { return character[event.Event[event.n].character].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[event.Event[event.n].character].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[event.Event[event.n].character].combat       + roll; }
//		if (skill == "trading")     { return character[event.Event[event.n].character].trading      + roll; }
//		if (skill == "diplomat")    { return character[event.Event[event.n].character].diplomat     + roll; }
//		if (skill == "scientist")   { return character[event.Event[event.n].character].scientist    + roll; }
//		if (skill == "integrity")   { return character[event.Event[event.n].character].integrity    + roll; }
//		if (skill == "holiness")    { return character[event.Event[event.n].character].holiness     + roll; }
//		if (skill == "purity")      { return character[event.Event[event.n].character].purity       + roll; }
//		if (skill == "security")    { return character[event.Event[event.n].character].security     + roll; }
//		if (skill == "violent")     { return character[event.Event[event.n].character].violent      + roll; }
//		if (skill == "aristocrat")  { return character[event.Event[event.n].character].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[event.Event[event.n].character].imperialist  + roll; }
//		if (skill == "corruption")  { return character[event.Event[event.n].character].corruption   + roll; }
//		return 0;
//	}
//	else {
//		Debug.LogError("ERROR: "+event.n+" .character was \"empty\"");
//		return 0;
//	}
//}
//
//function assign (name : String, job : String) {
//	// if null inputs
//		// if feeding name as 'null', unassign position
//		if (name == null) {
//			// unassign characters from the job
//			for (var person : KeyValuePair.<String, characterClass> in character) { if (person.Value.assignment == job) 	person.Value.assignment = null; } 
//			// unassign job
//			assignment[job]		= null;
//			return;
//		}
//		// if feeding assignment as 'null', unassign name from everything
//		if (job == null) {
//			// null out character's assignment
//			character[name].assignment = null;
//			// unassign name from all assignments
//			if (assignment.ContainsValue(name) ) for (var j : String in assignment.Keys) { if (assignment[j] == name) assignment[j] = null; }
//			return;
//		}
//	// check that 'name' is in character -dictionary
//	if (!character.ContainsKey(name) ) { Debug.LogError("ERROR: assigning '"+name+"' but he's not in 'character' -dictionary"); return; }
//	
//	// clean 'job' from all characters
//	for (var person : KeyValuePair.<String, characterClass> in character) { 
//		// null out all characters assigned to the matching the "job"
//		if (person.Value.assignment == job) 	person.Value.assignment = null; 
//	} 		
//	// clean 'name' from all assignments
//	for (var worker : String in assignment.Values) {
//		// null out the assigned character from all jobs
//		if (worker == "name") worker == null;
//	}
//
//	// assign character with "name" for the job
//	character[name].assignment = job; 	
//	// update assignment -dictionary			
//	assignment[job]		= name;
//}
//
//function getAssigned(job : String) : String {
//	return assignment[job];
//}
//
//
//
//// **************************************
//function assignmentRoll (Assignment : String, skill : String) : float {
//	
//	isProxy = false;
//	var characterProxy : String = null;
//	var proxy = -150;	// proxy is a pentalty for the character trying to fill a second position
//	
//	if (assignment[Assignment] != null && character[assignment[Assignment]].isActive) {
//		if (skill == "leadership")  { return character[assignment[Assignment]].leadership   + roll; }
//		if (skill == "emissar")     { return character[assignment[Assignment]].emissar      + roll; }
//		if (skill == "hr")          { return character[assignment[Assignment]].hr           + roll; }
//		if (skill == "engineering") { return character[assignment[Assignment]].engineering  + roll; }
//		if (skill == "precognition"){ return character[assignment[Assignment]].precognition + roll; }
//		if (skill == "psy")         { return character[assignment[Assignment]].psy          + roll; }
//		if (skill == "navigation")  { return character[assignment[Assignment]].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[assignment[Assignment]].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[assignment[Assignment]].combat       + roll; }
//		if (skill == "trading")     { return character[assignment[Assignment]].trading      + roll; }
//		if (skill == "diplomat")    { return character[assignment[Assignment]].diplomat     + roll; }
//		if (skill == "scientist")   { return character[assignment[Assignment]].scientist    + roll; }
//		if (skill == "integrity")   { return character[assignment[Assignment]].integrity    + roll; }
//		if (skill == "holiness")    { return character[assignment[Assignment]].holiness     + roll; }
//		if (skill == "purity")      { return character[assignment[Assignment]].purity       + roll; }
//		if (skill == "security")    { return character[assignment[Assignment]].security     + roll; }
//		if (skill == "violent")     { return character[assignment[Assignment]].violent      + roll; }
//		if (skill == "aristocrat")  { return character[assignment[Assignment]].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[assignment[Assignment]].imperialist  + roll; }
//		if (skill == "corruption")  { return character[assignment[Assignment]].corruption   + roll; }
//	}
//	else {
//		isProxy = true;
//		if (skill == "leadership")  { characterProxy = getBest("leadership"); 	return character[characterProxy].leadership  +proxy + roll; }
//		if (skill == "emissar")     { characterProxy = getBest("emissar"); 		return character[characterProxy].emissar     +proxy + roll; }
//		if (skill == "hr")          { characterProxy = getBest("hr"); 			return character[characterProxy].hr          +proxy + roll; }
//		if (skill == "engineering") { characterProxy = getBest("engineering"); 	return character[characterProxy].engineering +proxy + roll; }
//		if (skill == "precognition"){ characterProxy = getBest("precognition"); return character[characterProxy].precognition+proxy + roll; }
//		if (skill == "psy")         { characterProxy = getBest("psy"); 			return character[characterProxy].psy         +proxy + roll; }
//		if (skill == "navigation")  { characterProxy = getBest("navigation"); 	return character[characterProxy].navigation  +proxy + roll; }
//		if (skill == "spaceBattle") { characterProxy = getBest("spaceBattle"); 	return character[characterProxy].spaceBattle +proxy + roll; }
//		if (skill == "combat")      { characterProxy = getBest("combat"); 		return character[characterProxy].combat      +proxy + roll; }
//		if (skill == "trading")     { characterProxy = getBest("trading"); 		return character[characterProxy].trading     +proxy + roll; }
//		if (skill == "diplomat")    { characterProxy = getBest("diplomat"); 	return character[characterProxy].diplomat    +proxy + roll; }
//		if (skill == "scientist")   { characterProxy = getBest("scientist"); 	return character[characterProxy].scientist   +proxy + roll; }
//		if (skill == "integrity")   { characterProxy = getBest("integrity"); 	return character[characterProxy].integrity   +proxy + roll; }
//		if (skill == "holiness")    { characterProxy = getBest("holiness"); 	return character[characterProxy].holiness    +proxy + roll; }
//		if (skill == "purity")      { characterProxy = getBest("purity"); 		return character[characterProxy].purity      +proxy + roll; }
//		if (skill == "security")    { characterProxy = getBest("security"); 	return character[characterProxy].security    +proxy + roll; }
//		if (skill == "violent")     { characterProxy = getBest("violent"); 		return character[characterProxy].violent     +proxy + roll; }
//		if (skill == "aristocrat")  { characterProxy = getBest("aristocrat"); 	return character[characterProxy].aristocrat  +proxy + roll; }
//		if (skill == "imperialist") { characterProxy = getBest("imperialist"); 	return character[characterProxy].imperialist +proxy + roll; }
//		if (skill == "corruption")  { characterProxy = getBest("corruption"); 	return character[characterProxy].corruption  +proxy + roll; }
//	}
//	return 0;
//}
//
//
//function gatherPortraitFileNames() : List.<portraitClass> {
//	if (portrait == null) return;
//
//	var portraitFiles : String[] = Directory.GetFiles("Assets/NGUI_custom/Atlases/Sprites/HD/portraits", "portrait*.jpg");
//	var list = new List.<portraitClass>();
//	var tag : String;
//	for (file in portraitFiles) {
//		file = Path.GetFileNameWithoutExtension(file);
//		// only collect generation 'a' portraits
//		if (file[12] == "a") {
//			// find tag, if none = null
//			tag = null;
//			var count : int = file.Length;
//			for (var i:int = 0; i<count; i++) {
//				if (file[i] == "-") {
//					// parse tag
//					for (var j:int = i+1; j<count; j++) {
//						tag += file[j];
//					}
//				}
//			}
//			list.Add(new portraitClass(Path.GetFileNameWithoutExtension(file), tag));
//		}
//	}
//	// sort list by id's
//	var sortById = function(id1 : portraitClass, id2 : portraitClass) {
//	    return id1.id.CompareTo (id2.id);
//	};
//	// sorts all portraits by id
//	list.Sort(sortById);
//
//	return list;
//}
//
//
//function assignPortraits() {
//	var seekingTag : String = null;
//
//	var takePortrait = function(c : characterClass, portraitN : int) {
//		c.portraitName = portrait[portraitN].name;
//		c.portraitGeneration();
//		portrait[portraitN].status = portraitStatus.taken;
//		//age selector
//	};
//
//	var findPortrait = function(c : characterClass) : boolean {	
//		// go through portraits
//		var count : int = portrait.Count;
//		for (var i : int = 0; i<count; i++) {
//			if (portrait[i].status == portraitStatus.available) {
//				// if portrait and character tags match
//				if (portrait[i].tag == c.portraitTag) {
//					//if (debugging) Debug.Log("Portraits: mactch found for '"+c.name+": "+portrait[i].tag+" == portraitTag: c.portraitTag: "+c.portraitTag+".");
//					takePortrait(c, i);
//					return true;
//				}
//				// else remember tag to reset Used for
//				else seekingTag = c.portraitTag;
//			}
//		}
//		return false;
//	};
//	var makeUsedAvailable = function() : boolean {
//		var count : int = portrait.Count;
//		for (var i : int = 0; i<count; i++) {
//			// seek used ones
//			if (portrait[i].status == portraitStatus.used) {
//				// if portrait tag is the one we need
//				if (portrait[i].tag == seekingTag) {
//					portrait[i].status = portraitStatus.available;
//					return true;
//				}
//			}
//		}
//		return false;
//	};
//	var findNullPortrait = function(c : characterClass) : boolean {	
//		// go through portraits
//		var count : int = portrait.Count;
//		for (var i : int = 0; i<count; i++) {
//			if (portrait[i].status == portraitStatus.available) {
//				// if null portrait found
//				if (portrait[i].tag == null) {
//					takePortrait(c, i);
//					return true;
//				}
//			}
//		}
//		// no nulls free, make available
//		seekingTag = null;
//		if (!makeUsedAvailable()) {
//			// no "used" nulls: assign first "used" of any tag
//			for (i = 0; i<count; i++) {
//				if (portrait[i].status == portraitStatus.used) {
//					takePortrait(c, i);
//					return true;
//				}
//			}
//			// no "used" found
//			return false;
//		}
//		// used null is now available, grab it
//		else {
//			for (i = 0; i<count; i++) {
//				if (portrait[i].status == portraitStatus.available) {
//					if (portrait[i].tag == null) {
//						takePortrait(c, i);
//						return true;
//					}
//				}
//			}
//			Debug.LogError("ERROR BUG: portrait was supposedly made available, but re-assign failed to find it.");
//		}
//
//		return false;
//	};
//	var cleanUnusedTaken = function() {
//		// check if portrait "taken" count equals characters
//		var portraitCount : int = portrait.Count;
//		var characterCount : int = character.Count;
//		var takenCount : int;
//		for (var i : int = 0; i<portraitCount; i++) {
//			if (portrait[i].status == portraitStatus.taken) {
//				takenCount++;
//			}
//		}
//		if (characterCount != takenCount) {
//			var found : boolean;
//			if (debugging) Debug.Log("Portraits: 'characterCount' count ("+characterCount+") mismatch with 'taken' portraits ("+takenCount+"): performing cleaning");
//			// go thourgh all 'taken' portraits
//			for (i = 0; i<portraitCount; i++) {
//				if (portrait[i].status == portraitStatus.taken) {
//					found = false;
//					// go through all characters to find match
//					for (c in character.Values) {
//						if (c.portraitName == portrait[i].name) found = true;
//					}
//					// if match not found, clean it: make portrait 'used'
//					if (!found) portrait[i].status = portraitStatus.used;
//				}
//			}
//		}
//		// count again to make sure it's ok
//		takenCount = 0;
//		for (i = 0; i<portraitCount; i++) {
//			if (portrait[i].status == portraitStatus.taken) {
//				takenCount++;
//			}
//		}
//		if (characterCount != takenCount) { Debug.LogError("ERROR: couldn't clean mismatch between 'taken' portraits and the number of characters."); }
//	};
//
//	// ****************************************'
//	// go through all characters
//	for (c in character.Values) {
//		// needs portrait
//		if (c.portraitName == null) {
//			// finding portrait
//			//if (debugging) Debug.Log("Portraits: finding portrait for '"+c.name+"'");
//			if (!findPortrait(c)) {
//				// dind't find one, checking if "used" one can be released
//				//if (debugging) Debug.Log("Portraits: dind't find one for '"+c.name+"', checking if 'used' one can be released.");
//				if (!makeUsedAvailable()) {
//					// if out of tagged portraits, force-assigning null-tagged portrait
//					if (!findNullPortrait(c)) {
//						Debug.LogWarning("ERROR: Out of portraits!");
//					}
//				}
//				// portrait was released from "used" state, now assigning that
//				else if (!findPortrait(c)) {
//					Debug.LogError("ERROR BUG: portrait was supposedly made available, but re-assign failed to find it.");
//				}
//			}
//		} 
//	}
//	cleanUnusedTaken();
//	// ****************************************'
//}
//
//
///*
//function captainRoll (skill : String) : float {
//	
//	isProxy = false;
//	var characterProxy : String = null;
//	var proxy = -150;	// proxy is a pentalty for the character trying to fill a second position
//	
//	if (assignment["captain"] != null) {
//		if (skill == "leadership")  { return character[assignment["captain"]].leadership   + roll; }
//		if (skill == "emissar")     { return character[assignment["captain"]].emissar      + roll; }
//		if (skill == "hr")          { return character[assignment["captain"]].hr           + roll; }
//		if (skill == "engineering") { return character[assignment["captain"]].engineering  + roll; }
//		if (skill == "precognition"){ return character[assignment["captain"]].precognition + roll; }
//		if (skill == "psy")         { return character[assignment["captain"]].psy          + roll; }
//		if (skill == "navigation")  { return character[assignment["captain"]].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[assignment["captain"]].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[assignment["captain"]].combat       + roll; }
//		if (skill == "trading")     { return character[assignment["captain"]].trading      + roll; }
//		if (skill == "diplomat")    { return character[assignment["captain"]].diplomat     + roll; }
//		if (skill == "scientist")   { return character[assignment["captain"]].scientist    + roll; }
//		if (skill == "integrity")   { return character[assignment["captain"]].integrity    + roll; }
//		if (skill == "holiness")    { return character[assignment["captain"]].holiness     + roll; }
//		if (skill == "purity")      { return character[assignment["captain"]].purity       + roll; }
//		if (skill == "security")    { return character[assignment["captain"]].security     + roll; }
//		if (skill == "violent")     { return character[assignment["captain"]].violent      + roll; }
//		if (skill == "aristocrat")  { return character[assignment["captain"]].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[assignment["captain"]].imperialist  + roll; }
//		if (skill == "corruption")  { return character[assignment["captain"]].corruption   + roll; }
//	}
//	else {
//		isProxy = true;
//		if (skill == "leadership")  { characterProxy = getBest("leadership"); 	return character[characterProxy].leadership  -proxy + roll; }
//		if (skill == "emissar")     { characterProxy = getBest("emissar"); 		return character[characterProxy].emissar     -proxy + roll; }
//		if (skill == "hr")          { characterProxy = getBest("hr"); 			return character[characterProxy].hr          -proxy + roll; }
//		if (skill == "engineering") { characterProxy = getBest("engineering"); 	return character[characterProxy].engineering -proxy + roll; }
//		if (skill == "precognition"){ characterProxy = getBest("precognition"); return character[characterProxy].precognition-proxy + roll; }
//		if (skill == "psy")         { characterProxy = getBest("psy"); 			return character[characterProxy].psy         -proxy + roll; }
//		if (skill == "navigation")  { characterProxy = getBest("navigation"); 	return character[characterProxy].navigation  -proxy + roll; }
//		if (skill == "spaceBattle") { characterProxy = getBest("spaceBattle"); 	return character[characterProxy].spaceBattle -proxy + roll; }
//		if (skill == "combat")      { characterProxy = getBest("combat"); 		return character[characterProxy].combat      -proxy + roll; }
//		if (skill == "trading")     { characterProxy = getBest("trading"); 		return character[characterProxy].trading     -proxy + roll; }
//		if (skill == "diplomat")    { characterProxy = getBest("diplomat"); 	return character[characterProxy].diplomat    -proxy + roll; }
//		if (skill == "scientist")   { characterProxy = getBest("scientist"); 	return character[characterProxy].scientist   -proxy + roll; }
//		if (skill == "integrity")   { characterProxy = getBest("integrity"); 	return character[characterProxy].integrity   -proxy + roll; }
//		if (skill == "holiness")    { characterProxy = getBest("holiness"); 	return character[characterProxy].holiness    -proxy + roll; }
//		if (skill == "purity")      { characterProxy = getBest("purity"); 		return character[characterProxy].purity      -proxy + roll; }
//		if (skill == "security")    { characterProxy = getBest("security"); 	return character[characterProxy].security    -proxy + roll; }
//		if (skill == "violent")     { characterProxy = getBest("violent"); 		return character[characterProxy].violent     -proxy + roll; }
//		if (skill == "aristocrat")  { characterProxy = getBest("aristocrat"); 	return character[characterProxy].aristocrat  -proxy + roll; }
//		if (skill == "imperialist") { characterProxy = getBest("imperialist"); 	return character[characterProxy].imperialist -proxy + roll; }
//		if (skill == "corruption")  { characterProxy = getBest("corruption"); 	return character[characterProxy].corruption  -proxy + roll; }
//	}
//	return 0;
//}
//
//function navigatorRoll (skill : String) : float {
//	
//	isProxy = false;
//	var characterProxy : String = null;
//	var proxy = -150;	// proxy is a pentalty for the character trying to fill a second position
//	
//	if (assignment["navigator"] != null) {
//		if (skill == "leadership")  { return character[assignment["navigator"]].leadership   + roll; }
//		if (skill == "emissar")     { return character[assignment["navigator"]].emissar      + roll; }
//		if (skill == "hr")          { return character[assignment["navigator"]].hr           + roll; }
//		if (skill == "engineering") { return character[assignment["navigator"]].engineering  + roll; }
//		if (skill == "precognition"){ return character[assignment["navigator"]].precognition + roll; }
//		if (skill == "psy")         { return character[assignment["navigator"]].psy          + roll; }
//		if (skill == "navigation")  { return character[assignment["navigator"]].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[assignment["navigator"]].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[assignment["navigator"]].combat       + roll; }
//		if (skill == "trading")     { return character[assignment["navigator"]].trading      + roll; }
//		if (skill == "diplomat")    { return character[assignment["navigator"]].diplomat     + roll; }
//		if (skill == "scientist")   { return character[assignment["navigator"]].scientist    + roll; }
//		if (skill == "integrity")   { return character[assignment["navigator"]].integrity    + roll; }
//		if (skill == "holiness")    { return character[assignment["navigator"]].holiness     + roll; }
//		if (skill == "purity")      { return character[assignment["navigator"]].purity       + roll; }
//		if (skill == "security")    { return character[assignment["navigator"]].security     + roll; }
//		if (skill == "violent")     { return character[assignment["navigator"]].violent      + roll; }
//		if (skill == "aristocrat")  { return character[assignment["navigator"]].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[assignment["navigator"]].imperialist  + roll; }
//		if (skill == "corruption")  { return character[assignment["navigator"]].corruption   + roll; }
//	}
//	else {
//		isProxy = true;
//		if (skill == "leadership")  { characterProxy = getBest("leadership"); 	return character[characterProxy].leadership  -proxy + roll; }
//		if (skill == "emissar")     { characterProxy = getBest("emissar"); 		return character[characterProxy].emissar     -proxy + roll; }
//		if (skill == "hr")          { characterProxy = getBest("hr"); 			return character[characterProxy].hr          -proxy + roll; }
//		if (skill == "engineering") { characterProxy = getBest("engineering"); 	return character[characterProxy].engineering -proxy + roll; }
//		if (skill == "precognition"){ characterProxy = getBest("precognition"); return character[characterProxy].precognition-proxy + roll; }
//		if (skill == "psy")         { characterProxy = getBest("psy"); 			return character[characterProxy].psy         -proxy + roll; }
//		if (skill == "navigation")  { characterProxy = getBest("navigation"); 	return character[characterProxy].navigation  -proxy + roll; }
//		if (skill == "spaceBattle") { characterProxy = getBest("spaceBattle"); 	return character[characterProxy].spaceBattle -proxy + roll; }
//		if (skill == "combat")      { characterProxy = getBest("combat"); 		return character[characterProxy].combat      -proxy + roll; }
//		if (skill == "trading")     { characterProxy = getBest("trading"); 		return character[characterProxy].trading     -proxy + roll; }
//		if (skill == "diplomat")    { characterProxy = getBest("diplomat"); 	return character[characterProxy].diplomat    -proxy + roll; }
//		if (skill == "scientist")   { characterProxy = getBest("scientist"); 	return character[characterProxy].scientist   -proxy + roll; }
//		if (skill == "integrity")   { characterProxy = getBest("integrity"); 	return character[characterProxy].integrity   -proxy + roll; }
//		if (skill == "holiness")    { characterProxy = getBest("holiness"); 	return character[characterProxy].holiness    -proxy + roll; }
//		if (skill == "purity")      { characterProxy = getBest("purity"); 		return character[characterProxy].purity      -proxy + roll; }
//		if (skill == "security")    { characterProxy = getBest("security"); 	return character[characterProxy].security    -proxy + roll; }
//		if (skill == "violent")     { characterProxy = getBest("violent"); 		return character[characterProxy].violent     -proxy + roll; }
//		if (skill == "aristocrat")  { characterProxy = getBest("aristocrat"); 	return character[characterProxy].aristocrat  -proxy + roll; }
//		if (skill == "imperialist") { characterProxy = getBest("imperialist"); 	return character[characterProxy].imperialist -proxy + roll; }
//		if (skill == "corruption")  { characterProxy = getBest("corruption"); 	return character[characterProxy].corruption  -proxy + roll; }
//	}
//	return 0;
//}
//
//function engineerRoll (skill : String) :float {
//	
//	isProxy = false;
//	var characterProxy : String = null;
//	var proxy = -150;	// proxy is a pentalty for the character trying to fill a second position
//	
//	if (assignment["engineer"] != null) {
//		if (skill == "leadership")  { return character[assignment["engineer"]].leadership   + roll; }
//		if (skill == "emissar")     { return character[assignment["engineer"]].emissar      + roll; }
//		if (skill == "hr")          { return character[assignment["engineer"]].hr           + roll; }
//		if (skill == "engineering") { return character[assignment["engineer"]].engineering  + roll; }
//		if (skill == "precognition"){ return character[assignment["engineer"]].precognition + roll; }
//		if (skill == "psy")         { return character[assignment["engineer"]].psy          + roll; }
//		if (skill == "navigation")  { return character[assignment["engineer"]].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[assignment["engineer"]].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[assignment["engineer"]].combat       + roll; }
//		if (skill == "trading")     { return character[assignment["engineer"]].trading      + roll; }
//		if (skill == "diplomat")    { return character[assignment["engineer"]].diplomat     + roll; }
//		if (skill == "scientist")   { return character[assignment["engineer"]].scientist    + roll; }
//		if (skill == "integrity")   { return character[assignment["engineer"]].integrity    + roll; }
//		if (skill == "holiness")    { return character[assignment["engineer"]].holiness     + roll; }
//		if (skill == "purity")      { return character[assignment["engineer"]].purity       + roll; }
//		if (skill == "security")    { return character[assignment["engineer"]].security     + roll; }
//		if (skill == "violent")     { return character[assignment["engineer"]].violent      + roll; }
//		if (skill == "aristocrat")  { return character[assignment["engineer"]].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[assignment["engineer"]].imperialist  + roll; }
//		if (skill == "corruption")  { return character[assignment["engineer"]].corruption   + roll; }
//	}
//	else {
//		isProxy = true;
//		if (skill == "leadership")  { characterProxy = getBest("leadership"); 	return character[characterProxy].leadership  -proxy + roll; }
//		if (skill == "emissar")     { characterProxy = getBest("emissar"); 		return character[characterProxy].emissar     -proxy + roll; }
//		if (skill == "hr")          { characterProxy = getBest("hr"); 			return character[characterProxy].hr          -proxy + roll; }
//		if (skill == "engineering") { characterProxy = getBest("engineering"); 	return character[characterProxy].engineering -proxy + roll; }
//		if (skill == "precognition"){ characterProxy = getBest("precognition"); return character[characterProxy].precognition-proxy + roll; }
//		if (skill == "psy")         { characterProxy = getBest("psy"); 			return character[characterProxy].psy         -proxy + roll; }
//		if (skill == "navigation")  { characterProxy = getBest("navigation"); 	return character[characterProxy].navigation  -proxy + roll; }
//		if (skill == "spaceBattle") { characterProxy = getBest("spaceBattle"); 	return character[characterProxy].spaceBattle -proxy + roll; }
//		if (skill == "combat")      { characterProxy = getBest("combat"); 		return character[characterProxy].combat      -proxy + roll; }
//		if (skill == "trading")     { characterProxy = getBest("trading"); 		return character[characterProxy].trading     -proxy + roll; }
//		if (skill == "diplomat")    { characterProxy = getBest("diplomat"); 	return character[characterProxy].diplomat    -proxy + roll; }
//		if (skill == "scientist")   { characterProxy = getBest("scientist"); 	return character[characterProxy].scientist   -proxy + roll; }
//		if (skill == "integrity")   { characterProxy = getBest("integrity"); 	return character[characterProxy].integrity   -proxy + roll; }
//		if (skill == "holiness")    { characterProxy = getBest("holiness"); 	return character[characterProxy].holiness    -proxy + roll; }
//		if (skill == "purity")      { characterProxy = getBest("purity"); 		return character[characterProxy].purity      -proxy + roll; }
//		if (skill == "security")    { characterProxy = getBest("security"); 	return character[characterProxy].security    -proxy + roll; }
//		if (skill == "violent")     { characterProxy = getBest("violent"); 		return character[characterProxy].violent     -proxy + roll; }
//		if (skill == "aristocrat")  { characterProxy = getBest("aristocrat"); 	return character[characterProxy].aristocrat  -proxy + roll; }
//		if (skill == "imperialist") { characterProxy = getBest("imperialist"); 	return character[characterProxy].imperialist -proxy + roll; }
//		if (skill == "corruption")  { characterProxy = getBest("corruption"); 	return character[characterProxy].corruption  -proxy + roll; }
//	}
//	return 0;
//}
//
//function securityRoll (skill : String) :float {
//	
//	isProxy = false;
//	var characterProxy : String = null;
//	var proxy = -150;	// proxy is a pentalty for the character trying to fill a second position
//	
//	if (assignment["security"] != null) {
//		if (skill == "leadership")  { return character[assignment["security"]].leadership   + roll; }
//		if (skill == "emissar")     { return character[assignment["security"]].emissar      + roll; }
//		if (skill == "hr")          { return character[assignment["security"]].hr           + roll; }
//		if (skill == "engineering") { return character[assignment["security"]].engineering  + roll; }
//		if (skill == "precognition"){ return character[assignment["security"]].precognition + roll; }
//		if (skill == "psy")         { return character[assignment["security"]].psy          + roll; }
//		if (skill == "navigation")  { return character[assignment["security"]].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[assignment["security"]].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[assignment["security"]].combat       + roll; }
//		if (skill == "trading")     { return character[assignment["security"]].trading      + roll; }
//		if (skill == "diplomat")    { return character[assignment["security"]].diplomat     + roll; }
//		if (skill == "scientist")   { return character[assignment["security"]].scientist    + roll; }
//		if (skill == "integrity")   { return character[assignment["security"]].integrity    + roll; }
//		if (skill == "holiness")    { return character[assignment["security"]].holiness     + roll; }
//		if (skill == "purity")      { return character[assignment["security"]].purity       + roll; }
//		if (skill == "security")    { return character[assignment["security"]].security     + roll; }
//		if (skill == "violent")     { return character[assignment["security"]].violent      + roll; }
//		if (skill == "aristocrat")  { return character[assignment["security"]].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[assignment["security"]].imperialist  + roll; }
//		if (skill == "corruption")  { return character[assignment["security"]].corruption   + roll; }
//	}
//	else {
//		isProxy = true;
//		if (skill == "leadership")  { characterProxy = getBest("leadership"); 	return character[characterProxy].leadership  -proxy + roll; }
//		if (skill == "emissar")     { characterProxy = getBest("emissar"); 		return character[characterProxy].emissar     -proxy + roll; }
//		if (skill == "hr")          { characterProxy = getBest("hr"); 			return character[characterProxy].hr          -proxy + roll; }
//		if (skill == "engineering") { characterProxy = getBest("engineering"); 	return character[characterProxy].engineering -proxy + roll; }
//		if (skill == "precognition"){ characterProxy = getBest("precognition"); return character[characterProxy].precognition-proxy + roll; }
//		if (skill == "psy")         { characterProxy = getBest("psy"); 			return character[characterProxy].psy         -proxy + roll; }
//		if (skill == "navigation")  { characterProxy = getBest("navigation"); 	return character[characterProxy].navigation  -proxy + roll; }
//		if (skill == "spaceBattle") { characterProxy = getBest("spaceBattle"); 	return character[characterProxy].spaceBattle -proxy + roll; }
//		if (skill == "combat")      { characterProxy = getBest("combat"); 		return character[characterProxy].combat      -proxy + roll; }
//		if (skill == "trading")     { characterProxy = getBest("trading"); 		return character[characterProxy].trading     -proxy + roll; }
//		if (skill == "diplomat")    { characterProxy = getBest("diplomat"); 	return character[characterProxy].diplomat    -proxy + roll; }
//		if (skill == "scientist")   { characterProxy = getBest("scientist"); 	return character[characterProxy].scientist   -proxy + roll; }
//		if (skill == "integrity")   { characterProxy = getBest("integrity"); 	return character[characterProxy].integrity   -proxy + roll; }
//		if (skill == "holiness")    { characterProxy = getBest("holiness"); 	return character[characterProxy].holiness    -proxy + roll; }
//		if (skill == "purity")      { characterProxy = getBest("purity"); 		return character[characterProxy].purity      -proxy + roll; }
//		if (skill == "security")    { characterProxy = getBest("security"); 	return character[characterProxy].security    -proxy + roll; }
//		if (skill == "violent")     { characterProxy = getBest("violent"); 		return character[characterProxy].violent     -proxy + roll; }
//		if (skill == "aristocrat")  { characterProxy = getBest("aristocrat"); 	return character[characterProxy].aristocrat  -proxy + roll; }
//		if (skill == "imperialist") { characterProxy = getBest("imperialist"); 	return character[characterProxy].imperialist -proxy + roll; }
//		if (skill == "corruption")  { characterProxy = getBest("corruption"); 	return character[characterProxy].corruption  -proxy + roll; }
//	}
//	return 0;
//}
//
//function quartermasterRoll (skill : String) :float {
//	
//	isProxy = false;
//	var characterProxy : String = null;
//	var proxy = -100;	// proxy is a pentalty for the character trying to fill a second position
//	
//	if (assignment["quartermaster"] != null) {
//		if (skill == "leadership")  { return character[assignment["quartermaster"]].leadership   + roll; }
//		if (skill == "emissar")     { return character[assignment["quartermaster"]].emissar      + roll; }
//		if (skill == "hr")          { return character[assignment["quartermaster"]].hr           + roll; }
//		if (skill == "engineering") { return character[assignment["quartermaster"]].engineering  + roll; }
//		if (skill == "precognition"){ return character[assignment["quartermaster"]].precognition + roll; }
//		if (skill == "psy")         { return character[assignment["quartermaster"]].psy          + roll; }
//		if (skill == "navigation")  { return character[assignment["quartermaster"]].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[assignment["quartermaster"]].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[assignment["quartermaster"]].combat       + roll; }
//		if (skill == "trading")     { return character[assignment["quartermaster"]].trading      + roll; }
//		if (skill == "diplomat")    { return character[assignment["quartermaster"]].diplomat     + roll; }
//		if (skill == "scientist")   { return character[assignment["quartermaster"]].scientist    + roll; }
//		if (skill == "integrity")   { return character[assignment["quartermaster"]].integrity    + roll; }
//		if (skill == "holiness")    { return character[assignment["quartermaster"]].holiness     + roll; }
//		if (skill == "purity")      { return character[assignment["quartermaster"]].purity       + roll; }
//		if (skill == "security")    { return character[assignment["quartermaster"]].security     + roll; }
//		if (skill == "violent")     { return character[assignment["quartermaster"]].violent      + roll; }
//		if (skill == "aristocrat")  { return character[assignment["quartermaster"]].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[assignment["quartermaster"]].imperialist  + roll; }
//		if (skill == "corruption")  { return character[assignment["quartermaster"]].corruption   + roll; }
//	}
//	else {
//		isProxy = true;
//		if (skill == "leadership")  { characterProxy = getBest("leadership"); 	return character[characterProxy].leadership  -proxy + roll; }
//		if (skill == "emissar")     { characterProxy = getBest("emissar"); 		return character[characterProxy].emissar     -proxy + roll; }
//		if (skill == "hr")          { characterProxy = getBest("hr"); 			return character[characterProxy].hr          -proxy + roll; }
//		if (skill == "engineering") { characterProxy = getBest("engineering"); 	return character[characterProxy].engineering -proxy + roll; }
//		if (skill == "precognition"){ characterProxy = getBest("precognition"); return character[characterProxy].precognition-proxy + roll; }
//		if (skill == "psy")         { characterProxy = getBest("psy"); 			return character[characterProxy].psy         -proxy + roll; }
//		if (skill == "navigation")  { characterProxy = getBest("navigation"); 	return character[characterProxy].navigation  -proxy + roll; }
//		if (skill == "spaceBattle") { characterProxy = getBest("spaceBattle"); 	return character[characterProxy].spaceBattle -proxy + roll; }
//		if (skill == "combat")      { characterProxy = getBest("combat"); 		return character[characterProxy].combat      -proxy + roll; }
//		if (skill == "trading")     { characterProxy = getBest("trading"); 		return character[characterProxy].trading     -proxy + roll; }
//		if (skill == "diplomat")    { characterProxy = getBest("diplomat"); 	return character[characterProxy].diplomat    -proxy + roll; }
//		if (skill == "scientist")   { characterProxy = getBest("scientist"); 	return character[characterProxy].scientist   -proxy + roll; }
//		if (skill == "integrity")   { characterProxy = getBest("integrity"); 	return character[characterProxy].integrity   -proxy + roll; }
//		if (skill == "holiness")    { characterProxy = getBest("holiness"); 	return character[characterProxy].holiness    -proxy + roll; }
//		if (skill == "purity")      { characterProxy = getBest("purity"); 		return character[characterProxy].purity      -proxy + roll; }
//		if (skill == "security")    { characterProxy = getBest("security"); 	return character[characterProxy].security    -proxy + roll; }
//		if (skill == "violent")     { characterProxy = getBest("violent"); 		return character[characterProxy].violent     -proxy + roll; }
//		if (skill == "aristocrat")  { characterProxy = getBest("aristocrat"); 	return character[characterProxy].aristocrat  -proxy + roll; }
//		if (skill == "imperialist") { characterProxy = getBest("imperialist"); 	return character[characterProxy].imperialist -proxy + roll; }
//		if (skill == "corruption")  { characterProxy = getBest("corruption"); 	return character[characterProxy].corruption  -proxy + roll; }
//	}
//	return 0;
//}
//
//function psycherRoll (skill : String) :float {
//	
//	isProxy = false;
//	var characterProxy : String = null;
//	var proxy = -100;	// proxy is a pentalty for the character trying to fill a second position
//	
//	if (assignment["psycher"] != null) {
//		if (skill == "leadership")  { return character[assignment["psycher"]].leadership   + roll; }
//		if (skill == "emissar")     { return character[assignment["psycher"]].emissar      + roll; }
//		if (skill == "hr")          { return character[assignment["psycher"]].hr           + roll; }
//		if (skill == "engineering") { return character[assignment["psycher"]].engineering  + roll; }
//		if (skill == "precognition"){ return character[assignment["psycher"]].precognition + roll; }
//		if (skill == "psy")         { return character[assignment["psycher"]].psy          + roll; }
//		if (skill == "navigation")  { return character[assignment["psycher"]].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[assignment["psycher"]].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[assignment["psycher"]].combat       + roll; }
//		if (skill == "trading")     { return character[assignment["psycher"]].trading      + roll; }
//		if (skill == "diplomat")    { return character[assignment["psycher"]].diplomat     + roll; }
//		if (skill == "scientist")   { return character[assignment["psycher"]].scientist    + roll; }
//		if (skill == "integrity")   { return character[assignment["psycher"]].integrity    + roll; }
//		if (skill == "holiness")    { return character[assignment["psycher"]].holiness     + roll; }
//		if (skill == "purity")      { return character[assignment["psycher"]].purity       + roll; }
//		if (skill == "security")    { return character[assignment["psycher"]].security     + roll; }
//		if (skill == "violent")     { return character[assignment["psycher"]].violent      + roll; }
//		if (skill == "aristocrat")  { return character[assignment["psycher"]].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[assignment["psycher"]].imperialist  + roll; }
//		if (skill == "corruption")  { return character[assignment["psycher"]].corruption   + roll; }
//	}
//	else {
//		isProxy = true;
//		if (skill == "leadership")  { characterProxy = getBest("leadership"); 	return character[characterProxy].leadership  -proxy + roll; }
//		if (skill == "emissar")     { characterProxy = getBest("emissar"); 		return character[characterProxy].emissar     -proxy + roll; }
//		if (skill == "hr")          { characterProxy = getBest("hr"); 			return character[characterProxy].hr          -proxy + roll; }
//		if (skill == "engineering") { characterProxy = getBest("engineering"); 	return character[characterProxy].engineering -proxy + roll; }
//		if (skill == "precognition"){ characterProxy = getBest("precognition"); return character[characterProxy].precognition-proxy + roll; }
//		if (skill == "psy")         { characterProxy = getBest("psy"); 			return character[characterProxy].psy         -proxy + roll; }
//		if (skill == "navigation")  { characterProxy = getBest("navigation"); 	return character[characterProxy].navigation  -proxy + roll; }
//		if (skill == "spaceBattle") { characterProxy = getBest("spaceBattle"); 	return character[characterProxy].spaceBattle -proxy + roll; }
//		if (skill == "combat")      { characterProxy = getBest("combat"); 		return character[characterProxy].combat      -proxy + roll; }
//		if (skill == "trading")     { characterProxy = getBest("trading"); 		return character[characterProxy].trading     -proxy + roll; }
//		if (skill == "diplomat")    { characterProxy = getBest("diplomat"); 	return character[characterProxy].diplomat    -proxy + roll; }
//		if (skill == "scientist")   { characterProxy = getBest("scientist"); 	return character[characterProxy].scientist   -proxy + roll; }
//		if (skill == "integrity")   { characterProxy = getBest("integrity"); 	return character[characterProxy].integrity   -proxy + roll; }
//		if (skill == "holiness")    { characterProxy = getBest("holiness"); 	return character[characterProxy].holiness    -proxy + roll; }
//		if (skill == "purity")      { characterProxy = getBest("purity"); 		return character[characterProxy].purity      -proxy + roll; }
//		if (skill == "security")    { characterProxy = getBest("security"); 	return character[characterProxy].security    -proxy + roll; }
//		if (skill == "violent")     { characterProxy = getBest("violent"); 		return character[characterProxy].violent     -proxy + roll; }
//		if (skill == "aristocrat")  { characterProxy = getBest("aristocrat"); 	return character[characterProxy].aristocrat  -proxy + roll; }
//		if (skill == "imperialist") { characterProxy = getBest("imperialist"); 	return character[characterProxy].imperialist -proxy + roll; }
//		if (skill == "corruption")  { characterProxy = getBest("corruption"); 	return character[characterProxy].corruption  -proxy + roll; }
//	}
//	return 0;
//}
//
//function priestRoll (skill : String) :float {
//	
//	isProxy = false;
//	var characterProxy : String = null;
//	var proxy = -100;	// proxy is a pentalty for the character trying to fill a second position
//	
//	if (assignment["priest"] != null) {
//		if (skill == "leadership")  { return character[assignment["priest"]].leadership   + roll; }
//		if (skill == "emissar")     { return character[assignment["priest"]].emissar      + roll; }
//		if (skill == "hr")          { return character[assignment["priest"]].hr           + roll; }
//		if (skill == "engineering") { return character[assignment["priest"]].engineering  + roll; }
//		if (skill == "precognition"){ return character[assignment["priest"]].precognition + roll; }
//		if (skill == "psy")         { return character[assignment["priest"]].psy          + roll; }
//		if (skill == "navigation")  { return character[assignment["priest"]].navigation   + roll; }
//		if (skill == "spaceBattle") { return character[assignment["priest"]].spaceBattle  + roll; }
//		if (skill == "combat")      { return character[assignment["priest"]].combat       + roll; }
//		if (skill == "trading")     { return character[assignment["priest"]].trading      + roll; }
//		if (skill == "diplomat")    { return character[assignment["priest"]].diplomat     + roll; }
//		if (skill == "scientist")   { return character[assignment["priest"]].scientist    + roll; }
//		if (skill == "integrity")   { return character[assignment["priest"]].integrity    + roll; }
//		if (skill == "holiness")    { return character[assignment["priest"]].holiness     + roll; }
//		if (skill == "purity")      { return character[assignment["priest"]].purity       + roll; }
//		if (skill == "security")    { return character[assignment["priest"]].security     + roll; }
//		if (skill == "violent")     { return character[assignment["priest"]].violent      + roll; }
//		if (skill == "aristocrat")  { return character[assignment["priest"]].aristocrat   + roll; }
//		if (skill == "imperialist") { return character[assignment["priest"]].imperialist  + roll; }
//		if (skill == "corruption")  { return character[assignment["priest"]].corruption   + roll; }
//	}
//	else {
//		isProxy = true;
//		if (skill == "leadership")  { characterProxy = getBest("leadership"); 	return character[characterProxy].leadership  -proxy + roll; }
//		if (skill == "emissar")     { characterProxy = getBest("emissar"); 		return character[characterProxy].emissar     -proxy + roll; }
//		if (skill == "hr")          { characterProxy = getBest("hr"); 			return character[characterProxy].hr          -proxy + roll; }
//		if (skill == "engineering") { characterProxy = getBest("engineering"); 	return character[characterProxy].engineering -proxy + roll; }
//		if (skill == "precognition"){ characterProxy = getBest("precognition"); return character[characterProxy].precognition-proxy + roll; }
//		if (skill == "psy")         { characterProxy = getBest("psy"); 			return character[characterProxy].psy         -proxy + roll; }
//		if (skill == "navigation")  { characterProxy = getBest("navigation"); 	return character[characterProxy].navigation  -proxy + roll; }
//		if (skill == "spaceBattle") { characterProxy = getBest("spaceBattle"); 	return character[characterProxy].spaceBattle -proxy + roll; }
//		if (skill == "combat")      { characterProxy = getBest("combat"); 		return character[characterProxy].combat      -proxy + roll; }
//		if (skill == "trading")     { characterProxy = getBest("trading"); 		return character[characterProxy].trading     -proxy + roll; }
//		if (skill == "diplomat")    { characterProxy = getBest("diplomat"); 	return character[characterProxy].diplomat    -proxy + roll; }
//		if (skill == "scientist")   { characterProxy = getBest("scientist"); 	return character[characterProxy].scientist   -proxy + roll; }
//		if (skill == "integrity")   { characterProxy = getBest("integrity"); 	return character[characterProxy].integrity   -proxy + roll; }
//		if (skill == "holiness")    { characterProxy = getBest("holiness"); 	return character[characterProxy].holiness    -proxy + roll; }
//		if (skill == "purity")      { characterProxy = getBest("purity"); 		return character[characterProxy].purity      -proxy + roll; }
//		if (skill == "security")    { characterProxy = getBest("security"); 	return character[characterProxy].security    -proxy + roll; }
//		if (skill == "violent")     { characterProxy = getBest("violent"); 		return character[characterProxy].violent     -proxy + roll; }
//		if (skill == "aristocrat")  { characterProxy = getBest("aristocrat"); 	return character[characterProxy].aristocrat  -proxy + roll; }
//		if (skill == "imperialist") { characterProxy = getBest("imperialist"); 	return character[characterProxy].imperialist -proxy + roll; }
//		if (skill == "corruption")  { characterProxy = getBest("corruption"); 	return character[characterProxy].corruption  -proxy + roll; }
//	}
//	return 0;
//}
//*/
//
//// **************************************