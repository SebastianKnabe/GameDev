using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private bool isTotalVolume = false;
    [SerializeField] private bool isBGMVolume = false;
    [SerializeField] private bool isSFXVolume = false;

    public void Start()
    {
        SetVolume();
    }

    private void OnEnable()
    {
        SetVolume();
    }

    private void SetVolume()
    {
        if (isTotalVolume)
        {
            slider.value = gameSettings.TotalVolume;
        }
        else if (isBGMVolume)
        {
            slider.value = gameSettings.BGMVolume;
        }
        else if (isSFXVolume)
        {
            slider.value = gameSettings.SFXVolume;
        }
    }
}
