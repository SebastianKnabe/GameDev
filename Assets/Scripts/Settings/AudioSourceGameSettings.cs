using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceGameSettings : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = gameSettings.BGMVolume;
    }

    public void updateVolume()
    {
        audioSource.volume = gameSettings.BGMVolume * gameSettings.TotalVolume;
    }
}
