#pragma strict
import System.Collections.Generic;
/*
private var characters : Characters;

function Start() {
	//characters = GameObject.Find("ScriptHolder").gameObject.GetComponent("Characters");
	//sendAssignedCharacters();
	//sendClass()
}



function sendAssignedCharacters() {	// %3 : Name, portraitName, isActive (t/f)
	var characterDatapacket : List.<String> = new List.<String>();
	if (characters.assignment != null) {
		for (c in characters.assignment.Values) {
			// if character is assigned to the current job (assignment.Key)
			if (c != null) {
				// add name
				characterDatapacket.Add(characters.character[c].name);
				// add portraitName
				characterDatapacket.Add(characters.character[c].portraitName);
				// add isActive
				if (characters.character[c].isActive) characterDatapacket.Add("t");
				else characterDatapacket.Add("f");
			}
		}
	}
	else Debug.LogWarning("WARNING: no Character.assignment data. No-one assigned!");

	// send message
	SendMessage("characterPacketParser", characterDatapacket, SendMessageOptions.RequireReceiver);
}
*/
