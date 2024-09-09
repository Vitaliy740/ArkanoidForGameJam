using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Zenject;
public class SettingsMenu : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SFXSlider;
    public AudioMixer Mixer;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("musicVolume")) 
        {
            LoadVolume();
        }
        else 
        {
           SetMusicVolume();
        }
    }
    public void SetMusicVolume() 
    {
        //Mixer.SetFloat("MusicVolume", Mathf.Log10( MusicSlider.value)*20f);
        //PlayerPrefs.SetFloat("musicVolume", MusicSlider.value);
    }
    public void SetSFXVolume()
    {
        Mixer.SetFloat("SFXVoulume", Mathf.Log10(SFXSlider.value) * 20f);
        PlayerPrefs.SetFloat("sFXVolume", SFXSlider.value);
    }
    private void LoadVolume() 
    {
        //MusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
        SFXSlider.value = PlayerPrefs.GetFloat("sFXVolume");
        SetSFXVolume();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
