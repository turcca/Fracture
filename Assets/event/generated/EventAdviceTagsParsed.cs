
/* TAGS (4)-------

agree
unsure
yes
no

--------------- */


// EventAdviceTagsParsed.cs compiled: 18/02/2016 22:43:18
using UnityEngine;
using System.Collections.Generic;
 
 
public static class EventAdviceTagsParsed
{
 

	static public string getEventAdviceTag (string tagRecord, Character advisor)
	{
		// change first to lowercase for matching. return string will be checked where called
		if (char.IsUpper(tagRecord[0]) ) tagRecord = EventAdviceTags.lowercaseFirst(tagRecord);
 

		// looking for tag match

		if (tagRecord == "agree")
		{
			if (advisor.getStat(Character.Stat.holiness) < 0 && advisor.getStat(Character.Stat.kind) < 40) return "damn right";
			if (advisor.getStat(Character.Stat.kind) < 20) return "well of course";
			if (advisor.isIdeology(Faction.IdeologyID.cult)) return "it is so";
			return "I agree";
		}
		if (tagRecord == "unsure")
		{
			if (advisor.getStat(Character.Stat.kind) < 10) return "don't look at me";
			if (advisor.getStat(Character.Stat.kind) > 90) return "I'm sorry, but I am not sure about this";
			if (advisor.isIdeology(Faction.IdeologyID.cult) && advisor.getStat(Character.Stat.holiness) > 1) return "we must pray for guidance";
			if (advisor.isIdeology(Faction.IdeologyID.cult)) return "there is a quote in scripture I am trying to remember";
			if (advisor.isIdeology(Faction.IdeologyID.navigators)) return "the matter lies outside of the scope of my knowledge";
			return "I am not sure";
		}
		if (tagRecord == "yes")
		{
			if (advisor.getStat(Character.Stat.kind) < 10) return "it's pretty obvious";
			if (advisor.isIdeology(Faction.IdeologyID.aristocrat))  return "aye";
			if (advisor.getStat(Character.Stat.leadership) > 3 && advisor.getStat(Character.Stat.hr) > 2) return "for sure";
			if (advisor.getStat(Character.Stat.idealist) > 70)  return "sure";
			return "yes";
		}
		if (tagRecord == "no")
		{
			if (advisor.getStat(Character.Stat.kind) < 10) return "no way";
			if (advisor.getStat(Character.Stat.kind) > 90) return "sorry but no";
			if (advisor.isIdeology(Faction.IdeologyID.aristocrat))  return "nay";
			return "no";
		}


		Debug.LogWarning("WARNING: tag "+tagRecord+" wasn't in 'EventAdviceTags.txt' --> 'EventAdviceTagsParsed.cs'");
		return "ERROR: &"+tagRecord+"&";
	}
}
