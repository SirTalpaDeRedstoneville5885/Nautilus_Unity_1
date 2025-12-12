using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
   public AudioMixerGroup MusicMixer, SfxMixer;
   public Slider MusicSlider, SfxSlider;

   public void SetMusicLevel(float slider)
    {
        MusicMixer.audioMixer.SetFloat("MusicVolume",Mathf.Log10(slider) * 20);
    }
 public void SetSFXLevel(float slider)
    {
        MusicMixer.audioMixer.SetFloat("SFXVolume",Mathf.Log10(slider) * 20);
    }
}
