//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

/*
CONVERT PORTRAIT TOOL


function assignPortraits() {
	var seekingTag : String = null;

	var takePortrait = function(c : characterClass, portraitN : int) {
		c.portraitName = portrait[portraitN].name;
		c.portraitGeneration();
		portrait[portraitN].status = portraitStatus.taken;
		//age selector
	};

	var findPortrait = function(c : characterClass) : boolean {	
		// go through portraits
		var count : int = portrait.Count;
		for (var i : int = 0; i<count; i++) {
			if (portrait[i].status == portraitStatus.available) {
				// if portrait and character tags match
				if (portrait[i].tag == c.portraitTag) {
					//if (debugging) Debug.Log("Portraits: mactch found for '"+c.name+": "+portrait[i].tag+" == portraitTag: c.portraitTag: "+c.portraitTag+".");
					takePortrait(c, i);
					return true;
				}
				// else remember tag to reset Used for
				else seekingTag = c.portraitTag;
			}
		}
		return false;
	};
	var makeUsedAvailable = function() : boolean {
		var count : int = portrait.Count;
		for (var i : int = 0; i<count; i++) {
			// seek used ones
			if (portrait[i].status == portraitStatus.used) {
				// if portrait tag is the one we need
				if (portrait[i].tag == seekingTag) {
					portrait[i].status = portraitStatus.available;
					return true;
				}
			}
		}
		return false;
	};
	var findNullPortrait = function(c : characterClass) : boolean {	
		// go through portraits
		var count : int = portrait.Count;
		for (var i : int = 0; i<count; i++) {
			if (portrait[i].status == portraitStatus.available) {
				// if null portrait found
				if (portrait[i].tag == null) {
					takePortrait(c, i);
					return true;
				}
			}
		}
		// no nulls free, make available
		seekingTag = null;
		if (!makeUsedAvailable()) {
			// no "used" nulls: assign first "used" of any tag
			for (i = 0; i<count; i++) {
				if (portrait[i].status == portraitStatus.used) {
					takePortrait(c, i);
					return true;
				}
			}
			// no "used" found
			return false;
		}
		// used null is now available, grab it
		else {
			for (i = 0; i<count; i++) {
				if (portrait[i].status == portraitStatus.available) {
					if (portrait[i].tag == null) {
						takePortrait(c, i);
						return true;
					}
				}
			}
			Debug.LogError("ERROR BUG: portrait was supposedly made available, but re-assign failed to find it.");
		}

		return false;
	};
	var cleanUnusedTaken = function() {
		// check if portrait "taken" count equals characters
		var portraitCount : int = portrait.Count;
		var characterCount : int = character.Count;
		var takenCount : int;
		for (var i : int = 0; i<portraitCount; i++) {
			if (portrait[i].status == portraitStatus.taken) {
				takenCount++;
			}
		}
		if (characterCount != takenCount) {
			var found : boolean;
			if (debugging) Debug.Log("Portraits: 'characterCount' count ("+characterCount+") mismatch with 'taken' portraits ("+takenCount+"): performing cleaning");
			// go thourgh all 'taken' portraits
			for (i = 0; i<portraitCount; i++) {
				if (portrait[i].status == portraitStatus.taken) {
					found = false;
					// go through all characters to find match
					for (c in character.Values) {
						if (c.portraitName == portrait[i].name) found = true;
					}
					// if match not found, clean it: make portrait 'used'
					if (!found) portrait[i].status = portraitStatus.used;
				}
			}
		}
		// count again to make sure it's ok
		takenCount = 0;
		for (i = 0; i<portraitCount; i++) {
			if (portrait[i].status == portraitStatus.taken) {
				takenCount++;
			}
		}
		if (characterCount != takenCount) { Debug.LogError("ERROR: couldn't clean mismatch between 'taken' portraits and the number of characters."); }
	};

	// ****************************************'
	// go through all characters
	for (c in character.Values) {
		// needs portrait
		if (c.portraitName == null) {
			// finding portrait
			//if (debugging) Debug.Log("Portraits: finding portrait for '"+c.name+"'");
			if (!findPortrait(c)) {
				// dind't find one, checking if "used" one can be released
				//if (debugging) Debug.Log("Portraits: dind't find one for '"+c.name+"', checking if 'used' one can be released.");
				if (!makeUsedAvailable()) {
					// if out of tagged portraits, force-assigning null-tagged portrait
					if (!findNullPortrait(c)) {
						Debug.LogWarning("ERROR: Out of portraits!");
					}
				}
				// portrait was released from "used" state, now assigning that
				else if (!findPortrait(c)) {
					Debug.LogError("ERROR BUG: portrait was supposedly made available, but re-assign failed to find it.");
				}
			}
		} 
	}
	cleanUnusedTaken();
	// ****************************************'
}
*/