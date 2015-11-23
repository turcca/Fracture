using UnityEngine;
using System.Collections.Generic;

public static class AudioBank
{
    // keep audio clips in memory if you don't want to reload them on scene loads

    // clips
    static Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    // CLIP GROUPS
    // fracture ambiance
    public static string[] fractureAmbientClipNames;
    // location ambient
    public static string[] locationAmbientClipNames;
    // event ambient
    public static string[] eventAmbientClipNames;

    static AudioBank()
    {
        List<string[]> groups = new List<string[]>();

        // fracture
        fractureAmbientClipNames = new string[]
        {
            "Fracture_Low",
            "Fracture_Medium",
            "Fracture_High",
            "Fracture_Maximum"
        };
        groups.Add(fractureAmbientClipNames);

        // location
        locationAmbientClipNames = new string[]
        {
            "Shipyard",
            "TradeHub"
        };
        groups.Add(locationAmbientClipNames);

        // event
        eventAmbientClipNames = new string[]
        {
            "Default",
            "DefaultBridge"
        };
        groups.Add(eventAmbientClipNames);

        // add groups to clips
        addGroups(groups);
    }


    public static AudioClip getClip(string name)
    {
        if (clips.ContainsKey(name))
        {
            return clips[name];
        }
        else Debug.LogError("ERROR: clip not found: '" + name + "'");
        return null;
    }

    public static bool isAudioClipInAudioBank(AudioClip audio)
    {
        return clips.ContainsValue(audio) ? true : false;
    }

    // --------------------------------------------
    static void addAudioClip(string name)
    {
        if (!clips.ContainsKey(name))
        {

            AudioClip clip;

            // fracture
            if (contains(fractureAmbientClipNames, name)) {
                clip = Resources.Load<AudioClip>("sounds/fracture/" + name); // path
                if (clip != null) clips.Add(name, clip);
                else Debug.LogError("ERROR: unable to load from Resources: 'sounds/fracture/" + name + "'"); }

            // location
            else if (contains(locationAmbientClipNames, name))
            {
                clip = Resources.Load<AudioClip>("sounds/location/ambiance/" + name); // path
                if (clip != null) clips.Add(name, clip);
                else Debug.LogError("ERROR: unable to load from Resources: 'sounds/location/ambiance/" + name + "'");
            }

            // event
            else if (contains(eventAmbientClipNames, name))
            {
                clip = Resources.Load<AudioClip>("sounds/event/" + name); // path
                if (clip != null) clips.Add(name, clip);
                else Debug.LogError("ERROR: unable to load from Resources: 'sounds/event/" + name + "'");
            }

            else Debug.LogError("ERROR: tried to add ungrouped audio clip: '" + name + "'");
        }
        else Debug.LogError("ERROR: '"+name + "' already exists in 'clips'");
    }


    // ---------------------------------------------------------------------
    static void addGroups(List<string[]> groups)
    {
        foreach (string[] group in groups)
        {
            foreach (string name in group)
            {
                addAudioClip(name);
            }
        }
    }
    static bool contains(string[] group, string name)
    {
        foreach (string member in group)
        {
            if (member == name) return true;
        }
        return false;
    }
}
