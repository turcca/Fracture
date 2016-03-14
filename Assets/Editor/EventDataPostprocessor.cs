using UnityEngine;
using UnityEditor;
using System.Collections;

//public class EventDataPostprocessor : AssetPostprocessor
//{
//    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
//    {
//        Debug.Log("Imported Assets:");
//        foreach (string asset in importedAssets)
//        {
//            if (asset.EndsWith("events.txt"))
//            {
//                EventGenerator eg = new EventGenerator();
//                eg.generateEvents(asset);
//            }
//        }
//    }
//}

public class MenuExtensions : MonoBehaviour
{
    [MenuItem("Assets/Generate events", false, 0)]
    static void GenerateAssets()
    {
        Debug.Log("Generating events...");
        EventGenerator eg = new EventGenerator();
        string path = Application.dataPath + "/data/";
        string[] files = 
            {
            // all event .txt files to be read into events
            path +"events.txt"
            //path +"events_triggerPatch.txt",
            //path +"events_intro_valerian.txt"
        };
        foreach (string s in files) Debug.Log("Adding file: " + s);
        eg.generateEvents(files);
        Debug.Log("...done!");
    }
}