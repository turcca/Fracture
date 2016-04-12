using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


public static class ExternalFiles
{
    public enum file { Config, Locations, Ships }

    private static Dictionary<file, string> persistentDataPaths = new Dictionary<file, string>()
    {
        { file.Config, Path.Combine(Application.persistentDataPath, "config.ini") },
        { file.Locations, Path.Combine(Application.persistentDataPath, "locations.ini") },
        { file.Ships, Path.Combine(Application.persistentDataPath, "ships.ini") }
    };
    private static Dictionary<file, string> resourcePaths = new Dictionary<file, string>()
    {
        //{ file.Settings, Path.Combine(Application.persistentDataPath, "settings.ini") },
        { file.Locations, "data/locations" },
        { file.Ships, "data/ships" }
    };

    private static Dictionary<string, Dictionary<string, string>> IniDictionary = new Dictionary<string, Dictionary<string, string>>();
    private static bool Initialized = false;

    /// <summary>
    /// Sections list
    /// </summary>
    public enum Sections
    {
        Graphics,
    }
    /// <summary>
    /// Keys list
    /// </summary>
    public enum Keys
    {
        /// <summary>
        /// true, false
        /// </summary>
        DisplayResolutionDialog,
        /// <summary>
        /// true, false
        /// </summary>
        Fullscreen,
        /// <summary>
        /// int
        /// </summary>
        Windowed_Mode_x,
        /// <summary>
        /// int
        /// </summary>
        Windowed_Mode_y,
    }
    public static bool fileExists(file file)
    {
#if UNITY_EDITOR
        // no need for files
        return true;
#endif
        return File.Exists(persistentDataPaths[file.Config]);
    }
    private static bool FirstRead()
    {
        if (File.Exists(persistentDataPaths[file.Config]))
        {
            using (StreamReader sr = new StreamReader(persistentDataPaths[file.Config]))
            {
                string line;
                string theSection = "";
                string theKey = "";
                string theValue = "";
                while (!string.IsNullOrEmpty(line = sr.ReadLine()) || line.StartsWith("#") == false) // #comments
                {
                    line.Trim();
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        theSection = line.Substring(1, line.Length - 2);
                    }
                    else
                    {
                        string[] ln = line.Split(new char[] { '=' });
                        theKey = ln[0].Trim();
                        theValue = ln[1].Trim();
                    }
                    if (theSection == "" || theKey == "" || theValue == "")
                        continue;
                    PopulateIni(theSection, theKey, theValue);
                }
            }
        }
        return true;
    }

    private static void PopulateIni(string _Section, string _Key, string _Value)
    {
        if (IniDictionary.ContainsKey(_Section))
        {
            if (IniDictionary[_Section].ContainsKey(_Key))
                IniDictionary[_Section][_Key] = _Value;
            else
                IniDictionary[_Section].Add(_Key, _Value);
        }
        else
        {
            Dictionary<string, string> newValue = new Dictionary<string, string>();
            newValue.Add(_Key.ToString(), _Value);
            IniDictionary.Add(_Section.ToString(), newValue);
        }
    }
    /// <summary>
    /// Write data to INI file. Section and Key no in enum.
    /// </summary>
    /// <param name="_Section"></param>
    /// <param name="_Key"></param>
    /// <param name="_Value"></param>
    public static void IniWriteValue(string _Section, string _Key, string _Value)
    {
        if (!Initialized)
            FirstRead();
        PopulateIni(_Section, _Key, _Value);
        //write ini
        WriteIni();
    }
    /// <summary>
    /// Write data to INI file. Section and Key bound by enum
    /// </summary>
    /// <param name="_Section"></param>
    /// <param name="_Key"></param>
    /// <param name="_Value"></param>
    public static void IniWriteValue(Sections _Section, Keys _Key, string _Value)
    {
        IniWriteValue(_Section.ToString(), _Key.ToString(), _Value);
    }

    private static void WriteIni()
    {
        using (StreamWriter sw = new StreamWriter(persistentDataPaths[file.Config]))
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> sections in IniDictionary)
            {
                sw.WriteLine("[" + sections.Key.ToString() + "]");
                foreach (KeyValuePair<string, string> entry in sections.Value)
                {
                    // value must be in one line
                    string value = entry.Value.ToString();
                    value = value.Replace(Environment.NewLine, " ");
                    value = value.Replace("\r\n", " ");
                    sw.WriteLine(entry.Key.ToString() + " = " + value);
                }
            }
        }
    }
    
    private static void WriteIni(file file, string input)
    {
        using (StreamWriter sw = new StreamWriter(persistentDataPaths[file]))
            sw.WriteLine(input);
    }
    /// <summary>
    /// read an ini file. If running in build and ini -file doesn't exist, it will be loaded from resource/data
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string IniReadFile(file file)
    {
        if (file == file.Config) { Debug.LogError("Need to use IniReadValue('section', 'key')"); return null; }

#if UNITY_EDITOR
        // only load data from resources
        Debug.Log("UNITY_EDITOR: loading data from resources: " + persistentDataPaths[file]);
        if (resourcePaths.ContainsKey(file) == false) Debug.LogError("No file key: " + file);
        string rs = ((TextAsset)Resources.Load<TextAsset>(resourcePaths[file])).text; // resource-file needs to be TextAsset format (using .txt)
        if (rs == null)
        {
            Debug.LogError("Error loading resources for [" + file + "] from path (" + resourcePaths[file] + ")");
            return null;
        }
        else return rs;
#endif
        string returnString = "";
        if (File.Exists(persistentDataPaths[file]) == false)
        {
            Debug.Log("BUILD: Creating file: " + file.ToString().ToLower() + ".ini");
            returnString = ((TextAsset)Resources.Load<TextAsset>(resourcePaths[file])).text;
            // create file from resources
            WriteIni(file, returnString);
            return returnString;
        }
        Debug.Log("BUILD: "+file.ToString().ToLower()+".ini file exists and is being read: (" + persistentDataPaths[file]+")");
        returnString = File.ReadAllText(persistentDataPaths[file]);

        return returnString;
    }
    /// <summary>
    /// Read data from INI file. Section and Key bound by enum
    /// </summary>
    /// <param name="_Section"></param>
    /// <param name="_Key"></param>
    /// <returns></returns>
    public static string IniReadValue(Sections _Section, Keys _Key)
    {
        if (!Initialized)
            FirstRead();
        return IniReadValue(_Section.ToString(), _Key.ToString());
    }
    /// <summary>
    /// Read data from INI file. Section and Key no in enum.
    /// </summary>
    /// <param name="_Section"></param>
    /// <param name="_Key"></param>
    /// <returns></returns>
    private static string IniReadValue(string _Section, string _Key)
    {
        if (!Initialized)
            FirstRead();
        if (IniDictionary.ContainsKey(_Section))
            if (IniDictionary[_Section].ContainsKey(_Key))
                return IniDictionary[_Section][_Key];
        return null;
    }
}
     
