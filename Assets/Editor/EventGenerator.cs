using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

public class EventGenerator
{
    private string generatedScript = "";
    private int numEvents = 0;
    public void generateEvents(string[] dataFiles)
    {
        generatedScript = "";
        string data = "";
        StreamReader sr;// = new StreamReader();
        foreach (string dataFile in dataFiles)
        {
            sr = new StreamReader(dataFile);
            data += sr.ReadToEnd();
            sr.Close();
            Debug.Log("   >Event file: '" + dataFile + "'");
        }
        //StreamReader sr = new StreamReader(dataFile);
        //string data = sr.ReadToEnd();
        if (data == null)
        {
            Debug.Log("could not read data from " + dataFiles.ToString());
        }
        //sr.Close();

        writeLine ("// Events.cs compiled: "+System.DateTime.Now.ToString("HH:mm:ss")+" "+System.DateTime.Now.ToString("dd/MM/yyyy"));
        writeLine("#pragma warning disable 0162, 1717");
        writeLine("using System;");

        string[] eventStrs = data.Split(new string[] { "#" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string eventStr in eventStrs)
        {
            readEvent("#" + eventStr);
        }

        string outFile = Application.dataPath + "/event/generated/Events.cs";
        StreamWriter sw = File.CreateText(outFile);
        sw.Write(generatedScript);
        sw.Close();
    }

    // events

    void readEvent(string data)
    {
        numEvents++;
        string name = getEventName(data);
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
        writeLine("//------------------------------------------------------- FREQUENCY AND AMBIENT");
        readFreqAndSound(tags);
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
        writeLine("//------------------------------------------------------- FILTERS");
        readFilters(tags);
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
                    writeLine("triggerEvent=trigger.atLocation;");
                    writeLine("locationRequired=true;");
                }
                else if (Regex.Match(tag.Substring(8), "inLocation").Success)
                {
                    writeLine("triggerEvent=trigger.inLocation;");
                    writeLine("locationRequired=true;");
                }
                else if (Regex.Match(tag.Substring(8), "Object").Success)
                {
                    writeLine("triggerEvent=trigger.Object;");
                }
            }
            else if (Regex.Match(tag, @"^\@location[\s\{=]").Success)
            {
                string id = getNextValue(tag.Substring(10));
                id = Regex.Replace(id, @"getLocation\((.*)\)", "\"$1\"");
                id = Regex.Replace(id, "currentLocation", "getPlayerLocationID()");
                writeLine("location=" + id.ToLower() + ";");
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
        writeLine("base.lastProbability = p;"); // todo: add filter weight mul to p
        writeLine("return base.lastProbability;");
        writeLine("}");
    }
    private void readProbability(string tag)
    {
        checkAndConsumeConditional(ref tag);
        writeLine("{");
        writeLine("p *= " + getNextValue(tag).Replace("\r", "").Replace("\n", "") + ";");
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
                readAdvice(tag.Substring(2)/*.Replace("\n", "\\n")*/); // keep newlines in advice text strings
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
        writeLine("eventAdvice.text = \"" + getNextString(tag).Replace("\r\n", "\\n") + "\";");
        if (getNextValue(tag) != "")
        {
            writeLine("eventAdvice.recommend = " + getNextValue(tag).Replace("\r\n", "") + ";");
        }
        writeLine("return eventAdvice;");
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
                readText( tag.Substring(2).Replace("\r\n", "\\n")) ; // keep newlines in text strings
            }
        }
        writeLine("return \"INSERT TEXT HERE\";"); // possible fall-through
        writeLine("}");
    }
    private string convertPlusesToParsed(string inputStr)
    {
        // convert +'s into enclosing "+, +"
        string input = inputStr;
        int n = 0;
        char plus = char.Parse("+");

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == plus)
            {
                n++;
                // if first of pair
                if (n % 2 != 0) { input = input.Insert(i, "\""); i += 3; }
                // second of pair
                else input = input.Insert(i + 1, "\"");
                //Debug.Log("parse string: found '+' (" + n + ") @i: " + i + " (str length: " + input.Length + ")");
            }
        }
        if (n % 2 != 0) Debug.LogError("EventGenerator parse error on '+'. Odd number of +'s\n" + input);
        //Debug.Log("--> " + input);
        return input;
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
        effect = Regex.Replace(effect, @"resetOutcome", "outcome = 0");
        effect = Regex.Replace(effect, @"character.addStat\(.([a-z]*).*([0-9\.]{1,})\)", @"addCharacterStat(Character.Stat.$1, $2)");
        effect = Regex.Replace(effect, @"character.([a-z]*).*=([0-9\.]*)", @"setCharacterStat(Character.Stat.$1, $2)");
        effect = Regex.Replace(effect, @"\r", @"");
        effect = Regex.Replace(effect, @"\s", @"");
        writeLine(effect + ";");
        return true;
    }
    private void readFilters(string[] filters)
    {
        writeLine("public override void initFilters() {");
        foreach (string filter in filters)
        {
            if (filter.StartsWith("@f ", System.StringComparison.OrdinalIgnoreCase))
            {
                readFilter(filter.Substring(1));
            }
        }
        writeLine("}");
    }
    private void readFilter(string mod)
    {
        writeLine("addFilter(\"" + getNextString(mod)+"\");");
    }

    private void readFreqAndSound(string[] tags)
    {
        foreach (string tag in tags)
        {
            if (Regex.Match(tag, @"^\@frequency[\s\{]").Success)
            {
                readFrequency(getNextString(tag));
            }
            else if (Regex.Match(tag, @"^\@situation[\s\{]").Success)
            {
                readStatus(getNextString(tag));
            }
            else if (Regex.Match(tag, @"^\@ambient[\s\{]").Success)
            {
                readNoise(getNextString(tag));
            }
        }
    }
    private void readFrequency(string tag)
    {
        writeLine("public override freq getFrequency() {");
        writeLine("return freq."+System.Enum.Parse(typeof(EventBase.freq), tag) + ";");
        writeLine("}");
    }
    private void readStatus(string tag)
    {
        writeLine("public override status getCrewStatus() {");
        writeLine("return status."+System.Enum.Parse(typeof(EventBase.status), tag) + ";");
        writeLine("}");
    }
    private void readNoise(string tag)
    {
        writeLine("public override noise getAmbientNoise() {");
        writeLine("return noise."+System.Enum.Parse(typeof(EventBase.noise), tag) + ";");
        writeLine("}");
    }

    // helpers

    private string getNextValue(string tag)
    {
        if (tag.Contains("="))
        {
            string value = tag.Substring(tag.IndexOf("=") + 1);
            return value.Substring(0, value.IndexOfAny(new char[] { '\n', '$' }));
        }
        else
        {
            return "";
        }
    }
    string getNextString(string data)
    {
        int stringStartPos = data.IndexOf("\"");
        if (stringStartPos == -1) throw new System.Exception("Event generator throw (data:"+data+")");
        int length = data.Substring(stringStartPos + 1).IndexOf("\"");
        if (length == -1) throw new System.Exception("Event generator throw (missing end quote?) \n"+data);
        return convertPlusesToParsed(data.Substring(stringStartPos + 1, length));
    }
    string getEventName(string data)
    {
        int stringStartPos = data.IndexOf("#");
        if (stringStartPos == -1) throw new System.Exception("Event generator throw");
        int length = data.Substring(stringStartPos + 1).IndexOf("\n")-1;
        if (length == -1) throw new System.Exception("Event generator throw");
        //Debug.Log ("event: "+data.Substring(stringStartPos + 1, length));
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
        cond = Regex.Replace(cond, @"^o( |=)", "outcome$1");
        cond = Regex.Replace(cond, @"^c( |=)", "choice$1");
        cond = Regex.Replace(cond, @"shipstat\(([a-z]*)\)", "getShipStat(\"$1\")");
        cond = Regex.Replace(cond, "character.([a-z]*)", "getCharacterStat(Character.Stat.$1)", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "TheAdvisor", "getCharacter(job)", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "currentLocation", "getCurrentLocation()", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "locationObject", "locationObject", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "gameTime", "getElapsedDays()", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, "warpmag", "getWarpMagnitude()", RegexOptions.IgnoreCase);
        //cond = Regex.Replace(cond, "finder.magnitude", "getWarpMagnitude()", RegexOptions.IgnoreCase);
        //@todo
        cond = Regex.Replace(cond, @"playerLoc\(.atLocation.,.*\)", "(getPlayerLocationID() == location)", RegexOptions.IgnoreCase);
        //@todo
        cond = Regex.Replace(cond, @"playerLoc\(.inLocation.,.*\)", "(getPlayerLocationID() == location)", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, @"Event\[""(.*?)""\]", "getEvent(\"$1\")", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, @"theCaptain.([a-z]*)", "getCharacterStat(Character.Job.captain, Character.Stat.$1)", RegexOptions.IgnoreCase);
        cond = Regex.Replace(cond, @"location\.ideologyStats\[.([a-z]+).\]", "getLocation().ideology.effects[Simulation.Effect.$1]", RegexOptions.IgnoreCase);
        
        // need to convert eventEdit skill scale into ingame skill scale
        if (cond.Contains("getStat("))
        {
            //                                  get stat ("    <skill>                ") >=                 -4 / 4   
            Regex pattern = new Regex(@"(?<getStat>getStat..)(?<skill>[a-zA-Z]+)(?<logic>...[<>=]+.)(?<value>\-\d|\d+)"); //@"getStat=(?<skill>.*?)");
            Match match = pattern.Match(cond);
            // only skills need converting (not age etc.)
            if (Character.skillList.Contains((Character.Stat)Enum.Parse(typeof(Character.Stat), match.Groups["skill"].Value)))
            {
                string replaceValue = "${getStat}${skill}${logic} "+ EventSkillValueConvert.convertValue(match.Groups["skill"].Value, int.Parse(match.Groups["value"].Value)).ToString()+" ";
                //Debug.Log("replaceValue: " + replaceValue);
                cond = Regex.Replace(cond, pattern.ToString(), replaceValue);
            }
        }
        
        mod = mod.Substring(condEndPos + 1); 
        return cond;
    }

    private void writeLine(string line)
    {
        generatedScript += line + System.Environment.NewLine;
    }
}
