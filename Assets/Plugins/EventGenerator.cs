using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

public class EventGenerator
{
    private string generatedScript = "";
    private int numEvents = 0;
    public void generateEvents(string dataFile)
    {
        generatedScript = "";
        StreamReader sr = new StreamReader(dataFile);
        string data = sr.ReadToEnd();
        if (data == null)
        {
            Debug.Log("could not read data from " + dataFile);
        }
        sr.Close();

        writeLine("#pragma warning disable 0162, 1717");
        writeLine("using System;");

        string[] eventStrs = data.Split(new string[] { "#" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string eventStr in eventStrs)
        {
            readEvent("#" + eventStr);
        }

        string outFile = Application.dataPath + "/event/generated/Events.cs";
        Debug.Log(generatedScript);
        StreamWriter sw = File.CreateText(outFile);
        sw.Write(generatedScript);
        sw.Close();
    }

    // events

    void readEvent(string data)
    {
        numEvents++;
        string name = getNextString(data);
        writeLine("//---------------------------------------------------------------------------------");
        writeLine("//------------------------------------------------------- EVENT " + numEvents);
        writeLine("//---------------------------------------------------------------------------------");
        writeLine("public class Event_" + numEvents.ToString() + " : EventBase {");
        writeLine("public Event_" + numEvents.ToString() + "() : base(\"" + name + "\") {}");
        string[] tags = data.Split(new string[] { "@" }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < tags.Length; ++i )
        {
            tags[i] = "@" + tags[i];
        }
        writeLine("//------------------------------------------------------- PREINIT");
        readPreTags(tags);
        writeLine("//------------------------------------------------------- PROBABILITY");
        readProbabilities(tags);
        writeLine("//------------------------------------------------------- ADVICE");
        readAdvices(tags);
        writeLine("//------------------------------------------------------- TEXT");
        readTexts(tags);
        writeLine("//------------------------------------------------------- CHOICES");
        readChoices(tags);
        writeLine("//------------------------------------------------------- OUTCOMES");
        readOutcomes(tags);
        //readFilters(tags);
        writeLine("}");
    }

    // tags

    private void readPreTags(string[] tags)
    {
        writeLine("public override void initPre() {");
        foreach (string tag in tags)
        {
            if (Regex.Match(tag, @"^\@trigger[\s\{=]").Success)
            {
                if (Regex.Match(tag.Substring(8), "atLocation").Success)
                {
                    writeLine("locationEvent=true;");
                }
            }
            else if (Regex.Match(tag, @"^\@location[\s\{=]").Success)
            {
                string id = getNextValue(tag.Substring(8));
                id = Regex.Replace(id, @"getLocation\((.*)\)", "$1");
                id = Regex.Replace(id, "currentLocation", "getPlayerLocationID()");
                writeLine("location=" + id +";");
            }
            else if (Regex.Match(tag, @"^\@character[\s\{=]").Success)
            {
                string id = getNextValue(tag.Substring(10));
                id = Regex.Replace(id, @"getBestFromAll\(.([a-z]*).\)", "Character.Stat.$1");
                id = Regex.Replace(id, @"getBest\(.([a-z]*).\)", "Character.Stat.$1");
                writeLine("character = getBestCharacter("+ id +");");
            }
        }
        writeLine("}");
    }

    private void readProbabilities(string[] tags)
    {
        writeLine("public override float calculateProbability() {");
        writeLine("float p = 1.0f;");
        foreach (string tag in tags)
        {
            if (Regex.Match(tag, @"^\@p[\s\{]").Success)
            {
                readProbability(tag.Substring(2));
            }
        }
        writeLine("return p;");
        writeLine("}");
    }
    private void readProbability(string tag)
    {
        checkAndConsumeConditional(ref tag);
        writeLine("{");
        writeLine("p = p * " + getNextValue(tag) + ";");
        writeLine("}");

    }

    private void readAdvices(string[] tags)
    {
        writeLine("public override EventAdvice getAdvice(Character.Job job) {");
        writeLine("EventAdvice eventAdvice = new EventAdvice();");
        foreach (string tag in tags)
        {
            if (Regex.Match(tag, @"^\@a[\s\{]").Success)
            {
                readAdvice(tag.Substring(2));
            }
        }
        writeLine("return eventAdvice;");
        writeLine("}");
    }
    private void readAdvice(string tag)
    {
        Match m = Regex.Match(tag, @"([a-z]+)", RegexOptions.IgnoreCase);
        if (m.Groups.Count == 0) return;

        string who = m.Groups[0].Value;
        writeLine("if (job == Character.Job." + who + ") {");
        checkAndConsumeConditional(ref tag);
        writeLine("{");
        writeLine("eventAdvice.text = \"" + getNextString(tag) + "\";");
        if (getNextValue(tag) != "")
        {
            writeLine("eventAdvice.recommend = " + getNextValue(tag) + ";");
        }
        writeLine("}");
        writeLine("}");
    }

    private void readTexts(string[] tags)
    {
        writeLine("public override string getText() {");
        foreach (string tag in tags)
        {
            if (Regex.Match(tag, @"^\@t[\s\{]").Success)
            {
                Debug.Log(tag);
                readText(tag.Substring(2));
            }
        }
        writeLine("return \"INSERT TEXT HERE\";");
        writeLine("}");
    }
    private void readText(string tag)
    {
        checkAndConsumeConditional(ref tag);
        writeLine("{");
        writeLine("return \"" + getNextString(tag) + "\";");
        writeLine("}");
    }

    private void readChoices(string[] tags)
    {
        writeLine("public override void initChoices() {");
        foreach (string tag in tags)
        {
            if (Regex.Match(tag, @"^\@c[\s\{]").Success)
            {
                readChoice(tag.Substring(1));
            }
        }
        writeLine(" }");
    }
    private void readChoice(string tag)
    {
        checkAndConsumeConditional(ref tag);
        writeLine("{");
        writeLine("choices.Add(\"" + getNextString(tag) + "\", " + getNextValue(tag) + ");");
        writeLine("}");
    }

    private void readOutcomes(string[] tags)
    {
        writeLine("public override void doOutcome() {");
        foreach (string tag in tags)
        {
            if (tag.StartsWith("@o ", System.StringComparison.OrdinalIgnoreCase))
            {
                readOutcome(tag.Substring(1));
            }
        }
        writeLine("}");
    }
    private void readOutcome(string tag)
    {
        checkAndConsumeConditional(ref tag);
        writeLine("{");
        string outcomeResult = getNextValue(tag);
        if (Regex.Match(outcomeResult, "c").Success) outcomeResult = "choice";
        else if (Regex.Match(outcomeResult, "o").Success) outcomeResult = "outcome";
        outcomeResult = Regex.Replace(outcomeResult, @"/r", "");
        outcomeResult = Regex.Replace(outcomeResult, @"/s", "");
        writeLine("outcome=" + outcomeResult + ";");

        int i = 0;
        while (checkAndConsumeEffect(ref tag) || i > 99)
        {
            ++i;
        };
        writeLine("}");
    }
    private bool checkAndConsumeEffect(ref string tag)
    {
        int startPos = tag.IndexOf("$=");
        if (startPos == -1) return false;
        int length = tag.Substring(startPos + 2).IndexOf("$=");
        string effect = "";
        if (length != -1)
        {
            effect = tag.Substring(startPos + 2, length);
            tag = tag.Substring(startPos + 2 + length);
        }
        else
        {
            effect = tag.Substring(startPos + 2);
            tag = "";
        }

        effect = Regex.Replace(effect, @"end", "end()");
        effect = Regex.Replace(effect, @"character.addStat\(.([a-z]*).*([0-9\.]{1,})\)", @"addCharacterStat(Character.Stat.$1, $2)");
        effect = Regex.Replace(effect, @"character.([a-z]*).*=([0-9\.]*)", @"setCharacterStat(Character.Stat.$1, $2)");
        effect = Regex.Replace(effect, @"\r", @"");
        effect = Regex.Replace(effect, @"\s", @"");

        writeLine(effect + ";");
        return true;
    }

    private void readFilter(string mod)
    {
        writeLine("e.addFilter(\"" + getNextString(mod) + "\");");
    }

    // helpers

    private string getNextValue(string tag)
    {
        if (tag.Contains("="))
        {
            string value = tag.Substring(tag.IndexOf("=") + 1);
            return value.Substring(0, value.IndexOfAny(new char[] { '\n', '$' }) - 1);
        }
        else
        {
            return "";
        }
    }
    string getNextString(string data)
    {
        int stringStartPos = data.IndexOf("\"");
        if (stringStartPos == -1) throw new System.Exception("Event generator throw");
        int length = data.Substring(stringStartPos + 1).IndexOf("\"");
        if (length == -1) throw new System.Exception("Event generator throw");
        return data.Substring(stringStartPos + 1, length);
    }

    private void checkAndConsumeConditional(ref string tag)
    {
        string cond = consumeCondition(ref tag);
        if (cond != "")
        {
            writeLine("if (" + cond + ")");
        }
    }
    private string consumeCondition(ref string mod)
    {
        int condStartPos = mod.IndexOf("{");
        if (condStartPos == -1) return "";
        int condEndPos = mod.IndexOf("}");
        if (condEndPos == -1) return "";
        string cond = mod.Substring(condStartPos + 1, (condEndPos - condStartPos - 1));// < 0 ? 0 : condEndPos - condStartPos - 1);

        cond = Regex.Replace(cond, "[^<>=!]=", "==");
        cond = Regex.Replace(cond, "AND", "&&");
        cond = Regex.Replace(cond, "OR", "||");
        cond = Regex.Replace(cond, "NOT", "!=");
        cond = Regex.Replace(cond, "NO", "!");
        cond = Regex.Replace(cond, @"o( |=)", "outcome$1");
        cond = Regex.Replace(cond, @"c( |=)", "choice$1");
        cond = Regex.Replace(cond, "character.([a-z]*)", "getCharacterStat(Character.Stat.$1)", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "advisor", "getAdvisor()", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "locationObject", "locationObject", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "gameTime", "getElapsedDays()", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "finder.magnitude", "getWarpMagnitude()", RegexOptions.IgnoreCase);
        //@todo
        cond = Regex.Replace(cond, @"playerLoc\(.atLocation.,.*\)", "(getPlayerLocationID() == location)", RegexOptions.IgnoreCase);
        //@todo
        cond = Regex.Replace(cond, @"playerLoc\(.inLocation.,.*\)", "(getPlayerLocationID() == location)", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, @"Event\[(.*)\]", "getEvent($1)", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, @"theCaptain.([a-z]*)", "getCharacterStat(Character.Job.captain, Character.Stat.$1)", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, @"location\.ideologyStats\[.([a-z]+).\]", "getLocation().ideology.effects.$1", RegexOptions.IgnoreCase);


        mod = mod.Substring(condEndPos + 1);
        return cond;
    }

    private void writeLine(string line)
    {
        generatedScript += line + "\n";
    }
}
