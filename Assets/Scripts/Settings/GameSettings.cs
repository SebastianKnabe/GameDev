using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] [Range(0, 1)] public float TotalVolume = 1f;
    [SerializeField] [Range(0, 1)] public float BGMVolume = 0.2f;
    [SerializeField] [Range(0, 1)] public float SFXVolume = 0.2f;
}
