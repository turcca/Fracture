
/* TAGS (3)-------

agree
yes
no

--------------- */


// EventAdviceTagsParsed.cs compiled: 04/06/2014 14:01:01
using UnityEngine;
using System.Collections.Generic;


public static class EventAdviceTagsParsed
{


    static public string getEventAdviceTag(string tagRecord, Character advisor)
    {
        // change first to lowercase for matching. return string will be checked where called
        if (char.IsUpper(tagRecord[0])) tagRecord = EventAdviceTags.lowercaseFirst(tagRecord);


        // looking for tag match

        //if (tagRecord == "agree")
        //{
        //    if (advisor.ideology == "cult") return "it is so";
        //    return "I agree";
        //}
        //if (tagRecord == "yes")
        //{
        //    if (advisor.leadership > 150 && advisor.hr > 100) return "for sure";
        //    if (advisor.idealist < 10) return "sure";
        //    if (advisor.ideology == "aristocrat") return "aye";
        //    return "yes";
        //}
        //if (tagRecord == "no")
        //{
        //    if (advisor.ideology == "aristocrat") return "nay";
        //    return "no";
        //}


        Debug.LogWarning("WARNING: tag " + tagRecord + " wasn't in 'EventAdviceTags.txt' --> 'EventAdviceTagsParsed.cs'");
        return "ERROR: &" + tagRecord + "&";
    }
}
