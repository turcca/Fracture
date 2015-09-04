using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour 
{
    //public AudioMixerSnapshot starmap;
    //public AudioMixer mixer;


    // fracture
    public AudioSource fractureLow;
    public AudioSource fractureMed;
    public AudioSource fractureHi;
    public AudioSource fractureMax;

    float lowMax = 1.0f;
    float mediumMax = 1.0f;
    float highMax = 1.0f;
    float maximumMax = 1.0f;

    float lowVol = 0.0f;
    float mediumVol = 0.0f;
    float highVol = 0.0f;
    float maximumVol = 0.0f;

	// Use this for initialization
	void Start () 
    {
        lowVol = 0.0f;
        mediumVol = 0.0f;
        highVol = 0.0f;
        maximumVol = 0.0f;
        fractureLow.volume = 0.0f;
        fractureMed.volume = 0.0f;
        fractureHi.volume = 0.0f;
        fractureMax.volume = 0.0f;

        fractureLow.Play();
        fractureMed.Play();
        fractureHi.Play();
        fractureMax.Play();
	}
	
	// Update is called once per frame
	void Update () 
    {
        updateFractureVolume();
	}


    void updateFractureVolume()
    {
        float mag = Root.game.player.warpMagnitude *6.0f;

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
        if (source == fractureMed)
        {
            mediumVol += (inputVolume - mediumVol) * Mathf.Min(Time.deltaTime*1.0f, 1.0f);
            fractureMed.volume = mediumVol;
        }
        if (source == fractureHi)
        {
            highVol += (inputVolume - highVol) * Mathf.Min(Time.deltaTime*1.0f, 1.0f);
            fractureHi.volume = highVol;
        }
        if (source == fractureMax)
        {
            maximumVol += (inputVolume - maximumVol) * Mathf.Min(Time.deltaTime*1.0f, 1.0f);
            fractureMax.volume = maximumVol;
        }
    }
}
