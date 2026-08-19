using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]private Slider backgroundAudioSlider;
    [SerializeField]private Slider sfxAudioSlider;
    [SerializeField]private AudioSource backgroundAudioSource;
    [SerializeField]private AudioSource sfxAudioSource;
    void Start()
    {
        float backgroundVolume=PlayerPrefs.GetFloat("BackgroundVolume",1f);
        float sfxVolume=PlayerPrefs.GetFloat("SFXVolume",1f);
        backgroundAudioSource.volume=backgroundVolume;
        sfxAudioSource.volume=sfxVolume;
        backgroundAudioSlider.value=backgroundVolume;
        sfxAudioSlider.value=sfxVolume;
        backgroundAudioSlider.onValueChanged.AddListener(SetBackgroundVolume);
        sfxAudioSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    void SetBackgroundVolume(float volume)
    {
        backgroundAudioSource.volume=volume;
        PlayerPrefs.SetFloat("BackgroundVolume",volume);
        PlayerPrefs.Save();
    }
    void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume=volume;
        PlayerPrefs.SetFloat("SFXVolume",volume);
        PlayerPrefs.Save();
    }
}
