using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixerGroup MusicMixer, SfxMixer;
    public Slider MusicSlider, SfxSlider;
    //rende il float del volume pari a trenta volte il logaritmo di uno slider
    public void SetMusicLevel(float slider)
    {
        MusicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(slider) * 30);
    }
    public void SetSFXLevel(float slider)
    {
        MusicMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(slider) * 30);
    }
}
