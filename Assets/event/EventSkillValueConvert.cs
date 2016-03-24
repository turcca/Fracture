using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

static public class EventSkillValueConvert
{

    /// <summary>
    /// advisor tag parse (custom, not for general use!)
    /// </summary>
    /// <param name="parseString"></param>
    /// <returns></returns>
    static public string getSkillValueFromEntireLine(string parseString)
    {
        // NOTE: parseString is trimmed
        // separate condition and the rest

        // override!
        if (parseString.StartsWith("if") || parseString.StartsWith("return"))
        {
            return parseString;
        }

        if (parseString.StartsWith("if") && Regex.IsMatch(parseString, "[0-9]"))
        {
            string preCondition = "";
            string conditionContent = "";
            string postCondition = "";

            int count = parseString.Length;
            int i;
            // pre-condition
            for (i = 0; i < count; i++)
            {
                preCondition += parseString[i];
                if (parseString[i] == '(') break;
            }
            // conditionContent
            for (i = i + 1; i < count; i++)
            {
                if (parseString[i] == ')') break;
                conditionContent += parseString[i];
            }
            postCondition = parseString.Substring(i, count - i);

            // convert conditionContent
            conditionContent = getSkillValue(conditionContent);


            //	Debug.Log("parseString: "+parseString);
            //	Debug.Log(" --> "+preCondition + conditionContent + postCondition);

            // combine strings
            return preCondition + conditionContent + postCondition;
        }
        // no need to parse, string ok
        else return parseString;
    }

    private static string getSkillValue(string parseString)
    {
        string rv = parseString;
        bool isConverted = false;
        string[] splitStrings = splitString(parseString);
        for (int i = 0; i < splitStrings.Length; ++i)
        {
            foreach (string skillName in Character.getSkillNames())
            {
                if (splitStrings[i].Contains(skillName))
                {
                    splitStrings[i] = getIngameSkillValue(splitStrings[i], skillName);
                    isConverted = true;
                    break;
                }
            }
        }
        if (isConverted)
        {
            rv = "";
            foreach (string str in splitStrings)
            {
                rv += " " + str;
            }
        }
        return rv;
    }

    public static int convertValue(string skill, int value)
    {
        if ((Character.Stat?)Enum.Parse(typeof(Character.Stat), skill) == null) Debug.LogError("Invalid skill input: " + skill);
        return convertValue((Character.Stat)Enum.Parse(typeof(Character.Stat), skill), value);
    }
    /// <summary>
    /// event-based value (-6 - 6) converted to in-game value
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private static int convertValue(Character.Stat skill, int value)
    {
        // check bounds, if outside return raw number
        if (value < -3 || value > 6)
        {
            Debug.LogWarning("WARNING: '"+skill+"' value out of bounds: "+value);
            return value;
        }
        int newValue = 0;

        if (skill == Character.Stat.leadership)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 50;
            else if (value == 2) newValue = 100;
            else if (value == 3) newValue = 150;
            else if (value == 4) newValue = 250;
            else if (value == 5) newValue = 350;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.hr)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 50;
            else if (value == 2) newValue = 100;
            else if (value == 3) newValue = 150;
            else if (value == 4) newValue = 250;
            else if (value == 5) newValue = 350;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.engineering)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 50;
            else if (value == 2) newValue = 100;
            else if (value == 3) newValue = 150;
            else if (value == 4) newValue = 250;
            else if (value == 5) newValue = 350;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.precognition)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 150;
            else if (value == 2) newValue = 200;
            else if (value == 3) newValue = 250;
            else if (value == 4) newValue = 300;
            else if (value == 5) newValue = 400;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.psy)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 150;
            else if (value == 2) newValue = 200;
            else if (value == 3) newValue = 250;
            else if (value == 4) newValue = 300;
            else if (value == 5) newValue = 400;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.navigation)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 150;
            else if (value == 2) newValue = 200;
            else if (value == 3) newValue = 250;
            else if (value == 4) newValue = 300;
            else if (value == 5) newValue = 400;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.spaceBattle)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 100;
            else if (value == 2) newValue = 120;
            else if (value == 3) newValue = 150;
            else if (value == 4) newValue = 250;
            else if (value == 5) newValue = 350;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.combat)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 100;
            else if (value == 2) newValue = 120;
            else if (value == 3) newValue = 150;
            else if (value == 4) newValue = 250;
            else if (value == 5) newValue = 350;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.trading)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 100;
            else if (value == 2) newValue = 120;
            else if (value == 3) newValue = 150;
            else if (value == 4) newValue = 250;
            else if (value == 5) newValue = 350;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.diplomat)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 150;
            else if (value == 2) newValue = 200;
            else if (value == 3) newValue = 250;
            else if (value == 4) newValue = 325;
            else if (value == 5) newValue = 400;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.scientist)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 50;
            else if (value == 2) newValue = 100;
            else if (value == 3) newValue = 200;
            else if (value == 4) newValue = 300;
            else if (value == 5) newValue = 400;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.integrity)
        {
            if (value == -3) newValue = -300;
            else if (value == -2) newValue = -200;
            else if (value == -1) newValue = -100;
            else if (value == 0) newValue = 0;
            else if (value == 1) newValue = 100;
            else if (value == 2) newValue = 175;
            else if (value == 3) newValue = 300;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.holiness)
        {
            if (value == -3) newValue = -300;
            else if (value == -2) newValue = -200;
            else if (value == -1) newValue = -100;
            else if (value == 0) newValue = 0;
            else if (value == 1) newValue = 150;
            else if (value == 2) newValue = 300;
            else if (value == 3) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.purity)
        {
            if (value == -3) newValue = -200;
            else if (value == -2) newValue = -100;
            else if (value == -1) newValue = -50;
            else if (value == 0) newValue = 0;
            else if (value == 1) newValue = 150;
            else if (value == 2) newValue = 300;
            else if (value == 3) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.security)
        {
            if (value == -3) newValue = -300;
            else if (value == -2) newValue = -150;
            else if (value == -1) newValue = -100;
            else if (value == 0) newValue = 0;
            else if (value == 1) newValue = 175;
            else if (value == 2) newValue = 300;
            else if (value == 3) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.violent)
        {
            if (value == -3) newValue = -300;
            else if (value == -2) newValue = -100;
            else if (value == -1) newValue = -50;
            else if (value == 0) newValue = 0;
            else if (value == 1) newValue = 200;
            else if (value == 2) newValue = 300;
            else if (value == 3) newValue = 400;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.aristocrat)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 100;
            else if (value == 2) newValue = 150;
            else if (value == 3) newValue = 200;
            else if (value == 4) newValue = 300;
            else if (value == 5) newValue = 400;
            else if (value == 6) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.imperialist)
        {
            if (value == -3) newValue = -300;
            else if (value == -2) newValue = -200;
            else if (value == -1) newValue = -100;
            else if (value == 0) newValue = 0;
            else if (value == 1) newValue = 100;
            else if (value == 2) newValue = 200;
            else if (value == 3) newValue = 500;
            else Debug.LogError("ERROR: value = " + value);
        }
        else if (skill == Character.Stat.corruption)
        {
            if (value == 0) newValue = 0;
            else if (value == 1) newValue = 10;
            else if (value == 2) newValue = 20;
            else if (value == 3) newValue = 30;
            else if (value == 4) newValue = 50;
            else if (value == 5) newValue = 75;
            else if (value == 6) newValue = 100;
            else Debug.LogError("ERROR: value = " + value);
        }
        else
        {
            Debug.LogError("ERROR: unregistered skill encountered: " + skill.ToString()+ " : returning original value: "+value);
            return value;
        }
        return newValue;
    }

    static private string getIngameSkillValue(string parseString, string skill)
    {
        string numbersOnly = "0";
        int length = parseString.Length - 1;
        for (int i = length; i > 0; --i)
        {
            // check for "=", "<", ">" backwards from the end - logic operators end in ==, <=, >=, <, >, !=
            if (parseString[i] == '=' || parseString[i] == '<' || parseString[i] == '>')
            {
                numbersOnly = parseString.Substring(i + 1, length - i);
                parseString = parseString.Remove(i + 1);
                break;
            }
        }
        //Debug.Log("string: "+parseString);
        //Debug.Log("value: "+numbersOnly);
        try
        {
            int editorValue = System.Int32.Parse(numbersOnly);
            // get the converted value for current editor skill value
            editorValue = convertValue(skill, editorValue);
            // attach converted value to parseString
            parseString += " " + editorValue.ToString();
        }
        catch (System.FormatException e)
        {
            Debug.Log(e.ToString() + " - cannot convert: " + numbersOnly);
        }
        return parseString;
    }


    static private string[] splitString(string str)
    {
        return str.Split(new string[] { "&&", "||" }, System.StringSplitOptions.RemoveEmptyEntries);
    }

}
