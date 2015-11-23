using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour 
{
    //public AudioMixerSnapshot starmap;
    public static AudioMixer mixer;

    // snapshots?

    
    // fracture
    Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();
    AudioSource fractureLow;
    AudioSource fractureMed;
    AudioSource fractureHi;
    AudioSource fractureMax;

    // sound settings access
    float lowMax = 1.0f;
    float mediumMax = 1.0f;
    float highMax = 1.0f;
    float maximumMax = 1.0f;

    float lowVol = 0.0f;
    float mediumVol = 0.0f;
    float highVol = 0.0f;
    float maximumVol = 0.0f;


    void Start () 
    {
        // initialize mixer
        GameObject mixerObj = (GameObject) Instantiate(Resources.Load("sounds/prefabs/Sounds_starmap"));
        mixerObj.name = "Sounds_starmap";
        mixerObj.gameObject.transform.parent = this.gameObject.transform;
        mixer = mixerObj.GetComponentInChildren<Mixer>().masterMixer;
        if (mixer == null) Debug.LogError("ERROR: mixer loaded unsuccessfully");

        createFractureSources();

        // load AudioBank clips
        if (fractureLow == null) fractureLow = getSource("Fracture_Low");
        if (fractureMed == null) fractureMed = getSource("Fracture_Medium");
        if (fractureHi == null) fractureHi = getSource("Fracture_High");
        if (fractureMax == null) fractureMax = getSource("Fracture_Maximum");

        lowVol = 0.0f;
        mediumVol = 0.0f;
        highVol = 0.0f;
        maximumVol = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GameState.isState(GameState.State.Starmap)) updateFractureVolume();
	}


    void updateFractureVolume()
    {
        float mag = Root.game.player.getWarpMagnitude();

        // Fracture: low
        if (mag < 0.4f) setVolume (fractureLow, 0.4f * lowMax);
            else if (mag <= 1.6f) setVolume (fractureLow, mag * lowMax * 0.5f +0.2f);
                else setVolume (fractureLow, lowMax);
        // Fracture: medium
        if (mag < 0.7f) setVolume (fractureMed, 0.0f);
            else if (mag <= 1.9f) setVolume (fractureMed, Mathf.Min((mag * mediumMax - 0.7f)*0.85f, 1.0f));
                else setVolume (fractureMed, mediumMax);
        // Fracture: high
        if (mag < 1.5f) setVolume (fractureHi, 0.0f);
            else if (mag <= 2.5f) setVolume (fractureHi, mag * highMax -1.5f);
                else setVolume (fractureHi, highMax);
        // Fracture: maximum
        if (mag < 2.5f) setVolume (fractureMax, 0.0f);
            else if (mag <= 3.75f) setVolume (fractureMax, (mag * maximumMax -2.5f) * 0.8f);
                else setVolume (fractureMax, maximumMax);
    }

    void setVolume (AudioSource source, float inputVolume)
    {
        if (source == fractureLow)
        {
            lowVol += (inputVolume - lowVol) * Mathf.Min(Time.deltaTime*1.0f, 1.0f);
            fractureLow.volume = lowVol;
        }
        else if (source == fractureMed)
        {
            mediumVol += (inputVolume - mediumVol) * Mathf.Min(Time.deltaTime*1.0f, 1.0f);
            fractureMed.volume = mediumVol;
        }
        else if (source == fractureHi)
        {
            highVol += (inputVolume - highVol) * Mathf.Min(Time.deltaTime*1.0f, 1.0f);
            fractureHi.volume = highVol;
        }
        else if (source == fractureMax)
        {
            maximumVol += (inputVolume - maximumVol) * Mathf.Min(Time.deltaTime*1.0f, 1.0f);
            fractureMax.volume = maximumVol;
        }
    }

    AudioSource getSource(string name)
    {
        if (sources.ContainsKey(name))
        {
            return sources[name];
        }
        Debug.LogError("ERROR: getSource didn't find created source named '"+name+"'");
        return null;
    }

    void createFractureSources()
    {
        foreach (string name in AudioBank.fractureAmbientClipNames)
        {
            // create gameobjects for each audio source -> under this gameobject, attach clip from AudioBank
            GameObject obj = new GameObject();
            obj.name = name;
            obj.transform.parent = Mixer.Instance.gameObject.transform;
            AudioSource audio = obj.gameObject.AddComponent<AudioSource>();
            audio.spatialBlend = 0f;
            audio.loop = true;
            audio.playOnAwake = false;
            audio.bypassReverbZones = true;
            audio.priority = 128;
            audio.maxDistance = 800f;
            audio.volume = 0.0f;
            audio.clip = AudioBank.getClip(name);
            if (mixer != null) audio.outputAudioMixerGroup = mixer.FindMatchingGroups("Fracture Noise")[0];

            sources.Add(name, audio);

            AudioRandomStart start = obj.gameObject.AddComponent<AudioRandomStart>();
            start.clipName = name;
            start.begin(); // plays audio from random pos
        }
    }
}
