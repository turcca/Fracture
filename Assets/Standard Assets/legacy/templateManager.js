//#pragma strict
//import System.IO;
//import System.Collections.Generic;		// add List.<>();
//
//// ********************* TEMPLATE MANAGER
//var writingFile : boolean = false;
//var debugging : boolean = true;
//
//var eventData = new List.<String>();
//
//var eventName : String;
//var eventCount : int = 0;
//var triggerCount : int = 0;
//var template : String;
//var templateLength : int;
//
//var charPos : int;
//
//var filters : List.<String> = new List.<String>();
//
//private var bracketOpen : int = 0;
//private var emptyloop : boolean;
//
//private var character : String;
//private var location : String;
//private var filterFieldsDone : boolean = false;
//private var textFieldsDone : boolean = false;
//private var choicesDone:  boolean = false;
//private var outcomesDone : boolean = false;
//private var adviceDone : boolean = false;
//private var eventEnded : boolean = true;
//private var quote : char = 34;	// " = unicode in decimals 32, 34
//
//
//// *************** Run Template Manager *******************
//
//function Awake() {
//	WorldState.initialize();
//	initializeTemplate();
//	readTemplate();	
//			// debugging
//			//ADD("Debug.Log(\"\"+n+\" probability = \"+Event[n].probability());");
//			//ADD("Debug.Log(\"Test Event probability = \"+Event[\"Test Event\"].probability());");
//			//ADD("Debug.Log(\"Test Event text : \"+Event[\"Test Event\"].getText());");
//		ADD("// <-------------------------------------------------------------");
//		ADD("}");	// close initializeEvents() {
//		if (!eventEnded) { Debug.LogError("ERROR: Event "+eventCount+" (last event) is missing $=end -effect."); }
//	splitDictionaries(); // counts events and triggers ( NOT IN USE: splits dictionary to "event" and "trigger" )
//	eventTools();		// add character methods to Events.js
//	eventData.Insert(1, "// events: "+eventCount+"  triggers: "+triggerCount+"\n\n");
//										if (writingFile) {
//	writeTemplate("Events.js"); }		// write eventData to events.js in the right place
//	else Debug.Log("EVENTS: NOT WRITING FILE");
//	
//	//Debug.Log("Done: "+Time.time+"s");
//}
//
//function Start() {
//	Debug.Log("Done: "+Time.time+"s");
//	Application.Quit();
//	Debug.Break();
//}
//
//
//function initializeTemplate() {
//
//	addVariables();
//
//	var fileName = "events.txt";
//	var sr = new StreamReader(Application.dataPath + "/" + fileName);
//    template = sr.ReadToEnd();
//    	    if (template == null) { Debug.Log(fileName+" was not found"); }
//    sr.Close();
//    templateLength = template.Length;	if (debugging) { Debug.Log("Template length: "+templateLength); }
//	}
//
//	function addVariables() {	// events.js starts:
//	
//		var theTime : String = System.DateTime.Now.ToString("HH:mm:ss"); 
//		var theDate : String = System.DateTime.Now.ToString("dd/MM/yyyy"); 
//		
//			ADD("// Events.js compiled: "+theDate+" "+theTime);
//			ADD("#pragma strict");
//			ADD("import System.Collections.Generic;");
//			ADD(" \n ");
//
//	
//			ADD("// Player - LocationFinder (Collider / Finder)");
//			ADD("private var player : GameObject;");
//			ADD("private var finder : LocationFinder;");
//			
//			ADD("// ScriptHolder ");
//			ADD("private var scriptHolder : GameObject;");
//			ADD("private var nameGenerator : NameGenerator;");
//			ADD("private var gameTime : GameTime;");
//			ADD("private var collector : locationsDataCollector;");
//			ADD("private var ship : PlayerShipData;");
//			ADD("private var shipPop : ShipPopulation;");
//			ADD("private var economy : Economy;");
//			ADD("private var characters : Characters;");
//			ADD("private var location : locationDatabase;");
//			ADD("private var eventManager : EventManager;");
//			
//			ADD(" \n ");
//			ADD("var Event 	: Dictionary.<String, eventClass> = new Dictionary.<String, eventClass>();");
//			//ADD("var trigger: Dictionary.<String, eventClass> = new Dictionary.<String, eventClass>();");
//			ADD("var advice: Dictionary.<String, adviceClass> = new Dictionary.<String, adviceClass>(); 	// event-level dictionary with every character & opinion in the event (key: character 'name', value: 'opinion', recommend int) "); // key: character "name", value: opinion, recommend);
//			
//			ADD(" \n ");
//			ADD("class adviceClass {");
//			ADD("	var opinion 	: String;		// character advice txt");
//			ADD("	var recommend 	: int;			// character's recommended choice");
//			ADD("}");
//			ADD(" \n ");
//			
//			ADD("var eventCount : int;");
//			ADD("var triggerCount : int;");
//			ADD("var n : String; // current event name");
//			ADD("var filterWeights : Dictionary.<String, float> = new Dictionary.<String, float>();");
//			
//			ADD("enum freq { Rare, Default, Elevated, Probable }");
//			ADD("enum status { Quiet, Default, Alert, Panic }");
//			ADD("enum noise { QuietHum, QuietEerie, DefaultBridge, EchoRoom, EngineRoom, ActionBase, Fracture }");
//
//
//			ADD(" \n ");
//			ADD("class eventClass {");
//			ADD("	var name : String;				// Unique id, name of the event");
//			ADD("	var available : boolean;		// is event available for spawning");
//			ADD("	var frequency : freq = freq.Default;	// event frequency weight");
//			ADD("	var situation : status = status.Default;	// event situation (ambient sounds: mood and tone)");
//			ADD("	var ambient : noise = noise.DefaultBridge;	// event ambient location (ambient sounds: location and background noise)");
//			ADD("	var filters : List.<String> = new List.<String>();	// list of filter tags");
//			ADD("	var triggerType : String = null;			// if != null, trigger Event. List trigger type: inLocation, atLocation, object");
//			ADD("	var triggerObject : GameObject = null;		// triggerType may specify a particular object");
//			ADD("	var probability = function():float { return 1; }; 	// returns event probability - ALSO assigns the value to lastProbability");
//			ADD("	var getText = function():String { return null; }; 	// returns text field depending on conditions");
//			
//			ADD("	var lastProbability : float = 1;// record last probability for event");
//			ADD("	var getCharacter = function() 	{ return null; };	// loads character");
//			ADD("	var character 	: String;		// Character name");
//			ADD("	var advisor 	: String;		// acting advisor name");
//			ADD("	var locationRequired : boolean = false;");
//			ADD("	var getLocation = function() : GameObject { return null; };	// loads location");
//			ADD("	var locationObject : GameObject = null;	// GameObject of location");
//			ADD("	var location : locationDatabase = null;		// locationDatabase to use");
//			ADD("	var locationName : String;		// Game location name");
//			ADD("	var getChoices = function() : Dictionary.<String, int> { return null; }; 	// returns list of choices to select");
//			ADD("	var choice : int = 0;			// player choice picked - event-unique int");
//			ADD("	var outcomes = function() {};	// calculates current outcomes");
//			ADD("	var outcome : int = 0;			// event outcome = event-unique int");
//			ADD("	var getAdvice = function() {};	// pick advice");
//			ADD("	var dateLastDone : float = -1;	// date when event last had outcome (gameTime.gameDay) ");
//			ADD("}");
//
//			ADD(" \n ");
//			ADD("function filter(f : String) {");
//			ADD("	// check \"filterWeights\" dictionary");
//			ADD("	if (!filterWeights.ContainsKey(f)) filterWeights.Add(f, 1.0);");
//			ADD("	// add filter to event \"filters\" list");
//			ADD("	if (!Event[n].filters.Contains(f)) Event[n].filters.Add(f);");
//			ADD("}");
//
//			ADD(" \n \n \n ");
//			ADD("function Start() {");
//			ADD("	//initializeEvents(); 	// call from initializeGame");
//			ADD("}");
//			
//
//			ADD("function initializeEvents() {");
//
//			ADD("	player = GameObject.FindGameObjectWithTag(\"Player\");");
//			ADD("		finder = 	player.gameObject.GetComponent(LocationFinder);\n");
//	
//			ADD("	scriptHolder = GameObject.Find(\"ScriptHolder\");");
////			ADD("		nameGenerator 	= scriptHolder.gameObject.GetComponent(NameGenerator);");
////			ADD("		gameTime 		= scriptHolder.gameObject.GetComponent(GameTime);");
//			ADD("		collector 		= scriptHolder.gameObject.GetComponent(locationsDataCollector);");
////			ADD("		ship 			= scriptHolder.gameObject.GetComponent(PlayerShipData);");
////			ADD("		shipPop 		= scriptHolder.gameObject.GetComponent(ShipPopulation);");
////			ADD("		economy 		= scriptHolder.gameObject.GetComponent(Economy);");
//			ADD("		characters 		= scriptHolder.gameObject.GetComponent(Characters);");
//			ADD("		eventManager	= scriptHolder.gameObject.GetComponent(EventManager);");
//			
//			ADD("	loadEvents();");
//			ADD("	splitDictionaries();");
//			ADD("}");
//			ADD(" \n \n ");
//			ADD("// ***************************************EVENTS********************************************");
//			ADD("function loadEvents() {");
//
//			ADD("// ------------------------------------------------------------->");
//	}
//
//function ADD(txt : String) {
//	if (debugging) { Debug.Log("      Adding: "+txt); }
//	eventData.Add(txt+"\n");	// "\n" adds line  (using instead of WriteLine, line endings are better this way)
//}
//
//function charInput(pos : int) : char {
//	//if (debugging) { Debug.Log(template[pos]+" at  pos :"+pos+"  / templateLength: "+templateLength); }  // per-char input debugging
//	if (pos < templateLength) { return template[pos]; }
//	else { Debug.LogError("template[pos] "+charPos+" out of range @charInput "+charPos+" (\""+pos+"\")"); } 
//	return returnCharByInt(0);
//}
//
//function ADDchar(txt : String) {
//	if (debugging) { Debug.Log("      Adding char: "+txt); }
//	var str : String = "";
//	str = eventData[eventData.Count-1];
//	//Debug.Log("String captured in ADDchar: "+str);
//	str += txt+"\n";
//	//Debug.Log("String merged in ADDchar: "+str);
//	eventData[eventData.Count-1] = str;
//}
//
//function ADDextend(txt : String) {	// extend writing on a line
//	if (debugging) { Debug.Log("      Extending: "+txt); }
//	var str : String = "";
//	str = eventData[eventData.Count-1];
//	str += txt;
//	eventData[eventData.Count-1] = str;
//}
//
//function ADDcharInt(unicode : int) {	// add char by unicode decimal value
//	var a : char = unicode;
//	if (debugging) { Debug.Log("      Adding char \""+a+"\" by int: "+unicode); }
//	var str : String = "";
//	str = eventData[eventData.Count-1];
//	str += a+"\n";
//	eventData[eventData.Count-1] = str;
//}
//
//function returnCharByInt(unicode : int) :char {	// add char by unicode decimal value
//	var a : char = unicode;
//	if (debugging) { Debug.Log("      returning char \""+a+"\" by int: "+unicode); }
//	return a;
//}
//
//function isLocationTrue() : boolean {
//	if (location == null) return false;
//	else return true;
//}
//
//
//
//// *********************************************************************************
//
//function readTemplate() {
//
//	while (charPos < templateLength) {		// käy läpi kirjainmerkkejä templatessa
//
//		var c : char = charInput(charPos);
//		emptyloop = true;
//		
//		// Look for a new event or a tag
//		getTag(c);
//
//		// length check
//		if (charPos >= templateLength) { if (debugging) { Debug.Log("Template reached its length ("+charPos+" / "+templateLength+")"); } break; }
//	
//		// charPos +1 check
//		if (emptyloop) { 
//			if (debugging) { Debug.Log("reading template "+charInput(charPos)+" at ("+charPos+" / "+templateLength); } 
//			if (charPos+1 < templateLength) { charPos +=1; if (debugging) Debug.Log("moving to "+charInput(charPos)); } // charPos += 1;
//			else { if (debugging) Debug.Log("End read, templated length met"); break; } }	
//	}
//}
//
//function getTag(c : char) {
//	if (c == "#") { newEvent(); return; }
//	if (c == "@") { initializeTag(); return; }
//	// check if commented
//	if (charInput(charPos) == "/" && charInput(charPos+1) == "*") { commented(); return; }
//}
//
//
//
//// *********************************************************************************
//
//
//function commented() {
//
//	// length check for +2
//	if (charPos+2 <= templateLength) { 
//		charPos += 2; 
//	}
//	else { Debug.Log("End read, templated length met"); return; }
//
//	var comment : String = "";
//
//	// start commenting
//	ADDextend("/*");
//
//	// look for end comment
//	while (charPos+2 <= templateLength) {
//		// if commented out
//		if (charInput(charPos) == "*" && charInput(charPos+1) == "/") {
//			charPos +=2;
//			break;
//		}
//		// else record comment (no nl/newline)
//		else { 
//			if (charInput(charPos) != 0x0) comment += (charInput(charPos));
//			charPosNext();
//		}
//	}
//	if (debugging) Debug.Log("commenting: "+comment);
//
//	ADDextend(comment);
//	ADDextend("*/");
//	ADD(" \n ");
//}
//
//
//function newEvent() {
//
//	if (bracketOpen > 1) { Debug.LogError("Too many brackets open at the start of a new event"); }
//	if (bracketOpen == 1) { ADD("}"); bracketOpen = 0; }	// unicode for } = 7D // String.fromCharCode(7D)
//	
//	if (!eventEnded) { Debug.LogError("ERROR: Event "+eventCount+" is missing $=end -effect. ("+eventName+")"); }
//
//	eventCount += 1;
//	filterFieldsDone = false; // filter fields ready to be read for the new event
//	textFieldsDone = false;	// text fields ready to be read for the new event
//	choicesDone = false;	// choices ready to be read for the new event
//	outcomesDone = false;	// outcomes ready to be read for the new event
//	adviceDone = false;		// advices ready to be read for the new event
//	eventEnded = false;		// initiates new event, until player exits it
//	filters.Clear(); 			// empites event's filters
//	emptyloop = false;
//	
//	character = null;
//	location = null;
//	
//	if (debugging) { Debug.Log("NEW EVENT: eventCount: "+eventCount); }
//
//
//	charPosNext();		// charPos += 1;
//
//	// record Event.name
//	ADD(" \n ");
//	ADD("// NEW EVENT "+eventCount);
//	eventName = recordString(false);
//	ADD("n = \""+eventName+"\";");
//	ADD("eventCount += 1;");
//	ADD("if (Event.ContainsKey(n)) { Debug.LogError(\"ERROR: event \"+n+\" already excists.\"); }");
//	
//	// Create event object
//	ADD("Event.Add(n, new eventClass());");
//	ADD("	Event[n].name				= n;");
//	ADD("	Event[n].available			= true;");
//	ADD("	Event[n].triggerType		= null;");
//	ADD("	Event[n].character			= null;");
//	ADD("	Event[n].advisor			= null;");
//	ADD("	Event[n].location			= null;");
//	ADD("	Event[n].locationName		= \"empty\";");
//	ADD("	Event[n].choice				= 0;");
//	ADD("	Event[n].outcome			= 0;");
//	ADD("	Event[n].dateLastDone 		= -1;");
//	
//		// DEBUG UNICODE FROM CHAR
//		//var uniString : int = System.Convert.ToInt32("T"[0]);
//		//Debug.Log("T int: "+uniString);
//}
//
//
//
//function initializeTag() {
//	
//	var stringStart : int = charPos;	// start pos of the record
//	
//	if (bracketOpen > 1) { Debug.LogError("Too many brackets open at the start of new event"); }
//	if (bracketOpen == 1) { ADD("}"); bracketOpen = 0; }	// unicode for } = 7D // String.fromCharCode(7D)
//
//	charPosNext();		// charPos += 1;
//	
//	for (var j : int = 0; j<30; j++) {
//	
//		var c : char = charInput(charPos);
//	
//		// @P
//		if (c == "p" || c == "P") { probabilityVariable(); break; }		// @probability variables (should be all qued)
//		// @A
//		if (c == "a" || c == "A") { if (adviceDone) break; else { advice(); break;} }	// @advice (all)
//		// @T
//		if ((c == "t" || c == "T") && charInput(charPos+1) != "r") { if (textFieldsDone) break; else { text(); break;} }	// @text (all)
//		// @O
//		if (c == "o" || c == "O") { if (outcomesDone) break; else { outcome(); break;}}	// @outcome (all)
//		// @F
//		if (c == "f" || c == "F")  if (charInput(charPos+1) != "r") { if (filterFieldsDone) break; else { filter(); break;}}	// @filter (all)
//		// frequency
//		else if (charInput(charPos+2) == "e" && charInput(charPos+3) == "q" && charInput(charPos+4) == "u" && charInput(charPos+5) == "e" && charInput(charPos+6) == "n" && charInput(charPos+7) == "c" && charInput(charPos+8) == "y") { 
//				charPos += 8; frequency(); break; }									// @frequency (one)
//		// situation
//		if (c == "s" || c == "S" && charInput(charPos+1) == "i" && charInput(charPos+2) == "t" && charInput(charPos+3) == "u" && charInput(charPos+4) == "a" && charInput(charPos+5) == "t" && charInput(charPos+6) == "i" && charInput(charPos+7) == "o" && charInput(charPos+8) == "n") { 
//				charPos += 8; situation(); break; }	
//		// ambient
//		if (c == "a" || c == "A") {
//			if (charInput(charPos+1) == "m" && charInput(charPos+2) == "b" && charInput(charPos+3) == "i" && charInput(charPos+4) == "r" && charInput(charPos+5) == "n" && charInput(charPos+6) == "t") { 
//				charPos += 6; ambient(); break; } }
//		// trigger
//		if (c == "t" || c == "T") {
//			if (charInput(charPos+1) == "r" && charInput(charPos+2) == "i" && charInput(charPos+3) == "g" && charInput(charPos+4) == "g" && charInput(charPos+5) == "e" && charInput(charPos+6) == "r") { 
//				charPos += 6; trigger(); break; } }								// @location (one)
//		// character
//		if (c == "c" || c == "C") { 
//			if (charInput(charPos+1) == "h" && charInput(charPos+2) == "a" && charInput(charPos+3) == "r" && charInput(charPos+4) == "a" && charInput(charPos+5) == "c" && charInput(charPos+6) == "t" && charInput(charPos+7) == "e" && charInput(charPos+8) == "r") { 
//				charPos += 8; mainCharacter(); break; }									// @character (one)
//			else { if (choicesDone) break; else { choice(); break;} } }					// @choices (all)
//		// location
//		if (c == "l" || c == "L") {
//			if (charInput(charPos+1) == "o" && charInput(charPos+2) == "c" && charInput(charPos+3) == "a" && charInput(charPos+4) == "t" && charInput(charPos+5) == "i" && charInput(charPos+6) == "o" && charInput(charPos+7) == "n") { 
//				charPos += 7; mainLocation(); break; } }								// @location (one)
//		
//		if (c == "@") { Debug.LogError("Invalid tag at "+charPos+", another @ found"); }
//		
//		// skip space		
//		if (c == " ") { if (charPos+1 <= templateLength) { charPos +=1; } else { Debug.LogError("End tag read, templated length met but tag was open"); break; } 	}	// charPos += 1; }					
//		else { Debug.LogError("Invalid tag at "+charPos); break; }
//		
//		
//		if (charPos - stringStart > 20) { Debug.LogError("Invalid tag at "+stringStart+", nothing after 20 spaces"); break;}
//	}
//	
//	emptyloop = false;
//}
//
//
//// ********* TAGS
//
//function filter() {
//
//	ADD("");
//	ADD("	// Filters");
//
//	if (bracketOpen > 1) { Debug.LogError("Too many brackets open at the start of mainCharacter() at "+charPos); }
//	if (bracketOpen == 1) { ADD("}"); bracketOpen = 0; }	// unicode for } = 7D // String.fromCharCode(7D)
//
//	charPosNext();		// charPos += 1;	
//
//	var charPosSave : int = charPos+1;
//	var txt : String;
//
//	//record all @f's
//	while (charPos < templateLength) {
//		
//		if (filterFieldsDone) break;
//
//		//@f found, catch String
//		if (charInput(charPos) == quote || charInput(charPos+1) == quote || charInput(charPos+2) == quote) {
//			txt = recordString(false);
//			if (!filters.Contains(txt) ) {
//				ADD("		filter(\""+txt+"\");");
//				filters.Add(txt);
//			}
//			else Debug.LogWarning("WARNING: duplicate filter \""+txt+"\" found in event \""+eventName+"\"");
//		}
//
//		//look for next @f	
//		while (true) {
//			if (charPos+1 >= templateLength || charInput(charPos) == "#") { filterFieldsDone = true; break;}
//			if (charInput(charPos) == "@" && (charInput(charPos+1) == "f" || charInput(charPos+1) == "F")) {
//				charPos += 2; break; }
//			charPos +=1;
//		}
//	}
//	charPos = charPosSave;
//}
//
//
//function frequency() {
//	
//	ADD("");
//	ADD("	// Frequency");
//	
//	if (bracketOpen > 1) { Debug.LogError("Too many brackets open at the start of mainCharacter() at "+charPos); }
//	if (bracketOpen == 1) { ADD("}"); bracketOpen = 0; }	// unicode for } = 7D // String.fromCharCode(7D)
//
//	charPosNext();		// charPos += 1;	
//
//	//catch String
//			if (charInput(charPos) == quote || charInput(charPos+1) == quote || charInput(charPos+2) == quote) {
//				ADD("	Event[n].frequency = freq."+recordString(false)+";");
//			}
//			else Debug.LogError("Frequency value not recorded at eventCount: "+eventCount+"["+eventName+"] / charPos "+charPos);
//}
//
//function situation() {
//
//	ADD("");
//	ADD("	// Situation");
//
//	charPosNext();		// charPos += 1;	
//
//	//catch String
//			if (charInput(charPos) == quote || charInput(charPos+1) == quote || charInput(charPos+2) == quote) {
//				ADD("	Event[n].situation = status."+recordString(false)+";");
//			}
//			else Debug.LogError("Situation value not recorded at eventCount: "+eventCount+"["+eventName+"] / charPos "+charPos);
//
//}
//
//function ambient() {
//
//	ADD("");
//	ADD("	// Ambient");
//
//	charPosNext();		// charPos += 1;	
//
//	//catch String
//			if (charInput(charPos) == quote || charInput(charPos+1) == quote || charInput(charPos+2) == quote) {
//				ADD("	Event[n].ambient = noise."+recordString(false)+";");
//			}
//			else Debug.LogError("Ambient value not recorded at eventCount: "+eventCount+"["+eventName+"] / charPos "+charPos);
//}
//
//function trigger() {
//	
//	//eventCount -= 1;
//	//triggerCount += 1;
//	
//	ADD("");
//	ADD("	// Trigger");
//	
//	if (bracketOpen > 1) { Debug.LogError("Too many brackets open at the start of mainCharacter() at "+charPos); }
//	if (bracketOpen == 1) { ADD("}"); bracketOpen = 0; }	// unicode for } = 7D // String.fromCharCode(7D)
//
//	charPosNext();		// charPos += 1;	
//
//	// record triggerType
//	var triggerRecord : String = recordString(false);
//	var triggerObjectValue : String = "";
//	if (triggerRecord == "") { Debug.LogError("ERROR: no triggerType (string) recorded at "+eventCount+"["+eventName+"] / charPos "+charPos); return; }
//	ADD("	Event[\""+eventName+"\"].triggerType = \""+triggerRecord+"\";");
//
//	// catch "=", record triggerObject value. If none found, treated as "any"
//	if (charInput(charPos) == "=" || charInput(charPos+1) == "=" || charInput(charPos+2) == "=" || charInput(charPos+3) == "=") {
//
//		triggerObjectValue = recordValue();
//		ADD("	Event[\""+eventName+"\"].triggerObject = \""+triggerObjectValue+"\";");
//	}	
//	// debug
//	if (debugging) { Debug.Log("TriggerType : "+triggerRecord+"   / triggerObject : "+triggerObjectValue); }
//}
//
//
//function mainCharacter() {
//	
//	ADD("");
//	ADD("	// Main character");
//	
//	if (bracketOpen > 1) { Debug.LogError("Too many brackets open at the start of mainCharacter() at "+charPos); }
//	if (bracketOpen == 1) { ADD("}"); bracketOpen = 0; }	// unicode for } = 7D // String.fromCharCode(7D)
//
//	charPosNext();		// charPos += 1;	
//
//	// catch "{", record IF condition	:close bracket later
//	if (charInput(charPos) == "{" || charInput(charPos+1) == "{" || charInput(charPos+2) == "{") {
//		ADD("	if ("+recordCondition()+") {	");
//		bracketOpen +=1;
//	}
//
//	// catch "=", record character["name"] finder
//	if (charInput(charPos) == "=" || charInput(charPos+1) == "=" || charInput(charPos+2) == "=" || charInput(charPos+3) == "=") {
//		//var cha : String = recordValue();	// finder
//		character = recordValue();
//		if (character == "") character == null;
//
//			//character = "characters.character["+cha+"]";	// template root path to main character
//		if (debugging) { Debug.Log("Main Character: "+character); }
//		ADD("	Event[\""+eventName+"\"].getCharacter = function () : String {");
//		ADD("		Event[\""+eventName+"\"].character = "+character+";");
//		ADD("		return Event[\""+eventName+"\"].character;");
//		ADD("	};");
//	}	
//	
//
//	if (bracketOpen > 1) { Debug.LogError("Too many backets open at @character at "+charPos); }
//	if (bracketOpen == 1) { ADD("	}"); bracketOpen = 0; }
//
//}
//
//function mainLocation() {
//	
//	ADD("");
//	ADD("	// Location");
//	
//	if (bracketOpen > 1) { Debug.LogError("Too many brackets open at the start of mainLocation() at "+charPos); }
//	if (bracketOpen == 1) { ADD("}"); bracketOpen = 0; }	// unicode for } = 7D // String.fromCharCode(7D)
//
//	location = null;
//
//	charPosNext();		// charPos += 1;	
//
//	// Set locationRequired on
//	ADD("	if (!Event[\""+eventName+"\"].locationRequired) Event[\""+eventName+"\"].locationRequired = true;");
//
//	
//	// catch "=", record: use tool or record object name
//	if (charInput(charPos) == "=" || charInput(charPos+1) == "=" || charInput(charPos+2) == "=" || charInput(charPos+3) == "=") {
//		location = recordValue(); // string to method
//		ADD("	try {");
//		ADD("		Event[\""+eventName+"\"].getLocation = function () : GameObject {");
//		ADD("			Event[\""+eventName+"\"].locationObject = "+location+";");	
//		ADD("			if (Event[\""+eventName+"\"].locationObject != null) {");
//		ADD("				Event[\""+eventName+"\"].location = Event[\""+eventName+"\"].locationObject.gameObject.GetComponent(locationDatabase);");	// find .location locationDatabase
//		ADD("				Event[\""+eventName+"\"].locationName =  Event[\""+eventName+"\"].location.locName;");									// assign .location.locName to event loc name
//		ADD("				return Event[\""+eventName+"\"].locationObject;");
//		ADD("			}");
//		ADD("			else { Debug.Log(\"PROBLEM: event '"+eventName+"' location input does not resolve to an object (null returned)\"); return null; }");		
//		ADD("		};");
//		ADD("	}");
//		ADD("	catch (e : System.Exception) {");
//		ADD("		Debug.LogError(\"EXCEPTION: event '"+eventName+"' location input does not resolve to an object (reference exception)\");");
//		ADD("		Event[\""+eventName+"\"].location = null; Event[\""+eventName+"\"].locationName = \"empty\";");
//		ADD("		return null;");
//		ADD("	}");
//
//
//		if (debugging) { Debug.Log("Location \""+location+"\" picked"); }	
//	}	
//	
//	// if just "@location, closest loc is assigned
//	else 	{
//		Debug.LogError("ERROR: \""+eventName+"\" @Location is missing '= value'.");
//	}	
//
//
//}
//
//function probabilityVariable() {
//	
//	ADD("\n");
//	ADD("	// Probability Variables");
//	
//	charPosNext();		// charPos += 1;
//	
//	var pVarCount: int = 0;
//	var variable : String;
//	var moreP : boolean = true;
//	var ifBracketOpen : boolean = false;
//	
//	if (pVarCount == 0) { ADD("	Event[\""+eventName+"\"].probability = function () {"); bracketOpen += 1; ADD("		n = \""+eventName+"\";"); }
//	
//	// insert flat variables from filters according to filterWeights
//	var count : int = filters.Count;
//	for (var filterN : int = 0; filterN < count; filterN++) {
//		pVarCount += 1;
//		variable = "p"+pVarCount;
//		ADD("		var "+variable+" : float = filterWeights[\""+filters[filterN]+"\"];");
//	}
//
//	// look through and insert all probability variables
//	for (var j : int = 0; j<50; j++) {
//		
//		pVarCount += 1; if (debugging) { Debug.Log("New probability variable: "); }
//		
//		variable = "p"+pVarCount;
//		ADD("		var "+variable+" : float = 1;");
//		
//		// catch "{", record IF condition	:close bracket later
//		if (charInput(charPos) == "{" || charInput(charPos+1) == "{" || charInput(charPos+2) == "{" || charInput(charPos+3) == "{") {
//			ADD("		if ("+recordCondition()+") {	");
//			ifBracketOpen = true;
//		}
//
//		// catch "=", "record value", +=";"
//		if (charInput(charPos) == "=" || charInput(charPos+1) == "=" || charInput(charPos+2) == "=" || charInput(charPos+3) == "=") {
//			var val : String = recordValue();	// p1 = ?
//			if (debugging) { Debug.Log("Probability variable: "+variable+" = "+val+";"); }
//			
//			ADD("			"+variable+" = "+val+";");
//		}
//		else { Debug.LogError("No \"=\" found for probability variable tag at event: "+eventCount+" pos:"+charPos); break; }
//		if (ifBracketOpen) { ADDchar("		}"); ifBracketOpen = false; }
//	
//		// find next tag
//			for (var r : int = 0; r<500; r++) {
//				// If "#" found
//				if (charInput(charPos) == "#") { moreP = false; break; }				
//
//				// If "@" found
//				if (charInput(charPos) == "@") { 
//					if (charInput(charPos+1) == "p" || charInput(charPos+1) == "P") { break; }
//					else { moreP = false; break; }				
//				}
//				// charPos += 1; if safe
//				if (charPos+1 <= templateLength) { charPos +=1; }
//				else { if (debugging) {Debug.Log("Templated length met on find"); } moreP = false; break; }
//			}
//		if (!moreP) { if (debugging) { Debug.Log("End of Probability Variables, "+pVarCount+" found"); } break; }
//	}				
//	//ADD("Event[n].lastProbability = 1");
//	var insertPs : String = "Event[n].lastProbability = 1";
//		for (var i : int = 1; i<=pVarCount; i++) {
//			insertPs += " * p"; 
//			insertPs += i; 
//		}
//		insertPs += ";";
//	ADD("		"+insertPs);
//	
//	ADD("		return Event[n].lastProbability;");
//	
//	// close bracket "}"
//	if (bracketOpen > 1) { Debug.LogError("Too many backets open in @probabilityVariables at "+charPos); }
//	if (bracketOpen == 1) { ADD("	};"); bracketOpen = 0; }
//}
//
//
//function advice() {			// advice your inner circle characters have on events
//	ADD("\n");
//	ADD("	// Get Advice");
//	ADD("	Event[\""+eventName+"\"].getAdvice = function () {");
//	ADD("		n = \""+eventName+"\";");
//	ADD("		var advisor : String;");
//	ADD("");
//	ADD("		advice.Clear();");
//	ADD("");
//	
//	var condition : String = null;
//	var str : String = null;
//	var recommend : String = null;
//	var ifBracketOpen : boolean = false;
//	var charPosSave : int = charPos;
//	var jobNo : int = 0;
//	
//	
//					
//	// record all @a's
//	while (charPos < templateLength) {
//		
//		if (adviceDone) break;
//		
//		// position for recording
//		if (charInput(charPos) == "a") charPosNext();	// move to next char from "a"
//			// skip empty spaces
//			for (var i : int = 0; i < 10; i++) {
//				if (charInput(charPos) == " " || charInput(charPos+1) == " ") charPosNext();
//				else break;
//			}
//		
//		
//		//@a found
//		ADD("		// advice:");
//		// catch assignment
//		
//			// check if illegal input
//			if (charInput(charPos) == "{" || charInput(charPos+1) == "{" || charInput(charPos+2) == "{" || charInput(charPos+3) == "{") { Debug.LogError("ERROR: event \""+eventName+"\" ("+eventCount+") has illegal advice input @ pos:"+charPos+" (no assignment identifier, bracket found)"); break; }
//			if (charInput(charPos) == quote || charInput(charPos+1) == quote || charInput(charPos+2) == quote || charInput(charPos+3) == quote) { Debug.LogError("ERROR: event \""+eventName+"\" ("+eventCount+") has illegal advice input @ pos:"+charPos+" (no assignment identifier, \" found)"); break; }
//		
//			while (true) { 
//				// captain
//				if (charInput(charPos) == "c" || charInput(charPos) == "C") {
//					if (charInput(charPos+1) == "a" && charInput(charPos+2) == "p" && charInput(charPos+3) == "t" && charInput(charPos+4) == "a" && charInput(charPos+5) == "i" && charInput(charPos+6) == "n") { charPos += 7; jobNo = 1; break; }
//				}		
//				// navigator
//				if (charInput(charPos) == "n" || charInput(charPos) == "N") {
//					if (charInput(charPos+1) == "a" && charInput(charPos+2) == "v" && charInput(charPos+3) == "i" && charInput(charPos+4) == "g" && charInput(charPos+5) == "a" && charInput(charPos+6) == "t" && charInput(charPos+7) == "o" && charInput(charPos+8) == "r") 	{ charPos += 9; jobNo = 2; break; }
//				}
//				// engineer
//				if (charInput(charPos) == "e" || charInput(charPos) == "E") {
//					if (charInput(charPos+1) == "n" && charInput(charPos+2) == "g" && charInput(charPos+3) == "i" && charInput(charPos+4) == "n" && charInput(charPos+5) == "e" && charInput(charPos+6) == "e" && charInput(charPos+7) == "r") { charPos += 8; jobNo = 3; break; }
//				}
//				// security
//				if (charInput(charPos) == "s" || charInput(charPos) == "S") {
//					if (charInput(charPos+1) == "e" && charInput(charPos+2) == "c" && charInput(charPos+3) == "u" && charInput(charPos+4) == "r" && charInput(charPos+5) == "i" && charInput(charPos+6) == "t" && charInput(charPos+7) == "y") { charPos += 8; jobNo = 4; break; }
//				}
//				// quartermaster
//				if (charInput(charPos) == "q" || charInput(charPos) == "Q") {
//					if (charInput(charPos+1) == "u" && charInput(charPos+2) == "a" && charInput(charPos+3) == "r" && charInput(charPos+4) == "t" && charInput(charPos+5) == "e" && charInput(charPos+6) == "r" && charInput(charPos+7) == "m" && charInput(charPos+8) == "a" && charInput(charPos+9) == "s" && charInput(charPos+10) == "t" && charInput(charPos+11) == "e" && charInput(charPos+12) == "r") { charPos += 13; jobNo = 5; break; }
//				}
//				
//				if (charInput(charPos) == "p" || charInput(charPos) == "P") {
//				// psycher
//					if (charInput(charPos+1) == "s" && charInput(charPos+2) == "y" && charInput(charPos+3) == "c" && charInput(charPos+4) == "h" && charInput(charPos+5) == "e" && charInput(charPos+6) == "r") { charPos += 7; jobNo = 6; break; }
//				// priest
//					if (charInput(charPos+1) == "r" && charInput(charPos+2) == "i" && charInput(charPos+3) == "e" && charInput(charPos+4) == "s" && charInput(charPos+5) == "t") { charPos += 6; jobNo = 7; break; }
//				}
//				
//				// if none found, error
//				Debug.LogError("ERROR: event \""+eventName+"\" ("+eventCount+") \"catch assignment\" failed @ pos:"+charPos+" (no assignment name recognized)");
//				break;
//			}
//			
//			// dedicate current character to the assignment
//			if 		(jobNo == 1) ADD("		advisor = characters.assignment[\"captain\"]; 		");
//			else if (jobNo == 2) ADD("		advisor = characters.assignment[\"navigator\"]; 	");
//			else if (jobNo == 3) ADD("		advisor = characters.assignment[\"engineer\"]; 		");
//			else if (jobNo == 4) ADD("		advisor = characters.assignment[\"security\"]; 		");
//			else if (jobNo == 5) ADD("		advisor = characters.assignment[\"quartermaster\"]; ");
//			else if (jobNo == 6) ADD("		advisor = characters.assignment[\"psycher\"]; 		");
//			else if (jobNo == 7) ADD("		advisor = characters.assignment[\"priest\"];		");
//
//			else Debug.LogError("ERROR: event \""+eventName+"\" ("+eventCount+"): job number was 0 (no assignment recorded)");
//			
//			// catch if
//			if (charInput(charPos) == "{" || charInput(charPos+1) == "{" || charInput(charPos+2) == "{" || charInput(charPos+3) == "{") {
//				condition = recordCondition();
//				//ifBracketOpen = true;
//			}
//			else condition = null;
//			
//			// record string and value, add to advice
//			str = recordString(true);
//			
//			// catch value
//			if (charInput(charPos) == "=" || charInput(charPos+1) == "=" || charInput(charPos+2) == "=" || charInput(charPos+3) == "=") {
//				recommend = recordValue();
//			}
//			else recommend = null;	
//				
//				// build the function
//										ADD("		if (advisor != null) {");
//									 	ADD("			if (characters.character.ContainsKey(advisor) ) {");
//				if (condition != null) 	ADD("				if ("+condition+") { ");
//				/* debugging */ 		ADD(" 					if (advice.ContainsKey(advisor) ) Debug.LogError(\"ERROR: Got second opinion for same advisor: \"+advisor+\": '"+str+"' @event: \"+n);");
//										ADD("					advice.Add(advisor, new adviceClass() );");
//				if (str != null) 		ADD("					advice[advisor].opinion = \""+str+"\";"); 		else ADD("					advice[advisor].opinion = null;");
//				if (recommend != null) 	ADD("					advice[advisor].recommend = "+recommend+";"); 	else ADD("					advice[advisor].recommend = 0;");
//				if (condition != null) 	ADD("				}");
//										ADD("			}");
//										ADD("		}");
//			
//				str = null; recommend = null;
//			
//			// else if (debugging) Debug.Log("NOTE: event \""+eventName+"\" ("+eventCount+") had \"null\" advisor information @ pos:"+charPos);
//			
//
//			// close bracket
//			/*if (ifBracketOpen) { 
//				//ADDchar("		}"); 
//				ifBracketOpen = false; 
//			}*/
//			
//		//look for next @a
//		while (true) {
//			if (charPos+1 >= templateLength || charInput(charPos) == "#") { adviceDone = true; break;}
//			if (charInput(charPos) == "@" && (charInput(charPos+1) == "a" || charInput(charPos+1) == "A") /* && charInput(charPos+2) != "h"*/) {
//				charPos += 2; break; }
//			charPos +=1;
//		}
//	}
//	
//	//ADD("		return advice;");
//	ADD("	};");
//	
//	charPos = charPosSave;
//}
//
//
//function text() {
//	ADD("\n");
//	ADD("	// Text field");
//	ADD("	Event[\""+eventName+"\"].getText = function () : String {");
//	ADD("		var txt : String = \"\";");
//	ADD("		n = \""+eventName+"\";");
//
//	var ifBracketOpen : boolean = false;
//	var charPosSave : int = charPos+1;
//			
//	//record all @t's
//	while (charPos < templateLength) {
//		
//		if (textFieldsDone) break;
//		
//		//@t found
//			//catch if
//			if (charInput(charPos) == "{" || charInput(charPos+1) == "{" || charInput(charPos+2) == "{" || charInput(charPos+3) == "{") {
//				ADD("			if ("+recordCondition()+") {	");
//				ifBracketOpen = true;
//			}
//			//catch String
//			if (charInput(charPos) == quote || charInput(charPos+1) == quote || charInput(charPos+2) == quote) {
//				ADD("			txt += \""+recordString(true)+"\";");
//			}
//			//close bracket if open
//			if (ifBracketOpen) { ADDchar("			}"); ifBracketOpen = false; }
//		
//		//look for next @t	
//		while (true) {
//			if (charPos+1 >= templateLength || charInput(charPos) == "#") { textFieldsDone = true; break;}
//			if (charInput(charPos) == "@" && (charInput(charPos+1) == "t" || charInput(charPos+1) == "T")) {
//				charPos += 2; break; }
//			charPos +=1;
//		}
//	}
//	ADD("		return txt;");
//	ADD("	};");
//	
//	charPos = charPosSave;	
//}
//
//
//function choice() {			// ("choice text player can click", int) (int = number that is assigned to "c", choice)
//	ADD("\n");
//	ADD("	// choices");
//	ADD("	Event[\""+eventName+"\"].getChoices = function () : Dictionary.<String, int> {");
//	ADD("		var choices = new Dictionary.<String, int>();");
//	ADD("		n = \""+eventName+"\";");
//	
//	var ifBracketOpen : boolean = false;
//	var charPosSave : int = charPos+1;
//			
//	// record all @c's
//	while (charPos < templateLength) {
//		
//		if (choicesDone) break;
//		
//		//@c found
//		var str : String;
//			// catch if
//			if (charInput(charPos) == "{" || charInput(charPos+1) == "{" || charInput(charPos+2) == "{" || charInput(charPos+3) == "{") {
//				ADD("		if ("+recordCondition()+") {	");
//				ifBracketOpen = true;
//			}
//			// record string, assign to list
//			if (charInput(charPos) == quote || charInput(charPos+1) == quote || charInput(charPos+2) == quote || charInput(charPos+3) == quote) {
//				str += "choices.Add(\""+recordString(true)+"\", ";
//			}
//			// assign value to choice
//			if (charInput(charPos) == "=" || charInput(charPos+1) == "=" || charInput(charPos+2) == "=" || charInput(charPos+3) == "=") {
//				str += recordValue()+");";
//			} 	else Debug.LogError("ERROR: Recording choice at event : "+eventCount+" failed, \"=\" missing at charPos: "+charPos);
//			ADD("		"+str);
//			
//			// close bracket
//			if (ifBracketOpen) { ADDchar("		}"); ifBracketOpen = false; }
//			
//		//look for next @c
//		while (true) {
//			if (charPos+1 >= templateLength || charInput(charPos) == "#") { choicesDone = true; break;}
//			if (charInput(charPos) == "@" && (charInput(charPos+1) == "c" || charInput(charPos+1) == "C") && charInput(charPos+2) != "h") {
//				charPos += 2; break; }
//			charPos +=1;
//		}
//	}
//	// return list
//
//	ADD("		return choices;");
//	ADD("	};");
//	
//	charPos = charPosSave;
//}
//
//function outcome() {	// $ includes effects
//	ADD("\n");
//	ADD("	// Outcomes");
//	ADD("	Event[\""+eventName+"\"].outcomes = function () {");
//	ADD("		n = \""+eventName+"\";");
//	ADD("		var outcomes : int = 0;");
//	
//	var ifBracketOpen : boolean = false;
//	var charPosSave : int = charPos+1;
//	var effectsDone : boolean = false;
//	var effect : String;
//			
//	// record all @o's
//	while (charPos < templateLength) {
//		
//		if (outcomesDone) break;
//		
//		//@o found
//			// catch if
//			if (charInput(charPos) == "{" || charInput(charPos+1) == "{" || charInput(charPos+2) == "{" || charInput(charPos+3) == "{") {
//				ADD("		if ("+recordCondition()+") {	");
//				ifBracketOpen = true;
//			}
//				// check that no string
//				if (charInput(charPos) == quote || charInput(charPos+1) == quote || charInput(charPos+2) == quote) {
//					Debug.LogError("Outcome should have no string, but \" was found at event: "+eventCount+" charPos: "+charPos);
//				}
//			// assign value to outcome
//			if (charInput(charPos) == "=" || charInput(charPos+1) == "=" || charInput(charPos+2) == "=" || charInput(charPos+3) == "=") {
//				ADD("		Event[n].outcome = "+recordValue()+";");
//				ADD("		outcomes ++;"); // counts succesful outcomes when Event[].outcomes() is called
//			}
//			
//			//look for $'s (effects)	until runs into a # or @
//			while (true) {
//				if (charPos+1 >= templateLength ) { effectsDone = true; if (debugging) Debug.Log("Effects done for outcome at event: "+eventCount+" at charPos: "+charPos+" : \"Template at an end"); break; }
//				if (charInput(charPos) == "#")    { effectsDone = true; if (debugging) Debug.Log("Effects done for outcome at event: "+eventCount+" at charPos: "+charPos+" : \"#\" found"); break; }
//				if (charInput(charPos) == "@")    { effectsDone = true; if (debugging) Debug.Log("Effects done for outcome at event: "+eventCount+" at charPos: "+charPos+" : \"@\" found"); break; }
//				// $ found
//				for (var r : int = 0; r <5; r++) {
//					if (charInput(charPos) == "$" ) {
//						//charPosNext(); 
//						effect = "/*Effect: $= */"+recordValue();	// if eventEnded
//						if (debugging) Debug.Log("Effect: $= "+effect);
//						ADD("		"+effect+";");
//				}
//					}
//				charPos +=1;
//			}
//			// close bracket
//			if (ifBracketOpen) { ADDchar("		}"); ifBracketOpen = false; }
//				
//		//look for next @o
//		while (true) {
//			if (charPos+1 >= templateLength || charInput(charPos) == "#") { outcomesDone = true; break;}
//			if (charInput(charPos) == "@" && (charInput(charPos+1) == "o" || charInput(charPos+1) == "O")) {
//				charPos += 2; break; }
//			charPos +=1;
//		}
//	}
//	// end outcomes
//		ADD("		if (outcomes > 1) Debug.Log(\"PROBLEM: \"+n+\" had more than 1 outcome. Current choice: \"+Event[n].choice);");
//		ADD("		if (outcomes < 1) Debug.Log(\"PROBLEM: \"+n+\" had no outcomes. Current choice: \"+Event[n].choice);");
//	ADD("	};");
//	
//	charPos = charPosSave;
//}
//
//
//
//
//
//// ******** WRITER *****************************************************************
//
//
//function writeTemplate(file : String) {
//	
//    if (File.Exists(file)) { 
//        Debug.Log("Overwriting "+file); 
//        //return; 
//    }
//   
//	
//	
//    var sr = File.CreateText(Application.dataPath+ "/Standard Assets/scripts/ScriptHolder/" + file); 
//    
//    if (debugging) { Debug.Log("Writing "+file+" ..."); }
//
//    
//    for (var l : String in eventData) {
//    	
//    	sr.Write(l);
//    }
//    
//    //sr.WriteLine("Debug.Log(\"Debuggable: \");");
//    
//    //sr.WriteLine ("This is my file."); 
//    //sr.WriteLine ("I can write ints {0} or floats {1}, and so on.", 1, 4.2); 
//    sr.Close(); 
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
//
//
//// ********************************* RECORDERS (string, value, condition)
//
//
//function recordString(newLine : boolean) {
//
//		//var uniString : int = System.Convert.ToInt32("T"[0]); 
//		//Debug.Log("T int: "+uniString);
//		//Debug.Log("testing quote : char = n; > "+quote+" < ");
//	var record : String;
//	var stringStart : int = charPos;	// start pos of the record
//
//	var openVariable : boolean = false;
//
//	// move pos to first " or ” (8221)
//	for (var j : int = 0; j<12; j++) { 	// charInput(charPos) != quote
//	
//		// check template length
//		if (charPos+1 > templateLength) { Debug.LogWarning("End of record, templated length met"); break; }
//
//		// locate first "
//		if (charInput(charPos) == quote || charInput(charPos) == "”") { 
//			if (debugging) Debug.Log("First quote found at "+charPos+", starting to record"); 
//			stringStart = charPos+1;
//			charPos = stringStart;	// charPos += 1;
//			break;
//		} 
//
//		// catch comments
//		//if (charInput(charPos) == "/" && charInput(charPos+1) == "*") {
//		//	commented();
//		//}
//
//		// no " found
//		// Debugging format errors
//		//if (charPos - stringStart > 10) { Debug.Log("String not initialized after 10 chars with "+quote+" @ "+stringStart); break;}
//		
//		// charpos += 1
//		charPos += 1;
//	}
//		
//	charPos = stringStart;
//
//	
//
//	// record until next " or @
//	for (var k : int = 0; k<600; k++) {  	// "	charInput(charPos) != quote
//		
//		// check for commenting
//		if (charInput(charPos) == "/" && charInput(charPos+1) == "*") { return record; }
//
//		for (var r : int = 0; r<4; r++) {
//			// If second " or ” found
//			if (charInput(charPos) == quote || charInput(charPos) == "”") { if (debugging) { Debug.Log("Second quote found at "+charPos+",   Recorded: "+record); } break; }
//			
//			// if new @tag found
//			if (charInput(charPos) == "@") { if (debugging) { Debug.Log("New tag @ met while recording string at event: "+eventCount+" charPos: "+charPos+". String record stands: "+record); } break; }
//
//			// character 
//			if ((charInput(charPos) == "c" || charInput(charPos) == "C") && charInput(charPos+1) == "h" && charInput(charPos+2) == "a" && charInput(charPos+3) == "r" && charInput(charPos+4) == "a" && charInput(charPos+5) == "c" && charInput(charPos+6) == "t" && charInput(charPos+7) == "e" && charInput(charPos+8) == "r") { 
//					charPos += 9; record += "characters.character[Event[n].character]";  } //else Debug.LogError("ERROR: Trying to use \"character\", but variable is empty at event: "+eventCount); }
//			// advisor	(acting advisor) 
//			if ((charInput(charPos) == "a" || charInput(charPos) == "A") && charInput(charPos+1) == "d" && charInput(charPos+2) == "v" && charInput(charPos+3) == "i" && charInput(charPos+4) == "s" && charInput(charPos+5) == "o" && charInput(charPos+6) == "r") { 
//					charPos += 7; record += "characters.character[Event[n].advisor]"; }		
//			// location 
//			if (charInput(charPos-1) != "n" && charInput(charPos-1) != "t" && /*location*/ (charInput(charPos) == "l" || charInput(charPos) == "L") && charInput(charPos+1) == "o" && charInput(charPos+2) == "c" && charInput(charPos+3) == "a" && charInput(charPos+4) == "t" && charInput(charPos+5) == "i" && charInput(charPos+6) == "o" && charInput(charPos+7) == "n" && (charInput(charPos+8) == "+" || charInput(charPos+8) == "." )) { 
//					charPos += 8; record += "Event[n].location"; } //else Debug.LogError("ERROR: Trying to use \"location\", but variable is empty at event: "+eventCount); }
//			// gameTime 
//			if ((charInput(charPos) == "g" || charInput(charPos) == "G") && charInput(charPos+1) == "a" && charInput(charPos+2) == "m" && charInput(charPos+3) == "e" && (charInput(charPos+4) == "t" || charInput(charPos+4) == "T") && charInput(charPos+5) == "i" && charInput(charPos+6) == "m" && charInput(charPos+7) == "e") { 
//					charPos += 8; record += returnCharByInt(34); record += returnCharByInt(43); record += "gameTime.gameDay"; record += returnCharByInt(43); record += returnCharByInt(34); } 				
//			// +
//			if (charInput(charPos) == 43) { if (openVariable) { record += returnCharByInt(43); record += returnCharByInt(34); openVariable = false; charPosNext(); } else if (!openVariable) { record += returnCharByInt(34); record += returnCharByInt(43); openVariable = true; charPosNext(); } else Debug.LogError("Open variable mixup at event: "+eventCount); }
//		
//
//					// the"Assignment": theCaptain, theNavigator...
//			if ((charInput(charPos) == "t" || charInput(charPos) == "T") && charInput(charPos+1) == "h" && charInput(charPos+2) == "e" && charInput(charPos+3) != " ") { 
//				while (true) {
//					// captain
//					if ((charInput(charPos+3) == "c" || charInput(charPos+3) == "C") && charInput(charPos+4) == "a" && charInput(charPos+5) == "p" && charInput(charPos+6) == "t" && charInput(charPos+7) == "a" && charInput(charPos+8) == "i" && charInput(charPos+9) == "n") { 
//						charPos += 10; record += "characters.character[characters.assignment[\"captain\"]]"; break; }
//					// navigator
//					if ((charInput(charPos+3) == "n" || charInput(charPos+3) == "N") && charInput(charPos+4) == "a" && charInput(charPos+5) == "v" && charInput(charPos+6) == "i" && charInput(charPos+7) == "g" && charInput(charPos+8) == "a" && charInput(charPos+9) == "t" && charInput(charPos+10) == "o" && charInput(charPos+11) == "r") { 
//						charPos += 12; record += "characters.character[characters.assignment[\"navigator\"]]"; break; }
//					// engineer
//					if ((charInput(charPos+3) == "e" || charInput(charPos+3) == "E") && charInput(charPos+4) == "n" && charInput(charPos+5) == "g" && charInput(charPos+6) == "i" && charInput(charPos+7) == "n" && charInput(charPos+8) == "e" && charInput(charPos+9) == "e" && charInput(charPos+10) == "r") { 
//						charPos += 11; record += "characters.character[characters.assignment[\"engineer\"]]"; break; }
//					// security
//					if ((charInput(charPos+3) == "s" || charInput(charPos+3) == "S") && charInput(charPos+4) == "e" && charInput(charPos+5) == "c" && charInput(charPos+6) == "u" && charInput(charPos+7) == "r" && charInput(charPos+8) == "i" && charInput(charPos+9) == "t" && charInput(charPos+10) == "y") { 
//						charPos += 11; record += "characters.character[characters.assignment[\"security\"]]"; break; }
//					// quartermaster
//					if ((charInput(charPos+3) == "q" || charInput(charPos+3) == "Q") && charInput(charPos+4) == "u" && charInput(charPos+5) == "a" && charInput(charPos+6) == "r" && charInput(charPos+7) == "t" && charInput(charPos+8) == "e" && charInput(charPos+9) == "r" && charInput(charPos+10) == "m" && charInput(charPos+11) == "a" && charInput(charPos+12) == "s" && charInput(charPos+13) == "t" && charInput(charPos+14) == "e" && charInput(charPos+15) == "r") { 
//						charPos += 16; record += "characters.character[characters.assignment[\"quartermaster\"]]"; break; }
//					// psycher
//					if ((charInput(charPos+3) == "p" || charInput(charPos+3) == "P") && charInput(charPos+4) == "s" && charInput(charPos+5) == "y" && charInput(charPos+6) == "c" && charInput(charPos+7) == "h" && charInput(charPos+8) == "e" && charInput(charPos+9) == "r") { 
//						charPos += 10; record += "characters.character[characters.assignment[\"psycher\"]]"; break; }
//					// priest
//					if ((charInput(charPos+3) == "p" || charInput(charPos+3) == "P") && charInput(charPos+4) == "r" && charInput(charPos+5) == "i" && charInput(charPos+6) == "e" && charInput(charPos+7) == "s" && charInput(charPos+8) == "t") { 
//						charPos += 9;  record += "characters.character[characters.assignment[\"priest\"]]"; break; }
//					
//					break;
//				}
//			}
//
//		}
//
//
//		
//		// If second " or ” found
//		if (charInput(charPos) == quote || charInput(charPos) == "”") { if (debugging) { Debug.Log("Second quote found at "+charPos+",   Recorded: "+record); } break; }
//		
//		// Record				null				line feed: 13		new line: 10, 0x0a
//		if (charInput(charPos) != 0x0 && charPos <= templateLength) {
//			// no new lines / enters
//			if (!newLine) {
//				if (charInput(charPos) != 13 && charInput(charPos) != 10) record += charInput(charPos); 
//			}
//			else record += charInput(charPos);
//		}
//
//		// charPos += 1; if safe
//		if (charPos+1 <= templateLength) { charPos +=1; }
//		else { if (debugging) { Debug.Log("End of record, templated length met"); } break; }
//				
//			// Debugging format errors
//			if (charPos > templateLength) { Debug.LogError("No second quote found, template at its length."); break; }
//			//if (charInput(charPos) == "@") { Debug.LogError("New tag @ met while recording string - missing > "+quote+" < at event: "+eventCount+" charPos: "+charPos+". String record stands: "+record); break; }
//			if (charPos - stringStart > 500) { Debug.LogWarning("String recorded exceeds 500 chars at "+stringStart+" in "+record); break;}
//	}
//
//	return record;
//}
//
//function recordValue() {		// catch "="
//
//	var record : String;
//	var stringStart : int = charPos;	// start pos of the record
//
//
//	// move pos to first "="
//	for (var j : int = 0; j<12; j++) {
//	
//		// If = found start recording!	
//		if (charInput(charPos) == "=") { if (debugging) { Debug.Log(" \"=\" found at "+charPos+", starting to record value"); } break; }
//		
//		// charPos += 1; if safe
//		if (charPos+1 <= templateLength) { charPos +=1; }
//		else { if (debugging) { Debug.Log("End of record, templated length met");} break; }
//		
//			// Debugging format errors
//			if (charPos - stringStart > 10) { Debug.LogError(" ERROR: Missing \"=\" at event: "+eventCount +" /charPos: "+charPos); break;}
//	}
//
//	charPosNext();		// charPos += 1;
//
//	// record until next @ or # or \n or $
//	for (var q : int = 0; q<500; q++) {
//		for (var r : int = 0; r< 4; r++) {
//			// end If "@" or "#" or 10 (\n) or $ found
//			if (charInput(charPos) == "@" || charInput(charPos) == "#" || charInput(charPos) == 10 || charInput(charPos) == "$") { if (debugging) { Debug.Log("End of record \"@\" or new event or $effect found at "+charPos+",   Recorded: "+record); } break; }
//			
//			// available
//			if ((charInput(charPos) == "a" || charInput(charPos) == "A") && charInput(charPos+1) == "v" && charInput(charPos+2) == "a" && charInput(charPos+3) == "i" && charInput(charPos+4) == "l" && charInput(charPos+5) == "a" && charInput(charPos+6) == "b" && charInput(charPos+7) == "l" && charInput(charPos+8) == "e") { charPos += 9; record += "Event[n].available"; }	
//			
//			// character 
//			if ((charInput(charPos) == "c" || charInput(charPos) == "C") && charInput(charPos+1) == "h" && charInput(charPos+2) == "a" && charInput(charPos+3) == "r" && charInput(charPos+4) == "a" && charInput(charPos+5) == "c" && charInput(charPos+6) == "t" && charInput(charPos+7) == "e" && charInput(charPos+8) == "r") { 
//					charPos += 9; if (character != null) { record += "characters.character[Event[n].character]"; } else Debug.LogError("ERROR: Trying to use \"character\", but variable is empty at event: "+eventCount); }
//			// advisor	(acting advisor) 
//			if ((charInput(charPos) == "a" || charInput(charPos) == "A") && charInput(charPos+1) == "d" && charInput(charPos+2) == "v" && charInput(charPos+3) == "i" && charInput(charPos+4) == "s" && charInput(charPos+5) == "o" && charInput(charPos+6) == "r") { 
//					charPos += 7; record += "characters.character[Event[n].advisor]"; }		
//			// location 
//			if (charInput(charPos-1) != "n" && charInput(charPos-1) != "t" && /*location*/(charInput(charPos) == "l" || charInput(charPos) == "L") && charInput(charPos+1) == "o" && charInput(charPos+2) == "c" && charInput(charPos+3) == "a" && charInput(charPos+4) == "t" && charInput(charPos+5) == "i" && charInput(charPos+6) == "o" && charInput(charPos+7) == "n") { 
//					charPos += 8; record += "Event[n].location"; } //else Debug.LogError("ERROR: Trying to use \"location\", but variable is empty at event: "+eventCount); }
//			// "o" = outcome
//			if ((charInput(charPos) == "o" || charInput(charPos) == "O") && (charInput(charPos+1) == " " || charInput(charPos+1) == "=")) { charPos += 1; record += "Event[n].outcome"; }		
//			
//			// "c" = choice
//			if ((charInput(charPos) == "c" || charInput(charPos) == "C") && (charInput(charPos+1) == " " || charInput(charPos+1) == "=" || charInput(charPos+1) == "<" || charInput(charPos+1) == ">")) { charPos += 1; record += "Event[n].choice"; }			
//			
//			// gameTime 
//			if ((charInput(charPos) == "g" || charInput(charPos) == "G") && charInput(charPos+1) == "a" && charInput(charPos+2) == "m" && charInput(charPos+3) == "e" && (charInput(charPos+4) == "t" || charInput(charPos+4) == "T") && charInput(charPos+5) == "i" && charInput(charPos+6) == "m" && charInput(charPos+7) == "e") { 
//					charPos += 8; record += "gameTime.gameDay"; } 		
//			
//			// currentLocation 
//			if (charInput(charPos) == "c" && charInput(charPos+1) == "u" && charInput(charPos+2) == "r" && charInput(charPos+3) == "r" && charInput(charPos+4) == "e" && charInput(charPos+5) == "n" && charInput(charPos+6) == "t" && charInput(charPos+7) == "L" && charInput(charPos+8) == "o" && charInput(charPos+9) == "c" && charInput(charPos+10) == "a" && charInput(charPos+11) == "t" && charInput(charPos+12) == "i" && charInput(charPos+13) == "o" && charInput(charPos+14) == "n") { 
//					charPos += 15; record += "LocationFinder.Instance.currentLocation"; }
//
//							
//			// $ effect commands:			$	$	$
//			// End
//			if ((charInput(charPos) == "e" || charInput(charPos) == "E") && charInput(charPos+1) == "n" && charInput(charPos+2) == "d") { charPos += 3; record += "eventManager.inEvent = false; // EVENT CALLED TO AN END"; eventEnded = true; }	// END EVENT !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//		/*	
//			// basePopulationChange
//			if ((charInput(charPos) == "b" || charInput(charPos) == "B") && charInput(charPos+1) == "a" && charInput(charPos+2) == "s" && charInput(charPos+3) == "e" && (charInput(charPos+4) == "p" || charInput(charPos+4) == "P") && charInput(charPos+5) == "o" && charInput(charPos+6) == "p" && charInput(charPos+7) == "u" && charInput(charPos+8) == "l" && charInput(charPos+9) == "a" && charInput(charPos+10) == "t" && charInput(charPos+11) == "i" && charInput(charPos+12) == "o" && charInput(charPos+13) == "n" && (charInput(charPos+14) == "C" || charInput(charPos+14) == "c") && charInput(charPos+15) == "h" && charInput(charPos+16) == "a" && charInput(charPos+17) == "n" && charInput(charPos+18) == "g" && charInput(charPos+19) == "e") { 
//					charPos += 20; record += "Event[n].location.basePopulationChange"; if (!isLocationTrue()) Debug.LogError("BAD EVENT: event: "+eventCount+" has no location but calls for one with 'basePopulationChange'"); }
//			
//			// boostIdeology
//			if ((charInput(charPos) == "b" || charInput(charPos) == "B") && charInput(charPos+1) == "o" && charInput(charPos+2) == "o" && charInput(charPos+3) == "s" && charInput(charPos+4) == "t" && (charInput(charPos+5) == "i" || charInput(charPos+5) == "I") && charInput(charPos+6) == "d" && charInput(charPos+7) == "e" && charInput(charPos+8) == "o" && charInput(charPos+9) == "l" && charInput(charPos+10) == "o" && charInput(charPos+11) == "g" && charInput(charPos+12) == "y") { 
//					charPos += 13; record += "Event[n].location.boostIdeology"; if (!isLocationTrue()) Debug.LogError("BAD EVENT: event: "+eventCount+" has no location but calls for one with 'boostIdeology'"); }
//			
//			// factionChange
//			if ((charInput(charPos) == "f" || charInput(charPos) == "F") && charInput(charPos+1) == "a" && charInput(charPos+2) == "c" && charInput(charPos+3) == "t" && charInput(charPos+4) == "i" && charInput(charPos+5) == "o" && charInput(charPos+6) == "n" && (charInput(charPos+7) == "C" || charInput(charPos+7) == "c") && charInput(charPos+8) == "h" && charInput(charPos+9) == "a" && charInput(charPos+10) == "n" && charInput(charPos+11) == "g" && charInput(charPos+12) == "e") { 
//					charPos += 13; record += "Event[n].location.factionChange"; if (!isLocationTrue()) Debug.LogError("BAD EVENT: event: "+eventCount+" has no location but calls for one with 'factionChange'"); }
//
//			// agendaChange
//			if ((charInput(charPos) == "a" || charInput(charPos) == "A") && charInput(charPos+1) == "g" && charInput(charPos+2) == "e" && charInput(charPos+3) == "n" && charInput(charPos+4) == "d" && charInput(charPos+5) == "a" && (charInput(charPos+6) == "C" || charInput(charPos+6) == "c") && charInput(charPos+7) == "h" && charInput(charPos+8) == "a" && charInput(charPos+9) == "n" && charInput(charPos+10) == "g" && charInput(charPos+11) == "e") { 
//					charPos += 12; record += "Event[n].location.agendaChange"; if (!isLocationTrue()) Debug.LogError("BAD EVENT: event: "+eventCount+" has no location but calls for one with 'agendaChange'"); }
//		*/			
//					
//			// ^ ^ ^ ^ ^ ^ ^ ^ ^ 			$	$	$
//		}
//		// check for commenting
//		if (charInput(charPos) == "/" && charInput(charPos+1) == "*") commented();
//		
//		// end If "@" or "#" or 10 (\n) or $ found
//		if (charInput(charPos) == "@" || charInput(charPos) == "#" || charInput(charPos) == 10 || charInput(charPos) == "$") { if (debugging) { Debug.Log("End of record \"@\" or new event or $effect found at "+charPos+",   Recorded: "+record); } break; }
//			
//		// Record				null				line feed: 13		new line: 10, 0x0a
//		if (charInput(charPos) != 0x0 && charInput(charPos) != 13 && charInput(charPos) != 10 && charInput(charPos) != " " && charPos <= templateLength) { record += charInput(charPos); }	// don't record empty spaces
//				
//		// charPos += 1; if safe
//		if (charPos+1 <= templateLength) { charPos +=1; }
//		else { if (debugging) { Debug.Log("End of record, templated length met");} break; }
//		
//				
//			// Debugging format errors
//			if (charPos > templateLength) { if (debugging) { Debug.Log("Value record ends, template at its length.");} break; }
//			//if (charInput(charPos) == quote) { Debug.LogError(" \" met while recording value at charPos: "+charPos); break; }
//			if (charPos - stringStart > 500) { Debug.LogError("Value recorded exceeds 500 chars at "+stringStart+" in "+record); break;}
//	}
//
//	return record;
//}
//
//function recordCondition() {	// { if }
//
//	var record : String;
//	var stringStart : int = charPos;	// start pos of the record
//
//
//	// move pos to first "{"
//	for (var j : int = 0; j<12; j++) {
//	
//		// If "{" found start recording!	
//		if (charInput(charPos) == "{") { if (debugging) { Debug.Log("Opening \"bracket\" found at "+charPos+", starting to record"); } break; }
//		
//			// charPos += 1; if safe
//			if (charPos+1 <= templateLength) { charPos +=1; }
//			else { if (debugging) { Debug.Log("End of record, templated length met");} break; }
//			
//				// Debugging format errors
//				if (charPos - stringStart > 10) { Debug.LogError("String not initialized after 10 chars with "+quote+" @ event: "+eventCount+" pos:"+charPos); break;}
//	}
//
//	charPosNext();		// charPos += 1;
//
//	// record until "}"
//	for (var m : int = 0; m<600; m++) { 
//		
//		// RECORD
//		// "=" to "=="
//		if (charInput(charPos) == "=" && (charInput(charPos+1) == "<" || charInput(charPos+1) == ">")) Debug.Log("ERROR: logical mismatch, =< or => used instead of <= or >= at event "+eventCount+" charPos: "+charPos);
//		if (charInput(charPos) == "=" && charInput(charPos-1) != "<" && charInput(charPos-1) != ">" && charInput(charPos-1) != "!" && charInput(charPos-1) != "=" && charInput(charPos+1) != "=") { record += "="; }
//		
//		// "AND"
//		if (charInput(charPos) == "A" && charInput(charPos+1) == "N" && charInput(charPos+2) == "D") { charPos += 3; record += " && "; }	
//		
//		// "OR"
//		if (charInput(charPos) == "O" && charInput(charPos+1) == "R") { charPos += 2; record += " || "; }	
//		
//		// "NOT"
//		if (charInput(charPos) == "N" && charInput(charPos+1) == "O" && charInput(charPos+2) == "T") { charPos += 3; record += " != "; }	
//		
//		// "NO "
//		if (charInput(charPos) == "N" && charInput(charPos+1) == "O" && charInput(charPos+2) != "T") { if (charInput(charPos+2) == " ") { charPos += 1;} charPos += 2; record += "!"; }	
//		
//		// "o" = outcome
//		if ((charInput(charPos) == "o" || charInput(charPos) == "O") && (charInput(charPos+1) == " " || charInput(charPos+1) == "=")) { charPos += 1; record += "Event[n].outcome"; }		
//		
//		// "c" = choice
//			if ((charInput(charPos) == "c" || charInput(charPos) == "C") && (charInput(charPos+1) == " " || charInput(charPos+1) == "=" || charInput(charPos+1) == "<" || charInput(charPos+1) == ">")) { charPos += 1; record += "Event[n].choice"; }			
//		
//		// character 
//		if ((charInput(charPos) == "c" || charInput(charPos) == "C") && charInput(charPos+1) == "h" && charInput(charPos+2) == "a" && charInput(charPos+3) == "r" && charInput(charPos+4) == "a" && charInput(charPos+5) == "c" && charInput(charPos+6) == "t" && charInput(charPos+7) == "e" && charInput(charPos+8) == "r") { 
//				charPos += 9; if (character != null) { record += "characters.character[Event[n].character]"; } else Debug.LogError("ERROR: Trying to use \"character\", but variable is empty at event: "+eventCount); }
//		// advisor	(acting advisor) 
//		if ((charInput(charPos) == "a" || charInput(charPos) == "A") && charInput(charPos+1) == "d" && charInput(charPos+2) == "v" && charInput(charPos+3) == "i" && charInput(charPos+4) == "s" && charInput(charPos+5) == "o" && charInput(charPos+6) == "r") { 
//				charPos += 7; record += "characters.character[Event[n].advisor]"; }				
//		// locationObject 
//		if ((charInput(charPos) == "l" || charInput(charPos) == "L") && charInput(charPos+1) == "o" && charInput(charPos+2) == "c" && charInput(charPos+3) == "a" && charInput(charPos+4) == "t" && charInput(charPos+5) == "i" && charInput(charPos+6) == "o" && charInput(charPos+7) == "n" && /*Object*/ (charInput(charPos+8) == "O" || charInput(charPos+8) == "o") && charInput(charPos+9) == "b" && charInput(charPos+10) == "j" && charInput(charPos+11) == "e" && charInput(charPos+12) == "c" && charInput(charPos+13) == "t") { 
//				charPos += 14; if (location != null) { record += "Event[n].locationObject"; } else Debug.LogError("ERROR: Trying to use \"locationObject\", but variable is empty at event: "+eventCount); }
//		// location 
//		if (charInput(charPos-1) != "n" && charInput(charPos-1) != "t" && /*location*/ (charInput(charPos) == "l" || charInput(charPos) == "L") && charInput(charPos+1) == "o" && charInput(charPos+2) == "c" && charInput(charPos+3) == "a" && charInput(charPos+4) == "t" && charInput(charPos+5) == "i" && charInput(charPos+6) == "o" && charInput(charPos+7) == "n" && (charInput(charPos+8) != "O" || charInput(charPos+8) != "o")) { 
//				charPos += 8; if (location != null) { record += "Event[n].location"; } else Debug.LogError("ERROR: Trying to use \"location\", but variable is empty at event: "+eventCount); }
//		// gameTime 
//		if ((charInput(charPos) == "g" || charInput(charPos) == "G") && charInput(charPos+1) == "a" && charInput(charPos+2) == "m" && charInput(charPos+3) == "e" && (charInput(charPos+4) == "t" || charInput(charPos+4) == "T") && charInput(charPos+5) == "i" && charInput(charPos+6) == "m" && charInput(charPos+7) == "e") { 
//				charPos += 8; record += "gameTime.gameDay"; } 		
//		// loc 
////		if ((charInput(charPos) == "l" || charInput(charPos) == "L") && charInput(charPos+1) == "o" && charInput(charPos+2) == "c") { 
////				charPos += 3; record += "playerLoc()"; } 
//
//		// the"Assignment": theCaptain, theNavigator...
//		if ((charInput(charPos) == "t" || charInput(charPos) == "T") && charInput(charPos+1) == "h" && charInput(charPos+2) == "e" && charInput(charPos+3) != " ") { 
//			while (true) {
//				// advisor  !(to be used inside Events/Event.GetAdvice -scope only! advisor is name for the @a job )
//				if ((charInput(charPos+3) == "A" || charInput(charPos+3) == "a") && charInput(charPos+4) == "d" && charInput(charPos+5) == "v" && charInput(charPos+6) == "i" && charInput(charPos+7) == "s" && charInput(charPos+8) == "o" && charInput(charPos+9) == "r") { 
//					charPos += 10; record += "characters.character[advisor]"; break; }
//				// captain
//				if ((charInput(charPos+3) == "c" || charInput(charPos+3) == "C") && charInput(charPos+4) == "a" && charInput(charPos+5) == "p" && charInput(charPos+6) == "t" && charInput(charPos+7) == "a" && charInput(charPos+8) == "i" && charInput(charPos+9) == "n") { 
//					charPos += 10; record += "characters.character[characters.assignment[\"captain\"]]"; break; }
//				// navigator
//				if ((charInput(charPos+3) == "n" || charInput(charPos+3) == "N") && charInput(charPos+4) == "a" && charInput(charPos+5) == "v" && charInput(charPos+6) == "i" && charInput(charPos+7) == "g" && charInput(charPos+8) == "a" && charInput(charPos+9) == "t" && charInput(charPos+10) == "o" && charInput(charPos+11) == "r") { 
//					charPos += 12; record += "characters.character[characters.assignment[\"navigator\"]]"; break; }
//				// engineer
//				if ((charInput(charPos+3) == "e" || charInput(charPos+3) == "E") && charInput(charPos+4) == "n" && charInput(charPos+5) == "g" && charInput(charPos+6) == "i" && charInput(charPos+7) == "n" && charInput(charPos+8) == "e" && charInput(charPos+9) == "e" && charInput(charPos+10) == "r") { 
//					charPos += 11; record += "characters.character[characters.assignment[\"engineer\"]]"; break; }
//				// security
//				if ((charInput(charPos+3) == "s" || charInput(charPos+3) == "S") && charInput(charPos+4) == "e" && charInput(charPos+5) == "c" && charInput(charPos+6) == "u" && charInput(charPos+7) == "r" && charInput(charPos+8) == "i" && charInput(charPos+9) == "t" && charInput(charPos+10) == "y") { 
//					charPos += 11; record += "characters.character[characters.assignment[\"security\"]]"; break; }
//				// quartermaster
//				if ((charInput(charPos+3) == "q" || charInput(charPos+3) == "Q") && charInput(charPos+4) == "u" && charInput(charPos+5) == "a" && charInput(charPos+6) == "r" && charInput(charPos+7) == "t" && charInput(charPos+8) == "e" && charInput(charPos+9) == "r" && charInput(charPos+10) == "m" && charInput(charPos+11) == "a" && charInput(charPos+12) == "s" && charInput(charPos+13) == "t" && charInput(charPos+14) == "e" && charInput(charPos+15) == "r") { 
//					charPos += 16; record += "characters.character[characters.assignment[\"quartermaster\"]]"; break; }
//				// psycher
//				if ((charInput(charPos+3) == "p" || charInput(charPos+3) == "P") && charInput(charPos+4) == "s" && charInput(charPos+5) == "y" && charInput(charPos+6) == "c" && charInput(charPos+7) == "h" && charInput(charPos+8) == "e" && charInput(charPos+9) == "r") { 
//					charPos += 10; record += "characters.character[characters.assignment[\"psycher\"]]"; break; }
//				// priest
//				if ((charInput(charPos+3) == "p" || charInput(charPos+3) == "P") && charInput(charPos+4) == "r" && charInput(charPos+5) == "i" && charInput(charPos+6) == "e" && charInput(charPos+7) == "s" && charInput(charPos+8) == "t") { 
//					charPos += 9;  record += "characters.character[characters.assignment[\"priest\"]]"; break; }
//				
//			
//				break;
//			}
//		}
//
//		// check for commenting
//		//if (charInput(charPos) == "/" && charInput(charPos+1) == "*") commented();
//
//		// record
//		if (charInput(charPos) != 0x0 && charPos <= templateLength) { record += charInput(charPos); }
//		
//		// charPos += 1; if safe
//		if (charPos+1 <= templateLength) { charPos +=1; }
//		else { if (debugging) { Debug.Log("End of record, templated length met");} break; }
//		
//		// If closing "}" found
//		if (charInput(charPos) == "}") { if (debugging) { Debug.Log("Closing \"bracket\" found at "+charPos+",   Recorded: "+record); } break; }
//		
//			// Debugging format errors
//			if (charPos > templateLength) { Debug.LogError("No closing \"bracket\" found, template at its length."); break; }
//			if (charInput(charPos) == "@") { Debug.LogError("New tag @ met while recording string - missing \"bracket\" before charPos: "+charPos); break; }
//			if (charPos - stringStart > 500) { Debug.LogError("String recorded exceeds 500 chars at "+stringStart+" in "+record); break;}
//	}
//
//	// convert record skill values to 
//	record = EventSkillValueConvert.getSkillValue(record);
//
//	return record;
//}
//
//function charPosNext() {
//	// charPos += 1; if safe
//		if (charPos+1 <= templateLength) { charPos +=1; }
//		else { if (debugging) { Debug.Log("End of record, templated length met");} }
//}
//
//
//
//
//
//// ***************************************************************************************************** NOT IN USE -->
//// Split dictionaries to event and trigger
//
//function splitDictionaries() {
//
//	ADD(" \n ");
//	ADD(" \n \n \n \n \n \n \n \n ");
//	//ADD("// Split dictionaries to event and trigger");
//	ADD("// Count events and triggers");
//	ADD("function splitDictionaries() {\n");
//	//ADD("	var ks : List.<String> = new List.<String>();");
//	//ADD("	var k : String;");
//	//ADD("	var v : eventClass; \n");
//	ADD("	// go through all triggerable events");
//	ADD("	for (var e : KeyValuePair.<String, eventClass> in Event){");
//	ADD("		if (e.Value.triggerType != null) {");
//	//ADD("			k = e.Key;");
//	//ADD("			v = e.Value;");
//	ADD("			triggerCount += 1; /**/eventCount -= 1;");
//	//ADD("			trigger.Add(k, v);");
//	//ADD("			ks.Add(k);");
//	ADD("		}");
//	ADD("	}");
//	//ADD("	// go through and remove triggerable events from \"event\"");
//	//ADD("	for (var r : String in ks){");
//	//ADD("		eventCount -= 1;");
//	//ADD("		Event.Remove(r);");
//	//ADD("	}");
//	//ADD("	ks.Clear();");
//	ADD("}");
//}
//
//
//// *****************************************************************************************************
//// event tools
//
//
//function eventTools() {
//	
//	// Tools
//	ADD("// Tools");
//	ADD(" \n ");
//	
//		// player loc /true-false
//		ADD("function playerLoc(type : String, name : String) : boolean {");
//		ADD("	return finder.playerLoc(type, name); // returns \"true\" if palyer is in a confirmed location-state");
//		ADD("}");
//		
//		ADD(" \n ");
//		
//		
//		// object
//		ADD("function getObject(name : String) : GameObject {");
//		ADD("	return GameObject.Find(name);");
//		ADD("}");
//	
//	ADD(" \n ");
//
//
//	
//	// character tools
//	ADD("// Character Tools");
//	
//		// Character
//
//		// Get best character (skill) (from assigned characters)
//		ADD("function getBest(skill : String) : String {");
//		ADD("	return characters.getBest(skill);");
//		ADD("}");
//
//		// Get best character (skill) (from character pool)
//		ADD("function getBestFromAll(skill : String) : String {");
//		ADD("	return characters.getBestFromAll(skill);");
//		ADD("}");
//
//		// getAssigned (from assigned characters)
//		ADD("function getAssigned(job : String) : String {");
//		ADD("	return characters.getAssigned(job);");
//		ADD("}");
//
//		// get selected Character (from assigned characters)
//		ADD("function getSelected() : String {");
//		ADD("	return characters.selectedCharacter;");
//		ADD("}");
//
//
//
//		ADD(" \n ");
//
//		// Character pre-tools
//
//		// getStat (characterValue)
//		ADD("function getStat(characterValue : String) : float {");
//		ADD("	return characters.character[Event[n].character].getStat(characterValue);");
//		ADD("}");
//
//		// addStat (characterValue) [ Content effect ]
//		ADD("function addStat(characterValue : String, amount : float) {");
//		ADD("	characters.character[Event[n].character].addStat(characterValue, amount);");
//		ADD("}");
//
//		// setStat (characterValue)
//		ADD("function setStat(characterValue : String, amount : float) {");
//		ADD("	characters.character[Event[n].character].setStat(characterValue, amount);");
//		ADD("}");
//
//
//
//		ADD(" \n ");
//	
//		// advisor tools
//		ADD("// Advisor Tools");
//
//		// Get best character (skill) (from assigned characters)
//		ADD("function advisorSkill(advisorName : String, skill : String) : float {");
//		ADD("	return characters.character[advisorName].getStat(skill);");
//		ADD("}");
//
//
//
//	ADD(" \n ");
//
//	// location tools
//	ADD("// Location Tools");
//
//		// Location (gets location GameObject)
//
//		// get location (by String, or by db)
//		ADD("function getLocation(name : String) : GameObject {");
//		ADD("	return GameObject.Find(name);");
//		ADD("}");
//		ADD("function getLocation(loc : locationDatabase) : GameObject {");
//		ADD("	return loc.gameobject;");
//		ADD("}");
//
//		// one of the closest N locations
//		ADD("function getClosest(nth : int) : GameObject {");
//		ADD("	return collector.getClosestLocation(nth-1);");
//		ADD("}");
//
//				// closest location
//		ADD("function getOneOfTheClosest(nth : int) : GameObject {");
//		ADD("	return collector.getClosestLocation(Mathf.Floor(Random.Range(0,nth-1)));");
//		ADD("}");
//		
//		// closest location by faction
//		ADD("function getClosestByFaction(faction : String) : GameObject {");
//		ADD("	var loc : GameObject = collector.getClosestLocationByFaction(faction);");
//		ADD("	if (loc == null && eventManager.debugging) Debug.LogWarning(\"WARNING: No location controlled by \"+faction+\" found\");");
//		ADD("	return loc;");
//		ADD("}");
//
//		ADD(" \n ");
//
//		// Location pre-tools
//
//		// 
//
//	ADD(" \n ");
//
//	// Content condition
//	ADD("// Content condition");
//
//		// Make a stat roll (skill)
//		ADD("function statRoll(skill : String) : float {");
//		ADD("	return characters.statRoll(skill);");
//		ADD("}");
//			
//		// Make assignment skill roll
//		ADD("function assignmentRoll(assignment : String, skill : String)		{");
//		ADD("	return characters.assignmentRoll(assignment, skill);}");
//
//	ADD(" \n ");
//
//	// Content effects
//	ADD("// Content effects");
//
//		// Assign character
//		ADD("function assignCharacter(assignment : String)		{");
//		ADD("	characters.assign(Event[n].character, assignment);}");
//
//		// Assign someone for a job
//		ADD("function assign(name: String, assignment : String)		{");
//		ADD("	characters.assign(name, assignment);}");		
//
//		// Assign selected
//		ADD("function assignSelected(assignment : String)		{");
//		ADD("	characters.assign(characters.selectedCharacter, assignment);}");
//
//		// change situation [Quiet, Default, Alert, Panic]
//		ADD("function changeSituation(situation : status)		{");
//		ADD("	Event[n].situation = situation;}"); // ?
//		ADD(" // PING event sound state about the change? ");
//
//		// change ambient [QuietHum, QuietEerie, DefaultBridge, EchoRoom, EngineRoom, ActionBase, Fracture]
//		ADD("function changeAmbient(ambient : noise)		{");
//		ADD("	Event[n].ambient = ambient;}");
//		ADD(" // PING event sound state about the change? ");
//
//
//
//		// location effects
//		ADD("// 		location effects");
//
//		// basePopulationChange
//		ADD("function basePopulationChange(ideology : String, value : float) {");
//		ADD("	if (!Event[n].locationRequired) Debug.LogError(\"BAD EVENT: event: \"+n+\" has no location but calls for one with 'basePopulationChange'\");");
//		ADD("	Event[n].location.basePopulationChange(ideology, value);}");		
//
//		// boostIdeology
//		ADD("function boostIdeology(ideology : String, value : float)		{");
//		ADD("	if (!Event[n].locationRequired) Debug.LogError(\"BAD EVENT: event: \"+n+\" has no location but calls for one with 'boostIdeology'\");");
//		ADD("	Event[n].location.boostIdeology(ideology, value);}");		
//
//		// factionChange
//		ADD("function factionChange(faction : String, value : float)		{");
//		ADD("	if (!Event[n].locationRequired) Debug.LogError(\"BAD EVENT: event: \"+n+\" has no location but calls for one with 'factionChange'\");");
//		ADD("	Event[n].location.factionChange(faction, value);}");		
//
//		// agendaChange
//		ADD("function agendaChange(agenda : String, value : float)		{");
//		ADD("	if (!Event[n].locationRequired) Debug.LogError(\"BAD EVENT: event: \"+n+\" has no location but calls for one with 'agendaChange'\");");
//		ADD("	Event[n].location.agendaChange(agenda, value);}");		
//
//
//	ADD(" \n ");
//
//	// -------------------------- local tools
//
//	// filterWeight 	// Globally changes the filter's weight value
//	ADD("function filterWeight(filter : String, value : float) {");
//	ADD("	filterWeights[filter] = value;");
//	ADD("}");
//	
//	ADD(" \n ");
//
//
//
//}
//
//
