using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class HotbarItem : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private new string name = "New Hotbar Item Name";
    [SerializeField] private Sprite icon;

    public string Name => name;
    public abstract string ColouredName  { get; }
    public Sprite Icon => icon;

    public abstract string GetInfoDisplayText();
}
