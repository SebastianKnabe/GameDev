using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    public float TotalVolume;
    public float BGMVolume;
    public float SFXVolume;

    public ItemSlot[] itemSlots;

    public void SetGameSettings(GameSettings settings)
    {
        TotalVolume = settings.TotalVolume;
        BGMVolume = settings.BGMVolume;
        SFXVolume = settings.SFXVolume;
    }

    public void SetPlayerInventory(Inventory playerInventory)
    {
        int containerSize = playerInventory.ItemContainer.ContainerSize();
        itemSlots = new ItemSlot[containerSize];
        for (int i = 0; i < containerSize; i++)
        {
            ItemSlot itemSlot = playerInventory.ItemContainer.getItemAtIndex(i);
            if (itemSlot.quantity > 0)
            {
                itemSlots[i] = itemSlot;
            }
        }
    }
}
