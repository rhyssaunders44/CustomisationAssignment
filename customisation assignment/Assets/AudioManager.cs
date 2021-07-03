using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer masterAudio;
    [SerializeField] private float audioVolume;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle mutetoggle;
    [SerializeField] private float mutedVolume;

    private void Start()
    {
        mutedVolume = -80;
        volumeSlider.maxValue = 20;
        volumeSlider.minValue = -80;
    }

    //sets mute
    public void Mute(bool muted)
    {
        if (muted)
        {
            masterAudio.SetFloat("VolumePercent", mutedVolume);
        }
        else
        {
            masterAudio.SetFloat("VolumePercent", PlayerPrefs.GetFloat("currentVolume", audioVolume));
        }
    }

    //sets volume
    public void SetVolume()
    {
        audioVolume = volumeSlider.value;
        PlayerPrefs.SetFloat("currentVolume", audioVolume);
        masterAudio.SetFloat("VolumePercent", PlayerPrefs.GetFloat("currentVolume", audioVolume));
    }
}
