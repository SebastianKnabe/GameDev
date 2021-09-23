using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    public float TotalVolume;
    public float BGMVolume;
    public float SFXVolume;

    public void SetGameSettings(GameSettings settings)
    {
        TotalVolume = settings.TotalVolume;
        BGMVolume = settings.BGMVolume;
        SFXVolume = settings.SFXVolume;
    }
}
