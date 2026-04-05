using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsBehavior : MonoBehaviour
{
    public AudioMixer Mixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume(Slider slider)
    {
        SetVolumeLinear("Master", slider.value / 100.0f);
    }

    public void SetSFXVolume(Slider slider)
    {
        SetVolumeLinear("SFX", slider.value / 100.0f);
    }

    public void SetMusicVolume(Slider slider)
    {
        SetVolumeLinear("Music", slider.value / 100.0f);
    }

    private void SetVolumeLinear(string bus, float volume)
    {
        var expVolume = -80f;
        if (volume >= 1e-9)
            expVolume = 10.0f * (float)Math.Log10(volume);
        Mixer.SetFloat(bus + "Volume", expVolume);
        float volumeResult;
        Mixer.GetFloat(bus + "Volume", out volumeResult);
        Debug.Log($"{bus} volume set to {volumeResult}");
    }
}
