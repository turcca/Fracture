//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.RegularExpressions;
//using System.Xml;

//public class EventGenerator
//{
//    private string generatedScript = "";
//    private int numEvents = 0;
//    public void generateEvents(string dataFile)
//    {
//        generatedScript = "";
//        StreamReader sr = new StreamReader(dataFile);
//        string data = sr.ReadToEnd();
//        if (data == null)
//        {
//            Debug.Log("could not read data from " + dataFile);
//        }
//        sr.Close();

//        string[] eventStrs = data.Split(new string[] { "#" }, System.StringSplitOptions.RemoveEmptyEntries);
//        foreach (string eventStr in eventStrs)
//        {            
//            readEvent("#" + eventStr);
//        }

//        string outFile = Application.dataPath + "/event/generated/Events.cs";
//        Debug.Log(generatedScript);
//        StreamWriter sw = File.CreateText(outFile); 
//        sw.Write(generatedScript);
//        sw.Close(); 
//    }

//    void readEvent(string data)
//    {
//        numEvents++;
//        string name = getNextString(data);
//        writeLine("public class Event_" + numEvents.ToString() + " : EventBase {");
//        writeLine(" public Event_" + numEvents.ToString() + "() : base(\"" + name + "\") {}");
//        string[] tags = data.Split(new string[] { "@" }, System.StringSplitOptions.RemoveEmptyEntries);
//        readProbabilities(tags);
//        readAdvices(tags);
//        readTexts(tags);
//        readChoices(tags);
//        readOutcomes(tags);
//        //readFilters(tags);
//        writeLine("}");
//    }

//    private void readOutcomes(string[] tags)
//    {
//        writeLine(" public override void doOutcome() {");
//        foreach (string tag in tags)
//        {
//            if (tag.StartsWith("o ", System.StringComparison.OrdinalIgnoreCase))
//            {
//                readOutcome(tag.Substring(1));
//            }
//        }
//        writeLine(" }");
//    }

//    private void readOutcome(string tag)
//    {
//        checkAndConsumeConditional(ref tag);
//        writeLine("  {");
//        writeLine("   outcome = " + getNextValue(tag) + ";");
//        int i = 0;
//        while (checkAndConsumeEffect(ref tag) || i > 99)
//        {
//            ++i;
//        };
//        writeLine("  }");
//    }

//    private bool checkAndConsumeEffect(ref string tag)
//    {
//        int startPos = tag.IndexOf("$=");
//        if (startPos == -1) return false;
//        int length = tag.Substring(startPos+2).IndexOf("$=");
//        string effect = "";
//        if (length != -1)
//        {
//            effect = tag.Substring(startPos+2, length);
//            tag = tag.Substring(startPos+2+length);
//        }
//        else
//        {
//            effect = tag.Substring(startPos+2);
//            tag = "";
//        }
//        writeLine(effect + ";");
//        return true;
//    }

//    private void readChoices(string[] tags)
//    {
//        writeLine(" public override void initChoices() {");
//        foreach (string tag in tags)
//        {
//            if (tag.StartsWith("c ", System.StringComparison.OrdinalIgnoreCase))
//            {
//                readChoice(tag.Substring(1));
//            }
//        }
//        writeLine(" }");
//    }

//    private void readChoice(string tag)
//    {
//        checkAndConsumeConditional(ref tag);
//        writeLine("  {");
//        writeLine("   choices.Add(\"" + getNextString(tag) + "\", " + getNextValue(tag) + ");");
//        writeLine("  }");
//    }

//    private void readTexts(string[] tags)
//    {
//        writeLine(" public override string getText() {");
//        foreach (string tag in tags)
//        {
//            if (tag.StartsWith("t ", System.StringComparison.OrdinalIgnoreCase))
//            {
//                Debug.Log(tag);
//                readText(tag.Substring(1));
//            }
//        }
//        writeLine(" return \"\";");
//        writeLine(" }");
//    }

//    private void readText(string tag)
//    {
//        checkAndConsumeConditional(ref tag);
//        writeLine("  {");
//        writeLine("   return \"" + getNextString(tag) + "\";");
//        writeLine("  }");
//    }

//    private void readAdvices(string[] tags)
//    {
//        writeLine(" public override string getAdvice(string who) {");
//        foreach (string tag in tags)
//        {
//            if (tag.StartsWith("a ", System.StringComparison.OrdinalIgnoreCase))
//            {
//                readAdvice(tag.Substring(1));
//            }
//        }
//        writeLine(" return \"INSERT GENERAL ADVICE HERE\";");
//        writeLine(" }");
//    }

//    private void readAdvice(string tag)
//    {
//        Match m = Regex.Match(tag, @"([a-z]+)", RegexOptions.IgnoreCase);
//        if (m.Groups.Count == 0) return;

//        string who = m.Groups[0].Value;
//        writeLine("  if (who == \"" + who + "\") {");
//        checkAndConsumeConditional(ref tag);
//        writeLine("   {");
//        writeLine("    return \"" + getNextString(tag) + "\";");
//        writeLine("   }");
//        writeLine("  }");
//    }

//    private void checkAndConsumeConditional(ref string tag)
//    {
//        string cond = consumeCondition(ref tag);
//        if (cond != "")
//        {
//            writeLine("  if (" + cond + ")");
//        }
//    }

//    private void readProbabilities(string[] tags)
//    {
//        writeLine(" public override float calculateProbability() {");
//        writeLine("  float p = 1.0f;");
//        foreach (string tag in tags)
//        {
//            if (tag.StartsWith("p ", System.StringComparison.OrdinalIgnoreCase))
//            {
//                readProbability(tag.Substring(1));
//            }
//        }
//        writeLine("  return p;");
//        writeLine(" }");
//    }

//    private void readFilter(string mod)
//    {
//        writeLine("e.addFilter(\"" + getNextString(mod) + "\");");
//    }

//    private void readProbability(string tag)
//    {
//        checkAndConsumeConditional(ref tag);
//        writeLine("  {");
//        writeLine("   p = p * " + getNextValue(tag) + ";");
//        writeLine("  }");

//    }

//    private string getNextValue(string tag)
//    {
//        string value = tag.Substring(tag.IndexOf("=") + 1);
//        //value = Regex.Replace(value, "available", "event.available", RegexOptions.IgnoreCase);
//        return value.Substring(0, value.IndexOfAny(new char[]{'\n', '$'}));
//    }

//    private string consumeCondition(ref string mod)
//    {
//        int condStartPos = mod.IndexOf("{");
//        if (condStartPos == -1) return "";
//        int condEndPos = mod.IndexOf("}");
//        if (condEndPos == -1) return "";
//        string cond = mod.Substring(condStartPos + 1, (condEndPos - condStartPos - 1));// < 0 ? 0 : condEndPos - condStartPos - 1);

//        cond = Regex.Replace(cond, "[^<>=]=", "==");
//        cond = Regex.Replace(cond, "AND", "&&");
//        cond = Regex.Replace(cond, "OR", "||");
//        cond = Regex.Replace(cond, "NOT", "!=");
//        cond = Regex.Replace(cond, "NO", "!");
//        cond = Regex.Replace(cond, @"o( |=)", "outcome$1");
//        cond = Regex.Replace(cond, @"c( |=)", "choice$1");
//        cond = Regex.Replace(cond, "character", "getCharacter()", RegexOptions.IgnoreCase);
//        cond = Regex.Replace(cond, "advisor", "getAdvisor()", RegexOptions.IgnoreCase);
//        cond = Regex.Replace(cond, "locationObject", "locationObject", RegexOptions.IgnoreCase);
//        cond = Regex.Replace(cond, "gameTime", "getElapsedDays()", RegexOptions.IgnoreCase);
//        cond = Regex.Replace(cond, "finder.magnitude", "getWarpMagnitude()", RegexOptions.IgnoreCase);
//        //@todo
//        cond = Regex.Replace(cond, @"playerLoc\(.atLocation.,.*\)", "(getLocationID() == location)", RegexOptions.IgnoreCase);
//        //@todo
//        cond = Regex.Replace(cond, @"playerLoc\(.inLocation.,.*\)", "(getLocationID() == location)", RegexOptions.IgnoreCase);
//        cond = Regex.Replace(cond, @"Event\[(.*)\]", "getEvent($1)", RegexOptions.IgnoreCase);
//        cond = Regex.Replace(cond, @"theCaptain", "getCharacter(\"captain\")", RegexOptions.IgnoreCase);
//        cond = Regex.Replace(cond, @"location\.ideologyStats\[.([a-z]+).\]", "getLocation().ideology.effects.$1", RegexOptions.IgnoreCase);
        

//        mod = mod.Substring(condEndPos + 1);
//        return cond;
//    }

//    private void writeLine(string line)
//    {
//        generatedScript += line + "\n";
//    }

//    string getNextString(string data)
//    {
//        int stringStartPos = data.IndexOf("\"");
//        if (stringStartPos == -1) throw new System.Exception("Event generator throw");
//        int length = data.Substring(stringStartPos + 1).IndexOf("\"");
//        if (length == -1) throw new System.Exception("Event generator throw");
//        return data.Substring(stringStartPos + 1, length);
//    }
//}
