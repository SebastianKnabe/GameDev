using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private VoidEvent onInventoryItemsUpdated = null;

    public InventoryType inventoryType = InventoryType.Container;
    public ItemContainer ItemContainer { get; set; } = new ItemContainer(20);

    public void OnEnable() => ItemContainer.OnItemsUpdate += onInventoryItemsUpdated.Raise;

    public void OnDisable() => ItemContainer.OnItemsUpdate -= onInventoryItemsUpdated.Raise;

    public void setOnInventoryItemsUpdatedEvent(VoidEvent onInventoryItemsUpdatedEvent)
    {
        onInventoryItemsUpdated = onInventoryItemsUpdatedEvent;
    }

    [ContextMenu("Reset Inventory")]
    public void ResetInventory()
    {
        ItemContainer = new ItemContainer(20);
    }

    public void LoadInventory(SaveFile saveFile)
    {
        ItemSlot[] itemSlots = saveFile.itemSlots;
        ItemContainer = new ItemContainer(itemSlots.Length);
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot itemSlot = itemSlots[i];
            if (itemSlot.quantity > 0 && itemSlot.item.GetInstanceID() != 0)
            {
                ItemContainer.AddItemAtSlotIndex(itemSlot, i);
            }
        }
    }
}
