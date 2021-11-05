using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] [Range(0, 1)] public float TotalVolume = 1f;
    [SerializeField] [Range(0, 1)] public float BGMVolume = 0.2f;
    [SerializeField] [Range(0, 1)] public float SFXVolume = 0.2f;

    [SerializeField] private VoidEvent audioSettingsChanged;

    public void LoadSaveFile(SaveFile saveFile)
    {
        TotalVolume = saveFile.TotalVolume;
        BGMVolume = saveFile.BGMVolume;
        SFXVolume = saveFile.SFXVolume;
        audioSettingsChanged.Raise();
    }

    public void ChangeTotaleVolume(float value)
    {
        TotalVolume = value;
        audioSettingsChanged.Raise();
    }

    public void ChangeBGMVolume(float value)
    {
        BGMVolume = value;
        audioSettingsChanged.Raise();
    }
    public void ChangeSFXVolume(float value)
    {
        SFXVolume = value;
        audioSettingsChanged.Raise();
    }
}
