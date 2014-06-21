//// events.js compiled: 30/05/2014 11:35:06
//// events: 15  triggers: 0
//
//#pragma strict
//import System.Collections.Generic;
// 
// 
//// Player - LocationFinder (Collider / Finder)
//private var player : GameObject;
//private var finder : LocationFinder;
//// ScriptHolder 
//private var scriptHolder : GameObject;
//private var nameGenerator : NameGenerator;
//private var gameTime : GameTime;
//private var collector : locationsDataCollector;
//private var ship : PlayerShipData;
//private var shipPop : ShipPopulation;
//private var economy : Economy;
//private var characters : Characters;
//private var location : locationDatabase;
//private var eventManager : EventManager;
// 
// 
//var Event 	: Dictionary.<String, eventClass> = new Dictionary.<String, eventClass>();
//var advice: Dictionary.<String, adviceClass> = new Dictionary.<String, adviceClass>(); 	// event-level dictionary with every character & opinion in the event (key: character 'name', value: 'opinion', recommend int) 
// 
// 
//class adviceClass {
//	var opinion 	: String;		// character advice txt
//	var recommend 	: int;			// character's recommended choice
//}
// 
// 
//var eventCount : int;
//var triggerCount : int;
//var n : String; // current event name
//var filterWeights : Dictionary.<String, float> = new Dictionary.<String, float>();
//enum freq { Rare, Default, Elevated, Probable }
//enum status { Quiet, Default, Alert, Panic }
//enum noise { QuietHum, QuietEerie, DefaultBridge, EchoRoom, EngineRoom, ActionBase, Fracture }
// 
// 
//class eventClass {
//	var name : String;				// Unique id, name of the event
//	var available : boolean;		// is event available for spawning
//	var frequency : freq = freq.Default;	// event frequency weight
//	var situation : status = status.Default;	// event situation (ambient sounds: mood and tone)
//	var ambient : noise = noise.DefaultBridge;	// event ambient location (ambient sounds: location and background noise)
//	var filters : List.<String> = new List.<String>();	// list of filter tags
//	var triggerType : String = null;			// if != null, trigger Event. List trigger type: inLocation, atLocation, object
//	var triggerObject : GameObject = null;		// triggerType may specify a particular object
//	var probability = function():float { return 1; }; 	// returns event probability - ALSO assigns the value to lastProbability
//	var getText = function():String { return null; }; 	// returns text field depending on conditions
//	var lastProbability : float = 1;// record last probability for event
//	var getCharacter = function() 	{ return null; };	// loads character
//	var character 	: String;		// Character name
//	var advisor 	: String;		// acting advisor name
//	var locationRequired : boolean = false;
//	var getLocation = function() : GameObject { return null; };	// loads location
//	var locationObject : GameObject = null;	// GameObject of location
//	var location : locationDatabase = null;		// locationDatabase to use
//	var locationName : String;		// Game location name
//	var getChoices = function() : Dictionary.<String, int> { return null; }; 	// returns list of choices to select
//	var choice : int = 0;			// player choice picked - event-unique int
//	var outcomes = function() {};	// calculates current outcomes
//	var outcome : int = 0;			// event outcome = event-unique int
//	var getAdvice = function() {};	// pick advice
//	var dateLastDone : float = -1;	// date when event last had outcome (gameTime.gameDay) 
//}
// 
// 
//function filter(f : String) {
//	// check "filterWeights" dictionary
//	if (!filterWeights.ContainsKey(f)) filterWeights.Add(f, 1.0);
//	// add filter to event "filters" list
//	if (!Event[n].filters.Contains(f)) Event[n].filters.Add(f);
//}
// 
// 
// 
// 
//function Start() {
//	//initializeEvents(); 	// call from initializeGame
//}
//function initializeEvents() {
//	player = GameObject.FindGameObjectWithTag("Player");
//		finder = 	player.gameObject.GetComponent(LocationFinder);
//
//	scriptHolder = GameObject.Find("ScriptHolder");
//		collector 		= scriptHolder.gameObject.GetComponent(locationsDataCollector);
//		characters 		= scriptHolder.gameObject.GetComponent(Characters);
//		eventManager	= scriptHolder.gameObject.GetComponent(EventManager);
//	loadEvents();
//	splitDictionaries();
//}
// 
// 
// 
//// ***************************************EVENTS********************************************
//function loadEvents() {
//// ------------------------------------------------------------->
// 
// 
//// NEW EVENT 1
//n = "Test Event";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Trigger
//	Event["Test Event"].triggerType = "atLocation";
//
//	// Main character
//	Event["Test Event"].getCharacter = function () : String {
//		Event["Test Event"].character = getBestFromAll("leadership");
//		return Event["Test Event"].character;
//	};
//
//	// Location
//	if (!Event["Test Event"].locationRequired) Event["Test Event"].locationRequired = true;
//	try {
//		Event["Test Event"].getLocation = function () : GameObject {
//			Event["Test Event"].locationObject = getLocation("4S03");
//			if (Event["Test Event"].locationObject != null) {
//				Event["Test Event"].location = Event["Test Event"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["Test Event"].locationName =  Event["Test Event"].location.locName;
//				return Event["Test Event"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'Test Event' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'Test Event' location input does not resolve to an object (reference exception)");
//		Event["Test Event"].location = null; Event["Test Event"].locationName = "empty";
//		return null;
//	}
//
//	// Frequency
//	Event[n].frequency = freq.Elevated;
//
//	// Filters
//		filter("AI");
//		filter("mutants");
//
//
//	// Probability Variables
//	Event["Test Event"].probability = function () {
//		n = "Test Event";
//		var p1 : float = filterWeights["AI"];
//		var p2 : float = filterWeights["mutants"];
//		var p3 : float = 1;
//		if (gameTime.gameDay > 1) {	
//			p3 = 10;
//		}
//		var p4 : float = 1;
//		if ( !playerLoc("atLocation", Event[n].locationObject.name) ) {	
//			p4 = 0;
//		}
//		var p5 : float = 1;
//		if ( characters.character[Event[n].character].leadership > 120 ) {	
//			p5 = 2;
//		}
//		Event[n].lastProbability = 1 * p1 * p2 * p3 * p4 * p5;
//		return Event[n].lastProbability;
//	};
//
//
//	// Get Advice
//	Event["Test Event"].getAdvice = function () {
//		n = "Test Event";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
//				if ( characters.character[characters.assignment["captain"]].leadership <= 150 ) { 
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'I guess we have to settle.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "I guess we have to settle.";
//					advice[advisor].recommend = 1;
//				}
//			}
//		}
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
//				if ( characters.character[characters.assignment["captain"]].leadership > 150 ) { 
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Let me lead you!' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Let me lead you!";
//					advice[advisor].recommend = 2;
//				}
//			}
//		}
//		// advice:
//		advisor = characters.assignment["navigator"]; 	
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Hmm' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Hmm";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
//
//
//	// Text field
//	Event["Test Event"].getText = function () : String {
//		var txt : String = "";
//		n = "Test Event";
//			if ( Event[n].outcome == 0 ) {	
//			txt += "We must attack at "+Event[n].location.locName+".";
//			}
//			if ( Event[n].outcome == 0  &&  characters.character[Event[n].character].leadership > 150 ) {	
//			txt += " It won't work unless "+characters.character[Event[n].character].name+" leads us!";
//			}
//			if ( Event[n].outcome > 0 ) {	
//			txt += "It has happened before!";
//			}
//		return txt;
//	};
//
//
//	// choices
//	Event["Test Event"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "Test Event";
//		choices.Add("There is but one choise.", 1);
//		if ( characters.character[Event[n].character].leadership > 150) {	
//		choices.Add("Unless "+characters.character[Event[n].character].name+"'s leadership is >150: "+characters.character[Event[n].character].leadership+"", 2);
//		}
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["Test Event"].outcomes = function () {
//		n = "Test Event";
//		var outcomes : int = 0;
//		if (Event[n].choice > 0) {	
//		Event[n].outcome = Event[n].choice;
//		outcomes ++;
//		/*Effect: $= */factionChange("nobleHouse4",-1);
//		/*Effect: $= */filterWeight("mutants",0);
//		/*Effect: $= */Event[n].available=false;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
// 
// 
//// NEW EVENT 2
//n = "Test Event inLocation";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Trigger
//	Event["Test Event inLocation"].triggerType = "inLocation";
//
//	// Main character
//	Event["Test Event inLocation"].getCharacter = function () : String {
//		Event["Test Event inLocation"].character = getBestFromAll("leadership");
//		return Event["Test Event inLocation"].character;
//	};
//
//	// Location
//	if (!Event["Test Event inLocation"].locationRequired) Event["Test Event inLocation"].locationRequired = true;
//	try {
//		Event["Test Event inLocation"].getLocation = function () : GameObject {
//			Event["Test Event inLocation"].locationObject = getLocation("4S03");
//			if (Event["Test Event inLocation"].locationObject != null) {
//				Event["Test Event inLocation"].location = Event["Test Event inLocation"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["Test Event inLocation"].locationName =  Event["Test Event inLocation"].location.locName;
//				return Event["Test Event inLocation"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'Test Event inLocation' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'Test Event inLocation' location input does not resolve to an object (reference exception)");
//		Event["Test Event inLocation"].location = null; Event["Test Event inLocation"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["Test Event inLocation"].probability = function () {
//		n = "Test Event inLocation";
//		var p1 : float = 1;
//		if (gameTime.gameDay > 1) {	
//			p1 = 10;
//		}
//		var p2 : float = 1;
//		if ( !playerLoc("inLocation", Event[n].locationObject.name) ) {	
//			p2 = 0;
//		}
//		var p3 : float = 1;
//		if ( characters.character[Event[n].character].leadership > 120 ) {	
//			p3 = 2;
//		}
//		Event[n].lastProbability = 1 * p1 * p2 * p3;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["Test Event inLocation"].getText = function () : String {
//		var txt : String = "";
//		n = "Test Event inLocation";
//			if ( Event[n].outcome == 0 ) {	
//			txt += "What happens will happen in "+Event[n].location.locName+".";
//			}
//			if ( Event[n].outcome == 0 ) {	
//			txt += " It won't happen unless "+characters.character[Event[n].character].name+" leads us!";
//			}
//			if ( Event[n].outcome > 0 ) {	
//			txt += "It has happened before!";
//			}
//		return txt;
//	};
//
//
//	// choices
//	Event["Test Event inLocation"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "Test Event inLocation";
//		choices.Add("There is but one choise.", 1);
//		if ( characters.character[Event[n].character].leadership == 150) {	
//		choices.Add("Unless "+characters.character[Event[n].character].name+"'s leadership is >150: "+characters.character[Event[n].character].leadership+"", 2);
//		}
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["Test Event inLocation"].outcomes = function () {
//		n = "Test Event inLocation";
//		var outcomes : int = 0;
//		if (Event[n].choice > 0) {	
//		Event[n].outcome = Event[n].choice;
//		outcomes ++;
//		/*Effect: $= */Event[n].available=false;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
// 
// 
//// NEW EVENT 3
//n = "The root of all evil";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Main character
//	Event["The root of all evil"].getCharacter = function () : String {
//		Event["The root of all evil"].character = getBest("psy");
//		return Event["The root of all evil"].character;
//	};
//
//
//	// Probability Variables
//	Event["The root of all evil"].probability = function () {
//		n = "The root of all evil";
//		var p1 : float = 1;
//		if (finder.magnitude < 1.7) {	
//			p1 = 0;
//		}
//		var p2 : float = 1;
//		if (gameTime.gameDay < 60) {	
//			p2 = 10;
//		}
//		var p3 : float = 1;
//		if (Event["Test Event"].outcome == 0 ) {	
//			p3 = 10;
//		}
//		Event[n].lastProbability = 1 * p1 * p2 * p3;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["The root of all evil"].getText = function () : String {
//		var txt : String = "";
//		n = "The root of all evil";
//			if (Event[n].outcome == 0) {	
//			txt += "Traveling through deep Fracture, "+characters.character[Event[n].character].name+"";
//			}
//			if (Event[n].outcome == 0  &&  characters.character[Event[n].character].corruption >= 70) {	
//			txt += " finally contacts an intelligence in the warp.";
//			}
//			if (Event[n].outcome == 0  &&  characters.character[Event[n].character].corruption < 70) {	
//			txt += " has a terrible visitation from a daemon of the warp.";
//			}
//			if ( Event[n].outcome == 1 ) {	
//			txt += ""+characters.character[Event[n].character].name+" had long talks with it, but didn't remember much afterwards.";
//			}
//			if ( Event[n].outcome == 2 ) {	
//			txt += "After a quick encounter, there is a lingering feeling it'll be back...";
//			}
//			if ( Event[n].outcome == 3 ) {	
//			txt += ""+characters.character[Event[n].character].name+" learns its name, and starts preparing rituals.";
//			}
//			if ( Event[n].outcome == 4 ) {	
//			txt += "Escape! But what do they say about running from your problems?";
//			}
//			if ( Event[n].outcome == 5 ) {	
//			txt += "Reciting the holy scriptures banishes the daemon! Praised be!";
//			}
//			if ( Event[n].outcome == 6 ) {	
//			txt += ""+characters.character[Event[n].character].name+" succeeds, and now can call upon great powers.";
//			}
//			if ( Event[n].outcome == 7 ) {	
//			txt += ""+characters.character[Event[n].character].name+" leans its name, something will come out of this.";
//			}
//			if ( Event[n].outcome == 8 ) {	
//			txt += ""+characters.character[Event[n].character].name+" succeeds, and now can call upon great powers.";
//			}
//		return txt;
//	};
//
//
//	// choices
//	Event["The root of all evil"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "The root of all evil";
//		if ( Event[n].outcome == 0 ) {	
//		choices.Add("Try to communicate with it, and learn its name.", 1);
//		}
//		if ( Event[n].outcome == 0 ) {	
//		choices.Add("Oh no, run away!", 2);
//		}
//		if ( Event[n].outcome == 0  &&  characters.character[Event[n].character].holiness > 100) {	
//		choices.Add("Recite holy scriptures.", 3);
//		}
//		if ( Event[n].outcome == 0  &&  characters.character[Event[n].character].psy > 300) {	
//		choices.Add("Try to capture the daemon.", 4);
//		}
//		if ( Event[n].outcome > 0  &&  Event[n].outcome != 5 ) {	
//		choices.Add("Continue", 8);
//		}
//		if ( Event[n].outcome == 5 ) {	
//		choices.Add("Continue, you holy crusader!", 9);
//		}
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["The root of all evil"].outcomes = function () {
//		n = "The root of all evil";
//		var outcomes : int = 0;
//		if (Event[n].choice ==1  &&  statRoll("psy") < 200) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */characters.character[Event[n].character].addStat("possessed",20);
//		}
//		if ( Event[n].choice == 1  &&  statRoll("psy") > 199  &&  statRoll("psy") < 401 ) {	
//		Event[n].outcome = 2;
//		outcomes ++;
//		}
//		if ( Event[n].choice == 1  &&  statRoll("psy") > 400) {	
//		Event[n].outcome = 3;
//		outcomes ++;
//		}
//		if ( Event[n].choice == 2 ) {	
//		Event[n].outcome = 4;
//		outcomes ++;
//		}
//		if ( Event[n].choice == 3 ) {	
//		Event[n].outcome = 5;
//		outcomes ++;
//		}
//		if (Event[n].choice == 4  &&  statRoll("psy") <= 400) {	
//		Event[n].outcome = 6;
//		outcomes ++;
//		/*Effect: $= */characters.character[Event[n].character].possessed=100;
//		}
//		if (Event[n].choice == 4  &&  statRoll("psy") > 400  &&  characters.character[Event[n].character].corruption <= 100) {	
//		Event[n].outcome = 7;
//		outcomes ++;
//		}
//		if (Event[n].choice == 4  &&  statRoll("psy") > 400  &&  characters.character[Event[n].character].corruption > 100) {	
//		Event[n].outcome = 8;
//		outcomes ++;
//		/*Effect: $= */characters.character[Event[n].character].addStat("corruption",100);
//		}
//		if ( Event[n].choice  == 8 ) {	
//		Event[n].outcome = Event[n].outcome;
//		outcomes ++;
//		/*Effect: $= */Event[n].available=false;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if ( Event[n].choice == 9 ) {	
//		Event[n].outcome = Event[n].outcome;
//		outcomes ++;
//		/*Effect: $= */characters.character[Event[n].character].addStat("holiness",0.25);
//		/*Effect: $= */Event[n].available=false;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
// 
// 
//// NEW EVENT 4
//n = "Odd behavior";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//
//	// Text field
//	Event["Odd behavior"].getText = function () : String {
//		var txt : String = "";
//		n = "Odd behavior";
//			txt += "You hear reports of odd behavior among the crew.";
//		return txt;
//	};
//
//
//	// choices
//	Event["Odd behavior"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "Odd behavior";
//		choices.Add("It's probably nothing, carry on!", 1);
//		choices.Add("Odd behavior? It might be wise to investigate this one.", 2);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["Odd behavior"].outcomes = function () {
//		n = "Odd behavior";
//		var outcomes : int = 0;
//		Event[n].outcome = Event[n].choice;
//		outcomes ++;
//		/*Effect: $= */Event[n].available=false;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["Odd behavior"].getAdvice = function () {
//		n = "Odd behavior";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Weeed to figure out what's going on.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Weeed to figure out what's going on. Oh &yes&.";
//					advice[advisor].recommend = 2;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["navigator"]; 	
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Odd behavior you say?' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Odd behavior you say?";
//					advice[advisor].recommend = 0;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["engineer"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'I've got my men under control.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "I've got my men under control.";
//					advice[advisor].recommend = 1;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["security"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'We should set up a curfew and arrest the troublemakers at once!' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "We should set up a curfew and arrest the troublemakers at once!";
//					advice[advisor].recommend = 2;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["quartermaster"]; 
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'The crew could use some downtime. We've spend too much time in the Fracture lately.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "The crew could use some downtime. We've spend too much time in the Fracture lately.";
//					advice[advisor].recommend = 0;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["psycher"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Well well, what do we have here?' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Well well, what do we have here?";
//					advice[advisor].recommend = 2;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["priest"];		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Heretics! They are all damned heretics!' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Heretics! They are all damned heretics!";
//					advice[advisor].recommend = 2;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 5
//n = "locationAdvice";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//
//	// Probability Variables
//	Event["locationAdvice"].probability = function () {
//		n = "locationAdvice";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//	// Location
//	if (!Event["locationAdvice"].locationRequired) Event["locationAdvice"].locationRequired = true;
//	try {
//		Event["locationAdvice"].getLocation = function () : GameObject {
//			Event["locationAdvice"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["locationAdvice"].locationObject != null) {
//				Event["locationAdvice"].location = Event["locationAdvice"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["locationAdvice"].locationName =  Event["locationAdvice"].location.locName;
//				return Event["locationAdvice"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'locationAdvice' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'locationAdvice' location input does not resolve to an object (reference exception)");
//		Event["locationAdvice"].location = null; Event["locationAdvice"].locationName = "empty";
//		return null;
//	}
//
//
//	// Get Advice
//	Event["locationAdvice"].getAdvice = function () {
//		n = "locationAdvice";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
//				if ( characters.character[characters.assignment["captain"]].diplomat <= 150 ) { 
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": '"+Event[n].location.ruler+" is in charge here.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = ""+Event[n].location.ruler+" is in charge here.";
//					advice[advisor].recommend = 0;
//				}
//			}
//		}
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
//				if ( characters.character[characters.assignment["captain"]].diplomat > 150 ) { 
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": '"+Event[n].location.ruler+" is in charge here.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = ""+Event[n].location.ruler+" is in charge here.";
//					advice[advisor].recommend = 0;
//				}
//			}
//		}
//		// advice:
//		advisor = characters.assignment["navigator"]; 	
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'These people once counted their lucky stars. Now they worry about crops.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "These people once counted their lucky stars. Now they worry about crops.";
//					advice[advisor].recommend = 0;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["engineer"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'We could use more engineers.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "We could use more engineers.";
//					advice[advisor].recommend = 0;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["security"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'We should keep a low profile and not recruit here.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "We should keep a low profile and not recruit here.";
//					advice[advisor].recommend = 0;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["quartermaster"]; 
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'The crew could use some downtime. We've spend too much time in the Fracture lately.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "The crew could use some downtime. We've spend too much time in the Fracture lately.";
//					advice[advisor].recommend = 0;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["psycher"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'This is an ancient planet. Something forgotten is lying beneath the surface.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "This is an ancient planet. Something forgotten is lying beneath the surface.";
//					advice[advisor].recommend = 0;
//			}
//		}
//		// advice:
//		advisor = characters.assignment["priest"];		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
//				if ( Event[n].location.ideologyStats["holy"] < -50 ) { 
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Heretics! They are all damned heretics!' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Heretics! They are all damned heretics!";
//					advice[advisor].recommend = 0;
//				}
//			}
//		}
//		// advice:
//		advisor = characters.assignment["priest"];		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
//				if ( Event[n].location.ideologyStats["holy"] >= -50  &&  Event[n].location.ideologyStats["holy"] < 0) { 
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'What a misguided place this is.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "What a misguided place this is.";
//					advice[advisor].recommend = 0;
//				}
//			}
//		}
//		// advice:
//		advisor = characters.assignment["priest"];		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
//				if ( Event[n].location.ideologyStats["holy"] >= 0  &&  Event[n].location.ideologyStats["holy"] < 40) { 
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'This place seem decent enough' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "This place seem decent enough";
//					advice[advisor].recommend = 0;
//				}
//			}
//		}
//		// advice:
//		advisor = characters.assignment["priest"];		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
//				if ( Event[n].location.ideologyStats["holy"] > 40 ) { 
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Ah, "+Event[n].location.name+" is known for its diligent and productive citizens. Truly inspiring.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Ah, "+Event[n].location.name+" is known for its diligent and productive citizens. Truly inspiring.";
//					advice[advisor].recommend = 0;
//				}
//			}
//		}
//	};
//
//
//	// Outcomes
//	Event["locationAdvice"].outcomes = function () {
//		n = "locationAdvice";
//		var outcomes : int = 0;
//		Event[n].outcome = 0;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
// 
// 
//// NEW EVENT 6
//n = "appointment_Locals";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_Locals"].locationRequired) Event["appointment_Locals"].locationRequired = true;
//	try {
//		Event["appointment_Locals"].getLocation = function () : GameObject {
//			Event["appointment_Locals"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_Locals"].locationObject != null) {
//				Event["appointment_Locals"].location = Event["appointment_Locals"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_Locals"].locationName =  Event["appointment_Locals"].location.locName;
//				return Event["appointment_Locals"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_Locals' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_Locals' location input does not resolve to an object (reference exception)");
//		Event["appointment_Locals"].location = null; Event["appointment_Locals"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_Locals"].probability = function () {
//		n = "appointment_Locals";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_Locals"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_Locals";
//			txt += "You meet with the representative of the Local Government.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_Locals"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_Locals";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_Locals"].outcomes = function () {
//		n = "appointment_Locals";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_Locals"].getAdvice = function () {
//		n = "appointment_Locals";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'We get to meet with "+Event[n].location.ruler+"'s representative.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "We get to meet with "+Event[n].location.ruler+"'s representative.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 7
//n = "appointment_nobleHouse1";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_nobleHouse1"].locationRequired) Event["appointment_nobleHouse1"].locationRequired = true;
//	try {
//		Event["appointment_nobleHouse1"].getLocation = function () : GameObject {
//			Event["appointment_nobleHouse1"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_nobleHouse1"].locationObject != null) {
//				Event["appointment_nobleHouse1"].location = Event["appointment_nobleHouse1"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_nobleHouse1"].locationName =  Event["appointment_nobleHouse1"].location.locName;
//				return Event["appointment_nobleHouse1"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_nobleHouse1' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_nobleHouse1' location input does not resolve to an object (reference exception)");
//		Event["appointment_nobleHouse1"].location = null; Event["appointment_nobleHouse1"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_nobleHouse1"].probability = function () {
//		n = "appointment_nobleHouse1";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_nobleHouse1"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_nobleHouse1";
//			txt += "You meet with the representative of the House Furia.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_nobleHouse1"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_nobleHouse1";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_nobleHouse1"].outcomes = function () {
//		n = "appointment_nobleHouse1";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_nobleHouse1"].getAdvice = function () {
//		n = "appointment_nobleHouse1";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is House Furia.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is House Furia.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 8
//n = "appointment_nobleHouse2";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_nobleHouse2"].locationRequired) Event["appointment_nobleHouse2"].locationRequired = true;
//	try {
//		Event["appointment_nobleHouse2"].getLocation = function () : GameObject {
//			Event["appointment_nobleHouse2"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_nobleHouse2"].locationObject != null) {
//				Event["appointment_nobleHouse2"].location = Event["appointment_nobleHouse2"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_nobleHouse2"].locationName =  Event["appointment_nobleHouse2"].location.locName;
//				return Event["appointment_nobleHouse2"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_nobleHouse2' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_nobleHouse2' location input does not resolve to an object (reference exception)");
//		Event["appointment_nobleHouse2"].location = null; Event["appointment_nobleHouse2"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_nobleHouse2"].probability = function () {
//		n = "appointment_nobleHouse2";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_nobleHouse2"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_nobleHouse2";
//			txt += "You meet with the representative of the House Rathmund.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_nobleHouse2"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_nobleHouse2";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_nobleHouse2"].outcomes = function () {
//		n = "appointment_nobleHouse2";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_nobleHouse2"].getAdvice = function () {
//		n = "appointment_nobleHouse2";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is House Rathmund.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is House Rathmund.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 9
//n = "appointment_nobleHouse3";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_nobleHouse3"].locationRequired) Event["appointment_nobleHouse3"].locationRequired = true;
//	try {
//		Event["appointment_nobleHouse3"].getLocation = function () : GameObject {
//			Event["appointment_nobleHouse3"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_nobleHouse3"].locationObject != null) {
//				Event["appointment_nobleHouse3"].location = Event["appointment_nobleHouse3"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_nobleHouse3"].locationName =  Event["appointment_nobleHouse3"].location.locName;
//				return Event["appointment_nobleHouse3"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_nobleHouse3' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_nobleHouse3' location input does not resolve to an object (reference exception)");
//		Event["appointment_nobleHouse3"].location = null; Event["appointment_nobleHouse3"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_nobleHouse3"].probability = function () {
//		n = "appointment_nobleHouse3";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_nobleHouse3"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_nobleHouse3";
//			txt += "You meet with the representative of the House Tarquinia.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_nobleHouse3"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_nobleHouse3";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_nobleHouse3"].outcomes = function () {
//		n = "appointment_nobleHouse3";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_nobleHouse3"].getAdvice = function () {
//		n = "appointment_nobleHouse3";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is House Tarquinia.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is House Tarquinia.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 10
//n = "appointment_nobleHouse4";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_nobleHouse4"].locationRequired) Event["appointment_nobleHouse4"].locationRequired = true;
//	try {
//		Event["appointment_nobleHouse4"].getLocation = function () : GameObject {
//			Event["appointment_nobleHouse4"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_nobleHouse4"].locationObject != null) {
//				Event["appointment_nobleHouse4"].location = Event["appointment_nobleHouse4"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_nobleHouse4"].locationName =  Event["appointment_nobleHouse4"].location.locName;
//				return Event["appointment_nobleHouse4"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_nobleHouse4' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_nobleHouse4' location input does not resolve to an object (reference exception)");
//		Event["appointment_nobleHouse4"].location = null; Event["appointment_nobleHouse4"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_nobleHouse4"].probability = function () {
//		n = "appointment_nobleHouse4";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_nobleHouse4"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_nobleHouse4";
//			txt += "You meet with the representative of the House Valeria.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_nobleHouse4"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_nobleHouse4";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_nobleHouse4"].outcomes = function () {
//		n = "appointment_nobleHouse4";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_nobleHouse4"].getAdvice = function () {
//		n = "appointment_nobleHouse4";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is House Valeria.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is House Valeria.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 11
//n = "appointment_guild1";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_guild1"].locationRequired) Event["appointment_guild1"].locationRequired = true;
//	try {
//		Event["appointment_guild1"].getLocation = function () : GameObject {
//			Event["appointment_guild1"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_guild1"].locationObject != null) {
//				Event["appointment_guild1"].location = Event["appointment_guild1"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_guild1"].locationName =  Event["appointment_guild1"].location.locName;
//				return Event["appointment_guild1"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_guild1' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_guild1' location input does not resolve to an object (reference exception)");
//		Event["appointment_guild1"].location = null; Event["appointment_guild1"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_guild1"].probability = function () {
//		n = "appointment_guild1";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_guild1"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_guild1";
//			txt += "You meet with the representative of the Everlasting Union.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_guild1"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_guild1";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_guild1"].outcomes = function () {
//		n = "appointment_guild1";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_guild1"].getAdvice = function () {
//		n = "appointment_guild1";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is the Union.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is the Union.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 12
//n = "appointment_guild2";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_guild2"].locationRequired) Event["appointment_guild2"].locationRequired = true;
//	try {
//		Event["appointment_guild2"].getLocation = function () : GameObject {
//			Event["appointment_guild2"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_guild2"].locationObject != null) {
//				Event["appointment_guild2"].location = Event["appointment_guild2"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_guild2"].locationName =  Event["appointment_guild2"].location.locName;
//				return Event["appointment_guild2"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_guild2' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_guild2' location input does not resolve to an object (reference exception)");
//		Event["appointment_guild2"].location = null; Event["appointment_guild2"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_guild2"].probability = function () {
//		n = "appointment_guild2";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_guild2"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_guild2";
//			txt += "You meet with the representative of the Dacei Family.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_guild2"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_guild2";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_guild2"].outcomes = function () {
//		n = "appointment_guild2";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_guild2"].getAdvice = function () {
//		n = "appointment_guild2";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is Dacei Family.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is Dacei Family.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 13
//n = "appointment_guild3";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_guild3"].locationRequired) Event["appointment_guild3"].locationRequired = true;
//	try {
//		Event["appointment_guild3"].getLocation = function () : GameObject {
//			Event["appointment_guild3"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_guild3"].locationObject != null) {
//				Event["appointment_guild3"].location = Event["appointment_guild3"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_guild3"].locationName =  Event["appointment_guild3"].location.locName;
//				return Event["appointment_guild3"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_guild3' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_guild3' location input does not resolve to an object (reference exception)");
//		Event["appointment_guild3"].location = null; Event["appointment_guild3"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_guild3"].probability = function () {
//		n = "appointment_guild3";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_guild3"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_guild3";
//			txt += "You meet with the representative of the Coruna Cartel.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_guild3"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_guild3";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_guild3"].outcomes = function () {
//		n = "appointment_guild3";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_guild3"].getAdvice = function () {
//		n = "appointment_guild3";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is Coruna Cartel.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is Coruna Cartel.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 14
//n = "appointment_church";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_church"].locationRequired) Event["appointment_church"].locationRequired = true;
//	try {
//		Event["appointment_church"].getLocation = function () : GameObject {
//			Event["appointment_church"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_church"].locationObject != null) {
//				Event["appointment_church"].location = Event["appointment_church"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_church"].locationName =  Event["appointment_church"].location.locName;
//				return Event["appointment_church"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_church' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_church' location input does not resolve to an object (reference exception)");
//		Event["appointment_church"].location = null; Event["appointment_church"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_church"].probability = function () {
//		n = "appointment_church";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_church"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_church";
//			txt += "You meet with the representative of the Church.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_church"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_church";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_church"].outcomes = function () {
//		n = "appointment_church";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_church"].getAdvice = function () {
//		n = "appointment_church";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is the Church.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is the Church.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
// 
// 
//// NEW EVENT 15
//n = "appointment_heretic";
//eventCount += 1;
//if (Event.ContainsKey(n)) { Debug.LogError("ERROR: event "+n+" already excists."); }
//Event.Add(n, new eventClass());
//	Event[n].name				= n;
//	Event[n].available			= true;
//	Event[n].triggerType		= null;
//	Event[n].character			= null;
//	Event[n].advisor			= null;
//	Event[n].location			= null;
//	Event[n].locationName		= "empty";
//	Event[n].choice				= 0;
//	Event[n].outcome			= 0;
//	Event[n].dateLastDone 		= -1;
//
//	// Location
//	if (!Event["appointment_heretic"].locationRequired) Event["appointment_heretic"].locationRequired = true;
//	try {
//		Event["appointment_heretic"].getLocation = function () : GameObject {
//			Event["appointment_heretic"].locationObject = getLocation(LocationFinder.Instance.currentLocation);
//			if (Event["appointment_heretic"].locationObject != null) {
//				Event["appointment_heretic"].location = Event["appointment_heretic"].locationObject.gameObject.GetComponent(locationDatabase);
//				Event["appointment_heretic"].locationName =  Event["appointment_heretic"].location.locName;
//				return Event["appointment_heretic"].locationObject;
//			}
//			else { Debug.Log("PROBLEM: event 'appointment_heretic' location input does not resolve to an object (null returned)"); return null; }
//		};
//	}
//	catch (e : System.Exception) {
//		Debug.LogError("EXCEPTION: event 'appointment_heretic' location input does not resolve to an object (reference exception)");
//		Event["appointment_heretic"].location = null; Event["appointment_heretic"].locationName = "empty";
//		return null;
//	}
//
//
//	// Probability Variables
//	Event["appointment_heretic"].probability = function () {
//		n = "appointment_heretic";
//		var p1 : float = 1;
//			p1 = 0;
//		Event[n].lastProbability = 1 * p1;
//		return Event[n].lastProbability;
//	};
//
//
//	// Text field
//	Event["appointment_heretic"].getText = function () : String {
//		var txt : String = "";
//		n = "appointment_heretic";
//			txt += "You meet with the representative of the Radical Movement.";
//		return txt;
//	};
//
//
//	// choices
//	Event["appointment_heretic"].getChoices = function () : Dictionary.<String, int> {
//		var choices = new Dictionary.<String, int>();
//		n = "appointment_heretic";
//		choices.Add("Done", 1);
//		return choices;
//	};
//
//
//	// Outcomes
//	Event["appointment_heretic"].outcomes = function () {
//		n = "appointment_heretic";
//		var outcomes : int = 0;
//		if ( Event[n].choice == 1) {	
//		Event[n].outcome = 1;
//		outcomes ++;
//		/*Effect: $= */eventManager.inEvent = false; // EVENT CALLED TO AN END;
//		}
//		if (outcomes > 1) Debug.Log("PROBLEM: "+n+" had more than 1 outcome. Current choice: "+Event[n].choice);
//		if (outcomes < 1) Debug.Log("PROBLEM: "+n+" had no outcomes. Current choice: "+Event[n].choice);
//	};
//
//
//	// Get Advice
//	Event["appointment_heretic"].getAdvice = function () {
//		n = "appointment_heretic";
//		var advisor : String;
//
//		advice.Clear();
//
//		// advice:
//		advisor = characters.assignment["captain"]; 		
//		if (advisor != null) {
//			if (characters.character.ContainsKey(advisor) ) {
// 					if (advice.ContainsKey(advisor) ) Debug.LogError("ERROR: Got second opinion for same advisor: "+advisor+": 'Yes, this is Radical Movement.' @event: "+n);
//					advice.Add(advisor, new adviceClass() );
//					advice[advisor].opinion = "Yes, this is Radical Movement.";
//					advice[advisor].recommend = 0;
//			}
//		}
//	};
//// <-------------------------------------------------------------
//}
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
// 
//// Count events and triggers
//function splitDictionaries() {
//
//	// go through all triggerable events
//	for (var e : KeyValuePair.<String, eventClass> in Event){
//		if (e.Value.triggerType != null) {
//			triggerCount += 1; /**/eventCount -= 1;
//		}
//	}
//}
//// Tools
// 
// 
//function playerLoc(type : String, name : String) : boolean {
//	return finder.playerLoc(type, name); // returns "true" if palyer is in a confirmed location-state
//}
// 
// 
//function getObject(name : String) : GameObject {
//	return GameObject.Find(name);
//}
// 
// 
//// Character Tools
//function getBest(skill : String) : String {
//	return characters.getBest(skill);
//}
//function getBestFromAll(skill : String) : String {
//	return characters.getBestFromAll(skill);
//}
//function getAssigned(job : String) : String {
//	return characters.getAssigned(job);
//}
//function getSelected() : String {
//	return characters.selectedCharacter;
//}
// 
// 
//function getStat(characterValue : String) : float {
//	return characters.character[Event[n].character].getStat(characterValue);
//}
//function addStat(characterValue : String, amount : float) {
//	characters.character[Event[n].character].addStat(characterValue, amount);
//}
//function setStat(characterValue : String, amount : float) {
//	characters.character[Event[n].character].setStat(characterValue, amount);
//}
// 
// 
//// Advisor Tools
//function advisorSkill(advisorName : String, skill : String) : float {
//	return characters.character[advisorName].getStat(skill);
//}
// 
// 
//// Location Tools
//function getLocation(name : String) : GameObject {
//	return GameObject.Find(name);
//}
//function getLocation(loc : locationDatabase) : GameObject {
//	return loc.gameobject;
//}
//function getClosest(nth : int) : GameObject {
//	return collector.getClosestLocation(nth-1);
//}
//function getOneOfTheClosest(nth : int) : GameObject {
//	return collector.getClosestLocation(Mathf.Floor(Random.Range(0,nth-1)));
//}
//function getClosestByFaction(faction : String) : GameObject {
//	var loc : GameObject = collector.getClosestLocationByFaction(faction);
//	if (loc == null && eventManager.debugging) Debug.LogWarning("WARNING: No location controlled by "+faction+" found");
//	return loc;
//}
// 
// 
// 
// 
//// Content condition
//function statRoll(skill : String) : float {
//	return characters.statRoll(skill);
//}
//function assignmentRoll(assignment : String, skill : String)		{
//	return characters.assignmentRoll(assignment, skill);}
// 
// 
//// Content effects
//function assignCharacter(assignment : String)		{
//	characters.assign(Event[n].character, assignment);}
//function assign(name: String, assignment : String)		{
//	characters.assign(name, assignment);}
//function assignSelected(assignment : String)		{
//	characters.assign(characters.selectedCharacter, assignment);}
//function changeSituation(situation : status)		{
//	Event[n].situation = situation;}
// // PING event sound state about the change? 
//function changeAmbient(ambient : noise)		{
//	Event[n].ambient = ambient;}
// // PING event sound state about the change? 
//// 		location effects
//function basePopulationChange(ideology : String, value : float) {
//	if (!Event[n].locationRequired) Debug.LogError("BAD EVENT: event: "+n+" has no location but calls for one with 'basePopulationChange'");
//	Event[n].location.basePopulationChange(ideology, value);}
//function boostIdeology(ideology : String, value : float)		{
//	if (!Event[n].locationRequired) Debug.LogError("BAD EVENT: event: "+n+" has no location but calls for one with 'boostIdeology'");
//	Event[n].location.boostIdeology(ideology, value);}
//function factionChange(faction : String, value : float)		{
//	if (!Event[n].locationRequired) Debug.LogError("BAD EVENT: event: "+n+" has no location but calls for one with 'factionChange'");
//	Event[n].location.factionChange(faction, value);}
//function agendaChange(agenda : String, value : float)		{
//	if (!Event[n].locationRequired) Debug.LogError("BAD EVENT: event: "+n+" has no location but calls for one with 'agendaChange'");
//	Event[n].location.agendaChange(agenda, value);}
// 
// 
//function filterWeight(filter : String, value : float) {
//	filterWeights[filter] = value;
//}
// 
// 
