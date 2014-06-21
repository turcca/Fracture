//#pragma strict
//#pragma downcast
//import System.Collections.Generic;
//
//
//static var Instance : EventManager;
//
//// Player - LocationFinder (Collider / Finder)
//private var player : GameObject;
//private var finder : LocationFinder;
//
//// ScriptHolder 
//	private var scriptHolder : GameObject;
//	//private var nameGenerator : NameGenerator;
//	private var gameTime : GameTime;
//	private var collector : locationsDataCollector;
//	//private var ship : PlayerShipData;
//	//private var shipPop : ShipPopulation;
//	//private var economy : Economy;
//	private var event : Events;
//	private var characters : Characters;
//
//// eventUI
//	public var eventUI : GameObject;
//	public var locationUI : GameObject;
//
//var debugging : boolean;
//var overrideTestEvent : boolean = false;
//var testEventName : String = "Testevent";
//private var eventPicked : String;
//
//var inEvent : boolean = false;
//var choosing : boolean = false;
//var firstRun : boolean = true;
//
//private var keepTangent : boolean = false;
//
//// ***** Event spawner probability variables
//
//var eventInterval : float = 13;		// average days for an event (for time variable)
//var shipInterval : float = 1;		// 1 = 1/1000, 1,33 = 1/750 , 2 = 1/500, 3 = 1/333
//private var timePow : float = 2.0;
//
//var rareMaxWeight : float = 0.25;
//var ElevatedMinWeight : float = 0.25;
//var ProbableMinWeight : float = 0.55;
//
////Time
////time since last event
//private var lastEventDate : float = 0;
//private var lastEncounterDate : float = 0;
//
//private var eventChoiceMade : boolean = false;
//private var eventChoice : int = 0;
//
////warp weather var
////warp location var
////warp world state
//
//// *****************************************
// /*debugging*/ var averageEventeventInterval : float = 0;
// /*debugging*/ var eventeventInterval : List.<float>;
//
//
////*private*/ var triggersDone : List.<String> = new List.<String>();
//
//// ********* FUNCTIONS **********
//
//function Awake() {
//	EventManager.Instance = this;
//}
//
//function initializeEventManager() {
//
//	player = GameObject.FindGameObjectWithTag("Player");
//		finder = 	player.gameObject.GetComponent(LocationFinder);
//	
//	
//	scriptHolder = GameObject.Find("ScriptHolder");
//		//nameGenerator = scriptHolder.gameObject.GetComponent(NameGenerator);
//		gameTime 		= scriptHolder.gameObject.GetComponent(GameTime);
//		collector 		= scriptHolder.gameObject.GetComponent(locationsDataCollector);
//		//ship 			= scriptHolder.gameObject.GetComponent(PlayerShipData);
//		//shipPop 		= scriptHolder.gameObject.GetComponent(ShipPopulation);
//		//economy 		= scriptHolder.gameObject.GetComponent(Economy);
//		event 			= scriptHolder.gameObject.GetComponent(Events);
//		characters		= scriptHolder.gameObject.GetComponent(Characters);
//	
//	if (eventUI == null) eventUI = GameObject.Find("eventUI");
//	if (locationUI == null) locationUI = GameObject.Find("locationUI");
//}
//function eventChoiceReceiver(choice : int) {
//	if (choosing) { eventChoiceMade = true; eventChoice = choice; } }
//function eventChoiceDone() { eventChoiceMade = false; eventChoice = 0; }
//
//
//function eventSpawner() : boolean {		// rolls for an event from gameTime.runTime();
//
//					// ********************************************************************************************
//					if (overrideTestEvent && lastEventDate != gameTime.gameDay) {	// debug particular event
//						if (event.Event.ContainsKey(testEventName)) { /*Debug.Log("Test event key found");*/ 
//							event.Event[testEventName].getCharacter();	// loads character, if any
//							event.Event[testEventName].getLocation();	// loads location, if any
//							event.Event[testEventName].getAdvice();		// loads advice, if any
//							//lastEventDate = gameTime.gameDay;
//							//yield StartCoroutine(eventHandler(testEventName)); }
//							//if (debugging) Debug.Log("Override event: "+testEventName+", lastEventDate: "+lastEventDate+" / gameTime.gameDay: "+gameTime.gameDay);
//							overrideTestEvent = false;
//							eventHandler(testEventName); 
//							return true; }
//						else Debug.LogError("ERROR: manual Override Test Event: "+testEventName+" not found in events");
//					}
//					// ********************************************************************************************
//	
//	var probability : float;	// percentage of an event each "timeUpdateeventInterval" * "timeMultiplier"
//
//	var cumulativeTime = function () {
//		var d : float = gameTime.gameDay - lastEventDate;	// days since last event
//		d = Mathf.Pow(d/(eventInterval*1.35), timePow) / (eventInterval*1.35);
//		//Debug.Log("		Event chance: "+Mathf.Round(d*1000)/1000);
//		return d;
//	};
//
//	// Account for all variables to
//	probability = cumulativeTime() * 1.0;	// ADD ALL VARIABLES (environmental etc.)
//
//	// Roll for event
//	var roll : float = Random.value;
//	
//	// IF SUCCESSFUL, PICK EVENT AND RUN
//	if (roll < probability * gameTime.timeMultiplier) {		// If time is altered, probability is up/down accordingly
//		
//		
//		
//			if (debugging) {
//				//Debug.Log(gameTime.gameDay - lastEventDate+" since last event");
//				eventeventInterval.Add(gameTime.gameDay - lastEventDate);
//				averageEventeventInterval = 0;
//				for (var f : float in eventeventInterval) {
//					averageEventeventInterval += f;
//				}
//				averageEventeventInterval /= eventeventInterval.Count;
//				eventeventInterval.Sort();
//				Debug.Log("Average eventInterval: "+averageEventeventInterval+"  Lowest / highest: "+Mathf.Round(eventeventInterval[0])+" / "+Mathf.Round(eventeventInterval[eventeventInterval.Count-1]));
//		}
//		var e : String = pickEvent();
//		
//		// if null returned, no events have probability or out of events
//		if (e == null) {
//			if (debugging) Debug.Log("No events available to spawn - no probability or out of events");
//			return;
//		}
//
//		
//		// check that 'e' is legitime
//		if (event.Event.ContainsKey(e) ) { 
//			//event.Event[e].getAdvice();
//			eventHandler(e);
//		}
//		else {
//			Debug.LogWarning("ERROR: invalid key for events.event: '"+e+"'  ");
//			return;
//		}
//	}
//	return false;
//}
//
//
//function pickEvent() : String {			// chooses an event from available event pool
//	
//		
//	var available : List.<String> = new List.<String>();
//	var picked : String = "";
//	var adder : float;
//	var roll : float = Random.value;
//	
//	var charReturn : String;
//	var locReturn : GameObject;
//	//var locCheck : GameObject; 	in case (GameObject.Find(locReturn) != null) -check doesn't work, work around it
//
//	var charOk : boolean;
//	var locOk: boolean;
//
//	// go through all available, non-trigger events
//	for (var e : KeyValuePair.<String, eventClass> in event.Event){
//		//if (debugging) { Debug.Log("Anaylyzing if \""+e.Value.name+"\" is \"available\":"); }
//
//		// Availability Filter (exclude trigger-events)
//		if (e.Value.available && e.Value.triggerType == null) {	//if (debugging) { Debug.Log(" ---> it is!"); }
//			charOk = false;
//			locOk = false;
//			charReturn 	= e.Value.getCharacter();	// loads character, if any. Checks if it's valid
//			locReturn 	= e.Value.getLocation();	// loads location, if any
//			//e.Value.getAdvice();					// loads advice, if any
//
//			// checks if charCheck is ok 	[checks if name was given and it is in character dictionary & if that char is active]
//			if (charReturn == null) charOk = true;
//			else {
//				if (characters.character.ContainsKey(charReturn) && characters.character[charReturn].isActive) charOk = true;
//			}
//
//			// if charCheck is ok, continue to loc check [checks if location is required, and if an excisting location is returned]
//			if (charOk) {
//				if (!e.Value.locationRequired) locOk = true;	// if location not required, ok
//				else if (locReturn != null && GameObject.Find(locReturn.name) != null) locOk = true;
//			}
//
//			// if charOk and locOk, add event to available pool
//			if (charOk && locOk) {
//				available.Add(e.Value.name);	// record name for available characters
//				adder += e.Value.probability();	// probability() += adder (also assigns it as last probability) 
//				//if (debugging) Debug.Log("Cumulative brobability at "+e.Value.name+": "+adder);
//				//if (debugging) Debug.Log("added last probability: "+e.Value.lastProbability);
//			}
//		}
//	}
//	// return if probabilities at 0 - analyse and report
//	if (adder == 0) {
//		if (available.Count == 0) { 
//			if (debugging) Debug.Log("NOTE: zero events picked.");
//		}
//		else Debug.LogWarning("WARNING: zero-pribability events picked.");
//		return null; 
//	} 
//	
//
//	// Influence pool weights by event frequencies
//		// only if more than one event picked
//		var count : int = available.Count;
//		if (count > 1) {
//			var rareCap 	: float = adder * rareMaxWeight;
//			var elevatedMin : float = adder * ElevatedMinWeight;
//			var probableMin : float = adder * ProbableMinWeight;
//			// go through available events
//			for (var i : int; i < count; i++) {
//				// see if special frequency
//				if (event.Event[available[i]].frequency != freq.Default) {
//					// if Rare
//					if (event.Event[available[i]].frequency == freq.Rare) {
//						// if adjustment is needed
//						if (event.Event[available[i]].lastProbability > rareCap) {
//							adder = adder - event.Event[available[i]].lastProbability + rareCap;
//							event.Event[available[i]].lastProbability = rareCap;
//						}
//					}
//					// if Elevated
//					if (event.Event[available[i]].frequency == freq.Elevated) {
//						// if adjustment is needed
//						if (event.Event[available[i]].lastProbability < elevatedMin) {
//							adder = adder - event.Event[available[i]].lastProbability + elevatedMin;
//							event.Event[available[i]].lastProbability = elevatedMin;
//						}
//					}
//					// if Probable
//					if (event.Event[available[i]].frequency == freq.Probable) {
//						// if adjustment is needed
//						if (event.Event[available[i]].lastProbability < probableMin) {
//							adder = adder - event.Event[available[i]].lastProbability + probableMin;
//							event.Event[available[i]].lastProbability = probableMin;
//						}
//					}
//				}
//			}
//		}
//
//
//	roll *= adder;
//	adder = 0;
//	
//	
//	// start adding last probabilities until roll > adder  --> event piced
//	for (var e : String in available) {
//		//Debug.Log("Adding: "+event.Event[e].lastProbability);
//		adder += event.Event[e].lastProbability;
//		if (adder >= roll) {
//			picked = e;
//			break;
//		}
//	}
//	//Debug.Log("Roll: "+roll);
//	//Debug.Log("Adder reached: "+adder);
//	available.Clear();
//
//	if (picked == "") { Debug.LogWarning("NOTE: No event was picked"); }
//	if (debugging) Debug.Log("Event Picked: \""+picked+"\", days since last event: "+ Mathf.Round(gameTime.gameDay - lastEventDate));
//
//	lastEventDate = gameTime.gameDay;	// assign date to mark last event spawned
//
//	return picked;
//}
//
//
//// *****************Event Handler********************************************************************************************************
//
//function eventHandler(e : String) {		// event UI mechanics
//	
//	//if (debugging) Debug.Log("Entering eventHandler: "+e+".");
//	if (e == "") Debug.LogError("ERROR: event name was empty.");
//	if (e == null) Debug.LogError("ERROR: event name was 'null'.");
//	if (!event.Event.ContainsKey(e)) { Debug.LogError("ERROR: There is no event called \""+e+"\"."); return; }
//
//	var i : int = 0;
//	choosing = false;
//	inEvent = true;
//	firstRun = true;
//
//	// Pause Game
//	gameTime.pauseGame();
//
//	//event.Event[e].getAdvice(); // called in eventUI
//
//
//	// Go through event steps, until "end" is called (inEvent = false)
//	while (inEvent) {
//		i++;
//		
//		// if first run, sneak some calculations
//		if (firstRun) {
//			// *************************************
//			collector.sectorSimulationCycle();		// update production on all locations!
//			// *************************************
//		}
//		// location UI off (eventState on turns off other locationUI elements)
//		if (finder.inLocation) locationUI.SendMessage("loadEventState", true);
//
//		// eventUI on
//		eventUI.SendMessage("eventUI", event.Event[e]); // tarvii refreshaa portraittien sisältö, mutta älä uudelleenluo portraitteja joka kerta?
//		
//
//		// Wait for player to choose
//		choosing = true;	
//		while(choosing) {
//			if (eventChoiceMade) {
//				if (eventChoice == 0) Debug.LogError("ERROR: eventChoice was 0 while eventChoiceMade was true.");
//				// mark event choice
//				event.Event[e].choice = eventChoice;
//				// assign selected character as acting advisor in the event
//				if (firstRun && event.Event[e].advisor == null) event.Event[e].advisor = characters.selectedCharacter;
//				choosing = false;
//				firstRun = false;
//				eventChoiceDone();
//			}
//			yield;
//		}
//		
//		// Outcome
//		characters.roll = Random.value *100;
//		event.Event[e].outcomes();
//		//if (debugging) Debug.Log("Outcome value is "+event.Event[e].outcome+"  choice was: "+event.Event[e].choice+"  inEvent: "+inEvent+"  [iteration:"+i+"]");
//		
//		
//		if (!inEvent) break;
//		if (i > 100) { Debug.Log("ERROR: inEvent loop is stuck - ending process"); break; };
//		yield;
//	}
//	// location on (eventState off brings location back to .Main state)
//	if (finder.inLocation) locationUI.SendMessage("loadEventState", false);
//
//	// eventUI off
//	eventUI.SendMessage("eventUIOff");
//
//	// in Fracture, unpause
//	if (!finder.inLocation) gameTime.unPauseGame();
//}
//
//
//
//// IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
//// *****************Ship encounters************************
//
//function shipEncounter() {
//	
//	// Trade routes
//	var tradeRoutes = function() : float {
//		if (finder.onTraderoute) return 1;
//		else return 0.2;
//	};
//	/*/ Local warp magnitude
//	var warpMagnitude = function() : float {
//		//return Mathf.Min( Mathf.Pow((1.2-finder.magnitude/7), 6), 1);
//		return 1;
//	};*/
//	
//	// Naval Presence, probability of ship encounter
//	var navalPresence = function() : float {
//		var p : float;
//		//if (finder.onTraderoute) p = collector.presentNavalMilitaryPresence + collector.presentNavalCommercePresence;
//		/*else*/ p = collector.presentNavalMilitaryPresence*Mathf.Min( Mathf.Pow((1.2-finder.magnitude/7), 5), 1) + collector.presentNavalCommercePresence*Mathf.Min( Mathf.Pow((1.2-finder.magnitude/6.5), 5), 1);
//		return p*shipInterval / 1000 /100;
//	};
//	
//	var lastEncounter = function() : float {
//		var daysSince : float = gameTime.gameDay - lastEncounterDate;
//		
//		if (daysSince <= 6) return Mathf.Pow(daysSince/6, 2);
//		if (daysSince > 20) return daysSince/10 -1;
//		return 1;
//	};
//	
//	var probability = function() : float { return (tradeRoutes() /* warpMagnitude()*/ * navalPresence() * lastEncounter() ); };
//
//	// Roll for encounter
//	//var roll : float = Random.value;
//	//Debug.Log("Roll ship encounter: "+Mathf.Round(roll*100)/100+" / "+Mathf.Round(probability()*100)/100 );
//	if (Random.value < probability() * gameTime.timeMultiplier ) {		// If time is altered, probability is up/down accordingly
//		// Hunters
//			// Other Privateers		[Individual]				wealth/strength
//			// Local authorities 	[Planetary]					local infamy
//			// Faction grudge		[Faction]					faction infamy
//			// Enemy of Empire		[Sector, Capital Forces]	average infamy
//			
//		// Pick ship & origins
//		collector.findShip();
//		
//		// foundShipMilitary : boolean;
//		// foundShipOrigins : GameObject;
//		// foundShipExport	: boolean;
//		// foundShipLegality : int;
//		// foundShipClass : int;
//		// foundShipCargo : ??
//		
//		// Encounter Handler
//		lastEncounterDate = GameTime.gameDay;
//		
//		encounterHandler();
//	}
//}
//
//
//function encounterHandler() {
//	
//	// Pause Game, bring UI
//	gameTime.pauseGame();
//	
//	
//	// *************************************
//	collector.sectorSimulationCycle();		// update production on all locations!
//	// *************************************
//	
//	
//	var locationData : GameObject = collector.foundShipOrigins;
//	var location : locationDatabase = locationData.gameObject.GetComponent(locationDatabase);		
//	
//	var roll : float = Random.value;
//	
//	var choice : int = 0;
//	var eChoice : int = 0;
//	
//	var playerInitiative : float = 0;
//	var encounterInitiative : float = 0;
//	var playerWinsInitiative : boolean;
//	
//	var delay : float = 0;
//	var a : int;
//	
//	choosing = false;
//	
//	keepTangent = false;
//	
//	var endEncounter = function() {
//		// record worldstate / gamedata
//		
//		// timeDelay
//		timeDelay(delay);
//	
//		// Reset collector.shipFound data
//		
//		// Close UI, unpause game
//		gameTime.unPauseGame();
//		
//		// keep tangent
//		if (keepTangent) { Debug.Log("Lisää aluksen tangentin säilyminen pausesta"); }
//		//else Debug.Log("Lisää aluksen tangentin säilyminen pausesta");
//	};
//	
//	var choose = function() {
//		if (choice == 1) { combat(playerWinsInitiative); endEncounter(); return; }
//		if (choice == 2) {  }
//		if (choice == 3) {  }
//		if (choice == 4) { endEncounter(); return; }
//	};
//	
//		
//		var encounterAgendaClear : boolean = false;
//		
//		var recognizesPlayer : boolean = true;
//		var isIllegal		: boolean = true;
//		var isHostile 		: boolean = false;	// is ship hostile?
//		var isWar			: boolean = false;	// is it ok for the ship to just kill? Is it war zone? (is it in war, pirate alert, and military presence)
//		var isOnMission		: boolean = false;	// is ship on crucial mission?
//		var riskAnalysis = function () {
//			return true;	// make ship-size comparison along with reputation and military background and violence level
//		};
//		var canTakePlayer 	: boolean = riskAnalysis();	// can encountering ship take on the player?
//		
//		// isIllegal?
//		if (collector.foundShipLegality > 4) isIllegal = false;
//		// isWar?
//		
//		// Cargo
//			// isOnMission?
//		
//		
//	//Debug.Log("SHIP ENCOUNTER");	
//	
//	
//	// Determin other ship's action (eChoice)
//	while (true) {
//		//Debug.Log("eChoice processing");
//		if (collector.foundShipMilitary && !isOnMission) {	
//			eChoice = 1;
//			break;
//		}
//		if ((!collector.foundShipMilitary || collector.foundShipLegality <= 4) && !isOnMission && !isWar) {
//			eChoice = 2;
//			break;
//		}
//		if (!collector.foundShipMilitary || collector.foundShipLegality > 3 || isOnMission) {
//			eChoice = 3;
//			break;
//		}
//		else { Debug.LogError("ERROR: unable to determin other ship's actions."); break; }
//	}
//	
//	
//	// CHOICE 1
//
//	
//	var txt1 : String = "We've detected a warp beacon. Should we investigate?";
//	var txt2 : String = null;
//	
//	var choice1 : String = "1. Investigate at once.";
//	var choice2 : String = "2. Keep the course.";
//	var choice3 : String = "3. Take time to avoid being detected.";
//	var choice4 : String = null;
//	
//	
//	// Introduce ship encounter
//	if (debugging) { Debug.Log(txt1); }
//	
//	if (debugging) { Debug.Log(choice1); }
//	if (debugging) { Debug.Log(choice2); }
//	if (debugging) { Debug.Log(choice3); }
//
//
//	// Wait for player to choose
//		choosing = true;		
//			while (choosing) {				// Wait for user input
//				//j++;
//				for (var c : char in Input.inputString) {
//					if (c >= 49 && c <= 51) {		// ACII decimal of 1-9
//						a = c; a -= 48;							// transform char to int
//						//var ctxt : String = choiceText[a];
//						choice = a;	// assign choice value to Event[e].choice
//						//Debug.Log("chose "+a);
//						choosing = false;
//						break;
//					}
//					// if mouse select
//				}
//				//if (!choosing) break;
//				yield;
//			}	
//				
//	
//	// encounter MATRIX
//	
//	txt1 = null;
//	txt2 = null;
//	
//	choice1 = null;
//	choice2 = null;
//	choice3 = null;
//	choice4 = null;
//	// --------------------------------------------------------------------------------------------------
//		// Player investigates - encounter investigates
//		if (choice == 1 && eChoice == 1) {
//			playerInitiative = 1; encounterInitiative = 1;
//			// Initiative check
//				characters.roll = Random.value * 100;
//				if (characters.assignmentRoll("navigator", "navigation") * playerInitiative > 333 * encounterInitiative) { 
//					playerWinsInitiative = true; 
//					txt1 = "You investigate the beacon. ";
//					// Continue to Hailing
//				}
//				else {
//					playerWinsInitiative = false;
//					// See the intentions of the other ship
//					txt1 = "You are being contacted. ";
//					// Continue to Hailing
//					if (debugging) Debug.Log("	debugging: e loses init");
//				}
//			
//		}
//		
//		// Player investigates - encounter keeps course
//		if (choice == 1 && eChoice == 2) {
//			playerInitiative = 1; encounterInitiative = 0.4;
//			
//			// Initiative check
//				characters.roll = Random.value * 100;
//				if (characters.assignmentRoll("navigator", "navigation") * playerInitiative > 333 * encounterInitiative) { 
//					playerWinsInitiative = true; 
//					txt1 = "You investigate the beacon. [cc]";
//					//if (debugging) { Debug.Log(txt1); }
//					// Continue to Hailing
//				}
//				else {
//				// END
//					playerWinsInitiative = false;
//					if (characters.assignment["navigator"] != null) { txt1 = "You try to track the beacon, but "+characters.character[characters.assignment["navigator"]].name+" loses it eventually. "; }
//					else txt1 = "You try to track the beacon, but without a navigator it quickly becomes impossible.";
//					// Show description and choices
//					if (debugging) { Debug.Log(txt1); }
//					if (debugging) { Debug.Log("1. Continue."); }
//						// Wait for player to continue
//							choosing = true;		
//								while (choosing) {				// Wait for user input
//									//j++;
//									for (var c : char in Input.inputString) {
//										if (c >= 49 && c <= 49) {		// ACII decimal of 1-9
//											a = c; a -= 48;							// transform char to int
//											//var ctxt : String = choiceText[a];
//											//choice = a;	// assign choice value to Event[e].choice
//											choosing = false;
//											endEncounter();
//											return;
//										}
//										// if mouse select
//									}
//									//if (!choosing) { endEncounter(); return; }
//									yield;
//								}	
//				}
//			
//		}
//		
//		// Player investigates - encounter avoids
//		if (choice == 1 && eChoice == 3) {
//			playerInitiative = 1; encounterInitiative = 0.9;
//			
//			// Initiative check
//				characters.roll = Random.value * 100;
//				if (characters.assignmentRoll("navigator", "navigation") * playerInitiative > 333 * encounterInitiative) { 
//					playerWinsInitiative = true; 
//					txt1 = "You investigate the beacon. [e]";					
//				}
//				if (characters.assignmentRoll("navigator", "navigation") * playerInitiative > 333 * encounterInitiative +150) { 
//					txt1 = "You investigate the beacon. It seems to be avoiding contact. ";
//				}
//				// Continue to Hailing
//				
//				else {
//				// END
//					playerWinsInitiative = false;
//					if (characters.assignment["navigator"] != null) { txt1 = "You try to track the beacon, but "+characters.character[characters.assignment["navigator"]].name+" loses it eventually. "; }
//					else txt1 = "You try to track the beacon, but without a navigator it quickly becomes impossible.";
//					if (debugging) { Debug.Log(txt1); }
//					if (debugging) { Debug.Log("1. Continue."); }
//						// Wait for player to continue
//							choosing = true;		
//								while (choosing) {				// Wait for user input
//									//j++;
//									for (var c : char in Input.inputString) {
//										if (c >= 49 && c <= 49) {		// ACII decimal of 1-9
//											a = c; a -= 48;							// transform char to int
//											//var ctxt : String = choiceText[a];
//											//choice = a;	// assign choice value to Event[e].choice
//											choosing = false;
//											endEncounter();
//											return;
//										}
//										// if mouse select
//									}
//									if (!choosing) { endEncounter(); return; }
//									yield;
//								}	
//				}
//		}
//		
//		
//		// Player keeps course - encounter investigates
//		if (choice == 2 && eChoice == 1) {
//			playerInitiative = 0.4; encounterInitiative = 1;
//			
//			// Initiative check
//				characters.roll = Random.value * 100;
//				if (characters.assignmentRoll("navigator", "navigation") * playerInitiative > 333 * encounterInitiative) { 
//					playerWinsInitiative = true; 
//					// keep tangent
//					keepTangent = true;
//					// END
//					endEncounter();
//					return;
//					
//				}
//				else {									// ENCOUNTER SHIP INTENTIONS !!!!!!!!
//					playerWinsInitiative = false;
//					// See the intentions of the other ship
//					txt1 = "The beacon intercepts us. ";
//					//if (debugging) { Debug.Log(txt1); }
//
//				}
//						
//		}
//		
//		// Player keeps course - encounter keeps course
//		if (choice == 2 && eChoice == 2) {
//			// keep tangent
//			keepTangent = true;
//			// END
//			endEncounter();
//			return;
//		}
//		
//		// Player keeps course - encounter avoids
//		if (choice == 2 && eChoice == 3) {
//			// keep tangent
//			keepTangent = true;
//			// END
//			endEncounter();
//			return;
//		}
//		
//				
//		// Player avoids - encounter investigates
//		if (choice == 3 && eChoice == 1) {
//			playerInitiative = 0.9; encounterInitiative = 1;
//			
//			// Initiative check
//				characters.roll = Random.value * 100;
//				if (characters.assignmentRoll("navigator", "navigation") * playerInitiative > 333 * encounterInitiative) { 
//					playerWinsInitiative = true; 
//					delay = calcTimeDelay(8);
//					if (characters.assignment["navigator"] != null) { txt1 = "All noncritical systems are shut down, and "+characters.character[characters.assignment["navigator"]].name+" quietly lets the ship drift away. "; }
//					else txt1 = "Without a navigator, you can only power your systems down and hope for the best.";
//				}	
//				if (characters.assignmentRoll("navigator", "navigation") * playerInitiative > 333 * encounterInitiative +100) { 
//					playerWinsInitiative = true; 
//					delay = calcTimeDelay(6);
//					if (characters.assignment["navigator"] != null) { txt1 = "You shut down noncritical systems, and "+characters.character[characters.assignment["navigator"]].name+" quietly lets the ship drift away. "; }
//					else txt1 = "Without a navigator, you can only power your systems down and hope for the best.";
//				}
//				if (characters.assignmentRoll("navigator", "navigation") * playerInitiative > 333 * encounterInitiative +200) { 
//					playerWinsInitiative = true; 
//					delay = calcTimeDelay(4);
//					if (characters.assignment["navigator"] != null) { txt1 = characters.character[characters.assignment["navigator"]].name+" navigates the ship with grace and purpose. "; }
//					else txt1 = "Even without a navigator, your crew handles the situation admirably.";
//				}							
//				if (playerWinsInitiative) {	
//					if (delay == 0) 			{ txt1 += " You evade any contact, and no time was lost."; }
//					if (delay > 0 && delay <3) 	{ txt1 += " After a few days the ship is back on course."; }
//					if (delay >= 3 && delay <7) { txt1 += " It took almost a week to get the ship back on course safely."; }
//					if (delay >= 7 && delay <14){ txt1 += " Over a week later, the ship manages to find its way back from hiding."; }
//					if (delay >= 14)			{ txt1 += " After weeks hiding in the warp and losing cource, the ship is back on course."; }
//
//
//					if (debugging) { Debug.Log(txt1); }
//					if (debugging) { Debug.Log("1. Continue."); }	
//					// END
//						// Wait for player to continue
//							choosing = true;		
//								while (choosing) {				// Wait for user input
//									//j++;
//									for (var c : char in Input.inputString) {
//										if (c >= 49 && c <= 49) {		// ACII decimal of 1-9
//											a = c; a -= 48;							// transform char to int
//											//var ctxt : String = choiceText[a];
//											//choice = a;	// assign choice value to Event[e].choice
//											choosing = false;
//											endEncounter();
//											return;
//										}
//										// if mouse select
//									}
//									if (!choosing) return;
//									yield;
//								}					
//				}
//				else {									// ENCOUNTER SHIP INTENTIONS !!!!!!!!
//					playerWinsInitiative = false;
//					// See the intentions of the other ship
//					txt1 = "The beacon intercepts us. ";
//					if (characters.assignmentRoll("navigator", "navigation") * playerInitiative +150 < 333 * encounterInitiative) txt1 = "The beacon quickly intercepts us. ";
//				}			
//		}
//		
//		// Player avoids - encounter keeps course
//		if (choice == 3 && eChoice == 2) {
//			// END
//			delay = calcTimeDelay(4);/*
//					if (delay == 0) 			{ txt1 += " You evade any contact, and no time was lost."; }
//					if (delay > 0 && delay <3) 	{ txt1 += " After a few days the ship is back on course."; }
//					if (delay >= 3 && delay <7) { txt1 += " It took almost a week to get the ship back on course safely."; }
//					if (delay >= 7 && delay <14){ txt1 += " Over a week later, the ship manages to find its way back from hiding."; }
//					if (delay >= 14)			{ txt1 += " After weeks hiding in the warp and losing cource, the ship is back on course."; }*/
//			endEncounter();
//			return;
//		}
//		
//		// Player avoids - encounter avoids
//		if (choice == 3 && eChoice == 3) {
//			// END
//			delay = calcTimeDelay(3);/*
//					if (delay == 0) 			{ txt1 += " You evade any contact, and no time was lost."; }
//					if (delay > 0 && delay <3) 	{ txt1 += " After a few days the ship is back on course."; }
//					if (delay >= 3 && delay <7) { txt1 += " It took almost a week to get the ship back on course safely."; }
//					if (delay >= 7 && delay <14){ txt1 += " Over a week later, the ship manages to find its way back from hiding."; }
//					if (delay >= 14)			{ txt1 += " After weeks hiding in the warp and losing cource, the ship is back on course."; }*/
//			endEncounter();
//			return;
//		}
//		
//		if (choice == 0 || eChoice == 0) {
//			if (choice == 0) 	Debug.LogError("ERROR: Player choice was 0");
//			if (eChoice == 0) 	Debug.LogError("ERROR: eChoice was 0");
//		}
//		
//		// --------------------------------------------------------------------------------------------------
//		
//		// Description
//		txt1 += "It is a";
//		/*txt1 += " a large";	txt1 += " a small";*/ // "" for mid-sized							// optional	1
//		if (collector.foundShipMilitary) txt1 += " military";	else txt1 += " commercial";			// optional	2
//		txt1 += " ship";		
//		txt1 += " from "+location.locName;															// optional	4
//		if (location.controlledBy != null) txt1 += ", belonging to "+WorldState.faction[location.controlledBy].names[0]+"";		// optional	3
//		else 
//		txt1 += ".";
//		
//		//	txt1 += "It is +a large/a /a small+ + military/-/commercial + ship +from location+ +with unknown/faction markings+.";
//		
//		
//		// ENCOUNTER SHIP GOALS
//
//		// -------------------------->
//			// Hostile
//			if (canTakePlayer && (isWar || isHostile)) {
//				encounterAgendaClear = true;
//				// Attack
//				if (isWar && canTakePlayer) {
//					combat(playerWinsInitiative); endEncounter(); return;
//				}
//				// Demand Contact
//				else {
//					// check if player has several cargo items		!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//					if (!isWar) {
//						// Threaten/Loot (attack if not complying)
//						txt2 = "RADIO MESSAGE: \"You are to drop your cargo and leave, or face the consequences.\"";
//							choice1 = "1. Attack.";
//							choice2 = "2. Refuse to pay.";
//							choice3 = "3. [Drop all of your cargo]";
//							choice4 = null;
//							choose = function() {
//								if (choice == 1) { combat(playerWinsInitiative); endEncounter(); return; }
//								if (choice == 2) { if (isWar) { combat(playerWinsInitiative); endEncounter(); return; } if (!isWar ) { txt2 = "RADIO: \"All right then, beat it.\""; endEncounter(); return; } } // will they let go in any case?
//								if (choice == 3) {  }
//							};
//					}
//					// Let go
//					else {
//						txt2 = "RADIO MESSAGE: \"You are of no interest to us, keep clear.\"";
//							choose = function () {
//						if (choice == 1) { combat(playerWinsInitiative); endEncounter(); return; }
//						if (choice == 2) { endEncounter(); return; }
//						};
//						choice1 = "1. Attack";
//						choice2 = "2. Continue.";
//						choice3 = null;
//						choice4 = null;
//						}
//				}
//			}		
//
//			// Evasive
//			if (((isHostile || isWar) && !canTakePlayer) || (isIllegal && !collector.foundShipMilitary) && !encounterAgendaClear) { // onkohan fiksut ehdot?
//				encounterAgendaClear = true;
//				if (!playerWinsInitiative) { endEncounter(); return; } // END
//				// radio silence
//				if (canTakePlayer) {
//					if (recognizesPlayer) { txt2 = "They're running on radio silence."; }
//					else txt2 = "RADIO REPLY: \"Keep clear, we have no business with you.\"";
//					
//					choose = function () {
//						if (choice == 1) { combat(playerWinsInitiative); endEncounter(); return; }
//						if (choice == 2) { endEncounter(); return; }
//					};
//					choice1 = "1. Attack";
//					choice2 = "2. Continue.";
//					choice3 = null;
//					choice4 = null;
//					// proceed to Hailing
//				}
//				// willing to contact
//				else {
//					// contact
//					isHostile = false;
//					// proceed to Contact
//				}
//			}
//
//			// Contact
//			if (!isHostile /*|| !encounterAgendaClear*/) {
//				encounterAgendaClear = true;
//				// Police
//				if (collector.foundShipMilitary) {
//					// Inspection
//					if (canTakePlayer) {   /*|| !inGoodStanding*/
//						// if not in good standing
//						if (location.controlledBy != null) txt2 = "\"This is the "+WorldState.faction[location.controlledBy].names[0]+" Security from "+location.locName+"! Your cargo is to be inspected of anything illegal. Cooperate or be boarded.\"";
//						else txt2 = "\"This is the security from "+location.locName+"! Your cargo is to be inspected of anything illegal. Cooperate or be boarded.\"";
//						choose = function() {
//							if (choice == 1) { combat(playerWinsInitiative); endEncounter(); return; }
//							if (choice == 2) {  }
//							if (choice == 3) {  }
//							if (choice == 4) {  }
//						};
//						choice1 = "1. Attack.";
//						choice2 = "2. Allow the inspection.";
//						choice3 = "3. Attempt to bribe. [200]";		// increase if known smuggler
//						choice4 = "4. Try to leave.";
//						// Inspection check
//						
//					}
//					// Warn
//					else { 
//						if (location.controlledBy != null) txt2 = "\"This is the "+WorldState.faction[location.controlledBy].names[0]+" Security from "+location.locName+"! Be warned, there are smugglers and pirates around.\"";
//						else txt2 = "\"This is the security from "+location.locName+"! Be warned, there are smugglers and pirates around.\"";
//						choose = function () {
//							if (choice == 1) { combat(playerWinsInitiative); endEncounter(); return; }
//							if (choice == 2) { endEncounter(); return; }
//						};
//						choice1 = "1. Attack.";
//						choice2 = "2. Continue.";
//						choice3 = null;
//						choice4 = null;
//					}
//				}
//				// News
//				else {
//					// proceed to Hailing
//				}
//			}
//		if (!encounterAgendaClear) { 
//			Debug.LogError("ERROR: No encounter Agenda for encounter -ship.");
//			Debug.LogError("isHostile: "+isHostile+"  isWar: "+isWar+"  recognizesPlayer: "+recognizesPlayer+"  isIllegal: "+isIllegal+"  canTakePlayer: "+canTakePlayer);
//		}
//
//		// --------------------------<
//
//
//			
//		// CHOICE 2
//			//if (debugging) Debug.Log("DESCRIPTION:");
//	
//			if (debugging) Debug.Log(txt1);
//	
//			if (choice1 == null && choice2 == null && choice3 == null && choice4 == null) {
//				choice1 = "1. Attack!";
//				choice2 = "2. Demand all cargo.";
//				choice3 = "3. Trade news.";
//				choice4 = "4. Continue.";
//				
//				choose = function() {
//					if (choice == 1) { combat(playerWinsInitiative); endEncounter(); return; }
//					if (choice == 2) { 
//										if (!canTakePlayer) { 													
//															if (isHostile && isWar) { txt2 = "RADIO: \"We will cooperate. Just let us go.\""; /* ADD CONSEQUENCES*/ }
//															if (isHostile) 			{ txt2 = "RADIO: \"We bend to your demands, pirate.\""; /* ADD CONSEQUENCES*/ }
//															if (!isHostile && collector.foundShipMilitary) { txt2 = "RADIO: \"We will comply, but you have a lot to explain at "+location.locName+".\""; /* ADD CONSEQUENCES*/ }
//															else 					{ txt2 = "RADIO: \"Very well, but we will protest against your actions.\""; /* ADD CONSEQUENCES*/ }
//															// transfer cargo ()
//															}
//										else { txt2 = "RADIO: \"You will regret that, pirate!\""; combat(playerWinsInitiative); endEncounter(); return; }
//									}
//					if (choice == 3){ 
//										if (isWar || isHostile) { if (canTakePlayer) txt2 = "We have nothing to say to you, pirate."; else txt2 = "We have nothing right now."; }
//										else 					{ txt2 = "RUMOUR MILL"; }
//									}
//					if (choice == 4) { endEncounter(); return; }
//				};	
//			}
//		
//		// Hailing
//		if (debugging) Debug.Log("HAILING:");
//		if (debugging && txt2 != null) Debug.Log(txt2);
//			
//			if (debugging && choice1 != null) Debug.Log(choice1);
//			if (debugging && choice2 != null) Debug.Log(choice2);
//			if (debugging && choice3 != null) Debug.Log(choice3);
//			if (debugging && choice4 != null) Debug.Log(choice4);
//			
//			var count : int = 0;
//			if (choice1 != null) count++; if (choice2 != null) count++; if (choice3 != null) count++; if (choice4 != null) count++; 
//			if (count == 0) { Debug.LogError("ERROR: 0 choices available"); return; }
//			
//				// Wait for player to choose
//					choosing = true;		
//						while (choosing) {				// Wait for user input
//							//j++;
//							for (var c : char in Input.inputString) {
//								if (c >= 49 && c <= 48+count) {		// ACII decimal of 1-9
//									a = c; a -= 48;							// transform char to int
//									//var ctxt : String = choiceText[a];
//									choice = a;	// assign choice value to Event[e].choice
//									choosing = false;
//									choose();
//									break;
//								}
//								// if mouse select
//							}
//							if (!choosing) break;
//							yield;
//						}	
//
//		// Resolving conflict further
//		
//				
//	// If still unresolved
//	
//	
//	if (debugging) { Debug.Log("Ship debug: ");
//		if (location.controlledBy != null) { Debug.Log("Found ship of the "+WorldState.faction[location.controlledBy].names[0]+" from "+collector.foundShipOrigins+"  Military: "+collector.foundShipMilitary+"  Legality: "+collector.foundShipLegality); }
//		else Debug.Log("Found ship from "+collector.foundShipOrigins+"  Military: "+collector.foundShipMilitary+"  Legality: "+collector.foundShipLegality);
//	}
//	
//	endEncounter();
//}
//
//function calcTimeDelay(maxDays : int) : float {
//	
//	var val : float;
//	
//	val = maxDays;
//	//characters.roll = Random.value * 100;
//	val -= characters.assignmentRoll("navigator", "navigation")/100;
//	if (val < 0) return 0;
//	else return val;
//}
//
//function timeDelay(delay : float) {	// moves gameTime
//	gameTime.gameTime += delay;
//}
//
//function combat (playerWinsInitiative : boolean) {
//	if (debugging) Debug.Log("COMBAT");
//	
//	
//}
//
//
//// ******************** EVENT TRIGGERS ************************************************************
//
//						// tag 			collider -gameObject
//function eventTrigger (trigger : String, item : GameObject) {		
//
//	// "atLocation" is called from LocationDatabase (OnTriggerEnter)
//	// "inLocation" is called from LocationFinder (enterLocation)
//	// "trigger" is called from LocationFinder (OnTriggerEnter)
//
//
//	var e : String = null; // event name
//	var triggersDone : List.<String> = new List.<String>();
//
//
//	// Loop through all trigger events and execute the picked one at once, repeat ("triggersDone" -List checks a single event is never run more than once on the same trigger)
//	for (var i : int = 0; i <30; i++) {
//		e = pickTriggerEvent(trigger, item, triggersDone);
//		if (e == null) break;	// if no trigger events available
//		//if (debugging) Debug.Log("Event: "+e+" / Trigger type: "+trigger+" / GameObject: "+item.name);
//		
//		// execute trigger event
//		triggersDone.Add(e); 
//		yield StartCoroutine(eventHandler(e) ); // do not proceed until eventHandler is done
//		
//		if (i == 10) Debug.Log("NOTE: Trigger event \""+e+"\" was picked, but already 10 events were played.");
//	}
//	
//}
//
//
//						// object tag (atLocation, inLocation, Object)
//function pickTriggerEvent(trigger : String, collisionObject : GameObject, triggersDone : List.<String>) : String {			// chooses an event from triggerable event pool
//
//	var triggerable : List.<String> = new List.<String>();
//
//	var picked : String = null;
//	var adder : float;
//	var roll : float = Random.value;
//	
//
//	// collect all triggerable events
//		
//	// Checks trigger conditions from inLocation, atLocation, any, collider
//	var conditions = function(e : eventClass) : boolean {
//		
//		// check if same trigger type
//		if (trigger == e.triggerType) {
//			// check if "any" object works for the event
//			if (e.triggerObject == null) return true;
//			// if specified object
//			// try if event triggerObject exist
//			/*try {*/ var triggerObject : GameObject = GameObject.Find(e.triggerObject.name); /*}*/
//			//catch (err : System.Exception) { return false; }	// if no such item exist, no trigger
//			if (triggerObject == null) return false;
//
//			// check if objects are same
//			if (e.triggerObject == collisionObject) return true;
//		}
//		return false;
//	};
//
//
//	// go through all triggerable events 		[GOES THROUGH ALL THE TRIGGERABLE EVENTS AND PICKS JUST ONE - if need to optimize, stop to the first one but lose picking order]
//	for (var e : KeyValuePair.<String, eventClass> in event.Event){
//		//if (debugging) { Debug.Log("Anaylyzing if \""+e.Value.name+"\" is \"available\" and \"triggerable\":"); }
//		
//		// Availability Filter + trigger
//		if (e.Value.available && e.Value.triggerType != null) {	//if (debugging) { Debug.Log(" ---> it is!"); }
//			// check if trigger types match and it's not already done
//			if (conditions(e.Value) && !triggersDone.Contains(e.Key) ) {
//				// 
//				triggerable.Add(e.Key);	// record event name
//					e.Value.getCharacter();	// loads character, if any
//					e.Value.getLocation();	// loads location, if any
//					e.Value.getAdvice(); // loads advice
//				adder += e.Value.probability();	// probability() += adder (also assigns it as last probability in the event) 
//				//if (debugging) Debug.Log("Cumulative brobability at "+e.Value.name+": "+adder);
//				//if (debugging) Debug.Log("added last probability: "+e.Value.lastProbability);
//			}
//		}
//	}
//	
//	if (triggerable.Count < 1) { /*if (debugging) Debug.Log("No trigger events");*/ return null; }
//	
//	if (adder == 0) { /*if (debugging) Debug.Log("Trigger events had 0 probability"); */ return null; }
//	
//	
//	// Return random by probability weight  --- other choice would be to always return the largest probability
//	roll = adder * Random.value;
//	adder = 0;
//		
//	// start adding last probabilities until roll > adder  --> event piced
//	for (var e : String in triggerable) {
//		adder += event.Event[e].lastProbability;
//		if (adder >= roll) {
//			picked = e;
//			break;
//		}
//	}
//	//Debug.Log("Roll: "+roll);
//	//Debug.Log("Adder reached: "+adder);
//
//	if (picked == "") { if (debugging) Debug.Log("PROBLEM: picked empty event name!"); return null; }
//	else if (debugging) Debug.Log("Trigger Event Picked: \""+picked+"\", days since last event: "+ Mathf.Round(gameTime.gameDay - lastEventDate));
//
//	//lastEventDate = gameTime.gameDay;	// assign date to mark last event spawned		// Triggers don't assign last event
//
//	return picked;
//}