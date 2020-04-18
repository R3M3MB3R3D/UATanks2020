using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Slider effects;
    public Slider music;
    public AudioMixer mixer;

    void Start()
    {
        //if we have preferences
        if (PlayerPrefs.HasKey("eVolume"))
        {
            //remember the preferences.
            effects.value = PlayerPrefs.GetFloat("eVolume");
        }
        //if not
        else
        {
            //create a preference.
            effects.value = 1;
            PlayerPrefs.SetFloat("eVolume", (effects.value));
        }
        if (PlayerPrefs.HasKey("mVolume"))
        {
            music.value = PlayerPrefs.GetFloat("mVolume");
        }
        else
        {
            music.value = 1;
            PlayerPrefs.SetFloat("mVolume", (music.value));
        }
        PlayerPrefs.Save();
        //Debug.Log(PlayerPrefs.GetFloat("mVolume"));
        setVolume();
    }

    public void setVolume()
    {
        PlayerPrefs.SetFloat("eVolume", effects.value);
        PlayerPrefs.SetFloat("mVolume", music.value);
        mixer.SetFloat("Effects Volume", conversion(effects.value));
        mixer.SetFloat("Music Volume", conversion(music.value));
        //Debug.Log(PlayerPrefs.GetFloat("mVolume") + " / " + (music.value));
    }

    public float conversion(float i)
    {
        //create a decibel level
        float db;

        //if sliders are more than 0
        if (i != 0)
        {
            //escalate accordingly
            db = 20 * Mathf.Log10(i);
        }
        else
        {
            //no sound
            db = -80;
        }
        return db;
    }
}
