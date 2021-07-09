using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceGameSettings : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private bool isSFX;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (isSFX)
        {
            audioSource.volume = gameSettings.SFXVolume * gameSettings.TotalVolume;
        }
        else
        {
            audioSource.volume = gameSettings.BGMVolume * gameSettings.TotalVolume;
        }
    }

    public void updateVolume()
    {
        if (isSFX)
        {
            audioSource.volume = gameSettings.SFXVolume * gameSettings.TotalVolume;
        }
        else
        {
            audioSource.volume = gameSettings.BGMVolume * gameSettings.TotalVolume;
        }
    }
}
