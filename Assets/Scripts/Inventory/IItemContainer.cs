using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemContainer
{
    ItemSlot AddItem(ItemSlot itemSlot);
    void RemoveItem(ItemSlot itemSlot);
    ItemSlot RemoveItemAtIndex(int slotIndex);
    void Swap(int indexOne, int indexTwo);
    bool HasItem(InventoryItem item);
    int GetTotalQuantity(InventoryItem item);
    ItemSlot getItemAtIndex(int slotIndex);
}
