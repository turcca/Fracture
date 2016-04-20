using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class Mixer : MonoBehaviour {

    public AudioMixer masterMixer;
    public static Mixer Instance;

    private float masterVolumeSetting = 0;
    public float MasterVolumeSetting
    {
        get { return masterVolumeSetting; }
        set
        {
            SetMasterVolume(value);
            masterVolumeSetting = value;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// -80 to 0 (max) Mixer: exposed master volume parameter
    /// </summary>
    /// <param name="db"></param>
    private void SetMasterVolume(float db)
    {
        masterMixer.SetFloat("masterVolume", Mathf.Clamp(db, -80f, 0f));
    }

    public void pauseMaster(bool setPaused)
    {
        if (setPaused)
            SetMasterVolume(-80f);
        else
            SetMasterVolume(MasterVolumeSetting);
    }
}
