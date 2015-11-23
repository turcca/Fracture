using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class Mixer : MonoBehaviour {

    public AudioMixer masterMixer;
    public static Mixer Instance;

    void Awake()
    {
        Instance = this;
    }
}
