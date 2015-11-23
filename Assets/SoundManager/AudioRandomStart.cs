using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioRandomStart : MonoBehaviour
{
    public string clipName = "";
    AudioSource source;

    void OnEnable()
    {
        //begin();
        if (source == null) begin();
        else if (source != null && !source.isPlaying) begin();
    }

    public void begin()
    {
        if (clipName == "" && source != null && source.clip != null && AudioBank.isAudioClipInAudioBank(source.clip)) Debug.LogWarning("["+gameObject.name+"] Clip exists in AudioBank: load clip from here (by name), instead of assigning it to AudioSource.");

        if (source == null) source = gameObject.GetComponent<AudioSource>();
        
        if (source != null)
        {
            // if clip is loaded from AudioBank
            if (source.clip == null)
            {
                if (clipName == "") { Debug.LogError("ERROR: AudioSource.clip == null, but 'name' is empty!"); return; }
                source.clip = AudioBank.getClip(clipName);
            }
            //else if (clipName == "" && source.clip != null && AudioBank.isAudioClipInAudioBank(source.clip)) { clipName = source.clip.name; source.clip = AudioBank.getClip(clipName); Debug.LogWarning(" ---> fixed?"); }

            // clip is loaded, pick random position and play
            playFromRandom();
        }
        else Debug.LogError("ERROR: couldn't load AudioSource");
	}


    void playFromRandom()
    {
        source.time = source.clip.length * UnityEngine.Random.value * 0.85f;
        source.Play();
    }
}
