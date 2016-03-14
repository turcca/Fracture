using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class EventAdviceTagParser : MonoBehaviour
{

    public bool parse = true;
    public bool write = true;
    public bool debugging = false;
    

    public string fileName = "/data/EventAdviceTags.txt";
    public string outputFolder = "/event/generated/";

    static public string tagKey = "&";

    //string eventAdviceTags;
    System.IO.StreamReader sr;
    public List<string> advicetagLine;
    List<string> tags = new List<string>();

    // Use this for initialization
    void Start()
    {

        advicetagLine = new List<string>();

        if (parse || write) loadAdviceTags();
    }


    void loadAdviceTags()
    {
        // parseEventAdviceTags.txt
        // StreamReader
        fileName = Application.dataPath + fileName;
        sr = File.OpenText(fileName);
        if (File.Exists(fileName))
        {
            initializeParse();
            //sr = new StreamReader(Application.dataPath + "/" + fileName);
        }
        else Debug.LogError(fileName + " was not found");
        //eventAdviceTags = sr.ReadToEnd();


        sr.Close();

        if (write) writeEventAdviceTags("EventAdviceTagsParsed.cs");
    }

    // ***************PARSER*********************************************************************

    void initializeParse()
    {
        // 
        string theTime = System.DateTime.Now.ToString("HH:mm:ss");
        string theDate = System.DateTime.Now.ToString("dd/MM/yyyy");

        ADD("// EventAdviceTagsParsed.cs compiled: " + theDate + " " + theTime);
        ADD("using UnityEngine;");
        ADD("using System.Collections.Generic;");
        ADD(" \n ");
        ADD("public static class EventAdviceTagsParsed");
        ADD("{");
        ADD(" \n");

        ADD("static public string getEventAdviceTag (string tagRecord, Character advisor)", 1);
        ADD("{", 1);
        ADD("// change first to lowercase for matching. return string will be checked where called", 2);
        ADD("if (char.IsUpper(tagRecord[0]) ) tagRecord = EventAdviceTags.lowercaseFirst(tagRecord);", 2);
        ADD(" \n");
        ADD("// looking for tag match", 2);
        ADD("");

        parseEventAdviceTags();

        ADD("}", 1);

        ADD("}");
    }

    void parseEventAdviceTags()
    {
        // string tagRecord, characterClass advisor

        string line; // = sr.ReadLine();
        bool recordingContent = false;
        bool lastLineStartsWithReturn = false;
        string tag = "first tag";


        while ((line = sr.ReadLine()) != null)
        {
            line = line.Trim();

            if (line.Length > 0 && !line.StartsWith("//"))
            {
                if (debugging) Debug.Log("parsing line: " + line);

                // tag
                if (line.StartsWith(tagKey))
                {
                    if (recordingContent)
                    {
                        // see that previous line started with default "return" without "ifs" to catch all paths (no fallthroughs)
                        if (!lastLineStartsWithReturn) Debug.LogWarning("WARNING: all return paths weren't closed by \"return\".     [tag: \"" + tagKey + " " + tag + " " + tagKey + "\"]");
                        // close previous tag
                        if (recordingContent) ADD("}", 2);
                        recordingContent = false;
                    }
                    if (line.EndsWith(tagKey))
                    {
                        tag = line.Substring(1, line.Length - 1 - 1);
                        tag = tag.Trim();
                        // lowercase first letter of tags
                        if (char.IsUpper(tag[0])) { Debug.Log("		MODIFY: first letter was uppercase: " + tag + ". Forcing it to lowercase."); tag = EventAdviceTags.lowercaseFirst(tag); }
                        // check for dubplicate tags
                        if (!tags.Contains(tag))
                        {
                            tags.Add(tag);
                            //if (debugging) Debug.Log("	tag: "+line+" (lastLineStartsWithReturn = "+lastLineStartsWithReturn+")");
                            // new tag
                            ADD("if (tagRecord == \"" + tag + "\")");
                            if (recordingContent) ADDprefix("else ");
                            ADDprefix("", 2);
                            recordingContent = true;
                            ADD("{", 2);
                        }
                        else Debug.LogWarning("WARNING: duplicate tag encountered:   " + tag);
                    }
                    else Debug.LogError("ERROR: line starts with \"" + tagKey + "\" but doesn't end in one: " + line);
                }
                // if -content
                else if (recordingContent)
                {
                    // convert skill values
                    line = EventSkillValueConvert.getSkillValueFromEntireLine(line);
                    // record line
                    ADD(line, 3);
                    // check if content closes all return paths
                    if (line.StartsWith("return")) { lastLineStartsWithReturn = true; if (debugging) Debug.Log(lastLineStartsWithReturn + ": true"); } else { lastLineStartsWithReturn = false; if (debugging) Debug.Log(lastLineStartsWithReturn + ": false"); }
                }
                else
                {
                    // lines outside tag content
                    Debug.Log("		SKIPPING: line outside tag content: " + line);
                }
            }
        }
        // close previous tag to end else ifs
        if (recordingContent)
        {
            ADD("}", 2);
            if (!lastLineStartsWithReturn) Debug.LogWarning("WARNING: all return paths weren't closed by \"return\".     [tag: \"" + tagKey + " " + tag + " " + tagKey + "\"]");
        }
        // add ending else (falls through)
        //if (recordingContent)
        //{
        ADD("\n");
        //ADD("else", 2);
        //ADD("{", 2);
        ADD("Debug.LogWarning(\"WARNING: tag \"+tagRecord+\" wasn't in 'EventAdviceTags.txt' --> 'EventAdviceTagsParsed.cs'\");", 2);
        ADD("return \"ERROR: " + tagKey + "\"+tagRecord+\"" + tagKey + "\";", 2);
        //ADD("}", 2);
        //}
    }


    // ******************************************************************************************


    void ADD(string txt, int tabs)
    {
        // adds tab inserts
        for (int i = 0; i < tabs; i++) txt = string.Format("\t{0}", txt);
        ADD(txt);
    }
    void ADD(string txt)
    {
        if (debugging) { Debug.Log("      Parser (adding): " + txt); }
        advicetagLine.Add(txt + "\n");	// "\n" adds line  (using instead of WriteLine, line endings are better this way)
    }
    void ADDprefix(string txt, int tabs = 0)
    {
        if (debugging) { Debug.Log("      Adding prefix: " + txt); }
        int count = advicetagLine.Count;
        if (count > 0)
        {
            count--;
            string str = txt + advicetagLine[count];
            for (int i = 0; i < tabs; i++) str = string.Format("\t{0}", str);
            advicetagLine[count] = str;
        }
        else Debug.LogError("ERROR: prefixing \"" + txt + "\" but no advicetagLine lines to prefix");
    }


    void writeEventAdviceTags(string file)
    {
        string path = Application.dataPath + outputFolder;
        // overwrite
        if (File.Exists(file))
        {
            Debug.Log("Overwriting " + file);
        }
        else Debug.Log("Creating new file to: " + path + file);

        System.IO.StreamWriter sr = File.CreateText(path + file);

        if (debugging) { Debug.Log("Writing " + file + " ..."); }


        // list tags
        sr.Write("\n/* TAGS (" + tags.Count + ")-------\n");
        sr.Write("\n");
        foreach (string t in tags)
        {
            sr.Write(t + "\n");
        }
        sr.Write("\n--------------- */\n\n\n");


        // write code
        foreach (string l in advicetagLine)
        {
            sr.Write(l);
        }





        //sr.WriteLine("Debug.Log(\"Debuggable: \");");

        //sr.WriteLine ("This is my file."); 
        //sr.WriteLine ("I can write ints {0} or floats {1}, and so on.", 1, 4.2); 
        sr.Close();
    }


}
